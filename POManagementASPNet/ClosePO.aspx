<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ClosePO.aspx.cs" Inherits="POManagementASPNet.ClosePO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlClosedPO" runat="server">
                    <asp:Label ID="lblClosedPOTitle" runat="server" Text="All Closed(Approved, Rejected) Purchase Orders"
                        Font-Bold="True" Font-Size="12px" Style="text-decoration: underline"></asp:Label>
                    <br />
                    <br />
                    <asp:GridView ID="grdClosedPO" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                        Width="100%" AllowPaging="True" PageSize="20" OnPageIndexChanging="grdClosedPO_PageIndexChanging"
                        OnRowCommand="grdClosedPO_RowCommand" CssClass="mGrid" PagerStyle-CssClass="pgr"
                        AlternatingRowStyle-CssClass="alt">
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
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkView" CommandName="View" runat="server">View</asp:LinkButton>
                                    |
                                    <asp:LinkButton ID="lnkPrintPO" CommandName="Print" runat="server">Print PO</asp:LinkButton>
                                    |
                                    <asp:LinkButton ID="lnkResetPO" OnClientClick="javascript:return confirm('Do you want to reset this PO to edit mode?');"
                                        CommandName="Reset" runat="server">Reset</asp:LinkButton>
                                    |
                                    <asp:LinkButton ID="lnkReceivingPO" CommandName="Receiving" runat="server">Receiving PO</asp:LinkButton>
                                    |
                                    <asp:LinkButton ID="lnkInvoicePO" CommandName="InvoicePO" runat="server">Invoice PO</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="250px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="PO Number">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPOMainID" runat="server" Text='<%# Eval("POMainID") %>' CommandArgument='<%#Eval("POMainID") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="POMainID" SortExpression="POMainID" HeaderText="PO Number"
                                ItemStyle-Width="70px" />--%>
                            <asp:BoundField DataField="RequesterName" SortExpression="RequesterName" HeaderText="Requester"
                                ItemStyle-Width="120px" />
                            <asp:BoundField DataField="StationName" SortExpression="StationName" HeaderText="Station" />
                            <asp:BoundField DataField="TypeDescription" SortExpression="TypeDescription" HeaderText="PO Type"
                                ItemStyle-Width="70px" />
                            <asp:BoundField DataField="City" SortExpression="City" HeaderText="Ship To" />
                            <asp:BoundField DataField="CreatedDate" SortExpression="CreatedDate" HeaderText="Date"
                                HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="60px" />
                            <asp:BoundField DataField="ModifiedBy" SortExpression="ModifiedBy" HeaderText="Approved By"
                                ItemStyle-Width="90px" />
                            <%-- <asp:TemplateField HeaderText="Approval Status">--%>
                            <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Approval Status"
                                ItemStyle-Width="80px" />
                            <asp:BoundField DataField="EstimatedDeliveryTime" SortExpression="EstimatedDeliveryTime"
                                HeaderText="Estimated Delivery Time" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="Comment" SortExpression="Comment" ItemStyle-Width="400px"
                                ItemStyle-Wrap="true" HeaderText="Comment" />
                        </Columns>
                        <RowStyle Height="25px" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
