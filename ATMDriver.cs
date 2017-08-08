using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class ATMDriver
    {

        public static void Main(string[] args)
        {
            Interface ai = new Interface();
            Console.WriteLine("First time running? ");
            string input = Console.ReadLine();

            Console.WriteLine("");

            if (input.ToLower().Equals("yes"))
            {
                ai.populateArray();
                //ai.writeArray();
            }
            else
            {
                ai.readArray();
            }
            ai.AccountMenu();
        }
    }
}
