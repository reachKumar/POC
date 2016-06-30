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
    public partial class PrintPO : System.Web.UI.Page
    {        
        double grdItemsTotal = 0;       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    BindConditions();
                    BindShipTerm();                  
                    if (!string.IsNullOrEmpty(Request["EP"]))
                    {                        
                        string queryStringE = Convert.ToString(Request["EP"]);
                        Int64 poID = Convert.ToInt64(queryStringE);
                        ViewState["EditPONumber"] = poID;
                        lblPrintPONumber.Text = poID.ToString();
                        BindPOData(poID);                       
                    }
                }

                catch { }
            }
        }

        protected void drpPrintAddressDroppdown_SelectedIndexChanged(object sender, EventArgs e)
        {           
            string billto = string.Empty;
            if (drpPrintAddressDroppdown.SelectedValue == "VAT")
            {
                DataSet dsShipToDetails = new POMain().GetShipToDetailsByShipToId(Convert.ToInt32(ViewState["ShipTo"]));
                string billTo = dsShipToDetails.Tables[0].Rows[0]["VAT"].ToString();
                billTo = billTo.Replace("  ", "/r/n");
                //billTo = billTo.Replace("\n\n", "<br />");
                lblBillTo.Text = "Bill To:<br />" + billTo;
            }

            else
            {
                DataSet dsAddnBillingAdd = new POMain().GetAdditionalBillingAddress(Convert.ToInt32(ViewState["ShipTo"]));
                string addnBillingAdd = dsAddnBillingAdd.Tables[0].Rows[0]["NewAddress"].ToString();
                addnBillingAdd = addnBillingAdd.Replace("  ", "/r/n");
                //addnBillingAdd = addnBillingAdd.Replace("\n\n", "<br />");
                lblBillTo.Text = "Bill To:<br />" + addnBillingAdd;
            }
        }

        protected void grdPrintPOItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double rowTotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TotalPrice"));
                grdItemsTotal += rowTotal;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total:";
                e.Row.Cells[5].Text = String.Format("{0:0,0.00}", grdItemsTotal);
                grdItemsTotal = 0;
            }
        }

        private void BindPOData(Int64 poId)
        {
            DataSet dsPOData = new DataSet();
            dsPOData = new POMain().BindPOData(Convert.ToInt64(ViewState["EditPONumber"]));              
            DataTable dt1 = null;
            try
            {
                lblRequesterName.Text = dsPOData.Tables[0].Rows[0]["RequesterName"].ToString();
                string currency = dsPOData.Tables[0].Rows[0]["CurrencyName"].ToString();               
                dt1 = dsPOData.Tables[1].Copy();
                dt1.Columns.Add("Currency");
                foreach (DataRow row in dt1.Rows)
                {                   
                    row["Currency"] = currency;
                }             

                int supplier = Convert.ToInt32(dsPOData.Tables[0].Rows[0]["Supplier"]);
                BindSuppliersDetails(supplier);

                int shipTo = Convert.ToInt32(dsPOData.Tables[0].Rows[0]["ShipTo"]);
                ViewState["ShipTo"] = shipTo;
                BindShipToDetails(shipTo);

                lblPrintPODateEntered.Text = Convert.ToDateTime(dsPOData.Tables[0].Rows[0]["CreatedDate"]).ToString("yyyy-MM-dd");

                grdPrintPOItems.DataSource = dt1;
                grdPrintPOItems.DataBind();
            }
            catch (Exception ex)
            { }
        }
        private void BindConditions()
        {
            DataSet dsConditions = new POMain().BindConditions();
            drpTerms.DataSource = dsConditions;
            drpTerms.DataBind();
        }

        private void BindShipTerm()
        {
            DataSet dsShipTerm = new POMain().BindShipTerm();
            drpShippingTerms.DataSource = dsShipTerm;
            drpShippingTerms.DataBind();

            drpShippingTerms.Items.Insert(0, new ListItem("", "0"));
        }

        private void BindSuppliersDetails(int supplier)
        {
            DataSet dsSupplierDetails = new POMain().GetSupplierDetails(supplier);
            string supplierDetails = string.Empty;

            supplierDetails += dsSupplierDetails.Tables[0].Rows[0]["CompanyName"].ToString() + "<br />";
            if (dsSupplierDetails.Tables[0].Rows[0]["Address1"].ToString() != null)
                supplierDetails += dsSupplierDetails.Tables[0].Rows[0]["Address1"].ToString() + "<br />";
            if (dsSupplierDetails.Tables[0].Rows[0]["Address2"].ToString() != null)
                supplierDetails += dsSupplierDetails.Tables[0].Rows[0]["Address2"].ToString() + "<br />";
            if (dsSupplierDetails.Tables[0].Rows[0]["Address3"].ToString() != null)
                supplierDetails += dsSupplierDetails.Tables[0].Rows[0]["Address3"].ToString() + "<br />";
            if (dsSupplierDetails.Tables[0].Rows[0]["Address4"].ToString() != null)
                supplierDetails += dsSupplierDetails.Tables[0].Rows[0]["Address4"].ToString() + "<br />";
            if (dsSupplierDetails.Tables[0].Rows[0]["Address5"].ToString() != null)
                supplierDetails += dsSupplierDetails.Tables[0].Rows[0]["Address5"].ToString() + "<br />";

            lblPrintPOSupplier.Text = supplierDetails;

            if (dsSupplierDetails.Tables[0].Rows[0]["MainNumber"].ToString() != null)
                lblPrintPOTelNumber.Text = dsSupplierDetails.Tables[0].Rows[0]["MainNumber"].ToString();
            if (dsSupplierDetails.Tables[0].Rows[0]["FaxNumber"].ToString() != null)
                lblPrintPOFaxNumber.Text = dsSupplierDetails.Tables[0].Rows[0]["FaxNumber"].ToString();
        }

        private void BindShipToDetails(int shipTo)
        {
            DataSet dsShipToDetails = new POMain().GetShipToDetailsByShipToId(shipTo);
            string address = dsShipToDetails.Tables[0].Rows[0]["Address"].ToString();
            address = address.Replace("<br /><br />", "/r/n");
            //address = address.Replace("\n\n", "<br />");            

            lblPrintPODeliverTo.Text += address;//"<br />" + address;

            DataSet dsAddnBillingAdd = new POMain().GetAdditionalBillingAddress(shipTo);
            if (dsAddnBillingAdd.Tables[0].Rows.Count != 0)
            {

                string addnBillingAdd = dsAddnBillingAdd.Tables[0].Rows[0]["NewAddress"].ToString();

                if (addnBillingAdd != "")
                {
                    pnlPrintAddressDroppdown.Visible = true;
                    drpPrintAddressDroppdown.SelectedIndex = 0;
                }
                else
                    pnlPrintAddressDroppdown.Visible = false;
            }

            string billTo = dsShipToDetails.Tables[0].Rows[0]["VAT"].ToString();
            billTo = billTo.Trim().Replace("  ", "\n");
            //billTo = billTo.Replace("\n\n", "<br />");
            lblBillTo.Text = "Bill To:<br />" + billTo;

        }

        protected void drpTerms_SelectedIndexChanged(object sender, EventArgs e)
        {          
            drpTerms.SelectedItem.Text.ToString();            
        }

        protected void drpShippingTerms_SelectedIndexChanged(object sender, EventArgs e)
        {           
            drpShippingTerms.SelectedItem.Text.ToString();
        }     

    }
}