<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ApprovalStatus.aspx.cs" Inherits="POManagementASPNet.ApprovalStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlApprovePO" runat="server">
                    <asp:Label ID="lblApprovePOTitle" runat="server" Text="Approval Status" Font-Bold="True"
                        Font-Size="12px" Style="text-decoration: underline"></asp:Label>
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" runat="server" Font-Bold="true" Font-Size="14px" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="grdApprovalStatus" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                        Width="100%" AllowPaging="True" PageSize="20" CssClass="mGrid" PagerStyle-CssClass="pgr"
                        AlternatingRowStyle-CssClass="alt">
                        <AlternatingRowStyle BackColor="White" />
                        <%--  <EditRowStyle BackColor="#2461BF" />--%>
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
                            <asp:BoundField DataField="PONumber" SortExpression="PONumber" HeaderText="PO Number" />
                            <asp:BoundField DataField="CreatedDate" SortExpression="CreatedDate" HeaderText="Created Date"
                                HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="RequesterName" SortExpression="RequesterName" HeaderText="Requester" />
                            <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Status" ItemStyle-Width="50px" />
                            <asp:BoundField DataField="ApprovedBy" SortExpression="ApprovedBy" HeaderText="Approved By" />
                            <asp:BoundField DataField="ApprovalDate" SortExpression="ApprovalDate" HeaderText="Approval Date"
                                HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="Comment" SortExpression="Comment" ItemStyle-Width="200px"
                                ItemStyle-Wrap="true" HeaderText="Approver Comment" />
                            <%--<asp:BoundField DataField="StationName" SortExpression="StationName" HeaderText="Station" />
                            <asp:BoundField DataField="TypeDescription" SortExpression="TypeDescription" HeaderText="PO Type" />
                            <asp:BoundField DataField="City" SortExpression="City" HeaderText="Ship To" />
                            <asp:TemplateField HeaderText="Total Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Bind("CurrencyName") %>'></asp:Label>
                                    <asp:Label ID="lblTotalValue" runat="server" Text='<%# Bind("TotalValue", "{0:n2}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="TotalValueUSD" HeaderText="PO Value(USD)" SortExpression="TotalValueUSD"
                                DataFormatString="{0:n2}" />
                            <asp:BoundField DataField="ApprovedPOValue" HeaderText="PO Value(USD) of Last Approval" SortExpression="TotalValueUSD"
                                DataFormatString="{0:n2}" />
                        </Columns>
                        <RowStyle Height="25px" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
