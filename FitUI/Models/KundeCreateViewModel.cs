using System.ComponentModel.DataAnnotations;

namespace FitUI.Models;

public class KundeCreateViewModel
{
    [Required] public string Vorname { get; set; }
    [Required] public string Name { get; set; }

    [Required] public DateTimeOffset MemberSince { get; set; }
    [Required] public DateTimeOffset SubscriptionValidUntil { get; set; }

    [Required, EmailAddress] public string Email { get; set; }
    public string? Phone { get; set; }

    [Required, MinLength(8)]
    public string Password { get; set; }

    [Required, Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
}