using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using BAL;
using DAL;
using System.Web.UI.HtmlControls;

namespace POManagementASPNet
{
    public partial class Admin : System.Web.UI.Page
    {
        private Employee loggedInUser = null;
        string userName;

        public int drpdwnActive
        {
            get;
            set;
        }
        public string MessageError
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            userName = Session["PO_UserName"].ToString();
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
            liApprove.Attributes.Add("class", "");
            liClose.Attributes.Add("class", "");
            liAdmin.Attributes.Add("class", "active");
            liReport.Attributes.Add("class", "");

            //if (loggedInUser.HasEmployeeAccess(Roles.PORequester))
            //{
            //    liAdmin.Attributes.Remove("class", "");
            //}
            //else
            //{
            //    liAdmin.Attributes.Add("class", "active");
            //}

            try
            {
                if (!this.IsPostBack)
                {
                    if (loggedInUser == null)
                    {
                        Response.Redirect("Login.aspx?resign=true");
                    }
                    UpdPanelRequester.Visible = false;
                    UpdPanelShipTo.Visible = false;
                    BtnReqInsert.Visible = false;
                    updProjects.Visible = false;
                    updSupplier.Visible = false;
                    divBudgCode.Visible = false;

                    FillBudhetCodeInsertValues();

                }
            }
             
            catch (Exception ex)
            { }
        }    

        public void ControlState()
        {
            txtAddress.Text = "";
            txtID.Text = "";
            txtCity.Text = "";         
            txtVAT.Text = "";

            txtNewCity.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtNewVat.Text = "";

            txtRequesterID.Text = "";
            txtRequesterName.Text = "";

            txtReqName.Text = "";
            drpdwnRole.SelectedIndex = 0;
            txtEmailID.Text = "";
            txtUserID.Text = "";

            //txtProjectName.Text = "";
            //lblProjectID.Text = "";

            TxtReqSearchAddress.Text = "";
            txtSearchRequestor.Text = "";
            txtProjectSearch.Text = "";

            gvShowShipTo.Visible = false;
            gvRequesterView.Visible = false;

            UpdPanelRequester.Visible = false;
            UpdPanelShipTo.Visible = false;
            updProjects.Visible = false;
            updSupplier.Visible = false;
            divBudgCode.Visible = false;

            txtSupplierearch.Text = "";

            txtSearchBudgetCode.Text = "";

        }

