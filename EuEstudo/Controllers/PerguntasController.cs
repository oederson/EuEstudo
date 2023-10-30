using EuEstudo.Dados;

using EuEstudo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuEstudo.Controllers
{
    [Authorize]
    public class PerguntasController : Controller
    {
        private readonly AppDbContext _database;
        public PerguntasController(AppDbContext database)
        {
            _database = database;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Cadastrar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            PerguntasModel perguntaModel = new PerguntasModel();
            perguntaModel.DisciplinaId = (int)id;
 

            return View(perguntaModel);
        }

        [HttpPost]        
        public IActionResult Cadastrar(PerguntasModel pergunta)
        {
            //TO DO : Pricisa de atenção
            _database.Perguntas.Add(pergunta); 
            _database.SaveChanges();
            return RedirectToAction("Index" , "Disciplina");

        }
        [HttpGet]
        public IActionResult Editar(int? id) 
        {
            if(id == null || id == 0) 
            {
                return NotFound();
            }
            PerguntasModel pergunta = _database.Perguntas.FirstOrDefault(x => x.Id == id);
            if(pergunta == null)
            {
                return NotFound();
            }
            return View(pergunta);
        }
        [HttpPost]
        public IActionResult Editar(PerguntasModel pergunta)
        {
            if(ModelState.IsValid) 
            {
                _database.Perguntas.Update(pergunta);
                _database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pergunta);
        }
        [HttpGet]
        public IActionResult Excluir(int id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            PerguntasModel pergunta = _database.Perguntas.FirstOrDefault(x => x.Id == id);
            if (pergunta == null)
            {
                return NotFound();
            }
            return View(pergunta);

        }
        [HttpPost]
        public IActionResult Excluir(PerguntasModel pergunta)
        {
             if(pergunta == null) 
            { 
            return NotFound();
            }
             _database.Perguntas.Remove(pergunta);
            _database.SaveChanges();
            return RedirectToAction("Index");   
        }
        public IActionResult ListarPorDisciplinaId(int id)
        {            
            var perguntas = _database.Perguntas
                            .Where(p => p.DisciplinaId == id)
                            .Include(p => p.Disciplina)
                            .ToList();
            if(perguntas.Count == 0) 
            {
                //TO DO :Enviar mensagem que a Disciplina não tem questões
                return RedirectToAction("Index", "Disciplina"); ;
            }
            return View(perguntas);
        }
    }
}
