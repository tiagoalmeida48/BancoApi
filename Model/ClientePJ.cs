using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class ClientePJ : Cliente
    {

        public string CNPJ { get; set; }

        public double Lucro { get; set; }

        public string TipoEmpresa { get; set; }
    }
}