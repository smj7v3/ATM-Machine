using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ATM
{

    [Serializable()]


    public class Interface
    {
        DateTime FirstDate;
        DateTime SecondDate;
        protected bool dateFlag;
        protected double rate;
        Account[] acct = new Account[3];
        int i;


        public void populateArray()
        {
            for (int x = 0; x < acct.Length; x++)
            {
                acct[x] = new Account();
            }           
        }


        public void AccountMenu()
        {
            string sinput = null;
            int input = -1;

            while (input != -99)
            {
                Console.WriteLine("Please choose an account: 0, 1, or 2 -- choose 4 to exit ");

                sinput = Console.ReadLine();
                input = Convert.ToInt32(sinput);
               
                if (input == 0 || input == 1 || input == 2)
                {
                    i = input;
                    Console.WriteLine("");
                    Menu();
                }
                if (input == 4)
                {
                    writeArray();
                    int code = 4;
                    Environment.Exit(code);
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Please choose a valid account. ");
                    Console.WriteLine("");
                }               
                AccountMenu();
            }
        }


        public void Menu()     
        {
            writeArray();
            int menuInput = 0;

            while (menuInput != -99)
            {
                Console.WriteLine("Account #" + i);
                Console.WriteLine("1) Deposit");
                Console.WriteLine("2) Withdraw");
                Console.WriteLine("3) Check Balance");
                Console.WriteLine("4) Log Out");
                Console.WriteLine("Please enter a choice: ");

                menuInput = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");

                if (menuInput == 1)
                {
                    Deposit();
                    Menu();
                }
                if (menuInput == 2)
                {
                    Withdraw();
                    Menu();
                }
                if (menuInput == 3)
                {
                    getBalance();
                    Menu();
                }
                if (menuInput == 4)
                {
                    Console.WriteLine("Logging out...");
                    Console.WriteLine("");
                    AccountMenu();
                }
                else
                {
                    Console.WriteLine("Please enter a valid choice.");
                    Console.WriteLine("");
                    Menu();
                }
            }
        }


        private void Deposit()
        {
            if (dateFlag == false)
            {
                getDate1();
            }
            else
            {
                getDate2();
                getInterest();
            }

            Console.WriteLine("How much would you like to deposit?:");
            String dep = Console.ReadLine();
            decimal inputDep = Convert.ToDecimal(dep);
            Console.WriteLine("");

            acct[i].Balance += inputDep;

            Console.WriteLine("New balance: " + acct[i].Balance.ToString("C", CultureInfo.CurrentCulture));
            Console.WriteLine("");
        }



        private void Withdraw()
        {
            if (dateFlag == false)
            {
                getDate1();
            }
            else
            {
                getDate2();
                getInterest();
            }

            Console.WriteLine("How much would you like to withdraw? ");
            String wit = Console.ReadLine();
            decimal inputWit = Convert.ToDecimal(wit);
            Console.WriteLine("");

            acct[i].Balance -= inputWit;
            Console.WriteLine("New balance: " + acct[i].Balance.ToString("C", CultureInfo.CurrentCulture));
            Console.WriteLine("");
        }



        private void getBalance()
        {
            if (dateFlag == false)
            {
                getDate1();
            }
            else
            {
                getDate2();
                getInterest();
            }

            Console.WriteLine("Balance: " + acct[i].Balance.ToString("C", CultureInfo.CurrentCulture));
            Console.WriteLine("");
        }


        private void getInterest()
        {
            double datediff = (SecondDate - FirstDate).TotalDays;
            rate = .05 / 365;

            double ratetime = Math.Pow(1 + rate, datediff);
            decimal rater = System.Convert.ToDecimal(ratetime);

            acct[i].Balance *= rater;
        }


        private void getDate1()
        {
            Console.WriteLine("Please enter today's date [mm/dd/yyyy]: ");
            String mydate = Console.ReadLine();
            Console.WriteLine("");

            DateTime Test;
            if (DateTime.TryParseExact(mydate, "mm/dd/yyyy", null, DateTimeStyles.None, out Test) == true)
            {
                FirstDate = Convert.ToDateTime(mydate);
                dateFlag = true;
                
            }
            else
            {
                Console.WriteLine("Please follow the proper format for date input.");
                Console.WriteLine("");
                getDate1();
            }
        }


        private void getDate2()
        {
            Console.WriteLine("Please enter today's date [mm/dd/yyyy]: ");
            String mydate = Console.ReadLine();

            DateTime Test;
            if (DateTime.TryParseExact(mydate, "mm/dd/yyyy", null, DateTimeStyles.None, out Test) == true)
            {
                SecondDate = Convert.ToDateTime(mydate);
                dateFlag = true;
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Please follow the proper format for date input.");
                Console.WriteLine("");
                getDate2();
            }
            if (FirstDate.DayOfYear > SecondDate.DayOfYear)
            {
                Console.WriteLine("Enter a proper date");
                getDate2();

            }

        }


        //private void getDate1()
        //{
        //    Console.WriteLine("Please enter today's date [mm/dd/yyyy]: ");
        //    String mydate = Console.ReadLine();

        //    FirstDate = Convert.ToDateTime(mydate);
        //    dateFlag = true;
        //    Console.WriteLine("");
        //}


        //private void getDate2()
        //{
        //    Console.WriteLine("Please enter TODAY'S date [mm/dd/yyyy]: ");
        //    String mydate = Console.ReadLine();
        //    SecondDate = Convert.ToDateTime(mydate);
        //    dateFlag = true;
        //    Console.WriteLine("");

        //    if (FirstDate.DayOfYear > SecondDate.DayOfYear)
        //    {
        //        Console.WriteLine("Enter a proper date");
        //        getDate2();

        //    }

        //}

        public void writeArray()
        {
            Stream FileStream = File.Create("Accounts.xml");
            XmlSerializer serializer = new XmlSerializer(acct.GetType());
            XmlSerializer seriously = new XmlSerializer(typeof(Account[]));
            seriously.Serialize(FileStream, acct);
            FileStream.Close();
        }


        public void readArray()
        {
            Stream Filestream = File.OpenRead("Accounts.xml");
            XmlSerializer deserializer = new XmlSerializer(typeof(Account[]));
            acct = (Account[])deserializer.Deserialize(Filestream);
            Filestream.Close();

        }
    }
}
