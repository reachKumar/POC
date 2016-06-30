using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.IO;
using System.Xml.Serialization;


namespace BAL
{
    [Serializable]
    public class POMain
    {
        public Int64 PONumber { get; set; }

        public string ApprovalStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ApprovedBy { get; set; }

        public DateTime ApprovalDate { get; set; }

        public int RequesterID { get; set; }

        public string RequesterName { get; set; }

        public int StationID { get; set; }

        public string StationName { get; set; }

        public int POType { get; set; }

        public string POTypeName { get; set; }

        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public int SupplierID { get; set; }

        public string SupplierName { get; set; }

        public int ShipToID { get; set; }

        public string ShipToName { get; set; }

        public string ShipToAddress { get; set; }

        public string VAT { get; set; }

        public string Comment { get; set; }

        public int InBudgetID { get; set; }

        public string InBudgetName { get; set; }

        public int OrderClassificationID { get; set; }

        public string OrderClassificationName { get; set; }

        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public DateTime DateReceived { get; set; }

        public bool Received { get; set; }

        public string CreatedBy { get; set; }

        //public int GLCode { get; set; }

        //public string GLCodeName { get; set; }

        public int CurrencyID { get; set; }

        public double TotalValue { get; set; }

        public int NRC { get; set; }

        public int MRC { get; set; }

        public int Tail { get; set; }

        public string CircuitID { get; set; }

        public double TotalValueInUSD { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string UserID
        {
            get;
            set;
        }

        public string NewAddress { get; set; }

        public string MainNo { get; set; }

        public string FaxNo { get; set; }

        public string ContactPerson { get; set; }

        public string PhNo { get; set; }

        public string ContactPerson1 { get; set; }

        public string PhNo1 { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string Address4 { get; set; }

        public string Address5 { get; set; }

        public string Country { get; set; }

        public int Term { get; set; }

        public string Supervisor { get; set; }

        //public Int64 Opportunity { get; set; }
        public string Opportunity { get; set; }

        public DateTime EstimatedDeliveryTime { get; set; }

        public int BudgetCodeID { get; set; }

        public string BudgetCode { get; set; }

        public string BudgetCodeDescription { get; set; }

        public int YearOfBudgetCode { get; set; }


        private string GenerateXMLForPOData(List<POItem> objRequest)
        {
            TextWriter objTextWriter = new StringWriter();
            XmlSerializer objXmlSerializer = new XmlSerializer(typeof(List<POItem>));
            objXmlSerializer.Serialize(objTextWriter, objRequest);
            objTextWriter.Flush();
            string strXML = objTextWriter.ToString();
            objTextWriter.Close();
            StringBuilder sbXML = new StringBuilder();
            sbXML.Append(strXML.Substring(strXML.IndexOf("<POItem>")));
            //sbXML.Append("<RequestOfListOfSpaceDemand>", 0, 0);
            sbXML.Insert(0, "<POItems>");
            sbXML.Replace("</ArrayOfPOItem>", "</POItems>");
            return sbXML.ToString();

        }

        public int InsertPOData(List<POItem> poItems, POMain poMain, ref Int64 poNumber)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();            
            return dataHandler.InsertPOData(GenerateXMLForPOData(poItems), this.PONumber, this.RequesterID, this.OrderClassificationID, this.StationID, this.DepartmentID, this.POType, this.ProjectID, this.ShipToID, this.Comment, this.SupplierID, this.InBudgetID, this.CurrencyID, this.TotalValue, this.TotalValueInUSD, this.CreatedBy, this.NRC, this.MRC, this.Tail, this.CircuitID,this.EstimatedDeliveryTime, this.Supervisor,this.Opportunity, this.BudgetCodeID, ref poNumber);
        }

        public DataSet FillEditPO(string userName)
        {
            DataSet dsEditPO = new POSystemDataHandler().FillEditPO(userName);
            return dsEditPO;
        }

        public DataSet BindPOData(Int64 poID)
        {
            DataSet dsEditPO = new POSystemDataHandler().BindPOData(poID);
            return dsEditPO;
        }

        public int UpdatePOToApprove(Int64 poNumber, string status1, string comment, string empName, double poValueUSD)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.UpdatePOToApprove(poNumber,status1, comment, empName, poValueUSD);
        }

