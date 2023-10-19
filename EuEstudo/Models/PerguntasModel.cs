using System.ComponentModel.DataAnnotations;

namespace EuEstudo.Models
{
    public class PerguntasModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Necessario informar a questão")]
        public string Questao { get; set; }
        [Required(ErrorMessage = "Necessario informar a resposta")]
        public string Resposta { get; set; }
    }
}
