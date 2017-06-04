using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Go.GraphApi.Biz;

namespace webXBGiftCode.Master
{
    public partial class Default : System.Web.UI.MasterPage
    {
        public int AccountId
        {
            get
            {
                if (Session["AccountId"] != null)
                    return int.Parse(Session["AccountId"].ToString());
                return -1;
            }
        }
        protected bool IsOnline
        {
            get { return AccountId > 0; }
        }
        protected void lbtnLogout_OnClick(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect(UrlLogout);
        }
        protected string UrlAbsoluteUri
        {
            get { return HttpContext.Current.Request.Url.AbsoluteUri; }
        }
        protected string UrlLogout
        {
            get
            {
                return
                    GoConstants.GoUrlLogout(AccountId, UrlAbsoluteUri);
            }
        }
        protected string UrlFanpage
        {
            get
            {
                return
                    ConfigurationManager.AppSettings["UrlFanpage"] ?? "http://facebook.com/duoihinhbatchu/";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}