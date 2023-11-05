using EuEstudo.Controllers;
using EuEstudo.Dados;
using EuEstudo.Models;
using EuEstudo.Service;
using Microsoft.AspNetCore.Mvc;
using TesteEuEstudo.FakesParaMock;

namespace TesteEuEstudo
{
    public class PerguntaControllerTests
    {
        private readonly AppDbContext _dbContext;
        private readonly PerguntasService _perguntasService;
        private readonly PerguntasController _perguntasController;

        public PerguntaControllerTests()
        {
            var initializer = new AppDbContextTest();
            _dbContext = initializer.GetContext();
            _perguntasService = new PerguntasService(_dbContext);
            _perguntasController = new PerguntasController(_perguntasService);        
        }

        [Fact]
        public void CadastrarUmaPerguntaComSucesso() 
        {
            //Arrange
            PerguntasModel perguntas = new PerguntasModel() 
            {
                Questao = "Teste",
                Resposta = "Resposta teste",
                DisciplinaId = 1
            };
            //Act
            var result = _perguntasController.Cadastrar(perguntas) as RedirectToActionResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); 
            Assert.Equal("Disciplina", result.ControllerName);
            _dbContext.Dispose();
        }

        [Fact]
        public void NaoCadastrarUmaPerguntaSemQuestao()
        {
            //Arrange
            PerguntasModel perguntas = new PerguntasModel()
            {                
                Resposta = "Resposta teste",
                DisciplinaId = 1
            };
            //Act
            var result = _perguntasController.Cadastrar(perguntas) as RedirectToActionResult;
            //Assert
            Assert.NotNull(result); 
            Assert.Equal("Perguntas", result.ControllerName); 
            _dbContext.Dispose();
        }
       
        [Fact]
        public void EditarUmPergunta() 
        {
            //Arrange
            PerguntasModel perguntas = new PerguntasModel()
            {
                Id = 2,
                Questao = "Teste Editado",
                Resposta = "Resposta teste editado",
                DisciplinaId = 44
            };
            //Act
            var result = _perguntasController.Editar(perguntas) as RedirectToActionResult;
            //Assert
            Assert.NotNull(result); 
            Assert.Equal("ListarPorDisciplinaId", result.ActionName); 
            Assert.Equal("Perguntas", result.ControllerName); 
            _dbContext.Dispose();
        }

        [Fact]
        public void NaoEditarUmPergunta()
        {
            //Arrange
            PerguntasModel perguntas = new PerguntasModel()
            {
                Id = 2,
                DisciplinaId = 44
            };
            //Act
            var result = _perguntasController.Editar(perguntas) as RedirectToActionResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal("Editar", result.ActionName);
            Assert.Equal("Perguntas", result.ControllerName);
            _dbContext.Dispose();
        }

        [Fact]
        public void ExcluirUmaPergunta()
        {
            //Arrange
            PerguntasModel pergunta = new PerguntasModel()
            {
                Id = 44,
                Questao = "Teste Editado",
                Resposta = "Resposta teste editado",
                DisciplinaId = 44
            };
            //Act
            _perguntasController.Cadastrar(pergunta);
            var result = _perguntasController.Excluir(pergunta) as RedirectToActionResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal("ListarPorDisciplinaId", result.ActionName);
            Assert.Equal("Perguntas", result.ControllerName);
            _dbContext.Dispose();
        }
        [Fact]
        public void ListarPerguntasPeloIdDaDisciplina()
        {
            //Arrange
            PerguntasModel pergunta = new PerguntasModel()
            {
                Id = 444,
                Questao = "Teste Editado",
                Resposta = "Resposta teste editado",
                Disciplina = new DisciplinaModel() {
                Id=444,
                Nome = "teste",
                UsuarioId = "1"
                },
                DisciplinaId = 444
            };
            //Act
            _perguntasController.Cadastrar(pergunta);
            var result = _perguntasController.ListarPorDisciplinaId(444);
            //Assert
            Assert.IsType<ViewResult>(result);
            _dbContext.Dispose();
        }

    }
}
