<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSessionTimeOut.aspx.cs" Inherits="POManagementASPNet.frmSessionTimeOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POSystem</title>
</head>
<body>
    <form id="form1" runat="server">
     <table width="100%" border="0" style="height: 20%">
        <tr>
            <td align="left">
                <asp:Image ImageUrl="~/images/NewHibernialogo1.jpg" ID="imgcap" runat="server" />&nbsp;
                <div style="text-align: right">
                    &nbsp;</div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table align="center" border="0" width="100%">
        <tr>
            <td valign="top" align="center" style="font-family: Verdana">
                <asp:HiddenField ID="hfNotAuhtorizeMessage" runat="server" Value="You are not authorized to view the page. Inconvenience regretted. We have logged off you, please" />
                <asp:Label ID="lblMessage" runat="server">Your Session has been expired. Please</asp:Label>
                <asp:HyperLink ID="hypLogin" runat="server" SkinID="NotesLink" ToolTip="click here to login"
                    Font-Bold="true" Font-Size="Small" NavigateUrl="~/Login.aspx?resign=true">Login</asp:HyperLink>
                <asp:Label ID="lblMessageText" runat="server">again</asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <!--footer-->
            <td>
                <div id="footer" style="width: 100%;">
                    <div>
                        <div>
                            <div>
                                <hr />
                                <p>
                                    &copy; 2012 - Hibernia Networks Confidential
                                    <!--  | <a href="contact">Contact GDDC</a> -->
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
            <!-- end footer -->
        </tr>
    </table>
    </form>
</body>
</html>