        #region ShipTo 
        [WebMethod]
        public static List<string> GetShipToName(string prefixText, int count)
        {
            DataSet ds = new DataSet();
            List<string> address = new List<string>();
            try
            {
                ds = new POMain().GetShipToExtender(prefixText);
                string itemToAdd = string.Empty;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    itemToAdd = dr["City"].ToString();//AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["circuitid"].ToString(), dr["RecId"].ToString());
                    address.Add(itemToAdd);
                }

                //SqlConnection connection = new SqlConnection();
                //connection.ConnectionString = DatabaseCredientials;
                //SqlCommand command = new SqlCommand("SpGetShipToExtender", connection);
                //command.CommandType = System.Data.CommandType.StoredProcedure;
                //command.Parameters.AddWithValue("@City", prefixText);
                //command.CommandText = "SpGetShipToExtender";
                //connection.Open();
                //using (SqlDataReader sdr = command.ExecuteReader())
                //{
                //    while (sdr.Read())
                //    {
                //        address.Add(sdr["City"].ToString());
                //    }
                //}
                //connection.Close();
            }
            catch
            { }
            return address;
        }

        protected void btnShwShipTo_Click(object sender, EventArgs e)
        {
            lblUpdateMessage.Visible = false;
            lblMessageError.Visible = false;
            DataSet ds = new DataSet();
            try
            {
                string city = TxtReqSearchAddress.Text; ;
                ds = new POMain().GetShipToDetails(city);
                gvShowShipTo.DataSource = ds;
                gvShowShipTo.DataBind();
                gvShowShipTo.Visible = true;
            }
            catch (Exception ex)
            { }
        }

        protected void gvShowShipTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdPanelShipTo.Visible = true;
            try
            {
                Label lblID = gvShowShipTo.SelectedRow.FindControl("lblShipID") as Label;
                Label lblStation = gvShowShipTo.SelectedRow.FindControl("lblCity") as Label;
                Label lblShipToAddress = gvShowShipTo.SelectedRow.FindControl("lblAddress") as Label;
                Label lblVAT1 = gvShowShipTo.SelectedRow.FindControl("lblVAT") as Label;
                Label lblAddAddress = gvShowShipTo.SelectedRow.FindControl("lblAddAddress") as Label;

                txtID.Text = lblID.Text;
                txtCity.Text = lblStation.Text;
                txtAddress.Text = lblShipToAddress.Text;
                txtVAT.Text = lblVAT1.Text;
                txtAdditionalAddress.Text = lblAddAddress.Text;
            }
            catch (Exception ex)
            {

            }
        }

        protected void BtnEditShipTo_Click(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            poMain.ShipToID = Convert.ToInt32(txtID.Text);
            poMain.ShipToName = txtCity.Text;
            poMain.ShipToAddress = txtAddress.Text;
            poMain.VAT = txtVAT.Text;
            poMain.NewAddress = txtAdditionalAddress.Text;
            poMain.CreatedBy = userName;

            int retOut = poMain.UpdateShipToDetails(poMain);
            lblMessageError.Visible = true;

            if (retOut == 1)
            {
                ControlState();
                lblUpdateMessage.Visible = true;
                lblUpdateMessage.Text = "";
                lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                lblUpdateMessage.Text = "Data Successfully Updated";
            }

            else  
            {
                lblMessageError.Text = "";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Text = "Failed while Updating Data" + txtID.Text;
            }
        }

        protected void btnDeleteShipto_Click(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            poMain.ShipToID = Convert.ToInt32(txtID.Text);
            poMain.CreatedBy = userName;
            int retOut = poMain.DeleteShipToDetails(poMain);
            if (retOut == 1)
            {
                ControlState();
                lblUpdateMessage.Visible = true;
                lblUpdateMessage.Text = "";
                lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                lblUpdateMessage.Text = "Data Successfully Deleted";
            }
            else
            {
                lblMessageError.Text = "";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Text = "Failed while Deleting data" + txtID.Text;
            }
        }

        protected void btnInsertShipTo_Click(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            poMain.ShipToName = txtNewCity.Text;
            poMain.ShipToAddress = txtAddress1.Text + " " + txtAddress2.Text;
            poMain.VAT = txtNewVat.Text;
            poMain.NewAddress = txtNewAddAddress.Text;
            poMain.CreatedBy = userName;

            int retOut = poMain.InsertShipTo(poMain);
            lblMessageError.Visible = true;

            if (retOut == 1)
            {                
                lblUpdateMessage.Visible = true;
                lblUpdateMessage.Text = "";
                lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                ControlState();
                lblUpdateMessage.Text = "Data Successfully Inserted";
            }

            else if (retOut == -1)
            {
                ControlState();
                lblUpdateMessage.Visible = true;
                lblUpdateMessage.Text = "";
                lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                lblUpdateMessage.Text = "Data already exists";
            }
            else
            {
                lblMessageError.Text = "";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Text = "Insertion Failed";
            }
        }

        protected void TxtReqSearchAddress_TextChanged(object sender, EventArgs e)
        {
            gvShowShipTo.Visible = false;
            UpdPanelShipTo.Visible = false;
            lblUpdateMessage.Visible = false;
            lblMessageError.Visible = false;
        }

         #endregion

        #region Requester 
        [WebMethod]
        public static List<string> GetRequestorName(string prefixText, int count)
        {
            DataSet ds = new DataSet();
            List<string> address = new List<string>();
            try
            {
                ds = new POMain().GetRequesterExtender(prefixText);
                string itemToAdd = string.Empty;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    itemToAdd = dr["RequesterName"].ToString();//AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["circuitid"].ToString(), dr["RecId"].ToString());
                    address.Add(itemToAdd);
                }               
            }
            catch
            { }
            return address;
        }

        protected void BtnSearchRequester_Click(object sender, EventArgs e)
        {
            lblUpdateMessage.Visible = false;
            DataSet ds = new DataSet();
            try
            {
                string requesterName = txtSearchRequestor.Text; ;
                ds = new POMain().GetRequesterDetails(requesterName);
                //ds = new POMain().GetManagerEmailFromLDAP(requesterName);
                gvRequesterView.DataSource = ds;
                gvRequesterView.DataBind();
                gvRequesterView.Visible = true;
            }
            catch (Exception ex)
            { }
        }

        protected void gvRequesterView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdPanelRequester.Visible = true;

            try
            {
                DataSet ds = new DataSet();

                Label lblID = gvRequesterView.SelectedRow.FindControl("lblReqID") as Label;
                Label lblName = gvRequesterView.SelectedRow.FindControl("lblReqName") as Label;
                Label lblRole = gvRequesterView.SelectedRow.FindControl("lblRoleName") as Label;

                ds = new POMain().GetManagerEmailFromLDAP(lblName.Text);

                if (lblRole.Text == "PORequester")
                {
                    drpdwnActive = 1;
                }
                else if (lblRole.Text == "Supervisor")
                {
                    drpdwnActive = 2;
                }
                else if (lblRole.Text == "HOD")
                {
                    drpdwnActive = 3;
                }

                else if (lblRole.Text == "CFO")
                {
                    drpdwnActive = 4;
                }

                else if (lblRole.Text == "COO")
                {
                    drpdwnActive = 5;
                }

                else if (lblRole.Text == "CEO")
                {
                    drpdwnActive = 6;
                }


                txtRequesterID.Text = lblID.Text;
                txtRequesterName.Text = lblName.Text;
                DrpDwnRoleNames.SelectedIndex = drpdwnActive;
                reqEmail.Text = ds.Tables[0].Rows[0]["mail"].ToString();
                reqUserID.Text = ds.Tables[0].Rows[0]["SAMAccountName"].ToString();
            }
            catch (Exception ee)
            {

            }
        }

        protected void BtnUpdateRequester_Click(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            try
            {
                poMain.RequesterName = txtRequesterName.Text;
                poMain.Role = DrpDwnRoleNames.SelectedItem.Text;
                poMain.Email = reqEmail.Text;
                poMain.UserID = reqUserID.Text;
                poMain.CreatedBy = userName;

                int retOut = poMain.UpdateRequester(poMain);

                lblMessageError.Visible = true;

                if (retOut == 1)
                {
                    ControlState();
                    lblUpdateMessage.Visible = true;
                    lblUpdateMessage.Text = "";
                    lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                    lblUpdateMessage.Text = "Data Successfully Updated";
                }

                else
                {
                    lblMessageError.Text = "";
                    lblMessageError.ForeColor = System.Drawing.Color.Red;
                    lblMessageError.Text = "Failed while Updating Data" + txtRequesterID.Text;

                }
            }
            catch (Exception ex)
            {
                MessageError = ex.Message.ToString();
                lblMessageError.Text = "";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Text = "<b>Your Transaction Failed: </b>" + MessageError;
            }
        }

        protected void BtnReqInsert_Click(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            try
            {
                poMain.RequesterName = txtReqName.Text;
                poMain.Email = txtEmailID.Text;
                poMain.UserID = txtUserID.Text;
                poMain.Role = drpdwnRole.SelectedItem.Text;
                poMain.CreatedBy = userName;

                int retOut = poMain.InsertRequester(poMain);

                lblMessageError.Visible = true;

                if (retOut == 1)
                {
                    lblUpdateMessage.Visible = true;
                    lblUpdateMessage.Text = "";
                    lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                    ControlState();
                    lblUpdateMessage.Text = "Data Successfully Inserted";
                }

                else
                {
                    lblMessageError.Text = "";
                    lblMessageError.ForeColor = System.Drawing.Color.Red;
                    lblMessageError.Text = "User Already Exist.....";

                }
            }
            catch (Exception ex)
            { }
        }

        protected void txtReqName_TextChanged(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            DataSet ds = poMain.GetManagerEmailFromLDAP(txtReqName.Text);
            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    txtEmailID.Text = ds.Tables[0].Rows[i][1].ToString();
                    txtUserID.Text = ds.Tables[0].Rows[i][0].ToString();

                }
                ModalPopupExtender1.Show();
                lblValidation.Visible = false;
                BtnReqInsert.Visible = true;
            }

            else
            {
                txtReqName.Focus();
                ModalPopupExtender1.Show();
                lblValidation.Visible = true;
                lblValidation.Text = "Wrong User Name";
            }
        }

        protected void btnDeleteReq_Click(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            try
            {
                poMain.RequesterID = Convert.ToInt32(txtRequesterID.Text);
                poMain.CreatedBy = userName;

                int retOut = poMain.DeleteRequester(poMain);
                lblMessageError.Visible = true;
                if (retOut == 1)
                {
                    ControlState();
                    lblUpdateMessage.Visible = true;
                    lblUpdateMessage.Text = "";
                    lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                    lblUpdateMessage.Text = "Data Successfully Deleted";
                }

                else
                {
                    lblMessageError.Text = "";
                    lblMessageError.ForeColor = System.Drawing.Color.Red;
                    lblMessageError.Text = "Data Deletion Failed " + txtRequesterID.Text;

                }

            }
            catch (Exception ex)
            {
                MessageError = ex.Message.ToString();
                lblMessageError.Text = "";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Text = "<b>Your Transaction Failed: </b>" + MessageError;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ControlState();
        }

        protected void BtnReqCancel_Click(object sender, EventArgs e)
        {
            ControlState();
        }

        #endregion

        #region Projects

        [WebMethod]
        public static List<string> GetProjectsName(string prefixText, int count)
        {
            DataSet ds = new DataSet();
            List<string> project = new List<string>();
            try
            {
                ds = new POMain().GetProjectExtender(prefixText);
                string itemToAdd = string.Empty;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    itemToAdd = dr["ProjectName"].ToString();//AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["circuitid"].ToString(), dr["RecId"].ToString());
                    project.Add(itemToAdd);
                }
            }
            catch
            { }
            return project;
        }
       
        protected void btnSearchProject_Click(object sender, EventArgs e)
        {
            lblUpdateMessage.Visible = false;
            updProjects.Visible = true;
            DataSet ds = new DataSet();
            try
            {
                ds = new POMain().GetProjectDetails(txtProjectSearch.Text);
                txtProjectName.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                lblProjectID.Text = ds.Tables[0].Rows[0]["ProjectID"].ToString();
            }

            catch (Exception ex)
            { }
        }       

        protected void btnUpdateProject_Click(object sender, EventArgs e)
        {
             POMain poMain = new POMain();
             try
             {
                 poMain.ProjectID = Convert.ToInt32(lblProjectID.Text);
                 poMain.ProjectName = txtProjectName.Text;
                 poMain.CreatedBy = userName;

                 int retOut = poMain.UpdateProject(poMain);

                 lblMessageError.Visible = true;

                 if (retOut == 1)
                 {
                     ControlState();
                     lblUpdateMessage.Visible = true;
                     lblUpdateMessage.Text = "";
                     lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                     lblUpdateMessage.Text = "Data Successfully Updated";
                 }

                 else
                 {
                     lblMessageError.Text = "";
                     lblMessageError.ForeColor = System.Drawing.Color.Red;
                     lblMessageError.Text = "Failed while Updating Data" + lblProjectID.Text;

                 }
             }
             catch (Exception ex)
             {
                 MessageError = ex.Message.ToString();
                 lblMessageError.Text = "";
                 lblMessageError.ForeColor = System.Drawing.Color.Red;
                 lblMessageError.Text = "<b>Your Transaction Failed: </b>" + MessageError;
             }
        }

        protected void btnDeleteProject_Click(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            try
            {
                poMain.ProjectID = Convert.ToInt32(lblProjectID.Text);                
                poMain.CreatedBy = userName;

                int retOut = poMain.UpdateProject(poMain);

                lblMessageError.Visible = true;

                if (retOut == 1)
                {
                    ControlState();
                    lblUpdateMessage.Visible = true;
                    lblUpdateMessage.Text = "";
                    lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                    lblUpdateMessage.Text = "Data Successfully Deleted";
                }

                else
                {
                    lblMessageError.Text = "";
                    lblMessageError.ForeColor = System.Drawing.Color.Red;
                    lblMessageError.Text = "Data Deletion Failed " + lblProjectID.Text;

                }
            }
            catch (Exception ex)
            {
                MessageError = ex.Message.ToString();
                lblMessageError.Text = "";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Text = "<b>Your Transaction Failed: </b>" + MessageError;
            }
        }

        protected void btnCancelProject_Click(object sender, EventArgs e)
        {
            ControlState();
        }

        protected void btnInsertNewProject_Click(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            try
            {
                poMain.ProjectName = txtNewProject.Text;
                poMain.CreatedBy = userName;

                int retOut = poMain.InsertProject(poMain);

                if (retOut == 1)
                {
                    lblUpdateMessage.Visible = true;
                    lblUpdateMessage.Text = "";
                    lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                    ControlState();
                    lblUpdateMessage.Text = "Data Successfully Inserted";
                }

                else
                {
                    lblMessageError.Text = "";
                    lblMessageError.ForeColor = System.Drawing.Color.Red;
                    lblMessageError.Text = "Project Already Exist.....";
                }
            }

            catch (Exception ex)
            {
 
            }
        }

        #endregion

        #region Supplier

        [WebMethod]
        public static List<string> GetSupplierName(string prefixText, int count)
        {
            DataSet ds = new DataSet();
            List<string> supplier = new List<string>();
            try
            {
                ds = new POMain().GetSupplierExtender(prefixText);
                string itemToAdd = string.Empty;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    itemToAdd = dr["CompanyName"].ToString();//AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["circuitid"].ToString(), dr["RecId"].ToString());
                    supplier.Add(itemToAdd);
                }
            }
            catch
            { }
            return supplier;
        }

        protected void btnSearchSupplier_Click(object sender, EventArgs e)
        {
            lblUpdateMessage.Visible = false;
            updSupplier.Visible = true;
            DataSet ds = new DataSet();
            try
            { 
                string supplier = txtSupplierearch.Text;
                ds = new POMain().GetSupplierDetails(supplier);
                lblSupplierID.Text = ds.Tables[0].Rows[0]["SupplierID"].ToString();
                txtSuplierName.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                txtMainNo.Text = ds.Tables[0].Rows[0]["MainNumber"].ToString();
                txtFaxNo.Text = ds.Tables[0].Rows[0]["FaxNumber"].ToString();
                txtContactPerson.Text = ds.Tables[0].Rows[0]["Personalcontact"].ToString();
                txtPhNo.Text = ds.Tables[0].Rows[0]["Number"].ToString();
                txtContactPerson1.Text = ds.Tables[0].Rows[0]["PersonalContact2"].ToString();
                txtPhNo1.Text = ds.Tables[0].Rows[0]["Number2"].ToString();
                txtAdd1.Text = ds.Tables[0].Rows[0]["Address1"].ToString();
                txtAdd2.Text = ds.Tables[0].Rows[0]["Address2"].ToString();
                txtAdd3.Text = ds.Tables[0].Rows[0]["Address3"].ToString();
                txtAdd4.Text = ds.Tables[0].Rows[0]["Address4"].ToString();
                txtAdd5.Text = ds.Tables[0].Rows[0]["Address5"].ToString();
                txtCountry.Text = ds.Tables[0].Rows[0]["Country"].ToString();
                txtComment.Text = ds.Tables[0].Rows[0]["Comment"].ToString();
                txtTerm.Text = ds.Tables[0].Rows[0]["Term"].ToString();                
            }

            catch (Exception ex)
            { }
        }

        protected void btnUpdateSupplier_Click(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            try
            {
                poMain.SupplierID = Convert.ToInt32(lblSupplierID.Text);               
                poMain.MainNo = txtMainNo.Text;
                poMain.FaxNo = txtFaxNo.Text;
                poMain.ContactPerson = txtContactPerson.Text;
                poMain.PhNo = txtPhNo.Text;
                poMain.ContactPerson1 = txtContactPerson1.Text;
                poMain.PhNo1 = txtPhNo1.Text;
                poMain.Address1 = txtAdd1.Text;
                poMain.Address2 = txtAdd2.Text;
                poMain.Address3 = txtAdd3.Text;
                poMain.Address4 = txtAdd4.Text;
                poMain.Address5 = txtAdd5.Text;
                poMain.Country = txtCountry.Text;
                poMain.Comment = txtComment.Text;
                if (txtTerm.Text != "")
                    poMain.Term = Convert.ToInt32(txtTerm.Text);
                else 
                    poMain.Term = 0;
                
                poMain.CreatedBy = userName;
                poMain.Received = true;

                int retOut = poMain.UpdateSupplierDetails(poMain);

                

                if (retOut == 1)
                {
                    ControlState();
                    lblUpdateMessage.Visible = true;
                    lblUpdateMessage.Text = "";
                    lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                    lblUpdateMessage.Text = "Data Successfully Updated";
                }

                else
                {
                    lblMessageError.Visible = true;
                    lblMessageError.Text = "";
                    lblMessageError.ForeColor = System.Drawing.Color.Red;
                    lblMessageError.Text = "Failed while Updating Data" + lblSupplierID.Text;

                }
            }

            catch (Exception ex)
            {
                MessageError = ex.Message.ToString();
                lblMessageError.Text = "";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Text = "<b>Your Transaction Failed: </b>" + MessageError;
            }
        }

        protected void btnDeleteSupplier_Click(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            try
            {
                poMain.SupplierID = Convert.ToInt32(lblSupplierID.Text);
                poMain.Received = false;
                poMain.CreatedBy = userName;

                int retOut = poMain.UpdateSupplierDetails(poMain);

                lblMessageError.Visible = true;

                  if (retOut == 1)
                {
                    ControlState();
                    lblUpdateMessage.Visible = true;
                    lblUpdateMessage.Text = "";
                    lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                    lblUpdateMessage.Text = "Data Successfully Deleted";
                }

                else
                {
                    lblMessageError.Text = "";
                    lblMessageError.ForeColor = System.Drawing.Color.Red;
                    lblMessageError.Text = "Data Deletion Failed " + lblSupplierID.Text;

                }
            }
            catch (Exception ex)
            {
                MessageError = ex.Message.ToString();
                lblMessageError.Text = "";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Text = "<b>Your Transaction Failed: </b>" + MessageError;
            }            
        }

        protected void btnCancelSupplier_Click(object sender, EventArgs e)
        {
            ControlState();
        }

        protected void btnInsertNewSupplier_Click(object sender, EventArgs e)
        {
             POMain poMain = new POMain();
             try
             {
                 poMain.SupplierName = txtNewSupplier.Text;
                 poMain.MainNo = txtNewMainNo.Text;
                 poMain.FaxNo = txtNewFaxNo.Text;
                 poMain.ContactPerson = txtNewContactPerson.Text;
                 poMain.PhNo = txtNewPnNo.Text;
                 poMain.ContactPerson1 = txtNewContactPerson1.Text;
                 poMain.PhNo1 = txtNewPnNo1.Text;
                 poMain.Address1 = txtNewAdd1.Text;
                 poMain.Address2 = txtNewAdd2.Text;
                 poMain.Address3 = txtNewAdd3.Text;
                 poMain.Address4 = txtNewAdd4.Text;
                 poMain.Address5 = txtNewAdd5.Text;
                 poMain.Country = txtNewCountry.Text;
                 poMain.Comment = txtNewComment.Text;
                 if (txtNewTerm.Text != "")
                     poMain.Term = Convert.ToInt32(txtNewTerm.Text);
                 else
                     poMain.Term = 0;

                 poMain.CreatedBy = userName;

                 int retOut = poMain.InsertSupplier(poMain);

                 if (retOut == 1)
                 {
                     lblUpdateMessage.Visible = true;
                     lblUpdateMessage.Text = "";
                     lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                     ControlState();
                     lblUpdateMessage.Text = "Data Successfully Inserted";
                 }

                 else
                 {
                     lblMessageError.Visible = true;
                     lblMessageError.Text = "";
                     lblMessageError.ForeColor = System.Drawing.Color.Red;
                     lblMessageError.Text = "Supplier Already Exist.....";
                 }

             }
             catch (Exception ex)
             {
 
             }
        }

        #endregion       

        #region BudgetCode

        [WebMethod]
        public static List<string> GetBudgetCode(string prefixText, int count)
        {
            DataSet ds = new DataSet();
            List<string> budgetCode = new List<string>();
            try
            {
                ds = new POMain().GetBudgetCodeExtender(prefixText);
                string itemToAdd = string.Empty;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    itemToAdd = dr["BudgetCode"].ToString();//AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["circuitid"].ToString(), dr["RecId"].ToString());
                    budgetCode.Add(itemToAdd);
                }
            }
            catch
            { }
            return budgetCode;
        }

        protected void btnSearchBudgetCode_Click(object sender, EventArgs e)
        {
            divBudgCode.Visible = true;
            lblUpdateMessage.Visible = false;
          
            DataSet ds = new DataSet();
            try
            {
                DataSet dsDept = new POSystemDataHandler().GetDepartment();
                ddlDepartment.DataSource = dsDept;
                ddlDepartment.DataBind();

                DataSet dsOrderClass = new POMain().GetOrderClassification(1);
                ddlOrderClassification.DataSource = dsOrderClass;
                ddlOrderClassification.DataBind();

                DataSet dsStation = new POSystemDataHandler().GetStation();
                ddlStation.DataSource = dsStation;
                ddlStation.DataBind();
                ddlStation.Items.Insert(0,new ListItem("--Select--", "0"));

                string budgetCode = txtSearchBudgetCode.Text;
                ds = new POMain().GetBudgetCodeDetails(budgetCode);
                lblBudgetCodeID.Text = ds.Tables[0].Rows[0]["BudgetCodeID"].ToString();
                txtBudgetCode.Text = ds.Tables[0].Rows[0]["BudgetCode"].ToString();
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["Department"].ToString();
                ddlOrderClassification.SelectedValue = ds.Tables[0].Rows[0]["OrderClassification"].ToString();
                ddlStation.SelectedValue = ds.Tables[0].Rows[0]["Station"] != DBNull.Value ? ds.Tables[0].Rows[0]["Station"].ToString() : "0"; 
                txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
               
            }

            catch (Exception ex)
            { }
        }

        protected void btnUpdateBudgetCode_Click(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            try
            {
                poMain.BudgetCodeID = Convert.ToInt32(lblBudgetCodeID.Text);               
                poMain.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
                poMain.OrderClassificationID = Convert.ToInt32(ddlOrderClassification.SelectedValue);
                poMain.StationID = ddlStation.SelectedValue != "0" ? Convert.ToInt32(ddlStation.SelectedValue) : 0;
                poMain.BudgetCodeDescription = txtDescription.Text;
                poMain.CreatedBy = userName;
                poMain.Received = true;

                int retOut = poMain.UpdateBudgetCode(poMain);

                if (retOut == 1)
                {
                    ControlState();
                    lblUpdateMessage.Visible = true;
                    lblUpdateMessage.Text = "";
                    lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                    lblUpdateMessage.Text = "Data Successfully Updated";
                }

                else
                {
                    lblMessageError.Visible = true;
                    lblMessageError.Text = "";
                    lblMessageError.ForeColor = System.Drawing.Color.Red;
                    lblMessageError.Text = "Failed while Updating Data" + lblBudgetCodeID.Text;
                }
            }

            catch (Exception ex)
            {
                MessageError = ex.Message.ToString();
                lblMessageError.Text = "";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Text = "<b>Your Transaction Failed: </b>" + MessageError;
            }
        }

        protected void btnDeleteBudgetCode_Click(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            try
            {
                poMain.BudgetCodeID = Convert.ToInt32(lblBudgetCodeID.Text);
                poMain.Received = false;
                poMain.CreatedBy = userName;

                int retOut = poMain.UpdateBudgetCode(poMain);
                lblMessageError.Visible = true;

                if (retOut == 1)
                {
                    ControlState();
                    lblUpdateMessage.Visible = true;
                    lblUpdateMessage.Text = "";
                    lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                    lblUpdateMessage.Text = "Data Successfully Deleted";
                }

                else
                {
                    lblMessageError.Text = "";
                    lblMessageError.ForeColor = System.Drawing.Color.Red;
                    lblMessageError.Text = "Data Deletion Failed " + lblBudgetCodeID.Text;

                }
            }

            catch (Exception ex)
            {
                MessageError = ex.Message.ToString();
                lblMessageError.Text = "";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Text = "<b>Your Transaction Failed: </b>" + MessageError;
            }
        }

        protected void btnCancelBudgetCode_Click(object sender, EventArgs e)
        {
            ControlState();
        }

        private void FillBudhetCodeInsertValues()
        {
            try
            {
                DataSet dsDept = new POSystemDataHandler().GetDepartment();
                ddlInsertDepartment.DataSource = dsDept;
                ddlInsertDepartment.DataBind();
                ddlInsertDepartment.Items.Insert(0, new ListItem("--Select--", "0"));

                DataSet dsOrderClass = new POMain().GetOrderClassification(1);
                ddlInsertOrderClassification.DataSource = dsOrderClass;
                ddlInsertOrderClassification.DataBind();
                ddlInsertOrderClassification.Items.Insert(0, new ListItem("--Select--", "0"));

                DataSet dsStation = new POSystemDataHandler().GetStation();
                ddlInsertStation.DataSource = dsStation;
                ddlInsertStation.DataBind();
                ddlInsertStation.Items.Insert(0, new ListItem("--Select--", "0"));
            }

            catch (Exception e)
            { }
        }
     
        protected void btnInsertNewBudgetCode_Click(object sender, EventArgs e)
        {
            POMain poMain = new POMain();
            try
            {
                poMain.BudgetCode = txtInsertBudgetCode.Text;
                poMain.DepartmentID = Convert.ToInt32(ddlInsertDepartment.SelectedValue);
                poMain.OrderClassificationID = Convert.ToInt32(ddlInsertOrderClassification.SelectedValue);
                poMain.StationID = ddlStation.SelectedValue != "0" ? Convert.ToInt32(ddlInsertStation.SelectedValue) : 0;
                poMain.BudgetCodeDescription = txtInsertDescription.Text;
                poMain.YearOfBudgetCode = Convert.ToInt32(ddlInsertYear.SelectedValue);
                poMain.CreatedBy = userName;

                int retOut = poMain.InsertBudgetCode(poMain);
                    
                if (retOut == 1)
                {
                    lblUpdateMessage.Visible = true;
                    lblUpdateMessage.Text = "";
                    lblUpdateMessage.ForeColor = System.Drawing.Color.Green;
                    ControlState();
                    lblUpdateMessage.Text = "Data Successfully Inserted";
                }

                else
                {
                    lblMessageError.Visible = true;
                    lblMessageError.Text = "";
                    lblMessageError.ForeColor = System.Drawing.Color.Red;
                    lblMessageError.Text = "Budget Code Already Exist.....";
                }
            }

            catch (Exception ex)
            { }

        }   

        #endregion 
    }
}