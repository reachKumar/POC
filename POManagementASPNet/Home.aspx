<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="POManagementASPNet.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDepartment" runat="server" Text="Department"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDept" runat="server" Width="250px" DataValueField="DepartmentID"
                    DataTextField="DepartmentName">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                    Display="Dynamic" ErrorMessage="You must specify a value for this required field."
                    InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPOType" runat="server" Text="Purchase Type"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlPOType" runat="server" Width="250px" DataValueField="TypeID"
                    DataTextField="TypeDescription" AutoPostBack="true" OnSelectedIndexChanged="ddlPOType_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvPOType" runat="server" ControlToValidate="ddlPOType"
                    Display="Dynamic" ErrorMessage="You must specify a value for this required field."
                    InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</asp:Content>
