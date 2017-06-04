using System.Configuration;

namespace Libs
{
    public class Config
    {
       
        public static string ContactEmail = ConfigurationSettings.AppSettings["ContactEmail"];
        public static string SupporterYahoo = ConfigurationSettings.AppSettings["SupporterYahoo"];
        public static string SupporterMobile = ConfigurationSettings.AppSettings["SupporterMobile"];
        public static string SiteSlogan = ConfigurationSettings.AppSettings["SiteSlogan"];
        public static string SiteName = ConfigurationSettings.AppSettings["SiteName"]??"Bói Hay Lắm";
        public static string SiteUrl = ConfigurationSettings.AppSettings["SiteUrl"] ?? "http://boi.haylam.vn/";
        public static string Version = ConfigurationSettings.AppSettings["VersionMedia"] ?? "1.0";
        public static string FbAppId = ConfigurationSettings.AppSettings["FbAppId"] ?? "132607496783912";

        
        public static string UrlFanpage
        {
            get
            {
                return
                    ConfigurationManager.AppSettings["UrlFanpage"] ?? "https://www.facebook.com/haylamchamvn";
            }
        }
       
        public static bool DebugMode
        {
            get
            {
                var d = ConfigurationSettings.AppSettings["DEBUG_MODE"] ?? "false";
                bool debug;
                bool.TryParse(d, out debug);
                return debug;
            }
        }
        public static string MailServer = "pop.mail.vtc.vn";
        public static string MailDomain = "@vtc.vn";
    }
}
