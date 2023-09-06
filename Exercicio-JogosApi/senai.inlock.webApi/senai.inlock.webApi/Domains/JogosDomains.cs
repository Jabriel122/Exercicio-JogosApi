namespace senai.inlock.webApi_.Domains
{
    public class JogosDomains
    {
        public int IdJogo { get; set; }

        public EstudioDomains Estudio { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public DateTime DataLancamento { get; set; }

        public int Valor { get; set; }

    }
}
