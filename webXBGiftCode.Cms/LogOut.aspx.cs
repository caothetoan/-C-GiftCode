using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webXBGiftCode.Cms.Common;

namespace webXBGiftCode.Cms
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Session.Abandon();
            Session.Clear();
            Response.Redirect(string.Format("{0}Login.aspx?ur={1}", Setting.SiteUrl, HttpUtility.UrlEncode(Request.Url.AbsoluteUri)), false);
        }
    }
}