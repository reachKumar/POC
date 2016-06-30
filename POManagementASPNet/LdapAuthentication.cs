using System;
using System.DirectoryServices;

namespace POManagementASPNet
{
    public class LdapAuthentication
    {
        private string _path;
        private string _filterAttribute;

        public LdapAuthentication(string path)
        {
            _path = path;
        }

        public bool IsAuthenticated(string domain, string username, string pwd, out string loginName, out string userEmail)
        {
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);
            bool returnValue = false;
            try
            {

                loginName = string.Empty;
                userEmail = string.Empty;
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("mail");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }
                loginName = result.Properties["cn"][0].ToString();
                userEmail = result.Properties["mail"][0].ToString();
                // Update the new path to the user in the directory
                _path = result.Path;
                _filterAttribute = (String)result.Properties["cn"][0];

                returnValue = true;
            }
            catch
            {
                loginName = "";
                userEmail = "";
                returnValue = false;
               //throw new Exception("Error authenticating user. " + ex.Message);
            }
            return returnValue;
        }
    }
}