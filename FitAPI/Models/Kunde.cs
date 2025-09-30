using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FitAPI.Services;


namespace FitAPI.Models;

public class Kunde : IHasPassword
{
    public int Id { get; set; }

    [Required] public string Vorname { get; set; }
    [Required] public string Name { get; set; }

    [Required] public DateTimeOffset MemberSince { get; set; }
    [Required] public DateTimeOffset SubscriptionValidUntil { get; set; } 

    [Required, EmailAddress] public string Email { get; set; }
    public string? Phone { get; set; }

    [Required] public string PasswordHash { get; set; }
}
