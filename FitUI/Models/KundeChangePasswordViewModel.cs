using System.ComponentModel.DataAnnotations;

namespace FitUI.Models;

public class KundeChangePasswordViewModel
{
    public int Id { get; set; }

    [Required, MinLength(8)]
    public string NewPassword { get; set; }

    [Required, Compare(nameof(NewPassword))]
    public string ConfirmNewPassword { get; set; }
}