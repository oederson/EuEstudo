using EuEstudo.Controllers;
using EuEstudo.Data.DTO;
using EuEstudo.Models;
using EuEstudo.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TesteEuEstudoAPI.AccountControllerTests;

namespace TesteEuEstudoAPI.ControllerAPITest
{
    internal class AccountControllerTestes : IClassFixture<TesteServer<StartupBase>>
    {
        private AccountService _service { get; }

        private AccountController _controller { get; }

        public AccountControllerTestes(TesteServer<StartupBase> testeServer) 
        {
            var users = new List<Usuario>
            {
                new Usuario
                {
                    UserName = "Test",
                    Id = Guid.NewGuid().ToString(),
                    PasswordHash = "test@test.it"
                }
            }.AsQueryable();
            var fakeUserManager = new Mock<FalsoUserManager>();
            var signInManager = new Mock<FalsoSignInManager>();
        }

        [Fact]
        public async Task Login_ValidCredentials_RedirectsToIndex()
        {
            // Arrange
            var accountServiceMock = new Mock<AccountService>();
            accountServiceMock.Setup(service => service.Login(It.IsAny<LoginUsuarioDTO>()))
                .ReturnsAsync(true); // Simulate successful login

            var controller = new AccountController(accountServiceMock.Object);

            // Act
            var result = await controller.Login(new LoginUsuarioDTO());

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
        }


    }
}
        
    

