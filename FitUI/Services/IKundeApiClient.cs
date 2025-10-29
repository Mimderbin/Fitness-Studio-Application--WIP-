using FitUI.Models;

namespace FitUI.Services;

public interface IKundeApiClient
{
    Task<IReadOnlyList<KundeListItemViewModel>> GetAllAsync(CancellationToken ct = default);
    Task<KundeEditViewModel?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<int> CreateAsync(KundeCreateViewModel vm, CancellationToken ct = default);
    Task UpdateAsync(int id, KundeEditViewModel vm, CancellationToken ct = default);
    Task ChangePasswordAsync(int id, KundeChangePasswordViewModel vm, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}

