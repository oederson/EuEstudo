using EuEstudo.Auxiliar;
using EuEstudo.Data.DTO;
using EuEstudo.Service;
using Microsoft.AspNetCore.Mvc;

namespace EuEstudo.Controllers
{
    public class LoginController : Controller
    {
        private UsuarioService _usuarioService;
        private readonly ISessao _sessao;
        public LoginController(UsuarioService cadastroService, ISessao session)
        {
            _usuarioService = cadastroService;
            _sessao = session;
        }
        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoDoUsuario() != null) { return RedirectToAction("Home", "Index"); }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUsuarioDTO dto)
        {
            // Precisa de melhorias
            var token = await _usuarioService.Login(dto);

            if (token != null)
            {
                _sessao.CriarSessaoDoUsuario(token);
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }
        [HttpGet]
        public IActionResult CadastraUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CadastraUsuario(CriarUsuarioDTO dto)
        {
            var cadastra = await _usuarioService.Cadastra(dto);            
            var token = await _usuarioService.Login(cadastra);
            if (token != null)
            {
                _sessao.CriarSessaoDoUsuario(token);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public IActionResult LogOut()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

    }
}
