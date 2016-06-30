using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAL;
using BAL;
using System.Web.UI.HtmlControls;

namespace POManagementASPNet
{
    public partial class Home : System.Web.UI.Page
    {
        private Employee loggedInUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                loggedInUser = (Employee)Session["Employee"];
                if (!loggedInUser.HasEmployeeAccess(Roles.PORequester) && !loggedInUser.HasEmployeeAccess(Roles.HOD) && !loggedInUser.HasEmployeeAccess(Roles.COO) && !loggedInUser.HasEmployeeAccess(Roles.CFO) 
                    && !loggedInUser.HasEmployeeAccess(Roles.CEO) && !loggedInUser.HasEmployeeAccess(Roles.Supervisor) && !loggedInUser.HasEmployeeAccess(Roles.Admin) && !loggedInUser.HasEmployeeAccess(Roles.DeputyApprover))
                {
                    Response.Redirect("Login.aspx?resign=true");
                }
                //Response.Redirect("NewPO.aspx");

                if (loggedInUser == null)
                {
                    Response.Redirect("Login.aspx?resign=true");
                }
                if (!this.IsPostBack)
                {
                    FillDepartment();
                    FillType();
                }

                HtmlGenericControl liHome = (HtmlGenericControl)this.Master.FindControl("liHome");
                HtmlGenericControl liNew = (HtmlGenericControl)this.Master.FindControl("liNew");
                HtmlGenericControl liEdit = (HtmlGenericControl)this.Master.FindControl("liEdit");
                HtmlGenericControl liApprove = (HtmlGenericControl)this.Master.FindControl("liApprove");
                HtmlGenericControl liClose = (HtmlGenericControl)this.Master.FindControl("liClose");
                HtmlGenericControl liAdmin = (HtmlGenericControl)this.Master.FindControl("liAdmin");
                HtmlGenericControl liReport = (HtmlGenericControl)this.Master.FindControl("liReport");
                liHome.Attributes.Add("class", "active");
                liNew.Attributes.Add("class", "");
                liEdit.Attributes.Add("class", "");
                liApprove.Attributes.Add("class", "");
                liClose.Attributes.Add("class", "");
                liAdmin.Attributes.Add("class", "");
                liReport.Attributes.Add("class", "");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        //protected override void OnInit(EventArgs e)
        //{
        //    try
        //    {
        //        base.OnInit(e);
        //        //Form.InnerHtml

        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //}

        private void FillDepartment()
        {
            DataSet dsDept = new POSystemDataHandler().GetDepartment();
            ddlDept.DataSource = dsDept;
            ddlDept.DataBind();

            ddlDept.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void FillType()
        {
            DataSet dsType = new POSystemDataHandler().GetType();
            ddlPOType.DataSource = dsType;
            ddlPOType.DataBind();

            ddlPOType.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void ddlPOType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlDept.SelectedValue != "0")
            {
                if (ddlPOType.SelectedIndex == 3 || ddlPOType.SelectedIndex == 4 || ddlPOType.SelectedIndex == 5)
                    ddlDept.SelectedValue = "12";
                Response.Redirect("NewPO.aspx?H=" + ddlDept.SelectedValue + "&J=" + ddlPOType.SelectedValue);
            }
        }
    }
}