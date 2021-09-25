using System;
using System.Collections.Generic;
using System.Text;

namespace DAO
{
    public class clsDao
    {
        public string connectionString = @"server=DESKTOP-P5B1ELC;database=BANCO;integrated security=yes;";

        public string ConnString
        {
            get
            {
                return this.connectionString;
            }
        }
    }
}