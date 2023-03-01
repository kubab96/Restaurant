using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;   
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            _accountService.Register(registerUserDTO);
            
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var token = _accountService.GenerateJwtToken(loginDTO);

            return Ok(token);
        }
    }
}
