using EuEstudo.Dados;
using EuEstudo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuEstudo.Service
{
    public class PerguntasService
    {
        private readonly AppDbContext _database;
        public PerguntasService(AppDbContext database)
        {
            _database = database;
        }
        public PerguntasModel CadastrarIndex(int? id)
        {
            if (id == null || id == 0){return null;}
            PerguntasModel perguntaModel = new PerguntasModel();
            perguntaModel.DisciplinaId = (int)id;
            return perguntaModel;
        }
        public (string, string) CadastrarNoDb(PerguntasModel pergunta)
        {
            if(pergunta.Questao == null || pergunta.Resposta == null) 
            { return ($"Cadastrar/{pergunta.DisciplinaId}", "Perguntas"); };
            _database.Perguntas.Add(pergunta);
            _database.SaveChanges();
            return  ("Index", "Disciplina");
        }
        public PerguntasModel EditarIndex(int? id)
        {
            PerguntasModel pergunta = _database.Perguntas.FirstOrDefault(x => x.Id == id);
            if (id == null || id == 0 || pergunta == null) { return null; }
            return pergunta;
        }
        public bool EditarNoBd(PerguntasModel pergunta)
        {
            if (pergunta.Questao != null && pergunta.Resposta != null)
            {
                _database.Perguntas.Update(pergunta);
                _database.SaveChanges();
                return true;
            }
            return false;
        }
        public PerguntasModel ExcluirIndex(int id)
        {
            PerguntasModel pergunta = _database.Perguntas.FirstOrDefault(x => x.Id == id);
            if (id == null || id == 0 || pergunta == null)
            {
                return null;
            }
            return pergunta;
        }
        public bool ExcluirNoDb(PerguntasModel pergunta)
        {
            if (pergunta == null)
            {
                return false;
            }
            _database.Perguntas.Remove(pergunta);
            _database.SaveChanges();
            return true;
        }
        public List<PerguntasModel> ListarPorDisciplinaId(int id)
        {
            var perguntas = _database.Perguntas
                            .Where(p => p.DisciplinaId == id)
                            .Include(p => p.Disciplina)
                            .ToList();
            if (perguntas.Count == 0)
            {
                //TO DO :Enviar mensagem que a Disciplina não tem questões
                return null;
            }
            return perguntas;
        }
    }
}
