using System;
using System.Web;
using System.Web.UI;
using Go.GraphApi.Biz;
using Libs;

namespace webXBGiftCode
{
    public partial class Register : Page
    {
        protected int AccountId
        {
            get
            {
                
                return (int)Session.GetAccountId();
            }
        }
        protected bool IsOnline
        {
            get { return AccountId > 0; }
        }

        protected object UrlLogin
        {
            get { return "Login.aspx"; }
            
        }

        protected void lbtnRegister_OnClick(object sender, EventArgs e)
        {
            var graph = new GraphService();

            string message;
            var username = txtUserName.Text;
            var userPassword = txtPassword.Text;
            var userPasswordAgain = txtPasswordAgain.Text;
            //lbMsg.Text = "";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userPassword))
            {
                // chưa nhập tên hoặc mật khẩu
                lbMsg.Text = "Bạn chưa nhập tên đăng nhập hoặc mật khẩu";
                return;
            }
            if (!userPassword.Equals(userPasswordAgain))
            {
                // chưa nhập tên hoặc mật khẩu
                lbMsg.Text = "Mật khẩu không khớp";
                return;
            }
            var loginResult = graph.FastRegister(username.ToLower(), Encrypt.MD5(userPassword), out message);
            if (string.IsNullOrEmpty(message))
            {
                message = "Có lỗi xảy ra từ graph api";
            }
            lbMsg.Text = message;

            long accountId = loginResult.AccountId;
            string accountName = loginResult.AccountName;

            string accessToken = loginResult.AccessToken;

            if (loginResult.IsAuthen)
            {
                // "Đăng kí thành công";
                // write session

                HttpContext.Current.Session.Add(SessionExtension.ACCOUNTID, accountId);
                HttpContext.Current.Session.Add(SessionExtension.ACCOUNTNAME, accountName);
                HttpContext.Current.Session.Add(SessionExtension.GOACCESSTOKEN, accessToken);

                Log.WriteLog(String.Format("Account Register IsAuthen true write session succeed accountId = {0}, accountName = {1}", accountId, accountName), message);
               
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
                    Log.WriteLog("Account Register IsAuthen true SP_Accounts_SetLastLoginTime Exception", exSetLastLoginTime.Message);
                }

                Response.Redirect(UrlReturn);
            }
            else
            {
                Log.WriteLog("Account Register IsAuthen false", message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

       /* protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (IsOnline)
                Response.Redirect(UrlReturn);
        }*/

        protected string UrlReturn
        {
            get { return Request.Params["ur"]; }

        }
    }
}