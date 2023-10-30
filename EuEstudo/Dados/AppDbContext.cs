using EuEstudo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EuEstudo.Dados
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<PerguntasModel> Perguntas { get; set; }
        public DbSet<DisciplinaModel> Disciplinas { get; set;}
    }
}
