using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interface;
using System.Data.SqlClient;

namespace senai.inlock.webApi_.Repositories
{
    public class JogosRepository : IJogosRepository
    {
        private string StringConexao = "Data Source = DESKTOP-VLQ1I1C; Initial Catalog = Filmes_Gabriel; User Id = sa; pwd = Senai@134";
        public void Cadastre(JogosDomains jogosDomains)
        {

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryInsrt = "INSERT INTO Jogo(IdEstudio,Nome,Descricao,DataLancamento,Valor) VALAUES (@IdEstudio, @Nome,@Descricao,@DataLancamento,@Valor)";

                using (SqlCommand cmd = new SqlCommand(queryInsrt, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@IdEstudio", jogosDomains.IdJogo);
                    cmd.Parameters.AddWithValue("@Nome", jogosDomains.Nome);
                    cmd.Parameters.AddWithValue("@Descricao", jogosDomains.Descricao);
                    cmd.Parameters.AddWithValue("@DataLancamento", jogosDomains.DataLancamento);
                    cmd.Parameters.AddWithValue("@Valor", jogosDomains.Valor);

                    cmd.ExecuteNonQuery();


                }
            }

        }

        List<JogosDomains> IJogosRepository.ListarTodos()
        {
            List<JogosDomains> listarJogos = new List<JogosDomains>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
               string querySelectAll = "SELECT IdJogo, Nome FROM Jogo";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        JogosDomains jogo = new JogosDomains()
                        {
                            IdJogo = Convert.ToInt32(rdr["IdJogo"]),
                            Nome = Convert.ToString(rdr["Nome"])
                        };

                        listarJogos.Add(jogo);
                    }
                }

            }
            return listarJogos;
        }
    }
}
