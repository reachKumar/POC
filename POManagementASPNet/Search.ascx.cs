using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;

namespace POManagementASPNet
{
    public partial class Search : System.Web.UI.UserControl
    {
        //private Employee loggedInUser = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    loggedInUser = (Employee)Session["Employee"];

            //    if (!loggedInUser.HasEmployeeAccess(Roles.PORequester) && !loggedInUser.HasEmployeeAccess(Roles.HOD) && !loggedInUser.HasEmployeeAccess(Roles.COO) && !loggedInUser.HasEmployeeAccess(Roles.CFO)
            //       && !loggedInUser.HasEmployeeAccess(Roles.CEO) && !loggedInUser.HasEmployeeAccess(Roles.Supervisor) && !loggedInUser.HasEmployeeAccess(Roles.Admin) && !loggedInUser.HasEmployeeAccess(Roles.PreApprover))
            //    {
            //        Response.Redirect("Login.aspx?resign=true");
            //    }
            //    //Response.Redirect("NewPO.aspx");

            //    if (loggedInUser == null)
            //    {
            //        Response.Redirect("Login.aspx");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message);
            //}
        }      

        protected void btnSearchPO_Click(object sender, EventArgs e)
        {
            if (txtSearchPO.Text == "")
            {
                lblSearchMessage.Text = "Please enter PO number";
            }
            else
            {
                Int64 poID = Convert.ToInt64(txtSearchPO.Text);
                Response.Redirect("Search.aspx?E=" + poID);
            }
        }

        protected void lnkAdvanceSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("Search.aspx");
        }       
    }
}