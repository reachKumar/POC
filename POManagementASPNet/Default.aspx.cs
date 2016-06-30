using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.Security;

namespace POManagementASPNet
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CurrentUserLoginId;
            try
            {
                CurrentUserLoginId = Context.User.Identity.Name.ToString();

                string CurrUID = CurrentUserLoginId.Split('\\')[1];


                DirectoryEntry entry = new DirectoryEntry(System.Configuration.ConfigurationManager.ConnectionStrings["ADConnectionString"].ToString());
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + CurrUID + ")";
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("mail");
                SearchResult result = search.FindOne();
                if (result != null)
                {
                    Session["PO_UserName"] = result.Properties["cn"][0].ToString();
                    Session["UserEmail"] = result.Properties["mail"][0].ToString();
                    Session["UserID"] = CurrUID;

                    HttpCookie userCookie = new HttpCookie("UserInfo");
                    userCookie["UserName"] = result.Properties["cn"][0].ToString();
                    userCookie["UserEmail"] = result.Properties["mail"][0].ToString();
                    userCookie["UserID"] = CurrUID;
                    userCookie.Expires = DateTime.Now.AddDays(3);
                    Response.Cookies.Add(userCookie);

                    Response.Redirect("Home.aspx");

                }
            }
            catch
            {

            }
        }
    }
}