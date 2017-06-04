using System;
public static class Parameters
{
    public const string SessionVcoin = "SessionVcoin";
    public const string SessionLuotQuay = "SessionLuotQuay";
    public const string SessionNick = "SessionNick";
    public const string SessionLuotQuayMienPhi = "SessionLuotQuayMienPhi";
    public const string SessionCheckGetXengFree = "SessionCheckGetXengFree";

    public static int getDateIndex(DateTime? date = null)
    {
        if (date == null)
            date = GetCurrentDate();

        DateTime start = DateTime.ParseExact(Libs.DBCommon.getCateIdByKey("START_DATE"), "yyyy-MM-dd", new System.Globalization.CultureInfo("en-US", false));
        //DateTime startServerFormat = DateTime.ParseExact(start.ToString("yyyy-MM-dd"), "yyyy-MM-dd", new System.Globalization.CultureInfo("en-US", false));
        int index = date.Value.Subtract(start).Days;
        return index >= 10 ? 10 : index;
    }

    public static DateTime GetCurrentDate()
    {
        if(string.IsNullOrEmpty(Libs.DBCommon.getCateIdByKey("CURR_DATE")))
            return DateTime.Now;

        return  DateTime.ParseExact(Libs.DBCommon.getCateIdByKey("CURR_DATE"), "yyyy-MM-dd", new System.Globalization.CultureInfo("en-US", false));
    }

    public static DateTime GetStartDate()
    {
      return  DateTime.ParseExact(Libs.DBCommon.getCateIdByKey("START_DATE"), "yyyy-MM-dd", new System.Globalization.CultureInfo("en-US", false));
    }
}
 