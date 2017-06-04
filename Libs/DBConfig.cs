using System.Configuration;

namespace Libs
{
    /// <summary>
    /// Summary description for Config
    /// </summary>
    public sealed class DBConfig
    {
        private static readonly DBConfig instance = new DBConfig();
        private readonly string _DomainName;
        private readonly string _LoginURL;
        private readonly string _SQLConn;
        private readonly string _SSODomain;
        private readonly string _SiteName;
        private readonly string _urlRoot;

        private DBConfig()
        {
            _SSODomain = getAppStr("SSO_DOMAIN");
            _LoginURL = getAppStr("LOGIN_URL");
            _SQLConn = getConnStr("SQLConn");
            _DomainName = getAppStr("DomainName");
            _SiteName = getAppStr("SiteName");
            _urlRoot = getAppStr("URL_ROOT");
        }
        public static string UrlRoot
        {
            get { return instance._urlRoot; }
        }
        public static string SSODomain
        {
            get { return instance._SSODomain; }
        }

        public static string LoginURL
        {
            get { return instance._LoginURL; }
        }

        public static string SQLConn
        {
            get { return instance._SQLConn; }
        }

        public static string DomainName
        {
            get { return instance._DomainName; }
        }

        public static string SiteName
        {
            get { return instance._SiteName; }
        }

        private static DBConfig Instance
        {
            get { return instance; }
        }

        private string getConnStr(string Name)
        {
            var rijndaelKey = new RijndaelEnhanced(getAppStr("keyEncrypt"), "@1B2c3D4e5F6g7H8");
            return rijndaelKey.Decrypt(ConfigurationManager.ConnectionStrings[Name].ConnectionString);
        }

        private string getAppStr(string Name)
        {
            return ConfigurationManager.AppSettings[Name] == null
                       ? string.Empty
                       : ConfigurationManager.AppSettings[Name];
        }
    }
}