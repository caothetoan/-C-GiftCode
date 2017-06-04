using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Go.GraphApi.Biz;
using Libs;
using webXBGiftCode.Controllers;


namespace webXBGiftCode.handler
{
    /// <summary>
    /// Summary description for Account
    /// </summary>
    public class Account : IHttpHandler, IRequiresSessionState
    {
        public class ResponseInfo
        {
            public int Code { get; set; }
            public string ResponseMessage { get; set; }
            public object Data { get; set; }
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var act = context.Request.Params["act"];
            switch (act)
            {
                case "Login": //Lấy thông tin người dung
                    //Hàm
                    Login(context);

                    break;
            }
        }
        /// <summary>
        /// Write Json to client
        /// </summary>
        /// <param name="context"></param>
        /// <param name="obj"></param>
        public void RenderMessJson(HttpContext context, object obj)
        {
            var oSerializer = new JavaScriptSerializer();
            string strJsonMessage = oSerializer.Serialize(obj);
            context.Response.Write(strJsonMessage);
        }

        private void Logger(string functionName, string s)
        {
            Libs.NLogLogger.LogInfo(string.Format("[{0}] {1}", functionName, s));
        }
        private bool IsDebugMode
        {
            get
            {
                var cfg = ConfigurationManager.AppSettings["DebugMode"];
                bool isdebug = false;
                bool.TryParse(cfg, out isdebug);
                return isdebug;
            }
        }
        private string AccessToken = "AccessToken";
        private string AccountName = "AccountName";
        private string AccountId = "AccountId";

        public void Register(HttpContext context)
        {
            var objReturn = new ResponseInfo();
            var UserName = context.Request.Params["UserName"];
            var UserPassword = context.Request.Params["Password"];

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserPassword))
            {
                objReturn.Code = -11;
                objReturn.ResponseMessage = "Dữ liệu nhập vào chưa chính xác";
                RenderMessJson(context, objReturn);
                return;

            } var username = string.Empty;
            long accountId;
            string accessToken, publicName, accountName;

            try
            {
                var graph = new GraphService();

                var message = string.Empty;

                var loginResult = graph.FastRegister(UserName.ToLower(), Encrypt.MD5(UserPassword), out message);

                var Db = new DbQuaTangGiftCode();
                var _ContinuousLogin = 0;
                var _CountLoginInDay = 0;
                var _IsFirst = "";
                var _ResponseStatus = 0;


                accountId = loginResult.AccountId;
                accountName = loginResult.AccountName;

                accessToken = loginResult.AccessToken;

                publicName = loginResult.PublicName;

                objReturn.Code = (int)accountId;
                objReturn.ResponseMessage = message;

                if (loginResult.IsAuthen)
                {                    
                    //"Đăng nhập thành công";

                    objReturn.Data = accountName;

                    // write session
                    System.Web.HttpContext.Current.Session.Add("AccountId", accountId);
                    System.Web.HttpContext.Current.Session.Add("AccountName", accountName);
                    System.Web.HttpContext.Current.Session.Add("AccessToken", accessToken);

                    Logger("Account Login IsAuthen true write session succeed", message);
                    try
                    {
                        Db.SP_Accounts_SetLastLoginTime((int)accountId, accountName, DBCommon.ClientIP,
                  ref  _ContinuousLogin, ref  _CountLoginInDay, ref _IsFirst, ref  _ResponseStatus);
                    }
                    catch (Exception exSetLastLoginTime)
                    {
                        Logger("Account Login IsAuthen true SP_Accounts_SetLastLoginTime Exception", exSetLastLoginTime.Message);

                    }
                   
                }
                else
                {

                    Logger("Account Login SP_Accounts_SetLastLoginTime IsAuthen false", message);

                }
                RenderMessJson(context, objReturn);
            }
            catch (Exception ex)
            {                
                Logger("Account Login Exception", "Exception login>>" + ex.Message);

                RenderMessJson(context, new ResponseInfo
                {
                    Code = -1,
                    Data = ex.Message,
                    ResponseMessage = "Có lỗi xảy ra. Đăng nhập không thành công"
                });
            }
            
        }

        public void Login(HttpContext context)
        {
            var objReturn = new ResponseInfo();
            var UserName = context.Request.Params["UserName"];
            var UserPassword = context.Request.Params["Password"];

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserPassword))
            {
                objReturn.Code = -11;
                objReturn.ResponseMessage = "Dữ liệu nhập vào chưa chính xác";
                RenderMessJson(context, objReturn);
                return;

            }


            var username = string.Empty;
            long accountId;
            string accessToken, publicName, accountName;

            try
            {
                var graph = new GraphService();

                var message = string.Empty;

                var loginResult = graph.Authenticate(UserName.ToLower(), Encrypt.MD5(UserPassword), out message);

                var Db = new DbQuaTangGiftCode();
                var _ContinuousLogin = 0;
                var _CountLoginInDay = 0;
                var _IsFirst = "";
                var _ResponseStatus = 0;


                accountId = loginResult.AccountId;
                accountName = loginResult.AccountName;

                accessToken = loginResult.AccessToken;

                publicName = loginResult.PublicName;

                objReturn.Code = (int)accountId;
                objReturn.ResponseMessage = message;

                if (loginResult.IsAuthen)
                {                    
                    //"Đăng nhập thành công";

                    objReturn.Data = accountName;

                    // write session
                    System.Web.HttpContext.Current.Session.Add(AccountId, accountId);
                    System.Web.HttpContext.Current.Session.Add(AccountName, accountName);
                    System.Web.HttpContext.Current.Session.Add(AccessToken, accessToken);

                    Logger("Account Login IsAuthen true write session succeed", message);
                    try
                    {
                        Db.SP_Accounts_SetLastLoginTime((int)accountId, accountName, DBCommon.ClientIP,
                  ref  _ContinuousLogin, ref  _CountLoginInDay, ref _IsFirst, ref  _ResponseStatus);
                    }
                    catch (Exception exSetLastLoginTime)
                    {
                        Logger("Account Login IsAuthen true SP_Accounts_SetLastLoginTime Exception", exSetLastLoginTime.Message);

                    }                   
                }
                else
                {
                    Logger("Account Login SP_Accounts_SetLastLoginTime IsAuthen false", message);
                }

                RenderMessJson(context, objReturn);
            }
            catch (Exception ex)
            {               
                Logger("Account Login Exception", "Exception login>>" + ex.Message);

                RenderMessJson(context, new ResponseInfo
                {
                    Code = -1,
                    Data = ex.Message,
                    ResponseMessage = "Có lỗi xảy ra. Đăng nhập không thành công"
                });
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}