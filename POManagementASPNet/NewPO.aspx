<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="NewPO.aspx.cs" Inherits="POManagementASPNet.NewPO" Theme="Skin1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="UC" TagName="NumberTextBox" Src="~/NumberTextBox.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="StyleSheet1.css" rel="Stylesheet" type="text/css" />
    <link href="StyleSheet2.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function calculateprice() {
            //debugger;
            var qty = document.getElementById('<%= txtQuantity.ClientID %>').value; //ctl00_m_g_60e49b11_5324_45e8_8514_e94cb4859937_ctl00_
            var unitprice = document.getElementById('<%= txtUnitPrice.ClientID %>').value;
            var totalprice = qty * unitprice;
            document.getElementById('<%= txtTotalPrice.ClientID %>').value = totalprice;
        }

        function DeletePO() {
            //debugger;
            var result = confirm('Do you want to Delete this PO');
            if (result == true)
                return true;
            else
                return false;

        }
        //        function ShowPopUp() {
        //            $find("ModalPopupExtender1").show();
        //        }

        function getUserID(val) {
            debugger;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" PopupControlID="pnlPopUp"
                BackgroundCssClass="modalbackground" TargetControlID="lnkAddNewproject" BehaviorID="ModalPopupExtender1">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlPopUp" runat="server" Style="display: none" CssClass="modalpopup">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblNewProject" runat="server" Text="Project"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewProject" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewProject" ControlToValidate="txtNewProject"
                                runat="server" Display="Dynamic" ErrorMessage="Please fill Project Name" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnOK" Text="OK" runat="server" OnClick="btnOK_Click" />
                            <asp:Button ID="btnPCancel" runat="server" Text="Cancel" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>--%>
    <asp:UpdatePanel runat="server" ID="upNewPO" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="drpType" />
            <asp:PostBackTrigger ControlID="drpSupplier" />
            <asp:PostBackTrigger ControlID="drpDepartment" />
            <asp:PostBackTrigger ControlID="drpOrderClassification" />
            <asp:PostBackTrigger ControlID="btnAddItemToPO" />
            <asp:PostBackTrigger ControlID="btnSubmitPO" />
            <asp:PostBackTrigger ControlID="btnUpdatePO" />
            <asp:PostBackTrigger ControlID="btnDeletePO" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <%-- <asp:PostBackTrigger ControlID="lnkImportItems" />--%>
            <%--<asp:AsyncPostBackTrigger ControlID="lnkImportItems" EventName="Click" />--%>
        </Triggers>
        <ContentTemplate>
            <table width="110%">
                <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Visible="false" Font-Size="12px"></asp:Label>
                <tr>
                    <td>
                        <asp:Panel ID="pnlNewPO" runat="server" BorderWidth="0px">
                            <asp:Label ID="lblPOTitle" runat="server" Text="Open New PO" Font-Bold="True" Font-Size="12px"
                                Style="text-decoration: underline"></asp:Label>
                            <br />
                            <br />
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRequester" runat="server" Text="Requester" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpRequester" runat="server" Width="250px" DataValueField="RequesterID"
                                            DataTextField="RequesterName" AutoPostBack="true" OnSelectedIndexChanged="drpRequester_SelectedIndexChanged"
                                            Visible="false">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="drpRequester"
                                            runat="server" ErrorMessage="You must specify a value for this required field."
                                            Display="Dynamic" ForeColor="Red" InitialValue="0" ValidationGroup="SubmitPO"></asp:RequiredFieldValidator>
                                        <%-- <cc1:ValidatorCalloutExtender ID="RequesterValidatorCalloutExtender" runat="server"
                                            TargetControlID="RequiredFieldValidator1"></cc1:ValidatorCalloutExtender>--%>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblProject" runat="server" Text="Project"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpProject" runat="server" Width="250px" DataValueField="ProjectID"
                                            DataTextField="ProjectName"> <%--OnSelectedIndexChanged="drpProject_SelectedIndexChanged"
                                            AutoPostBack="true" --%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvProject" runat="server" ControlToValidate="drpProject"
                                            Display="Dynamic" ErrorMessage="You must specify a value for this required field."
                                            ForeColor="Red" InitialValue="0" ValidationGroup="SubmitPO"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblShipTo" runat="server" Text="Ship To"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpShipTo" runat="server" Width="250px" DataValueField="ShipToID"
                                            DataTextField="City">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvShipTo" runat="server" ControlToValidate="drpShipTo"
                                            Display="Dynamic" ErrorMessage="You must specify a value for this required field."
                                            ForeColor="Red" InitialValue="0" ValidationGroup="SubmitPO"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text="Type"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpType" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="drpType_SelectedIndexChanged"
                                            DataValueField="TypeID" DataTextField="TypeDescription">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvType" runat="server" Display="Dynamic" ErrorMessage="You must specify a value for this required field."
                                            ForeColor="Red" ControlToValidate="drpType" InitialValue="0" ValidationGroup="SubmitPO"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDepartment" runat="server" Text="Department"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpDepartment" runat="server" Width="250px" DataValueField="DepartmentID"
                                            DataTextField="DepartmentName" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvdrpDepartment" runat="server" ControlToValidate="drpDepartment"
                                            Display="Dynamic" ErrorMessage="You must specify a value for this required field."
                                            ForeColor="Red" InitialValue="0" ValidationGroup="SubmitPO"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTypeDesc" Text="Type Description" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTypeDescText" runat="server" Width="250PX" BackColor="Aquamarine"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOrderClassification" runat="server" Text="Order Classification"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpOrderClassification" runat="server" Width="250px" DataValueField="OrderClassificationID"
                                            DataTextField="OrderClassificationName" Enabled="false" OnSelectedIndexChanged="drpOrderClassification_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="You must specify a value for this required field."
                                            ControlToValidate="drpOrderClassification" Display="Dynamic" ForeColor="Red"
                                            InitialValue="0" ValidationGroup="SubmitPO"></asp:RequiredFieldValidator>
                                        <%--<cc1:ValidatorCalloutExtender ID="vceOrder" runat="server" TargetControlID="RequiredFieldValidator2">
                                            </cc1:ValidatorCalloutExtender>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblStation" runat="server" Text="Station"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpStation" runat="server" Width="250px" DataValueField="StationID"
                                            DataTextField="StationName" OnSelectedIndexChanged="drpStation_SelectedIndexChanged"
                                            AutoPostBack="true" Enabled="false">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvStation" runat="server" ControlToValidate="drpStation"
                                            Display="Dynamic" ErrorMessage="You must specify a value for this required field."
                                            ForeColor="Red" InitialValue="0" ValidationGroup="SubmitPO"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSupplier" runat="server" Text="Supplier"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpSupplier" runat="server" Width="250px" OnSelectedIndexChanged="drpSupplier_SelectedIndexChanged"
                                            AutoPostBack="true" DataValueField="SupplierID" DataTextField="CompanyName">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvdrpSupplier" runat="server" ControlToValidate="drpSupplier"
                                            Display="Dynamic" ErrorMessage="You must specify a value for this required field."
                                            ForeColor="Red" InitialValue="0" ValidationGroup="SubmitPO"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblBudgetCode" runat="server" Text="Budget Code" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBudgetCode" runat="server" Visible="false" DataTextField="BudgetCodeNumber"
                                            DataValueField="BudgetCodeID" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTerm" Text="Supplier payment Term" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTerm" runat="server" Width="240px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInBudget" runat="server" Text="In Budget"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpInBudget" runat="server" Width="250px" DataValueField="BudgetedFieldID"
                                            DataTextField="FieldName">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvdrpInBudget" runat="server" ControlToValidate="drpInBudget"
                                            Display="Dynamic" ErrorMessage="You must specify a value for this required field."
                                            ForeColor="Red" InitialValue="0" ValidationGroup="SubmitPO"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNRC" runat="server" Text="NRC"></asp:Label>
                                    </td>
                                    <td>
                                        <UC:NumberTextBox ID="txtNRC" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCurrency" runat="server" Text="Currency"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpCurrency" runat="server" Width="250px" DataValueField="CurrencyID"
                                            DataTextField="CurrencyName">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpCurrency"
                                            Display="Dynamic" ErrorMessage="You must specify a value for this required field."
                                            ForeColor="Red" InitialValue="0" ValidationGroup="SubmitPO"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMRC" runat="server" Text="MRC"></asp:Label>
                                    </td>
                                    <td>
                                        <UC:NumberTextBox ID="txtMRC" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOpportunity" runat="server" Text="Sales Opportunity ID"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOpportunity" runat="server"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="acOpportunity" runat="server" ServiceMethod="GetOpportunityID"
                                            ServicePath="NewPO.aspx" MinimumPrefixLength="1" CompletionSetCount="1" CompletionInterval="0"
                                            TargetControlID="txtOpportunity" EnableCaching="false" FirstRowSelected="false"
                                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTail" runat="server" Text="Tail/Circuit Term"></asp:Label>
                                    </td>
                                    <td>
                                        <UC:NumberTextBox ID="txtTail" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEstimatedDeliveryTime" Text="Estimated Delivery Time" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEstimatedDeliveryTime" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calEstDeliveryDate" runat="server" TargetControlID="txtEstimatedDeliveryTime"
                                            Format="dd-MMM-yyyy">
                                        </cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvEstDeliveryDate" runat="server" ControlToValidate="txtEstimatedDeliveryTime"
                                            Display="Dynamic" ErrorMessage="You must specify a value for this required field."
                                            ForeColor="Red" ValidationGroup="SubmitPO"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCircuitID" Text="Circuit ID" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCircuit" runat="server" Visible="false"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="acCircuitID" runat="server" ServiceMethod="GetCircuitID"
                                            ServicePath="NewPO.aspx" MinimumPrefixLength="1" CompletionSetCount="1" CompletionInterval="0"
                                            TargetControlID="txtCircuit" EnableCaching="false" FirstRowSelected="false" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2" valign="top">
                                        <asp:Label ID="lblComment" runat="server" Text="Comment"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Height="70px" Width="400px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment"
                                            Display="Dynamic" ErrorMessage="You must specify a value for this required field."
                                            ForeColor="Red" ValidationGroup="SubmitPO"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Button ID="btnAddItemToPO" CssClass="btn btn-primary configure" runat="server"
                                            Text="Add Item To PO" OnClick="btnAddItemToPO_Click" CausesValidation="False" />
                                        &nbsp;<asp:Button ID="btnSubmitPO" CssClass="btn btn-primary configure" runat="server"
                                            CausesValidation="true" Text="Submit PO" OnClick="btnSubmitPO_Click" ValidationGroup="SubmitPO" />
                                        <asp:Button ID="btnUpdatePO" CssClass="btn btn-primary configure" runat="server"
                                            Text="Update PO" OnClick="btnSubmitPO_Click" ValidationGroup="SubmitPO" />
                                        &nbsp;<asp:Button ID="btnDeletePO" CssClass="btn btn-primary configure" runat="server"
                                            Text="Delete PO" Visible="false" OnClick="btnDeletePO_Click" CausesValidation="False"
                                            OnClientClick="return DeletePO();" />
                                        &nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary configure"
                                            Text="Cancel" Visible="false" OnClick="btnCancel_Click" CausesValidation="False" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upNewPO"
        DisplayAfter="0">
        <ProgressTemplate>
            <div style="position: fixed; left: 41%; top: 40%; z-index: 100600; background-color: white;
                width: 190px; height: 35px; border: 1px solid #fcac37; padding: 5px;">
                <img src="images/UpdateProgress.gif" alt="" />
                Processing, please wait...
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <br />
    <asp:Panel ID="pnlAddItemsToPO" runat="server" Visible="false" Width="100%">
        <table style="width: 100%;">
            <tr style="font-weight: bold">
                <td>
                    <asp:Label ID="lblDescriptionOfPart" runat="server" Text="Description Of Part"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblSuppliersCode" runat="server" Text="Suppliers Code"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblUnitPrice" runat="server" Text="Unit Price"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTotalPrice" runat="server" Text="Total Price"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblGlCode" runat="server" Text="GL Code" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td rowspan="2">
                    <asp:TextBox ID="txtDescriptionOfPart" runat="server" Height="70px" TextMode="MultiLine"
                        Width="250px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtSuppliersCode" runat="server" Width="127px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtQuantity" runat="server" Width="127px">0</asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtUnitPrice" runat="server" onblur="calculateprice();" Width="127px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtTotalPrice" runat="server" ReadOnly="true" Width="126px"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGlCode" runat="server" Width="250px" DataValueField="GLCodeID"
                        DataTextField="GLCodeName" Visible="false">
                    </asp:DropDownList>
                    <%-- <asp:RequiredFieldValidator ID="rfvGlCode" runat="server" ControlToValidate="ddlGlCode"
                        Display="Dynamic" ErrorMessage="You must specify a value for this required field."
                        ForeColor="Red" InitialValue="0" ValidationGroup="SubmitPO"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div style="float: left; vertical-align: top; text-align: right">
                        Browse to Import Items from Excel File
                        <asp:FileUpload ID="POItemsFileUpload" runat="server" />
                        &nbsp;<asp:LinkButton ID="lnkImportItems" runat="server" CausesValidation="false"
                            Font-Size="9pt" OnClick="lnkImportItems_Click" ToolTip="Import Items from Excel file">Import</asp:LinkButton>
                        &nbsp;|&nbsp;<asp:HyperLink ID="HyperLink1" NavigateUrl="~/SharedDocuments/PO Items Sample File.xlsx"
                            Font-Size="9pt" runat="server">Click Here</asp:HyperLink>
                        To Download Sample File
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:LinkButton ID="lnkAddAnotherItem" runat="server" OnClick="lnkAddAnotherItem_Click"
                        CausesValidation="False" Font-Size="11pt">Add Another Item</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView ID="grdItems" runat="server" Width="100%" OnRowDataBound="grdItems_RowDataBound"
                        ShowFooter="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        OnRowCommand="grdItems_RowCommand" AutoGenerateColumns="false" DataKeyNames="POItemID"
                        OnRowCreated="grdItems_RowCreated">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" CommandName="EditRecord" runat="server">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" CommandName="DeleteRecord" runat="server">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Description Of Part" HeaderText="Description Of Part"
                                SortExpression="Description Of Part" />
                            <asp:BoundField DataField="Suppliers Code" HeaderText="Suppliers Code" SortExpression="Suppliers Code" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
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
                            <asp:TemplateField HeaderText="GL Code">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlGLCode1" runat="server" DataValueField="GLCodeID" DataTextField="GLCodeName"
                                        Enabled="false" Width="250px" Visible="false">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle HorizontalAlign="Right" />
                        <RowStyle Height="25px" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
