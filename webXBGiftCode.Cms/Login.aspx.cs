using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Go.App.Utils;
using Libs;
using Minxtu.Biz.Utils;
using webXBGiftCode.Cms.Common;

namespace webXBGiftCode.Cms
{
    public partial class Login : System.Web.UI.Page
    {


        protected void Authenticate(object sender, AuthenticateEventArgs e)
        {
            try
            {
                //var txtUserName = (RadTextBox)CMSAdminLogin.FindControl("UserName");
                //var txtPassword = (RadTextBox)CMSAdminLogin.FindControl("Password");
                var txtUserName = (TextBox)CMSAdminLogin.FindControl("UserName");
                var txtPassword = (TextBox)CMSAdminLogin.FindControl("Password");
                var lblError = (Literal)CMSAdminLogin.FindControl("lblError");

                const string mailServer = "@vtc.vn";
                var username = txtUserName.Text.Trim().ToLower();
                var password = txtPassword.Text.Trim();

                if (username.EndsWith(mailServer)) username = username.Replace(mailServer, string.Empty);
                var isAdmin = Setting.AdminList.IndexOf(username + "|", StringComparison.CurrentCultureIgnoreCase) >= 0;

                //
                Pop3client myPop = new Pop3client(username, password, Config.MailServer);

                Exception ex = null;
                bool loginSuccess = myPop.CheckLogin(ref ex);

                if (ex != null)
                {
                    lblError.Text =
                       "Sai tên đăng nhập hoặc mật khẩu. Vui lòng thử lại hoặc liên hệ với quản trị website để được hỗ trợ.";
                }

                /* var pop3Auth = new VtcMailPop3();
                 var successed = (pop3Auth.CheckAuth(username, password) == "OK");*/
                if (isAdmin && loginSuccess)
                {
                    Session[Setting.SESSION_USERID] = 0;
                    Session[Setting.SESSION_USERNAME] = username;
                    Session[Setting.SESSION_USEREMAIL] = username + mailServer;

                    var ur = Request.Params["ur"] ?? Setting.SiteUrl;
                    Response.Redirect(ur);
                }
                else
                {
                    lblError.Text = "Thông tin đăng nhập sai. Hãy thử lại.";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogType.Error, ex.Message);
            }
        }
    }
}