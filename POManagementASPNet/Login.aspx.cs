using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Principal;
using System.DirectoryServices;
using BAL;


namespace POManagementASPNet
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Muktesh Code start
            if (!IsPostBack)
            {
                if (Request.QueryString["resign"] != null)
                {
                    if (Request.QueryString["resign"].ToString() == "true")
                    {
                        //FormsAuthentication.SignOut();
                    }
                }
                else if (Context.User.Identity != null)
                {
                   //TryAutoLogin(Context.User.Identity.Name);
                    
                }
            }

            //Muktesh Code end
        }

        //Muktesh Code start
        private void TryAutoLogin(string userName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
            if (cookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket != null)
                {
                    if (ticket.Name.Length > 0)
                    {
                        try
                        {
                            FormsAuthentication.RedirectFromLoginPage(userName, true);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
        //protected void LoginButton_Click(object sender, EventArgs e)
        {
            //just added to resolve impersonation issue

            //Employee employee = new Employee();

            try
            {
                //LdapAuthentication authntication = new LdapAuthentication("LDAP://dubdc001/DC=hiberniaatlantic,DC=local");
                LdapAuthentication authntication = new LdapAuthentication(System.Configuration.ConfigurationManager.ConnectionStrings["ADConnectionString"].ToString());
                string userName = LoginUser.UserName.ToString();
                string password = LoginUser.Password.ToString();
                string loginName;
                string userEmail;

                bool isAuthenticated = authntication.IsAuthenticated("hib-atl", userName, password, out loginName, out userEmail);
                e.Authenticated = isAuthenticated;
                if (isAuthenticated)
                {
                    Session["PO_UserName"] = loginName;
                    Session["UserEmail"] = userEmail;
                    Session["UserID"] = userName;

                    ////Raga Code
                    ////HttpCookie userCookie = new HttpCookie("UserInfo");
                    ////userCookie["UserName"] = loginName;
                    ////userCookie["UserEmail"] = userEmail;
                    ////userCookie["UserID"] = userName;
                    //////userCookie.Expires = DateTime.Now.AddDays(-1);
                    ////Response.Cookies.Add(userCookie);

                    ////Muktesh Code
                    FormsAuthentication.Initialize();
                    DateTime expires = DateTime.Now.AddDays(100);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                     userName,
                     DateTime.Now,
                     expires,
                     true,
                     String.Empty,
                     FormsAuthentication.FormsCookiePath);
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    HttpCookie authCookie = new HttpCookie(
                          FormsAuthentication.FormsCookieName,
                          encryptedTicket);
                    authCookie.Expires = expires;
                    Response.Cookies.Add(authCookie);
                    string returnUrl = FormsAuthentication.GetRedirectUrl(userName, true);
                    if (string.IsNullOrEmpty(returnUrl)) returnUrl = "Home.aspx";
                    Response.Redirect(returnUrl);

                    //Response.Redirect("Home.aspx");


                    //employee = new Employee(Convert.ToString(Session["UserID"]));
                    //Session["Employee"] = employee;                    
                }

                else
                {
                    Response.Redirect("Login.aspx");
                }

            }
            catch (Exception ex)
            { }
        }
    }
}