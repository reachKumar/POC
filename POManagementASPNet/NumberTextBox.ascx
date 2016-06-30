<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NumberTextBox.ascx.cs" Inherits="POManagementASPNet.NumberTextBox"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:TextBox ID="txtSpaceNumber" runat="server" SkinID="Skin1" Width="210px"></asp:TextBox>
<asp:FilteredTextBoxExtender ID="FilterSpaceNumber" runat="server" FilterType="Numbers"
    TargetControlID="txtSpaceNumber">
</asp:FilteredTextBoxExtender>
<asp:TextBoxWatermarkExtender ID="tweNumbers" TargetControlID="txtSpaceNumber"
    runat="server" WatermarkText="Numbers only">
</asp:TextBoxWatermarkExtender>
<asp:RangeValidator ID="rvSpaceNumber" runat="server" ErrorMessage="You can not provide more than demand"
    Type="Integer" MinimumValue="0" ControlToValidate="txtSpaceNumber" Display="None"
    SetFocusOnError="True"></asp:RangeValidator>
<asp:ValidatorCalloutExtender ID="vcRangeSpaceNumber" runat="server" TargetControlID="rvSpaceNumber">
</asp:ValidatorCalloutExtender>
<asp:RequiredFieldValidator ID="rfvSpaceNumber" runat="server" ErrorMessage="Please select SpaceNumber"
    Display="None" ControlToValidate="txtSpaceNumber" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:ValidatorCalloutExtender ID="vcReqSpaceNumber" runat="server" TargetControlID="rfvSpaceNumber">
</asp:ValidatorCalloutExtender>
