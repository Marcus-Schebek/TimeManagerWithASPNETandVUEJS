using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        // Construtor que injeta o serviço de autenticação
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // Caminho da API para cadastrar um usuário
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var registeredUser = await _authService.RegisterAsync(user);
            return Ok(registeredUser);
        }

        // Caminho da API para logar um usuário
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginUser)
        {
            var (id, token) = await _authService.LoginAsync(loginUser.Email, loginUser.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(new { Id = id, Token = token });
        }
    }
}
