<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="Login.aspx.cs" Inherits="POManagementASPNet.Login" Theme="Skin1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <center>
        <div>
        
         <h2>
        Log In
    </h2>
   <p>
        Please enter your Domin username and password.
       <%-- <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false"><%--Register--%></asp:HyperLink> <%--if you don't have an account.--%>
    </p>
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="true" RememberMeSet="True" OnAuthenticate="LoginUser_Authenticate">
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server" EnableViewState="false"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="LoginUserValidationGroup"/>
            <div class="accountInfo">
           
                <fieldset class="login">
                    <%--<legend style="vertical-align:middle;">Account Information</legend>--%>
                   
                    <p>
                       <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                   
                    <p>
                       <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    
                   <%-- <p>
                        <asp:CheckBox ID="RememberMe" runat="server"/>
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                    </p>--%>
                   
                </fieldset>
                
                <p class="submitButton">
                    <asp:Button ID="LoginButton" CssClass="btn btn-primary configure" runat="server" CommandName="Login" Text="Log In" 
                        ValidationGroup="LoginUserValidationGroup" />
                </p>
            </div>
        </LayoutTemplate>
    </asp:Login>
    
    </div>
    </center>
   
</asp:Content>
