using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// NameSpace que contem  as classes que manipulam os dados
using System.Data;
using System.Data.SqlClient;

using AcessoBancoDados.Properties;


namespace AcessoBancoDados
{
    public class AcessoDadosSqlServer
    {
        // Criar Conexao;
        private SqlConnection CriarConexao()
        {
            return new SqlConnection(Settings.Default.StringConexao);
        }

        //Parâmetros que vão para o Banco
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        //Limpar Parâmetros;
        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }

        //Adicionando Parâmetros;
        public void AdicionarParametros(string nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }

        //Persitência: Inserir, Alterar, Excluir;
        public object ExecutarManipulacao(CommandType commandType, String nomeStoredProcedureOuTextoSql)
        {

            try
            {
                // Criar conexao;
                SqlConnection sqlConnection = CriarConexao();
                //Abrir conexao;
                sqlConnection.Open();
                // Criar comando que leva as informações para o Banco;
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as informações dentro dos comandos  (Dentro da caixa que vai trafegar na conexao);
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; // Em segundos

                // Adicionar os parâmetros no comando;
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }
                //Executar o comando, ou seja, mandar  o comando  ir ate o banco de dados;
                return sqlCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        //Consultar registros  do banco de dados 
        public DataTable ExecutarConsulta(CommandType commandType, String nomeStoredProcedureOuTextoSql)
        {
            try
            {
                // Criar conexao;
                SqlConnection sqlConnection = CriarConexao();
                //Abrir conexao;
                sqlConnection.Open();
                // Criar comando que leva as informações para o Banco;
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as informações dentro dos comandos  (Dentro da caixa que vai trafegar na conexao);
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; // Em segundos

                // Adicionar os parâmetros no comando;
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                // Criar um adapdador
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                // DataTable = Tabela de dados vazia onde vou colcoar os dados que vem do banco
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                return dataTable;


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
