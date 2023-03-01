using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IAccountService
    {
        string GenerateJwtToken(LoginDTO loginDTO);
        void Register(RegisterUserDTO registerUserDTO);
    }
}