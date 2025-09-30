using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FitAPI.Services;

namespace FitAPI.Models;

public class Admin : IHasPassword
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } 

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string PasswordHash { get; set; } 
}
