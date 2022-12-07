using ProjetoLogin.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace ProjetoLogin.Repository
{
    public class PessoaRepository
    {
        public IList<Pessoa> Listar()
        {
            IList<Pessoa> lista = new List<Pessoa>();

            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("ProjetoLoginConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "SELECT IDPESSOA, NOME, CIDADE, EMAIL FROM PESSOA";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    // Recupera os dados
                    Pessoa cadastroPessoa = new Pessoa();
                    cadastroPessoa.IdPessoa = Convert.ToInt32(dataReader["IDPESSOA"]);
                    cadastroPessoa.Nome = dataReader["NOME"].ToString();
                    cadastroPessoa.Cidade = dataReader["CIDADE"].ToString();
                    cadastroPessoa.Email = dataReader["EMAIL"].ToString();


                    // Adiciona o modelo da lista
                    lista.Add(cadastroPessoa);
                }

                connection.Close();

            } // Finaliza o objeto connection

            // Retorna a lista
            return lista;
        }

        public Pessoa Consultar(int id)
        {

            Pessoa cadastroPessoa = new Pessoa();

            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("ProjetoLoginConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "SELECT IDPESSOA, NOME, CIDADE, EMAIL FROM PESSOA WHERE IDPESSOA = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@ID", SqlDbType.Int);
                command.Parameters["@ID"].Value = id;
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    // Recupera os dados
                    cadastroPessoa.IdPessoa = Convert.ToInt32(dataReader["IDPESSOA"]);
                    cadastroPessoa.Nome = dataReader["NOME"].ToString();
                    cadastroPessoa.Cidade = dataReader["CIDADE"].ToString();
                    cadastroPessoa.Email = dataReader["EMAIL"].ToString();
                }

                connection.Close();

            } // Finaliza o objeto connection

            // Retorna a lista
            return cadastroPessoa;
        }

        public void Cadastrar(PessoaRequest cadastroPessoa)
        {
            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("ProjetoLoginConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "INSERT INTO PESSOA ( NOME, CIDADE, EMAIL ) VALUES ( @NOME, @CIDADE, @EMAIL )";

                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("@NOME", SqlDbType.Text);
                command.Parameters["@NOME"].Value = cadastroPessoa.Nome;
                command.Parameters.Add("@CIDADE", SqlDbType.Text);
                command.Parameters["@CIDADE"].Value = Convert.ToString(cadastroPessoa.Cidade);
                command.Parameters.Add("@EMAIL", SqlDbType.Text);
                command.Parameters["@EMAIL"].Value = Convert.ToString(cadastroPessoa.Email);

                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public void Editar(Pessoa cadastroPessoa)
        {
            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("ProjetoLoginConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "UPDATE PESSOA SET NOME = @NOME , CIDADE = @CIDADE, EMAIL = @EMAIL WHERE IDPESSOA = @ID";

                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("@ID", SqlDbType.Int);
                command.Parameters.Add("@NOME", SqlDbType.Text);
                command.Parameters.Add("@CIDADE", SqlDbType.Text);
                command.Parameters.Add("@EMAIL", SqlDbType.Text);
                command.Parameters["@ID"].Value = cadastroPessoa.IdPessoa;
                command.Parameters["@NOME"].Value = cadastroPessoa.Nome;
                command.Parameters["@CIDADE"].Value = Convert.ToString(cadastroPessoa.Cidade);
                command.Parameters["@EMAIL"].Value = cadastroPessoa.Email;

                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public void Excluir(int id)
        {
            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("ProjetoLoginConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "DELETE PESSOA WHERE IDPESSOA = @ID";

                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("@ID", SqlDbType.Int);
                command.Parameters["@ID"].Value = id;

                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
