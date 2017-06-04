using System;
using Go.GraphApi.Biz;

namespace webXBGiftCode
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected int AccountId
        {
            get
            {
                if (Session["AccountId"] != null)
                    return Int32.Parse(Session["AccountId"].ToString());
                return -1;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Session.Abandon();
            Session.Clear();
            var urlReturn = Request.QueryString["ur"];
            string logoutUrl = string.Format("{0}", GoConstants.GoUrlLogout(AccountId, urlReturn));
               
            Response.Redirect(logoutUrl);
            
        }
    }
}