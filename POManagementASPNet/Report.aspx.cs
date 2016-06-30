using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace POManagementASPNet
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //iframe1.Attributes["src"] = "https://ssrs.hibernianetworks.com/ReportServer_SHAREPOINT/Pages/ReportViewer.aspx?%2fPOSystem%2fPOSummary&rs:Command=Render";
            iframe1.Attributes["src"] = ConfigurationManager.AppSettings["POSummary"].ToString();

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
            liClose.Attributes.Add("class", "");
            liAdmin.Attributes.Add("class", "");
            liReport.Attributes.Add("class", "active");
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedIndex == 0)
            {
                //iframe1.Attributes["src"] = "https://ssrs.hibernianetworks.com/ReportServer_SHAREPOINT/Pages/ReportViewer.aspx?%2fPOSystem%2fPOSummary&rs:Command=Render";
                iframe1.Attributes["src"] = ConfigurationManager.AppSettings["POSummary"].ToString();
                   
            }

            if (RadioButtonList1.SelectedIndex == 1)
            {
                //iframe1.Attributes["src"] = "https://ssrs.hibernianetworks.com/ReportServer_SHAREPOINT/Pages/ReportViewer.aspx?%2fPOSystem%2fPOMonth&rs:Command=Render";
                iframe1.Attributes["src"] = ConfigurationManager.AppSettings["POByMonth"].ToString();
            }

            if (RadioButtonList1.SelectedIndex == 2)
            {
                //iframe1.Attributes["src"] = "https://ssrs.hibernianetworks.com/ReportServer_SHAREPOINT/Pages/ReportViewer.aspx?%2fPOSystem%2fPOSupplier&rs:Command=Render";
                iframe1.Attributes["src"] = ConfigurationManager.AppSettings["POBySupplier"].ToString();
            }

            if (RadioButtonList1.SelectedIndex == 3)
            {
                //iframe1.Attributes["src"] = "https://ssrs.hibernianetworks.com/ReportServer_SHAREPOINT/Pages/ReportViewer.aspx?%2fPOSystem%2fPOCircuit&rs:Command=Render";
                iframe1.Attributes["src"] = ConfigurationManager.AppSettings["CircuitPO"].ToString();
            }

            if (RadioButtonList1.SelectedIndex == 4)
            {
                //iframe1.Attributes["src"] = "https://ssrs.hibernianetworks.com/ReportServer_SHAREPOINT/Pages/ReportViewer.aspx?%2fPOSystem%2fPODateWise&rs:Command=Render";
                iframe1.Attributes["src"] = ConfigurationManager.AppSettings["PODatewise"].ToString();
            }

            if (RadioButtonList1.SelectedIndex == 5)
            {
                //iframe1.Attributes["src"] = "https://ssrs.hibernianetworks.com/ReportServer_SHAREPOINT/Pages/ReportViewer.aspx?%2fPOSystem%2fPOBackOrder&rs:Command=Render";
                iframe1.Attributes["src"] = ConfigurationManager.AppSettings["BackOrder"].ToString();
            }

            if (RadioButtonList1.SelectedIndex == 6)
            {
                //iframe1.Attributes["src"] = "https://ssrs.hibernianetworks.com/ReportServer_SHAREPOINT/Pages/ReportViewer.aspx?%2fPOSystem%2fPODeptWise&rs:Command=Render";
                iframe1.Attributes["src"] = ConfigurationManager.AppSettings["PODeptWise"].ToString();
            }
        }
    }
}