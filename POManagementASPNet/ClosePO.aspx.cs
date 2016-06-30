using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Web.UI.HtmlControls;

namespace POManagementASPNet
{
    public partial class ClosePO : System.Web.UI.Page
    {
        private Employee loggedInUser = null;
        string userName;
        string userEmail;
        //static Int64 poID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            loggedInUser = (Employee)Session["Employee"];
            userName = Session["PO_UserName"].ToString();
            userEmail = Session["UserEmail"].ToString();

            HtmlGenericControl liHome = (HtmlGenericControl)this.Master.FindControl("liHome");
            HtmlGenericControl liNew = (HtmlGenericControl)this.Master.FindControl("liNew");
            HtmlGenericControl liEdit = (HtmlGenericControl)this.Master.FindControl("liEdit");
            HtmlGenericControl liApprove = (HtmlGenericControl)this.Master.FindControl("liApprove");
            HtmlGenericControl liClose = (HtmlGenericControl)this.Master.FindControl("liClose");
            HtmlGenericControl liAdmin = (HtmlGenericControl)this.Master.FindControl("liAdmin");
            HtmlGenericControl liReport = (HtmlGenericControl)this.Master.FindControl("liReport");
            liHome.Attributes.Add("class", "");
            liNew.Attributes.Add("class", "");
            liEdit.Attributes.Add("class", "");
            liApprove.Attributes.Add("class", "");
            liClose.Attributes.Add("class", "active");
            liAdmin.Attributes.Add("class", "");
            liReport.Attributes.Add("class", "");

            if (loggedInUser == null)
            {
                Response.Redirect("Login.aspx?resign=true");
            }



            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["E"]))
                {
                    string queryStringE = Convert.ToString(Request["E"]);
                    Int64 poID = Convert.ToInt64(queryStringE);
                    FillGrid(poID);
                }
                else
                    FillGridView();
            }
        }

        protected void grdClosedPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdClosedPO.PageIndex = e.NewPageIndex;
            FillGridView();
        }

        protected void grdClosedPO_RowCommand(object sender, GridViewCommandEventArgs e)
        {            
            if (e.CommandName == "View")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnkPOMainID = (LinkButton)row.Cells[1].FindControl("lnkPOMainID");
                Int64 intPONumber = Convert.ToInt64(lnkPOMainID.CommandArgument.ToString());
                //Int64 intPONumber = Convert.ToInt64(row.Cells[1].Text);
                Response.Redirect("NewPO.aspx?CV=" + intPONumber);
            }

            if (e.CommandName == "Print")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnkPOMainID = (LinkButton)row.Cells[1].FindControl("lnkPOMainID");
                Int64 intPONumber = Convert.ToInt64(lnkPOMainID.CommandArgument.ToString());
                //int intPONumber = Convert.ToInt32(grdrow.Cells[1].Text);
                Response.Redirect("PrintPO.aspx?EP=" + intPONumber);
            }
            if (e.CommandName == "Reset")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnkPOMainID = (LinkButton)row.Cells[1].FindControl("lnkPOMainID");
                Int64 intPONumber = Convert.ToInt64(lnkPOMainID.CommandArgument.ToString());
                //Int64 intPONumber = Convert.ToInt64(row.Cells[1].Text);
                new POMain().UpdatePOToReset(intPONumber,userName);
                FillGridView();
            }
            if (e.CommandName == "Receiving")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnkPOMainID = (LinkButton)row.Cells[1].FindControl("lnkPOMainID");
                Int64 intPONumber = Convert.ToInt64(lnkPOMainID.CommandArgument.ToString());
                //Int64 intPONumber = Convert.ToInt64(row.Cells[1].Text);
                Response.Redirect("ReceivingPO.aspx?E=" + intPONumber);
            }

            if (e.CommandName == "InvoicePO")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnkPOMainID = (LinkButton)row.Cells[1].FindControl("lnkPOMainID");
                Int64 intPONumber = Convert.ToInt64(lnkPOMainID.CommandArgument.ToString());
                //Int64 intPONumber = Convert.ToInt64(row.Cells[1].Text);
                Response.Redirect("ReceivingPO.aspx?I=" + intPONumber);
            }

            if (e.CommandName == "")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnkPOMainID = (LinkButton)row.Cells[1].FindControl("lnkPOMainID");
                Int64 poMainID = Convert.ToInt64(lnkPOMainID.CommandArgument.ToString());
                Response.Redirect("ApprovalStatus.aspx?ID=" + poMainID);
            }

        }        

        private void FillGridView()
        {
            grdClosedPO.DataSource = new POMain().GetPOForClosed();
            grdClosedPO.DataBind();
        }

        private void FillGrid(Int64 poID)
        {            
            DataSet dsPOData = new POMain().BindPOData(poID);
            grdClosedPO.DataSource = dsPOData;
            grdClosedPO.DataBind();
        }
    }
}