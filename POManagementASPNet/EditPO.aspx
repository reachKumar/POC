<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditPO.aspx.cs" Inherits="POManagementASPNet.EditPO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <asp:Panel ID="pnlEditPO" runat="server">
                    <asp:Label ID="lblEditTitle" runat="server" Font-Bold="True" Font-Size="12px" Text="Edit Pending Purchase Orders"
                        Style="text-decoration: underline"></asp:Label>
                    <br />
                    <br />
                    <asp:GridView ID="grdEditPO" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                        Width="100%" AutoGenerateEditButton="True" OnRowEditing="grdEditPO_RowEditing"
                        AllowPaging="True" OnPageIndexChanging="grdEditPO_PageIndexChanging" PageSize="20"
                        OnRowCommand="grdEditPO_RowCommand" CssClass="mGrid" PagerStyle-CssClass="pgr"
                        DataKeyNames="POMainID">
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
                            <%-- <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton Width="100%" ID="lnkPrintPO" CommandName="Print" runat="server">Print PO</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="PO Number">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPOMainID" runat="server" Text='<%# Eval("POMainID") %>' CommandArgument='<%#Eval("POMainID") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RequesterName" SortExpression="RequesterName" HeaderText="Requester"
                                ItemStyle-Width="70px" />
                            <asp:BoundField DataField="CreatedDate" SortExpression="CreatedDate" HeaderText="Date"
                                HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="StationName" SortExpression="StationName" HeaderText="Station" />
                            <asp:BoundField DataField="TypeDescription" SortExpression="TypeDescription" HeaderText="PO Type" />
                            <asp:BoundField DataField="City" SortExpression="City" HeaderText="Ship To" ItemStyle-Width="50px" />
                            <asp:TemplateField HeaderText="Total Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Bind("CurrencyName") %>'></asp:Label>
                                    <asp:Label ID="lblTotalValue" runat="server" Text='<%# Bind("TotalValue", "{0:n2}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DepartmentName" SortExpression="DepartmentName" HeaderText="Department"
                                ItemStyle-Width="100px" />
                            <asp:BoundField DataField="EstimatedDeliveryTime" SortExpression="EstimatedDeliveryTime"
                                HeaderText="Estimated Delivery Time" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}"
                                ItemStyle-Width="120px" />
                            <asp:BoundField DataField="Comment" SortExpression="Comment" ItemStyle-Width="200px"
                                ItemStyle-Wrap="true" HeaderText="Comment" />
                        </Columns>
                        <RowStyle Height="25px" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
