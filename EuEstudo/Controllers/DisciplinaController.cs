using EuEstudo.Models;
using EuEstudo.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EuEstudo.Controllers;

[Authorize]
public class DisciplinaController : Controller
{
    private DisciplinaService _disciplinaService;
    public DisciplinaController(DisciplinaService disciplinaService)
    {
        _disciplinaService = disciplinaService;
    }
    public async Task<ActionResult> Index()
    {        
        return View(await _disciplinaService.DisciplinasDoUsuario(User));
    }
    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(DisciplinaModel disciplina)
    {
        return await _disciplinaService.CadastrarDisciplina(disciplina, User) ? RedirectToAction("Index") : View(disciplina);
    }

    [HttpGet]
    public async Task<IActionResult> Excluir(int id)
    {
        var res = await _disciplinaService.ExcluirDisciplina(id);
        return  res == null ? View() : View(res);
    }

    [HttpPost]
    public async Task<IActionResult> Excluir(DisciplinaModel disciplina)
    {
        return await _disciplinaService.ExcluirDisciplinaDoBD(disciplina) ? RedirectToAction("Index") : View(disciplina);
    }

}
