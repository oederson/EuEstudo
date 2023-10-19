using EuEstudo.Models;
using Microsoft.EntityFrameworkCore;

namespace EuEstudo.Dados
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<PerguntasModel> Perguntas { get; set; }
    }
}
