using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        // Construtor que injeta o repositório de usuários
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Rota GET para obter todos os usuários
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        // Rota GET para obter um usuário pelo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // Rota PUT para atualizar um usuário
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
            return NoContent();
        }

        // Rota DELETE para deletar um usuário
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userRepository.DeleteAsync(user);
            await _userRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}
