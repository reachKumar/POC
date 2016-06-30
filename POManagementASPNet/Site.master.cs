using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using BAL;
using System.Web.UI.HtmlControls;

namespace POManagementASPNet
{
    public partial class Site : System.Web.UI.MasterPage
    {
        //Employee loggedInUser = null;
        Employee employee;// = new Employee();

        protected void Page_Load(object sender, EventArgs e)
        {
           
            Employee loggedInUser = (Employee)Session["Employee"];
            HtmlGenericControl liAdmin = (HtmlGenericControl)this.FindControl("liAdmin");

            if(loggedInUser.HasEmployeeAccess(Roles.PORequester))
            {                
                liAdmin.Visible = false;
            }

            //Employee employee = new Employee();
            //if (!IsPostBack)
            //{
            //    if (Session["PO_UserName"] == null)
            //    {
            //        try
            //        {
            //            DirectoryEntry entry = new DirectoryEntry(System.Configuration.ConfigurationManager.ConnectionStrings["ADConnectionString"].ToString());
            //            Object obj = entry.NativeObject;
            //            DirectorySearcher search = new DirectorySearcher(entry);
            //            search.Filter = "(SAMAccountName=" + Context.User.Identity.Name + ")";
            //            search.PropertiesToLoad.Add("cn");
            //            search.PropertiesToLoad.Add("mail");
            //            search.PropertiesToLoad.Add("SAMAccountName");
            //            SearchResult result = search.FindOne();
            //            if (result != null)
            //            {
            //                Session["PO_UserName"] = result.Properties["cn"][0].ToString();
            //                Session["UserEmail"] = result.Properties["mail"][0].ToString();
            //                Session["UserID"] = result.Properties["SAMAccountName"][0].ToString();

            //                lblUserName.Text = "Welcome " + Session["PO_UserName"].ToString();
            //            }
            //        }
            //        catch
            //        {
            //            lblUserName.Text = string.Empty;
            //            A1.Visible = false;
            //        }
            //    }
            //    else
            //    {
            //        lblUserName.Text = "Welcome " + Session["PO_UserName"].ToString();
            //        A1.Visible = true;
            //    }

            //    employee = new Employee(Convert.ToString(Session["UserID"]));
            //    Session["Employee"] = employee;
            //}
        }
    //protected void Page_Load(object sender, EventArgs e)
    //    {
    //        if (!IsPostBack)
    //        {
    //            //HttpCookie cookie = Request.Cookies["UserInfo"];
    //            //string CurrentUserLoginId;

    //            //if (cookie != null)
    //            //{
    //            //    lblUserName.Text = cookie["UserName"];

    //            //    //session

    //            //    Session["PO_UserName"] = cookie["UserName"];
    //            //    Session["UserEmail"] = cookie["UserEmail"];
    //            //    Session["UserID"] = cookie["UserID"];
    //            //}

    //            //else
    //            //{
                    
    //                try
    //                {
    //                    CurrentUserLoginId = Context.User.Identity.Name.ToString();

    //                    string CurrUID = CurrentUserLoginId.Split('\\')[1];


    //                    DirectoryEntry entry = new DirectoryEntry(System.Configuration.ConfigurationManager.ConnectionStrings["ADConnectionString"].ToString());
    //                    Object obj = entry.NativeObject;
    //                    DirectorySearcher search = new DirectorySearcher(entry);
    //                    search.Filter = "(SAMAccountName=" + CurrUID + ")";
    //                    search.PropertiesToLoad.Add("cn");
    //                    search.PropertiesToLoad.Add("mail");
    //                    SearchResult result = search.FindOne();
    //                    if (result != null)
    //                    {
    //                        Session["PO_UserName"] = result.Properties["cn"][0].ToString();
    //                        Session["UserEmail"] = result.Properties["mail"][0].ToString();
    //                        Session["UserID"] = CurrUID;


    //                        lblUserName.Text = Session["PO_UserName"].ToString();


    //                        HttpCookie userCookie = new HttpCookie("UserInfo");
    //                        userCookie["UserName"] = result.Properties["cn"][0].ToString();
    //                        userCookie["UserEmail"] = result.Properties["mail"][0].ToString();
    //                        userCookie["UserID"] = CurrUID;
    //                        userCookie.Expires = DateTime.Now.AddDays(3);
    //                        Response.Cookies.Add(userCookie);

    //                        //Response.Redirect("Map.aspx");

    //                    }
    //                }
    //                catch
    //                {
    //                    Response.Redirect("Login.aspx");
    //                }
    //            }
    //        }
    //    }

        protected override void OnInit(EventArgs e)
        {  
            try
            {
                base.OnInit(e);

                if (Session["PO_UserName"] == null)
                {
                    try
                    {
                        DirectoryEntry entry = new DirectoryEntry(System.Configuration.ConfigurationManager.ConnectionStrings["ADConnectionString"].ToString());
                        Object obj = entry.NativeObject;
                        DirectorySearcher search = new DirectorySearcher(entry);
                        search.Filter = "(SAMAccountName=" + Context.User.Identity.Name + ")";
                        search.PropertiesToLoad.Add("cn");
                        search.PropertiesToLoad.Add("mail");
                        search.PropertiesToLoad.Add("SAMAccountName");
                        SearchResult result = search.FindOne();
                        if (result != null)
                        {
                            Session["PO_UserName"] = result.Properties["cn"][0].ToString();
                            Session["UserEmail"] = result.Properties["mail"][0].ToString();

                            Session["UserID"] = result.Properties["SAMAccountName"][0].ToString();

                            lblUserName.Text = "Welcome " + Session["PO_UserName"].ToString();
                        }
                        ////if (Session["NTUserId"] != null && Session["Employee"] == null)
                        ////{
                        ////    employee = new Employee(Convert.ToString(Session["NTUserId"]));
                        ////    Session["Employee"] = employee;
                        ////}//Otherwise take it from Session
                        ////else
                        ////    employee = (Employee)Session["Employee"];

                        //Response.Redirect("frmSessionTimeOut.aspx");
                        ////Response.Redirect("Login.aspx");

                        employee = new Employee(Convert.ToString(Session["UserID"]));
                        Session["Employee"] = employee;
                    }
                    catch
                    {
                        Response.Redirect("Login.aspx?resign=true");
                        //lblUserName.Text = string.Empty;
                        //A1.Visible = false;
                    }
                }
                else
                {
                    lblUserName.Text = "Welcome " + Session["PO_UserName"].ToString();
                    A1.Visible = true;

                    employee = new Employee(Convert.ToString(Session["UserID"]));
                    Session["Employee"] = employee;

                 
                }
            }

            catch (Exception ex)
            { }
        }
    }
}