<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Search.aspx.cs" Inherits="POManagementASPNet.Search1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

  <link href="StyleSheet1.css" rel="Stylesheet" type="text/css" />
    <link href="StyleSheet2.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<table>
    <tr id="trSearchPO" runat="server">
        <td>
            <div style="float: right; vertical-align: top">
                <asp:Label ID="lblSearchMessage" runat="server" Font-Bold="false"></asp:Label>
                <asp:TextBox ID="txtSearchPO" runat="server" ToolTip="Enter a PO Number to Search"></asp:TextBox>
                <asp:Button ID="btnSearchPO" OnClick="btnSearchPO_Click" runat="server" Text="Search PO"
                    CausesValidation="False" ToolTip="Enter a PO Number to Search" />
                <br />
                <asp:LinkButton ID="lnkAdvanceSearch" runat="server" Font-Bold="false" Font-Size="9pt"
                    CausesValidation="false" OnClick="lnkAdvanceSearch_Click">Advance Search</asp:LinkButton>
            </div>
        </td>
    </tr>
</table>--%>
    <table>
        <tr>
            <td align="right">
                <asp:Label ID="label1" runat="server" ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Panel ID="pnlAdvanceSearch" runat="server" BorderWidth="1px">
                    <table width="100%">
                        <%--<tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;&nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>--%>
                        <tr>
                            <td>
                                <asp:Label ID="lblRequester" runat="server" Text="Requester"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpAdvSearchRequester" runat="server" Width="250px" DataValueField="RequesterID"
                                    DataTextField="RequesterName">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Station"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpAdvSearchStation" runat="server" Width="250px" DataValueField="StationID"
                                    DataTextField="StationName">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <%--  <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>--%>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Ship To"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpAdvSearchShipTo" runat="server" Width="250px" DataValueField="ShipToID"
                                    DataTextField="City">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Supplier"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpAdvSearchSupplier" runat="server" Width="250px" DataValueField="SupplierID"
                                    DataTextField="CompanyName">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <%-- <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>--%>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Department"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpAdvSearchDepartment" runat="server" Width="250px" DataValueField="DepartmentID"
                                    DataTextField="DepartmentName">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Comment like"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdvSearchComment" runat="server" Width="245px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProject" runat="server" Text="Project"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpAdvSearchProject" runat="server" Width="250px" DataValueField="ProjectID"
                                    DataTextField="ProjectName">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblCircuitId" runat="server" Text="Circuit ID"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCircuit" runat="server" Width="245px"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="acCircuitID" runat="server" ServiceMethod="GetCircuitID"
                                    ServicePath="NewPO.aspx" MinimumPrefixLength="1" CompletionSetCount="1" CompletionInterval="0"
                                    TargetControlID="txtCircuit" EnableCaching="false" FirstRowSelected="false" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                </cc1:AutoCompleteExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="btnAdvSearchPO" runat="server" CausesValidation="False" OnClick="btnAdvSearchPO_Click"
                                    Text="Search PO" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <asp:CheckBox ID="chkSearchPObyPartDescription" Text="Search PO by Description of Part/Supplier Code"
                    runat="server" OnCheckedChanged="chkSearchPObyPartDescription_CheckedChanged"
                    AutoPostBack="True" />
                <br />
                <br />
                <asp:Panel ID="pnlAdvanceSearchByPartDescription" Enabled="false" runat="server"
                    BorderWidth="1px">
                    <table width="100%">
                        <%-- <tr>
                                <td>
                                    &nbsp;&nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>--%>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Decription of Part like"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdvSearchDecriptionofPart" runat="server" Width="245px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Suppliers Code like"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdvSearchSuppliersCode" runat="server" Width="245px"></asp:TextBox>
                            </td>
                        </tr>
                        <%--  <tr>
                                <td colspan="4" align="center">
                                    &nbsp;&nbsp;
                                </td>
                            </tr>--%>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="btnAdvSearchPOByPart" runat="server" CausesValidation="False" OnClick="btnAdvSearchPOByPart_Click"
                                    Text="Search PO" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblAdvanceSearchMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:LinkButton ID="lnkAdvExportToExcel" OnClick="lnkAdvExportToExcel_Click" runat="server">Export To Excel</asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="grdSearchPO" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                    Width="100%" AllowPaging="False" PageSize="20" OnRowDataBound="grdSearchPO_RowDataBound"
                    OnRowCommand="grdSearchPO_RowCommand" CssClass="mGrid" PagerStyle-CssClass="pgr"
                    AlternatingRowStyle-CssClass="alt" DataKeyNames="POMainID">
                    <Columns>
                        <asp:TemplateField HeaderText="Go To">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSearchEdit" CommandName="Edit" runat="server">Edit Page |</asp:LinkButton>
                                <asp:LinkButton ID="lnkSearchApprove" CommandName="Approve" runat="server">Approve Page </asp:LinkButton>
                                <asp:LinkButton ID="lnkSearchClosed" CommandName="Closed" runat="server">Closed PO Page</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="POMainID" SortExpression="POMainID" HeaderText="PO Number" />
                        <asp:BoundField DataField="RequesterName" SortExpression="RequesterName" HeaderText="Requester" />
                        <asp:BoundField DataField="StationName" SortExpression="StationName" HeaderText="Station" />
                        <asp:BoundField DataField="TypeDescription" SortExpression="TypeDescription" HeaderText="PO Type" />
                        <asp:BoundField DataField="City" SortExpression="City" HeaderText="Ship To" />
                        <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Approval Status" />
                         <asp:BoundField DataField="CircuitID" SortExpression="CircuitID" HeaderText="Circuit ID" />
                        <%--<asp:TemplateField HeaderText="Approval Status">
                            <ItemTemplate>
                                <asp:Label ID="lblBindStatus" runat="server" Text='<%# Bind("_ModerationStatus") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField="Comment" SortExpression="Comment" ItemStyle-Width="400px"
                            ItemStyle-Wrap="true" HeaderText="Comment" />
                    </Columns>
                    <RowStyle Height="25px" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
