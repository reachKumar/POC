using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;


namespace POManagementASPNet
{
    public class LDAP
    {
        //private string _path;
        //private string _filterAttribute;

        //public LDAP(string path)
        //{ _path = path; }
        public void GetManagerName()
        {

            try
            {
                string connection = "LDAP://DUBDCV01/DC=hibernianetworks,DC=local";
              
                DirectorySearcher dssearch = new DirectorySearcher(connection);
                string userName = HttpContext.Current.Session["PO_UserName"].ToString();
                dssearch.Filter = "(SAMAccountName =" + userName + ")";
                dssearch.PropertiesToLoad.Add("cn");
                dssearch.PropertiesToLoad.Add("mail");
                SearchResult sresult = dssearch.FindOne();
                DirectoryEntry dsresult = sresult.GetDirectoryEntry();
                string Manager = dsresult.Properties["manager"][0].ToString();
                if (Manager != "")
                {
                    if (Manager.Contains("CN="))
                    {
                        int Length = Manager.IndexOf(',');
                        Manager = Manager.Substring(3, Length - 3);
                    }
                    else
                    {
                        Manager = string.Empty;
                    }
                }
                userName = Manager;

            }

            catch (Exception ex)
            { }
        }

       
    }
}