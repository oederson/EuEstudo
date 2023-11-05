using System.Security.Claims;
using EuEstudo.Controllers;
using EuEstudo.Dados;
using EuEstudo.Models;
using EuEstudo.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TesteEuEstudo.FakesParaMock;

namespace EuEstudo.Tests
{
    public class DisciplinaControllerTests 
    {
        private readonly AppDbContext _dbContext;
        private readonly DisciplinaService _disciplinaService;
        private readonly DisciplinaController _controller;
        private readonly Mock<FalsoUserManager> _userManager;

        public DisciplinaControllerTests()
        {
            var initializer = new AppDbContextTest();
            _dbContext = initializer.GetContext();

            _userManager = new Mock<FalsoUserManager>();
            _disciplinaService = new DisciplinaService(_dbContext, _userManager.Object);
            _controller = new DisciplinaController(_disciplinaService);
        }

        [Fact]
        public async Task IndexRetornaTodasAsDisciplinasDoUsuario()
        {
            //Arrange
            _userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                    .ReturnsAsync(new Usuario { Id = "123", UserName = "TesteUsername" });
            // Act
            var result = await _controller.Index();
            // Assert
            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            var model = viewResult.Model;
            Assert.NotNull(model);
            var listaDeDisciplinas = (List<DisciplinaModel>)model;
            Assert.Equal("Disciplina 1", listaDeDisciplinas[0].Nome);
            Assert.Equal("Disciplina 2", listaDeDisciplinas[1].Nome);
            _dbContext.Dispose();
        }

        [Fact]
        public async Task CadastrarUmaDisciplina()
        {
            //Arrange
            _userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                    .ReturnsAsync(new Usuario { Id = "123456", UserName = "TesteUsername" });
            DisciplinaModel dis = new DisciplinaModel()
            {
                Nome = "Teste"
            };

            //Act
            var result =  await _controller.Cadastrar(dis);
            //Assert
            Assert.IsType<RedirectToActionResult>(result);
            _dbContext.Dispose();
        }
        [Fact]
        public async Task ExcluirUmaDisciplina()
        {
            //Arrange
            _userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                    .ReturnsAsync(new Usuario { Id = "123", UserName = "TesteUsername" });
            DisciplinaModel disciplina = new DisciplinaModel()
            {
                Id = 99,
                Nome = "Disciplina 99",
                UsuarioId = "123"
            };
            //Act
            await _controller.Cadastrar(disciplina);
            var result = await _controller.Excluir(disciplina);
            //Assert
            Assert.IsType<RedirectToActionResult>(result);
            _dbContext.Dispose(); 
        }

    }
}
