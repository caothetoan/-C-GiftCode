using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webXBGiftCode.Cms.Common;

namespace webXBGiftCode.Cms
{
    public partial class CmsPage : System.Web.UI.MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Session[Setting.SESSION_USERNAME] != null &&
                !string.IsNullOrEmpty(Session[Setting.SESSION_USERNAME].ToString())) return;

            lnkLogout.Visible = false;
            ShowMenu = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!Page.IsPostBack)
            {
                var today = DateTime.Today;
                lblDay.Text = today.ToString("dd");
                lblMonth.Text = today.ToString("MM");
                lblYear.Text = today.ToString("yyyy");
            }

            if (Session[Setting.SESSION_USERNAME] != null &&
                !string.IsNullOrEmpty(Session[Setting.SESSION_USERNAME].ToString()))
            {
                lblAccountName.Text = string.Format("Xin chào: {0}", Session[Setting.SESSION_USERNAME]);
                lnkLogout.Visible = true;
                lnkLogout.NavigateUrl = string.Format("Logout.aspx?ur={0}",
                                                      HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
            }
            else
            {
                lblAccountName.Text = string.Empty;
                lnkLogout.Visible = false;
            }
        }

        public void SetLoginStatus(string admin)
        {
            if (!string.IsNullOrEmpty(admin))
            {
                lblAccountName.Text = string.Format("Xin chào: {0}", admin);
                lnkLogout.Visible = true;
            }
            else
            {
                lblAccountName.Text = string.Empty;
                lnkLogout.Visible = false;
            }
        }

        public bool ShowMenu
        {
            set { MainMenu.Visible = value; }
        }
    }
}