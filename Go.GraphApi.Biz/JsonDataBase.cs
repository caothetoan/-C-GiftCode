namespace Go.GraphApi.Biz
{
    public class AuthenResult
    {
        public bool IsAuthen { get; set; }

        public string AccessToken { get; set; }

        public string PublicName { get; set; }

        public long AccountId { get; set; }

        public string AccountName { get; set; }
    }

    public class GetSaltResult
    {
        public string _code { set; get; }
        public string _data { set; get; }
        public string _message { set; get; }
    }

    public class BaseApiResult
    {
        public string _code { set; get; }

        public string _message { set; get; }
    }

    public class UpdateResult : BaseApiResult
    {
        public int _data { get; set; }
    }

    public class DataBalanceResult
    {
        public string GameBalance { get; set; }
        public string ResponseStatus { get; set; }
        public string TotalGameBalance { get; set; }
        public string Vcoin { get; set; }
        public string VcoinBalance { get; set; }
    }

    public class GetBalanceReult : BaseApiResult
    {
        public DataBalanceResult _data { set; get; }
    }

    public class DataBuyItemResult
    {
        public string ResponseStatus { get; set; }
        public string GameBalance { get; set; }
        public string TotalGameBalance { get; set; }
        public string Vcoin { get; set; }
        public string VcoinBalance { get; set; }
    }

    public class GetBuyItemReult : BaseApiResult
    {
        public DataBuyItemResult _data { set; get; }
    }

    public class DataInputMoneyBetting
    {
        public string ResponseStatus { get; set; }
        public string GameBalance { get; set; }
        public string TotalGameBalance { get; set; }
        public string Vcoin { get; set; }
        public string VcoinBalance { get; set; }
    }

    public class InputMoneyBetting : BaseApiResult
    {
        public DataInputMoneyBetting _data { set; get; }
    }

    public class BillingInfo
    {
        public int ResponseStatus { get; set; }
        public long BillingCardLogId { get; set; }
        public int Money { get; set; }
        public string Description { get; set; }
    }

    public class BillingResult : BaseApiResult
    {
        public BillingInfo _data { get; set; }
    }

    public class FastPlayResult : BaseApiResult
    {
        public FastPlayData _data { get; set; }
    }

    public class GetAccessTokenResult
    {
        public string access_token { set; get; }
        public string code { set; get; }
        public string expires { set; get; }
        public string User { set; get; }
        public string UserId { set; get; }
    }

    public class LoginResult
    {
        public string _code { set; get; }
        public LoginData _data;
        public string _message { set; get; }
    }
    public class LoginData
    {
        public string Code { set; get; }
        public string Expires { set; get; }
        public string User { set; get; }
        public string UserId { set; get; }
    }

    public class FastPlayData
    {
        public string Code { set; get; }
        public string Expires { set; get; }
        public string User { set; get; }
        public string UserId { set; get; }
        public string access_token { get; set; }
    }

    public class RegisterResult
    {
        public string _code { set; get; }
        public LoginData _data;
        public string _message { set; get; }
    }

    public class ConfirmTopupResult
    {
        public int gopc_responsecode { get; set; }
        public int gopc_transid { get; set; }
        public string gopc_securehash { get; set; }
    }

    public class UserInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Picture { get; set; }
        public string AccountName { get; set; }
        public string Birthday { get; set; }
        public byte Gender { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Mobile { get; set; }
        public string PublicName { get; set; }
    }

    public class UserData : BaseApiResult
    {
        public UserInfo _data { get; set; }
    }
}
