using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.IO;
using DAL;
using BAL;
using Excel;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Services;
//using System.DirectoryServices;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace POManagementASPNet
{
    public partial class NewPO : System.Web.UI.Page
    {
        private Employee loggedInUser = null;      
        double grdItemsTotal = 0;        
        string userName;
        string userEmail;
        object userID;// this will be used when 4 users change Requester
        string sessionUserID;//added to use for logged in user
        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {               
                userName = Session["PO_UserName"].ToString();
                userEmail = Session["UserEmail"].ToString();
                sessionUserID= Session["UserID"].ToString();

                loggedInUser = (Employee)Session["Employee"];
                if (!loggedInUser.HasEmployeeAccess(Roles.PORequester) && !loggedInUser.HasEmployeeAccess(Roles.Supervisor) && !loggedInUser.HasEmployeeAccess(Roles.HOD) && !loggedInUser.HasEmployeeAccess(Roles.COO)
                    && !loggedInUser.HasEmployeeAccess(Roles.CFO) && !loggedInUser.HasEmployeeAccess(Roles.CEO) && !loggedInUser.HasEmployeeAccess(Roles.Admin) && !loggedInUser.HasEmployeeAccess(Roles.DeputyApprover))
                {
                    Response.Redirect("Login.aspx?resign=true");
                }

                if (loggedInUser == null)
                {
                    Response.Redirect("Login.aspx?resign=true");
                }

                if (sessionUserID == "bpriyam" || sessionUserID == "jgarte" || sessionUserID == "atuccinardi" || sessionUserID == "jfoley" || sessionUserID == "scummings" || sessionUserID == "jbellini")
                {
                    lblRequester.Visible = true;
                    drpRequester.Visible = true;
                }

                //ViewState["isEditingPO"] = false;

                HtmlGenericControl liHome = (HtmlGenericControl)this.Master.FindControl("liHome");
                HtmlGenericControl liNew = (HtmlGenericControl)this.Master.FindControl("liNew");
                HtmlGenericControl liEdit = (HtmlGenericControl)this.Master.FindControl("liEdit");
                HtmlGenericControl liApprove = (HtmlGenericControl)this.Master.FindControl("liApprove");
                HtmlGenericControl liClose = (HtmlGenericControl)this.Master.FindControl("liClose");
                HtmlGenericControl liAdmin = (HtmlGenericControl)this.Master.FindControl("liAdmin");
                HtmlGenericControl liReport = (HtmlGenericControl)this.Master.FindControl("liReport");
                liHome.Attributes.Add("class", "");
                liNew.Attributes.Add("class", "active");
                liEdit.Attributes.Add("class", "");
                liApprove.Attributes.Add("class", "");
                liClose.Attributes.Add("class", "");
                liAdmin.Attributes.Add("class", "");
                liReport.Attributes.Add("class", "");


                if (!this.IsPostBack)
                {
                    
                    GetRequester();                  
                    FillStation();
                    FillProject();
                    FillDepartment();               
                    FillType();
                    FillShipTo();
                    FillSuppliers();
                    FillBuggetedField();
                    FillCurrency();
                   // GetUserID();

                    ViewState["isEditingPO"] = false;

                    btnUpdatePO.Visible = false;                   

                    if (!string.IsNullOrEmpty(Request["H"]))
                    {
                        string queryStringE = Convert.ToString(Request["H"]);
                        string queryStringJ = Convert.ToString(Request["J"]);
                        drpDepartment.SelectedValue = queryStringE;
                        drpOrderClassification.Enabled = true;
                     
                        drpType.SelectedValue = queryStringJ;
                        if (drpType.SelectedValue == "1")
                        {
                            lblTypeDescText.Text = "When you select this option you are ordering an item that will become part of the Hibernia Infrastructure like a desk, hauwei card, network equipment etc.";
                        }

                        else if (drpType.SelectedValue == "2")
                        {
                            lblTypeDescText.Text = "When you select this option you are ordering an item that you are consuming ie. Tea or coffee, pens or electricity or you are buying a tail or cross connect either for a customer or for our network";
                            lblGlCode.Visible = true;
                            ddlGlCode.Visible = true;
                        }

                        else if (drpType.SelectedValue == "3")
                        {
                            lblTypeDescText.Text = "Select this option for any tail orders associated with a Hibernia Circuit sale, please add the opportunity ID in the comments box";                           
                            lblGlCode.Visible = true;
                            ddlGlCode.Visible = true;
                            ShowProvision();
                        }

                        else if (drpType.SelectedValue == "4")
                        {
                            lblTypeDescText.Text = "Select this option if you are purchasing a cross connect for a Hibernia circuit sales, please add the circuit ID to the comments box";                           
                            lblGlCode.Visible = true;
                            ddlGlCode.Visible = true;
                            ShowProvision();
                        }

                        else if (drpType.SelectedValue == "5")
                        {
                            lblTypeDescText.Text = "Select this option if you are purchasing DF or an IRU or a circuit for a vendor";                           
                            lblGlCode.Visible = true;
                            ddlGlCode.Visible = true;
                            ShowProvision();
                        }

                        FillOrderClassification(Convert.ToInt32(drpType.SelectedIndex));
                    }

                    if (!string.IsNullOrEmpty(Request["E"]))
                    {                        
                        btnUpdatePO.Visible = true;
                        btnSubmitPO.Visible = false;
                        btnDeletePO.Visible = true;
                        btnCancel.Visible = true;
                        string queryStringE = Convert.ToString(Request["E"]);
                        Int64 poID = Convert.ToInt64(queryStringE);
                        ViewState["EditPONumber"] = poID;
                        BindPOData(poID);
                        lblPOTitle.Text = "Editing PO: " + poID;
                        ViewState["isEditingPO"] = true;                        

                    }

                    if (!string.IsNullOrEmpty(Request["CV"]))
                    {
                        btnUpdatePO.Visible = true;
                        btnSubmitPO.Visible = false;
                        string queryStringE = Convert.ToString(Request["CV"]);
                        Int64 poID = Convert.ToInt64(queryStringE);
                        ViewState["EditPONumber"] = poID;
                        BindPOData(poID);
                        drpRequester.Enabled = false;
                        drpOrderClassification.Enabled = false;
                        drpStation.Enabled = false;
                        drpDepartment.Enabled = false;
                        drpType.Enabled = false;
                        drpProject.Enabled = false;                       
                        drpShipTo.Enabled = false;
                        drpSupplier.Enabled = false;
                        drpInBudget.Enabled = false;
                        drpCurrency.Enabled = false;
                        txtComment.Enabled = false;
                        txtMRC.Enabled = false;
                        txtNRC.Enabled = false;
                        txtTail.Enabled = false;
                        txtOpportunity.Enabled = false;
                        ddlBudgetCode.Enabled = false;
                        btnAddItemToPO.Visible = false;
                        lnkAddAnotherItem.Visible = false;
                        lblPOTitle.Text = "Viewing PO: " + poID;
                        
                        SetControlsReadonly(pnlAddItemsToPO, true);
                        ViewState["isEditingPO"] = null;
                    }
                }               
            }

            catch (Exception ex)
            {              
                Response.Write(ex.Message);
            }
        }

        private void BindPOData(Int64 poID)
        {     

            DataSet dsPOData = new DataSet();

            try
            {
                drpStation.Enabled = true;
                dsPOData = new POMain().BindPOData(poID);

                ViewState["ViewStateItems"] = dsPOData.Tables[1];

                drpRequester.SelectedValue = dsPOData.Tables[0].Rows[0]["Requester"].ToString(); 
                GetUserID();
                drpType.SelectedValue = dsPOData.Tables[0].Rows[0]["POType"].ToString();
                FillOrderClassification(Convert.ToInt32(drpType.SelectedValue));
                drpOrderClassification.SelectedValue = dsPOData.Tables[0].Rows[0]["OrderClassification"].ToString();
                drpStation.SelectedValue = dsPOData.Tables[0].Rows[0]["Station"].ToString();
                drpDepartment.SelectedValue = dsPOData.Tables[0].Rows[0]["Department"].ToString();            

                drpOrderClassification.Enabled = true;
                if (drpType.SelectedValue == "1")
                {
                    lblTypeDescText.Text = "When you select this option you are ordering an item that will become part of the Hibernia Infrastructure like a desk, hauwei card, network equipment etc.";
                    lblCircuitID.Visible = false;
                    txtCircuit.Visible = false;
                }
                else if (drpType.SelectedValue == "2")
                {
                    lblTypeDescText.Text = "When you select this option you are ordering an item that you are consuming ie. Tea or coffee, pens or electricity or you are buying a tail or cross connect either for a customer or for our network";
                    lblCircuitID.Visible = false;
                    txtCircuit.Visible = false;
                    lblGlCode.Visible = true;
                    ddlGlCode.Visible = true;
                    FillGLCode(Convert.ToInt32(drpOrderClassification.SelectedValue));
                }
                else if (drpType.SelectedValue == "3")
                {
                    lblTypeDescText.Text = "Select this option for any tail orders associated with a Hibernia Circuit sale, please add the opportunity ID in the comments box";
                  
                    ShowProvision();
                    lblGlCode.Visible = true;
                    ddlGlCode.Visible = true;
                    FillGLCode(Convert.ToInt32(drpOrderClassification.SelectedValue));
                }
                else if (drpType.SelectedValue == "4")
                {
                    lblTypeDescText.Text = "Select this option if you are purchasing a cross connect for a Hibernia circuit sales, please add the circuit ID to the comments box";
                   
                    ShowProvision();
                    lblGlCode.Visible = true;
                    ddlGlCode.Visible = true;
                    FillGLCode(Convert.ToInt32(drpOrderClassification.SelectedValue));
                }
                else if (drpType.SelectedValue == "5")
                {
                    lblTypeDescText.Text = "Select this option if you are purchasing DF or an IRU or a circuit for a vendor";
                 
                    ShowProvision();
                    lblGlCode.Visible = true;
                    ddlGlCode.Visible = true;
                    FillGLCode(Convert.ToInt32(drpOrderClassification.SelectedValue));
                }

                drpProject.SelectedValue = dsPOData.Tables[0].Rows[0]["Project"].ToString();                        
                drpShipTo.SelectedValue = dsPOData.Tables[0].Rows[0]["ShipTo"].ToString();
                drpSupplier.SelectedValue = dsPOData.Tables[0].Rows[0]["Supplier"].ToString();
                txtTerm.Text = dsPOData.Tables[0].Rows[0]["Term"].ToString();
                txtComment.Text = dsPOData.Tables[0].Rows[0]["Comment"].ToString();
                drpInBudget.SelectedValue = dsPOData.Tables[0].Rows[0]["InBudget"].ToString();
                drpCurrency.SelectedValue = dsPOData.Tables[0].Rows[0]["Currency"].ToString();
                txtNRC.NumberValue = dsPOData.Tables[0].Rows[0]["NRC"].ToString();
                txtMRC.NumberValue = dsPOData.Tables[0].Rows[0]["MRC"].ToString();
                txtTail.NumberValue = dsPOData.Tables[0].Rows[0]["Tail"].ToString();
                txtCircuit.Text = dsPOData.Tables[0].Rows[0]["CircuitID"].ToString();
                txtEstimatedDeliveryTime.Text = dsPOData.Tables[0].Rows[0]["EstimatedDeliveryTime"] != DBNull.Value ? Convert.ToDateTime(dsPOData.Tables[0].Rows[0]["EstimatedDeliveryTime"]).ToString("dd-MMM-yyyy") : null;
                txtOpportunity.Text = dsPOData.Tables[0].Rows[0]["Opportunity"].ToString();

               
                //if (drpProject.SelectedValue == "175" || drpProject.SelectedValue == "221")
                //{
                //    //if (drpShipTo.SelectedValue == "75" || drpShipTo.SelectedValue == "76" || drpShipTo.SelectedValue == "77")
                //    //{
                //    //drpDepartment.SelectedValue = "2";
                //    drpType.Items.FindByValue("3").Enabled = false;
                //    drpType.Items.FindByValue("4").Enabled = false;
                //    drpType.Items.FindByValue("5").Enabled = false;
                //}
               

                if (dsPOData.Tables[0].Rows[0]["BudgetCode"].ToString() != "0" && dsPOData.Tables[0].Rows[0]["BudgetCode"].ToString() != "")
                {
                    DataSet dsBudgetCode = new POMain().GetBudgetCodes(Convert.ToInt32(dsPOData.Tables[0].Rows[0]["Department"]), Convert.ToInt32(dsPOData.Tables[0].Rows[0]["OrderClassification"]), Convert.ToInt32(dsPOData.Tables[0].Rows[0]["Station"]), Convert.ToInt32(dsPOData.Tables[0].Rows[0]["Year"]));

                    if (dsBudgetCode.Tables.Count > 0 && dsBudgetCode.Tables[0].Rows.Count > 0)
                    {
                        lblBudgetCode.Visible = true;
                        ddlBudgetCode.Visible = true;
                        ddlBudgetCode.DataSource = dsBudgetCode;
                        ddlBudgetCode.DataBind();
                        ddlBudgetCode.SelectedValue = dsPOData.Tables[0].Rows[0]["BudgetCode"].ToString();
                        ddlBudgetCode.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }

                pnlAddItemsToPO.Visible = true;
                grdItems.DataSource = dsPOData.Tables[1];
                grdItems.DataBind();               
            }

            catch (Exception ex)
            { }
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpOrderClassification.Enabled = true;
            lblBudgetCode.Visible = false;
            ddlBudgetCode.Visible = false;

            if (drpType.SelectedIndex == 1)
            {
                lblTypeDescText.Text = "When you select this option you are ordering an item that will become part of the Hibernia Infrastructure like a desk, hauwei card, network equipment etc.";
                lblCircuitID.Visible = false;
                txtCircuit.Visible = false;
                lblGlCode.Visible = false;
                ddlGlCode.Visible = false;
                //lblTail.Visible = false;
                //txtTail.Visible = false;    
            }

            else if (drpType.SelectedIndex == 2)
            {
                lblTypeDescText.Text = "When you select this option you are ordering an item that you are consuming ie. Tea or coffee, pens or electricity or you are buying a tail or cross connect either for a customer or for our network";
                lblCircuitID.Visible = false;
                txtCircuit.Visible = false;
                lblGlCode.Visible = true;
                ddlGlCode.Visible = true;
                //lblTail.Visible = false;
                //txtTail.Visible = false;    
            }

            else if (drpType.SelectedValue == "3")
            {
                lblTypeDescText.Text = "Select this option for any tail orders associated with a Hibernia Circuit sale, please add the opportunity ID in the comments box";
              
                lblGlCode.Visible = true;
                ddlGlCode.Visible = true;
                drpDepartment.SelectedValue = "12";
                ShowProvision();
            }

            else if (drpType.SelectedValue == "4")
            {
                lblTypeDescText.Text = "Select this option if you are purchasing a cross connect for a Hibernia circuit sales, please add the circuit ID to the comments box";
               
                lblGlCode.Visible = true;
                ddlGlCode.Visible = true;
                drpDepartment.SelectedValue = "12";
                ShowProvision();
            }

            else if (drpType.SelectedValue == "5")
            {
                lblTypeDescText.Text = "Select this option if you are purchasing DF or an IRU or a circuit for a vendor";
               
                lblGlCode.Visible = true;
                ddlGlCode.Visible = true;
                drpDepartment.SelectedValue = "12";
                ShowProvision();
            }

            else
                lblTypeDescText.Text = null;

            FillOrderClassification(Convert.ToInt32(drpType.SelectedValue));
        }

        protected void drpSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            int supplierID = Convert.ToInt32(drpSupplier.SelectedValue);
            int term = new POSystemDataHandler().GetTerm(supplierID);
            txtTerm.Text = term.ToString();
        }

        protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpStation.SelectedIndex = 0;
            ddlBudgetCode.Visible = false;
            lblBudgetCode.Visible = false;
        }

        protected void btnAddItemToPO_Click(object sender, EventArgs e)
        {
            if (pnlAddItemsToPO.Visible == false)
            {
                pnlAddItemsToPO.Visible = true;
            }
        }   

        protected void btnSubmitPO_Click(object sender, EventArgs e)
        {
            Int64 poNumber = 0;
            double totalValue = 0.00;
            decimal rate =0;
            //string rate = string.Empty;
            POMain poMain = new POMain();
            try
            {
                if (ViewState["EditPONumber"] != null)
                {
                    poMain.PONumber = Convert.ToInt64(ViewState["EditPONumber"]);
                }

                if (sessionUserID == "bpriyam" || sessionUserID == "jgarte" || sessionUserID == "atuccinardi" || sessionUserID == "jfoley" || sessionUserID == "scummings" || sessionUserID == "jbellini")
                { 
                    poMain.RequesterID = Convert.ToInt32(drpRequester.SelectedValue);
                }

                else 
                {
                    poMain.RequesterID = Convert.ToInt32(poMain.GetRequesterID(sessionUserID));
                    
                }
                
                poMain.OrderClassificationID = Convert.ToInt32(drpOrderClassification.SelectedValue);
                poMain.StationID = Convert.ToInt32(drpStation.SelectedValue);
                poMain.DepartmentID = Convert.ToInt32(drpDepartment.SelectedValue);
                poMain.POType = Convert.ToInt32(drpType.SelectedValue);
                poMain.ProjectID = Convert.ToInt32(drpProject.SelectedValue);             
                poMain.ShipToID = Convert.ToInt32(drpShipTo.SelectedValue);
                poMain.Comment = txtComment.Text;
                poMain.SupplierID = Convert.ToInt32(drpSupplier.SelectedValue);
                poMain.InBudgetID = Convert.ToInt32(drpInBudget.SelectedValue);
                poMain.CurrencyID = Convert.ToInt32(drpCurrency.SelectedValue);
                poMain.NRC = txtNRC.NumberValue != "" ? Convert.ToInt32(txtNRC.NumberValue) : 0;

                poMain.MRC = txtMRC.NumberValue != "" ? Convert.ToInt32(txtMRC.NumberValue) : 0;
                poMain.Tail = txtTail.NumberValue != "" ? Convert.ToInt32(txtTail.NumberValue) : 0;
                poMain.CircuitID = txtCircuit.Text != "" ? txtCircuit.Text : string.Empty;
                poMain.EstimatedDeliveryTime = Convert.ToDateTime(txtEstimatedDeliveryTime.Text);
                //poMain.Opportunity = txtOpportunity.Text != "" ? Convert.ToInt64(txtOpportunity.Text):0;
                poMain.Opportunity = txtOpportunity.Text;
                poMain.Supervisor = GetManagerName();
                poMain.BudgetCodeID = ddlBudgetCode.SelectedValue!="" ? Convert.ToInt32(ddlBudgetCode.SelectedValue):0;

                try
                {
                    totalValue = Convert.ToDouble(grdItems.FooterRow.Cells[5].Text);
                }
                catch
                {
                    totalValue = 0.00;
                }
                poMain.CreatedBy = userName;

                if (poMain.CurrencyID != 1)
                {
                    //CurrencyConverter.CurrencyConvertorSoapClient currencyConvert = new CurrencyConverter.CurrencyConvertorSoapClient();
                    //rate = Common.GetCurrencyUSDollarValue(drpCurrency.SelectedItem.Text);

                    rate = new WebService().ConvertYHOO(1, drpCurrency.SelectedItem.Text, "USD");
                }

                List<POItem> poItems = new List<POItem>();

                POItem poItem = null;

                foreach (GridViewRow dr in grdItems.Rows)
                {
                    if (dr.RowType == DataControlRowType.DataRow)
                    {
                        poItem = new POItem();
                        long poItemid;

                        UIUtility.TryParseDakaKeyValue(ref grdItems, dr.RowIndex, "POItemID", out poItemid);

                        poItem.ItemID = poItemid;
                        poItem.DescriptionOfParts = dr.Cells[1].Text;
                        if (dr.Cells[2].Text == "&nbsp;")
                            poItem.SupplierCode = "";
                        else
                            poItem.SupplierCode = dr.Cells[2].Text.Trim();
                        poItem.Quantity = Convert.ToDecimal(dr.Cells[3].Text);                       
                        poItem.UnitPrice = Convert.ToDecimal(((Label)dr.FindControl("lblUnitPrice")).Text);
                        poItem.TotalPrice = Convert.ToDouble(((Label)dr.FindControl("lblTotalPrice")).Text);
                        poItem.CreatedBy = userName;

                        if (drpType.SelectedValue != "1")                         
                     
                            poItem.GLCode = Convert.ToInt32(((DropDownList)dr.FindControl("ddlGLCode1")).Text);

                        poItems.Add(poItem);                       
                    }
                }
                

                if (txtDescriptionOfPart.Text != string.Empty)
                {
                    poItem = new POItem();

                    poItem.ItemID = Convert.ToInt64(ViewState["poItemID"]);
                    poItem.DescriptionOfParts = txtDescriptionOfPart.Text;
                    
                    poItem.SupplierCode = txtSuppliersCode.Text;
                    poItem.Quantity = Convert.ToDecimal(txtQuantity.Text);
                    poItem.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
                    poItem.TotalPrice = Convert.ToDouble(CalculateTotalSumByRow(txtQuantity.Text, txtUnitPrice.Text));
                    poItem.CreatedBy = userName;

                    if(drpType.SelectedValue!="1")
                        poItem.GLCode = Convert.ToInt32(ddlGlCode.SelectedValue);

                    totalValue += poItem.TotalPrice;

                    poItems.Add(poItem);                    
                }

                try
                {
                    poMain.TotalValue = totalValue;
                    if (rate == 0)
                        rate = 1;
                    //if (rate == "")                    
                    //    rate = "1";
                   
                    poMain.TotalValueInUSD = Convert.ToDouble(rate) * totalValue;                    
                }
                catch
                {
                    poMain.TotalValue = 0.00;
                }

                poMain.InsertPOData(poItems, poMain, ref poNumber);
              

                if (ViewState["isEditingPO"] != null)
                {
                    SendMail(poNumber, poMain.TotalValueInUSD, poMain.POType, poMain.DepartmentID, poMain.ProjectID);
                }
                Response.Redirect("EditPO.aspx");
               
            }
            catch (Exception ex)
            { 
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
            }
        }

        private void SendMail(Int64 poNumber, double totalValueUSD, int poType, int dept, int projectID)
        { 
            DataSet dsPOData = new DataSet();
            DataSet dsTo = new DataSet();   

            dsPOData = new POMain().BindPOData(poNumber);            
            int roleID = 0;            

            string strPOItems = string.Empty;
            foreach (DataRow dr in dsPOData.Tables[1].Rows)
            {
                strPOItems += dr["Description Of Part"].ToString()+',';
            }

            if (strPOItems != "")
                strPOItems = strPOItems.ToString().Substring(0, strPOItems.ToString().Length - 1);

            SmtpClient smtpClient = new SmtpClient("mailhost.hiberniaatlantic.com");
            MailMessage mailMsg = new MailMessage();

            //setting the approval based on value
            //if (totalValueUSD < 25001)
            //{
            //if (shipToID == 75 || shipToID == 76 || shipToID == 77)
            //{
            //    string mail = ConfigurationManager.AppSettings["mail"].ToString();
            //    mailMsg.To.Add(mail);
            //}

            /*else*/
            if ((poType != 3 && poType != 4 && poType != 5 && totalValueUSD < 25001 /*&& projectID != 175 && projectID != 221*/) || ((poType == 3 || poType == 4 || poType == 5) && !loggedInUser.IsServiceDeliveryGroup))
            {
                dsTo = GetManagerEmail();
                mailMsg.To.Add(dsTo.Tables[0].Rows[0]["mail"].ToString());
            }

                //else
                //{
                //    roleID = 3;
                //    dsTo = new POMain().GetApproverList(roleID, dept);
                //    mailMsg.To.Add(dsTo.Tables[0].Rows[0]["Email"].ToString());
                //}
            //}
                

            else //if (totalValueUSD > 25000)
            {
                roleID = 3;

                dsTo = new POMain().GetApproverList(roleID, dept);

                foreach (DataRow dr in dsTo.Tables[0].Rows)
                {
                    mailMsg.To.Add(new MailAddress(dr["Email"].ToString()));
                }
            }

            mailMsg.CC.Add(userEmail);
            if (sessionUserID == "bpriyam" || sessionUserID == "jgarte" || sessionUserID == "atuccinardi" || sessionUserID == "jfoley" || sessionUserID == "scummings" || sessionUserID == "jbellini")
            {
                mailMsg.CC.Add(new POMain().GetUserId(drpRequester.SelectedItem.Text).Tables[0].Rows[0]["Email"].ToString());
            }
            mailMsg.Bcc.Add("Basuki.Priyam@hibernianetworks.com");

            if (Convert.ToBoolean(ViewState["isEditingPO"]) == true)
                mailMsg.Subject = "PO: " + poNumber + " is edited by : " + userName;
            else
                mailMsg.Subject = "New Purchase Order(PO" + poNumber + ") is created by : " + userName;

                     
            //string itemURL = "http://hib-sppune2010:34104/Search.aspx?E=" + poNumber;
           // string itemURL ="http://info.hiberniaatlantic.com:34104/ApprovePO.aspx?E=" + poNumber;
            string itemURL = ConfigurationManager.AppSettings["URL"].ToString() + "/ApprovePO.aspx?E=" + poNumber;


            string strBody = "PO Number : " + poNumber + "<br />Requester : " + dsPOData.Tables[0].Rows[0]["RequesterName"].ToString() + "<br />Station : " + dsPOData.Tables[0].Rows[0]["StationName"].ToString() + "<br />Ship To : " + dsPOData.Tables[0].Rows[0]["City"].ToString() + "<br />Supplier : " + dsPOData.Tables[0].Rows[0]["CompanyName"].ToString()
                     + "<br />Department : " + dsPOData.Tables[0].Rows[0]["DepartmentName"].ToString() + "<br />Status : " + "Pending" + "<br /> Items : " + strPOItems + "<br />Total Value : " + dsPOData.Tables[0].Rows[0]["CurrencyName"].ToString() + " " + Convert.ToDouble(dsPOData.Tables[0].Rows[0]["TotalValue"]).ToString("#,##0")
                     + "<br />NRC : " + Convert.ToDouble(dsPOData.Tables[0].Rows[0]["NRC"]).ToString("#,##0") + "<br />MRC : " + Convert.ToDouble(dsPOData.Tables[0].Rows[0]["MRC"]).ToString("#,##0") + "<br />Term : " + Convert.ToDouble(dsPOData.Tables[0].Rows[0]["Tail"]).ToString("#,##0") + "<br />Comment : " + dsPOData.Tables[0].Rows[0]["Comment"].ToString() + "<br />GMEE Opportunity ID : " + dsPOData.Tables[0].Rows[0]["Opportunity"].ToString();
            if (strBody.Length > 8000)
                strBody = strBody.Substring(0, 8000);

            StringBuilder message = new StringBuilder();
            message.Append("<table><tr><td><h3><a href='" + itemURL + "'>Purchase Order System</a></h3></td></tr><tr><td><h3>Date: " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h3></td></tr>" +
                "<tr><td>" + strBody + "</td></tr><tr><td></td></tr></table>");
            mailMsg.Body = message.ToString();
            mailMsg.IsBodyHtml = true;          

            mailMsg.From = new MailAddress("noreply@hibernianetworks.com");

            //if(dsTo.Tables[0].Rows.Count>0)
            //if(dsTo.Tables.Count>0)
                smtpClient.Send(mailMsg);
        }

        //public void GetManagerName()
        //{
        //    try
        //    {
        //        string connection = ConfigurationManager.ConnectionStrings["ADConnectionString"].ToString();
        //        DirectorySearcher dssearch = new DirectorySearcher(connection);
        //        dssearch.Filter = "(displayName=" + Session["PO_UserName"] + ")";
        //        SearchResult sresult = dssearch.FindOne();
        //        DirectoryEntry dsresult = sresult.GetDirectoryEntry();
        //        string Manager = dsresult.Properties["manager"][0].ToString();

        //        //string connection = ConfigurationManager.ConnectionStrings["ADConnectionString"].ToString();
        //        //DirectorySearcher dssearch = new DirectorySearcher(connection);
        //        //dssearch.Filter = "(sAMAccountName  =" + Session["PO_UserName"] + ")";
        //        //SearchResult sresult = dssearch.FindOne();
        //        //DirectoryEntry dsresult = sresult.GetDirectoryEntry();
        //        //string Manager = dsresult.Properties["manager"][0].ToString();

        //        if (Manager != "")
        //        {
        //            if (Manager.Contains("CN="))
        //            {
        //                int Length = Manager.IndexOf(',');
        //                Manager = Manager.Substring(3, Length - 3);
        //            }
        //            else
        //            {
        //                Manager = string.Empty;
        //            }
        //        }

        //        dssearch.Filter = "(displayName=" + Manager + ")";
        //        SearchResult sresult1 = dssearch.FindOne();
        //        DirectoryEntry dsresult1 = sresult1.GetDirectoryEntry();
        //        string ManagerEmail = dsresult1.Properties["email"][0].ToString();

        //        Session["ManagerName"] = Manager;
        //        Session["ManagerEmail"] = ManagerEmail;
        //    }
        //    catch (Exception ex)
        //    { }

        //}

        private DataSet GetManagerEmail()
        {
            string userId;

            if (sessionUserID == "bpriyam" || sessionUserID == "jgarte" || sessionUserID == "atuccinardi" || sessionUserID == "jfoley" || sessionUserID == "scummings" || sessionUserID == "jbellini")
            {
                userId = ViewState["UserID"].ToString();
            }

            else
                userId = sessionUserID;

            DataSet dsMgr = new POMain().GetManagerFromLDAP(userId);       
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

        private string GetManagerName()
        {
            string userId;

            if (sessionUserID == "bpriyam" || sessionUserID == "jgarte" || sessionUserID == "atuccinardi" || sessionUserID == "jfoley" || sessionUserID == "scummings" || sessionUserID == "jbellini")
            {
                userId = ViewState["UserID"].ToString();
            }

            else
                userId = sessionUserID;

            DataSet dsMgr = new POMain().GetManagerFromLDAP(userId);      

            string manager = dsMgr.Tables[0].Rows[0]["manager"].ToString();
            if (manager != "")
            {
                if (manager.Contains("CN="))
                {
                    int Length = manager.IndexOf(',');
                    manager = manager.Substring(3, Length - 3);                    
                }
                else
                {
                    manager = string.Empty;
                }
            }
            return manager;
        }

        protected void btnDeletePO_Click(object sender, EventArgs e)
        {
            new POMain().DeletePO(Convert.ToInt64(ViewState["EditPONumber"]), userName);
            Response.Redirect("EditPO.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditPO.aspx");
        }

        protected void lnkImportItems_Click(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            string glCodeID = string.Empty;
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.ClearHeaders();
            //HttpContext.Current.Response.Buffer = true;
            //HttpContext.Current.Response.ContentType = "application/ms-excel";
            //HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            if (POItemsFileUpload.HasFile)
            {
                try
                {
                    string strFileType = System.IO.Path.GetExtension(POItemsFileUpload.FileName).ToString().ToLower();
                    string strConnectionString = string.Empty;
                    if (strFileType == ".xls" || strFileType == ".xlsx")
                    {
                        string filepath = Server.MapPath("~/POSample/" + System.IO.Path.GetFileName(POItemsFileUpload.PostedFile.FileName));
                        POItemsFileUpload.PostedFile.SaveAs(filepath);
                        FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
                        IExcelDataReader excelReader = null;
                        if (strFileType == ".xls")
                            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                        else if (strFileType == ".xlsx")
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                         excelReader.IsFirstRowAsColumnNames = true;
                            DataSet dsresult = excelReader.AsDataSet();
                            excelReader.Close();
                            try
                            {
                                File.Delete(filepath);
                            }
                            catch { }
                            foreach (DataRow row in dsresult.Tables[0].Rows)
                            {
                                if (row.ItemArray[0].ToString().Trim() != string.Empty)
                                {
                                    DataTable dtItems = new DataTable();
                                    dtItems.Columns.Add("POItemID");
                                    dtItems.Columns.Add("Description Of Part");
                                    dtItems.Columns.Add("Suppliers Code");
                                    dtItems.Columns.Add("Quantity");
                                    dtItems.Columns.Add("UnitPrice");                                   
                                    dtItems.Columns.Add("TotalPrice");
                                    dtItems.Columns.Add("GLCodeName");
                                    dtItems.Columns.Add("GLCodeID");

                                    DataRow dtRow = dtItems.NewRow();
                                    dtRow["POItemID"] = 0;
                                    dtRow["Description Of Part"] = row.ItemArray[0].ToString();
                                    dtRow["Suppliers Code"] = row.ItemArray[1].ToString();
                                    try
                                    {
                                        Convert.ToDouble(row.ItemArray[2].ToString());
                                        dtRow["Quantity"] = row.ItemArray[2].ToString();
                                    }
                                    catch
                                    {
                                        dtRow["Quantity"] = 0;
                                    }
                                    try
                                    {
                                        Convert.ToDouble(row.ItemArray[3].ToString());
                                        dtRow["UnitPrice"] = row.ItemArray[3].ToString();
                                    }
                                    catch
                                    {
                                        dtRow["UnitPrice"] = 0;
                                    }
                                 
                                    dtRow["TotalPrice"] = CalculateTotalSumByRow(row.ItemArray[2].ToString(), row.ItemArray[3].ToString());

                                    dtRow["GLCodeName"] = row.ItemArray[4].ToString();
                                    if (poMain.GetGLCodeID(row.ItemArray[4].ToString()) != null)
                                    {
                                        glCodeID = poMain.GetGLCodeID(row.ItemArray[4].ToString()).ToString();
                                        dtRow["GLCodeID"] = glCodeID;
                                    }                                      
                                                                  
                                   
                                    dtItems.Rows.Add(dtRow);

                                    if (ViewState["ViewStateItems"] == null)
                                        ViewState["ViewStateItems"] = dtItems;
                                    else
                                    {
                                        ((DataTable)(ViewState["ViewStateItems"])).ImportRow(dtRow);
                                    }
                                }
                            }
                            grdItems.DataSource = (DataTable)(ViewState["ViewStateItems"]);
                            grdItems.DataBind();
                        }
                        else
                        {
                            lblMessage.Text = "Only excel files allowed";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Visible = true;
                            return;
                        }               

                }
                catch(Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.Visible = true;
                }
            }
        }

        protected void lnkAddAnotherItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescriptionOfPart.Text.Trim() != string.Empty)
                {
                    DataTable dtItems = new DataTable();
                    dtItems.Columns.Add("Description Of Part");
                    dtItems.Columns.Add("Suppliers Code");
                    dtItems.Columns.Add("Quantity");
                    dtItems.Columns.Add("UnitPrice");
                    dtItems.Columns.Add("TotalPrice");
                    dtItems.Columns.Add("POItemID");
                    dtItems.Columns.Add("GLCodeID");
                    dtItems.Columns.Add("GLCodeName");


                    DataRow dtRow = dtItems.NewRow();
                    dtRow["Description Of Part"] = txtDescriptionOfPart.Text;
                    dtRow["Suppliers Code"] = txtSuppliersCode.Text;
                    try
                    {
                        Convert.ToDouble(txtQuantity.Text);
                        dtRow["Quantity"] = txtQuantity.Text;
                    }
                    catch
                    {
                        dtRow["Quantity"] = 0;
                    }
                    try
                    {
                        Convert.ToDouble(txtUnitPrice.Text);
                        dtRow["UnitPrice"] = txtUnitPrice.Text;
                    }
                    catch
                    {
                        dtRow["UnitPrice"] = 0;
                    }

                    dtRow["TotalPrice"] = CalculateTotalSumByRow(txtQuantity.Text, txtUnitPrice.Text);

                    try
                    {
                        dtRow["POItemID"] = Convert.ToInt64(ViewState["poItemID"]);
                    }
                    catch
                    {
                        dtRow["POItemID"] = 0;
                    }

                    if (drpType.SelectedValue == "1" || drpType.SelectedValue=="0")
                    {
                        dtRow["GLCodeID"] = 0;
                        dtRow["GLCodeName"] = "";
                    }
                    else
                    {
                        dtRow["GLCodeID"] = ddlGlCode.SelectedValue;
                        dtRow["GLCodeName"] = ddlGlCode.SelectedItem.Text;
                    }

                    dtItems.Rows.Add(dtRow);

                    if (ViewState["ViewStateItems"] == null)
                        ViewState["ViewStateItems"] = dtItems;
                    else
                    {
                        ((DataTable)(ViewState["ViewStateItems"])).ImportRow(dtRow);
                    }
                    grdItems.DataSource = (DataTable)(ViewState["ViewStateItems"]);
                    grdItems.DataBind();
                    ClearControls(pnlAddItemsToPO);
                }                
            }

            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
            }
        }

        protected void grdItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    double rowTotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TotalPrice"));
                    grdItemsTotal += rowTotal;

                    DropDownList ddlGL = (DropDownList)e.Row.FindControl("ddlGLCode1");
                    DataTable dt = new DataTable();
                    dt.Columns.Add("GLCodeID");
                    dt.Columns.Add("GLCodeName");
                    DataRow dtRow = dt.NewRow();
                    dtRow["GLCodeID"] = DataBinder.Eval(e.Row.DataItem, "GLCodeID").ToString();
                    dtRow["GLCodeName"] = DataBinder.Eval(e.Row.DataItem, "GLCodeName").ToString();
                    dt.Rows.Add(dtRow);
                    ddlGL.DataSource = dt;
                    ddlGL.DataBind();
                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[4].Text = "Total:";
                    string s = grdItemsTotal.ToString();
                    //s = string.Format("0:n2");
                    //e.Row.Cells[5].Text.ToString({"0:n2"}); 
                    e.Row.Cells[5].Text = s;
                    grdItemsTotal = 0;
                }
            }

            catch (Exception ex)
            { }
        }      

        private void GetRequester()
        {
            DataSet dsRequester = new POSystemDataHandler().GetRequester();
            drpRequester.DataSource = dsRequester;
            drpRequester.DataBind();

            drpRequester.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void FillOrderClassification(int poType)
        {
            DataSet dsOrderClass = new POMain().GetOrderClassification(poType);

            if (dsOrderClass.Tables.Count > 0 && dsOrderClass.Tables[0].Rows.Count > 0)
            {
                drpOrderClassification.DataSource = dsOrderClass;
                drpOrderClassification.DataBind();
                drpOrderClassification.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        private void FillStation()
        {
            DataSet dsStation = new POSystemDataHandler().GetStation();
            drpStation.DataSource = dsStation;
            drpStation.DataBind();
            drpStation.Items.Insert(0, new ListItem( "--Select--","0"));
        }

        private void FillProject()
        {
            DataSet dsProject = new POSystemDataHandler().GetProject();
            drpProject.DataSource = dsProject;
            drpProject.DataBind();
            drpProject.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void FillDepartment()
        {
            DataSet dsDept = new POSystemDataHandler().GetDepartment();
            drpDepartment.DataSource = dsDept;
            drpDepartment.DataBind();
            drpDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void FillGLCode(int orderClassification)
        {
            try
            {
                DataSet dsGL = new POSystemDataHandler().GetGLCode(orderClassification);
                if (dsGL.Tables.Count > 0)
                {
                    if (dsGL.Tables[0].Rows.Count > 0)
                    {
                        ddlGlCode.DataSource = dsGL;
                        ddlGlCode.DataBind();
                    }
                }
                ddlGlCode.Items.Insert(0, new ListItem("--Select--", "0"));
            }

            catch (Exception ex)
            { }
        }

        private void FillShipTo()
        {
            DataSet dsShip = new POSystemDataHandler().GetShipTo();
            drpShipTo.DataSource = dsShip;
            drpShipTo.DataBind();
            drpShipTo.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void FillType()
        {
            DataSet dsType = new POSystemDataHandler().GetType();
            drpType.DataSource = dsType;
            drpType.DataBind();
            drpType.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void FillSuppliers()
        {
            DataSet dsSuppliers = new POSystemDataHandler().GetSupplier();
            drpSupplier.DataSource = dsSuppliers;
            drpSupplier.DataBind();
            drpSupplier.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void FillBuggetedField()
        {
            DataSet dsBuggeted = new POSystemDataHandler().GetBuggetedField();
            drpInBudget.DataSource = dsBuggeted;
            drpInBudget.DataBind();
            drpInBudget.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void FillCurrency()
        {
            DataSet dsCurrency = new POSystemDataHandler().GetCurrency();
            drpCurrency.DataSource = dsCurrency;
            drpCurrency.DataBind();
            drpCurrency.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private string CalculateTotalSumByRow(string quantity, string unitprice)
        {
            double totalprice = 0;
            try
            {
                if (quantity != string.Empty && unitprice != string.Empty)
                {
                    totalprice = Convert.ToDouble(quantity) * Convert.ToDouble(unitprice);
                }
            }
            catch
            { }
            return totalprice.ToString("#,0.00");//String.Format("{0:0,0}", totalprice); //totalprice.ToString();
        }

        private void ClearControls(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).Text = string.Empty;
                }
                if (ctrl is DropDownList && (drpType.SelectedValue!="1"))
                {
                    ((DropDownList)ctrl).SelectedIndex = 0;
                }
            }
        }

        private void SetControlsReadonly(Control control, bool value)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).ReadOnly = value;
                }
                if (ctrl is DropDownList)
                {
                    ((DropDownList)ctrl).Enabled = !value;
                }
                if (ctrl is FileUpload)
                {
                    ((FileUpload)ctrl).Enabled = !value;
                }
                if (ctrl is LinkButton)
                {
                    ((LinkButton)ctrl).Enabled = !value;
                }
                if (ctrl is HyperLink)
                {
                    ((HyperLink)ctrl).Enabled = !value;
                }
                if (ctrl is GridView)
                {
                    ((GridView)ctrl).Enabled = !value;
                }
            }
        }

        protected void grdItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                if (e.CommandName == "EditRecord")
                {
                    DataTable dt = (DataTable)(ViewState["ViewStateItems"]);
                    txtDescriptionOfPart.Text = dt.Rows[row.RowIndex]["Description Of Part"].ToString();

                    txtSuppliersCode.Text = dt.Rows[row.RowIndex]["Suppliers Code"].ToString();
                    txtQuantity.Text = dt.Rows[row.RowIndex]["Quantity"].ToString();
                    txtUnitPrice.Text = dt.Rows[row.RowIndex]["UnitPrice"].ToString();
                    //txtTotalPrice.Text= dt.Rows[row.RowIndex][]
                    Int64 poItemID = Convert.ToInt64(dt.Rows[row.RowIndex]["POItemID"]);

                    ddlGlCode.SelectedValue = dt.Rows[row.RowIndex]["GLCodeID"].ToString();
                    ViewState["poItemID"] = poItemID;

                    try
                    {
                        txtTotalPrice.Text = (Convert.ToInt32(txtQuantity.Text) * Convert.ToDecimal(txtUnitPrice.Text)).ToString();
                    }

                    catch { }

                    dt.Rows[row.RowIndex].Delete();
                    dt.AcceptChanges();

                    grdItems.DataSource = (DataTable)(ViewState["ViewStateItems"]);
                    grdItems.DataBind();
                }

                if (e.CommandName == "DeleteRecord")
                {
                    DataTable dt = (DataTable)(ViewState["ViewStateItems"]);
                    dt.Rows[row.RowIndex].Delete();
                    dt.AcceptChanges();
                    grdItems.DataSource = (DataTable)(ViewState["ViewStateItems"]);
                    grdItems.DataBind();
                }
            }

            catch (Exception ex)
            { }
        }

        private void ShowProvision()
        {            
            //lblTail.Visible = true;
            //txtTail.Visible = true;

            lblCircuitID.Visible = true;
            txtCircuit.Visible = true;           
        }      

        [WebMethod]
        public static List<string> GetCircuitID(string prefixText, int count)
        {
            
            DataSet ds = new DataSet();
            List<string> listToReturn = new List<string>();
         
            ds = new POMain().GetCircuitID(prefixText);

                string itemToAdd = string.Empty;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    itemToAdd = dr["circuitid"].ToString();//AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["circuitid"].ToString(), dr["RecId"].ToString());
                    listToReturn.Add(itemToAdd);
                }
          
                return listToReturn;
        }

        [WebMethod]
        public static List<string> GetOpportunityID(string prefixText, int count)
        {

            DataSet ds = new DataSet();
            List<string> listToReturn = new List<string>();

            ds = new POMain().GetOpportunityID(prefixText);

            string itemToAdd = string.Empty;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                itemToAdd = dr["ReferenceID"].ToString();//AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["circuitid"].ToString(), dr["RecId"].ToString());
                listToReturn.Add(itemToAdd);
            }

            return listToReturn;
        }

        protected void drpOrderClassification_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt32(drpType.SelectedValue) !=1)
            
                FillGLCode(Convert.ToInt32(drpOrderClassification.SelectedValue));

            drpStation.Enabled = true;
            drpStation.SelectedIndex = 0;
            ddlBudgetCode.Visible = false;
            lblBudgetCode.Visible = false;
        }

        protected void grdItems_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                    if (drpType.SelectedValue == "1")
                        e.Row.Cells[6].Visible = false;
                    else
                        e.Row.Cells[6].Visible = true;

                if (e.Row.RowType == DataControlRowType.DataRow)
                    if (drpType.SelectedValue == "1")
                        e.Row.Cells[6].Visible = false;
                    else
                        e.Row.Cells[6].Visible = true;

                if (e.Row.RowType == DataControlRowType.Footer)
                    if (drpType.SelectedValue == "1")
                        e.Row.Cells[6].Visible = false;
                    else
                        e.Row.Cells[6].Visible = true;
            }
            catch (Exception ex)
            { }
        }

        protected void drpRequester_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUserID();
        }

        public void GetUserID()
        {
            if (sessionUserID == "bpriyam" || sessionUserID == "jgarte" || sessionUserID == "atuccinardi" || sessionUserID == "jfoley" || sessionUserID == "scummings" || sessionUserID == "jbellini")
            {
                //if (!Convert.ToBoolean(ViewState["isEditingPO"]))
                    userID = new POMain().GetUserId(drpRequester.SelectedItem.Text).Tables[0].Rows[0]["UserID"].ToString();
                //else
                //{
                    //userID = new POMain().GetUserId().Tables[0].Rows[0]["UserID"].ToString();
                //}
            }
            else
            {
                userID = sessionUserID;
            }
            ViewState["UserID"] = userID;
        }

        protected void drpStation_SelectedIndexChanged(object sender, EventArgs e)
        {    

            DataSet dsBudgetCodes = new POSystemDataHandler().GetBudgetCodes(Convert.ToInt32(drpDepartment.SelectedValue), Convert.ToInt32(drpOrderClassification.SelectedValue), Convert.ToInt32(drpStation.SelectedValue), 0);

            if (dsBudgetCodes.Tables.Count > 0 && dsBudgetCodes.Tables[0].Rows.Count>0)
            {
                ddlBudgetCode.Visible = true;
                lblBudgetCode.Visible = true;

                ddlBudgetCode.DataSource = dsBudgetCodes;
                ddlBudgetCode.DataBind();

                ddlBudgetCode.Items.Insert(0, new ListItem("--Select--", "0"));
            }

            else
            {
                ddlBudgetCode.Visible = false;
                lblBudgetCode.Visible = false;                
            }
        }

        //protected void drpShipTo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (drpShipTo.SelectedValue == "75" || drpShipTo.SelectedValue == "76" || drpShipTo.SelectedValue == "77")
        //    {
        //        drpDepartment.SelectedValue = "2";
        //        drpType.Items.FindByValue("3").Enabled = false;
        //        drpType.Items.FindByValue("4").Enabled = false;
        //        drpType.Items.FindByValue("5").Enabled = false;
        //    }

        //    else
        //    {
        //        drpDepartment.SelectedIndex = 0;
        //        drpType.SelectedIndex = 0;
        //        drpType.Items.FindByValue("3").Enabled = true;
        //        drpType.Items.FindByValue("4").Enabled = true;
        //        drpType.Items.FindByValue("5").Enabled = true;
        //    }
        //}

        //protected void drpProject_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataSet ds = new POMain().GetShipToOnProject(Convert.ToInt32(drpProject.SelectedValue));
            
        //    drpShipTo.DataSource = ds;
        //    drpShipTo.DataBind();
        //    drpShipTo.Items.Insert(0, new ListItem("--Select--", "0"));

        //    if (drpProject.SelectedValue == "175" || drpProject.SelectedValue=="221")
        //    {
        //        drpDepartment.SelectedValue = "2";
        //        drpType.Items.FindByValue("3").Enabled = false;
        //        drpType.Items.FindByValue("4").Enabled = false;
        //        drpType.Items.FindByValue("5").Enabled = false;
        //    }

        //    else
        //    {
        //        drpDepartment.SelectedIndex = 0;
        //        drpType.SelectedIndex = 0;
        //        drpType.Items.FindByValue("3").Enabled = true;
        //        drpType.Items.FindByValue("4").Enabled = true;
        //        drpType.Items.FindByValue("5").Enabled = true;
        //    }
        //}
    }
}