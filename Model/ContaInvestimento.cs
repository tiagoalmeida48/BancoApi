using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Model
{
    public class ContaInvestimento : Conta
    {
        public double Sacar(double valor)
        {
            var SaldoAposSaque = this.Saldo_Inicial -= valor;
            return SaldoAposSaque;
        }

        public double Depositar(double valor)
        {
            var SaldoAposDeposito = this.Saldo_Inicial += valor;
            return SaldoAposDeposito;
        }

        public double RenderJuros()
        {
            var juros = this.Saldo_Inicial * (0.2 / 100);
            var SaldoAposJuros = this.Saldo_Inicial + juros;
            return SaldoAposJuros;
        }

        public ContaInvestimento RetornarCliente()
        {
            ContaInvestimento objCtaInvest = new ContaInvestimento();

            objCtaInvest.CodConta = 1;

            ClientePF objCliPF = new ClientePF();
            objCliPF.ID = 1;
            objCliPF.Nome = "Tiago Almeida";
            objCliPF.CPF = "375.464.464-45";

            objCtaInvest.Cliente = objCliPF;

            objCtaInvest.Agencia = "0001";
            objCtaInvest.NumeroConta = "25864-3";
            objCtaInvest.Codigo_Banco = "865";
            objCtaInvest.Saldo_Inicial = 1500;
            objCtaInvest.Saldo_Atual = objCtaInvest.Sacar(100);

            return objCtaInvest;

        }

        public List<ContaInvestimento> RetornarListaClientes()
        {
            List<ContaInvestimento> lstContas = new List<ContaInvestimento>();
            lstContas.Add(RetornarCliente());

            ContaInvestimento objCtaInvest1 = new ContaInvestimento();

            objCtaInvest1.CodConta = 2;

            ClientePF objCliPF1 = new ClientePF();
            objCliPF1.ID = 2;
            objCliPF1.Nome = "Miriam";
            objCliPF1.CPF = "345.321.345-45";

            objCtaInvest1.Cliente = objCliPF1;

            objCtaInvest1.Agencia = "0021";
            objCtaInvest1.NumeroConta = "586125-8";
            objCtaInvest1.Codigo_Banco = "752";
            objCtaInvest1.Saldo_Inicial = 2000;
            objCtaInvest1.Saldo_Atual = objCtaInvest1.RenderJuros();

            lstContas.Add(objCtaInvest1);

            ContaInvestimento objCtaInvest2 = new ContaInvestimento();

            objCtaInvest2.CodConta = 3;

            ClientePF objCliPF2 = new ClientePF();
            objCliPF2.ID = 3;
            objCliPF2.Nome = "Armando";
            objCliPF2.CPF = "896.568.123-45";

            objCtaInvest2.Cliente = objCliPF2;

            objCtaInvest2.Agencia = "0001";
            objCtaInvest2.NumeroConta = "39789-3";
            objCtaInvest2.Codigo_Banco = "955";
            objCtaInvest2.Saldo_Inicial = 500;
            objCtaInvest2.Saldo_Atual = objCtaInvest2.Depositar(1000);

            lstContas.Add(objCtaInvest2);

            return lstContas;
        }
    }
}