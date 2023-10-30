//using AutoMapper;
//using EuEstudo.Data.DTO;
//using EuEstudo.Models;
//using Microsoft.AspNetCore.Identity;

//namespace EuEstudo.Service;
//public class UsuarioService
//{
//    private IMapper _mapper;
//    private UserManager<Usuario> _userManager;
//    private SignInManager<Usuario> _signInManager;
//    private TokenService _tokenService;

//    public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
//    {
//        _mapper = mapper;
//        _userManager = userManager;
//        _signInManager = signInManager;
//        _tokenService = tokenService;
//    }

//    public async Task<LoginUsuarioDTO> Cadastra(CriarUsuarioDTO dto)
//    {
//        Usuario usuario = _mapper.Map<Usuario>(dto);
//        IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);
//        if (!resultado.Succeeded)
//        {
//            throw new ApplicationException("Falha ao cadastrar usuário");
//        }
//        LoginUsuarioDTO loginDTO = _mapper.Map<LoginUsuarioDTO>(dto); 
//        return loginDTO; 

//    }

//    public async Task<Usuario> Login(LoginUsuarioDTO dto)
//    {        
//        var resultado = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);
//        if (!resultado.Succeeded)
//        {
//            throw new ApplicationException("Usuário nao autenticado");
//        }
//            var usuario = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());
//            var token = _tokenService.GenerateToken(usuario);        
//        return usuario;
//    }
//}

