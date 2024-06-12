using api.Repositories;
using api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Services
{
    // Serviço que fornece a lógica de autenticação e registro de usuários
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecret;

        // Construtor que injeta o repositório de usuários e a chave secreta JWT
        public AuthService(IUserRepository userRepository, string jwtSecret)
        {
            _userRepository = userRepository;
            _jwtSecret = jwtSecret;
        }

        // Método assíncrono para registrar um novo usuário
        public async Task<User> RegisterAsync(User user)
        {
            // Criptografa a senha antes de salvar
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return user;
        }

        // Método assíncrono para fazer login e gerar um token JWT
        public async Task<(int Id, string? Token)> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return (0, null);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Token válido por 7 dias
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return (user.Id, tokenString);
        }
    }
}
