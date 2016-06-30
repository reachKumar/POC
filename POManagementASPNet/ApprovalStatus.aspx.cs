using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;

namespace POManagementASPNet
{
    public partial class ApprovalStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.IsPostBack)
            //{
                if (!string.IsNullOrEmpty(Request["ID"]))
                {
                    string queryStringE = Convert.ToString(Request["ID"]);
                    Int64 poID = Convert.ToInt64(queryStringE);
                    ShowApprovalStatus(poID);
                }
            //}
        }

        private void ShowApprovalStatus(Int64 poNumber)
        {
            DataSet dsPOData = new POMain().GetApprovalRecords(poNumber);
            grdApprovalStatus.DataSource = dsPOData;
            grdApprovalStatus.DataBind();
        }
    }
}