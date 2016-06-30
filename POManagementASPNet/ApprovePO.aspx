<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ApprovePO.aspx.cs" Inherits="POManagementASPNet.ApprovePO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlApprovePO" runat="server">
                    <asp:Label ID="lblApprovePOTitle" runat="server" Text="Approve Pending Purchase Orders"
                        Font-Bold="True" Font-Size="12px" Style="text-decoration: underline"></asp:Label>
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" runat="server" Font-Bold="true" Font-Size="14px" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="grdApprovePO" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="grdApprovePO_PageIndexChanging"
                        PageSize="20" OnRowCommand="grdApprovePO_RowCommand" CssClass="mGrid" PagerStyle-CssClass="pgr"
                        AlternatingRowStyle-CssClass="alt" DataKeyNames="Project,CreatedBy">
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
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSelect" CommandName="Select" runat="server" CommandArgument="<%#((GridViewRow) Container).RowIndex %>">Select</asp:LinkButton>
                                    |
                                    <asp:LinkButton ID="lnkView" CommandName="View" runat="server">View</asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="lnkApprove" OnClientClick="javascript:return confirm('Do you want to Approve this PO');"
                                        CommandName="Approve" runat="server" CommandArgument="<%#((GridViewRow) Container).RowIndex %>">Approve |</asp:LinkButton>
                                    <asp:LinkButton ID="lnkReject" OnClientClick="javascript:return confirm('Do you want to Reject this PO');"
                                        CommandName="Reject" runat="server" CommandArgument="<%#((GridViewRow) Container).RowIndex %>">Reject</asp:LinkButton>
                                </EditItemTemplate>
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO Number">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPOMainID" runat="server" Text='<%# Eval("POMainID") %>' CommandArgument='<%#Eval("POMainID") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="lnkPOMainID" runat="server" Text='<%# Eval("POMainID") %>' CommandArgument='<%#Eval("POMainID") %>'></asp:LinkButton>
                                    <%--<asp:Label ID="lblPOMainId" runat="server" Text='<%# Eval("POMainID") %>'></asp:Label>--%>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requester">
                                <ItemTemplate>
                                    <asp:Label ID="lblRequesterName" runat="server" Text='<%# Eval("RequesterName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblRequesterName" runat="server" Text='<%# Eval("RequesterName") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Station">
                                <ItemTemplate>
                                    <asp:Label ID="lblStationName" runat="server" Text='<%# Eval("StationName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblStationName" runat="server" Text='<%# Eval("StationName") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblTypeDescription" runat="server" Text='<%# Eval("TypeDescription") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblTypeDescription" runat="server" Text='<%# Eval("TypeDescription") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ship To">
                                <ItemTemplate>
                                    <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Bind("CurrencyName") %>'></asp:Label>
                                    <asp:Label ID="lblTotalValue" runat="server" Text='<%# Bind("TotalValue", "{0:n2}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Bind("CurrencyName") %>'></asp:Label>
                                    <asp:Label ID="lblTotalValue" runat="server" Text='<%# Bind("TotalValue", "{0:n2}") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO Value(USD)">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalValueUSD" runat="server" Text='<%# Eval("TotalValueUSD" , "{0:n2}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblTotalValueUSD" runat="server" Text='<%# Eval("TotalValueUSD" , "{0:n2}") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%# Eval("CreatedDate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%# Eval("CreatedDate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approval Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estimated Delivery Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstimatedDeliveryTime" runat="server" Text='<%# Eval("EstimatedDeliveryTime","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblEstimatedDeliveryTime" runat="server" Text='<%# Eval("EstimatedDeliveryTime","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comment">
                                <ItemTemplate>
                                    <asp:Label ID="lblComment" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblComment" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:BoundField DataField="POMainID" SortExpression="POMainID" HeaderText="PO Number" />
                         <asp:BoundField DataField="RequesterName" SortExpression="RequesterName" HeaderText="Requester" />
                            <asp:BoundField DataField="StationName" SortExpression="StationName" HeaderText="Station" />
                            <asp:BoundField DataField="TypeDescription" SortExpression="TypeDescription" HeaderText="PO Type" />
                            <asp:BoundField DataField="City" SortExpression="City" HeaderText="Ship To" />
                            <asp:TemplateField HeaderText="Total Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Bind("CurrencyName") %>'></asp:Label>
                                    <asp:Label ID="lblTotalValue" runat="server" Text='<%# Bind("TotalValue", "{0:n2}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TotalValueUSD" HeaderText="PO Value(USD)" SortExpression="TotalValueUSD"
                                DataFormatString="{0:n2}" />
                            <asp:BoundField DataField="CreatedDate" SortExpression="CreatedDate" HeaderText="Date"
                                HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Approval Status"
                                ItemStyle-Width="50px" />
                            <asp:BoundField DataField="EstimatedDeliveryTime" SortExpression="EstimatedDeliveryTime"
                                HeaderText="Estimated Delivery Time" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="Comment" SortExpression="Comment" ItemStyle-Width="200px"
                                ItemStyle-Wrap="true" HeaderText="Comment" />--%>
                            <asp:TemplateField HeaderText="Approver Comment">
                                <ItemTemplate>
                                    &nbsp;
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtComment"
                                        ErrorMessage="Please provide comment">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle Height="25px" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
