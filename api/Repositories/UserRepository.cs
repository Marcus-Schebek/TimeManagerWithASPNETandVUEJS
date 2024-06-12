using api.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    // Implementa a interface IUserRepository e fornece a lógica de acesso aos dados para a entidade User
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        // Construtor que injeta o contexto do banco de dados
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método assíncrono para obter um usuário pelo email
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        // Método assíncrono para adicionar um novo usuário
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        // Método assíncrono para salvar as mudanças no banco de dados
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Método assíncrono para obter todos os usuários
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Método assíncrono para obter um usuário pelo ID
        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Método assíncrono para atualizar um usuário
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await Task.CompletedTask; // Adiciona uma tarefa completada para satisfazer a assinatura assíncrona
        }

        // Método assíncrono para deletar um usuário
        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await Task.CompletedTask; // Adiciona uma tarefa completada para satisfazer a assinatura assíncrona
        }
    }
}
