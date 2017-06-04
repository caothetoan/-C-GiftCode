using System.Configuration;

namespace webXBGiftCode.Cms.Common
{
    public static class Setting
    {
        // ReSharper disable InconsistentNaming
        public const string SESSION_USERID = "CMS_USER_ID";
        public const string SESSION_USERNAME = "CMS_USER_NAME";
        public const string SESSION_USEREMAIL = "CMS_USER_EMAIL";
        // ReSharper restore InconsistentNaming
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


        public static string SiteUrl
        {
            get
            {
                var url = string.Empty;

                try
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SiteUrl"]))
                        url = ConfigurationManager.AppSettings["SiteUrl"].Trim();
                }
                catch
                {
                }

                return url;
            }
        }

        public static string AdminList
        {
            get
            {
                var adminList = string.Empty;

                try
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ADMIN"]))
                        adminList = ConfigurationManager.AppSettings["ADMIN"].Trim();
                }
                catch
                {
                }

                return adminList;
            }
        }

        public static string ShortDateTimeFormat
        {
            get
            {
                var format = "dd/MM/yyyy";

                try
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ShortDateTimeFormat"]))
                        format = ConfigurationManager.AppSettings["ShortDateTimeFormat"].Trim();
                }
                catch
                {
                }

                return format;
            }
        }

        public static string LongDateTimeFormat
        {
            get
            {
                var format = "dd/MM/yyyy hh:mm:ss";

                try
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["LongDateTimeFormat"]))
                        format = ConfigurationManager.AppSettings["LongDateTimeFormat"].Trim();
                }
                catch
                {
                }

                return format;
            }
        }

        public static string MessageBoxScriptFormat
        {
            get
            {
                var script = "ShowMessageBox('{0}');";

                try
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MessageBoxScriptFormat"]))
                        script = ConfigurationManager.AppSettings["MessageBoxScriptFormat"].Trim();
                }
                catch
                {
                }

                return script;
            }
        }

        public static string NoImageAvailable
        {
            get
            {
                var url = "../Images/NoImage.png";

                try
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["NoImageAvailable"]))
                        url = ConfigurationManager.AppSettings["NoImageAvailable"].Trim();
                }
                catch
                {
                }

                return url;
            }
        }

        public static bool GridThemeEnable
        {
            get
            {
                var enable = true;
                try
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["GridThemeEnable"]))
                        enable = bool.Parse(ConfigurationManager.AppSettings["GridThemeEnable"].Trim());
                }
                catch
                {
                }
                return enable;
            }
        }

        public static bool NetworkFolderEnable
        {
            get
            {
                var enable = false;
                try
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["NetworkFolderEnable"]))
                        enable = bool.Parse(ConfigurationManager.AppSettings["NetworkFolderEnable"].Trim());
                }
                catch
                {
                }
                return enable;
            }
        }

        #region Network folders

        public static string PhotoUploadServer
        {
            get
            {
                var server = "/Uploaded/";
                try
                {
                    server = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["PhotoUploadServer"])
                                 ? ConfigurationManager.AppSettings["PhotoUploadServer"].Trim()
                                 : string.Empty;
                }
                catch
                {
                }
                return server;
            }
        }

        public static string PhotoDownloadServer
        {
            get
            {
                var server = "/Uploaded/";
                try
                {
                    server = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["PhotoDownloadServer"])
                                 ? ConfigurationManager.AppSettings["PhotoDownloadServer"].Trim()
                                 : string.Empty;
                }
                catch
                {
                }
                return server;
            }
        }

        public static string VideoUploadServer
        {
            get
            {
                var server = "~/Resources/Videos/";
                try
                {
                    server = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["VideoUploadServer"])
                                 ? ConfigurationManager.AppSettings["VideoUploadServer"].Trim()
                                 : string.Empty;
                }
                catch
                {
                }
                return server;
            }
        }

        public static string VideoDownloadServer
        {
            get
            {
                var server = string.Empty;
                try
                {
                    server = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["VideoDownloadServer"])
                                 ? ConfigurationManager.AppSettings["VideoDownloadServer"].Trim()
                                 : string.Empty;
                }
                catch
                {
                }
                return server;
            }
        }

        public static string SoundUploadServer
        {
            get
            {
                var server = "~/Resources/Sounds/";
                try
                {
                    server = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["SoundUploadServer"])
                                 ? ConfigurationManager.AppSettings["SoundUploadServer"].Trim()
                                 : string.Empty;
                }
                catch
                {
                }
                return server;
            }
        }

        public static string SoundDownloadServer
        {
            get
            {
                var server = "~/Resources/Sounds/";
                try
                {
                    server = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["SoundDownloadServer"])
                                 ? ConfigurationManager.AppSettings["SoundDownloadServer"].Trim()
                                 : string.Empty;
                }
                catch
                {
                }
                return server;
            }
        }

        #endregion Network folders

      

        #region Thumbnail size

        public static string ImageThumbnailSize
        {
            get
            {
                var size = ".300.0.cache";

                try
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ImageThumbnailSize"]))
                        size = ConfigurationManager.AppSettings["ImageThumbnailSize"].Trim();
                }
                catch
                {
                }

                return size;
            }
        }

        #endregion Thumbnail size

        
    }
}