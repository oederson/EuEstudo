using EuEstudo.Dados;
using EuEstudo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EuEstudo.Service
{
    public class DisciplinaService
    {
        private readonly AppDbContext _database;
        private UserManager<Usuario> _userManager;

        public DisciplinaService()
        {

        }

        public DisciplinaService(AppDbContext database, UserManager<Usuario> userManager)
        {
            _database = database;
            _userManager = userManager;
        }
        public async Task<List<DisciplinaModel>> ObterDisciplinasDoUsuarioAsync(ClaimsPrincipal user)
        {
            var usuario = await _userManager.GetUserAsync(user);
            var res = _database.Disciplinas.Where(d => d.UsuarioId == usuario.Id);
            return res.ToList();
        }

        public async Task<List<DisciplinaModel>> DisciplinasDoUsuario(ClaimsPrincipal user)
        {
            var usuario = await _userManager.GetUserAsync(user);            
            return _database.Disciplinas.Where(d => d.UsuarioId == usuario.Id).ToList();
        }
        public async Task<bool> CadastrarDisciplina(DisciplinaModel disciplina, ClaimsPrincipal user)
        {
            if (disciplina.Nome != null)
            {
                var usuario = await _userManager.GetUserAsync(user);
                disciplina.Usuario = usuario;
                await _database.Disciplinas.AddAsync(disciplina);
                await _database.SaveChangesAsync();

                return true;
            }
            return false;
        }
        public async Task<DisciplinaModel> ExcluirDisciplina(int id)
        {
            DisciplinaModel disciplina = await _database.Disciplinas.FirstOrDefaultAsync(x => x.Id == id);
            if (id == null || id == 0 || disciplina == null)
            {
                return null;
            }
            return disciplina;
        }
        public async Task<bool> ExcluirDisciplinaDoBD(DisciplinaModel disciplina)
        {
            if (disciplina == null)
            {
                return false;
            }
            _database.Disciplinas.Remove(disciplina);
            _database.SaveChangesAsync();
            return true;
        }
    }
}
