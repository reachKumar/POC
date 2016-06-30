using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.Common;

namespace DAL
{
    public class POSystemDataHandler : CommonDataHandler
    {
        public DataSet GetRequester()
        {
            return this.DefaultDB.ExecuteDataSet("Sel_Requester");
        }

        public DataSet GetOrderClassification(int poType)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_OrderClassification", poType);
        }

        public DataSet GetStation()
        {
            return this.DefaultDB.ExecuteDataSet("Sel_Station");
        }

        public DataSet GetProject()
        {
            return this.DefaultDB.ExecuteDataSet("Sel_Projects");
        }

        public DataSet GetDepartment()
        {
            return this.DefaultDB.ExecuteDataSet("Sel_Department");
        }

        public DataSet GetGLCode(int orderClassification)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_GLCode",orderClassification);
        }

        public DataSet GetShipTo()
        {
            return this.DefaultDB.ExecuteDataSet("Sel_ShipTo");
        }

        public DataSet GetType()
        {
            return this.DefaultDB.ExecuteDataSet("Sel_Type");
        }

        public DataSet GetSupplier()
        {
            return this.DefaultDB.ExecuteDataSet("Sel_Suppliers");
        }

        public int GetTerm(int supplierID)
        {
            int returnValue = -1;
            string sql = "Sel_TermForSupplier";
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand(sql);

            this.DefaultDB.AddInParameter(cmd, "SupplierID", DbType.Int64, supplierID);
            this.DefaultDB.AddOutParameter(cmd, "Term", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            returnValue = Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "Term"));

            return returnValue;
        }

        public DataSet GetBuggetedField()
        {
            return this.DefaultDB.ExecuteDataSet("Sel_BudgetedFields");
        }

        public object GetGLCodeID(string glCodeName)
        {
            return this.DefaultDB.ExecuteScalar("Sel_GLCodeID", glCodeName);
           
        }

        public DataSet GetCurrency()
        {
            return this.DefaultDB.ExecuteDataSet("Sel_Currency");
        }

        public int InsertPOData(string poDataXML, Int64 poNo, int requester, int orderClassification, int station, int dept, int poType, int project, int shipTo, string comment, int supplier, int budgeted, int currency, double totalValue, double totalValueInUSD, string userName, int nrc, int mrc, int tail, string circuitID, DateTime estDeliveryTime, string supervisor,string opportunity, Int32 budgetCode, ref Int64 poNumber)
        {
            int returnValue = -1;
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Ins_POData");
            this.DefaultDB.AddInParameter(cmd, "XMLDoc", DbType.Xml, poDataXML);
            this.DefaultDB.AddInParameter(cmd, "PONumber", DbType.Int64, poNo);
            this.DefaultDB.AddInParameter(cmd, "Requester", DbType.Int32, requester);
            this.DefaultDB.AddInParameter(cmd, "OrderClassification", DbType.Int32, orderClassification);
            this.DefaultDB.AddInParameter(cmd, "Station", DbType.Int32, station);
            this.DefaultDB.AddInParameter(cmd, "Department", DbType.Int32, dept);
            this.DefaultDB.AddInParameter(cmd, "POType", DbType.Int32, poType);
            this.DefaultDB.AddInParameter(cmd, "Project", DbType.Int32, project);
           // this.DefaultDB.AddInParameter(cmd, "GLCode", DbType.Int32, glCode);
            this.DefaultDB.AddInParameter(cmd, "ShipTo", DbType.Int32, shipTo);
            this.DefaultDB.AddInParameter(cmd, "Comment", DbType.String, comment);
            this.DefaultDB.AddInParameter(cmd, "Supplier", DbType.Int32, supplier);
            this.DefaultDB.AddInParameter(cmd, "Budgeted", DbType.Int32, budgeted);
            this.DefaultDB.AddInParameter(cmd, "Currency", DbType.Int32, currency);
            this.DefaultDB.AddInParameter(cmd, "TotalValue", DbType.Double, totalValue);
            this.DefaultDB.AddInParameter(cmd, "TotalValueInUSD", DbType.Double, totalValueInUSD);

            this.DefaultDB.AddInParameter(cmd, "CreatedBy", DbType.String, userName);
            this.DefaultDB.AddInParameter(cmd, "NRC", DbType.Int32, nrc);
            this.DefaultDB.AddInParameter(cmd, "MRC", DbType.Int32, mrc);
            this.DefaultDB.AddInParameter(cmd, "Tail", DbType.Int32, tail);
            this.DefaultDB.AddInParameter(cmd, "CircuitID", DbType.String, circuitID);
            this.DefaultDB.AddInParameter(cmd, "EstimatedDeliveryTime", DbType.Date, estDeliveryTime);
            this.DefaultDB.AddInParameter(cmd, "Supervisor", DbType.String, supervisor);
            this.DefaultDB.AddInParameter(cmd, "Opportunity", DbType.String, opportunity);
            this.DefaultDB.AddInParameter(cmd, "BudgetCode", DbType.Int32, budgetCode);

            this.DefaultDB.AddOutParameter(cmd, "ErrorCode", DbType.Int32, 4);
            this.DefaultDB.AddOutParameter(cmd, "PON", DbType.Int64, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            returnValue = Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "ErrorCode"));
            poNumber = Convert.ToInt64(this.DefaultDB.GetParameterValue(cmd, "PON"));
            return returnValue;
        }

        public DataSet FillEditPO(string createdBy)
        {
            return this.DefaultDB.ExecuteDataSet("SelPOForEdit", createdBy);
        }

        public DataSet BindPOData(Int64 poNo)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_PODataByPONumber", poNo);
        }

        public int UpdatePOToApprove(Int64 poNumber, string status1, string comment, string empName, double poValueUSD)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Upd_PONumberToApprove");
            this.DefaultDB.AddInParameter(cmd, "PONumber", DbType.Int64, poNumber);
            this.DefaultDB.AddInParameter(cmd, "Status1", DbType.String, status1);
            this.DefaultDB.AddInParameter(cmd, "Comment", DbType.String, comment);
            this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);
            this.DefaultDB.AddInParameter(cmd, "POValueUSD", DbType.Double, poValueUSD);

            this.DefaultDB.AddOutParameter(cmd, "ErrorCode", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "ErrorCode"));
        }

        public int UpdatePOToSupApprove(Int64 poNumber, string status1, string comment,  string empName)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Upd_PONumberToSupervisorApprove");
            this.DefaultDB.AddInParameter(cmd, "PONumber", DbType.Int64, poNumber);
            this.DefaultDB.AddInParameter(cmd, "Status1", DbType.String, status1);
            this.DefaultDB.AddInParameter(cmd, "Comment", DbType.String, comment);
            this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);

            this.DefaultDB.AddOutParameter(cmd, "ErrorCode", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "ErrorCode"));
        }

        public int UpdatePOToReject(Int64 poNumber, string comment,  string empName)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Upd_PONumberToReject");
            this.DefaultDB.AddInParameter(cmd, "PONumber", DbType.Int64, poNumber);
            this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);
            this.DefaultDB.AddInParameter(cmd, "Comment", DbType.String, comment);

            this.DefaultDB.AddOutParameter(cmd, "ErrorCode", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "ErrorCode"));
        }

        public int UpdatePOToPreApprove(Int64 poNumber, string comment, string empName)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Upd_POToPreApproved");
            this.DefaultDB.AddInParameter(cmd, "PONumber", DbType.Int64, poNumber);
            this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);
            this.DefaultDB.AddInParameter(cmd, "Comment", DbType.String, comment);
            this.DefaultDB.AddOutParameter(cmd, "ErrorCode", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "ErrorCode"));
        }


        //Newly added for Jan'14 change
        public int UpdatePOToHODApproved(Int64 poNumber, string comment, string empName)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Upd_POToHODApproved");
            this.DefaultDB.AddInParameter(cmd, "PONumber", DbType.Int64, poNumber);
            this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);
            this.DefaultDB.AddInParameter(cmd, "Comment", DbType.String, comment);
            this.DefaultDB.AddOutParameter(cmd, "ErrorCode", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "ErrorCode"));
        }

        public DataSet GetPOForClosed()
        {
            return this.DefaultDB.ExecuteDataSet("Sel_POForClosed");
        }

        public int UpdatePOToReset(Int64 poNumber, string empName)
        {
            return this.DefaultDB.ExecuteNonQuery("Upd_PONumberToReset", poNumber, empName);
        }

        public int UpdatePOReceiving(string receivingPOXML, bool allPOReceived, string empName)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Upd_POReceiving");
            this.DefaultDB.AddInParameter(cmd, "XMLDoc", DbType.Xml, receivingPOXML);
            this.DefaultDB.AddInParameter(cmd, "POReceived", DbType.Boolean, allPOReceived);
            this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);

            this.DefaultDB.AddOutParameter(cmd, "ErrorCode", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);
            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "ErrorCode"));
        }

        public int DeletePO(Int64 poNumber, string empName)
        {
            return this.DefaultDB.ExecuteNonQuery("Del_POData", poNumber,empName);
        }

        public int UpdatePOInvoicing(Int64 poNumber, DateTime invoiceDate, string empName)
        {
            return this.DefaultDB.ExecuteNonQuery("Upd_POInvoicing", poNumber, invoiceDate, empName);
        }

        public DataSet BindConditions()
        {
            return this.DefaultDB.ExecuteDataSet("Sel_Conditions");
        }

        public DataSet BindShipTerm()
        {
            return this.DefaultDB.ExecuteDataSet("Sel_ShipTerm");
        }

        public DataSet GetSupplierDetails(int supplierId)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_SupplierDetailsByID", supplierId);
        }

        public DataSet GetApproverList(int roleID,int? deptID)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_MailingToList", roleID, deptID);
        }

        public DataSet GetRequesterList(int deptID)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_RequesterList", deptID);
        }

        public DataSet GetPOByAdvSearch(int? requester, int? station, int? shipTo, int? supplier, int? dept, int? project, string comment, string circuitID)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_POAfterAdvSearch", requester, station, shipTo, supplier, dept, project, comment, circuitID);
        }

        public DataSet GetPOByAdvSearchByDescOfParts(string descOfParts, string supplierCode)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_POAdvSearchByDescOfPartsOrSuppCode", descOfParts, supplierCode);
        }

        public DataSet GetShipToDetailsByShipToId(int shipTo)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_ShipToDetailsByShipID", shipTo);
        }

        public DataSet GetPOForHODApproval(string deptIDs, string userName)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_POForHODApproval", deptIDs, userName);
        }

        //For SD HOD approval
        public DataSet GetPOForSDHODApproval(string deptIDs, string userName)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_POForSDHODApproval", deptIDs, userName);
        }

        //For Tail/Circuit/Cross-Connect PO HOD 
        public DataSet GetPOForTailCircuitCrossHODApproval(string deptIDs, string userName)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_POForTailCircuitCrossHODApproval", deptIDs, userName);
        }

        public DataSet GetPOForPreApproval(string deptIDs, string userName)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_POForPreApproval", deptIDs, userName);
        }

        public DataSet GetPOForDeputyApproval(string userName)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_POForDeputyApprover", userName);
        }

        public DataSet GetAdditionalBillingAddress(int city)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_AdditionalBillingAddress", city);
        } 

        public DataSet GetCircuitID(string prefixText)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_CircuitID", prefixText);
        }

        public DataSet GetOpportunityID(string prefixText)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_OpportunityID", prefixText);
        }

        public DataSet GetManagerFromLDAP(string userID)
        {
            return this.DefaultDB.ExecuteDataSet("PO_selManagerFromLDAP", userID);
        }

        public DataSet GetManagerEmailFromLDAP(string userName)
        {
            return this.DefaultDB.ExecuteDataSet("SelManagerEmailFromLDAP", userName);
        }

        public DataSet GetMailIdForThirdLevelOfApproval(int mailNo, int deptID)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_MailingListFor3rdLevelApproval", mailNo, deptID);
        }

        public DataSet GetMailListForCC(int mailNo, int deptID)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_MailingListForCC",mailNo, deptID);
        }

        public DataSet GetPOForSDMgrApproval(string userName)
        {
            return this.DefaultDB.ExecuteDataSet("SelPOForSDMgrApproval", userName);
        }

        public DataSet GetPOFORNONSDMgrApproval(string userName)
        {
            return this.DefaultDB.ExecuteDataSet("SelPOForNONSDMgrApproval", userName);
        }       

        public DataSet GetPOForCFOApproval()
        {
            return this.DefaultDB.ExecuteDataSet("SelPOForCFOApproval");
        }

        public DataSet GetPOForCOOApproval()
        {
            return this.DefaultDB.ExecuteDataSet("SelPOForCOOApproval");
        }

        public DataSet GetPOForCEOApproval()
        {
            return this.DefaultDB.ExecuteDataSet("SelPOForCEOApproval");
        }     

        public int UpdatePOFor5thLevelApprovalForCFOCOO(Int64 poNumber, string status1, string comment, string empName)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Upd_PO5thLevelApprovalFORCFOCOO");
            this.DefaultDB.AddInParameter(cmd, "PONumber", DbType.Int64, poNumber);
            this.DefaultDB.AddInParameter(cmd, "Status1", DbType.String, status1);
            this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);
            this.DefaultDB.AddInParameter(cmd, "Comment", DbType.String, comment);
            this.DefaultDB.AddOutParameter(cmd, "ErrorCode", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "ErrorCode"));
        }

        public DataSet GetMailingListFor2ndLevelApproval(int deptID)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_MailingListFor2ndLevelApproval", deptID);
        }

        //public DataSet GetMailingListForFelipJeff()
        //{
        //    return this.DefaultDB.ExecuteDataSet("Sel_MailingListForFelipeJeff");
        //}

        public DataSet GetMailingListForPreDeputyApprover(double totalValueUSD)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_MailingListForPre-DeputyApprover", totalValueUSD);
        }

        public DataSet GetShipToExtender(string prefixText)
        {
            return this.DefaultDB.ExecuteDataSet("SpGetShipToExtender", prefixText);
        }

        public DataSet GetShipToDetails(string city)
        {
            return this.DefaultDB.ExecuteDataSet("Sp_ShowInGrid", city);
        }

        public int DeleteShipToDetails(Int32 shipToID, string empName)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Del_ShipTo");
            this.DefaultDB.AddInParameter(cmd, "ShipToID", DbType.Int32, shipToID);
            this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);

            this.DefaultDB.AddOutParameter(cmd, "Retout", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "Retout"));
        }

        public int UpdateShipToDetails(Int32 shipToID, string city, string vat, string address, string newAddress, string empName)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Upd_ShipTo");
            this.DefaultDB.AddInParameter(cmd, "ShipToID", DbType.Int32, shipToID);
            this.DefaultDB.AddInParameter(cmd, "City", DbType.String, city);
            this.DefaultDB.AddInParameter(cmd, "Vat", DbType.String, vat);
            this.DefaultDB.AddInParameter(cmd, "Address", DbType.String, address);
            this.DefaultDB.AddInParameter(cmd, "NewAddress", DbType.String, newAddress);
            this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);

            this.DefaultDB.AddOutParameter(cmd, "Retout", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "Retout"));
        }

        public int InsertShipTo(string city, string vat, string address, string newAddress, string empName)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("InsUp_ShipTo");

            this.DefaultDB.AddInParameter(cmd, "City", DbType.String, city);
            this.DefaultDB.AddInParameter(cmd, "Vat", DbType.String, vat);
            this.DefaultDB.AddInParameter(cmd, "Address", DbType.String, address);
            this.DefaultDB.AddInParameter(cmd, "NewAddress", DbType.String, newAddress);
            this.DefaultDB.AddInParameter(cmd, "CreatedBy", DbType.String, empName);

            this.DefaultDB.AddOutParameter(cmd, "Retout", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "Retout"));
        }

        public DataSet GetRequesterExtender(string prefixText)
        {
            return this.DefaultDB.ExecuteDataSet("SpGetRequestorExtender", prefixText);
        }

        public DataSet GetRequesterDetails(string requesterName)
        {
            return this.DefaultDB.ExecuteDataSet("Sp_ShowRequestorInGrid", requesterName);
        }

        public int UpdateRequester(string requesterName, string role, string email, string userID, string empName)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Upd_Requester");

            this.DefaultDB.AddInParameter(cmd, "EmployeeName", DbType.String, requesterName);
            this.DefaultDB.AddInParameter(cmd, "Role", DbType.String, role);
            this.DefaultDB.AddInParameter(cmd, "Email", DbType.String, email);
            this.DefaultDB.AddInParameter(cmd, "UserID", DbType.String, userID);
            this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);

            this.DefaultDB.AddOutParameter(cmd, "Retout", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "Retout"));
        }

        public int InsertRequester(string requesterName, string email, string userID, string role, string empName)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Ins_Requester");

            this.DefaultDB.AddInParameter(cmd, "RequesterName", DbType.String, requesterName);
            this.DefaultDB.AddInParameter(cmd, "Email", DbType.String, email);
            this.DefaultDB.AddInParameter(cmd, "UserID", DbType.String, userID);
            this.DefaultDB.AddInParameter(cmd, "Role", DbType.String, role);
            this.DefaultDB.AddInParameter(cmd, "CreatedBy", DbType.String, empName);           

            this.DefaultDB.AddOutParameter(cmd, "Retout", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "Retout"));
        }

        public int DeleteRequester(int requesterID, string empName)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Del_Requester");

            this.DefaultDB.AddInParameter(cmd, "RequesterID", DbType.Int32, requesterID);
            this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);

            this.DefaultDB.AddOutParameter(cmd, "Retout", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "Retout"));
        }

        public DataSet GetProjectExtender(string prefixText)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_ProjectExtender", prefixText);
        }       

        public DataSet GetProjectDetails(string projectName)
        {
            return this.DefaultDB.ExecuteDataSet("Sel_ProjectDetails", projectName);
        }

        public int UpdateProject(int projectID,string projectName, string empName)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Upd_Project");

            this.DefaultDB.AddInParameter(cmd, "ProjectID", DbType.Int32, projectID);
            this.DefaultDB.AddInParameter(cmd, "ProjectName", DbType.String, projectName);
            this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);

            this.DefaultDB.AddOutParameter(cmd, "Retout", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "Retout"));
        }

        public int InsertProject(string projectName, string empName)
        {
            DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Ins_Project");

            this.DefaultDB.AddInParameter(cmd, "ProjectName", DbType.String, projectName);
            this.DefaultDB.AddInParameter(cmd, "CreatedBy", DbType.String, empName);

            this.DefaultDB.AddOutParameter(cmd, "ErrorCode", DbType.Int32, 4);
            this.DefaultDB.ExecuteNonQuery(cmd);

            return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "ErrorCode"));
        }

          public DataSet GetSupplierExtender(string supplier)
          {
              return this.DefaultDB.ExecuteDataSet("Sel_SupplierExtender", supplier);
          }

          public DataSet GetSupplierDetails(string supplier)
          {
              return this.DefaultDB.ExecuteDataSet("Sel_SupplierDetails",supplier);
          }          

          public int UpdateSupplierDetails(int supplierID, string mainNo, string fax, string contactPerson, string phNo, string contactPerson1, string phNo1, string add1, string add2, string add3, string add4, string add5, string country, string comment, int term, string empName, bool isUpdated)
          {
              DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Upd_Supplier");

              this.DefaultDB.AddInParameter(cmd, "SupplierID", DbType.Int32, supplierID);
              this.DefaultDB.AddInParameter(cmd, "MainNo", DbType.String, mainNo);
              this.DefaultDB.AddInParameter(cmd, "FaxNo", DbType.String, fax);
              this.DefaultDB.AddInParameter(cmd, "ContactPerson", DbType.String, contactPerson);
              this.DefaultDB.AddInParameter(cmd, "PhNo", DbType.String, phNo);
              this.DefaultDB.AddInParameter(cmd, "ContactPerson1", DbType.String, contactPerson1);
              this.DefaultDB.AddInParameter(cmd, "PhNo1", DbType.String, phNo1);
              this.DefaultDB.AddInParameter(cmd, "Address1", DbType.String, add1);
              this.DefaultDB.AddInParameter(cmd, "Address2", DbType.String, add2);
              this.DefaultDB.AddInParameter(cmd, "Address3", DbType.String, add3);
              this.DefaultDB.AddInParameter(cmd, "Address4", DbType.String, add4);
              this.DefaultDB.AddInParameter(cmd, "Address5", DbType.String, add5);
              this.DefaultDB.AddInParameter(cmd, "Country", DbType.String, country);
              this.DefaultDB.AddInParameter(cmd, "Comment", DbType.String, comment);
              this.DefaultDB.AddInParameter(cmd, "Term", DbType.String, term);
              this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);
              this.DefaultDB.AddInParameter(cmd, "IsUpdated", DbType.Boolean, isUpdated);

              this.DefaultDB.AddOutParameter(cmd, "Retout", DbType.Int32, 4);
              this.DefaultDB.ExecuteNonQuery(cmd);

              return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "Retout"));
          }         

          public int InsertSupplier(string supplierName, string mainNo, string fax, string contactPerson, string phNo, string contactPerson1, string phNo1, string add1, string add2, string add3, string add4, string add5, string country, string comment, int term, string empName)
          {
              DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Ins_Supplier");

              try
              {
                  this.DefaultDB.AddInParameter(cmd, "SupplierName", DbType.String, supplierName);
                  this.DefaultDB.AddInParameter(cmd, "MainNo", DbType.String, mainNo);
                  this.DefaultDB.AddInParameter(cmd, "FaxNo", DbType.String, fax);
                  this.DefaultDB.AddInParameter(cmd, "ContactPerson", DbType.String, contactPerson);
                  this.DefaultDB.AddInParameter(cmd, "PhNo", DbType.String, phNo);
                  this.DefaultDB.AddInParameter(cmd, "ContactPerson1", DbType.String, contactPerson1);
                  this.DefaultDB.AddInParameter(cmd, "PhNo1", DbType.String, phNo1);
                  this.DefaultDB.AddInParameter(cmd, "Address1", DbType.String, add1);
                  this.DefaultDB.AddInParameter(cmd, "Address2", DbType.String, add2);
                  this.DefaultDB.AddInParameter(cmd, "Address3", DbType.String, add3);
                  this.DefaultDB.AddInParameter(cmd, "Address4", DbType.String, add4);
                  this.DefaultDB.AddInParameter(cmd, "Address5", DbType.String, add5);
                  this.DefaultDB.AddInParameter(cmd, "Country", DbType.String, country);
                  this.DefaultDB.AddInParameter(cmd, "Comment", DbType.String, comment);
                  this.DefaultDB.AddInParameter(cmd, "Term", DbType.Int32, term);
                  this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);

                  this.DefaultDB.AddOutParameter(cmd, "Retout", DbType.Int32, 4);
                  this.DefaultDB.ExecuteNonQuery(cmd);                               
              }

              catch (Exception ex)
              { }

              return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "Retout"));
          }

          public DataSet GetBudgetCodeExtender(string prefixText)
          {
              return this.DefaultDB.ExecuteDataSet("Sel_BudgetCodeExtender", prefixText);
          }

          public DataSet GetBudgetCodeDetails(string budgetCode)
          {
              return this.DefaultDB.ExecuteDataSet("Sel_BudgetCodeDetails", budgetCode);
          }

          public int UpdateBudgetCode(int budgetCodeID, int department, int orderClassification, int station, string description, string empName, bool isUpdated)
          {
              DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Upd_BudgetCode");

              this.DefaultDB.AddInParameter(cmd, "BudgetCodeID", DbType.Int32, budgetCodeID);
              this.DefaultDB.AddInParameter(cmd, "Department", DbType.Int32, department);
              this.DefaultDB.AddInParameter(cmd, "OrderClassification", DbType.Int32, orderClassification);
              this.DefaultDB.AddInParameter(cmd, "Station", DbType.Int32, station);
              this.DefaultDB.AddInParameter(cmd, "Description", DbType.String, description);
              this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);
              this.DefaultDB.AddInParameter(cmd, "IsUpdated", DbType.Boolean, isUpdated);

              this.DefaultDB.AddOutParameter(cmd, "Retout", DbType.Int32, 4);
              this.DefaultDB.ExecuteNonQuery(cmd);

              return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "Retout"));
          }

          public int InsertBudgetCode(string budgetCode, int department, int orderClassification, int station, string description, int year, string empName)
          {
              DbCommand cmd = this.DefaultDB.GetStoredProcCommand("Ins_BudgetCode");

              try
              {
                  this.DefaultDB.AddInParameter(cmd, "BudgetCode", DbType.String, budgetCode);
                  this.DefaultDB.AddInParameter(cmd, "Department", DbType.Int32, department);
                  this.DefaultDB.AddInParameter(cmd, "OrderClassification", DbType.Int32, orderClassification);
                  this.DefaultDB.AddInParameter(cmd, "Station", DbType.Int32, station);
                  this.DefaultDB.AddInParameter(cmd, "BudgetCodeDescription", DbType.String, description);
                  this.DefaultDB.AddInParameter(cmd, "Year", DbType.Int32, year);   
                  this.DefaultDB.AddInParameter(cmd, "ModifiedBy", DbType.String, empName);

                  this.DefaultDB.AddOutParameter(cmd, "Retout", DbType.Int32, 4);
                  this.DefaultDB.ExecuteNonQuery(cmd);
              }

              catch (Exception ex)
              { }

              return Convert.ToInt32(this.DefaultDB.GetParameterValue(cmd, "Retout")); 
          }

          public DataSet GetPOForAdminApproval()
          {
              return this.DefaultDB.ExecuteDataSet("Sel_POForAdmin");
          }

          public DataSet GetOrderClassificationForCapex()
          {
              return this.DefaultDB.ExecuteDataSet("Sel_OrderClassificationForCapex");
          }

          public DataSet GetUserId(object userName)
          //public object GetUserId(object userName)
          {
              return this.DefaultDB.ExecuteDataSet("GetUserID", userName);
          }         

          public DataSet GetApprovalRecords(Int64 poNumber)
          {
              return this.DefaultDB.ExecuteDataSet("GetApprovalRecords", poNumber);
          }

          public DataSet GetBudgetCodes(int dept, int orderClassification, int station, int year)
          {
              return this.DefaultDB.ExecuteDataSet("Sel_BudgetCode", dept, orderClassification, station, year);
          }

          //public DataSet GetShipToOnProject(int projectID)
          //{
          //    return this.DefaultDB.ExecuteDataSet("Sel_ShipToOnProject", projectID);
          //}

          public object GetRequesterID(object userName)
          {
              return this.DefaultDB.ExecuteScalar("GetRequesterID", userName); 
          }
    }

}