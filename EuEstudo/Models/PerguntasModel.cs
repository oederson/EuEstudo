using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuEstudo.Models
{
    public class PerguntasModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage ="Necessario informar a questão")]
        public string Questao { get; set; }
        [Required(ErrorMessage = "Necessario informar a resposta")]
        public string Resposta { get; set; }
        [ForeignKey("Disciplina")]
        public int DisciplinaId { get; set; }
        public virtual DisciplinaModel Disciplina { get; set; }
    }
}
