using senai.inlock.webApi_.Domains;

namespace senai.inlock.webApi_.Interface
{
    public interface IJogosRepository
	{
        void Cadastre(JogosDomains jogosDomains);

        List<JogosDomains> ListarTodos();
        
    }
}
