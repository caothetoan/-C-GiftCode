using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webXBGiftCode.Cms.Common;

namespace webXBGiftCode.Cms
{
    public partial class CmsSite : System.Web.UI.MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Session[Setting.SESSION_USERNAME] == null ||
                string.IsNullOrEmpty(Session[Setting.SESSION_USERNAME].ToString()))
            {
                var pageName = Path.GetFileName(Request.Path);
                if (pageName != null &&
                    !pageName.Replace(".aspx", string.Empty).Equals("LogIn", StringComparison.CurrentCultureIgnoreCase))

                    Response.Redirect(string.Format("{0}Login.aspx?ur={1}", Setting.SiteUrl, HttpUtility.UrlEncode(Request.Url.AbsoluteUri)), false);
            }
            else
            {
                var isAdmin =
                    Setting.AdminList.IndexOf(Session[Setting.SESSION_USERNAME] + "|",
                                             StringComparison.CurrentCultureIgnoreCase) >= 0;
                if (!isAdmin)
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Redirect(string.Format("{0}Login.aspx?ur={1}", Setting.SiteUrl, HttpUtility.UrlEncode(Request.Url.AbsoluteUri)), false);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}