using Microsoft.Data.SqlClient;
using MyATM.Model;
using System.Data;

namespace MyATM
{
    public class ATMService : IATMService
    {
        private readonly ATMDbContext _atmdbcontext;
        private bool disposedValue;

        public ATMService()
        {

        }
        public ATMService(ATMDbContext atmdbcontext)
        {
            _atmdbcontext = atmdbcontext;
        }

        public int RegisterUser(UserModel user)
        {
            var sqlconnection = _atmdbcontext.OpenConn();

            string insertQuery =
                $"INSERT INTO CustomerInfo (LastName, FirstName, AccountBal)" +
                $"VALUES ('{user.LastName}', '{user.FirstName}', '{user.AcctBal}')";

            SqlCommand command = new SqlCommand(insertQuery, sqlconnection);

            command.ExecuteNonQuery();

            string userIdQuery = $"SELECT SCOPE_IDENTITY();";
            command.CommandText = userIdQuery;

            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                var value = reader.GetValue(0);
                if (!value.Equals(DBNull.Value))
                {
                    return (int)(decimal)reader.GetValue(0);
                }
            }
            return 0;
        }

        public int RegisterCard(CardModel card)
        {

            var sqlconnection = _atmdbcontext.OpenConn();

            string insertQuery = $"INSERT INTO CardInfo (CardNo, CardUserID)" +
            $"VALUES ('{card.CardNo}', '{card.CardUserID}')";

            SqlCommand command = new SqlCommand(insertQuery, sqlconnection);
            command.ExecuteNonQuery();


            string cardIdQuery = $"SELECT SCOPE_IDENTITY();";
            command.CommandText = cardIdQuery;

            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                var value = reader.GetValue(0);
                if (!value.Equals(DBNull.Value))
                {
                    Console.WriteLine("Card successfully registered");
                    return (int)(decimal)reader.GetValue(0);
                }
            }
            return 0;
        }

        public void Login(int cardid)
        {
            var sqlconnection = _atmdbcontext.OpenConn();

            string insertQuery = $"SELECT CardID FROM CardInfo WHERE CardID = {cardid}";
            SqlCommand command = new SqlCommand(insertQuery, sqlconnection);

            try
            {
                int result = (int)command.ExecuteScalar();
            }
            catch (Exception)
            {

                Console.WriteLine("invalid card detail");
            }
            _atmdbcontext.CloseConnection();

            Console.WriteLine("Welcome.\n\nEnter 4 digit pin below");
            GetPin();
            TakeAction();
        }

        private static void GetPin()
        {
            int allowedpinTries = 3;

        enterpin:
            string pin = Console.ReadLine();

            if (pin.Length == 4 && int.TryParse(pin, out int x))
            {
                Console.WriteLine("\nLog in successful\n");
                Thread.Sleep(1000);
                Console.Clear();
                return;
            }

            for (int i = 1; i <= allowedpinTries; i++)
            {
                --allowedpinTries;
                if (allowedpinTries == 0)
                {
                    Console.WriteLine("\nCard blocked. Visit the bank for further assistance");
                    Environment.Exit(1);
                }
                Console.Clear();
                Console.WriteLine($"\nPin must be a 4-digit number.\nYou have " +
                $"{allowedpinTries} tries left\n");
                goto enterpin;
            }
        }

        private static void TakeAction()
        {
        start:
            Console.WriteLine("\nWhat would you like to do? Press\n\n" +
                "1. For withdrawal\n\n2. For transfer\n\nPress 0 to cancel");
            string userInput = Console.ReadLine();
            Console.Clear();

            switch (userInput)
            {
                case "1":
                    WithdrawFunds();
                    break;
                case "2":
                    TransferFunds();
                    break;
                case "0":
                    Console.WriteLine("\nSee you some other time");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nINVALID INPUT");
                    goto start;
            }
        }

        private static void WithdrawFunds()
        {
            Console.WriteLine("Withdrawal");
        }


        public void GetTransferRecords(int userid)
        {
            throw new NotImplementedException();
        }

        public void GetWithdrawalRecords(int userid)
        {
            throw new NotImplementedException();
        }

        private static void TransferFunds()
        {
            throw new NotImplementedException();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
            {
                return;
            }
            if (disposing)
            {
                _atmdbcontext.Dispose();
            }

            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
