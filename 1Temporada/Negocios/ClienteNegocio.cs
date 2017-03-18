using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcessoBancoDados;
using ObjetoTrasferencia;
using System.Data;
using System.Data.SqlClient;


namespace Negocios
{
    public class ClienteNegocio
    {
        // Instanciar = cria um novo objeto baseado em um modelo;
        AcessoDadosSqlServer acessoDadosSqlServer = new AcessoDadosSqlServer();

        public string Inserir(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@Nome", cliente.Nome);
                acessoDadosSqlServer.AdicionarParametros("@DataNascimento", cliente.DataNascimento);
                acessoDadosSqlServer.AdicionarParametros("@Sexo", cliente.Sexo);
                acessoDadosSqlServer.AdicionarParametros("@LimitCompra", cliente.LimiteCompra);
                String idCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteInserir").ToString();

                return idCliente;

            }
            catch (Exception exception)
            {

                return exception.Message;
            }

        }

        public string Alterar(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IdCliente", cliente.IdCliente);
                acessoDadosSqlServer.AdicionarParametros("@Nome", cliente.Nome);
                acessoDadosSqlServer.AdicionarParametros("@DataNascimento", cliente.DataNascimento);
                acessoDadosSqlServer.AdicionarParametros("@Sexo", cliente.Sexo);
                acessoDadosSqlServer.AdicionarParametros("@LimitCompra", cliente.LimiteCompra);
                string IdCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteAlterar").ToString();
                return IdCliente;

            }
            catch (Exception exception)
            {

                return exception.Message;
            }

        }

        public string Excluir(Cliente cliente)
        {
            try
            {

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IdCliente", cliente.IdCliente);
                string IdCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteExclir").ToString();
                return IdCliente;


            }
            catch (Exception exception)
            {

                return exception.Message;
            }
        }

        public ClientesColecao ConsultarPorNome(String nome)
        {
            try
            {
                // Cria uma coleção nova de clientes ( aqui ela está vasia);
                ClientesColecao clientesColecao = new ClientesColecao();
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@Nome", nome);

                // Data = Dados e Table = Tabela
                DataTable dataTableCliente = acessoDadosSqlServer.ExecutarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorNome");

                //Percorrer o DataTable e trasfomar em coleção de clientes;
                // Cada linha do DataTable e uma linha do cliente;
                // Data = dados  Row = linhas;
                // For = para  = Each = cada;

                foreach (DataRow linha in dataTableCliente.Rows)
                {
                    // Cria um cliente vazio;
                    Cliente cliente = new Cliente();

                    // colocar os dados da linha nele;
                    cliente.IdCliente = Convert.ToInt32(linha["IdCliente"]);
                    cliente.Nome = Convert.ToString(linha["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(linha["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(linha["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(linha["LimiteCompra"]);

                    // adicionar ele na coleção;

                    clientesColecao.Add(cliente);

                }


                return clientesColecao;

            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possivel consultar o cliente por nome.  Detalhes" + ex.Message);
            }

        }

        public ClientesColecao ConsultarPorId(int idCliente)
        {
            try
            {
                ClientesColecao clientesColecao = new ClientesColecao();
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@Idcliente", idCliente);

                DataTable dataTableCliente = acessoDadosSqlServer.ExecutarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorId");

                foreach (DataRow dataRowLinha in dataTableCliente.Rows)
                {
                    // Cria um cliente vazio;
                    Cliente cliente = new Cliente();

                    // colocar os dados da linha nele;
                    cliente.IdCliente = Convert.ToInt32(dataRowLinha["IdCliente"]);
                    cliente.Nome = Convert.ToString(dataRowLinha["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(dataRowLinha["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(dataRowLinha["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(dataRowLinha["LimiteCompra"]);

                    // adicionar ele na coleção;
                    clientesColecao.Add(cliente);

                }


                return clientesColecao;
            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possivel consultar o cliente por Id.  Detalhes" + ex.Message);
            }
        }

    }
}
