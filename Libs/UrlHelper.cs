using System.Configuration;
using System.Web;

namespace Libs
{
    public static class UrlHelper
    {
        public static string UrlAbsoluteUri
        {
            get { return HttpContext.Current.Request.Url.AbsoluteUri; }
        }
      
        public static string UrlRegister
        {
            get { return string.Format("Register.aspx?ur={0}", HttpUtility.UrlEncode(UrlAbsoluteUri)); }

        }
        public static string UrlLogout
        {
            get { return string.Format("Logout.aspx?ur={0}", HttpUtility.UrlEncode(UrlAbsoluteUri)); }

        }
        public static string UrlLogin
        {
            get { return string.Format("Login.aspx?ur={0}", HttpUtility.UrlEncode(UrlAbsoluteUri)); }

        }
        public static string GetUrlLogin
        {
            get { return string.Format("Login.aspx?ur={0}", HttpUtility.UrlEncode(UrlReturn)); }

        }
        public static string UrlReturn
        {
            get { return HttpContext.Current.Request.Params["ur"]; }

        }

        public static string UrlFanpage
        {
            get
            {
                return
                    ConfigurationManager.AppSettings["UrlFanpage"] ?? "http://facebook.com/duoihinhbatchu/";
            }
        }
        public static string UrlForgotPassword
        {
            get
            {
                return
                    string.Format(
                        "https://go.vn/oauth/accounts/ForgotPassword.aspx?sid=330002&ur={0}&m=1&continue=https%3a%2f%2fmy.go.vn%2fbilling%2fsso%2faccountlogin%2fdefault.aspx&cpid=0&campaign=",
                        HttpUtility.UrlEncode(UrlAbsoluteUri));
            }
        }
    }
}
