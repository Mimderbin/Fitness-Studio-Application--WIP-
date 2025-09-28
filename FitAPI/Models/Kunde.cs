using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FitAPI.Services;


namespace FitAPI.Models;

public class Kunde : IHasPassword
{
    public int Id { get; set; }

    [Required]
    public string Vorname { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime MemberSince { get; set; }

    [Required]
    public DateTime SubscriptionValidUntil { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string? Phone { get; set; }
    
    [JsonIgnore]
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
}
