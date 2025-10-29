using System.Text.Json;
using System.Text.Json.Serialization;
using FitUI.Models;

namespace FitUI.Services;

public sealed class KundeApiClient : IKundeApiClient
    {
        private readonly HttpClient _http;
        private static readonly HttpMethod Patch = new("PATCH");
        
        private sealed record ODataList<T>([property: JsonPropertyName("value")] T[] Value);
        private sealed record IdOnly(int Id);

        public KundeApiClient(HttpClient http)
        {
            _http = http;
            _http.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        }

        public async Task<IReadOnlyList<KundeListItemViewModel>> GetAllAsync(CancellationToken ct = default)
        {
            var res = await _http.GetFromJsonAsync<ODataList<KundeListItemViewModel>>("Kunden", ct);
            return res?.Value ?? Array.Empty<KundeListItemViewModel>();
        }

        public async Task<KundeEditViewModel?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _http.GetFromJsonAsync<KundeEditViewModel>($"Kunden({id})", ct);
        }

        public async Task<int> CreateAsync(KundeCreateViewModel vm, CancellationToken ct = default)
        {
            var payload = new
            {
                vm.Vorname,
                Name = vm.Name,
                vm.MemberSince,
                vm.SubscriptionValidUntil,
                vm.Email,
                vm.Phone,
                PasswordHash = vm.Password  // plain; API hashes
            };

            var resp = await _http.PostAsJsonAsync("Kunden", payload, ct);
            resp.EnsureSuccessStatusCode();

            var created = await resp.Content.ReadFromJsonAsync<IdOnly>(cancellationToken: ct);
            return created?.Id ?? 0;
        }

        public async Task UpdateAsync(int id, KundeEditViewModel vm, CancellationToken ct = default)
        {
            var payload = new
            {
                vm.Vorname,
                Name = vm.Name,
                vm.MemberSince,
                vm.SubscriptionValidUntil,
                vm.Email,
                vm.Phone
            };

            var resp = await PatchJsonAsync($"Kunden({id})", payload, ct);
            resp.EnsureSuccessStatusCode();
        }

        public async Task ChangePasswordAsync(int id, KundeChangePasswordViewModel vm, CancellationToken ct = default)
        {
            var payload = new { PasswordHash = vm.NewPassword };
            var resp = await PatchJsonAsync($"Kunden({id})", payload, ct);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var resp = await _http.DeleteAsync($"Kunden({id})", ct);
            resp.EnsureSuccessStatusCode();
        }

        // --- helpers ----
        private async Task<HttpResponseMessage> PatchJsonAsync(string url, object body, CancellationToken ct)
        {
            var req = new HttpRequestMessage(Patch, url)
            {
                Content = JsonContent.Create(body, options: SerializerOptions)
            };
            return await _http.SendAsync(req, ct);
        }

        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        
    }