using System.ComponentModel.DataAnnotations;


namespace FitAPI.Models;

public class Kunde
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Vorname { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public DateTime MemberSince {get; set;}
    
    [Required]
    public DateTime SubscriptionValidUntil { get; set;}
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    public string? Phone { get; set; }
    
}