using System;
using System.Web;
using Go.GraphApi.Biz;
using Libs;

namespace webXBGiftCode
{
    public partial class Login : System.Web.UI.Page
    {
        protected int AccountId
        {
            get
            {
                if (Session["AccountId"] != null)
                    return int.Parse(Session["AccountId"].ToString());
                return -1;
            }
        }
        protected bool IsOnline
        {
            get { return AccountId > 0; }
        }
        protected string UrlAbsoluteUri
        {
            get { return HttpContext.Current.Request.Url.AbsoluteUri; }
        }
      
        protected string UrlReturn
        {
            get { return Request.Params["ur"]; }

        }
        protected void lbtnLogin_OnClick(object sender, EventArgs e)
        {
            var graph = new GraphService();

            string message;
            var username = txtUserName.Text;
            var userPassword = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userPassword))
            {
                // chưa nhập tên hoặc mật khẩu
                lbMsg.Text = "Bạn chưa nhập tên đăng nhập hoặc mật khẩu";
                return;
            }
            var loginResult = graph.Authenticate(username.ToLower(), Encrypt.MD5(userPassword), out message);

            long accountId = loginResult.AccountId;
            string accountName = loginResult.AccountName;

            string accessToken = loginResult.AccessToken;

            if (loginResult.IsAuthen)
            {
                // "Đăng nhập thành công";
                // write session

                HttpContext.Current.Session.Add("AccountId", accountId);
                HttpContext.Current.Session.Add("AccountName", accountName);
                HttpContext.Current.Session.Add("AccessToken", accessToken);

                Log.WriteLog("Account Login IsAuthen true write session succeed", message);
                var db = new DbQuaTangGiftCode();
                var continuousLogin = 0;
                var countLoginInDay = 0;
                var isFirst = "";
                var responseStatus = 0;
                try
                {
                    db.SP_Accounts_SetLastLoginTime((int)accountId, accountName, DBCommon.ClientIP,
              ref  continuousLogin, ref  countLoginInDay, ref isFirst, ref  responseStatus);
                }
                catch (Exception exSetLastLoginTime)
                {
                    Log.WriteLog("Account Login IsAuthen true SP_Accounts_SetLastLoginTime Exception", exSetLastLoginTime.Message);

                }
                Response.Redirect(UrlHelper.UrlReturn);
            }
            else
            {
                Log.WriteLog("Account Login SP_Accounts_SetLastLoginTime IsAuthen false", message);
                lbMsg.Text = "Sai tên đăng nhập hoặc mật khẩu";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (IsOnline)
                Response.Redirect(UrlHelper.UrlReturn);
        }
    }
}