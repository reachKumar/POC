using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POManagementASPNet
{
    public partial class frmSessionTimeOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Items.Contains("NotAuthorize"))
                    lblMessage.Text = hfNotAuhtorizeMessage.Value;
                //if ((ConfigurationSettings.AppSettings["ENVIORNMENT"]).ToString().Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
                //    hypLogin.NavigateUrl = @"~/LoginForm.aspx";
                //else
                //    hypLogin.NavigateUrl = @"~/Space/OldSupervisorApproval.aspx";
                if (Session != null)
                    Session.Abandon();
            }

            catch (Exception ex)
            { }
        }
    }
}