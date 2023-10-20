using EuEstudo.Dados;
using EuEstudo.Models;
using Microsoft.AspNetCore.Mvc;

namespace EuEstudo.Controllers
{
    public class PerguntasController : Controller
    {
        private readonly AppDbContext _database;
        public PerguntasController(AppDbContext database)
        {
            _database = database;
        }
        public IActionResult Index()
        {
            IEnumerable<PerguntasModel> questoes = _database.Perguntas;
            return View(questoes);
        }
        [HttpGet]
        public IActionResult Cadastrar() 
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult Cadastrar(PerguntasModel pergunta)
        {

            Console.WriteLine($"Questao{pergunta.Questao}");

            if (ModelState.IsValid) 
            {
            Console.WriteLine("Entrei no if");
            _database.Perguntas.Add(pergunta);
            _database.SaveChanges();

            return RedirectToAction("Index");
                }
            return View(pergunta);

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
    }
}
