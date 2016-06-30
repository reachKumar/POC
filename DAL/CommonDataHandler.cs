using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;

namespace DAL
{
    public class CommonDataHandler
    {
        public Database DefaultDB = DatabaseFactory.CreateDatabase("ConnectionString");

        public DataSet GetEmployeeDetails(string strLoginID)
        {
            return DefaultDB.ExecuteDataSet("Sel_EmployeeDetails", strLoginID);
        }
    }
}