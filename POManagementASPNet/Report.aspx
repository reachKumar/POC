<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Report.aspx.cs" Inherits="POManagementASPNet.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                    OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Selected="True">PO Summary</asp:ListItem>
                    <asp:ListItem>PO By Month</asp:ListItem>
                    <asp:ListItem>PO By Supplier</asp:ListItem>
                    <asp:ListItem>Circuit PO</asp:ListItem>
                    <asp:ListItem>PO Datewise</asp:ListItem>
                    <asp:ListItem>Back Order</asp:ListItem>
                    <asp:ListItem>PO DeptWise</asp:ListItem>
                </asp:RadioButtonList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <iframe id="iframe1" width="100%" height="550" runat="server"></iframe>
            </td>
        </tr>
    </table>
</asp:Content>
