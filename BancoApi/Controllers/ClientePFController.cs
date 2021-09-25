using DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BancoAPI.Controllers
{
    [Route("banco/clipf")]
    [ApiController]
    public class ClientePFController : ControllerBase
    {
        [HttpGet]
        public List<ClientePF> RetornarClientes()
        {
            ClientePFDAO objDao = new();
            return objDao.RetornarListaCliente();
        }


        [HttpGet("{codCli}")]
        public Cliente ReturnCli(int codCli)
        {
            ClientePFDAO clienteDAO = new ClientePFDAO();
            List<ClientePF> lstCliPF = clienteDAO.RetornarListaCliente();


            List<ClientePF> lstCliPFWhere = lstCliPF.Where(c => c.ID == codCli).Take(1).ToList();

            ClientePF cli = lstCliPFWhere.FirstOrDefault();

            return cli;
        }

        [HttpGet]
        [Route("QtdClientes")]
        public double QtdClientes()
        {
            ClientePFDAO objDao = new ClientePFDAO();

            List<ClientePF> lstCliPF = objDao.RetornarListaCliente();

            return lstCliPF.Count();
        }

        [HttpGet]
        [Route("MediaRendaCliente")]
        public double MediaRendaCliente()
        {
            ClientePFDAO objDao = new ClientePFDAO();

            List<ClientePF> lstCliPF = objDao.RetornarListaCliente();

            return lstCliPF.Average(c => c.Renda);
        }

        [HttpGet]
        [Route("ValorTotalRenda")]
        public double ValorTotalRenda()
        {
            ClientePFDAO objDao = new ClientePFDAO();

            List<ClientePF> lstCliPF = objDao.RetornarListaCliente();

            return lstCliPF.Sum(c => c.Renda);
        }

        [HttpGet]
        [Route("MaiorRenda")]
        public double MaiorRenda()
        {
            ClientePFDAO objDao = new ClientePFDAO();

            List<ClientePF> lstCliPF = objDao.RetornarListaCliente();

            return lstCliPF.Max(c => c.Renda);
        }

        [HttpGet]
        [Route("MenorRenda")]
        public double MenorRenda()
        {
            ClientePFDAO objDao = new ClientePFDAO();

            List<ClientePF> lstCliPF = objDao.RetornarListaCliente();

            return lstCliPF.Min(c => c.Renda);
        }


        [HttpGet]
        [Route("FirstOrDefault")]
        public ClientePF FirstOrDefault()
        {
            ClientePFDAO objDao = new ClientePFDAO();

            List<ClientePF> lstCliPF = objDao.RetornarListaCliente();

            return lstCliPF.FirstOrDefault(c => c.TipoCliente == TipoCliente.PF);
        }


        [HttpPost]
        [Route("DetailsPost")]
        public ClientePF DetailsPost([FromBody] ClientePF cli)
        {
            ClientePFDAO objDao = new ClientePFDAO();

            return objDao.RetornarCliente(cli.ID);

        }


        [HttpPost]
        public ClientePF Details([FromBody] ClientePF cli)
        {
            ClientePFDAO objDao = new ClientePFDAO();

            return objDao.RetornarCliente(cli.ID);

        }


        [HttpPut]
        public ActionResult<IEnumerable<string>> Put([FromBody] ClientePF cli)
        {
            int retorno = 0;
            ClientePFDAO clienteDAO = new ClientePFDAO();
            if (cli.ID < 1)
            {
                retorno = clienteDAO.CadastrarCliente(cli);
            }
            else
            {
                retorno = clienteDAO.AtualizarCliente(cli);
            }
            if (retorno == 1)
            {
                return new string[] { "Cliente Inserido ou atualizado com sucesso!" };
            }
            return new string[] { "Cliente Não inserido ou não atualizado!" };
        }

        [HttpDelete("{codCli}")]
        public ActionResult<IEnumerable<string>> Delete(int codCli)
        {
            int retorno = 0;
            if (codCli == 0)
            {
                return new string[] { "Cliente invalido!" };
            }
            else
            {
                ClientePFDAO clienteDAO = new ClientePFDAO();
                ClientePF cli = new ClientePF();
                cli.ID = codCli;

                retorno = clienteDAO.ApagarCliente(cli);
                if (retorno == 1)
                {
                    return new string[] { "Cliente excluido com sucesso!" };
                }

            }
            return new string[] { string.Empty };
        }
    }
}