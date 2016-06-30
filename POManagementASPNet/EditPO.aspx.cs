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
    public partial class EditPO : System.Web.UI.Page
    {
        private Employee loggedInUser = null;       
        //static string poType = null;
        string sessionUserID;//added to use for logged in user

        protected void Page_Load(object sender, EventArgs e)
        {
            loggedInUser = (Employee)Session["Employee"];
            string userName = Session["PO_UserName"].ToString();
            sessionUserID = Session["UserID"].ToString();

            if (loggedInUser == null)
            {
                Response.Redirect("Login.aspx?resign=true");
            }


            HtmlGenericControl liHome = (HtmlGenericControl)this.Master.FindControl("liHome");
            HtmlGenericControl liNew = (HtmlGenericControl)this.Master.FindControl("liNew");
            HtmlGenericControl liEdit = (HtmlGenericControl)this.Master.FindControl("liEdit");
            HtmlGenericControl liApprove = (HtmlGenericControl)this.Master.FindControl("liApprove");
            HtmlGenericControl liClose = (HtmlGenericControl)this.Master.FindControl("liClose");
            HtmlGenericControl liAdmin = (HtmlGenericControl)this.Master.FindControl("liAdmin");
            HtmlGenericControl liReport = (HtmlGenericControl)this.Master.FindControl("liReport");
            liHome.Attributes.Add("class", "");
            liNew.Attributes.Add("class", "");
            liEdit.Attributes.Add("class", "active");
            liApprove.Attributes.Add("class", "");
            liClose.Attributes.Add("class", "");
            liAdmin.Attributes.Add("class", "");
            liReport.Attributes.Add("class", "");

            if (!this.IsPostBack)
            {               
                if (!string.IsNullOrEmpty(Request["E"]))
                {
                    string queryStringE = Convert.ToString(Request["E"]);
                    Int64 poID = Convert.ToInt64(queryStringE);
                    FillGrid(poID);                    
                }
                else if(loggedInUser.HasEmployeeAccess(Roles.Admin))
                {
                    userName = null;
                    FillGridView(userName);
                }
                else
                    FillGridView(userName);
            }
        }

        protected void grdEditPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdEditPO.PageIndex = e.NewPageIndex;
            FillGridView(Session["PO_UserName"].ToString());
        }

        protected void grdEditPO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
           
            //if (e.CommandName == "Print")
            //{
            //    GridViewRow grdrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //    int intPONumber = Convert.ToInt32(grdrow.Cells[2].Text);
            //    Response.Redirect("PrintPO.aspx?EP=" + intPONumber);
            //}

            if (e.CommandName == "")
            {
                GridViewRow grdrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnkPOMainID = (LinkButton)grdrow.Cells[0].FindControl("lnkPOMainID");
                Int64 poMainID = Convert.ToInt64(lnkPOMainID.CommandArgument.ToString());
                Response.Redirect("ApprovalStatus.aspx?ID=" + poMainID);
            }
        
        }

        protected void grdEditPO_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Int64 editPONo;
           
            GridViewRow row = grdEditPO.Rows[e.NewEditIndex];
            LinkButton lnkPOMainID = (LinkButton)row.Cells[1].FindControl("lnkPOMainID");
            //UIUtility.TryParseDakaKeyValue(ref grdEditPO, row.RowIndex, "POMainID", out editPONo);

            //editPONo = Convert.ToInt64(grdEditPO.Rows[e.NewEditIndex].Cells[1].Text);
            editPONo = Convert.ToInt64(lnkPOMainID.Text);
            Response.Redirect("NewPO.aspx?E=" + editPONo);            
        }

        private void FillGridView(string userName)
        {
            grdEditPO.DataSource = new POMain().FillEditPO(userName);
            grdEditPO.DataBind();
        }

        private void FillGrid(Int64 poID)
        {
            DataSet dsPOData = new DataSet();
            //if (sessionUserID == "bpriyam" || sessionUserID == "jgarte" || sessionUserID == "atuccinardi" || sessionUserID == "jfoley" || sessionUserID == "scummings" || sessionUserID == "jbellini")
            //{
                dsPOData = new POMain().BindPOData(poID);
            //}

            //else 

            grdEditPO.DataSource = dsPOData;
            grdEditPO.DataBind();
        }
       
    }
}