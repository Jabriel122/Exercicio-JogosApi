using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interface;
using System.Data.SqlClient;

namespace senai.inlock.webApi_.Repositories
{
    public class UsuarioRepository : IUsauarioRepository
    {
        private string StringConexao= "Data Source =  DESKTOP-VLQ1I1C; Initial Catalog = Filme_Gabriel; User Id = sa; pwd = Senai@134";

        public UsuarioDomain Logar(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySelect = "SELECT IdUsuario, Email, IdTipoUsuario FROM Usuario WHERE Email = @Email AND Senha = @Senha";

                con.Open();
                
                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain()
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            Email = rdr["email"].ToString(),
                            //IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                            
                        };
                        return usuario;
                    }
                    return null;
                }
            }
        }


    }
}
