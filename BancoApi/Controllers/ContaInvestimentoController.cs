using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DAO;
using Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BancoAPI.Controllers
{
    [Route("banco/contaInvestimento")]
    [ApiController]
    public class ContaInvestimentoController : ControllerBase
    {
        [HttpGet]
        public List<Conta> RetornarContas()
        {
            ContaInvestimentoDAO objConta = new();

            return objConta.RetornarListaContaInvestimento();
        }

        [HttpGet("{id}")]
        public Conta RetornarConta(int CodConta)
        {
            ContaInvestimentoDAO objConta = new ContaInvestimentoDAO();
            List<Conta> lstConta = objConta.RetornarListaContaInvestimento();

            List<Conta> lstContaWhere = lstConta.Where(c => c.Cod_Conta == CodConta).Take(1).ToList();

            Conta conta = lstContaWhere.FirstOrDefault();

            return conta;
        }

        [HttpGet]
        [Route("mediaSaldo")]
        public double MediaSaldo()
        {
            ContaInvestimentoDAO contaDao = new();

            List<Conta> lstContaInvest = contaDao.RetornarListaContaInvestimento();

            return lstContaInvest.Average(c => c.Saldo_Atual);
        }

        [HttpPost]
        public Conta Details([FromBody] Conta conta)
        {
            ContaInvestimentoDAO contaDao = new();
            return contaDao.RetornarContaInvestimento(conta.Cod_Conta);
        }

        [HttpPut]
        public ActionResult<IEnumerable<string>> Put([FromBody] Conta conta)
        {
            int retorno = 0;
            ContaInvestimentoDAO contaDAO = new();
            if (conta.Cod_Conta < 1)
            {
                retorno = contaDAO.CadastrarConta(conta);
            }
            else
            {
                retorno = contaDAO.AtualizarConta(conta);
            }
            if (retorno == 1)
            {
                return new string[] { "Conta inserida ou atualizada com sucesso!" };
            }
            return new string[] { "Conta não inserida ou não atualizada!" };
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<string>> Delete(int codConta)
        {
            int retorno = 0;
            if (codConta == 0)
            {
                return new string[] { "Conta invalido!" };
            }
            else
            {
                ContaInvestimentoDAO contaDAO = new();
                Conta conta = new();
                conta.Cod_Conta = codConta;

                retorno = contaDAO.ApagarConta(conta);
                if (retorno == 1)
                {
                    return new string[] { "Conta excluida com sucesso!" };
                }

            }
            return new string[] { string.Empty };
        }
    }
}