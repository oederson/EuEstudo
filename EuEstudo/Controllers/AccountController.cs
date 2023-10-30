
using AutoMapper;
using EuEstudo.Data.DTO;
using EuEstudo.Models;
using EuEstudo.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EuEstudo.Controllers
{
    public class AccountController : Controller
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        
        public AccountController( UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,IMapper mapper)
        {
            
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {            
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUsuarioDTO dto)
        {
                var resultado = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);
                if (!resultado.Succeeded)
                {
                    return View();
                }
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public IActionResult CadastraUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CadastraUsuario(CriarUsuarioDTO dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);
            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);
                    if (!resultado.Succeeded)
                    {
                        throw new ApplicationException("Falha ao cadastrar usuário");
                    }
           LoginUsuarioDTO loginDTO = _mapper.Map<LoginUsuarioDTO>(dto);
            var res = await _signInManager.PasswordSignInAsync(loginDTO.Username, loginDTO.Password, false, false);
            if (!res.Succeeded)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}