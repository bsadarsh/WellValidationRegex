using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPRTDValidation
{
    public partial class WebUpdateStart : System.Web.UI.Page
    {
        protected string strMode
        {
            get
            {
                if (ViewState["strMode"] != null)
                    return Convert.ToString(ViewState["strMode"]);
                else
                    return string.Empty;
            }
            set
            {
                ViewState["strMode"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            strMode = Request.QueryString["mode"];
        }
    }
}