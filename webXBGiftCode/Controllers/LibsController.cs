using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Libs;

namespace webXBGiftCode.Controllers
{
    public class LibsController : ApiController
    {
        protected int AccountId
        {
            get
            {
                return (int)HttpContext.Current.Session.GetAccountId();

                //return HttpContext.Current.Session["AccountId"] != null ? Convert.ToInt32(HttpContext.Current.Session["AccountId"]) : 0;
            }
        }

        protected string AccountName
        {
            get
            {
                return HttpContext.Current.Session.GetGoAccountName();

                //return  = HttpContext.Current.Session["AccountName"] != null ? (HttpContext.Current.Session["AccountName"].ToString()) : "";
            }
        }
        public class GiftCodeInfo
        {

            public long _RowNumber { get; set; }
            public int _AccountID { get; set; }
            public string _Username { get; set; }
            public int _QuestionID { get; set; }
            public string _Content { get; set; }
            public int _GiftCodeID { get; set; }
            public string _GiftCode { get; set; }
            public string _Status { get; set; }

            public string _CreatedDatetime { get; set; }

        }

        public class QuestionInfo
        {
            public int _QuestionId { get; set; }
            public string _QuestionContent { get; set; }
            public string _UrlImage { get; set; }
            public int _ResponseStatus { get; set; }
            public string _Username { get; set; }
        }
        public class AccountInfo
        {
            public string _Username { get; set; }
            public int _QuestionPending { get; set; }
            public int _StatusAnswer { get; set; }
            public int _CurrentStep { get; set; }
            public DateTime _Date { get; set; }
        }


        public class ListGiftCode
        {
            public List<GiftCodeInfo> listCode { get; set; }
        }

        [HttpPost]
        [ActionName("Login")]
        public string Login(string url, string fileName)
        {

            return "";
        }

        [HttpGet]
        [ActionName("GetGifCodeDiary")]
        public ListGiftCode GetGifCodeDiary()
        {
            var listCode = new ListGiftCode();
            var objReturn = new List<GiftCodeInfo>();
            try
            {
                if (AccountId > 0)
                {
                    var db = new DbQuaTangGiftCode();

                    var a = db.SP_GetGiftCodeInfo(AccountId);
                    if (a.Count > 0)
                    {
                        foreach (var giftCodeInfo in a)
                        {
                            var giftcode = new GiftCodeInfo
                                {
                                    _RowNumber = giftCodeInfo.RowNumber,
                                    _AccountID = giftCodeInfo.AccountID,
                                    _Content = giftCodeInfo.Content,
                                    _GiftCode = giftCodeInfo.GiftCode,
                                    _GiftCodeID = giftCodeInfo.GiftCodeID,
                                    _QuestionID = giftCodeInfo.QuestionID,
                                    _Status = giftCodeInfo.Status,
                                    _Username = giftCodeInfo.Username,
                                    _CreatedDatetime = giftCodeInfo.CreatedDatetime
                                };
                            objReturn.Add(giftcode);
                        }
                    }
                }

                listCode.listCode = objReturn;
                return listCode;

            }
            catch (Exception ex)
            {
                Libs.NLogLogger.LogInfo("Exception GetGiftCode>>" + ex.Message);
                return listCode;
            }
        }

        [HttpGet]
        [ActionName("GetAccountInfor")]
        public AccountInfo SP_Accounts_GetInfo()
        {
            var objReturn = new AccountInfo();
            try
            {
                if (AccountId > 0)
                {
                    var db = new DbQuaTangGiftCode();
                    var _Username = "";
                    var _CurrentStep = 0;
                    var _QuestionPending = 0;
                    var _ResponseStatus = 0;
                    int _StatusAnswer = 0;
                    var _Date = new DateTime();
                    db.SP_Accounts_GetInfo(AccountId, ref  _Username, ref  _CurrentStep, ref  _QuestionPending, ref  _ResponseStatus, ref  _Date, ref  _StatusAnswer);

                    objReturn._Username = _Username;
                    objReturn._CurrentStep = _CurrentStep;
                    objReturn._QuestionPending = _QuestionPending;
                    objReturn._Date = _Date;
                    objReturn._StatusAnswer = _StatusAnswer;
                }
            }
            catch (Exception ex)
            {
                Libs.NLogLogger.LogInfo("Exception GetAccountInfor>>" + ex.Message);

            }
            return objReturn;
        }


        [HttpPost]
        [ActionName("GetQuestion")]
        public QuestionInfo GetQuestion()
        {
            var objReturn = new QuestionInfo();
            var _QuestionId = 0; var _QuestionContent = ""; var _UrlImage = ""; var _ResponseStatus = 0;
            try
            {
                if (AccountId > 0)
                {
                    var db = new DbQuaTangGiftCode();
                    var _Username = "";
                    db.SP_SurveyQuestion_Get(AccountId, ref _Username, ref _QuestionId, ref _QuestionContent,
                                             ref _UrlImage, ref _ResponseStatus);
                    objReturn._Username = AccountName;
                    objReturn._QuestionId = _QuestionId;
                    objReturn._QuestionContent = _QuestionContent;
                    objReturn._UrlImage = _UrlImage;
                    objReturn._ResponseStatus = _ResponseStatus;

                }
                else objReturn._ResponseStatus = -1;
            }
            catch (Exception ex)
            {
                Libs.NLogLogger.LogInfo("Exception GetQuestion>>" + ex.Message);
                objReturn._ResponseStatus = -99;
            }

            return objReturn;
        }


        [HttpGet]
        [ActionName("UploadFinish")]
        public int UploadFinish(int _QuestionID, string _UrlImage)
        {
            int _ResponseStatus = -1;
            try
            {


                if (AccountId > 0)
                {
                    var db = new DbQuaTangGiftCode();
                    db.SP_SurveyAccounts_Finish(AccountId, AccountName, _QuestionID, _UrlImage, ref  _ResponseStatus);
                }
            }
            catch (Exception ex)
            {
                Libs.NLogLogger.LogInfo("Exception UploadFinish>>" + ex.Message);
            }
            return _ResponseStatus;
        }


        [HttpGet]
        [ActionName("UpdateCurrentStep")]
        public int SP_Update_CurrentStep(int _CurrentStep)
        {
            int AccountId = HttpContext.Current.Session["AccountId"] != null ? Convert.ToInt32(HttpContext.Current.Session["AccountId"]) : 0;
            var _ResponseStatus = 0;
            try
            {


                if (AccountId > 0)
                {
                    var db = new DbQuaTangGiftCode();

                    db.SP_Update_CurrentStep(AccountId, _CurrentStep, ref  _ResponseStatus);
                }
            }
            catch (Exception ex)
            {
                Libs.NLogLogger.LogInfo("Exception UploadFinish>>" + ex.Message);
            }
            return _ResponseStatus;
        }

    }
}