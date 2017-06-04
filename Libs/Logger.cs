namespace Libs
{
    public static class Log
    {
        public static void WriteLog(string s)
        {
            Libs.NLogLogger.LogInfo(string.Format("{0}", s));
        }
        public static void WriteLog(string functionName, string s)
        {
            Libs.NLogLogger.LogInfo(string.Format("[{0}] {1}", functionName, s));
        }
    }
}
