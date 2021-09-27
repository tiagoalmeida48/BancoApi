using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Conta : IConta
    {
        public int CodConta { get; set; }

        public string Agencia { get; set; }

        public string NumeroConta { get; set; }

        public string Codigo_Banco { get; set; }

        public Cliente Cliente { get; set; }
        public int CodCli { get; set; }

        public double Saldo_Inicial { get; set; }

        public double Saldo_Atual { get; set; }

        public TipoConta TipoConta { get; set; }
        public int Cod_TipoConta { get; set; }

        public double Sacar(double valor)
        {
            return 0;
        }

        public double Depositar(double valor)
        {
            return 0;
        }
    }
}