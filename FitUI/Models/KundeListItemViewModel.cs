namespace FitUI.Models;

public class KundeListItemViewModel
{
    public int Id { get; set; }
    public string Vorname { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTimeOffset MemberSince { get; set; }
    public DateTimeOffset SubscriptionValidUntil { get; set; }
    public string? Phone { get; set; }
}