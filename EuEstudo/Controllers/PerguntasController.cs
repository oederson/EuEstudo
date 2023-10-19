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
        
        public IActionResult Cadastrar( PerguntasModel pergunta)
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
    }
}
