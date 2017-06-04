using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Libs
{
    public class DbQuaTangGiftCode
    {
        private readonly DBHelper _db;
        public DbQuaTangGiftCode()
        {
            _db = new DBHelper(DBConfig.SQLConn);
        }

        public List<GiftCodeInfo> SP_GetGiftCodeInfo(int _AccountId)
        {
            try
            {
                var oCommand = new SqlCommand("SP_GiftCodeAccount_Get") { CommandType = CommandType.StoredProcedure };
                oCommand.Parameters.AddWithValue("@_AccountId", _AccountId);
                var lst = _db.GetList<GiftCodeInfo>(oCommand);
                return lst;
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        /// <summary>
        /// Insert dữ liệu login
        /// </summary>
        /// <param name="_AccountID"></param>
        /// <param name="_Username"></param>
        /// <param name="_ClientIP"></param>
        /// <param name="_ContinuousLogin"></param>
        /// <param name="_CountLoginInDay"></param>
        /// <param name="_IsFirst"></param>
        public void SP_Accounts_SetLastLoginTime(int _AccountID, string _Username, string _ClientIP,
            ref int _ContinuousLogin, ref int _CountLoginInDay, ref string _IsFirst, ref int _ResponseStatus)
        {
            try
            {


                var oCommand = new SqlCommand("SP_Accounts_SetLastLoginTime") { CommandType = CommandType.StoredProcedure };
                oCommand.Parameters.AddWithValue("@_AccountID", _AccountID);
                oCommand.Parameters.AddWithValue("@_Username", _Username);
                oCommand.Parameters.AddWithValue("@_ClientIP", _ClientIP);

                var p_ContinuousLogin = new SqlParameter("@_ContinuousLogin", SqlDbType.Int) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_ContinuousLogin);

                var p_CountLoginInDay = new SqlParameter("@_CountLoginInDay", SqlDbType.Int) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_CountLoginInDay);

                var p_IsFirst = new SqlParameter("@_IsFirst", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_IsFirst);

                var p_ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_ResponseStatus);

                _db.ExecuteNonQuery(oCommand);
                _ContinuousLogin = Convert.ToInt32(p_ContinuousLogin.Value);
                _CountLoginInDay = Convert.ToInt32(p_CountLoginInDay.Value);
                _IsFirst = p_IsFirst.Value.ToString();
                _ResponseStatus = Convert.ToInt32(p_ResponseStatus.Value);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Lấy thông tin đăng nhập của user
        /// </summary>
        /// <returns></returns>
        /// @_AccountID INT
        //,@_Username NVARCHAR(30) OUTPUT
        //,@_CurrentPoint INT OUTPUT --Step da hoan thanh	=0 la chua tham gia khao sat, >0 la cac buoc tiep theo tu dinh nghia
        //,@_ImageUrl NVARCHAR(1000) OUTPUT -- da tham gia khao sat chua, neu tham gia roi thi url != null
        //,@_Date DATE OUTPUT -- Ngay dang nhap lan cuoi
        public void SP_Accounts_GetInfo(int _AccountID, ref string _Username, ref int _CurrentStep, ref int _QuestionPending, ref int _ResponseStatus, ref DateTime _Date, ref int _StatusAnswer)
        {
            try
            {
                var oCommand = new SqlCommand("SP_Accounts_GetInfo") { CommandType = CommandType.StoredProcedure };
                oCommand.Parameters.AddWithValue("@_AccountID", _AccountID);

                var p_Username = new SqlParameter("@_Username", SqlDbType.NVarChar, 30) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_Username);

                var p_CurrentStep = new SqlParameter("@_CurrentStep", SqlDbType.Int) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_CurrentStep);

                var p_QuestionPending = new SqlParameter("@_QuestionPending", SqlDbType.Int) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_QuestionPending);

                var p_StatusAnswer = new SqlParameter("@_StatusAnswer", SqlDbType.Int) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_StatusAnswer);


                var p_Date = new SqlParameter("@_Date", SqlDbType.DateTime) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_Date);

                var p_ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_ResponseStatus);

                _db.ExecuteNonQuery(oCommand);

                _Username = p_Username.Value == null ? "" : p_Username.Value.ToString();

                if (p_CurrentStep.Value != null && int.TryParse(p_CurrentStep.Value.ToString(), out _CurrentStep))
                {

                }
                if (p_QuestionPending.Value != null && int.TryParse(p_QuestionPending.Value.ToString(), out _QuestionPending))
                {

                }

                if (p_ResponseStatus.Value != null && int.TryParse(p_ResponseStatus.Value.ToString(), out _ResponseStatus))
                {

                }
                if (p_StatusAnswer.Value != null && int.TryParse(p_StatusAnswer.Value.ToString(), out _StatusAnswer))
                {

                }
                if (p_Date.Value != null && DateTime.TryParse(p_Date.Value.ToString(), out _Date))
                {

                }

            }
            catch (Exception myException)
            {
                //throw (new Exception(myException.Message));
                Libs.NLogLogger.LogInfo("Exception SP_Accounts_GetInfo>>" + myException.Message);
            }
        }
        /// <summary>
        /// Set vị trí hiện tại của người dùng
        /// </summary>
        /// <param name="_AccountID"></param>
        /// <param name="_CurrentPoint"></param>
        /// <param name="_ResponseStatus"></param>
        public void SP_Update_CurrentStep(int _AccountID, int _CurrentPoint, ref int _ResponseStatus)
        {
            try
            {
                var oCommand = new SqlCommand("SP_Update_CurrentStep") { CommandType = CommandType.StoredProcedure };
                oCommand.Parameters.AddWithValue("@_AccountID", _AccountID);
                oCommand.Parameters.AddWithValue("@_CurrentStep", _CurrentPoint);

                var p_ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_ResponseStatus);

                _db.ExecuteNonQuery(oCommand);
                _ResponseStatus = Convert.ToInt32(p_ResponseStatus.Value);
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }
        /// <summary>
        /// Lấy câu hỏi
        /// </summary>
        /// <param name="_AccountID"></param>
        /// <param name="_Username"></param>
        /// <param name="_QuestionId"></param>
        /// <param name="_QuestionContent"></param>
        /// <param name="_UrlImage"></param>
        /// <param name="_ResponseStatus"></param>
        public void SP_SurveyQuestion_Get(int _AccountID, ref string _Username,
            ref int _QuestionId, ref string _QuestionContent, ref string _UrlImage, ref int _ResponseStatus)
        {
            try
            {
                var oCommand = new SqlCommand("SP_SurveyQuestion_Get") { CommandType = CommandType.StoredProcedure };
                oCommand.Parameters.AddWithValue("@_AccountID", _AccountID);

                var p_Username = new SqlParameter("@_Username", SqlDbType.NVarChar, 30) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_Username);

                var p_QuestionId = new SqlParameter("@_QuestionId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_QuestionId);

                var p_QuestionContent = new SqlParameter("@_QuestionContent", SqlDbType.NVarChar, 1000) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_QuestionContent);

                var p_UrlImage = new SqlParameter("@_UrlImage", SqlDbType.NVarChar, 1000) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_UrlImage);

                var p_ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_ResponseStatus);

                _db.ExecuteNonQuery(oCommand);


                if (p_ResponseStatus.Value != null && int.TryParse(p_ResponseStatus.Value.ToString(), out _ResponseStatus))
                {

                }

                if (_ResponseStatus >= 0 || _ResponseStatus == -46)
                {
                    if (p_Username.Value == null)
                        _Username = "";
                    else
                        _Username = p_Username.Value.ToString();

                    _QuestionContent = p_QuestionContent.Value.ToString();
                    _UrlImage = p_UrlImage.Value.ToString();

                    if (p_QuestionId.Value != null && int.TryParse(p_QuestionId.Value.ToString(), out _QuestionId))
                    {

                    }

                }
                else
                {
                    _Username = p_Username.Value.ToString();
                    _QuestionContent = "";
                    _UrlImage = "";
                    _QuestionId = 0;
                }
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }
        /// <summary>
        /// Hoàn thiện upload ảnh cho câu hỏi
        /// </summary>
        /// <param name="_AccountID"></param>
        /// <param name="_Username"></param>
        /// <param name="_QuestionID"></param>
        /// <param name="_UrlImage"></param>
        /// <param name="_GiftCode"></param>
        public void SP_SurveyAccounts_Finish(int _AccountID, string _Username, int _QuestionID, string _UrlImage, ref int _ResponseStatus)
        {
            try
            {
                var oCommand = new SqlCommand("SP_SurveyAccounts_Finish") { CommandType = CommandType.StoredProcedure };
                oCommand.Parameters.AddWithValue("@_AccountID", _AccountID);
                oCommand.Parameters.AddWithValue("@_Username", _Username);
                oCommand.Parameters.AddWithValue("@_QuestionID", _QuestionID);
                oCommand.Parameters.AddWithValue("@_UrlImage", _UrlImage);

                var p_ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(p_ResponseStatus);

                _db.ExecuteNonQuery(oCommand);
                _ResponseStatus = Convert.ToInt32(p_ResponseStatus.Value);
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }
        /// <summary>
        /// lấy survey account
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="userName"></param>
        /// <param name="statusAnswer"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<SurveyAccountInfo> GetList(int accountId, string userName, int statusAnswer, DateTime fromDate, DateTime toDate, int pageNumber, int pageSize, out int totalRecord)
        {
            try
            {
                //var db = new DBHelper(DBConfig.SQLConn);
                var oCommand = new SqlCommand("[SP_Cms_SurveyAccount_GetList]")
                {
                    CommandType =
                        CommandType.StoredProcedure
                };
                oCommand.Parameters.Add(new SqlParameter("@_AccountId", SqlDbType.Int) { Value = accountId });
                oCommand.Parameters.Add(new SqlParameter("@_UserName", SqlDbType.VarChar) { Value = userName });
                oCommand.Parameters.Add(new SqlParameter("@_StatusAnser", SqlDbType.Int) { Value = statusAnswer });
                oCommand.Parameters.Add(new SqlParameter("@_FromDate", SqlDbType.DateTime) { Value = fromDate });
                oCommand.Parameters.Add(new SqlParameter("@_ToDate", SqlDbType.DateTime) { Value = toDate });
                oCommand.Parameters.Add(new SqlParameter("@_PageNumber", SqlDbType.Int) { Value = pageNumber });
                oCommand.Parameters.Add(new SqlParameter("@_PageSize", SqlDbType.Int) { Value = pageSize });
                var total = new SqlParameter("@_TotalRecord", SqlDbType.Int) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(total);
                var tb = _db.GetList<SurveyAccountInfo>(oCommand);
                int.TryParse(total.Value.ToString(), out totalRecord);
                return tb;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            catch
            {
                totalRecord = 0;
                return null;
            }
        }

        #region Class

        #endregion Class
    }
    public class GiftCodeInfo
    {
        public long RowNumber { get; set; }
        public int AccountID { get; set; }
        public string Username { get; set; }
        public int QuestionID { get; set; }
        public string Content { get; set; }
        public int GiftCodeID { get; set; }
        public string GiftCode { get; set; }
        public string Status { get; set; }

        public string CreatedDatetime { get; set; }
    }

    public class Surveys
    {

        public int SurveyID { get; set; }
        public string SurveyName { get; set; }
        public string CreatedUser { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string Image { get; set; }

    }
}
