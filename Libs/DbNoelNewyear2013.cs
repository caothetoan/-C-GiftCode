using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Libs
{
    public class DbTongTuLenh
    {
        private readonly DBHelper _db;
        public DbTongTuLenh()
        {
            _db = new DBHelper(DBConfig.SQLConn);
        }
   //     /// <summary>
   //     /// Lấy thông tin đăng nhập của user
   //     /// </summary>
   //     /// <returns></returns>
   //     public List<DBCondition> GetListItemsCondition(int AccountId, int NumDay)
   //     {
   //         try
   //         {
   //             var oCommand = new SqlCommand("sp_Account_GetCondition") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.AddWithValue("@AccountId", AccountId);
   //             oCommand.Parameters.AddWithValue("@Day", NumDay);
   //             return _db.GetList<DBCondition>(oCommand);
   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }
   //     /// <summary>
   //     /// Ghi log đăng nhập của người dùng
   //     /// Người tạo: Vinh.vu >>> Ngày tạo:01/11/2014
   //     /// </summary>
   //     /// <param name="_Username"></param>
   //     /// <param name="_ClientIP"></param>
   //     /// <param name="_ContinuousLogin">Số ngày đăng nhập liên tiếp</param>
   //     /// <param name="_CountLoginInDay">Số lần đăng nhập trong ngày</param>
   //     /// <param name="_IsFirst"> =1 là lần đầu đăng nhập</param>
   //     /// <param name="_ResponseStatus">Trạng thái sp</param>
   //     public void SP_EventT11_SetLastLoginTime(string _Username, string _ClientIP, out int _ContinuousLogin, out int _CountLoginInDay, out int _IsFirst, out int _ResponseStatus)
   //     {
   //         var oCommand = new SqlCommand("SP_EventT11_SetLastLoginTime") { CommandType = CommandType.StoredProcedure };
   //         oCommand.Parameters.AddWithValue("@_Username", _Username);
   //         oCommand.Parameters.AddWithValue("@_ClientIP", _ClientIP);

   //         var p_ContinuousLogin = new SqlParameter("@_ContinuousLogin", SqlDbType.Int);
   //         p_ContinuousLogin.Direction = ParameterDirection.Output;
   //         oCommand.Parameters.Add(p_ContinuousLogin);

   //         var p_CountLoginInDay = new SqlParameter("@_CountLoginInDay", SqlDbType.Int);
   //         p_CountLoginInDay.Direction = ParameterDirection.Output;
   //         oCommand.Parameters.Add(p_CountLoginInDay);

   //         var p_IsFirst = new SqlParameter("@_IsFirst", SqlDbType.Int);
   //         p_IsFirst.Direction = ParameterDirection.Output;
   //         oCommand.Parameters.Add(p_IsFirst);

   //         var p_ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int);
   //         p_ResponseStatus.Direction = ParameterDirection.Output;
   //         oCommand.Parameters.Add(p_ResponseStatus);

   //         _db.ExecuteNonQuery(oCommand);

   //         _ContinuousLogin = (int)p_ContinuousLogin.Value;
   //         _CountLoginInDay = (int)p_CountLoginInDay.Value;
   //         _IsFirst = (int)p_IsFirst.Value;
   //         _ResponseStatus = (int)p_ResponseStatus.Value;
   //     }

   //     /// <summary>
   //     /// Lấy thông tin kho đồ
   //     /// </summary>
   //     public List<DBItemInventory> SP_EventT11_GetInventories(string _Username, int _EventID, int _PageNumber, int _PageSize, out int _TotalPage)
   //     {
   //         try 
   //         {
   //             var oCommand = new SqlCommand("SP_EventT11_GetInventories") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.AddWithValue("@_Username", _Username);
   //             oCommand.Parameters.AddWithValue("@_EventID", _EventID);
   //             oCommand.Parameters.AddWithValue("@_PageNumber", _PageNumber);
   //             oCommand.Parameters.AddWithValue("@_PageSize", _PageSize);

   //             var p_TotalRow = new SqlParameter("@_TotalPage", SqlDbType.Int);
   //             p_TotalRow.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_TotalRow);

   //             var objreturn = _db.GetList<DBItemInventory>(oCommand);
   //             _TotalPage = (int)p_TotalRow.Value;
   //             return objreturn;
   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }

   //     /// <summary>
   //     /// Lấy thông tin hoàn thành nhiệm vụ của tấn công
   //     /// </summary>
   //     /// <returns></returns>
   //     public List<DBCondition> SP_EventT11_GetAttackResult(string _Username, out string _Result)
   //     {
   //         try
   //         {
   //             var oCommand = new SqlCommand("SP_EventT11_GetAttackResult") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.AddWithValue("@_Username", _Username);

   //             var p_Result = new SqlParameter("@_Result", SqlDbType.NVarChar, 500);
   //             p_Result.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_Result);

   //             var objreturn = _db.GetList<DBCondition>(oCommand);

   //             _Result = p_Result.Value.ToString();
   //             return objreturn;
   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }
   //     /// <summary>
   //     /// Lấy dữ liệu sự kiện Vinh danh
   //     /// Người tao: Vinh.vu      Ngày tạo: 03/11/2014
   //     /// </summary>
   //     /// <param name="_Username"></param>
   //     /// <param name="_GloryType"></param>
   //     /// <param name="_PageNumber"></param>
   //     /// <param name="_PageSize"></param>
   //     /// <param name="_CurrNo"></param>
   //     /// <returns></returns>
   //     public List<DBCondition> SP_EventT11_GetGloryResult(string _Username, string _Nickname, int _GloryType, int _PageNumber, int _PageSize,
   //         out int _CurrNo, out int _TotalPage, out int _CurrInventoryID, out int _CurrStatus)
   //     {
   //         try
   //         {
   //             var oCommand = new SqlCommand("SP_EventT11_GetGloryResult") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.AddWithValue("@_Username", _Username);
   //             oCommand.Parameters.AddWithValue("@_Nickname", _Nickname);
   //             oCommand.Parameters.AddWithValue("@_GloryType", _GloryType);
   //             oCommand.Parameters.AddWithValue("@_PageNumber", _PageNumber);
   //             oCommand.Parameters.AddWithValue("@_PageSize", _PageSize);

   //             var p_CurrNo = new SqlParameter("@_CurrNo", SqlDbType.Int);
   //             p_CurrNo.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_CurrNo);

   //             var p_TotalRow = new SqlParameter("@_TotalRow", SqlDbType.Int);
   //             p_TotalRow.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_TotalRow);
                
   //              var p_CurrInventoryID = new SqlParameter("@_CurrInventoryID", SqlDbType.Int);
   //             p_CurrInventoryID.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_CurrInventoryID);

   //             var p_CurrStatus = new SqlParameter("@_CurrStatus", SqlDbType.Int);
   //             p_CurrStatus.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_CurrStatus);

   //             var objreturn = _db.GetList<DBCondition>(oCommand);
   //             _CurrNo = (int)p_CurrNo.Value;
   //             _TotalPage = (int)p_TotalRow.Value;
   //             _CurrInventoryID = (int)p_CurrInventoryID.Value;
   //             _CurrStatus = (int)p_CurrStatus.Value;
   //             return objreturn;
   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }
   //     /// <summary>
   //     /// Hàm quay vòng quay trinh thám
   //     /// Người tạo: anhnv ngày tạo 31/10/2014
   //     /// </summary>
   //     /// <returns></returns>
   //     public void SP_Event_T11_Spin(string userName, out int _ResponseStatus, out string _Result, out int _TotalSpin, out int _SpinFree)
   //     {
   //         try
   //         {
   //             var oCommand = new SqlCommand("SP_EventT11_Spin") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.AddWithValue("@_Username", userName);

   //             var p_Result = new SqlParameter("@_Result", SqlDbType.NVarChar, 500);
   //             p_Result.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_Result);

   //             var p_ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int);
   //             p_ResponseStatus.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_ResponseStatus);

   //             var p_SpinFree = new SqlParameter("@_SpinFree", SqlDbType.Int);
   //             p_SpinFree.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_SpinFree);

   //             var p_Spin = new SqlParameter("@_Spin", SqlDbType.Int);
   //             p_Spin.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_Spin);

   //             _db.ExecuteNonQuery(oCommand);

   //             var p_totalSpin = 0;
   //             if (p_Spin.Value != null)
   //             {
   //                 p_totalSpin += (int)p_Spin.Value;
   //             }
   //             if (p_SpinFree.Value != null)
   //             {
   //                 p_totalSpin += (int)p_SpinFree.Value;
   //             }
   //             _ResponseStatus = (int)p_ResponseStatus.Value;
   //             _Result = (string)p_Result.Value.ToString();
   //             _SpinFree = (int)p_SpinFree.Value;
   //             _TotalSpin = p_totalSpin;
   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }

   //     /// <summary>
   //     /// Lấy notification hiển thị chữ chạy
   //     /// Người tạo: vietanh.nguyen ngày tạo 4/11/2014
   //     /// </summary>
   //     /// 
   //     public List<DBNotification> SP_Event_GetNotification(int _Rows)
   //     {
   //         try
   //         {
   //             var oCommand = new SqlCommand("SP_Event_GetNotification") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.AddWithValue("@_Rows", _Rows);

   //             var objReturn = _db.GetList<DBNotification>(oCommand);
   //             return objReturn;
   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }

   //     /// <summary>
   //     /// Mua mượt quay
   //     /// Người tạo: vinh.vu ngày tạo 31/10/2014
   //     /// </summary>
   //     /// <returns></returns>
   //     public void SP_EventT11_AddSpin(string _Username, int _Quantity, int _Amount, out int _SpinFree, out int _Spin, out int _ResponseStatus)
   //     {
   //         try
   //         {
   //             var oCommand = new SqlCommand("SP_EventT11_AddSpin") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.AddWithValue("@_Username", _Username);
   //             oCommand.Parameters.AddWithValue("@_Quantity", _Quantity);
   //             oCommand.Parameters.AddWithValue("@_Amount", _Amount);

   //             var p_SpinFree = new SqlParameter("@_SpinFree", SqlDbType.Int);
   //             p_SpinFree.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_SpinFree);

   //             var p_Spin = new SqlParameter("@_Spin", SqlDbType.Int);
   //             p_Spin.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_Spin);

   //             var p_ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int);
   //             p_ResponseStatus.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_ResponseStatus);


   //             _db.ExecuteNonQuery(oCommand);

   //             _SpinFree = (int)p_SpinFree.Value;
   //             _Spin = (int)p_Spin.Value;
   //             _ResponseStatus = (int)p_ResponseStatus.Value;
   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }

   //     /// <summary>
   //     /// Lấy thông tin hoàn thành nhiệm vụ của chiếm đóng
   //     /// </summary>
   //     /// <returns></returns>
   //     public void SP_EventT11_GetBalance(string _Username, out int _Vcoin, out int _SpinFree, out int _Spin, out int _ResponseStatus)
   //     {
   //         try
   //         {
   //             var oCommand = new SqlCommand("SP_EventT11_GetBalance") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.AddWithValue("@_Username", _Username);

   //             var p_Vcoin = new SqlParameter("@_Vcoin", SqlDbType.Int);
   //             p_Vcoin.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_Vcoin);

   //             var p_SpinFree = new SqlParameter("@_SpinFree", SqlDbType.Int);
   //             p_SpinFree.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_SpinFree);
   //             var p_Spin = new SqlParameter("@_Spin", SqlDbType.Int);
   //             p_Spin.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_Spin);
   //             var p_ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int);
   //             p_ResponseStatus.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_ResponseStatus);
   //             _db.ExecuteNonQuery(oCommand);
   //             _Vcoin = (int)p_Vcoin.Value;
   //             _SpinFree = (int)p_SpinFree.Value;
   //             _Spin = (int)p_Spin.Value;
   //             _ResponseStatus = (int)p_ResponseStatus.Value;
   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }
   //     public void SP_EventT11_GetAccountInfo(string _Username, out int _SpinFree, out int _Spin, out int _Kills, out int _TotalExp, out int _Mission, out int _Headshot, out int _TotalVcoin, out int _ResponseStatus)
   //     {
   //         try
   //         {
   //             var oCommand = new SqlCommand("SP_EventT11_GetAccountInfo") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.AddWithValue("@_Username", _Username);

   //             var p_SpinFree = new SqlParameter("@_SpinFree", SqlDbType.Int);
   //             p_SpinFree.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_SpinFree);

   //             var p_Spin = new SqlParameter("@_Spin", SqlDbType.Int);
   //             p_Spin.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_Spin);

   //             var p_Kills = new SqlParameter("@_Kills", SqlDbType.Int);
   //             p_Kills.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_Kills);

   //             var p_TotalExp = new SqlParameter("@_TotalExp", SqlDbType.Int);
   //             p_TotalExp.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_TotalExp);

   //             var p_Mission = new SqlParameter("@_Mission", SqlDbType.Int);
   //             p_Mission.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_Mission);

   //             var p_Headshot = new SqlParameter("@_Headshot", SqlDbType.Int);
   //             p_Headshot.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_Headshot);

   //             var p_TotalVcoin = new SqlParameter("@_TotalVcoin", SqlDbType.Int);
   //             p_TotalVcoin.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_TotalVcoin);

   //             var p_ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int);
   //             p_ResponseStatus.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_ResponseStatus);

   //             _db.ExecuteNonQuery(oCommand);

   //             _SpinFree = (int)p_SpinFree.Value;
   //             _Spin = (int)p_Spin.Value;
   //             _Kills = (int)p_Kills.Value;
   //             _TotalExp = (int)p_TotalExp.Value;
   //             _Mission = (int)p_Mission.Value;
   //             _Headshot = (int)p_Headshot.Value;
   //             _TotalVcoin = (int)p_TotalVcoin.Value;
   //             _ResponseStatus = (int)p_ResponseStatus.Value;
   //         }
   //         catch (Exception myException)
   //         {
   //             throw new Exception(myException.Message);
   //         }
   //     }
   //     /// <summary>
   //     /// Get packetid 
   //     /// </summary>
   //     /// <param name="_Landmark"></param>
   //     /// <param name="_Username"></param>
   //     /// <param name="_PaketID"></param>
   //     /// <param name="_ResponseStatus"></param>
   //     public void SP_EventT11_ReceivePrize(int _PrizeID, int _InventoryID, string _Username, 
   //         out int _PaketID, out int _ResponseStatus, out int _OutInventoryID, out int _OutPrizeID,out int _OutPrizeValue)
   //     {
   //         try
   //         {
   //             var oCommand = new SqlCommand("[SP_EventT11_ReceivePrize]") { CommandType = CommandType.StoredProcedure };
   //            // oCommand.Parameters.AddWithValue("@_PrizeID", _PrizeID);
   //             oCommand.Parameters.AddWithValue("@_Username", _Username);

   //             //var p_OutInventoryID = oCommand.Parameters.AddWithValue("@_InventoryID", _InventoryID);
   //             //p_OutInventoryID.Direction = ParameterDirection.InputOutput;
   //             //oCommand.Parameters.Add(p_OutInventoryID);

   //             var DataRows = new SqlParameter("@_InventoryID", SqlDbType.Int) { Value = _InventoryID, Direction = ParameterDirection.InputOutput };
   //             oCommand.Parameters.Add(DataRows);

   //             var p_PrizeID = new SqlParameter("@_PrizeID", SqlDbType.Int) { Value = _PrizeID, Direction = ParameterDirection.InputOutput };
   //             oCommand.Parameters.Add(p_PrizeID);

   //             var p_PaketID = new SqlParameter("@_PacketID", SqlDbType.Int);
   //             p_PaketID.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_PaketID);

   //             var p_ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int);
   //             p_ResponseStatus.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_ResponseStatus);

   //              var p_OutPrizeValue = new SqlParameter("@_PrizeValue", SqlDbType.Float);
   //             p_OutPrizeValue.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_OutPrizeValue);
                

   //             _db.ExecuteNonQuery(oCommand);

   //             _PaketID = (int)p_PaketID.Value;
   //             _ResponseStatus = (int)p_ResponseStatus.Value;
   //             _OutInventoryID = (int)DataRows.Value;
   //             _OutPrizeID = (int)p_PrizeID.Value;
   //             _OutPrizeValue = Convert.ToInt32(p_OutPrizeValue.Value);
                
   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }
   //     public void sp_Attendance_Day(int AccountId, int NumDay, out string Image, out int OutPut)
   //     {
   //         try
   //         {
   //             var oCommand = new SqlCommand("sp_Attendance_Day") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.AddWithValue("@AccountId", AccountId);
   //             oCommand.Parameters.AddWithValue("@Day", NumDay);

   //             var pImage = new SqlParameter("@Image", SqlDbType.NVarChar, 100);
   //             pImage.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(pImage);

   //             var pOutPut = new SqlParameter("@OutPut", SqlDbType.Int);
   //             pOutPut.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(pOutPut);

   //             _db.ExecuteNonQuery(oCommand);
   //             Image = pImage.Value.ToString();
   //             OutPut = Convert.ToInt32(pOutPut.Value);

   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }
   //     //        sp_Attendance_Day]
   //     // -- Add the parameters for the stored procedure here
   //     // @AccountId INT,
   //     // @Day INT ,
   //     // @Image NVARCHAR(100) OUT,
   //     // @OutPut INT out
   //     //AS
   //     /// <summary>
   //     /// Lấy thông tin đăng nhập của user
   //     /// </summary>
   //     /// <returns></returns>
   //     public List<DBCondition> GetListItemsConditionFinish(int AccountId)
   //     {
   //         try
   //         {
   //             var oCommand = new SqlCommand("sp_Account_GetCondition_Finish") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.AddWithValue("@AccountId", AccountId);
   //             return _db.GetList<DBCondition>(oCommand);
   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }
   //     //// <summary>
   //     ///// Lấy thông tin đăng nhập của user
   //     ///// </summary>
   //     ///// <returns></returns>
   //     //public int NhanQua(int AccountId, int conId, int type, out int _Result)
   //     //{
   //     //    try
   //     //    {

   //     //        var oCommand = new SqlCommand("SP_HistoryItem_Insert") { CommandType = CommandType.StoredProcedure };
   //     //        oCommand.Parameters.AddWithValue("@_AccountId", AccountId);
   //     //        oCommand.Parameters.AddWithValue("@_IDItem", conId);
   //     //        oCommand.Parameters.AddWithValue("@_Type", type);

   //     //        var outResponseStatus = new SqlParameter("@_Result", SqlDbType.Int);
   //     //        outResponseStatus.Direction = ParameterDirection.Output;
   //     //        oCommand.Parameters.Add(outResponseStatus);
   //     //        _db.ExecuteNonQuery(oCommand);
   //     //        return _Result = (int)outResponseStatus.Value;
   //     //    }
   //     //    catch (Exception myException)
   //     //    {
   //     //        throw (new Exception(myException.Message));
   //     //    }
   //     //}
   //     //public void GetAccountInfo(int AccountId)
   //     //{
   //     //    var oCommand = new SqlCommand("sp_Accounts_GetInfo") { CommandType = CommandType.StoredProcedure };
   //     //    oCommand.Parameters.AddWithValue("@AccountId", AccountId);
   //     //    //return _db.GetList<Noel2013Condition>(oCommand);
   //     //}
   //     ///// <summary>
   //     ///// Lấy số vcoin đã tiêu thụ
   //     ///// </summary>
   //     ///// <param name="AccountId"></param>
   //     ///// <returns></returns>
   //     //public AccountValues GetVcoinPay(long AccountId)
   //     //{
   //     //    try
   //     //    {
   //     //        var oCommand = new SqlCommand("sp_Account_Get_VcoinPay") { CommandType = CommandType.StoredProcedure };
   //     //        oCommand.Parameters.AddWithValue("@AccountId", AccountId);
   //     //        var lst = _db.GetList<AccountValues>(oCommand);
   //     //        if (lst.Count == 1)
   //     //        {
   //     //            return lst[0];
   //     //        }
   //     //        else
   //     //        {
   //     //            return null;
   //     //        }
   //     //    }
   //     //    catch (Exception myException)
   //     //    {
   //     //        throw (new Exception(myException.Message));
   //     //    }
   //     //}

   //     public class AccountValues
   //     {
   //         public int AccountId { get; set; }
   //         public int ConditionId { get; set; }
   //         public int TotalValue { get; set; }
   //         public int UseValue { get; set; }
   //     }
   //     public List<DBitemVongQuay> GetVongQuay(int eventID)
   //     {
   //         try
   //         {
   //             var oCommand = new SqlCommand("SP_Event_GetPrizes") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.AddWithValue("@_EventID", eventID);
   //             return _db.GetList<DBitemVongQuay>(oCommand);
   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }
   //     public List<DBLogLogin> GetLogLogins(string _UserName, string _FromDate, string _ToDate)
   //     {
   //         try
   //         {
   //             var oCommand = new SqlCommand("SP_EventT11_GetLogLogin") { CommandType = CommandType.StoredProcedure };

   //             DateTime start = DateTime.ParseExact(_FromDate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US", false));
   //             DateTime end = DateTime.ParseExact(_ToDate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US", false));
   //             TimeSpan time = new TimeSpan(0, 23, 59, 59);
   //             end = end.Add(time);
   //             oCommand.Parameters.AddWithValue("@_UserName", _UserName);
   //             oCommand.Parameters.AddWithValue("@_FromDate", start);
   //             oCommand.Parameters.AddWithValue("@_ToDate", end);
   //             return _db.GetList<DBLogLogin>(oCommand);
   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }
   //     /// <summary>
   //     /// Lịch sử nhận thưởng CMS
   //     /// người tạo: Phùng thái thắng - ngày 07-11-2014
   //     /// </summary>
   //     /// <param name="_Username"></param>
   //     /// <param name="_EventID"></param>
   //     /// <param name="_PageNumber"></param>
   //     /// <param name="_PageSize"></param>
   //     /// <param name="_TotalPage"></param>
   //     /// <returns></returns>
   //     public List<DBLogNhanThuong> SP_EventT11_GetInventories_Log(string _Username, int _EventID, int _PageNumber, int _PageSize, out int _TotalPage)
   //     {
   //         try
   //         {
   //             var oCommand = new SqlCommand("SP_EventT11_GetInventories") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.AddWithValue("@_Username", _Username);
   //             oCommand.Parameters.AddWithValue("@_EventID", _EventID);
   //             oCommand.Parameters.AddWithValue("@_PageNumber", _PageNumber);
   //             oCommand.Parameters.AddWithValue("@_PageSize", _PageSize);



   //             var p_TotalRow = new SqlParameter("@_TotalPage", SqlDbType.Int);
   //             p_TotalRow.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(p_TotalRow);
   //             //_db.ExecuteNonQuery(oCommand);
   //             List<DBLogNhanThuong> objreturn = _db.GetList<DBLogNhanThuong>(oCommand);

   //             _TotalPage = (int)p_TotalRow.Value;
   //             return objreturn;
   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }
   //     /// <summary>
   //     /// get log tiêu vcoin ingame, mua lượt quay CMS
   //     /// người tạo: phùng thái thắng
   //     /// </summary>
   //     /// <param name="_Username"></param>
   //     /// <param name="_FromDate"></param>
   //     /// <param name="_ToDate"></param>
   //     /// <returns></returns>
   //     public List<DBLogVcoin> GetLogVcoin(string _Username, string _FromDate, string _ToDate, out Int64 _Totalvcoinspin)
   //     {
   //         try
   //         {
   //             SqlCommand oCommand = new SqlCommand("SP_EventT11_GetLogDeductVcoin") { CommandType = CommandType.StoredProcedure };

   //             DateTime start = DateTime.ParseExact(_FromDate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US", false));
   //             DateTime end = DateTime.ParseExact(_ToDate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US", false));
                
   //             TimeSpan time = new TimeSpan(0, 23, 59, 59);
   //             end = end.Add(time);
   //             oCommand.Parameters.Add("@_Username", _Username);
   //             oCommand.Parameters.Add("@_FromDate", start);
   //             oCommand.Parameters.Add("@_ToDate", end);

   //             var pOutPut = new SqlParameter("@_TotalVcoinBuySpin", SqlDbType.BigInt);
   //             pOutPut.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(pOutPut);

   //             _db.ExecuteNonQuery(oCommand);

   //             _Totalvcoinspin = Convert.ToInt64(pOutPut.Value);

   //             return _db.GetList<DBLogVcoin>(oCommand);
   //         }
   //         catch (Exception myException)
   //         {
   //             throw (new Exception(myException.Message));
   //         }
   //     }
   //     /// <summary>
   //     /// get log thêm lượt quay CMS
   //     /// người tạo: phùng thái thắng
   //     /// </summary>
   //     /// <param name="_Username"></param>
   //     /// <param name="_Type"></param>
   //     /// <param name="_FromDate"></param>
   //     /// <param name="_ToDate"></param>
   //     /// <returns></returns>
   //     public List<DBLogGiaoDich> SP_EventT11_GetLogAddSpin(string _Username, int _Type, string _FromDate, string _ToDate)
   //     {
   //         try
   //         {
   //             SqlCommand oCommand = new SqlCommand("SP_EventT11_GetLogAddSpin") { CommandType = CommandType.StoredProcedure };

   //             DateTime start = DateTime.ParseExact(_FromDate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US", false));
   //             DateTime end = DateTime.ParseExact(_ToDate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US", false));
   //             TimeSpan time = new TimeSpan(0, 23, 59, 59);
   //             end = end.Add(time);
   //             oCommand.Parameters.Add("@_Username", _Username);
   //             oCommand.Parameters.Add("@_Type", _Type);
   //             oCommand.Parameters.Add("@_FromDate", start);
   //             oCommand.Parameters.Add("@_ToDate", end);
   //             return _db.GetList<DBLogGiaoDich>(oCommand);
   //         }
   //         catch (Exception MyException)
   //         {
   //             throw (new Exception(MyException.Message));
   //         }
   //     }
   //     /// <summary>
   //     /// thống kê các thông số tài khoản CMS
   //     /// người tạo: phùng thái thắng
   //     /// </summary>
   //     /// <param name="_FromDate"></param>
   //     /// <param name="_ToDate"></param>
   //     /// <param name="_TotalFirstLogin"></param>
   //     /// <param name="_TotalLastLogin"></param>
   //     /// <param name="_TotalJoinedSpin"></param>
   //     /// <param name="_TotalJoinedAttack"></param>
   //     /// <param name="_TotalJoinedOccupy"></param>
   //     public void SP_EventT11_StatisticAccount(string _FromDate, string _ToDate, out double _TotalFirstLogin, out double _TotalLastLogin, out double _TotalJoinedSpin, out double _TotalJoinedAttack, out double _TotalJoinedOccupy)
   //     {
   //         try
   //         {
   //             SqlCommand oCommand = new SqlCommand("SP_EventT11_StatisticAccount") { CommandType = CommandType.StoredProcedure };

   //             DateTime start = DateTime.ParseExact(_FromDate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US", false));
   //             DateTime end = DateTime.ParseExact(_ToDate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US", false));
   //             TimeSpan time = new TimeSpan(0, 23, 59, 59);
   //             end = end.Add(time);
   //             NLogLogger.LogInfo("start"+start + ">>" + start);

   //             oCommand.Parameters.Add("@_FromDate", start);
   //             oCommand.Parameters.Add("@_ToDate", end);

   //             var o_TotalFirstLogin = new SqlParameter("@_TotalFirstLogin", SqlDbType.Int);
   //             o_TotalFirstLogin.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(o_TotalFirstLogin);

   //             var o_TotalLastLogin = new SqlParameter("@_TotalLastLogin", SqlDbType.Int);
   //             o_TotalLastLogin.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(o_TotalLastLogin);

   //             var o_TotalJoinedSpin = new SqlParameter("@_TotalJoinedSpin", SqlDbType.Int);
   //             o_TotalJoinedSpin.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(o_TotalJoinedSpin);

   //             var o_TotalJoinedAttack = new SqlParameter("@_TotalJoinedAttack", SqlDbType.Int);
   //             o_TotalJoinedAttack.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(o_TotalJoinedAttack);

   //             var o_TotalJoinedOccupy = new SqlParameter("@_TotalJoinedOccupy", SqlDbType.Int);
   //             o_TotalJoinedOccupy.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(o_TotalJoinedOccupy);


   //             _db.ExecuteNonQuery(oCommand);
   //             _TotalFirstLogin = int.Parse(o_TotalFirstLogin.Value.ToString());
   //             _TotalLastLogin = int.Parse(o_TotalLastLogin.Value.ToString());
   //             _TotalJoinedSpin = int.Parse(o_TotalJoinedSpin.Value.ToString());
   //             _TotalJoinedAttack = int.Parse(o_TotalJoinedAttack.Value.ToString());
   //             _TotalJoinedOccupy = int.Parse(o_TotalJoinedOccupy.Value.ToString());

   //         }
   //         catch (Exception MyException)
   //         {
   //             throw (new Exception(MyException.Message));
   //         }
   //     }
   //     /// <summary>
   //     /// thống kê các thông số về sự kiện trinh thám CMS
   //     /// người tạo: phùng thái thắng
   //     /// </summary>
   //     /// <param name="_FromDate"></param>
   //     /// <param name="_ToDate"></param>
   //     /// <param name="_TotalVcoin_AddSpin1"></param>
   //     /// <param name="_TotalVcoin_AddSpin5"></param>
   //     /// <param name="_TotalVcoin_AddSpin10"></param>
   //     /// <param name="_TotalAccount_AddSpin"></param>
   //     /// <param name="_TotalSpin_PrizeIsVcoin"></param>
   //     /// <param name="_TotalVcoin_PrizeIsVcoin"></param>
   //     public void SP_EventT11_StatisticSpin(string _FromDate, string _ToDate, out int _TotalVcoin_AddSpin1, out int _TotalVcoin_AddSpin5, out int _TotalVcoin_AddSpin10, out int _TotalAccount_AddSpin, out int _TotalSpin_PrizeIsVcoin, out int _TotalVcoin_PrizeIsVcoin)
   //     {
   //         try
   //         {
   //             SqlCommand oCommand = new SqlCommand("SP_EventT11_StatisticSpin") { CommandType = CommandType.StoredProcedure };
   //             DateTime start = DateTime.ParseExact(_FromDate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US", false));
   //             DateTime end = DateTime.ParseExact(_ToDate, "dd/MM/yyyy", new System.Globalization.CultureInfo("en-US", false));
   //             TimeSpan time = new TimeSpan(0, 23, 59, 59);
   //             end = end.Add(time);
   //             NLogLogger.LogInfo("start" + start + ">>" + start);

   //             oCommand.Parameters.Add("@_FromDate", start);
   //             oCommand.Parameters.Add("@_ToDate", end);

   //             var o_TotalVcoin_AddSpin1 = new SqlParameter("@_TotalVcoin_AddSpin1", SqlDbType.Int);
   //             o_TotalVcoin_AddSpin1.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(o_TotalVcoin_AddSpin1);

   //             var o_TotalVcoin_AddSpin5 = new SqlParameter("@_TotalVcoin_AddSpin5", SqlDbType.Int);
   //             o_TotalVcoin_AddSpin5.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(o_TotalVcoin_AddSpin5);

   //             var o_TotalVcoin_AddSpin10 = new SqlParameter("@_TotalVcoin_AddSpin10", SqlDbType.Int);
   //             o_TotalVcoin_AddSpin10.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(o_TotalVcoin_AddSpin10);

   //             var o_TotalAccount_AddSpin = new SqlParameter("@_TotalAccount_AddSpin", SqlDbType.Int);
   //             o_TotalAccount_AddSpin.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(o_TotalAccount_AddSpin);

   //             var o_TotalSpin_PrizeIsVcoin = new SqlParameter("@_TotalSpin_PrizeIsVcoin", SqlDbType.Int);
   //             o_TotalSpin_PrizeIsVcoin.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(o_TotalSpin_PrizeIsVcoin);

   //             var o_TotalVcoin_PrizeIsVcoin = new SqlParameter("@_TotalVcoin_PrizeIsVcoin", SqlDbType.Int);
   //             o_TotalVcoin_PrizeIsVcoin.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(o_TotalVcoin_PrizeIsVcoin);

   //             _db.ExecuteNonQuery(oCommand);
   //             _TotalVcoin_AddSpin1 = Convert.ToInt32(o_TotalVcoin_AddSpin1.Value);
   //             _TotalVcoin_AddSpin5 = Convert.ToInt32(o_TotalVcoin_AddSpin5.Value);
   //             _TotalVcoin_AddSpin10 = Convert.ToInt32(o_TotalVcoin_AddSpin10.Value);
   //             _TotalAccount_AddSpin = Convert.ToInt32(o_TotalAccount_AddSpin.Value);
   //             _TotalSpin_PrizeIsVcoin = Convert.ToInt32(o_TotalSpin_PrizeIsVcoin.Value);
   //             _TotalVcoin_PrizeIsVcoin = Convert.ToInt32(o_TotalVcoin_PrizeIsVcoin.Value);
   //         }
   //         catch (Exception MyException)
   //         {
   //             throw (new Exception(MyException.Message));
   //         }
   //     }
   //     /// <summary>
   //     /// Get thông tin giải thưởng CMS
   //     /// người tạo: phùng thái thắng
   //     /// </summary>
   //     /// <param name="_EventID"></param>
   //     /// <returns></returns>"- @_TotalPage

   //     public List<DBGetPrizes> SP_Event_GetPrizes(double _EventID)
   //     {
   //         try
   //         {
   //             SqlCommand oCommand = new SqlCommand("SP_Event_GetPrizes") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.Add("@_EventID", _EventID);
   //             return _db.GetList<DBGetPrizes>(oCommand);
   //         }
   //         catch (Exception ex)
   //         {
   //             throw (new Exception(ex.Message));
   //         }
           
   //     }
   //     /// <summary>
   //     /// Update trạng thái đã chốt cho 180 User không trúng thưởng bốc thăm (trong top 200 tháng)
   //     /// </summary>
   //     /// <param name="_GloryType"></param>
   //     /// <param name="_Round"></param>
   //     /// <param name="_Username"></param>
   //     /// <param name="_Status"></param>
   //     /// <param name="_Confirmname"></param>
   //     /// <param name="_ResponseStatus"></param>
   //     public void SP_EventT11_UpdateGloryStatus(  int _GloryType, int _Round, string _Username,int _Status, string _Confirmname, out int _ResponseStatus)
   //     {
   //         try
   //         {
   //             SqlCommand oCommand = new SqlCommand("SP_EventT11_UpdateGloryStatus") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.Add("@_GloryType", _GloryType);
   //             oCommand.Parameters.Add("@_Round", _Round);
   //             oCommand.Parameters.Add("@_Username", _Username);
   //             oCommand.Parameters.Add("@_Status", _Status);
   //             oCommand.Parameters.Add("@_Confirmname", _Confirmname);

   //             var o_ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int);
   //             o_ResponseStatus.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(o_ResponseStatus);

   //             _db.ExecuteNonQuery(oCommand);
   //             _ResponseStatus = (int)o_ResponseStatus.Value;
   //         }
   //         catch (Exception ex)
   //         {
   //             throw (new Exception(ex.Message));
   //         }
   //     }
   ////update trạng thái nhận quà
   //     public void SP_EventT11_UnReceivePrize_ForAdmin(int _InventoryID, string _Username, string _CreatedUser, out int _ResponseStatus)
   //     {
   //         try
   //         {
   //             SqlCommand oCommand = new SqlCommand("SP_EventT11_UnReceivePrize_ForAdmin") { CommandType = CommandType.StoredProcedure };
   //             oCommand.Parameters.Add("@_InventoryID", _InventoryID);
   //             oCommand.Parameters.Add("@_Username", _Username);
   //             oCommand.Parameters.Add("@_CreatedUser", _CreatedUser);

   //             var o_ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int);
   //             o_ResponseStatus.Direction = ParameterDirection.Output;
   //             oCommand.Parameters.Add(o_ResponseStatus);

   //             _db.ExecuteNonQuery(oCommand);
   //             _ResponseStatus = (int)o_ResponseStatus.Value;
   //         }
   //         catch (Exception ex)
   //         {
   //             throw (new Exception(ex.Message));
   //         }

   //     }
    }
}
