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
    public class POItem: POMain
    {
        public Int64 ItemID { get; set; }
        public string DescriptionOfParts { get; set; }

        public string SupplierCode { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public double TotalPrice { get; set; }

        public DateTime POReceivedDate { get; set; }

        public bool POReceived { get; set; }

        public decimal QuantityReceived { get; set; }

        public string CreatedBy { get; set; }

        public int GLCode { get; set; }

        public string GLCodeName { get; set; }

        public int UpdatePOReceiving(List<POItem> poItems, bool allPOReceived,string empName)
        {
            POSystemDataHandler dataHandler = new POSystemDataHandler();
            return dataHandler.UpdatePOReceiving(GenerateXMLForPOReceiving(poItems), allPOReceived,empName);
        }

        private string GenerateXMLForPOReceiving(List<POItem> poItems)
        {
            TextWriter objTextWriter = new StringWriter();
            XmlSerializer objXmlSerializer = new XmlSerializer(typeof(List<POItem>));
            objXmlSerializer.Serialize(objTextWriter, poItems);
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

        public DataSet GetPOByAdvSearchByDescOfParts(string descOfParts, string supplierCode)
        {
            DataSet dsPO = new POSystemDataHandler().GetPOByAdvSearchByDescOfParts(descOfParts, supplierCode);
            return dsPO;
        }

    }
}