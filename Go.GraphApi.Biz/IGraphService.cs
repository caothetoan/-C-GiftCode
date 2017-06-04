namespace Go.GraphApi.Biz
{
    public enum CardType
    {
        Vietel = 1,
        Vinaphone = 2,
        Mobiphone = 3,
        Vcoin = 4,
        VietNamMobile = 5,
        SFONE = 6,
        Gate = 7,
        Megacard = 8
    }

    public interface IGraphService
    {
        AuthenResult Authenticate(string username, string password, out string message);

        AuthenResult AuthenticateDetail(string username, string password, out string error, out string message);

        AuthenResult FastRegister(string username, string password, out string message);

        BaseApiResult UpdatePassword(string username, string password, string newpass, string accesstoken, out string message);

        AuthenResult FastPlay(string deviceid, string email, string cpid, string refcode, string agent, string platform, out string message);

        AuthenResult LoginOverFaceBook(string facebookid, string email, string fullname, string cpid, string refcode, string agent, string platform, out  string message);

        BillingResult ActiveTelCo(string accesstoken, string accountName, string cardCode, string cardSerial, int cardType, int transactionId, out string message);

        DataBalanceResult GetBalance(string userid, string token);

        DataBuyItemResult BuyItem(string username, string token, long transId, long itemcode, long itemprice, string accountIP, string description, out string message);

        bool ConfirmTopup(long gopc_billingtransid, bool gopc_transconfirm, string gopc_servicekey, string gopc_serviceappid, string gopc_securehash, string gopc_transid);

        UserData GetInfo(string token);
    }
}
