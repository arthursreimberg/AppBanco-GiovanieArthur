using appQuintoGiovaniArthur.Models;
using appQuintoGiovaniArthur.Repository.Contract;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Cms;
using System.Data;

namespace appQuintoGiovaniArthur.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _conexaoMySQL;

        public ClienteRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL2");
        }

        public void Atualizar(Cliente cliente)
        {
            using (var conexao2 = new MySqlConnection(_conexaoMySQL))
            {
                conexao2.Open();
                MySqlCommand cmd = new MySqlCommand("Update usuario set nomeCli=@nomeCli, RG=@RG, CPF=@CPF " + "DataNasCli=@DataNasCli where IdCli=@IdCli", conexao2);

                cmd.Parameters.Add("@nomeCli", MySqlDbType.VarChar).Value = cliente.NomeCli;
                cmd.Parameters.Add("@RG", MySqlDbType.VarChar).Value = cliente.RG;
                cmd.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = cliente.CPF;
                cmd.Parameters.Add("@DataNasCli", MySqlDbType.VarChar).Value = cliente.DataNasCli.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@IdCli", MySqlDbType.VarChar).Value = cliente.IdCli;

                cmd.ExecuteNonQuery();
                conexao2.Close();
            }
        }

        public void Cadastrar(Cliente cliente)
        {
            using (var conexao2 = new MySqlConnection(_conexaoMySQL))
            {
                conexao2.Open();

                MySqlCommand cmd = new MySqlCommand("insert into cliente (nomeCli, DataNasCli, RG, CPF)" +
                                                    " values (@nomeCli, @DataNasCli, @RG, @CPF)", conexao2);
                cmd.Parameters.Add("@nomeCli", MySqlDbType.VarChar).Value = cliente.NomeCli;
                cmd.Parameters.Add("@RG", MySqlDbType.VarChar).Value = cliente.RG;
                cmd.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = cliente.CPF;
                cmd.Parameters.Add("@DataNasCli", MySqlDbType.VarChar).Value = cliente.DataNasCli.ToString("yyyy/MM/dd");

                cmd.ExecuteNonQuery();
                conexao2.Close();

            }
        }

        public void Excluir(int id)
        {
            using (var conexao2 = new MySqlConnection(_conexaoMySQL))
            {
                conexao2.Open();
                MySqlCommand cmd = new MySqlCommand("delete from cliente where IdCli=@IdCli", conexao2);
                cmd.Parameters.AddWithValue("@IdCli", id);
                int i = cmd.ExecuteNonQuery();
                conexao2.Close();
            }
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            List<Cliente> ClienteList = new List<Cliente>();
            using (var conexao2 = new MySqlConnection(_conexaoMySQL))
            {
                conexao2.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from cliente", conexao2);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao2.Clone();

                foreach (DataRow dr in dt.Rows)
                {
                    ClienteList.Add(
                        new Cliente
                        {
                            IdCli = Convert.ToInt32(dr["IdCli"]),
                            NomeCli = (string)dr["nomeCli"],
                            RG = (string)dr["RG"],
                            CPF = (string)dr["CPF"],
                            DataNasCli = Convert.ToDateTime(dr["DataNasCli"])
                        });
                }
                return ClienteList;

            }

        }

        public Cliente ObterCliente(int Id)
        {
            using (var conexao2 = new MySqlConnection(_conexaoMySQL))
            {
                conexao2.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from cliente " + " where IdCli=@IdCli ", conexao2);

                cmd.Parameters.AddWithValue("@IdCli", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.IdCli = Convert.ToInt32(dr["IdCli"]);
                    cliente.NomeCli = (string)(dr["NomeCli"]);
                    cliente.RG = (string)(dr["RG"]);
                    cliente.CPF = (string)(dr["CPF"]);
                    cliente.DataNasCli = Convert.ToDateTime(dr["DataNasCli"]);
                }
                return cliente;
            }

        }
    }
}
