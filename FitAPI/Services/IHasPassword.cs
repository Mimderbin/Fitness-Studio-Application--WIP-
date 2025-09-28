namespace FitAPI.Services;

public interface IHasPassword
{
    string PasswordHash { get; set; }
}
