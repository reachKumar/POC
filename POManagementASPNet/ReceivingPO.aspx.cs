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
    public partial class ReceivingPO : System.Web.UI.Page
    {
        string userName;
        string userEmail;

        //static bool isInvoiced = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            userName = Session["PO_UserName"].ToString();
            userEmail = Session["UserEmail"].ToString();

            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["E"]))
                {
                    string queryStringE = Convert.ToString(Request["E"]);
                    Int64 poID = Convert.ToInt64(queryStringE);
                    BindPOData(poID);
                }

                if (!string.IsNullOrEmpty(Request["I"]))
                {
                    bool isInvoiced = true;
                    ViewState["IsInvoiced"] = isInvoiced;
                    string queryStringE = Convert.ToString(Request["I"]);
                    Int64 poID = Convert.ToInt64(queryStringE);
                    BindPOData(poID);

                    if (chkRReceived.Checked == true && chkInvoicedMain.Checked!=true)
                    {
                        btnUpdateReceivedPO.Visible = true;
                        pnlInvoice.Visible = true;
                    }
                    else 
                    {
                        btnUpdateReceivedPO.Visible = false;
                        pnlInvoice.Visible = false;
                    }
                }
            }
        }

        private void BindPOData(Int64 poID)
        {
            DataSet dsPOData = new DataSet();
            dsPOData = new POMain().BindPOData(poID);

            ViewState["ReceivePOItems"] = dsPOData.Tables[1];

            lblRPONumber.Text = poID.ToString();
            lblRRequester.Text = dsPOData.Tables[0].Rows[0]["RequesterName"].ToString();
            lblROrderClassification.Text = dsPOData.Tables[0].Rows[0]["OrderClassificationName"].ToString();
            lblRStation.Text = dsPOData.Tables[0].Rows[0]["StationName"].ToString();
            lblRDepartment.Text = dsPOData.Tables[0].Rows[0]["DepartmentName"].ToString();
            lblRType.Text = dsPOData.Tables[0].Rows[0]["TypeDescription"].ToString();
            lblRProject.Text = dsPOData.Tables[0].Rows[0]["ProjectName"].ToString();
            lblRDateApproved.Text = dsPOData.Tables[0].Rows[0]["ApprovedDate"].ToString();
            lblRDateReceived.Text = dsPOData.Tables[0].Rows[0]["ReceivedDate"].ToString();
            //ddlGlCode.SelectedItem.Text = dsPOData.Tables[0].Rows[0]["GLCodeName"].ToString();
            lblRShipTo.Text = dsPOData.Tables[0].Rows[0]["City"].ToString();
            lblRStatus.Text = dsPOData.Tables[0].Rows[0]["Status"].ToString();
            if (lblRStatus.Text == "Closed")
            {
                chkRReceived.Checked = true;
                btnUpdateReceivedPO.Visible = false;
            }

            if (lblRStatus.Text == "Invoiced")
            {
                chkRReceived.Checked = true;
                //chkInvoiced.Checked = true;
                chkInvoicedMain.Checked = true;
            }

            lblRSupplier.Text = dsPOData.Tables[0].Rows[0]["CompanyName"].ToString();
            //lblTermText.Text = dsPOData.Tables[0].Rows[0]["Term"].ToString();
            lblRApprovedBy.Text = dsPOData.Tables[0].Rows[0]["ModifiedBy"].ToString();
            lblRComment.Text = dsPOData.Tables[0].Rows[0]["Comment"].ToString();
            lblRInBudget.Text = dsPOData.Tables[0].Rows[0]["FieldName"].ToString();
            lblCurrency.Text = dsPOData.Tables[0].Rows[0]["CurrencyName"].ToString();

            grdReceivingPOItems.DataSource = dsPOData.Tables[1];
            grdReceivingPOItems.DataBind();
        }

        protected void grdReceivingPOItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {
                GridViewRow grdrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            }
        }

        protected void grdReceivingPOItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = (DataTable)(ViewState["ReceivePOItems"]);
                foreach (DataRow row in dt.Rows)
                {
                    CheckBox chk = (CheckBox)(e.Row.FindControl("chkReceived"));
                    TextBox txtDate = (TextBox)(e.Row.FindControl("txtReceivedDate"));
                    TextBox txtRecQty = (TextBox)(e.Row.FindControl("txtReceivedQty"));

                    if (grdReceivingPOItems.DataKeys[e.Row.RowIndex].Value.ToString() == row["POItemID"].ToString())
                    {
                        if (row["Recieved"].ToString() == "True")
                        {
                            chk.Checked = true;
                            chk.Enabled = false;
                            txtDate.Text = Convert.ToDateTime(row["DateRecieved"]).ToString("dd-MMM-yyyy");
                            txtDate.Enabled = false;
                            txtRecQty.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void btnUpdateReceivedPO_Click(object sender, EventArgs e)
        {
            if (! Convert.ToBoolean(ViewState["IsInvoiced"]))
            {
                List<POItem> poItems = new List<POItem>();
                POItem poItem = null;
                bool allPOReceived = false;
                //int i = 0;          
                //DateTime lastItemDate = DateTime.Now;
                foreach (GridViewRow row in grdReceivingPOItems.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        poItem = new POItem();
                        decimal receivedQty = 0.0M;
                        Int64 poItemID;

                        UIUtility.TryParseDakaKeyValue(ref grdReceivingPOItems, row.RowIndex, "POItemID", out poItemID);
                        poItem.ItemID = poItemID;
                        poItem.POReceivedDate = ((TextBox)row.FindControl("txtReceivedDate")).Text!= "" ? Convert.ToDateTime(((TextBox)row.FindControl("txtReceivedDate")).Text):System.DateTime.Now;
                        poItem.POReceived = ((CheckBox)row.FindControl("chkReceived")).Checked;

                        string receiveqtyStr = ((TextBox)row.FindControl("txtReceivedQty")).Text;
                        if (receiveqtyStr == "")
                        {
                            receivedQty = 0.0M;
                        }
                        else
                        {
                            receivedQty = Convert.ToDecimal(((TextBox)row.FindControl("txtReceivedQty")).Text);
                        }

                        poItem.QuantityReceived = Convert.ToDecimal(row.Cells[9].Text) + receivedQty;
                        poItems.Add(poItem);
                    }
                }

                foreach (GridViewRow row in grdReceivingPOItems.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        bool poReceived = ((CheckBox)row.FindControl("chkReceived")).Checked;
                        if (poReceived)
                            allPOReceived = true;
                        else
                        {
                            allPOReceived = false;
                            break;
                        }
                    }
                }

                poItem.UpdatePOReceiving(poItems, allPOReceived, userName);
            }
            else
            {
                bool isInvoicedChkd = chkInvoiced.Checked;
                Int64 poNumber = Convert.ToInt64(lblRPONumber.Text);
                if (isInvoicedChkd)
                {
                    DateTime invoiceDate = Convert.ToDateTime(txtPOInvoiceDate.Text);
                    new POMain().UpdatePOInvoicing(poNumber, invoiceDate, userName);
                }
            }

            Response.Redirect("ClosePO.aspx");
        }

        //protected void calReceivedDate_SelectionChanged(object sender, EventArgs e)
        //{
        //    Calendar calendar = (Calendar)sender;
        //    TextBox txtDate = (TextBox)((GridViewRow)calendar.Parent.Parent).FindControl("txtReceivedDate");
        //    txtDate.Text = calendar.SelectedDate.ToString("dd-MMM-yyyy");
        //    calendar.Visible = false;
        //}

        //protected void btnCalender_Click(object sender, ImageClickEventArgs e)
        //{
        //    ImageButton button = (ImageButton)sender;
        //    Calendar calendar = (Calendar)((GridViewRow)button.Parent.Parent).FindControl("calReceivedDate");
        //    if (calendar.Visible)
        //        calendar.Visible = false;
        //    else
        //        calendar.Visible = true;
        //}

        //protected void btnPOCalender_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (calPOReceivedDate.Visible == false)
        //        calPOReceivedDate.Visible = true;
        //    else if (calPOReceivedDate.Visible == true)
        //        calPOReceivedDate.Visible = false;
        //}

        //protected void calPOReceivedDate_SelectionChanged(object sender, EventArgs e)
        //{
        //    txtPOReceivedDate.Text = calPOReceivedDate.SelectedDate.ToString("dd-MMM-yyyy");
        //    calPOReceivedDate.Visible = false;
        //}

        protected void chkInvoiced_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInvoiced.Checked)
            {
                //lblPOReceivedDate.Visible = true;
                //txtPOReceivedDate.Visible = true;
                //btnPOCalender.Visible = true;
                pnlPOReceivedDate.Visible = true;
                txtPOInvoiceDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            else
            {
                pnlPOReceivedDate.Visible = false;
                //lblPOReceivedDate.Visible = true;
                //txtPOReceivedDate.Visible = true;
                //btnPOCalender.Visible = true;
            }
        }
    }
}