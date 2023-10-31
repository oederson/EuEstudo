using EuEstudo.Data.DTO;
using EuEstudo.Service;
using Microsoft.AspNetCore.Mvc;

namespace EuEstudo.Controllers;

public class AccountController : Controller
{
    private AccountService _accountService;
    
    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }
    public IActionResult Index()
    {            
        return View();
    }
    public IActionResult Login()
    {
        return View();
    }
    public IActionResult CadastraUsuario()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginUsuarioDTO dto)
    {
        if(await _accountService.Login(dto)) return RedirectToAction("Index", "Home");
        //TODO : Retrun view precisa de mensagem de erro para o front.
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CadastraUsuario(CriarUsuarioDTO dto)
    {
        switch(await _accountService.CadastraUsuario(dto)) 
        {
            //TODO: Mensagem para o front que não foi possivel cadastrar o usuário
            case null: return View(); break;
            //TODO: Mensagem para o front que foi possivel cadastrar o usuário, mais não foi possivel logar 
            case false: return View(); break;
            //Deu tudo certo
            case true: return RedirectToAction("Index", "Home"); break;
            default: 
        }
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        if(await _accountService.LogOut()) return RedirectToAction("Index", "Home");
        //TODO : Retrun view precisa de mensagem de erro para o front.
        return RedirectToAction("Index", "Home");
    }
}