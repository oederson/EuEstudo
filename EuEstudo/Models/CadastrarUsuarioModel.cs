using System.ComponentModel.DataAnnotations;

namespace EuEstudo.Models
{
    public class CadastrarUsuarioModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}
