<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="PrintPO.aspx.cs" Inherits="POManagementASPNet.PrintPO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function Print(strid) {
            //debugger;
            document.getElementById('<%= btnPrint.ClientID %>').style.display = "none";
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            //prtContent.innerHTML = strOldOne;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <asp:Panel ID="pnlPrintAddressDroppdown" runat="server" Visible="false">
                    Print Billing Address:
                    <asp:DropDownList ID="drpPrintAddressDroppdown" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="drpPrintAddressDroppdown_SelectedIndexChanged">
                        <asp:ListItem Selected="True">VAT</asp:ListItem>
                        <asp:ListItem>Additional</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                </asp:Panel>
                <div id="dvPrintPO">
                    <table width="800px" style="font-size: 1em">
                        <tr valign="top">
                            <td>
                                <table>
                                    <tr valign="top">
                                       <%-- <td>
                                            <img width="150px" src="images/NewHibernialogo1.jpg" alt="" />
                                        </td>--%>
                                        <td style="width: 20px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblBillTo" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                <h2>
                                    PO number:</h2>
                                <b>Requester:</b><asp:Label ID="lblRequesterName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <h1>
                                    <asp:Label ID="lblPrintPONumber" runat="server" Text=""></asp:Label></h1>
                            </td>
                        </tr>
                        <%-- <tr align="right"><td align="right">Requester:</td></tr>--%>
                        <tr>
                            <td>
                            </td>
                            <td align="right">
                                <span style="float: left">Terms:</span>
                                <asp:DropDownList ID="drpTerms" runat="server" DataValueField="ID" DataTextField="ConditionName"
                                    Width="450px" OnSelectedIndexChanged="drpTerms_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="right">
                                <span style="float: left">Shipping Terms:</span>
                                <asp:DropDownList ID="drpShippingTerms" runat="server" DataValueField="ShipTermId"
                                    DataTextField="ShipTermName" OnSelectedIndexChanged="drpShippingTerms_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr align="left" style="width: 800px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="100%">
                                    <tr valign="top">
                                        <td style="font-weight: bold">
                                            Supplier:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPrintPOSupplier" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <table>
                                                <tr valign="top">
                                                    <td>
                                                        <span style="font-weight: bold">Deliver To: </span>
                                                    </td>
                                                    <td>
                                                        &nbsp; &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPrintPODeliverTo" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Tel Number:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPrintPOTelNumber" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Fax Number:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPrintPOFaxNumber" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="right">
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            Date Entered:
                                            <asp:Label ID="lblPrintPODateEntered" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:GridView ID="grdPrintPOItems" Width="100%" runat="server" AutoGenerateColumns="False"
                                                EnableModelValidation="True" OnRowDataBound="grdPrintPOItems_RowDataBound" ShowFooter="True">
                                                <Columns>
                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                    <asp:BoundField DataField="Suppliers Code" ItemStyle-Width="150px" HeaderText="Suppliers Code" />
                                                    <asp:BoundField DataField="Description Of Part" ItemStyle-Width="400px" HeaderText="Description Of Part" />
                                                    <%--  <asp:BoundField DataField="Unit Price" HeaderText="Unit Price" />--%>
                                                    <asp:TemplateField HeaderText="Unit Price">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Bind("UnitPrice", "{0:n2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Currency" HeaderText="Currency" />
                                                    <%-- <asp:BoundField DataField="Total Price" HeaderText="Total Price" />--%>
                                                    <asp:TemplateField HeaderText="Total Price">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Bind("TotalPrice", "{0:n2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Total Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCurrency" runat="server" Text='<%# Bind("CurrencyName") %>'></asp:Label>
                                                            <asp:Label ID="lblTotalValue" runat="server" Text='<%# Bind("TotalValue", "{0:n2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                            <br />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            Signed:________________________________________________
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            Date:__________________________________________________
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            All invoices must contain a PO number. Invoices received without a PO number will
                                            not be processed.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center">
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:Button ID="btnPrint" CssClass="btn btn-primary configure" runat="server" OnClientClick="javascript:Print('dvPrintPO');"
                                                Text="Print" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
