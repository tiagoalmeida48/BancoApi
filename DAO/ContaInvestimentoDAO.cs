using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using System.Linq;

namespace DAO
{
    public class ContaInvestimentoDAO : clsDao
    {
        public List<Conta> RetornarListaContaInvestimento()
        {
            List<Conta> lstConta = new();

            using (SqlConnection conn = new(this.connectionString))
            {
                StringBuilder sbQuery = new();
                sbQuery.AppendLine("select cliente.nome, ");
                sbQuery.AppendLine("conta.agencia, ");
                sbQuery.AppendLine("conta.numeroConta, ");
                sbQuery.AppendLine("conta.saldo_inicial, ");
                sbQuery.AppendLine("conta.saldo_atual ");
                sbQuery.AppendLine("from conta ");
                sbQuery.AppendLine("inner join cliente on ");
                sbQuery.AppendLine("conta.cod_cli = cliente.cod_cli ");
                sbQuery.AppendLine("where conta.cod_tipoconta = 2 ");
                sbQuery.AppendLine("order by 1");

                SqlCommand objCmd = new(sbQuery.ToString(), conn);
                objCmd.CommandType = CommandType.Text;

                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    SqlDataReader sdReader = objCmd.ExecuteReader();

                    while (sdReader.Read())
                    {
                        Conta conta = new();

                        int iConvert = 0;
                        double dConvert = 0;

                        if (sdReader["Cod_Conta"] != null)
                        {
                            conta.Cod_Conta = int.TryParse(sdReader["Cod_Conta"].ToString(), out iConvert) ? iConvert : 0;
                        }
                        if (sdReader["Agencia"] != null)
                        {
                            conta.Agencia = sdReader["Agencia"].ToString();
                        }
                        if (sdReader["NumeroConta"] != null)
                        {
                            conta.NumeroConta = sdReader["NumeroConta"].ToString();
                        }
                        if (sdReader["Codigo_Banco"] != null)
                        {
                            conta.Codigo_Banco = sdReader["Codigo_Banco"].ToString();
                        }
                        if (sdReader["Cod_Cli"] != null)
                        {
                            conta.Cliente.ID = int.TryParse(sdReader["Cod_Cli"].ToString(), out iConvert) ? iConvert : 0;
                        }
                        if (sdReader["Saldo_Inicial"] != null)
                        {
                            conta.Saldo_Inicial = double.TryParse(sdReader["Saldo_Inicial"].ToString(), out dConvert) ? dConvert : 0;
                        }
                        if (sdReader["Saldo_Atual"] != null)
                        {
                            conta.Saldo_Atual = double.TryParse(sdReader["Saldo_Atual"].ToString(), out dConvert) ? dConvert : 0;
                        }
                        if (sdReader["Cod_tipoConta"] != null)
                        {
                            conta.Cod_TipoConta = int.TryParse(sdReader["Cod_tipoConta"].ToString(), out iConvert) ? iConvert : 0;
                            conta.TipoConta = (TipoConta)conta.Cod_TipoConta;
                        }
                        
                        lstConta.Add(conta);
                    }
                    if (!sdReader.IsClosed)
                    {
                        sdReader.Close();
                    }

                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex1)
                {

                    throw ex1;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return lstConta;
        }

        public Conta RetornarContaInvestimento(int CodConta)
        {
            List<Conta> lstConta = RetornarListaContaInvestimento();

            Conta conta = lstConta.Where(c => c.Cod_Conta == CodConta).FirstOrDefault();
            return conta;
        }

        public int CadastrarConta(Conta conta)
        {
            SqlConnection conn = new(this.connectionString);

            int retorno = 0;

            StringBuilder sbComando = new();
            sbComando.AppendLine("INSERT INTO CONTA (");
            sbComando.AppendLine("        Agencia");
            sbComando.AppendLine("       ,NumeroConta");
            sbComando.AppendLine("       ,Codigo_Banco");
            sbComando.AppendLine("       ,Cod_Cli");
            sbComando.AppendLine("       ,Saldo_Inicial");
            sbComando.AppendLine("       ,Saldo_Atual");
            sbComando.AppendLine("       ,Cod_tipoConta");
            sbComando.AppendLine(") VALUES (");
            sbComando.AppendLine("        @Agencia");
            sbComando.AppendLine("       ,@NumeroConta");
            sbComando.AppendLine("       ,@Codigo_Banco");
            sbComando.AppendLine("       ,@Cod_Cli");
            sbComando.AppendLine("       ,@Saldo_Inicial");
            sbComando.AppendLine("       ,@Saldo_Atual");
            sbComando.AppendLine("       ,@Cod_tipoConta");
            sbComando.AppendLine(")");

            SqlCommand cmd = new(sbComando.ToString(), conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Agencia", conta.Agencia);
            cmd.Parameters.AddWithValue("@NumeroConta", conta.NumeroConta);
            cmd.Parameters.AddWithValue("@Codigo_Banco", conta.Codigo_Banco);
            cmd.Parameters.AddWithValue("@Cod_Cli", conta.Cliente);
            cmd.Parameters.AddWithValue("@Saldo_Inicial", conta.Saldo_Inicial);
            cmd.Parameters.AddWithValue("@Saldo_Atual", conta.Saldo_Atual);
            cmd.Parameters.AddWithValue("@Cod_tipoConta", conta.Cod_TipoConta);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return retorno;
        }

        public int AtualizarConta(Conta conta)
        {
            SqlConnection conn = new(this.connectionString);

            int retorno = 0;

            StringBuilder sbComando = new();
            sbComando.AppendLine("UPDATE CONTA SET");
            sbComando.AppendLine("     Agencia = @Agencia");
            sbComando.AppendLine("    ,NumeroConta = @NumeroConta");
            sbComando.AppendLine("    ,Codigo_Banco = @Codigo_Banco");
            sbComando.AppendLine("    ,Saldo_Inicial = @Saldo_Inicial");
            sbComando.AppendLine("    ,Saldo_Atual = @Saldo_Atual");
            sbComando.AppendLine("    ,Cod_TipoConta = @Cod_tipoConta");
            sbComando.AppendLine("WHERE Cod_Conta = @Cod_Conta ");
            sbComando.AppendLine("and Cod_TipoCli = @Cod_TipoCli  ");


            SqlCommand cmd = new SqlCommand(sbComando.ToString(), conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Cod_Conta", conta.Cod_Conta);
            cmd.Parameters.AddWithValue("@Agencia", conta.Agencia);
            cmd.Parameters.AddWithValue("@NumeroConta", conta.NumeroConta);
            cmd.Parameters.AddWithValue("@Codigo_Banco", conta.Codigo_Banco);
            cmd.Parameters.AddWithValue("@Saldo_Inicial", conta.Saldo_Inicial);
            cmd.Parameters.AddWithValue("@Saldo_Atual", conta.Saldo_Atual);
            cmd.Parameters.AddWithValue("@Cod_tipoConta", conta.Cod_TipoConta);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return retorno;
        }

        public int ApagarConta(Conta conta)
        {
            SqlConnection conn = new(this.connectionString);

            int retorno = 0;

            StringBuilder sbComando = new();
            sbComando.AppendLine("DELETE FROM CONTA ");
            sbComando.AppendLine("WHERE Cod_Cli = @Cod_Cli ");
            sbComando.AppendLine(" AND Cod_Conta = @Cod_Conta");

            SqlCommand cmd = new SqlCommand(sbComando.ToString(), conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Cod_Conta", conta.Cod_Conta);
            cmd.Parameters.AddWithValue("@Cod_Cli", conta.Cliente.ID);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return retorno;
        }
    }
}
