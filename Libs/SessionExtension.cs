using System;
using System.Web;
using System.Web.SessionState;
using VTCeBank.SSO.Utils;

namespace Libs
{
    public static class SessionExtension
    {
        public const string ACCOUNTID = "AccountId";
        public const string ACCOUNTNAME = "AccountName";
        
        private const string CAPTCHA = "CheckNumber";
      /*  private const string ACCOUNTID = "UserID";
        private const string ACCOUNTNAME = "UserName";*/
        public const string PUBLICNAME = "PublicName";
        public const string GOACCESSTOKEN = "GoAccessToken";
        private const string VTCACCESSTOKEN = "VtcAccessToken";

        private const string LASTACTION = "&LastAction&";

        private const string SECURITYLEVEL = "&SecurityLevel&";

        private const string PASSWORD = "PASSWORD";

        public static bool IsOnline(this HttpSessionState state)
        {

            if (state != null && (state[ACCOUNTID] != null || state[ACCOUNTNAME] != null))
                return true;
            return false;

        }

        
        public static void SetGoAccessToken(this HttpSessionState state, string accessToken)
        {
            state[GOACCESSTOKEN] = accessToken;
        }

        public static string GetGoAccessToken(this HttpSessionState state)
        {
            if (state[GOACCESSTOKEN] != null)
                return state[GOACCESSTOKEN].ToString();
            return string.Empty;
        }

        public static void SetVtcAccessToken(this HttpSessionState state, string vtcAccessToken)
        {
            state[VTCACCESSTOKEN] = vtcAccessToken;
        }

        public static string GetVtcAccessToken(this HttpSessionState state)
        {
            if (state != null && state[VTCACCESSTOKEN] != null)
                return state[VTCACCESSTOKEN].ToString();
            return string.Empty;
        }

        public static void SetCaptcha(this HttpSessionState state, string accessToken)
        {
            state[CAPTCHA] = accessToken;
        }

        public static string GetCaptcha(this HttpSessionState state)
        {
            if (state[CAPTCHA] != null)
                return state[CAPTCHA].ToString();
            return null;
        }
        
      
        [Obsolete]
        public static void SetOnline(this HttpSessionState state)
        {
            state["&IsOnline&"] = true;
        }

        public static void SetOffline(this HttpSessionState state)
        {

            state[ACCOUNTID] = null;
            state[ACCOUNTNAME] = null;
            state[PUBLICNAME] = null;
        }

       
        public static long GetAccountId(this HttpSessionState state)
        {
            if (state != null && state[ACCOUNTID] != null)
                return long.Parse(state[ACCOUNTID].ToString());
            return -1;

        }
        public static string GetPublicName(this HttpSessionState state)
        {
            if (state != null && state[PUBLICNAME] != null)
            {
                if(string.IsNullOrEmpty(state[PUBLICNAME].ToString()))
                {
                    //state[PUBLICNAME] = Provider.GoAccountService.GetPublicName(GetAccountId(state));
                }
                return state[PUBLICNAME].ToString();
            }
                
            return null;

        }
        public static long GetGoAccountId(this HttpSessionState state)
        {
            if (state != null && state[ACCOUNTID] != null)
                return long.Parse(state[ACCOUNTID].ToString());
            return -18;
        }

        public static string GetGoAccountName(this HttpSessionState state)
        {
            if (state != null && state[ACCOUNTNAME] != null)
                return state[ACCOUNTNAME].ToString();
            return null;
        }

        public static void UpdateLastAction(this HttpSessionState state)
        {
            if (state != null && state[LASTACTION] != null)
                state[LASTACTION] = DateTime.Now.Ticks;
        }
        public static long GetLastAction(this HttpSessionState state)
        {
            if (state != null && state[LASTACTION] != null)
                return (long) state[LASTACTION];
            return DateTime.Now.Ticks;
        }
        public static bool IsAllowAction(this HttpSessionState state)
        {
            //return true;
            if (state != null && state[LASTACTION] != null)
            {
                var lastAction = (long)state[LASTACTION];
                var ticks = (DateTime.Now.Ticks - lastAction);

                var seconds = ticks/10000000;

                if (seconds > 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

            
        }

        public static string GetGoPassword(this HttpSessionState state)
        {
            if (state != null && state[PASSWORD] != null)
                return state[PASSWORD].ToString();
            return null;
        }

        public static void SetGoPassword(this HttpSessionState state, string password)
        {
            state[PASSWORD] = password;
        }

    }
}