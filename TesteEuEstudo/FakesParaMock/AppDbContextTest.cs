using EuEstudo.Dados;
using EuEstudo.Models;
using Microsoft.EntityFrameworkCore;

namespace TesteEuEstudo.FakesParaMock
{
    public class AppDbContextTest 
    {
        private readonly AppDbContext _dbContext;
        public AppDbContextTest() 
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "NaMemoriaAppDatabase")
            .Options;

            _dbContext = new AppDbContext(options);

            InitializeData();

        }
        private void InitializeData()
        {
            var disciplina1 = new DisciplinaModel { Nome = "Disciplina 1", UsuarioId = "123" };
            var disciplina2 = new DisciplinaModel { Nome = "Disciplina 2", UsuarioId = "123" };
            var pergunta1 = new PerguntasModel { Questao = "Teste", Resposta = "Teste", DisciplinaId=44 };
            _dbContext.Disciplinas.Add(disciplina1);
            _dbContext.Disciplinas.Add(disciplina2);
            _dbContext.Perguntas.Add(pergunta1);
            _dbContext.SaveChanges();
        }

        public AppDbContext GetContext()
        {
            return _dbContext;
        }
    }
}
