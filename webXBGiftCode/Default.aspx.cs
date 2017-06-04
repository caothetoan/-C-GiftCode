using System;
using System.IO;
using System.Web;
using System.Web.UI;
using Go.GraphApi.Biz;
using Libs;

namespace webXBGiftCode
{
    public partial class Default : Page
    {
        protected string AccountName
        {
            get
            {
                return Session.GetGoAccountName();
                //return Session["AccountName"].ToString();
            }
        }
        protected int AccountId
        {
            get
            {
                /*  if (Session["AccountId"] != null)
                      return Int32.Parse(Session["AccountId"].ToString());
               
                 Log.WriteLog("Session AccountId is null ");
                 return -1;*/

                var accountId = (int)Session.GetAccountId();

                return accountId;

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            Get_Accounts_GetInfo();
        }

        readonly DbQuaTangGiftCode _db = new DbQuaTangGiftCode();

        public void Get_Accounts_GetInfo()
        {

            try
            {
                if (AccountId > 0)
                {
                    
                    var _Username = "";

                    var _QuestionPending = 0;
                    var _ResponseStatus = 0;

                    var _Date = new DateTime();
                    _db.SP_Accounts_GetInfo(AccountId, ref  _Username, ref  CurrentStep, ref  _QuestionPending, ref  _ResponseStatus, ref  _Date, ref  StatusAnswer);


                }
            }
            catch (Exception ex)
            {
                Libs.NLogLogger.LogInfo("Exception GetAccountInfor>>" + ex.Message);

            }

        }
        protected int StatusAnswer = 0, CurrentStep = 0;

        protected bool IsOnline
        {
            get { return AccountId > 0; }
        }

      
        protected void lbtnLogin_OnClick(object sender, EventArgs e)
        {
            var graph = new GraphService();

            string message;
            //lbMsg.Text = "";

            /* var username = Request.Params["txtUserName"];
           var userPassword = Request.Params["txtPassword"];
*/
            var username = txtUserName.Text;
            var userPassword = txtPassword.Text;

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(userPassword))
            {
                // chưa nhập tên hoặc mật khẩu
                lbMsg.Text = "Bạn chưa nhập tên đăng nhập hoặc mật khẩu";
                return;
            }
            var loginResult = graph.Authenticate(username.ToLower(), Encrypt.MD5(userPassword), out message);

            long accountId = loginResult.AccountId;
            string accountName = loginResult.AccountName;

            string accessToken = loginResult.AccessToken;
            lbMsg.Text = message;

            if (loginResult.IsAuthen)
            {
                // "Đăng nhập thành công";
                // write session
                if (accountId > 0)
                    HttpContext.Current.Session.Add(SessionExtension.ACCOUNTID, accountId);
                if (!string.IsNullOrEmpty(accountName))
                    HttpContext.Current.Session.Add(SessionExtension.ACCOUNTNAME, accountName);
                if (!string.IsNullOrEmpty(accessToken))
                    HttpContext.Current.Session.Add(SessionExtension.GOACCESSTOKEN, accessToken);

                Log.WriteLog("Account Login IsAuthen true write session succeed", message);

                Get_Accounts_GetInfo();
                try
                {
                    var db = new DbQuaTangGiftCode();
                    var continuousLogin = 0;
                    var countLoginInDay = 0;
                    var isFirst = "";
                    var responseStatus = 0;

                    db.SP_Accounts_SetLastLoginTime((int)accountId, accountName, DBCommon.ClientIP,
                  ref  continuousLogin, ref  countLoginInDay, ref isFirst, ref  responseStatus);
                }
                catch (Exception exSetLastLoginTime)
                {
                    Log.WriteLog("Account Login IsAuthen true SP_Accounts_SetLastLoginTime Exception", exSetLastLoginTime.Message);
                }

            }
            else
            {
                Log.WriteLog("Account Login SP_Accounts_SetLastLoginTime IsAuthen false", message);
                //lbMsg.Text = "Sai tên đăng nhập hoặc mật khẩu";
            }
        }

        protected void lbtnRegister_OnClick(object sender, EventArgs e)
        {
            var graph = new GraphService();

            string message;
            /* var username = Request.Params["txtUserName"];
             var userPassword = Request.Params["txtPassword"];
 */
            var username = txtUserName.Text;
            var userPassword = txtPassword.Text;

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(userPassword))
            {
                // chưa nhập tên hoặc mật khẩu
                lbMsg.Text = "Bạn chưa nhập tên đăng nhập hoặc mật khẩu";
                return;
            }
            var loginResult = graph.FastRegister(username.ToLower(), Encrypt.MD5(userPassword), out message);

            long accountId = loginResult.AccountId;
            string accountName = loginResult.AccountName;

            string accessToken = loginResult.AccessToken;

            if (loginResult.IsAuthen)
            {
                // "Đăng kí thành công";
                // write session

                HttpContext.Current.Session.Add("AccountId", accountId);
                HttpContext.Current.Session.Add("AccountName", accountName);
                HttpContext.Current.Session.Add("AccessToken", accessToken);

                Log.WriteLog("Account Login IsAuthen true write session succeed", message);
             
                var continuousLogin = 0;
                var countLoginInDay = 0;
                var isFirst = "";
                var responseStatus = 0;
                try
                {
                    _db.SP_Accounts_SetLastLoginTime((int)accountId, accountName, DBCommon.ClientIP,
              ref  continuousLogin, ref  countLoginInDay, ref isFirst, ref  responseStatus);
                }
                catch (Exception exSetLastLoginTime)
                {
                    Log.WriteLog("Account Login IsAuthen true SP_Accounts_SetLastLoginTime Exception", exSetLastLoginTime.Message);
                }
            }
            else
            {
                Log.WriteLog("Account Login IsAuthen false", message);
            }
        }

        protected void lbtnLogout_OnClick(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            if (AccountId > 0)
                HttpContext.Current.Response.Redirect(UrlLogout(AccountId));
        }
        protected string UrlAbsoluteUri
        {
            get { return HttpContext.Current.Request.Url.AbsoluteUri; }
        }

        protected string SurveyName
        {
            get
            {
                //_db.SP_SurveyQuestion_Get();
                return "Tên bạn thực sự có nghĩa gì";
            }
           
        }

        private string UrlLogout(int accountId)
        {
            //return string.Format("Logout.aspx?ur={0}", HttpUtility.UrlEncode(UrlHelper.UrlAbsoluteUri));

            return
                GoConstants.GoUrlLogout(accountId, UrlHelper.UrlAbsoluteUri);

        }
    }
}