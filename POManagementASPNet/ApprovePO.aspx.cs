using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using BAL;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Configuration;

namespace POManagementASPNet
{
    public partial class ApprovePO : System.Web.UI.Page
    {
        private Employee loggedInUser = null;
        string userName;
        string userEmail;        
        int intApproved = 0;
        object userID;
        //string userID;
     
        protected void Page_Load(object sender, EventArgs e)
        {
            userName = Session["PO_UserName"].ToString();
            
            userEmail = Session["UserEmail"].ToString();
            loggedInUser = (Employee)Session["Employee"];            
               

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
            liApprove.Attributes.Add("class", "active");
            liClose.Attributes.Add("class", "");
            liAdmin.Attributes.Add("class", "");
            liReport.Attributes.Add("class", "");

            if (!loggedInUser.HasEmployeeAccess(Roles.CEO) && !loggedInUser.HasEmployeeAccess(Roles.CFO) && !loggedInUser.HasEmployeeAccess(Roles.COO) && !loggedInUser.HasEmployeeAccess(Roles.HOD)
                && !loggedInUser.HasEmployeeAccess(Roles.Supervisor) && !loggedInUser.HasEmployeeAccess(Roles.PORequester) && !loggedInUser.HasEmployeeAccess(Roles.Admin) && !loggedInUser.HasEmployeeAccess(Roles.DeputyApprover)) /*&& !loggedInUser.HasEmployeeAccess(Roles.PreApprover))*/
            {
                Response.Redirect("Login.aspx?resign=true");
            }
            if (loggedInUser == null)
            {
                Response.Redirect("Login.aspx?resign=true");
            }

            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["E"]))
                {
                    if (!loggedInUser.HasEmployeeAccess(Roles.PORequester))
                    {
                        string queryStringE = Convert.ToString(Request["E"]);
                        Int64 poID = Convert.ToInt64(queryStringE);
                        DataSet dsPO = new POMain().BindPOData(poID);
                        double totalValueUSD = Convert.ToDouble(dsPO.Tables[0].Rows[0]["TotalValueUSD"]);
                      
                        int dept = Convert.ToInt32(dsPO.Tables[0].Rows[0]["Department"]);
                        string status1 = dsPO.Tables[0].Rows[0]["Status1"].ToString();
                        string status = dsPO.Tables[0].Rows[0]["Status"].ToString();
                        string supervisor = dsPO.Tables[0].Rows[0]["Supervisor"].ToString();
                        int poType = Convert.ToInt32(dsPO.Tables[0].Rows[0]["POType"]);
                        int shipTo = Convert.ToInt32(dsPO.Tables[0].Rows[0]["ShipTo"]);


                        //for SD Supervisor
                        if (totalValueUSD < 25001 && status == "Pending" && supervisor == userName && loggedInUser.HasEmployeeAccess(Roles.Supervisor) && loggedInUser.IsServiceDeliveryGroup && (poType!=3 || poType!=4 || poType!=5 ))
                        {
                            FillGrid(poID);
                        }

                        //for NON  SD Supervisor
                        if (totalValueUSD < 25001 && status == "Pending" && supervisor == userName && loggedInUser.HasEmployeeAccess(Roles.Supervisor) && !loggedInUser.IsServiceDeliveryGroup && shipTo != 75 && shipTo != 76 && shipTo != 77)
                        {
                            FillGrid(poID);
                        }

                        // for Engineering - Media
                        if ((dept == 6 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName && (poType != 3 || poType != 4 || poType != 5)) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(6))
                        {
                            FillGrid(poID);
                        }

                        // for MOP - Media Ops
                        if ((dept == 8 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName && (poType != 3 || poType != 4 || poType != 5)) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(8))
                        {
                            FillGrid(poID);
                        }

                        // for IT - IT
                        if ((dept == 2 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName ) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(2))
                        {
                            FillGrid(poID);
                        }

                        // for ENG - Engineering
                        if ((dept == 9 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName && (poType != 3 || poType != 4 || poType != 5)) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(9))
                        {
                            FillGrid(poID);
                        }

                        // for NPL – Network Planning
                        if ((dept == 12  && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName)  && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(12))
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept SVC - Provisioning
                        if ((dept == 1 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName && (poType != 3 || poType != 4 || poType != 5)) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(1))
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept NOP - Network Ops
                        if ((dept == 10 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName && (poType != 3 || poType != 4 || poType != 5)) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(10))
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept WPM - Wet Plant & Marine
                        if ((dept == 23 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName && (poType != 3 || poType != 4 || poType != 5)) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(1))  
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept MKT - Marketing
                        if ((dept == 18 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(18)) 
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept PRD - Product
                        if ((dept == 21 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(21))
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept SEN - Sales Engineering
                        if ((dept == 22 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(22))
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept FIN - Finance
                        if ((dept == 3 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(3))
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept SLF - Finance Sales
                        if ((dept == 13 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(13))
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept LEG - Legal
                        if ((dept == 14 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(14))
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept BDV - Business Development
                        if (((dept == 15 && status == "Pending") || (status == "SupApproved" && (poType!=3 || poType!=4 || poType!= 5) || status == "HODApproved") && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(15)))
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept EMT - Executive Mgmt
                        if ((dept == 16 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(16))
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept HR - HR
                        if ((dept == 17 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(17))
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept MSL - Media Sales
                        if ((dept == 19 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(19))
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept PDV - Project Development
                        if ((dept == 20 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(20))
                        {
                            FillGrid(poID);
                        }

                        // for HOD for Dept SLW - Wholesale Sales
                        if ((dept == 24 && status == "Pending") || (totalValueUSD < 25001 && status == "Pending" && supervisor == userName) && loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(24))
                        {
                            FillGrid(poID);
                        }                       

                        //for level 3 approval
                        if (totalValueUSD > 100000  && status == "PreApproved" && loggedInUser.HasEmployeeAccess(Roles.CFO)) 
                        {
                            FillGrid(poID);
                        }                               

                        //for level 4, CEO
                        if (totalValueUSD > 250000  && status == "CFOApproved"  && (loggedInUser.HasEmployeeAccess(Roles.CEO)))
                        {
                            FillGrid(poID);
                        }                      

                        //For Admin we need to do the code
                    }

                    else
                        lblMessage.Text = "You are not authorized to view this page";
                }

                else if (loggedInUser.HasEmployeeAccess(Roles.CEO))
                {
                    grdApprovePO.DataSource = new POMain().GetPOForCEOApproval();
                    grdApprovePO.DataBind();
                }

                else if (loggedInUser.HasEmployeeAccess(Roles.COO))
                {
                    grdApprovePO.DataSource = new POMain().GetPOForCOOApproval();
                    grdApprovePO.DataBind();
                }

                else if (loggedInUser.HasEmployeeAccess(Roles.CFO))
                {
                    grdApprovePO.DataSource = new POMain().GetPOForCFOApproval();
                    grdApprovePO.DataBind();
                }

                //For PreApprover(Jeff)
                else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(15))
                {
                    string depts = string.Empty;
                    foreach (var objEmp in loggedInUser.EmployeeRoles)
                    {
                        depts += objEmp.DeptID + ","; // getting depts they have under 
                    }

                    if (depts != "")
                        depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                    grdApprovePO.DataSource = new POMain().GetPOForPreApproval(depts, userName);
                    grdApprovePO.DataBind();
                }

                else if (loggedInUser.HasEmployeeAccess(Roles.DeputyApprover))
                {
                    grdApprovePO.DataSource = new POMain().GetPOForDeputyApproval(userName);
                    grdApprovePO.DataBind();
                }

                //For Tail/Circuit/Cross-Connect PO HOD 
                else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(12))
                {
                    string depts = string.Empty;
                    foreach (var objEmp in loggedInUser.EmployeeRoles)
                    {
                        depts += objEmp.DeptID + ","; // getting depts they have under 
                    }

                    if (depts != "")
                        depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                    grdApprovePO.DataSource = new POMain().GetPOForTailCircuitCrossHODApproval(depts, userName);
                    grdApprovePO.DataBind();
                }

                else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && !loggedInUser.IsServiceDeliveryGroup)
                {
                    string depts = string.Empty;
                    foreach (var objEmp in loggedInUser.EmployeeRoles)
                    {
                        depts += objEmp.DeptID + ","; // getting depts they have under 
                    }

                    if (depts != "")
                        depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                    grdApprovePO.DataSource = new POMain().GetPOForHODApproval(depts, userName);
                    grdApprovePO.DataBind();
                }

                //for SD HOD 
                else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsServiceDeliveryGroup)
                {
                    string depts = string.Empty;
                    foreach (var objEmp in loggedInUser.EmployeeRoles)
                    {
                        depts += objEmp.DeptID + ","; // getting depts they have under 
                    }

                    if (depts != "")
                        depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                    grdApprovePO.DataSource = new POMain().GetPOForSDHODApproval(depts, userName);
                    grdApprovePO.DataBind();
                }


                else if (loggedInUser.HasEmployeeAccess(Roles.Supervisor) && loggedInUser.IsServiceDeliveryGroup)
                {
                    grdApprovePO.DataSource = new POMain().GetPOForSDMgrApproval(userName);
                    grdApprovePO.DataBind();
                }

                //For Non service delivery group manager
                else if (loggedInUser.HasEmployeeAccess(Roles.Supervisor) && !loggedInUser.IsServiceDeliveryGroup)
                {
                    grdApprovePO.DataSource = new POMain().GetPOFORNONSDMgrApproval(userName);
                    grdApprovePO.DataBind();
                }

                else if (loggedInUser.HasEmployeeAccess(Roles.Admin))
                {
                    grdApprovePO.DataSource = new POMain().GetPOForAdminApproval();
                    grdApprovePO.DataBind();
                }

                else if (loggedInUser.HasEmployeeAccess(Roles.PORequester))
                {
                    lblMessage.Text = "You are not authorized to view this page";
                }   
            }
        }

        protected void grdApprovePO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdApprovePO.PageIndex = e.NewPageIndex;           

            if (loggedInUser.HasEmployeeAccess(Roles.CFO))
            {
                grdApprovePO.DataSource = new POMain().GetPOForCFOApproval();
                grdApprovePO.DataBind();
            }

            else if (loggedInUser.HasEmployeeAccess(Roles.COO))
            {
                grdApprovePO.DataSource = new POMain().GetPOForCOOApproval();
                grdApprovePO.DataBind();
            }

            else if (loggedInUser.HasEmployeeAccess(Roles.CEO))
            {
                grdApprovePO.DataSource = new POMain().GetPOForCEOApproval();
                grdApprovePO.DataBind();
            }           

            //For PreApprover(Jeff)
            else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(15))
            {
                string depts = string.Empty;
                foreach (var objEmp in loggedInUser.EmployeeRoles)
                {
                    depts += objEmp.DeptID + ","; // getting depts they have under 
                }

                if (depts != "")
                    depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                grdApprovePO.DataSource = new POMain().GetPOForPreApproval(depts, userName);
                grdApprovePO.DataBind();
            }

            //For Tail/Circuit/Cross-Connect PO HOD 
            else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(12))
            {
                string depts = string.Empty;
                foreach (var objEmp in loggedInUser.EmployeeRoles)
                {
                    depts += objEmp.DeptID + ","; // getting depts they have under 
                }

                if (depts != "")
                    depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                grdApprovePO.DataSource = new POMain().GetPOForTailCircuitCrossHODApproval(depts, userName);
                grdApprovePO.DataBind();
            } 

            else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && !loggedInUser.IsServiceDeliveryGroup) 
            {
                string depts = string.Empty;
                foreach (var objEmp in loggedInUser.EmployeeRoles)
                {
                    depts += objEmp.DeptID + ","; // getting depts they have under 
                }

                if (depts != "")
                    depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                grdApprovePO.DataSource = new POMain().GetPOForHODApproval(depts, userName);
                grdApprovePO.DataBind();
            }

            //for SD HOD 
            else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsServiceDeliveryGroup)
            {
                string depts = string.Empty;
                foreach (var objEmp in loggedInUser.EmployeeRoles)
                {
                    depts += objEmp.DeptID + ","; // getting depts they have under 
                }

                if (depts != "")
                    depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                grdApprovePO.DataSource = new POMain().GetPOForSDHODApproval(depts, userName);
                grdApprovePO.DataBind();
            }

