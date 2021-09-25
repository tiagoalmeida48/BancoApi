using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using System.Linq;

namespace DAO
{
    public class ClientePFDAO : clsDao
    {
        public List<ClientePF> RetornarListaCliente()
        {
            List<ClientePF> lstCliente = new();
            using (SqlConnection conn = new(this.connectionString))
            {
                StringBuilder sbQuery = new();
                sbQuery.AppendLine("SELECT cli.Cod_Cli");
                sbQuery.AppendLine(",cli.Nome");
                sbQuery.AppendLine(",cli.Data_NascFund");
                sbQuery.AppendLine(",cli.Renda_Lucro");
                sbQuery.AppendLine(",cli.Sexo");
                sbQuery.AppendLine(",cli.Email");
                sbQuery.AppendLine(",cli.Endereco");
                sbQuery.AppendLine(",cli.Documento");
                sbQuery.AppendLine(",cli.Cod_TipoCli");

                sbQuery.AppendLine(" FROM Cliente cli");
                sbQuery.AppendLine(" INNER JOIN TipoCli tp on tp.Cod_TipoCli = cli.Cod_TipoCli");
                sbQuery.AppendLine(" WHERE tp.Nome_TipoCli = 'PF'");

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
                        ClientePF cli = new ClientePF();


                        int iConvert = 0;
                        double dConvert = 0;
                        DateTime dtConvert = DateTime.MinValue;


                        if (sdReader["Cod_Cli"] != null)
                        {
                            cli.ID = int.TryParse(sdReader["Cod_Cli"].ToString(), out iConvert) ? iConvert : 0;
                        }
                        if (sdReader["Nome"] != null)
                        {
                            cli.Nome = sdReader["Nome"].ToString();
                        }
                        if (sdReader["Data_NascFund"] != null)
                        {
                            cli.DtNascFund = DateTime.TryParse(sdReader["Data_NascFund"].ToString(), out dtConvert) ? dtConvert : DateTime.MinValue;
                        }
                        if (sdReader["Renda_Lucro"] != null)
                        {
                            cli.Renda = double.TryParse(sdReader["Renda_Lucro"].ToString(), out dConvert) ? dConvert : 0;
                        }
                        if (sdReader["Sexo"] != null)
                        {
                            cli.Sexo = sdReader["Sexo"].ToString();
                        }

                        if (sdReader["Email"] != null)
                        {
                            cli.Email = sdReader["Email"].ToString();
                        }
                        if (sdReader["Endereco"] != null)
                        {
                            cli.Endereco = sdReader["Endereco"].ToString();
                        }
                        if (sdReader["Documento"] != null)
                        {
                            cli.CPF = sdReader["Documento"].ToString();
                        }
                        if (sdReader["Cod_TipoCli"] != null)
                        {
                            cli.cod_tp_Cliente = int.TryParse(sdReader["Cod_TipoCli"].ToString(), out iConvert) ? iConvert : 0;
                            cli.TipoCliente = (TipoCliente)cli.cod_tp_Cliente;
                        }

                        lstCliente.Add(cli);
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
            return lstCliente;
        }

        public ClientePF RetornarCliente(int CodCli)
        {
            List<ClientePF> lstCli = RetornarListaCliente();

            ClientePF cli = lstCli.Where(c => c.ID == CodCli).FirstOrDefault();
            return cli;
        }

        public int CadastrarCliente(ClientePF cli)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);

            int retorno = 0;

            StringBuilder sbComando = new StringBuilder();
            sbComando.AppendLine("INSERT INTO CLIENTE (");
            sbComando.AppendLine("                       Cod_TipoCli");
            sbComando.AppendLine("                      ,Nome");
            sbComando.AppendLine("                      ,Data_NascFund");
            sbComando.AppendLine("                      ,Renda_Lucro");
            sbComando.AppendLine("                      ,Sexo");
            sbComando.AppendLine("                      ,Email");
            sbComando.AppendLine("                      ,Endereco");
            sbComando.AppendLine("                      ,Documento");
            sbComando.AppendLine("                     )");
            sbComando.AppendLine("VALUES (");
            sbComando.AppendLine("        @Cod_TipoCli");
            sbComando.AppendLine("       ,@Nome");
            sbComando.AppendLine("       ,@Data_NascFund");
            sbComando.AppendLine("       ,@Renda_Lucro");
            sbComando.AppendLine("       ,@Sexo");
            sbComando.AppendLine("       ,@Email");
            sbComando.AppendLine("       ,@Endereco");
            sbComando.AppendLine("       ,@Documento");
            sbComando.AppendLine("       )");

            SqlCommand cmd = new SqlCommand(sbComando.ToString(), conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Cod_TipoCli", (int)TipoCliente.PF);
            cmd.Parameters.AddWithValue("@Nome", cli.Nome);
            cmd.Parameters.AddWithValue("@Data_NascFund", cli.DtNascFund);
            cmd.Parameters.AddWithValue("@Renda_Lucro", cli.Renda);
            cmd.Parameters.AddWithValue("@Sexo", cli.Sexo);
            cmd.Parameters.AddWithValue("@Email", cli.Email);
            cmd.Parameters.AddWithValue("@Endereco", cli.Endereco);
            cmd.Parameters.AddWithValue("@Documento", cli.CPF);

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

        public int AtualizarCliente(ClientePF cli)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);

            int retorno = 0;

            StringBuilder sbComando = new StringBuilder();
            sbComando.AppendLine("UPDATE CLIENTE ");
            sbComando.AppendLine(" SET ");
            sbComando.AppendLine("    Nome = @Nome");
            sbComando.AppendLine("    ,Data_NascFund = @Data_NascFund");
            sbComando.AppendLine("    ,Renda_Lucro = @Renda_Lucro");
            sbComando.AppendLine("    ,Sexo = @Sexo");
            sbComando.AppendLine("    ,Email = @Email");
            sbComando.AppendLine("    ,Endereco = @Endereco");
            sbComando.AppendLine("    ,Documento = @Documento");
            sbComando.AppendLine("WHERE Cod_Cli = @Cod_Cli ");
            sbComando.AppendLine("and Cod_TipoCli =@Cod_TipoCli  ");

            SqlCommand cmd = new SqlCommand(sbComando.ToString(), conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Cod_Cli", cli.ID);
            cmd.Parameters.AddWithValue("@Cod_TipoCli", (int)TipoCliente.PF);
            cmd.Parameters.AddWithValue("@Nome", cli.Nome);
            cmd.Parameters.AddWithValue("@Data_NascFund", cli.DtNascFund);
            cmd.Parameters.AddWithValue("@Renda_Lucro", cli.Renda);
            cmd.Parameters.AddWithValue("@Sexo", cli.Sexo);
            cmd.Parameters.AddWithValue("@Email", cli.Email);
            cmd.Parameters.AddWithValue("@Endereco", cli.Endereco);
            cmd.Parameters.AddWithValue("@Documento", cli.CPF);

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

        public int ApagarCliente(ClientePF cli)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);

            int retorno = 0;

            StringBuilder sbComando = new StringBuilder();
            sbComando.AppendLine("DELETE FROM CLIENTE ");
            sbComando.AppendLine("WHERE Cod_Cli = @Cod_Cli ");
            sbComando.AppendLine(" AND Cod_TipoCli = @Cod_TipoCli");



            SqlCommand cmd = new SqlCommand(sbComando.ToString(), conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Cod_Cli", cli.ID);
            cmd.Parameters.AddWithValue("@Cod_TipoCli", (int)TipoCliente.PF);


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
