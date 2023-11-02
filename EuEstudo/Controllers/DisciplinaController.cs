using EuEstudo.Dados;
using EuEstudo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EuEstudo.Controllers;

[Authorize]
public class DisciplinaController : Controller
{
    private readonly AppDbContext _database;
    private UserManager<Usuario> _userManager;
    public DisciplinaController(AppDbContext database, UserManager<Usuario> userManager)
    {
        _database = database;
        _userManager = userManager;
    }
    private async Task<List<DisciplinaModel>> ObterDisciplinasDoUsuarioAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        return _database.Disciplinas.Where(d => d.UsuarioId == user.Id).ToList();
    }

    public async Task<ActionResult> Index()
    {
        var disciplinas = await ObterDisciplinasDoUsuarioAsync();
        if (disciplinas != null)
        { return View(disciplinas); }
        return View();

    }
    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(DisciplinaModel disciplina)
    {
        var usuario = await _userManager.GetUserAsync(User);
        disciplina.Usuario = usuario;
        if (disciplina.Nome != null)
        {
            _database.Disciplinas.Add(disciplina);
            _database.SaveChanges();

            return RedirectToAction("Index");
        }
        return View(disciplina);

    }

    [HttpGet]
    public IActionResult Excluir(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        DisciplinaModel disciplina = _database.Disciplinas.FirstOrDefault(x => x.Id == id);
        if (disciplina == null)
        {
            return NotFound();
        }
        return View(disciplina);

    }
    [HttpPost]
    public IActionResult Excluir(DisciplinaModel disciplina)
    {
        if (disciplina == null)
        {
            return NotFound();
        }
        _database.Disciplinas.Remove(disciplina);
        _database.SaveChanges();
        return RedirectToAction("Index");
    }

}
