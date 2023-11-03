using AutoMapper;
using EuEstudo.Controllers;
using EuEstudo.Data.DTO;
using EuEstudo.Models;
using EuEstudo.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TesteEuEstudo.FakesParaMock;

namespace TesteEuEstudo
{
    public class AccountServiceTests
    {
        private IMapper mapper;
        private AccountController controller;
        private AccountService _accountService;
        private Mock<FalsoUserManager> _userManager;
        private Mock<FalsoSignInManager> _signInManager;

        public AccountServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CriarUsuarioDTO, Usuario>();
                cfg.CreateMap<CriarUsuarioDTO, LoginUsuarioDTO>();
            });
            mapper = configuration.CreateMapper();

            _userManager = new Mock<FalsoUserManager>();
            _signInManager = new Mock<FalsoSignInManager>(_userManager.Object);
            _accountService = new AccountService(_userManager.Object, _signInManager.Object, mapper);
            controller = new AccountController(_accountService);
        }
        [Fact]
        public async Task LoginValidaNomeESenhaERedirecionaParaHome()
        {  
            //Arrange
            _signInManager.Setup(x => x.PasswordSignInAsync("TesteUsername", "ValidoPassword", false, false))
                         .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            //Act
            var result = await controller.Login(new LoginUsuarioDTO
            {
                Username = "TesteUsername",
                Password = "ValidoPassword"
            });

            //Assert
            Assert.IsType<RedirectToActionResult>(result); 
        }
        [Fact]
        public async Task LoginTentaValidarNomeESenhaEContinuaNapaginaDeLoginSeFalhar()
        {
            //Arrange
            _signInManager.Setup(x => x.PasswordSignInAsync("TesteUsername", "ValidoPassword", false, false))
                         .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            //Act
            var result = await controller.Login(new LoginUsuarioDTO
            {
                Username = "TesteUsername",
                Password = "ValidoPassword"
            });

            //Assert
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task CadastraUsuarioELoga()
        {
            //Arrange
           _userManager.Setup(x => x.CreateAsync(It.IsAny<Usuario>(), It.IsAny<string>()))
                                        .ReturnsAsync(IdentityResult.Success);

            _signInManager.Setup(x => x.PasswordSignInAsync("Teste", "ValidoPassword", false, false))
                         .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            //ACT
            var result = await controller.CadastraUsuario(new CriarUsuarioDTO
            {
               Username = "Teste",
               Password = "ValidoPassword"
            });
            //Assert
            Assert.IsType<RedirectToActionResult>(result);
        }
        [Fact]
        public async Task NaoCadastraUsuarioENaoLoga()
        {
            //Arrange
            _userManager.Setup(x => x.CreateAsync(It.IsAny<Usuario>(), It.IsAny<string>()))
                                         .ReturnsAsync(IdentityResult.Failed());

            _signInManager.Setup(x => x.PasswordSignInAsync("Teste", "ValidoPassword", false, false))
                         .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            //ACT
            var result = await controller.CadastraUsuario(new CriarUsuarioDTO
            {
                Username = "Teste",
                Password = "ValidoPassword"
            });
            //Assert
            Assert.IsType<ViewResult>(result); 
        }
        [Fact]
        public async Task CadastraUsuarioMasFalhaAoLogar()
        {
            //Arrange
            _userManager.Setup(x => x.CreateAsync(It.IsAny<Usuario>(), It.IsAny<string>()))
                                         .ReturnsAsync(IdentityResult.Success);

            _signInManager.Setup(x => x.PasswordSignInAsync("Teste", "ValidoPassword", false, false))
                         .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);
            //ACT
            var result = await controller.CadastraUsuario(new CriarUsuarioDTO
            {
                Username = "Teste",
                Password = "ValidoPassword"
            });
            //Assert
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task FazOLogout()
        {
            //Arrange 
            _signInManager.Setup(x => x.SignOutAsync());
            //Act
            var result = await controller.Logout();
            //Assert
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}

