using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Libs
{
    public class DBHistory
    {
        public long Id { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public int GiftCode { get; set; }
        public string GetCharacter { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<DBHistory> GetList(string _accName, DateTime fr, DateTime to, int CurrPage, int PageSize, out int total)
        {
            try
            {
                var db = new DBHelper(DBConfig.SQLConn);
                var oCommand = new SqlCommand("[sp_History_GetList]") { CommandType = CommandType.StoredProcedure };

                oCommand.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.VarChar) { Value = _accName });
                oCommand.Parameters.Add(new SqlParameter("@dateFrom", SqlDbType.DateTime) { Value = fr });
                oCommand.Parameters.Add(new SqlParameter("@dateTo", SqlDbType.DateTime) { Value = to });
                oCommand.Parameters.Add(new SqlParameter("@CurrPage", SqlDbType.Int) { Value = CurrPage });
                oCommand.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int) { Value = PageSize });
                var pa_total = new SqlParameter("@TotalRecord", SqlDbType.Int) { Direction = ParameterDirection.Output };
                oCommand.Parameters.Add(pa_total);
                var tb = db.GetList<DBHistory>(oCommand);
                int.TryParse(pa_total.Value.ToString(), out total);
                return tb;
            }
            catch
            {
                total = 0;
                return null;
            }
        }

        public DataTable GetMarquee()
        {
            try
            {
                var db = new DBHelper(DBConfig.SQLConn);
                var oCommand = new SqlCommand("[sp_History_GetMarquee]") { CommandType = CommandType.StoredProcedure };
                return db.getDataTable(oCommand);
            }
            catch
            {                
                return null;
            }
        }
    }
}
