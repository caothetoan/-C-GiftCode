using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Go.GraphApi.Biz;

namespace webXBGiftCode.login
{
    public partial class Default : System.Web.UI.Page
    {
        GraphService _graphService = new GraphService();
       
        private string AccessToken = "AccessToken";
        /*private string AccountName = "AccountName";
        private string AccountId = "AccountId";*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var code = Request.QueryString["code"];

                if (!string.IsNullOrEmpty(code))
                {
                    var result = _graphService.GetAccessToken(code);
                    if (result != null)
                    {

                        Session[AccessToken] = result;
                      
                    }

                }
            }
        }
    }
}