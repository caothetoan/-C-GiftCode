using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Libs
{
    
    
    public class SurveyAccountInfo
    {
        public long RowNumber { get; set; }
        public int AccountID { get; set; }
        public string Username { get; set; }
        public int QuestionID { get; set; }
        public string Content { get; set; }
        public int StatusAnswer { get; set; }
        public string GiftCode { get; set; }
        public int GiftCodeID { get; set; }
        public string UrlImage { get; set; }
        public DateTime CreatedDatetime { get; set; }
    }
    public class DBSurveyAccount
    {
        private readonly DBHelper _db;

        public static string ConnectionString
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["SQLConn"].ConnectionString;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public DBSurveyAccount()
        {
            _db = new DBHelper(DBSurveyAccount.ConnectionString);
            //_db.ConnectionToDB = new SqlConnection(DBSurveyAccount.ConnectionString);
        }

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
                oCommand.Parameters.Add(new SqlParameter("@_AccountId", SqlDbType.Int) {Value = accountId});
                oCommand.Parameters.Add(new SqlParameter("@_UserName", SqlDbType.VarChar) {Value = userName});
                oCommand.Parameters.Add(new SqlParameter("@_StatusAnser", SqlDbType.Int) {Value = statusAnswer});
                oCommand.Parameters.Add(new SqlParameter("@_FromDate", SqlDbType.DateTime) {Value = fromDate});
                oCommand.Parameters.Add(new SqlParameter("@_ToDate", SqlDbType.DateTime) {Value = toDate});
                oCommand.Parameters.Add(new SqlParameter("@_PageNumber", SqlDbType.Int) {Value = pageNumber});
                oCommand.Parameters.Add(new SqlParameter("@_PageSize", SqlDbType.Int) {Value = pageSize});
                var total = new SqlParameter("@_TotalRecord", SqlDbType.Int) {Direction = ParameterDirection.Output};
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

        public void Update(int accountId, int questionId, int statusAnswer,out string giftCode, out int responseStatus)
        {
            try
            {
                var oCommand = new SqlCommand("[SP_Cms_SurveyAccount_Update]") { CommandType = CommandType.StoredProcedure };
                oCommand.Parameters.AddWithValue("@_AccountId", accountId);
                oCommand.Parameters.AddWithValue("@_QuestionId", questionId);
                oCommand.Parameters.AddWithValue("@_StatusAnswer", statusAnswer);
                
                var _giftCode = new SqlParameter("@_GiftCode", SqlDbType.VarChar,50) { Direction = ParameterDirection.Output };
                var _responseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(_responseStatus);
                oCommand.Parameters.Add(_giftCode);
                _db.ExecuteNonQuery(oCommand);
                responseStatus = Convert.ToInt32(_responseStatus.Value);
                giftCode = Convert.ToString(_giftCode.Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
