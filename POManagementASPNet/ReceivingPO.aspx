<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ReceivingPO.aspx.cs" Inherits="POManagementASPNet.ReceivingPO" Theme="Skin1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function validatePOReceived() {

            debugger;
            var grid = document.getElementById("<%= grdReceivingPOItems.ClientID%>");
            var inputs = grid.getElementsByTagName("input");
            for (var i = 1; i < grid.rows.length - 1; i++) 
            {
                var obj = grid.rows[i].cells[7];
                var rec = grid.rows[i].cells[5];
                for (j = 0; j < obj.childNodes.length; j++) 
                {
                    if (obj.childNodes[j].type == "text")
                    {
                        var recQty = obj.childNodes[1].value;
                        if (recQty =="")
                        {recQty = "0"; }
                    }
                    if (rec.childNodes[j].type == "checkbox") 
                    {
//                        var recValue1 = rec.childNodes[1].disabled;
//                        if (!recValue1)
//                        {
                            var recValue = rec.childNodes[1].checked;
//                        }

                    }
                }
                var outQty = grid.rows[i].cells[8].innerText;
                if (recValue) 
                {
                    if (parseInt(recQty) == parseInt(outQty))
                    { }
                    else 
                    {
                        alert('Received Quantity and Outstanding Quantity are not equal');
                        return false;                        
                    }
                }
                else
                {
                    if (parseInt(recQty) > parseInt(outQty))
                    {
                        alert('Received Quantity can not be greater than Outstanding Quantity');
                        return false;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="font-size: 16px; font-weight: bold">
                PO Number:
            </td>
            <td style="font-size: 16px; font-weight: bold">
                <asp:Label ID="lblRPONumber" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Requester:
            </td>
            <td>
                <asp:Label ID="lblRRequester" runat="server"></asp:Label>
            </td>
            <td>
                Order Classification:
            </td>
            <td>
                <asp:Label ID="lblROrderClassification" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Station:
            </td>
            <td>
                <asp:Label ID="lblRStation" runat="server"></asp:Label>
            </td>
            <td>
                Department:
            </td>
            <td>
                <asp:Label ID="lblRDepartment" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Type:
            </td>
            <td>
                <asp:Label ID="lblRType" runat="server"></asp:Label>
            </td>
            <td>
                Project:
            </td>
            <td>
                <asp:Label ID="lblRProject" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Ship To:
            </td>
            <td>
                <asp:Label ID="lblRShipTo" runat="server"></asp:Label>
            </td>
            <td>
                Status:
            </td>
            <td>
                <asp:Label ID="lblRStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Supplier:
            </td>
            <td>
                <asp:Label ID="lblRSupplier" runat="server"></asp:Label>
            </td>
            <td>
                Approved By:
            </td>
            <td>
                <asp:Label ID="lblRApprovedBy" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Budgetted:
            </td>
            <td>
                <asp:Label ID="lblRInBudget" runat="server"></asp:Label>
            </td>
            <td>
                Date Approved:
            </td>
            <td>
                <asp:Label ID="lblRDateApproved" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Received:
            </td>
            <td>
                <asp:CheckBox ID="chkRReceived" Enabled="false" runat="server" />
            </td>
            <td>
                EU Build:
            </td>
            <td>
                <asp:CheckBox ID="chkREUBuild" Enabled="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Finance Code:
            </td>
            <td>
                <asp:Label ID="lblRFinanceCode" runat="server"></asp:Label>
            </td>
            <td>
                WO:
            </td>
            <td>
                <asp:Label ID="lblRWO" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Date Received:
            </td>
            <td>
                <asp:Label ID="lblRDateReceived" runat="server"></asp:Label>
            </td>
            <td>
                Comment:
            </td>
            <td rowspan="2" valign="top">
                <asp:Label ID="lblRComment" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Invoiced:
            </td>
            <td>
                <asp:CheckBox ID="chkInvoicedMain" Enabled="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Invoiced Date:
            </td>
            <td>
                <asp:Label ID="lblInvoicedDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Currency:
            </td>
            <td>
                <asp:Label ID="lblCurrency" runat="server"></asp:Label>
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
            <td colspan="4">
                <asp:GridView ID="grdReceivingPOItems" runat="server" AutoGenerateColumns="False"
                    EnableModelValidation="True" ShowFooter="True" Width="100%" OnRowCommand="grdReceivingPOItems_RowCommand"
                    OnRowDataBound="grdReceivingPOItems_RowDataBound" CssClass="mGrid" PagerStyle-CssClass="pgr"
                    AlternatingRowStyle-CssClass="alt" DataKeyNames="POItemID">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <Columns>
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="Suppliers Code" HeaderText="Suppliers Code" />
                        <asp:BoundField DataField="Description Of Part" HeaderText="Description Of Part" />
                        <asp:TemplateField HeaderText="Unit Price">
                            <ItemTemplate>
                                <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Bind("UnitPrice", "{0:n2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Price">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Bind("TotalPrice", "{0:n2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:BoundField DataField="Unit Price" HeaderText="Unit Price" />
                        <asp:BoundField DataField="Total Price" DataFormatString="{0:n}" HeaderText="Total Price"
                            HtmlEncode="false" />--%>
                        <asp:TemplateField HeaderText="Received" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkReceived" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Received Date">
                            <ItemTemplate>
                                <asp:TextBox ID="txtReceivedDate" runat="server" Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="calReceivedDate" runat="server" TargetControlID="txtReceivedDate"
                                    Format="dd-MMM-yyyy">
                                </cc1:CalendarExtender>
                            </ItemTemplate>
                            <ItemStyle Width="110px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Received Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtReceivedQty" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Outstanding Quantity">
                            <ItemTemplate>
                                <asp:Label ID="txtOutstandingQty" runat="server" Text='<%# Bind("OutstandingQuantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="QuantityReceived" HeaderText="Already Received" Visible="true" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Panel ID="pnlInvoice" runat="server" Visible="false">
                    <%-- Added to show the Check box if all PO items are selected--%>
                    <tr>
                        <%--id="pnlTrReceivedChkBox" runat="server">--%>
                        <td>
                            <asp:CheckBox ID="chkInvoiced" runat="server" AutoPostBack="True" OnCheckedChanged="chkInvoiced_CheckedChanged" />Invoiced
                            <asp:Panel ID="pnlPOReceivedDate" runat="server" Visible="false">
                                <asp:Label ID="lblPOReceivedDate" runat="server" Text="Invoice Date"></asp:Label>
                                <asp:TextBox ID="txtPOInvoiceDate" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="calPOInvoicedDate" TargetControlID="txtPOInvoiceDate" runat="server"
                                    Format="dd-MMM-yyyy">
                                </cc1:CalendarExtender>
                            </asp:Panel>
                            <%-- Please select this checkbox only if all the PO Items are received and checked.--%>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlUpdateReceivedItems" runat="server">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnUpdateReceivedPO" CssClass="btn btn-primary configure" runat="server"
                                    Text="Update" OnClick="btnUpdateReceivedPO_Click" OnClientClick="return validatePOReceived();" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
