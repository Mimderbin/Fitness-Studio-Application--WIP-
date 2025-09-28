using Microsoft.EntityFrameworkCore;
using FitAPI.Data;

namespace FitAPI.Services;

public interface IValidationService<T> where T : class
{
    Task<bool> EntityExists(int key, CancellationToken ct);
}