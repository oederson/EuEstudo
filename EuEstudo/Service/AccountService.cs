using AutoMapper;
using EuEstudo.Data.DTO;
using EuEstudo.Models;
using Microsoft.AspNetCore.Identity;

namespace EuEstudo.Service
{
    public class AccountService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;

        public AccountService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IMapper mapper)
        {

            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<bool> Login(LoginUsuarioDTO usuario)
        {
            Microsoft.AspNetCore.Identity.SignInResult resultado = await _signInManager.PasswordSignInAsync(usuario.Username, 
                                                                                                            usuario.Password, 
                                                                                                            false, 
                                                                                                            false);
            if (resultado.Succeeded) return true;
            return false;
        }
        public async Task<bool> LogOut()
        {
            await _signInManager.SignOutAsync();
            return true;           
        }
        public async Task<bool?> CadastraUsuario(CriarUsuarioDTO dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);
            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);
            if (!resultado.Succeeded)
            {
                return null;
            }
            LoginUsuarioDTO loginDTO = _mapper.Map<LoginUsuarioDTO>(dto);
            var res = await _signInManager.PasswordSignInAsync(loginDTO.Username, loginDTO.Password, false, false);
            if (!res.Succeeded)
            {
                return false;
            }
            return true;
        }
    }
}
