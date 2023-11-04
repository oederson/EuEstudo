using EuEstudo.Models;
using EuEstudo.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EuEstudo.Controllers
{
    [Authorize]
    public class PerguntasController : Controller
    {
        private PerguntasService _perguntasService;
        
        public PerguntasController(PerguntasService perguntasService)
        {
            
            _perguntasService = perguntasService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Cadastrar(int? id)
        {
            return View(_perguntasService.CadastrarIndex(id));
        }

        [HttpPost]        
        public IActionResult Cadastrar(PerguntasModel pergunta)
        {
            var (actionName, controllerName) = _perguntasService.CadastrarNoDb(pergunta);
            return RedirectToAction(actionName, controllerName);
        }
        [HttpGet]
        public IActionResult Editar(int? id) 
        {
            return View(_perguntasService.EditarIndex(id));
        }
        [HttpPost]
        public IActionResult Editar(PerguntasModel pergunta)
        {           
            return _perguntasService.EditarNoBd(pergunta)? RedirectToAction("ListarPorDisciplinaId", "Perguntas" , new {id = pergunta.DisciplinaId}):
                                                           RedirectToAction ($"Editar", "Perguntas", new { id = pergunta.Id }); ;
        }
        [HttpGet]
        public IActionResult Excluir(int id) 
        {
            return View(_perguntasService.ExcluirIndex(id));
        }
        [HttpPost]
        public IActionResult Excluir(PerguntasModel pergunta)
        {
            return _perguntasService.ExcluirNoDb(pergunta)? RedirectToAction("ListarPorDisciplinaId", "Perguntas", new {id = pergunta.DisciplinaId}) :View();   
        }
        public IActionResult ListarPorDisciplinaId(int id)
        {
            var perguntas = _perguntasService.ListarPorDisciplinaId(id);
            return perguntas == null ? RedirectToAction("Index", "Disciplina", new {id = id}) : View(perguntas);         
        }
    }
}
