using FitAPI.Data;
using FitAPI.Models;
using FitAPI.Services;

namespace FitAPI.Controllers;

public class AdminsController : UserController<Admin>
{
    public AdminsController(
        AppDbContext context,
        IValidationService<Admin> validation,
        IPasswordService passwordService)
        : base(context, validation, passwordService) { }
}