            else if (loggedInUser.HasEmployeeAccess(Roles.Supervisor) && loggedInUser.IsServiceDeliveryGroup)
            {
                grdApprovePO.DataSource = new POMain().GetPOForSDMgrApproval(userName);
                grdApprovePO.DataBind();
            }

            //For Non service delivery group manager
            else if (loggedInUser.HasEmployeeAccess(Roles.Supervisor) && !loggedInUser.IsServiceDeliveryGroup)
            {
                grdApprovePO.DataSource = new POMain().GetPOFORNONSDMgrApproval(userName);
                grdApprovePO.DataBind();
            }

            else if (loggedInUser.HasEmployeeAccess(Roles.Admin))
            {
                grdApprovePO.DataSource = new POMain().GetPOForAdminApproval();
                grdApprovePO.DataBind();
            }
        }

        protected void grdApprovePO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            double poValueUSD =0;
            string depts = string.Empty;          

            try
            { 
                if (e.CommandName == "")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    LinkButton lnkPOMainID = (LinkButton)row.Cells[1].FindControl("lnkPOMainID");
                    Int64 poMainID = Convert.ToInt64(lnkPOMainID.CommandArgument.ToString());
                    Response.Redirect("ApprovalStatus.aspx?ID=" + poMainID);
                }

                if (e.CommandName == "View")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    LinkButton lnkPOMainID = (LinkButton)row.Cells[1].FindControl("lnkPOMainID");                   
                    Int64 intPONumber = Convert.ToInt64(lnkPOMainID.CommandArgument.ToString());
                    Response.Redirect("NewPO.aspx?CV=" + intPONumber);
                }              

                if (e.CommandName == "Select")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    int index = Convert.ToInt32(e.CommandArgument.ToString());
                    grdApprovePO.EditIndex = index;                    

                    if (loggedInUser.HasEmployeeAccess(Roles.CFO))
                    {
                        grdApprovePO.DataSource = new POMain().GetPOForCFOApproval();
                        grdApprovePO.DataBind();
                    }

                    else if (loggedInUser.HasEmployeeAccess(Roles.COO))
                    {
                        grdApprovePO.DataSource = new POMain().GetPOForCOOApproval();
                        grdApprovePO.DataBind();
                    }

                    else if (loggedInUser.HasEmployeeAccess(Roles.CEO))
                    {
                        grdApprovePO.DataSource = new POMain().GetPOForCEOApproval();
                        grdApprovePO.DataBind();
                    }

                    //for PreApprover(Jeff)
                    else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(15))
                    {
                        foreach (var objEmp in loggedInUser.EmployeeRoles)
                        {
                            depts += objEmp.DeptID + ","; // getting depts they have under 
                        }

                        if (depts != "")
                            depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                        grdApprovePO.DataSource = new POMain().GetPOForPreApproval(depts, userName);
                        grdApprovePO.DataBind();
                    }

                    //for Joseph
                    else if (loggedInUser.HasEmployeeAccess(Roles.DeputyApprover))
                    {
                        grdApprovePO.DataSource = new POMain().GetPOForDeputyApproval(userName);
                        grdApprovePO.DataBind();
                    }


                    //For Tail/Circuit/Cross-Connect PO HOD 
                    else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(12))
                    {
                        foreach (var objEmp in loggedInUser.EmployeeRoles)
                        {
                            depts += objEmp.DeptID + ","; // getting depts they have under 
                        }

                        if (depts != "")
                            depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                        grdApprovePO.DataSource = new POMain().GetPOForTailCircuitCrossHODApproval(depts, userName);
                        grdApprovePO.DataBind();
                    }

                    else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && !loggedInUser.IsServiceDeliveryGroup /* && !loggedInUser.IsEmployeeHOD(15) && !loggedInUser.IsEmployeeHOD(3) && !loggedInUser.IsEmployeeHOD(16) */ )
                    {
                        foreach (var objEmp in loggedInUser.EmployeeRoles)
                        {
                            depts += objEmp.DeptID + ","; // getting depts they have under 
                        }

                        if (depts != "")
                            depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                        grdApprovePO.DataSource = new POMain().GetPOForHODApproval(depts, userName);
                        grdApprovePO.DataBind();
                    }

                    //for SD HOD 
                    else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsServiceDeliveryGroup)
                    {
                        foreach (var objEmp in loggedInUser.EmployeeRoles)
                        {
                            depts += objEmp.DeptID + ","; // getting depts they have under 
                        }

                        if (depts != "")
                            depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                        grdApprovePO.DataSource = new POMain().GetPOForSDHODApproval(depts, userName);
                        grdApprovePO.DataBind();
                    }

                    else if (loggedInUser.HasEmployeeAccess(Roles.Supervisor) && loggedInUser.IsServiceDeliveryGroup)
                    {
                        grdApprovePO.DataSource = new POMain().GetPOForSDMgrApproval(userName);
                        grdApprovePO.DataBind();
                    }

                    //For Non service delivery group manager
                    else if (loggedInUser.HasEmployeeAccess(Roles.Supervisor) && !loggedInUser.IsServiceDeliveryGroup)
                    {
                        grdApprovePO.DataSource = new POMain().GetPOFORNONSDMgrApproval(userName);
                        grdApprovePO.DataBind();
                    }
                }

                if (e.CommandName == "Approve")
                {
                    string status1 = string.Empty;
                    int status = 0;
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    int index = Convert.ToInt32(e.CommandArgument.ToString());

                    LinkButton lnkPOMainID = (LinkButton)row.FindControl("lnkPOMainID");                 
                    Int64 intApprovingPONumber = Convert.ToInt64(lnkPOMainID.CommandArgument.ToString());

                    Label lblTotalValueUSD = (Label)row.FindControl("lblTotalValueUSD");                   
                    poValueUSD = Convert.ToDouble(lblTotalValueUSD.Text);

                    TextBox txtComment = (TextBox)row.FindControl("txtComment");                

                    Label lblStatus = (Label)row.FindControl("lblStatus");                
                    string poStatus = lblStatus.Text;                

                    int project = Convert.ToInt32(grdApprovePO.DataKeys[row.RowIndex]["Project"]);
                    string createdBy = grdApprovePO.DataKeys[row.RowIndex]["CreatedBy"].ToString();

                    Label lblType = (Label)row.FindControl("lblTypeDescription");                  
                    string poType = lblType.Text;
                                     
                    //For PreApprover(Jeff)
                    if (loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(15))
                    { 
                        if (poValueUSD < 25001)
                        {
                            status = new POMain().UpdatePOToApprove(intApprovingPONumber, status1,txtComment.Text, userName, poValueUSD);
                            if (poType == "Cross Connect Order" || poType == "Tail Order" || poType == "Circuit Order")
                                intApproved = 14;

                            //else if (project == 175 || project == 221)                           
                            //    intApproved = 11;
                            else
                                intApproved = 10;                           
                        }

                        //code for PO value betn 25001 & 100000
                        if (poValueUSD > 25000 && poValueUSD < 100001)
                        {
                            status = new POMain().UpdatePOToApprove(intApprovingPONumber, status1,txtComment.Text, userName, poValueUSD);
                            if (poType == "Cross Connect Order" || poType == "Tail Order" || poType == "Circuit Order")
                                //intApproved = 16; 
                                intApproved = 14;
                            else
                                intApproved = 11;                          
                        }

                        if (poValueUSD > 100000 )
                        {
                            status = new POMain().UpdatePOToPreApprove(intApprovingPONumber, txtComment.Text, userName);
                            intApproved = 3;                           
                        }

                        foreach (var objEmp in loggedInUser.EmployeeRoles)
                        {
                            depts += objEmp.DeptID + ","; // getting depts they have under 
                        }

                        if (depts != "")
                            depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                        grdApprovePO.DataSource = new POMain().GetPOForPreApproval(depts, userName);
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }

                    //for Joseph
                    else if (loggedInUser.HasEmployeeAccess(Roles.DeputyApprover))
                    {
                        status = new POMain().UpdatePOToApprove(intApprovingPONumber, status1, txtComment.Text, userName, poValueUSD);

                        if (poType == "Cross Connect Order" || poType == "Tail Order" || poType == "Circuit Order")
                            intApproved = 14;
                        else
                            intApproved = 10;     

                        grdApprovePO.DataSource = new POMain().GetPOForDeputyApproval(userName);
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();

                    }

                    //For Tail/Circuit/Cross-Connect PO HOD  
                    else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(12))
                    {
                        //code for PO value <25001
                        if (poValueUSD < 25001)
                        {
                            if (poStatus == "SupApproved")
                            {
                                status = new POMain().UpdatePOToHODApproved(intApprovingPONumber, txtComment.Text, userName);
                                intApproved = 13;
                            }
                            else
                            {
                                if (poType == "Cross Connect Order" || poType == "Tail Order" || poType == "Circuit Order")
                                {
                                    status = new POMain().UpdatePOToHODApproved(intApprovingPONumber, txtComment.Text, userName);
                                    intApproved = 12;
                                }
                                else
                                {
                                    status = new POMain().UpdatePOToSupApprove(intApprovingPONumber, status1, txtComment.Text, userName);
                                    intApproved = 12;
                                }
                            }
                        }

                        //code for PO value greater than 25001 
                        if (poValueUSD > 25000)
                        {
                            status = new POMain().UpdatePOToHODApproved(intApprovingPONumber, txtComment.Text, userName);
                            if (poStatus == "SupApproved") 
                                intApproved = 15;
                            else
                                intApproved = 2;
                        }

                        foreach (var objEmp in loggedInUser.EmployeeRoles)
                        {
                            depts += objEmp.DeptID + ","; // getting depts they have under 
                        }

                        if (depts != "")
                            depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                        grdApprovePO.DataSource = new POMain().GetPOForTailCircuitCrossHODApproval(depts, userName);
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }

                    
                    //For Non SD HOD approval
                    else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && !loggedInUser.IsServiceDeliveryGroup)
                    {
                        if (poType == "Cross Connect Order" || poType == "Tail Order" || poType == "Circuit Order")
                        {
                            status = new POMain().UpdatePOToSupApprove(intApprovingPONumber, status1, txtComment.Text, userName);
                            intApproved = 5;
                        }

                        else
                        {
                            //code for PO value <25001
                            if (poValueUSD < 25001)
                            {
                                status = new POMain().UpdatePOToSupApprove(intApprovingPONumber, status1, txtComment.Text, userName);
                                intApproved = 1;                              
                            }

                            //code for PO value greater than 25001 
                            if (poValueUSD > 25000)
                            {
                                status = new POMain().UpdatePOToHODApproved(intApprovingPONumber, txtComment.Text, userName);
                                intApproved = 2;                               
                            }
                        }

                        foreach (var objEmp in loggedInUser.EmployeeRoles)
                        {
                            depts += objEmp.DeptID + ","; // getting depts they have under 
                        }

                        if (depts != "")
                            depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                        grdApprovePO.DataSource = new POMain().GetPOForHODApproval(depts, userName);
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }


                    //For SD HOD Approval
                    else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsServiceDeliveryGroup)
                    {  
                        //code to approve NPL PO raised by Non-Service delivery member under Service delivery group HOD 
                        if (poType == "Cross Connect Order" || poType == "Tail Order" || poType == "Circuit Order")
                        {
                            status = new POMain().UpdatePOToSupApprove(intApprovingPONumber, status1, txtComment.Text, userName);
                            intApproved = 5;
                        }

                        else
                        {
                            //code for PO value <25001                        
                            if (poValueUSD < 25001)
                            {
                                status = new POMain().UpdatePOToSupApprove(intApprovingPONumber, status1, txtComment.Text, userName);
                                intApproved = 1;
                            }

                            //code for PO value greater than 25001 
                            if (poValueUSD > 25000)
                            {
                                status = new POMain().UpdatePOToHODApproved(intApprovingPONumber, txtComment.Text, userName);
                                intApproved = 2;
                            }
                        }

                        foreach (var objEmp in loggedInUser.EmployeeRoles)
                        {
                            depts += objEmp.DeptID + ","; // getting depts they have under 
                        }

                        if (depts != "")
                            depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                        grdApprovePO.DataSource = new POMain().GetPOForSDHODApproval(depts, userName);
                        grdApprovePO.EditIndex = -1; 
                        grdApprovePO.DataBind();                        
                    }    

                    else if (loggedInUser.HasEmployeeAccess(Roles.CFO))
                    {
                        status1 = "CFOApproved";

                        if (poValueUSD < 25001)
                        {
                            status = new POMain().UpdatePOToSupApprove(intApprovingPONumber, status1, txtComment.Text, userName);
                            intApproved = 1;
                        }

                        if (poValueUSD > 25001 && poValueUSD < 100001)
                        {
                            status = new POMain().UpdatePOToHODApproved(intApprovingPONumber, txtComment.Text, userName);                         
                            intApproved = 2;
                        }

                        else if (poValueUSD > 100000 && poValueUSD < 250001)
                        {
                            if (poStatus == "PreApproved")
                            {
                                status = new POMain().UpdatePOToApprove(intApprovingPONumber, null, txtComment.Text, userName,poValueUSD);
                                intApproved = 6;
                            }

                            else
                            {
                                status = new POMain().UpdatePOToHODApproved(intApprovingPONumber, txtComment.Text, userName);
                                intApproved = 2;
                            }
                        }

                        else if (poValueUSD > 250000)
                        {
                            status = new POMain().UpdatePOFor5thLevelApprovalForCFOCOO(intApprovingPONumber, status1, txtComment.Text, userName);                           
                            intApproved = 9;   
                        }

                        grdApprovePO.DataSource = new POMain().GetPOForCFOApproval();
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }

                    else if (loggedInUser.HasEmployeeAccess(Roles.COO))
                    {
                        status1 = "COOApproved";
                        if (poValueUSD > 100000 && poValueUSD < 250001)
                        {
                            status = new POMain().UpdatePOToApprove(intApprovingPONumber, status1,txtComment.Text, userName, poValueUSD);                           
                            intApproved = 4;
                        }

                        //else if (poValueUSD > 100000 && poValueUSD < 250001)
                        //{
                        //    status = new POMain().UpdatePOFor4thLevelApproval(intApprovingPONumber, status1, userName);
                           
                        //    if (gridStatus1 != "")
                        //        intApproved = 5;
                        //    else
                        //        intApproved = 7;
                        //}

                        else if (poValueUSD > 250000)
                        {
                            status = new POMain().UpdatePOFor5thLevelApprovalForCFOCOO(intApprovingPONumber, status1, txtComment.Text, userName);
                          
                            intApproved = 9;
                        }

                        grdApprovePO.DataSource = new POMain().GetPOForCOOApproval();
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }

                    else if (loggedInUser.HasEmployeeAccess(Roles.CEO))
                    {
                        status1 = "CEOApproved";
                        if (poValueUSD < 25001 )
                        {
                            status = new POMain().UpdatePOToSupApprove(intApprovingPONumber, null, txtComment.Text, userName);//UpdatePOToApprove(intApprovingPONumber, status1, txtComment.Text,userName);                        
                            intApproved = 1;
                        }

                        else if (poValueUSD > 25000 && poValueUSD < 250001)
                        {
                            status = new POMain().UpdatePOToHODApproved(intApprovingPONumber, txtComment.Text, userName);//UpdatePOFor4thLevelApproval(intApprovingPONumber, status1, userName);                          
                            intApproved = 2;
                        }

                        else if (poValueUSD > 250000)
                        {
                            status = new POMain().UpdatePOToApprove(intApprovingPONumber, status1,txtComment.Text, userName, poValueUSD);
                          
                            intApproved = 4;
                        }

                        grdApprovePO.DataSource = new POMain().GetPOForCEOApproval();
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }                              

                    else if (loggedInUser.HasEmployeeAccess(Roles.Supervisor) && loggedInUser.IsServiceDeliveryGroup)
                    {                        
                        status = new POMain().UpdatePOToSupApprove(intApprovingPONumber, status1,txtComment.Text, userName);
                        intApproved = 1;
                        grdApprovePO.DataSource = new POMain().GetPOForSDMgrApproval(userName);
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();                        
                    }

                    //For Non Service delivery Manager 
                    else if (loggedInUser.HasEmployeeAccess(Roles.Supervisor) && !loggedInUser.IsServiceDeliveryGroup)
                    {
                        status = new POMain().UpdatePOToSupApprove(intApprovingPONumber, status1, txtComment.Text, userName);
                        if (poType == "Tail Order" || poType == "Cross Connect Order" || poType == "Circuit Order")
                            intApproved = 5;
                        else
                            intApproved = 1;
                        grdApprovePO.DataSource = new POMain().GetPOFORNONSDMgrApproval(userName);
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }

                    SendMail(intApprovingPONumber,intApproved);                                        
                }

                if (e.CommandName == "Reject")
                {   
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    TextBox txtComment = (TextBox)row.Cells[12].FindControl("txtComment");

                    LinkButton lnkPOMainID = (LinkButton)row.Cells[1].FindControl("lnkPOMainID");
                    Int64 intApprovingPONumber = Convert.ToInt64(lnkPOMainID.CommandArgument.ToString());
                    int status = new POMain().UpdatePOToReject(intApprovingPONumber,txtComment.Text, userName);                 
                    intApproved = 0;

                    //for PreApprover(Jeff)
                    if (loggedInUser.HasEmployeeAccess(Roles.HOD) &&  loggedInUser.IsEmployeeHOD(15))
                    {
                        foreach (var objEmp in loggedInUser.EmployeeRoles)
                        {
                            depts += objEmp.DeptID + ","; // getting depts they have under 
                        }

                        if (depts != "")
                            depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                        grdApprovePO.DataSource = new POMain().GetPOForPreApproval(depts, userName);
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }

                    //for Joseph
                    if (loggedInUser.HasEmployeeAccess(Roles.DeputyApprover))
                    {
                        grdApprovePO.DataSource = new POMain().GetPOForDeputyApproval(userName);
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }


                    //For Tail/Circuit/Cross-Connect PO HOD  
                    else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsEmployeeHOD(12))
                    {
                        foreach (var objEmp in loggedInUser.EmployeeRoles)
                        {
                            depts += objEmp.DeptID + ","; // getting depts they have under 
                        }

                        if (depts != "")
                            depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                        grdApprovePO.DataSource = new POMain().GetPOForTailCircuitCrossHODApproval(depts, userName);
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }

                    //if HOD is SD 
                    else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && loggedInUser.IsServiceDeliveryGroup)
                    {
                        foreach (var objEmp in loggedInUser.EmployeeRoles)
                        {
                            depts += objEmp.DeptID + ","; // getting depts they have under 
                        }

                        if (depts != "")
                            depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                        grdApprovePO.DataSource = new POMain().GetPOForSDHODApproval(depts, userName);
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }

                    //For Non SD HD 
                    else if (loggedInUser.HasEmployeeAccess(Roles.HOD) && !loggedInUser.IsServiceDeliveryGroup)
                    {
                        foreach (var objEmp in loggedInUser.EmployeeRoles)
                        {
                            depts += objEmp.DeptID + ","; // getting depts they have under 
                        }

                        if (depts != "")
                            depts = depts.ToString().Substring(0, depts.ToString().Length - 1);

                        grdApprovePO.DataSource = new POMain().GetPOForHODApproval(depts, userName);
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }

                    else if (loggedInUser.HasEmployeeAccess(Roles.CFO))
                    {
                        grdApprovePO.DataSource = new POMain().GetPOForCFOApproval();
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }

                    else if (loggedInUser.HasEmployeeAccess(Roles.COO))
                    {
                        grdApprovePO.DataSource = new POMain().GetPOForCOOApproval();
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }

                    else if (loggedInUser.HasEmployeeAccess(Roles.CEO))
                    {
                        grdApprovePO.DataSource = new POMain().GetPOForCEOApproval();
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }

                    else if (loggedInUser.HasEmployeeAccess(Roles.Supervisor) && loggedInUser.IsServiceDeliveryGroup)
                    {
                        grdApprovePO.DataSource = new POMain().GetPOForSDMgrApproval(userName);
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }

                    else if (loggedInUser.HasEmployeeAccess(Roles.Supervisor) && !loggedInUser.IsServiceDeliveryGroup)
                    {
                        grdApprovePO.DataSource = new POMain().GetPOFORNONSDMgrApproval(userName);
                        grdApprovePO.EditIndex = -1;
                        grdApprovePO.DataBind();
                    }

                    SendMail(intApprovingPONumber, intApproved);
                    
                }              
            }
            catch (Exception ex)
            { }
        }
     

        private void SendMail(Int64 poNumber, int intApproved)      
        {
            int mailNo = 0;
            DataSet dsPOData = new DataSet();
            dsPOData = new POMain().BindPOData(poNumber);           
            int dept = Convert.ToInt32(dsPOData.Tables[0].Rows[0]["Department"]);
            double totalValueUSD = Convert.ToDouble(dsPOData.Tables[0].Rows[0]["TotalValueUSD"]);

            DataSet dsTo = new DataSet();
            DataSet dsCC = new DataSet();
            DataSet dsCC1 = new DataSet();
            DataSet dsCC2 = new DataSet();

            string strPOItems = string.Empty;
            foreach (DataRow dr in dsPOData.Tables[1].Rows)
            {
                strPOItems += dr["Description Of Part"].ToString() + ',';
            }

            if (strPOItems != "")
                strPOItems = strPOItems.ToString().Substring(0, strPOItems.ToString().Length - 1);

            SmtpClient smtpClient = new SmtpClient("smtp.hiberniaatlantic.com");
            MailMessage mailMsg = new MailMessage();

            if(intApproved==1)           
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
                dsTo = new POMain().GetMailingListForPreDeputyApprover(totalValueUSD);                
                dsCC = GetManagerEmail(userID.ToString());
                dsCC1 = new POMain().GetEmployeeDetails(userID.ToString());
            }

            if (intApproved == 2)
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
                dsTo = new POMain().GetMailingListForPreDeputyApprover(totalValueUSD);   
                dsCC = new POMain().GetMailingListFor2ndLevelApproval(dept);
                dsCC1 = new POMain().GetEmployeeDetails(userID.ToString());
            }

            if (intApproved == 3)
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();

                dsTo = new POMain().GetMailIdForThirdLevelOfApproval(8, dept);

                //get HOD email id dept wise
                dsCC = new POMain().GetMailingListFor2ndLevelApproval(dept);
                dsCC1 = new POMain().GetMailingListForPreDeputyApprover(totalValueUSD);   
                dsCC2 = new POMain().GetEmployeeDetails(userID.ToString());
            }

            if (intApproved == 4 /*|| intApproved==5*/)
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
                dsTo = new POMain().GetEmployeeDetails(userID.ToString());
                dsCC = new POMain().GetMailIdForThirdLevelOfApproval(9, dept);
            }

            if (intApproved == 5)
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
                dsTo = new POMain().GetMailingListFor2ndLevelApproval(dept);//mail to anthony..shud be edited
                dsCC = GetManagerEmail(userID.ToString());
                dsCC1 = new POMain().GetEmployeeDetails(userID.ToString());
            }

            if (intApproved == 6)
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
                dsTo = new POMain().GetEmployeeDetails(userID.ToString());
                dsCC = new POMain().GetMailListForCC(intApproved, dept);
            }

            if (intApproved == 7 || intApproved == 8)
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
                dsTo = new POMain().GetMailIdForThirdLevelOfApproval(intApproved, dept);
                dsCC = new POMain().GetMailListForCC(intApproved, dept);
                dsCC1 = new POMain().GetEmployeeDetails(userID.ToString());
            }

            if (intApproved == 9)
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
                dsTo = new POMain().GetMailListForCC(8, dept);
                dsCC = new POMain().GetMailIdForThirdLevelOfApproval(10, dept);
                dsCC1 = new POMain().GetEmployeeDetails(userID.ToString());
            }

            if (intApproved == 10)
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
                dsTo = new POMain().GetEmployeeDetails(userID.ToString());
                dsCC = new POMain().GetMailingListForPreDeputyApprover(totalValueUSD);   
                dsCC1 = GetManagerEmail(userID.ToString());
            }

            if (intApproved == 11)
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
                dsTo = new POMain().GetEmployeeDetails(userID.ToString());
                dsCC = new POMain().GetMailingListForPreDeputyApprover(totalValueUSD);   
                dsCC1 = new POMain().GetMailingListFor2ndLevelApproval(dept);
            }

            if (intApproved == 13)
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
                dsTo = new POMain().GetMailingListForPreDeputyApprover(totalValueUSD);    
                dsCC2 = new POMain().GetMailingListFor2ndLevelApproval(dept);//mail to anthony..shud be edited
                dsCC = GetManagerEmail(userID.ToString());
                dsCC1 = new POMain().GetEmployeeDetails(userID.ToString());
            }

            if (intApproved == 14)
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
                dsTo = new POMain().GetEmployeeDetails(userID.ToString());
                dsCC1 = new POMain().GetMailingListForPreDeputyApprover(totalValueUSD);   
                dsCC2 = new POMain().GetMailingListFor2ndLevelApproval(dept);
                dsCC = GetManagerEmail(userID.ToString());               
            }          

            if (intApproved == 15)
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
                dsTo = new POMain().GetMailingListForPreDeputyApprover(totalValueUSD);
                dsCC = new POMain().GetMailingListFor2ndLevelApproval(dept);
                dsCC1 = new POMain().GetEmployeeDetails(userID.ToString());
                dsCC2 = GetManagerEmail(userID.ToString());
            }             

            //if (intApproved == 16)
            //{
            //    DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
            //    userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
            //    dsTo = new POMain().GetEmployeeDetails(userID.ToString());
            //    dsCC = new POMain().GetMailingListForFelipJeff();
            //    dsCC1 = new POMain().GetMailingListFor2ndLevelApproval(dept);
            //    dsCC2 = GetManagerEmail(userID.ToString());
            //}

            if (intApproved == 12)
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
                dsTo = new POMain().GetMailingListForPreDeputyApprover(totalValueUSD);
                dsCC = new POMain().GetEmployeeDetails(userID.ToString());               
                dsCC1 = new POMain().GetMailingListFor2ndLevelApproval(dept);
            }

            if (intApproved == 0)
            {
                DataSet dsUserID = new POMain().GetUserId(dsPOData.Tables[0].Rows[0]["RequesterName"].ToString());
                userID = dsUserID.Tables[0].Rows[0]["UserID"].ToString();
                dsTo = new POMain().GetEmployeeDetails(userID.ToString());
                mailMsg.CC.Add(userEmail);
            }
          
            foreach (DataRow dr in dsTo.Tables[0].Rows)
            {
                mailMsg.To.Add(new MailAddress(dr["mail"].ToString()));
            }

            if (dsCC.Tables.Count>0)
            {
                foreach (DataRow dr in dsCC.Tables[0].Rows)
                {
                    mailMsg.CC.Add(new MailAddress(dr["mail"].ToString()));                  
                }              
            }

            if (dsCC1.Tables.Count > 0)
            {
                foreach (DataRow dr in dsCC1.Tables[0].Rows)
                {
                    mailMsg.CC.Add(new MailAddress(dr["mail"].ToString()));
                }     
            }

            if (dsCC2.Tables.Count > 0)
            {
                foreach (DataRow dr in dsCC2.Tables[0].Rows)
                {
                    mailMsg.CC.Add(new MailAddress(dr["mail"].ToString()));
                }
            }

            mailMsg.Bcc.Add("Basuki.Priyam@hibernianetworks.com");

            if (intApproved == 6 || intApproved == 10 || intApproved == 11 || intApproved == 4 || intApproved == 14 || intApproved == 16)
           
                mailMsg.Subject = "PO: " + poNumber + "  is Approved by : " + userName;

            else if (intApproved == 3)
                mailMsg.Subject = "PO: " + poNumber + "  is Pre-Approved by : " + userName;

            else if (intApproved == 1 || intApproved == 5)
                mailMsg.Subject = "PO: " + poNumber + "  is Supervisor-Approved by : " + userName;

            else if (intApproved == 2 || intApproved == 12 || intApproved == 13 || intApproved == 15)
                mailMsg.Subject = "PO: " + poNumber + "  is HOD-Approved by : " + userName;

            else if ( intApproved==7 || intApproved==8 || intApproved==9)
                mailMsg.Subject = "PO: " + poNumber + "  is Partial-Approved by : " + userName;

            else if(intApproved==0)
                mailMsg.Subject = "PO: " + poNumber + "  is rejected by : " + userName;

            //string itemURL = "http://info.hiberniaatlantic.com:34104/ApprovePO.aspx?E=" + poNumber;
            string itemURL = ConfigurationManager.AppSettings["URL"].ToString() + "/ApprovePO.aspx?E=" + poNumber;

            string strBody = "PO Number : " + poNumber + "<br />Requester : " + dsPOData.Tables[0].Rows[0]["RequesterName"].ToString() + "<br />Station : " + dsPOData.Tables[0].Rows[0]["StationName"].ToString() + "<br />Ship To : " + dsPOData.Tables[0].Rows[0]["City"].ToString() + "<br />Supplier : " + dsPOData.Tables[0].Rows[0]["CompanyName"].ToString()
                     + "<br />Department : " + dsPOData.Tables[0].Rows[0]["DepartmentName"].ToString() + "<br />Status : " + dsPOData.Tables[0].Rows[0]["Status"] + "<br /> Items : " + strPOItems + "<br />Total Value : " + dsPOData.Tables[0].Rows[0]["CurrencyName"].ToString() + " " + Convert.ToDouble(dsPOData.Tables[0].Rows[0]["TotalValue"]).ToString("#,##0")
                     + "<br />NRC : " + Convert.ToDouble(dsPOData.Tables[0].Rows[0]["NRC"]).ToString("#,##0") + "<br />MRC : " + Convert.ToDouble(dsPOData.Tables[0].Rows[0]["MRC"]).ToString("#,##0") + "<br />Term : " + Convert.ToDouble(dsPOData.Tables[0].Rows[0]["Tail"]).ToString("#,##0") + "<br />Comment : " + dsPOData.Tables[0].Rows[0]["Comment"].ToString() + "<br />GMEE Opportunity ID : " + dsPOData.Tables[0].Rows[0]["Opportunity"].ToString();
            if (strBody.Length > 8000)
                strBody = strBody.Substring(0, 8000);

            StringBuilder message = new StringBuilder();
            message.Append("<table><tr><td><h3><a href='" + itemURL + "'>Purchase Order System</a></h3></td></tr><tr><td><h3>Date: " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h3></td></tr>" +
                "<tr><td>" + strBody + "</td></tr></table>");
            mailMsg.Body = message.ToString();
            mailMsg.IsBodyHtml = true;

            mailMsg.From = new MailAddress("noreply@hibernianetworks.com");

            if (dsTo.Tables[0].Rows.Count > 0)
                smtpClient.Send(mailMsg);
        }

        private DataSet GetManagerEmail(string name)
        {
            DataSet dsMgr = new POMain().GetManagerFromLDAP(name);
            DataSet dsEmail = new DataSet();

            string manager = dsMgr.Tables[0].Rows[0]["manager"].ToString();
            if (manager != "")
            {
                if (manager.Contains("CN="))
                {
                    int Length = manager.IndexOf(',');
                    manager = manager.Substring(3, Length - 3);
                    dsEmail = new POMain().GetManagerEmailFromLDAP(manager);
                }
                else
                {
                    manager = string.Empty;
                }
            }
            return dsEmail;
        }

        private void FillGrid(Int64 poID)
        {
            DataSet dsPOData = new DataSet();
            dsPOData = new POMain().BindPOData(poID);
            grdApprovePO.DataSource = dsPOData;
            grdApprovePO.DataBind();
        }     
    }
}