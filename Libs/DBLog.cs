using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Libs
{
    public class DBLog
    {
        public long Id { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public DateTime Time { get; set; }
        public string IPAddress { get; set; }
        public string Contents { get; set; }
        public Byte Type { get; set; }
        public int ResponseStatus { get; set; }
        public int CreatedDate { get; set; }

        public DBLog()
        { }
        

        public void Insert(bool isSuccess)
        {
            try
            {
                if (!isSuccess)
                    Contents = "<span style='color:red'>" + Contents + "</span>";
                DBHelper db = new DBHelper(DBConfig.SQLConn);
                SqlCommand oCommand = new SqlCommand("sp_Logs_Insert");
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.Add(new SqlParameter("@AccountId", SqlDbType.Int) { Value = AccountId });
                oCommand.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.VarChar, 100) { Value = AccountName });
               // oCommand.Parameters.Add(new SqlParameter("@IP", SqlDbType.VarChar, 100) { Value = DBCommon.ClientIP });
                oCommand.Parameters.Add(new SqlParameter("@Contents", SqlDbType.NVarChar) { Value = Contents });
                oCommand.Parameters.Add(new SqlParameter("@Type", SqlDbType.TinyInt) { Value = this.Type });
                //oCommand.Parameters.Add(new SqlParameter("@isSuccess", SqlDbType.Bit) { Value = isSuccess });
                //oCommand.Parameters.Add(new SqlParameter("@ResponseStatus", SqlDbType.Int) { Value = -2345678 });
                db.ExecuteNonQuery(oCommand);
            }
            catch { }
        }

        public void Insert(long accId, string AccountName, string Contents, bool isSuccess, LogType logType)
        {
            try
            {
                if (!isSuccess)
                    Contents = "<span style='color:red'>" + Contents + "</span>";
                DBHelper db = new DBHelper(DBConfig.SQLConn);
                SqlCommand oCommand = new SqlCommand("sp_Logs_Insert");
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.Add(new SqlParameter("@AccountId", SqlDbType.Int) { Value = accId });
                oCommand.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.VarChar, 100) { Value = AccountName });
                //oCommand.Parameters.Add(new SqlParameter("@IP", SqlDbType.VarChar, 100) { Value = DBCommon.ClientIP });
                oCommand.Parameters.Add(new SqlParameter("@Contents", SqlDbType.NVarChar) { Value = Contents });
                oCommand.Parameters.Add(new SqlParameter("@Type", SqlDbType.TinyInt) { Value = logType });
                //oCommand.Parameters.Add(new SqlParameter("@isSuccess", SqlDbType.Bit) { Value = isSuccess });
                //oCommand.Parameters.Add(new SqlParameter("@ResponseStatus", SqlDbType.Int) { Value = -2345678 });
                db.ExecuteNonQuery(oCommand);
            }
            catch { }
        }

        public void Insert(long accId, string AccountName, string Contents, bool isSuccess, long ResponseStatus, LogType logType)
        {
            try
            {
                if (!isSuccess)
                    Contents = "<span style='color:red'>" + Contents + "</span>";
                DBHelper db = new DBHelper(DBConfig.SQLConn);
                SqlCommand oCommand = new SqlCommand("sp_Logs_Insert");
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.Add(new SqlParameter("@AccountId", SqlDbType.Int) { Value = accId });
                oCommand.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.VarChar, 100) { Value = AccountName });
               // oCommand.Parameters.Add(new SqlParameter("@IP", SqlDbType.VarChar, 100) { Value = DBCommon.ClientIP });
                oCommand.Parameters.Add(new SqlParameter("@Contents", SqlDbType.NVarChar) { Value = Contents });
                oCommand.Parameters.Add(new SqlParameter("@Type", SqlDbType.TinyInt) { Value = logType });
               // oCommand.Parameters.Add(new SqlParameter("@isSuccess", SqlDbType.Bit) { Value = isSuccess });
                //oCommand.Parameters.Add(new SqlParameter("@ResponseStatus", SqlDbType.BigInt) { Value = ResponseStatus });
                db.ExecuteNonQuery(oCommand);
            }
            catch { }
        }


        public List<DBLog> GetByAccountName(string AccountName, DateTime dateFrom, DateTime dateTo, int CurrPage, int PageSize, out int TotalRecord)
        {
            var db = new DBHelper(DBConfig.SQLConn);
            var oCommand = new SqlCommand("sp_Log_GetByAccount") { CommandType = CommandType.StoredProcedure };
            oCommand.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.VarChar, 100) { Value = AccountName });
            oCommand.Parameters.Add(new SqlParameter("@dateFrom", SqlDbType.DateTime) { Value = dateFrom });
            oCommand.Parameters.Add(new SqlParameter("@dateTo", SqlDbType.DateTime) { Value = dateTo });            
            oCommand.Parameters.Add(new SqlParameter("@CurrPage", SqlDbType.Int) { Value = CurrPage });
            oCommand.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int) { Value = PageSize });

            var output = new SqlParameter("@TotalRecord", SqlDbType.Int) { Direction = ParameterDirection.Output };
            oCommand.Parameters.Add(output);

            var tb = db.GetList<DBLog>(oCommand);
            int.TryParse(output.Value.ToString(), out TotalRecord);
            return tb;
        }
    }

    public class LogBuyTicket
    {
        public Int64 Id { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public int Ticket { get; set; }
        public int Price { get; set; }
        public string Content { get; set; }
        public int Option { get; set; }
        /// <summary>
        /// 0: Khong thanh cong, 1: Thanh cong
        /// </summary>
        public int Status { get; set; }
        public DateTime Created { get; set; }

        public void Insert()
        {
            try
            {
                if (Status != 2)
                    this.Content = "<span style='color:red'>" + this.Content + "</span>";
                var db = new DBHelper(DBConfig.SQLConn);
                var oCommand = new SqlCommand("[sp_LogBuyTicket_Insert]") { CommandType = CommandType.StoredProcedure };
                oCommand.Parameters.Add(new SqlParameter("@AccountId", SqlDbType.Int) { Value = this.AccountId });
                oCommand.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.VarChar) { Value = this.AccountName });
                oCommand.Parameters.Add(new SqlParameter("@Ticket", SqlDbType.VarChar) { Value = this.Ticket });
                oCommand.Parameters.Add(new SqlParameter("@Price", SqlDbType.Int) { Value = this.Price });
                oCommand.Parameters.Add(new SqlParameter("@Content", SqlDbType.NVarChar) { Value = this.Content });
                oCommand.Parameters.Add(new SqlParameter("@Option", SqlDbType.Int) { Value = this.Option });
                oCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int) { Value = this.Status });
                db.ExecuteNonQuery(oCommand);
            }
            catch (Exception)
            {

            }
        }

        public List<LogBuyTicket> GetList(string accName, DateTime dateFrom, DateTime dateTo, out Int64 _vcoin, out Int64 totalVcoinEvent, int CurrPage, int PageSize, out int TotalRecord)
        {
            totalVcoinEvent = _vcoin = 0;
            var db = new DBHelper(DBConfig.SQLConn);
            var oCommand = new SqlCommand("[sp_LogBuyTicket_GetPaged]") { CommandType = CommandType.StoredProcedure };
            oCommand.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.VarChar) { Value = accName });            
            oCommand.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = dateFrom });
            oCommand.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = dateTo });
            oCommand.Parameters.Add(new SqlParameter("@CurrPage", SqlDbType.Int) { Value = CurrPage });
            oCommand.Parameters.Add(new SqlParameter("@RecordPP", SqlDbType.Int) { Value = PageSize });

            var pa_vcoin = new SqlParameter("@VCoin", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            oCommand.Parameters.Add(pa_vcoin);

            var pa_totalVCoinEvent = new SqlParameter("@TotalVCoinOfEvent", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            oCommand.Parameters.Add(pa_totalVCoinEvent);

            var output = new SqlParameter("@TotalRecord", SqlDbType.Int) { Direction = ParameterDirection.Output };
            oCommand.Parameters.Add(output);

            var tb = db.GetList<LogBuyTicket>(oCommand);

            Int64.TryParse(pa_vcoin.Value.ToString(), out _vcoin);
            Int64.TryParse(pa_totalVCoinEvent.Value.ToString(), out totalVcoinEvent);
            int.TryParse(output.Value.ToString(), out TotalRecord);
            return tb;
        }
    }

    public enum LogType { User = 1, Admin = 2, System = 3, Error = -99, EBANK_BUYITEM = 4,
        /// <summary>
        /// log ebank get total
        /// </summary>
        EBANK_GETTOTAL = 5,  }
}
