using senai.inlock.webApi_.Domains;

namespace senai.inlock.webApi_.Interface
{
    public interface IUsauarioRepository
    {
        UsuarioDomain Logar(string email, string senha);
    }
}
