using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;

namespace Libs
{
    public sealed class DBCommon
    {
        public static string ClientIP
        {
            get
            {
                string IP = "";
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                if (IP == "")
                {
                    IP = HttpContext.Current.Request.UserHostAddress;
                }
                return IP;
                
            }
        }

        public static string UrlRoot
        {
            get
            {
                return ConfigurationManager.AppSettings["URL_ROOT"] == null ? "" : ConfigurationManager.AppSettings["URL_ROOT"];
            }
        }
        public static string URL_ROOT_CATE_URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL_ROOT_CATE_URL"] == null ? "" : ConfigurationManager.AppSettings["URL_ROOT_CATE_URL"];
            }
        }


        public static string SiteName
        {
            get
            {
                string sRet = DBConfig.SiteName;
                if (string.IsNullOrEmpty(sRet))
                {
                    string sDomain = HttpContext.Current.Request.Url.Host.Trim().ToLower();
                    if (sDomain.IndexOf("localhost") < 0)
                    {
                        sRet = sDomain.Replace("." + DBConfig.DomainName, "");
                    }
                    else
                    {
                        sRet = "www";
                    }
                }
                return sRet;
            }
        }

        public string XSSFilter(string sValue)
        {
            string sTemp = "?=:/._-0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string sOut = "";
            for (int i = 0; i < sValue.Length; i++)
            {
                if (sTemp.IndexOf(sValue[i]) >= 0)
                {
                    sOut += sValue[i];
                }
            }

            return sOut;
        }

        public static String UCS2Convert(string sContent)
        {
            if (string.IsNullOrEmpty(sContent))
                return string.Empty;
            sContent = sContent.Trim();
            String sUTF8Lower =
                "a|á|à|ả|ã|ạ|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ|đ|e|é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ|i|í|ì|ỉ|ĩ|ị|o|ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ|u|ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự|y|ý|ỳ|ỷ|ỹ|ỵ";

            String sUTF8Upper =
                "A|Á|À|Ả|Ã|Ạ|Ă|Ắ|Ằ|Ẳ|Ẵ|Ặ|Â|Ấ|Ầ|Ẩ|Ẫ|Ậ|Đ|E|É|È|Ẻ|Ẽ|Ẹ|Ê|Ế|Ề|Ể|Ễ|Ệ|I|Í|Ì|Ỉ|Ĩ|Ị|O|Ó|Ò|Ỏ|Õ|Ọ|Ô|Ố|Ồ|Ổ|Ỗ|Ộ|Ơ|Ớ|Ờ|Ở|Ỡ|Ợ|U|Ú|Ù|Ủ|Ũ|Ụ|Ư|Ứ|Ừ|Ử|Ữ|Ự|Y|Ý|Ỳ|Ỷ|Ỹ|Ỵ";

            String sUCS2Lower =
                "a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|d|e|e|e|e|e|e|e|e|e|e|e|e|i|i|i|i|i|i|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|u|u|u|u|u|u|u|u|u|u|u|u|y|y|y|y|y|y";

            String sUCS2Upper =
                "A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|D|E|E|E|E|E|E|E|E|E|E|E|E|I|I|I|I|I|I|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|U|U|U|U|U|U|U|U|U|U|U|U|Y|Y|Y|Y|Y|Y";

            String[] aUTF8Lower = sUTF8Lower.Split(new[] {'|'});

            String[] aUTF8Upper = sUTF8Upper.Split(new[] {'|'});

            String[] aUCS2Lower = sUCS2Lower.Split(new[] {'|'});

            String[] aUCS2Upper = sUCS2Upper.Split(new[] {'|'});

            Int32 nLimitChar;

            nLimitChar = aUTF8Lower.GetUpperBound(0);

            for (int i = 1; i <= nLimitChar; i++)
            {
                sContent = sContent.Replace(aUTF8Lower[i], aUCS2Lower[i]);

                sContent = sContent.Replace(aUTF8Upper[i], aUCS2Upper[i]);
            }
            string sUCS2regex = @"[A-Za-z0-9- ]";
            string sEscaped =
                new Regex(sUCS2regex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture).
                    Replace(sContent, string.Empty);
            if (string.IsNullOrEmpty(sEscaped))
                return sContent;
            sEscaped = sEscaped.Replace("[", "\\[");
            sEscaped = sEscaped.Replace("]", "\\]");
            sEscaped = sEscaped.Replace("^", "\\^");
            string sEscapedregex = @"[" + sEscaped + "]";
            return
                new Regex(sEscapedregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture)
                    .Replace(sContent, string.Empty);
        }

        public static string SubString(string sSource, int length)
        {
            if (string.IsNullOrEmpty(sSource))
                return string.Empty;
            if (sSource.Length <= length)
                return sSource;

            string mSource = sSource;
            int nLength = length;

            int m = mSource.Length;
            while (nLength > 0 && mSource[nLength].ToString() != " ")
            {
                nLength--;
            }
            mSource = mSource.Substring(0, nLength);
            return mSource + "...";
        }

        public static string StripDiacritics(string accented)
        {
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = accented.Normalize(System.Text.NormalizationForm.FormD);
            string content=regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            content = content.Replace(".", "-");
            content = content.Replace("--", "-");
            content = content.Replace(" ", "-");            
            content = content.Replace("?", "-");
            content = content.Replace("\"", "-");
            content = content.Replace("'", "-");
            content = content.Replace("!", "-");
            content = content.Replace("&", "-");
            content = content.Replace(":", "-");            
            return content;
        }

        public static string StripDiacriticsFile(string accented)
        {
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = accented.Normalize(System.Text.NormalizationForm.FormD);
            string content = regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');            
            content = content.Replace("--", "-");
            content = content.Replace(" ", "-");
            content = content.Replace("?", "-");
            content = content.Replace("\"", "-");
            content = content.Replace("'", "-");
            content = content.Replace("!", "-");
            content = content.Replace("&", "-");
            content = content.Replace(":", "-");
            return content;
        }

        public static string getCateIdByKey(String cateIdKey)
        {
            return ConfigurationManager.AppSettings[cateIdKey] == null ? "" : ConfigurationManager.AppSettings[cateIdKey];
        }

        public static string rewriteString(string str)
        {
            string sRewriteUrl = DBCommon.UCS2Convert(str).Replace(" ", "-").ToLower();
            while (sRewriteUrl.IndexOf("--") > -1)
            {
                sRewriteUrl = sRewriteUrl.Replace("--", "-");
            }
            while (sRewriteUrl.StartsWith("/"))
            {
                sRewriteUrl = sRewriteUrl.Remove(0, 1);
            }
            while (sRewriteUrl.EndsWith("/"))
            {
                sRewriteUrl = sRewriteUrl.Remove(sRewriteUrl.Length - 1, 1);
            }

            return sRewriteUrl;
        }
        
        public static string ConvertImgCache(string img, int width, int height)
        {
            if (string.IsNullOrEmpty(img))
            {
                return UrlRoot + "images/no_photo.gif";
            }
            if (checkUrlServerIUmgRoor(img))
            {
                img = img + "." + width + "." + height + ".cache";
            }

            return img;
        }

        public static string URL_SERVER_IMG_ROOT
        {
            get
            {
                return ConfigurationManager.AppSettings["URL_SERVER_IMG_ROOT"] == null ? "" : ConfigurationManager.AppSettings["URL_SERVER_IMG_ROOT"];
            }
        }

        private static bool checkUrlServerIUmgRoor(string img)
        {
            string[] lstUrl = URL_SERVER_IMG_ROOT.Split(",".ToCharArray());
            foreach (var item in lstUrl)
            {
                if (img.IndexOf(item.ToString()) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Author: Duocbv
        /// Note: Hàm này set cache từ lúc tạo object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheId"></param>
        /// <param name="getItemCallback"></param>
        /// <param name="_slidingExpire"></param>
        /// <returns></returns>
        public static T Get<T>(string cacheId, Func<T> getItemCallback, int _hours) where T : class
        {
            cacheId = cacheId.Trim().ToLower();
            T item = HttpRuntime.Cache.Get(cacheId) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpRuntime.Cache.Insert(cacheId, item, null, DateTime.UtcNow.AddHours(_hours), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            return item;
        }

        /// <summary>
        /// Author: Duocbv
        /// Note: Hàm này set cache từ lúc lần truy cập cuối
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheId"></param>
        /// <param name="getItemCallback"></param>
        /// <param name="_hours"></param>
        /// <returns></returns>
        public static T Get<T>(string cacheId, Func<T> getItemCallback, TimeSpan _slidingExpire) where T : class
        {
            cacheId = cacheId.Trim().ToLower();
            T item = HttpRuntime.Cache.Get(cacheId) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpRuntime.Cache.Insert(cacheId,
                            item,
                            null,
                            System.Web.Caching.Cache.NoAbsoluteExpiration,
                            _slidingExpire,
                            System.Web.Caching.CacheItemPriority.Normal,
                            null);
            }
            return item;
        }

        public static string formatDateTime(DateTime _dateTime)
        {
            //string[] f = {"Chủ nhật", "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy"};
            var e = new string[3, 2] { { "11", "sáng" }, { "14", "trưa" }, { "19", "chiều" } };
            DateTime currentDate = DateTime.Now;
            System.TimeSpan diff1 = currentDate.Subtract(_dateTime);
            double secondsNum = Math.Floor(diff1.TotalSeconds);
            if (secondsNum < 0)
            {
                return _dateTime.ToString("HH:mm dd/MM/yyyy");
            }
            if (secondsNum < 60)
            {
                return (secondsNum == 0 ? "vài" : secondsNum.ToString()) + " giây trước";
            }
            if (secondsNum < 3600) return Math.Floor(secondsNum / 60) + " phút trước";
            if (secondsNum < 43200) return Math.Floor(secondsNum / 3600) + " tiếng trước";

            int h = _dateTime.Hour;
            int m = _dateTime.Minute;

            if (secondsNum < 518400)
            {
                string b = "tối";
                for (int i = 0; i < 3; i++) if (h < (Int32.Parse(e[i, 0])))
                    {
                        b = e[i, 1];
                        break;
                    }

                string k = string.Empty;
                if (currentDate.Day == _dateTime.Day)
                    k = "hôm nay";
                else
                    return "vào lúc " + _dateTime.ToString("HH:mm dd/MM/yyyy");

                return "vào lúc " + (h % 12).ToString() + ":" + m + " " + b + " " + k;
            }
            return "vào lúc " + _dateTime.ToString("HH:mm dd/MM/yyyy");
        }
    }
}