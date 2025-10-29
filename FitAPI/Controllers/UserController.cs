using FitAPI.Controllers;
using FitAPI.Data;
using FitAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;

public class UserController<T> : CrudController<T> where T : class, IHasPassword
{
    private readonly IPasswordService _passwordService;

    public UserController(AppDbContext context, IValidationService<T> validation, IPasswordService passwordService)
        : base(context, validation)
    {
        _passwordService = passwordService;
    }

    public override async Task<IActionResult> Post([FromBody] T entity, CancellationToken ct = default)
    {
        if (entity == null)
            return BadRequest("Request body is missing.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        entity.PasswordHash = _passwordService.HashPassword(entity.PasswordHash);

        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync(ct);

        return Created(entity);
    }

    public override async Task<IActionResult> Put([FromODataUri] int key, [FromBody] T entity, CancellationToken ct = default)
    {
        if (entity == null)
            return BadRequest("Request body is missing.");
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (EF.Property<int>(entity, "Id") != key)
            return BadRequest("Entity ID in URL does not match request body.");
        
        entity.PasswordHash = _passwordService.HashPassword(entity.PasswordHash);

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
}