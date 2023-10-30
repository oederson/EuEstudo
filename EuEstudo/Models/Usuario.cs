using Microsoft.AspNetCore.Identity;

namespace EuEstudo.Models;
public class Usuario : IdentityUser
{
    public DateTime DataCadastro { get; set; }
    public virtual ICollection<DisciplinaModel> Disciplina { get; set; }
    public Usuario() : base()
      {

      }
}

