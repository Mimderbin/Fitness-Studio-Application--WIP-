using FitAPI.Data;
using FitAPI.Models;
using FitAPI.Services;

namespace FitAPI.Controllers
{
    public class KundenController : UserController<Kunde>
    {
        public KundenController(
            AppDbContext context,
            IValidationService<Kunde> validation,
            IPasswordService passwordService)
            : base(context, validation, passwordService) { }
    }
}