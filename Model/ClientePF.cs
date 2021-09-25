using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class ClientePF : Cliente
    {
        public string CPF { get; set; }

        public double Renda { get; set; }

        public string Sexo { get; set; }
    }
}