        public int UpdatePOToSupApprove(Int64 poNumber, string status1, string comment, string empName)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.UpdatePOToSupApprove(poNumber, status1, comment, empName);
        }


        public int UpdatePOToReject(Int64 poNumber, string comment, string empName)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.UpdatePOToReject(poNumber, comment,empName);
        }

        public int UpdatePOToPreApprove(Int64 poNumber, string comment, string empName)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.UpdatePOToPreApprove(poNumber,comment, empName);
        }

        public int UpdatePOToHODApproved(Int64 poNumber, string comment, string empName)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.UpdatePOToHODApproved(poNumber,comment, empName);
        }

        public DataSet GetPOForClosed()
        {
            DataSet dsClosedPO = new POSystemDataHandler().GetPOForClosed();
            return dsClosedPO;
        }

        public void UpdatePOToReset(Int64 poNumber,string empName)
        {
            new POSystemDataHandler().UpdatePOToReset(poNumber,empName);
        }

        public void DeletePO(Int64 poNumber, string empName)
        {
            new POSystemDataHandler().DeletePO(poNumber,empName);
        }

        public void UpdatePOInvoicing(Int64 poNumber, DateTime invoiceDate,string empName)
        {
            new POSystemDataHandler().UpdatePOInvoicing(poNumber, invoiceDate,empName);
        }

        public DataSet BindConditions()
        {
            DataSet dsConditions = new DataSet();
            dsConditions = new POSystemDataHandler().BindConditions();
            return dsConditions;
        }

        public DataSet BindShipTerm()
        {
            DataSet dsShipTerm = new POSystemDataHandler().BindShipTerm();
            return dsShipTerm;
        }

        public DataSet GetSupplierDetails(int supplierID)
        {
            DataSet dsSupplierDetails = new POSystemDataHandler().GetSupplierDetails(supplierID);
            return dsSupplierDetails;
        }

        public DataSet GetApproverList(int roleID,int? deptID)
        {
            DataSet dsApprover = new POSystemDataHandler().GetApproverList(roleID,deptID);
            return dsApprover;
        }      

        public DataSet GetPOByAdvSearch(int? requester, int? station, int? shipTo, int? supplier, int? dept, int? project, string comment, string circitID)
        {
            DataSet dsPO = new POSystemDataHandler().GetPOByAdvSearch(requester, station, shipTo, supplier, dept, project, comment, circitID);
            return dsPO;
        }

        public DataSet GetShipToDetailsByShipToId(int shipTo)
        {
            DataSet dsPO = new POSystemDataHandler().GetShipToDetailsByShipToId(shipTo);
            return dsPO;
        }

        public DataSet GetPOForHODApproval(string deptIDs, string userName)
        {
            DataSet dsPO = new POSystemDataHandler().GetPOForHODApproval(deptIDs, userName);
            return dsPO;
        }

        //For SD HOD approval
        public DataSet GetPOForSDHODApproval(string deptIDs, string userName)
        {
            DataSet dsPO = new POSystemDataHandler().GetPOForSDHODApproval(deptIDs, userName);
            return dsPO;
        }

        //For Tail/Circuit/Cross-Connect PO HOD 
        public DataSet GetPOForTailCircuitCrossHODApproval(string deptIDs, string userName)
        {
            DataSet dsPO = new POSystemDataHandler().GetPOForTailCircuitCrossHODApproval(deptIDs, userName);
            return dsPO;
        }

        public DataSet GetPOForPreApproval(string deptIDs, string userName)
        {
            DataSet dsPO = new POSystemDataHandler().GetPOForPreApproval(deptIDs, userName);
            return dsPO;
        }

        public DataSet GetPOForDeputyApproval(string userName)
        {
            DataSet dsPO = new POSystemDataHandler().GetPOForDeputyApproval(userName);
            return dsPO;
        }

        public DataSet GetAdditionalBillingAddress(int city)
        {
            DataSet dsAddnBillingAdd = new POSystemDataHandler().GetAdditionalBillingAddress(city);
            return dsAddnBillingAdd;
        }        

        public DataSet GetCircuitID(string prefixText)
        {
            return new POSystemDataHandler().GetCircuitID(prefixText);
        }

        public DataSet GetOpportunityID(string prefixText)
        {
            return new POSystemDataHandler().GetOpportunityID(prefixText);
        }

        public DataSet GetManagerFromLDAP(string userID)
        {
            return new POSystemDataHandler().GetManagerFromLDAP(userID);
        }

        public DataSet GetManagerEmailFromLDAP(string userName)
        {
            return new POSystemDataHandler().GetManagerEmailFromLDAP(userName);
        }

        public DataSet GetMailIdForThirdLevelOfApproval(int mailNo, int deptID)
        {
            return new POSystemDataHandler().GetMailIdForThirdLevelOfApproval(mailNo, deptID);
        }

        public DataSet GetMailListForCC(int mailNo, int deptID)
        {
            return new POSystemDataHandler().GetMailListForCC(mailNo, deptID);
        }

        public DataSet GetPOForSDMgrApproval(string userName)
        {
            return new POSystemDataHandler().GetPOForSDMgrApproval(userName);
        }

        public DataSet GetPOFORNONSDMgrApproval(string userName)
        {
            return new POSystemDataHandler().GetPOFORNONSDMgrApproval(userName);
        }

        public DataSet GetPOForCFOApproval()
        {
            return new POSystemDataHandler().GetPOForCFOApproval();
        }

        public DataSet GetPOForCOOApproval()
        {
            return new POSystemDataHandler().GetPOForCOOApproval();
        }

        public DataSet GetPOForCEOApproval()
        {
            return new POSystemDataHandler().GetPOForCEOApproval();
        }       

        public int UpdatePOFor5thLevelApprovalForCFOCOO(Int64 poNumber, string status1, string comment, string empName)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.UpdatePOFor5thLevelApprovalForCFOCOO(poNumber, status1, comment, empName);
        }

        public DataSet GetMailingListFor2ndLevelApproval(int deptID)
        {
            return new POSystemDataHandler().GetMailingListFor2ndLevelApproval(deptID);
        }

        //public DataSet GetMailingListForFelipJeff()
        //{
        //    return new POSystemDataHandler().GetMailingListForFelipJeff();
        //}

        public DataSet GetMailingListForPreDeputyApprover(double totalValueUSD)
        {
            return new POSystemDataHandler().GetMailingListForPreDeputyApprover(totalValueUSD);
        }

        public DataSet GetShipToExtender(string prefixText)
        {
            return new POSystemDataHandler().GetShipToExtender(prefixText);
        }

        public DataSet GetShipToDetails(string city)
        {
            return new POSystemDataHandler().GetShipToDetails(city);
        }

        public int DeleteShipToDetails(POMain poMain)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.DeleteShipToDetails(this.ShipToID,this.CreatedBy);
        }

        public int UpdateShipToDetails(POMain poMain)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.UpdateShipToDetails(this.ShipToID, this.ShipToName, this.VAT, this.ShipToAddress,this.NewAddress, this.CreatedBy);
        }

        public int InsertShipTo(POMain poMain)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.InsertShipTo(this.ShipToName,this.VAT,this.ShipToAddress,this.NewAddress,this.CreatedBy);
        }

        public DataSet GetRequesterExtender(string prefixText)
        {
            return new POSystemDataHandler().GetRequesterExtender(prefixText);
        }

        public DataSet GetRequesterDetails(string requesterName)
        {
            return new POSystemDataHandler().GetRequesterDetails(requesterName);
        }

        public int UpdateRequester(POMain poMain)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.UpdateRequester(this.RequesterName, this.Role, this.Email,this.UserID, this.CreatedBy);
        }

        public int InsertRequester(POMain poMain)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.InsertRequester(this.RequesterName, this.Email, this.UserID, this.Role, this.CreatedBy);
        }

        public int DeleteRequester(POMain poMain)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.DeleteRequester(this.RequesterID, this.CreatedBy);
        }

        public DataSet GetProjectExtender(string prefixText)
        {
            return new POSystemDataHandler().GetProjectExtender(prefixText);
        }

        public DataSet GetProjectDetails(string projectName)
        {
            return new POSystemDataHandler().GetProjectDetails(projectName);
        }

        public int UpdateProject(POMain poMain)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.UpdateProject(this.ProjectID, this.ProjectName, this.CreatedBy);
        }

        public int InsertProject(POMain poMain)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.InsertProject(this.ProjectName, this.CreatedBy);
        }

        public DataSet GetSupplierExtender(string supplier)
        {
            return new POSystemDataHandler().GetSupplierExtender(supplier);
        }

        public DataSet GetSupplierDetails(string supplier)
        {
            return new POSystemDataHandler().GetSupplierDetails(supplier);
        }

        public int UpdateSupplierDetails(POMain poMain)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.UpdateSupplierDetails(this.SupplierID, this.MainNo, this.FaxNo, this.ContactPerson, this.PhNo, this.ContactPerson1, this.PhNo1, this.Address1, this.Address2, this.Address3, this.Address4, this.Address5, this.Country, this.Comment, this.Term, this.CreatedBy,this.Received);
        }

        public int InsertSupplier(POMain poMain)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.InsertSupplier(this.SupplierName, this.MainNo, this.FaxNo, this.ContactPerson, this.PhNo, this.ContactPerson1, this.PhNo1, this.Address1, this.Address2, this.Address3, this.Address4, this.Address5, this.Country, this.Comment, this.Term, this.CreatedBy);
        }

        public DataSet GetBudgetCodeExtender(string budgetCode)
        {
            return new POSystemDataHandler().GetBudgetCodeExtender(budgetCode);
        }

        public int UpdateBudgetCode(POMain poMain)
        {
            return new POSystemDataHandler().UpdateBudgetCode(this.BudgetCodeID, this.DepartmentID, this.OrderClassificationID, this.StationID, this.BudgetCodeDescription, this.CreatedBy, this.Received);  
        }

        public DataSet GetBudgetCodeDetails(string budgetCode)
        {
            return new POSystemDataHandler().GetBudgetCodeDetails(budgetCode);
        }

        public int InsertBudgetCode(POMain poMain)
        {
            return new POSystemDataHandler().InsertBudgetCode(this.BudgetCode, this.DepartmentID, this.OrderClassificationID, this.StationID, this.BudgetCodeDescription, this.YearOfBudgetCode, this.CreatedBy);
        }

        public DataSet GetPOForAdminApproval()
        {
            return new POSystemDataHandler().GetPOForAdminApproval();
        }

        public DataSet GetOrderClassificationForCapex()
        {
            return new POSystemDataHandler().GetOrderClassificationForCapex();
        }

        public DataSet GetOrderClassification(int poType)
        {
            return new POSystemDataHandler().GetOrderClassification(poType);
        }

        public object GetGLCodeID(string glCodeName)
        {
            return new POSystemDataHandler().GetGLCodeID(glCodeName);            
        }     

        public DataSet GetUserId(object userName)
        {
            return new POSystemDataHandler().GetUserId(userName);
        }

        public DataSet GetEmployeeDetails(string strLoginID)
        {
            return new CommonDataHandler().GetEmployeeDetails(strLoginID);
        }

        public DataSet GetApprovalRecords(Int64 poNumber)
        {
            return new POSystemDataHandler().GetApprovalRecords(poNumber);
        }

        public DataSet GetBudgetCodes(int dept, int orderClassification, int station, int year)
        {
            return new POSystemDataHandler().GetBudgetCodes(dept, orderClassification, station, year);
        }

        //public DataSet GetShipToOnProject(int projectID)
        //{
        //    return new POSystemDataHandler().GetShipToOnProject(projectID);
        //}

        public object GetRequesterID(object userName)
        {
            return new POSystemDataHandler().GetRequesterID(userName);
        }
    }

}