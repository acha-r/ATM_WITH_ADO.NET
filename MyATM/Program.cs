using MyATM.Model;

namespace MyATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***ATM Services***");
            using (IATMService atm = new ATMService(new ATMDbContext()))
            {
                /*try
                {
                    var userData = new UserModel
                    {
                        LastName = "Ogubuike",
                        FirstName = "Ozigi",
                        AcctBal = "60900.00"
                    };

                    var createdUserId = atm.RegisterUser(userData);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid data type supplied");
                }


                try
                {
                    var cardData = new CardModel
                    {
                        CardNo = 90988987887,
                        CardUserID = 300
                    };

                    atm.RegisterCard(cardData);

                    Console.WriteLine("done");
                }
                catch (Exception)
                {
                    Console.WriteLine("Oops! One or both of the following error(s) occurred. \n\n\tCard number is invalid or belongs to another account\n\tUser ID is invalid or belongs to another card");

                }*/

                atm.Login(4);

            }

        }

    }
}