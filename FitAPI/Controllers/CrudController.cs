using FitAPI.Data;
using FitAPI.Services;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.EntityFrameworkCore;

namespace FitAPI.Controllers;

public class CrudController<T> : ODataController where T : class
{
    protected readonly AppDbContext _context;
    protected readonly IValidationService<T> _validation;

    public CrudController(AppDbContext context, IValidationService<T> validation)
    {
        _context = context;
        _validation = validation;
    }
    // GET COLLECLTION
    [EnableQuery ]
    public virtual IQueryable<T> Get()
    {
        return _context.Set<T>().AsNoTracking();
    }
    // GET SINGLE
    [EnableQuery]
    public virtual SingleResult<T> Get([FromODataUri] int key)
    {
        var result = _context.Set<T>()
            .AsNoTracking()
            .Where(e => EF.Property<int>(e, "Id") == key);
        return SingleResult.Create(result);
    }
    // POST
    public virtual async Task<IActionResult> Post([FromBody] T entity, CancellationToken ct)
    {
        if (entity == null)
            return BadRequest("Request body is missing.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync(ct);

        return Created(entity);
    }
    // PATCH
    public virtual async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<T> patch, CancellationToken ct)
    {
        if (patch == null)
            return BadRequest("Request body is missing.");
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = await _context.Set<T>().FindAsync(new object[] { key }, ct);
        if (entity == null)
            return NotFound();

        patch.Patch(entity);

        try
        {
            await _context.SaveChangesAsync(ct);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _validation.EntityExists(key, ct))
                return NotFound();
            throw;
        }

        return Updated(entity);
    }
    // PUT
public virtual async Task<IActionResult> Put([FromODataUri] int key, [FromBody] T entity, CancellationToken ct)
{
    if (entity == null)
        return BadRequest("Request body is missing.");
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var idProp = typeof(T).GetProperty("Id");
    if (idProp == null)
        return BadRequest("Entity type does not have an Id property.");

    var idValue = idProp.GetValue(entity);
    if (idValue == null || !int.TryParse(idValue.ToString(), out var entityId))
        return BadRequest("Entity Id is missing or not an integer.");

    if (entityId != key)
        return BadRequest("Entity ID in URL does not match request body.");

    _context.Entry(entity).State = EntityState.Modified;
    try
    {
        await _context.SaveChangesAsync(ct);
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!await _validation.EntityExists(key, ct))
            return NotFound();
        throw;
    }
    return Updated(entity);
}
    // DELETE
    public virtual async Task<IActionResult> Delete([FromODataUri] int key, CancellationToken ct)
    {
        var entity = await _context.Set<T>().FindAsync(new object[] { key }, ct);
        
        if (entity == null) {
            return NotFound();
        }
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(ct);
        return NoContent();
    }

}

