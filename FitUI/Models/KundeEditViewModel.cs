using System.ComponentModel.DataAnnotations;

namespace FitUI.Models;

public class KundeEditViewModel
{
    public int Id { get; set; }

    [Required] public string Vorname { get; set; }
    [Required] public string Name { get; set; }

    [Required] public DateTimeOffset MemberSince { get; set; }
    [Required] public DateTimeOffset SubscriptionValidUntil { get; set; }

    [Required, EmailAddress] public string Email { get; set; }
    public string? Phone { get; set; }
}