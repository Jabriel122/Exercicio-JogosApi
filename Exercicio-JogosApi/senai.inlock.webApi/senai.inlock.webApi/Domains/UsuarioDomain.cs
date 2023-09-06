using System.ComponentModel.DataAnnotations;

namespace senai.inlock.webApi_.Domains
{
    public class UsuarioDomain
    {
        public int IdUsario { get; set; }

        [Required(ErrorMessage = "Um email é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Uma senha é obrigatório")]
        public string Senha { get; set; }
    }
}
