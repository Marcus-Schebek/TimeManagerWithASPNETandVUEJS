using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    // Login Model serivirá para passar como parametros para passar para a rota de Login
    public class LoginModel
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
