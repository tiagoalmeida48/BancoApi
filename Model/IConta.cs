﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public interface IConta
    {
        double Sacar(double valor);

        double Depositar(double valor);
    }
}