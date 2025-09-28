using Microsoft.EntityFrameworkCore;
using FitAPI.Data;

namespace FitAPI.Services;

public class ValidationService<T> : IValidationService<T> where T : class
{
    private readonly AppDbContext _context;

    public ValidationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> EntityExists(int key, CancellationToken ct)
    {
        return await _context.Set<T>()
            .AnyAsync(e => EF.Property<int>(e, "Id") == key, ct);
    }
}