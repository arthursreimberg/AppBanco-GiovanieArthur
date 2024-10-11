using appQuintoGiovaniArthur.Models;
using appQuintoGiovaniArthur.Repository.Contract;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Cms;
using System.Data;

namespace appQuintoGiovaniArthur.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _conexaoMySQL;

        public UsuarioRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Atualizar(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Update usuario set nomeusu=@nomeusu, cargo=@cargo, " + " DataNasc=@datanasc where idusu=@idusu", conexao);

                cmd.Parameters.Add("@nomeusu", MySqlDbType.VarChar).Value = usuario.NomeUsu;
                cmd.Parameters.Add("@Cargo", MySqlDbType.VarChar).Value = usuario.Cargo;
                cmd.Parameters.Add("@datanasc", MySqlDbType.VarChar).Value = usuario.DataNasc.ToString("yyyy/MM/dd") ;
                cmd.Parameters.Add("@idusu", MySqlDbType.VarChar).Value = usuario.IdUsu;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL)) 
            { 
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into usuario (nomeUsu, cargo, dataNasc)" +
                                                    " values (@nomeUsu, @Cargo, @DataNasc)", conexao);
                cmd.Parameters.Add("@NomeUsu", MySqlDbType.VarChar).Value = usuario.NomeUsu;
                cmd.Parameters.Add("@Cargo", MySqlDbType.VarChar).Value = usuario.Cargo;
                cmd.Parameters.Add("@DataNasc", MySqlDbType.VarChar).Value = usuario.DataNasc.ToString("yyyy/MM/dd");

                cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from usuario where idusu=@idusu", conexao);
                cmd.Parameters.AddWithValue("@idusu", id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Usuario> ObterTodosUsuarios()
        {
            List<Usuario> UsuarioList = new List<Usuario>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from usuario",conexao );
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Clone();
                
                foreach(DataRow dr in dt.Rows)
                {
                    UsuarioList.Add(
                        new Usuario
                        {
                            IdUsu = Convert.ToInt32(dr["IdUsu"]),
                            NomeUsu = (string)dr["NomeUsu"],
                            Cargo = (string)dr["Cargo"],
                            DataNasc = Convert.ToDateTime(dr["DataNasc"])
                        });

                }
                return UsuarioList;

            }


        }

        public Usuario ObterUsuario(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from usuario " + " where IdUsu=@IdUsu ", conexao);

                cmd.Parameters.AddWithValue("@IdUsu", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Usuario usuario = new Usuario();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while(dr.Read())
                {
                    usuario.IdUsu = Convert.ToInt32(dr["IdUsu"]);
                    usuario.NomeUsu = (string)(dr["NomeUsu"]);
                    usuario.Cargo = (string)(dr["Cargo"]);
                    usuario.DataNasc = Convert.ToDateTime(dr["DataNasc"]);
                }
            return usuario;
            }

        }
    }
}
