using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace webXBGiftCode.Cms.Common
{
    public static class Cache
    {
        public static string ContentTypeAll = "ContentType_All";

        public static int Duration
        {
            get
            {
                var duration = 120;
                try
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheDuration"]))
                        duration = int.Parse(ConfigurationManager.AppSettings["CacheDuration"]);
                }
                catch
                {
                }
                return duration;
            }
        }

        public static void Flush()
        {
            var keys = new List<string>();
            var enumerator = HttpContext.Current.Cache.GetEnumerator();
            while (enumerator.MoveNext()) keys.Add(enumerator.Key.ToString());
            foreach (var key in keys) HttpContext.Current.Cache.Remove(key);
        }

        public static void Remove(string prefix)
        {
            var keys = new List<string>();
            var enumerator = HttpContext.Current.Cache.GetEnumerator();
            while (enumerator.MoveNext()) keys.Add(enumerator.Key.ToString());
            foreach (var key in keys.Where(key => key.StartsWith(prefix)))
                HttpContext.Current.Cache.Remove(key);
        }
    }
}