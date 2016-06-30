using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAL;
using BAL;
using System.IO;
using System.Web.Services;

namespace POManagementASPNet
{
    public partial class Search1 : System.Web.UI.Page
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                try
                {
                    GetRequester();
                    FillStation();
                    FillShipTo();
                    FillSuppliers();
                    FillDepartment();
                    FillProject();
                    lnkAdvExportToExcel.Visible = false;

                    if(!string.IsNullOrEmpty(Request["E"]))
                    {
                        string queryStringE = Convert.ToString(Request["E"]);
                        Int64 poID = Convert.ToInt64(queryStringE);
                        DataSet dsPOData = new DataSet();
                        dsPOData = new POMain().BindPOData(poID);
                        if (dsPOData.Tables[0].Rows.Count != 0)
                        {
                            grdSearchPO.DataSource = dsPOData;
                            grdSearchPO.DataBind();
                            pnlAdvanceSearch.Visible = false;
                            chkSearchPObyPartDescription.Visible = false;
                            pnlAdvanceSearchByPartDescription.Visible = false;
                            lnkAdvExportToExcel.Visible = false;
                            grdSearchPO.Visible = true;
                        }

                        else
                        {
                            label1.Text = "The PO Number " + poID + " does not exist";
                            
                            pnlAdvanceSearch.Visible = false;
                            chkSearchPObyPartDescription.Visible = false;
                            pnlAdvanceSearchByPartDescription.Visible = false;
                            lnkAdvExportToExcel.Visible = false;
                        }
                    }
                }
                catch(Exception ex)
                {
                    Response.Write(ex.Message);
                }
        }

        protected void btnAdvSearchPO_Click(object sender, EventArgs e)
        {
            int? requester = Convert.ToInt32(drpAdvSearchRequester.SelectedValue);
            if (requester == 0)
                requester = null;
            int? station = Convert.ToInt32(drpAdvSearchStation.SelectedValue);
            if (station == 0)
                station = null;
            int? shipTo = Convert.ToInt32(drpAdvSearchShipTo.SelectedValue);
            if (shipTo == 0)
                shipTo = null;
            int? supplier = Convert.ToInt32(drpAdvSearchSupplier.SelectedValue);
            if (supplier == 0)
                supplier = null;
            int? dept = Convert.ToInt32(drpAdvSearchDepartment.SelectedValue);
            if (dept == 0)
                dept = null;
            int? proj = Convert.ToInt32(drpAdvSearchProject.SelectedValue);
            if(proj ==0)
                proj=null;
            string comment = txtAdvSearchComment.Text;
            if (comment == "")
                comment = null;
            string circuitID = txtCircuit.Text;
            if (circuitID == "")
                circuitID = null;

            DataSet dsPO = new POMain().GetPOByAdvSearch(requester, station, shipTo, supplier, dept, proj, comment, circuitID);
            ViewState["vwExportTable"] = dsPO;
            if (dsPO.Tables[0].Rows.Count > 0)
            {
                grdSearchPO.DataSource = dsPO;
                grdSearchPO.DataBind();
                lnkAdvExportToExcel.Visible = true;
            }

            else
            {
                lblAdvanceSearchMessage.Text = "No Data Available";
                grdSearchPO.DataSource = null;
                grdSearchPO.DataBind();
            }
        }

        protected void chkSearchPObyPartDescription_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSearchPObyPartDescription.Checked)
            {
                pnlAdvanceSearch.Enabled = false;
                pnlAdvanceSearchByPartDescription.Enabled = true;
            }
            else
            {
                pnlAdvanceSearch.Enabled = true;
                pnlAdvanceSearchByPartDescription.Enabled = false;
            }
            grdSearchPO.DataSource = null;
            grdSearchPO.DataBind();
            lblAdvanceSearchMessage.Text = string.Empty;
            lnkAdvExportToExcel.Visible = false;
        }

        protected void lnkAdvExportToExcel_Click(object sender, EventArgs e)
        {
            DataTable dtExport = new DataTable();
            string strReportName = DateTime.Now.ToString("dd MMMM yyyy");
            if (grdSearchPO.Rows.Count <= 0)
            {
                if (ViewState["vwExportTable"] != null)
                {
                    dtExport = (DataTable)ViewState["vwExportTable"];
                }
                grdSearchPO.DataSource = dtExport;
                grdSearchPO.DataBind();
            }

            grdSearchPO.Columns[0].Visible = false;
            string title = string.Empty;
            string totalString = string.Empty;
            string attachment = string.Empty;

            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", strReportName + ".xls"));
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a table to contain the grid  
                    Table table = new Table();

                    //Adding Header
                    if (grdSearchPO.HeaderRow != null)
                    {
                        GridViewExportUtil.PrepareControlForExport(grdSearchPO.HeaderRow);
                        table.Rows.Add(grdSearchPO.HeaderRow);
                    }
                    //  add each of the data rows to the table  
                    foreach (GridViewRow row in grdSearchPO.Rows)
                    {
                        GridViewExportUtil.PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }
                    //  render the table into the htmlwriter  
                    table.RenderControl(htw);
                    //  render the htmlwriter into the response
                    System.Web.HttpContext.Current.Response.Write(sw.ToString());
                    System.Web.HttpContext.Current.Response.End();
                    //System.Web.HttpContext.Current.Response.Flush();                    
                }
            }
        }

        protected void btnAdvSearchPOByPart_Click(object sender, EventArgs e)
        {
            string descOfParts = txtAdvSearchDecriptionofPart.Text;
            string supplierCode = txtAdvSearchSuppliersCode.Text;
            if (descOfParts == "")
                descOfParts = null;
            if (supplierCode == "")
                supplierCode = null;

            DataSet dsPO = new POItem().GetPOByAdvSearchByDescOfParts(descOfParts, supplierCode);
            ViewState["vwExportTable"] = dsPO;
            if (dsPO.Tables[0].Rows.Count > 0)
            {
                grdSearchPO.DataSource = dsPO;
                grdSearchPO.DataBind();
                lnkAdvExportToExcel.Visible = true;
            }

            else
            {
                lblAdvanceSearchMessage.Text = "No Data Available";
                grdSearchPO.DataSource = null;
                grdSearchPO.DataBind();
            }
        }      

        protected void grdSearchPO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Int64 intSearchPONumber;

            if (e.CommandName != "Page")
            {
                GridViewRow grdrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                intSearchPONumber = Convert.ToInt32(grdrow.Cells[1].Text);
                //ViewState["SearchPO"] = grdrow.Cells[1].Text;

                //UIUtility.TryParseDakaKeyValue(ref grdSearchPO, grdrow.RowIndex, "POMainID", out intSearchPONumber);

                if (e.CommandName == "Edit")
                {
                    Response.Redirect("EditPO.aspx?E=" + intSearchPONumber);
                }
                else if (e.CommandName == "Approve")
                {                    
                    Response.Redirect("ApprovePO.aspx?E=" + intSearchPONumber);
                }
                else if (e.CommandName == "Closed")
                {
                    Response.Redirect("ClosePO.aspx?E=" + intSearchPONumber);
                }
                //ViewState["SearchPO"] = null;
            }
        }

        protected void grdSearchPO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               string status= e.Row.Cells[6].Text.ToString();
               try
               {
                   if (status == "Approved" || status == "Rejected" || status == "Cancelled" || status == "Closed" || status == "Invoiced")
                   {
                       ((LinkButton)(e.Row.FindControl("lnkSearchEdit"))).Visible = false;
                       ((LinkButton)(e.Row.FindControl("lnkSearchApprove"))).Visible = false;
                   }

                   else
                       ((LinkButton)(e.Row.FindControl("lnkSearchClosed"))).Visible = false;
               }
               catch { }
            }
        }      

        private void GetRequester()
        {
            DataSet dsRequester = new POSystemDataHandler().GetRequester();
            drpAdvSearchRequester.DataSource = dsRequester;
            drpAdvSearchRequester.DataBind();

            drpAdvSearchRequester.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void FillStation()
        {
            DataSet dsStation = new POSystemDataHandler().GetStation();
            drpAdvSearchStation.DataSource = dsStation;
            drpAdvSearchStation.DataBind();

            drpAdvSearchStation.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void FillShipTo()
        {
            DataSet dsShip = new POSystemDataHandler().GetShipTo();
            drpAdvSearchShipTo.DataSource = dsShip;
            drpAdvSearchShipTo.DataBind();

            drpAdvSearchShipTo.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void FillSuppliers()
        {
            DataSet dsSuppliers = new POSystemDataHandler().GetSupplier();
            drpAdvSearchSupplier.DataSource = dsSuppliers;
            drpAdvSearchSupplier.DataBind();

            drpAdvSearchSupplier.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void FillDepartment()
        {
            DataSet dsDept = new POSystemDataHandler().GetDepartment();
            drpAdvSearchDepartment.DataSource = dsDept;
            drpAdvSearchDepartment.DataBind();

            drpAdvSearchDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void FillProject()
        {
            DataSet dsProject = new POSystemDataHandler().GetProject();
            drpAdvSearchProject.DataSource = dsProject;
            drpAdvSearchProject.DataBind();

            drpAdvSearchProject.Items.Insert(0, new ListItem("--Select--", "0"));
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
    }
}