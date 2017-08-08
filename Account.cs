using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class Account
    {
        private decimal balance = 100;

        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }


        }
    }
}

