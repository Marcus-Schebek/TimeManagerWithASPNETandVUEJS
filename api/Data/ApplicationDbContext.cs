using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    // Define o contexto do banco de dados
    public class ApplicationDbContext : DbContext
    {
        // Construtor que aceita opções de contexto e passa para a classe base
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet para a entidade User
        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<WorkInterval> WorkIntervals { get; set; }

    }
}
