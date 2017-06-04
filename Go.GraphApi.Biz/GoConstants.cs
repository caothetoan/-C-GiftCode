using System;
using System.Configuration;

namespace Go.GraphApi.Biz
{
    public class GoConstants
    {
        

        public static string GoserviceAppId
        {
            get { return ConfigurationManager.AppSettings["GoserviceAppId"]; }
        }

        public static string GoserviceKey
        {
            get { return ConfigurationManager.AppSettings["GoserviceKey"]; }
        }

        public static string UrlTopUpByCardDirect(string accesstoken)
        {
            
                return string.Format("{0}billing/UseCardAndTopup?access_token={1}", GoserverAuthen, accesstoken);
            
        }

        public static string GoserverAuthen
        {
            get { return ConfigurationManager.AppSettings["GoserverAuthen"]; }
        }

        public static string MyGoserverBilling
        {
            get { return ConfigurationManager.AppSettings["MyGoserverBilling"]; }
        }

        public static string GoServerLogin
        {
            get { return GoserverAuthen + "oauth/"; }
        }

        public static string ClientId
        {
            get { return ConfigurationManager.AppSettings["ClientId"]; } //GoClientKey
        }

        public static string SecretKey
        {
            get { return ConfigurationManager.AppSettings["Client_secret"]; } //GoClientSecretkey
        }

        public static string redirect_uri
        {
            get { return ConfigurationManager.AppSettings["redirect_uri"]; }
        }

        public static string get_balance_url(string token)
        {
           
                return string.Format("{0}billing/GetBalance/?access_token={1}", GoserverAuthen, token);
            
        }

        public static string get_userinfo(string token)
        {
            return string.Format("{0}me/?access_token={1}", GoserverAuthen, token);
        }

        public static string get_buyitem_url(string token)
        {
            
                return string.Format("{0}billing/buyitems?access_token={1}", GoserverAuthen, token);
            
        }

        public static string get_InputMoneyBetting_url(string token)
        {
            return string.Format("{0}billing/InputMoneyBetting?access_token={1}", GoserverAuthen, token);
        }

        public static string GoUrlSalt
        {
            get
            {
                
                    return String.Format("{0}authentication/salt?client_id={1}&client_secret={2}", GoserverAuthen, ClientId, SecretKey);
                
            }
        }

        public static string GoUrlLogin
        {
            get
            {
                
                    return String.Format("{0}authentication/login?client_id={1}&client_secret={2}", GoserverAuthen, ClientId, SecretKey);
                
            }
        }

        public static string GoUrlFastPlay
        {
            get
            {
                
                    return String.Format("{0}authentication/fastlogin?client_id={1}&client_secret={2}", GoserverAuthen, ClientId, SecretKey);
               
            }
        }

        public static string GoUrlLoginOverFaceBook
        {
            get
            {
                
                    return String.Format("{0}authentication/loginfacebook?client_id={1}&client_secret={2}", GoserverAuthen, ClientId, SecretKey);
                
            }
        }

        public static string GoUrlUpdatePassWord(string accesstoken)
        {
            return String.Format("{0}user/updatepassword?access_token={1}", GoserverAuthen, accesstoken);
        }

        public static string UrlGetAccessToken(string url, string code)
        {
            return String.Format("{0}access_token?client_id={1}&code={2}&client_secret={3}&redirect_uri={4}", GoServerLogin, ClientId, code, SecretKey, url);
        }

        public static string GoUrlLogout(string accountid, string urlReturn)
        {
            return string.Format("{0}request?action=logout&accountid={1}&ur={2}", GoServerLogin, accountid, urlReturn);
        }

        public static string GoUrlRegister()
        {
            return string.Format("{0}authentication/createandauthenticate?client_id={1}&client_secret={2}", GoserverAuthen, ClientId, SecretKey);
        }

        public static string UpdateInfoUrl(string accesstoken)
        {
            return string.Format("{0}user/update/?access_token={1}", GoserverAuthen, accesstoken);
        }

        public static string GoUrlLogout(long accountid, string urlreturn)
        {
            return string.Format("{0}oauth/request?action=logout&accountid={1}&ur={2}", GoserverAuthen, accountid, urlreturn);
        }

        public static string GoUrlRegisterRedirect
        {
            get { return ConfigurationManager.AppSettings["go_Register"]; }
        }

        public static string go_input_money_url
        {
            get
            {
                return MyGoserverBilling + "billing/payment/Default.aspx";
            }
        }

        public static string go_confirm_top_url
        {
            get
            {
                return MyGoserverBilling + "billing/payment/ConfirmTopup.aspx";
            }
        }

        public static string go_secure_secret
        {
            get
            {
                return ConfigurationManager.AppSettings["go_secure_secret"];
            }
        }
    }
}
