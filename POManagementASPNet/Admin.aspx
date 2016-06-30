<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Admin.aspx.cs" Inherits="POManagementASPNet.Admin" Theme="Skin1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="StyleSheet1.css" rel="Stylesheet" type="text/css" />
    <link href="StyleSheet2.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="4" OnDemand="False"
        ScrollBars="None" VerticalStripWidth="120px" ViewStateMode="Inherit" Width="885px"
        Font-Bold="True" Height="450px" UseVerticalStripPlacement="True">
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Ship To">
            <HeaderTemplate>
                Ship To</HeaderTemplate>
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td style="text-align: right">
                            Search Address :-
                        </td>
                        <td>
                            <asp:TextBox ID="TxtReqSearchAddress" runat="server" Width="200px" OnTextChanged="TxtReqSearchAddress_TextChanged"
                                AutoPostBack="true"></asp:TextBox><asp:Button ID="btnShwShipTo" runat="server" Text="Search"
                                    OnClick="btnShwShipTo_Click" /><asp:Button ID="btnShowPoP" runat="server" Text="Insert New" /><asp:AutoCompleteExtender
                                        ID="AutoCompleteExtender2" runat="server" CompletionInterval="0" CompletionSetCount="1"
                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetShipToName"
                                        ServicePath="" TargetControlID="TxtReqSearchAddress" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                    </asp:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td colspan="2">
                            <asp:GridView ID="gvShowShipTo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                PageSize="5" Width="100%" OnSelectedIndexChanged="gvShowShipTo_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True">
                                        <ItemStyle Width="40px" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShipID" runat="server" Text='<%# Bind("ShipToID") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label></ItemTemplate>
                                        <HeaderStyle Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label></ItemTemplate>
                                        <FooterStyle Width="25%" />
                                        <HeaderStyle Width="25%" Wrap="True" />
                                        <ItemStyle Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VAT Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVAT" runat="server" Text='<%# Bind("VAT") %>'></asp:Label></ItemTemplate>
                                        <ControlStyle Width="45%" />
                                        <FooterStyle Width="45%" />
                                        <HeaderStyle Width="45%" />
                                        <ItemStyle Width="45%" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Additional Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddAddress" runat="server" Text='<%#Bind("NewAddress") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdPanelShipTo" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    ID #
                                </td>
                                <td>
                                    <asp:TextBox ID="txtID" runat="server" Width="176px" Height="22px" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                    City
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCity" runat="server" Height="22px" Width="176px" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddress" runat="server" Height="60px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td>
                                    VAT Address
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVAT" runat="server" Height="60px" Width="179px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Additional Address
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAdditionalAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="BtnEditShipTo" runat="server" Text="Update" OnClick="BtnEditShipTo_Click" /><asp:Button
                                        ID="btnDeleteShipto" runat="server" OnClick="btnDeleteShipto_Click" Text="Delete" /><asp:Button
                                            ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnDeleteShipto" />
                        <asp:PostBackTrigger ControlID="BtnEditShipTo" />
                        <asp:PostBackTrigger ControlID="btnCancel" />
                    </Triggers>
                </asp:UpdatePanel>
                <table>
                    <tr>
                        <td>
                            <asp:ModalPopupExtender ID="InsertShipTO" runat="server" BackgroundCssClass="modalbackground"
                                DropShadow="True" DynamicServicePath="" Enabled="True" CancelControlID="Cancel"
                                PopupControlID="Panel1" TargetControlID="btnShowPoP">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="Panel1" runat="server" CssClass="modal-body" BackColor="#DFE8F6" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2px" Width="461px">
                                <div>
                                    <table class="style1" style="width: 459px">
                                        <tr>
                                            <td>
                                                City <span class="style2">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewCity" runat="server" Height="22px" Width="176px"></asp:TextBox><asp:RequiredFieldValidator
                                                    ID="CityValidator" runat="server" ErrorMessage="*" Font-Size="Smaller" ForeColor="#FF3300"
                                                    ValidationGroup="ShipToValidation" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Address 1 <span class="style2">* </span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAddress1" runat="server" Height="60px" TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                    ID="AddressValidator" runat="server" ErrorMessage="*" Font-Size="Smaller" ForeColor="#FF3300"
                                                    ValidationGroup="ShipToValidation" ControlToValidate="txtAddress1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Address 2
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAddress2" runat="server" Height="60px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                VAT Address
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewVat" runat="server" Height="60px" Width="179px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Additional Address
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewAddAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnInsertShipTo" ValidationGroup="ShipToValidation" runat="server"
                                                    Text="Save" OnClick="btnInsertShipTo_Click" /><asp:Button ID="Cancel" runat="server"
                                                        Text="Cancel" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Requestor">
            <HeaderTemplate>
                Requester</HeaderTemplate>
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="text-align: right">
                            &nbsp;&nbsp;
                        </td>
                        <td style="text-align: right">
                            Search Requester : -
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSearchRequestor" runat="server" autocomplete="off" CssClass="search-input"
                                Width="200px"></asp:TextBox><asp:AutoCompleteExtender ID="AutoCompleteExtender1"
                                    runat="server" CompletionInterval="0" CompletionSetCount="1" DelimiterCharacters=""
                                    Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetRequestorName" ServicePath=""
                                    TargetControlID="txtSearchRequestor" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                </asp:AutoCompleteExtender>
                            <asp:Button ID="BtnSearchRequester" runat="server" OnClick="BtnSearchRequester_Click"
                                Text="Search" /><asp:Button ID="BtnInsertRequetor" runat="server" Text="Insert New" />
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td colspan="2">
                            <asp:GridView ID="gvRequesterView" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvRequesterView_SelectedIndexChanged"
                                Width="100px">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True">
                                        <ItemStyle Width="40px" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="RequesterID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReqID" runat="server" Text='<%# Bind("RequesterID") %>'></asp:Label></ItemTemplate>
                                        <ControlStyle Width="45%" />
                                        <FooterStyle Width="45%" />
                                        <HeaderStyle Width="45%" />
                                        <ItemStyle Width="45%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RequesterName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReqName" runat="server" Text='<%# Bind("RequesterName") %>'></asp:Label></ItemTemplate>
                                        <ControlStyle Width="45%" />
                                        <FooterStyle Width="45%" />
                                        <HeaderStyle Width="45%" />
                                        <ItemStyle Width="45%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Role Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRoleName" runat="server" Text='<%# Bind("[RoleName]") %>'></asp:Label></ItemTemplate>
                                        <ControlStyle Width="45%" />
                                        <FooterStyle Width="45%" />
                                        <HeaderStyle Width="45%" />
                                        <ItemStyle Width="45%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdPanelRequester" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    ID # :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRequesterID" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Requester Name :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRequesterName" runat="server" ReadOnly="True" Width="161px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Role Name :
                                </td>
                                <td>
                                    <asp:DropDownList ID="DrpDwnRoleNames" runat="server" Width="161px">
                                        <asp:ListItem Value="0">------------Select-------------</asp:ListItem>
                                        <asp:ListItem Value="1">PORequester</asp:ListItem>
                                        <asp:ListItem Value="2">Supervisor</asp:ListItem>
                                        <asp:ListItem Value="3">HOD</asp:ListItem>
                                        <asp:ListItem Value="4">CFO</asp:ListItem>
                                        <asp:ListItem Value="5">COO</asp:ListItem>
                                        <asp:ListItem Value="6">CEO</asp:ListItem>
                                        <asp:ListItem Value="8">PreApprover</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Requester Email:
                                </td>
                                <td>
                                    <asp:TextBox ID="reqEmail" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Requester UserID :
                                </td>
                                <td>
                                    <asp:TextBox ID="reqUserID" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="BtnUpdateRequester" runat="server" OnClick="BtnUpdateRequester_Click"
                                        Text="Update" /><asp:Button ID="btnDeleteReq" runat="server" OnClick="btnDeleteReq_Click"
                                            Text="Delete" /><asp:Button ID="BtnReqCancel" runat="server" Text="Cancel" OnClick="BtnReqCancel_Click" />
                                </td>
                                <td>
                                    &#160;&#160;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="BtnUpdateRequester" />
                        <asp:PostBackTrigger ControlID="btnDeleteReq" />
                        <asp:PostBackTrigger ControlID="BtnReqCancel" />
                    </Triggers>
                </asp:UpdatePanel>
                <table>
                    <tr>
                        <td>
                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalbackground"
                                DropShadow="True" DynamicServicePath="" Enabled="True" CancelControlID="BtnRequseterCancel"
                                PopupControlID="Panel2" TargetControlID="BtnInsertRequetor">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="Panel2" runat="server" CssClass="modal-body" BackColor="#DFE8F6" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2px" Width="550px">
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                Requester Name <span>*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtReqName" runat="server" Height="22px" Width="176px" OnTextChanged="txtReqName_TextChanged"
                                                    AutoPostBack="true"></asp:TextBox><asp:RequiredFieldValidator ID="ReqNameValidator"
                                                        runat="server" ErrorMessage="*" Font-Size="Smaller" ForeColor="#FF3300" ValidationGroup="RequesterValidation"
                                                        ControlToValidate="txtReqName"></asp:RequiredFieldValidator><asp:Label ID="lblValidation"
                                                            runat="server" Font-Size="Smaller" ForeColor="#FF3300" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Role :- <span class="style2">*
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpdwnRole" runat="server">
                                                    <asp:ListItem Value="0">------------Select-------------</asp:ListItem>
                                                    <asp:ListItem Value="1">PORequester</asp:ListItem>
                                                    <asp:ListItem Value="2">Supervisor</asp:ListItem>
                                                    <asp:ListItem Value="3">HOD</asp:ListItem>
                                                    <asp:ListItem Value="4">CFO</asp:ListItem>
                                                    <asp:ListItem Value="5">COO</asp:ListItem>
                                                    <asp:ListItem Value="6">CEO</asp:ListItem>
                                                    <asp:ListItem Value="8">PreApprover</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RoleValidator" runat="server" ErrorMessage="*" Font-Size="Smaller"
                                                    ForeColor="#FF3300" ValidationGroup="RequesterValidation" ControlToValidate="drpdwnRole"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Email ID: -
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEmailID" runat="server" Width="176px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                User ID : -
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUserID" runat="server" Width="176px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &#160;&#160;
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnReqInsert" ValidationGroup="RequesterValidation" runat="server"
                                                    Text="Save" OnClick="BtnReqInsert_Click" /><asp:Button ID="BtnRequseterCancel" runat="server"
                                                        Text="Cancel" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Project">
            <HeaderTemplate>
                Project</HeaderTemplate>
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="text-align: right">
                            &nbsp;&nbsp;
                        </td>
                        <td style="text-align: right">
                            Search Project : -
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtProjectSearch" runat="server"></asp:TextBox><asp:AutoCompleteExtender
                                ID="autoProject" runat="server" CompletionInterval="0" CompletionSetCount="1"
                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetProjectsName"
                                ServicePath="" TargetControlID="txtProjectSearch" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </asp:AutoCompleteExtender>
                            <asp:Button ID="btnSearchProject" runat="server" Text="Search" OnClick="btnSearchProject_Click" /><asp:Button
                                ID="btnInsertProject" runat="server" Text="Insert New" />
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="updProjects" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    ID # :
                                </td>
                                <td>
                                    <asp:Label ID="lblProjectID" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Project Name :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProjectName" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnDeleteProject" runat="server" Text="Delete" OnClick="btnDeleteProject_Click" /><asp:Button
                                        ID="btnCancelProject" runat="server" Text="Cancel" OnClick="btnCancelProject_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnDeleteProject" />
                        <asp:PostBackTrigger ControlID="btnCancelProject" />
                    </Triggers>
                </asp:UpdatePanel>
                <table>
                    <tr>
                        <td>
                            <asp:ModalPopupExtender ID="insertProject" runat="server" BackgroundCssClass="modalbackground"
                                DropShadow="True" DynamicServicePath="" Enabled="True" CancelControlID="btnProjectCancel"
                                PopupControlID="pnlProject" TargetControlID="btnInsertProject">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="pnlProject" runat="server" CssClass="modal-body" BackColor="#DFE8F6"
                                BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" Width="461px">
                                <div>
                                    <table class="style1" style="width: 459px">
                                        <tr>
                                            <td>
                                                Project <span class="style2">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewProject" runat="server" Height="22px" Width="176px"></asp:TextBox><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Font-Size="Smaller"
                                                    ForeColor="#FF3300" ValidationGroup="SubmitProject" ControlToValidate="txtNewProject"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnInsertNewProject" ValidationGroup="SubmitProject" runat="server"
                                                    Text="Save" OnClick="btnInsertNewProject_Click" /><asp:Button ID="btnProjectCancel"
                                                        runat="server" Text="Cancel" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Project">
            <HeaderTemplate>
                Supplier</HeaderTemplate>
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="text-align: right">
                            &nbsp;&nbsp;
                        </td>
                        <td style="text-align: right">
                            Search Supplier: -
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSupplierearch" runat="server"></asp:TextBox><asp:AutoCompleteExtender
                                ID="autoSupplier" runat="server" CompletionInterval="0" CompletionSetCount="1"
                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetSupplierName"
                                ServicePath="" TargetControlID="txtSupplierearch" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </asp:AutoCompleteExtender>
                            <asp:Button ID="btnSearchSupplier" runat="server" Text="Search" OnClick="btnSearchSupplier_Click" /><asp:Button
                                ID="btnInsertSupplier" runat="server" Text="Insert New" />
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="updSupplier" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    ID # :
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblSupplierID" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Supplier Name :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSuplierName" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Main Number :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtMainNo" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Fax Number :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFaxNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Contact Person :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtContactPerson" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Phone Number :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPhNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Contact Person 1 :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContactPerson1" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Phone Number 1 :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPhNo1" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address1 :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAdd1" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Address2 :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAdd2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address3 :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAdd3" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Address4 :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAdd4" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address5 :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAdd5" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Country :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Comment :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td>
                                    Term :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTerm" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnUpdateSupplier" runat="server" Text="Update" OnClick="btnUpdateSupplier_Click" /><asp:Button
                                        ID="btnDeleteSupplier" runat="server" Text="Delete" OnClick="btnDeleteSupplier_Click" /><asp:Button
                                            ID="btnCancelSupplier" runat="server" Text="Cancel" OnClick="btnCancelSupplier_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnUpdateSupplier" />
                        <asp:PostBackTrigger ControlID="btnDeleteSupplier" />
                        <asp:PostBackTrigger ControlID="btnCancelSupplier" />
                    </Triggers>
                </asp:UpdatePanel>
                <table>
                    <tr>
                        <td>
                            <asp:ModalPopupExtender ID="insertSupplier" runat="server" BackgroundCssClass="modalbackground"
                                DropShadow="True" DynamicServicePath="" Enabled="True" CancelControlID="btnSupplierCancel"
                                PopupControlID="pnlSupplier" TargetControlID="btnInsertSupplier">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="pnlSupplier" runat="server" CssClass="modal-body" BackColor="#DFE8F6"
                                BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" Width="461px">
                                <div>
                                    <table class="style1" style="width: 459px">
                                        <tr>
                                            <td>
                                                Supplier Name<span class="style2">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewSupplier" runat="server" Height="22px" Width="176px"></asp:TextBox><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Font-Size="Smaller"
                                                    ForeColor="#FF3300" ValidationGroup="SubmitSupplier" ControlToValidate="txtNewSupplier"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Main Number
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewMainNo" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Fax Number
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewFaxNo" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Contact Person
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewContactPerson" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Phone Number
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewPnNo" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Contact Person1
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewContactPerson1" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Phone Number1
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewPnNo1" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Address1
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewAdd1" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Address2
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewAdd2" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Address3
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewAdd3" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Address4
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewAdd4" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Address5
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewAdd5" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Country
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewCountry" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Comment
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Term
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewTerm" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnInsertNewSupplier" ValidationGroup="SubmitSupplier" runat="server"
                                                    Text="Save" OnClick="btnInsertNewSupplier_Click" />
                                                <asp:Button ID="btnSupplierCancel" runat="server" Text="Cancel" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="BudgetCode">
            <HeaderTemplate>
                Budget Code</HeaderTemplate>
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="text-align: right">
                            &nbsp;&nbsp;
                        </td>
                        <td style="text-align: right">
                            Search Budget Code: -
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSearchBudgetCode" runat="server"></asp:TextBox><asp:AutoCompleteExtender
                                ID="aceSearchBudgetCode" runat="server" CompletionInterval="0" CompletionSetCount="1"
                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetBudgetCode"
                                ServicePath="" TargetControlID="txtSearchBudgetCode" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </asp:AutoCompleteExtender>
                            <asp:Button ID="btnSearchBudgetCode" runat="server" Text="Search" OnClick="btnSearchBudgetCode_Click" />
                            <asp:Button ID="btnInsertBudgetCode" runat="server" Text="Insert New" />
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="divBudgCode" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    ID # :
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblBudgetCodeID" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Budget Code :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtBudgetCode" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Department :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDepartment" runat="server" DataValueField="DepartmentID"
                                        DataTextField="DepartmentName">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Order Classification :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlOrderClassification" runat="server" DataValueField="OrderClassificationID"
                                        DataTextField="OrderClassificationName">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Station :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlStation" runat="server" DataValueField="StationID" DataTextField="StationName">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Description :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnUpdateBudgetCode" runat="server" Text="Update" OnClick="btnUpdateBudgetCode_Click" />
                                    <asp:Button ID="btnDeleteBudgetCode" runat="server" Text="Delete" OnClick="btnDeleteBudgetCode_Click" />
                                    <asp:Button ID="btnCancelBudgetCode" runat="server" Text="Cancel" OnClick="btnCancelBudgetCode_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnUpdateBudgetCode" />
                        <asp:PostBackTrigger ControlID="btnDeleteBudgetCode" />
                        <asp:PostBackTrigger ControlID="btnCancelBudgetCode" />
                    </Triggers>
                </asp:UpdatePanel>
                <table>
                    <tr>
                        <td>
                            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalbackground"
                                DropShadow="True" DynamicServicePath="" Enabled="True" CancelControlID="btnBudgetCodeCancel"
                                PopupControlID="pnlBudgetCode" TargetControlID="btnInsertBudgetCode">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="pnlBudgetCode" runat="server" CssClass="modal-body" BackColor="#DFE8F6"
                                BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" Width="461px">
                                <div>
                                    <table class="style1" style="width: 459px">
                                        <tr>
                                            <td>
                                                Department<span class="style2">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlInsertDepartment" runat="server" Height="22px" Width="176px"
                                                    DataValueField="DepartmentID" DataTextField="DepartmentName">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvInsertDept" runat="server" ErrorMessage="*" Font-Size="Smaller"
                                                    ForeColor="#FF3300" ValidationGroup="SubmitBudgetCode" ControlToValidate="ddlInsertDepartment"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Order Classification<span class="style2">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlInsertOrderClassification" runat="server" DataValueField="OrderClassificationID"
                                                    DataTextField="OrderClassificationName">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvInsertOrderClassification" runat="server" ErrorMessage="*"
                                                    Font-Size="Smaller" ForeColor="#FF3300" ValidationGroup="SubmitBudgetCode" ControlToValidate="ddlInsertOrderClassification"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Station
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlInsertStation" runat="server" DataValueField="StationID"
                                                    DataTextField="StationName">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Budget Code<span class="style2">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInsertBudgetCode" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                                    ID="rfvInsertBudgetCode" runat="server" ErrorMessage="*" Font-Size="Smaller"
                                                    ForeColor="#FF3300" ValidationGroup="SubmitBudgetCode" ControlToValidate="txtInsertBudgetCode"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Description
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInsertDescription" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Year
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlInsertYear" runat="server">
                                                    <asp:ListItem Selected="True" Value="2015">2015</asp:ListItem>
                                                    <asp:ListItem Value="2016">2016</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnInsertNewBudgetCode" ValidationGroup="SubmitBudgetCode" runat="server"
                                                    Text="Save" OnClick="btnInsertNewBudgetCode_Click" />
                                                <asp:Button ID="btnBudgetCodeCancel" runat="server" Text="Cancel" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    <asp:Label ID="lblMessageError" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblUpdateMessage" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCreatedBy" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblModifiedBy" runat="server" Visible="False"></asp:Label>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
