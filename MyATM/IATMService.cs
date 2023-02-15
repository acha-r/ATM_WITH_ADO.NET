using MyATM.Model;

namespace MyATM
{
    public interface IATMService : IDisposable
    {
        int RegisterUser(UserModel user);
        int RegisterCard(CardModel card);
        void Login(int cardid);
        void GetWithdrawalRecords(int userid);
        void GetTransferRecords(int userid);
    }
}
