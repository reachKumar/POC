<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Search.ascx.cs" Inherits="POManagementASPNet.Search" %>
<%@ Register TagPrefix="UC" TagName="NumberTextBox" Src="~/NumberTextBox.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script language="javascript" type="text/javascript">

var input = document.getElementById('<%=txtSearchPO.ClientID %>');
input.onkeydown = function(e) {
//debugger;
    var k = e.which;
    /* numeric inputs can come from the keypad or the numeric row at the top */
    if ( (k < 48 || k > 57) && (k < 96 || k > 105)) {
        e.preventDefault();
        return false;
    }
};​

 
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
        
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            alert('Please enter only numeric value');
            //document.getElementById("error").style.display = ret ? "none" : "inline";
            //return ret;
        }
   
</script>
<table>
    <tr id="trSearchPO" runat="server">
        <td>
            <div style="float: right; vertical-align: top">
                <%-- <UC:NumberTextBox ID="txtSearchPO1" runat="server" Visible="false" />--%>
                <asp:TextBox ID="txtSearchPO" runat="server" ToolTip="Enter a PO Number to Search"></asp:TextBox>
                <asp:FilteredTextBoxExtender ID="ftxteSearch" runat="server" TargetControlID="txtSearchPO"
                    InvalidChars=",#$@%^!*'':;/=()&<>?|{}[]_`~abcdefghijklmnopqrstuvwxyz " FilterMode="InvalidChars"
                    FilterType="Custom">
                </asp:FilteredTextBoxExtender>
                <asp:Button ID="btnSearchPO" OnClick="btnSearchPO_Click" runat="server" Text="Search PO"
                    CausesValidation="False" ToolTip="Enter a PO Number to Search" />
                <br />
                <asp:LinkButton ID="lnkAdvanceSearch" runat="server" Font-Bold="false" Font-Size="9pt"
                    CausesValidation="false" OnClick="lnkAdvanceSearch_Click">Advance Search</asp:LinkButton>
                <asp:Label ID="lblSearchMessage" runat="server" Font-Bold="false" ForeColor="Red"></asp:Label>
            </div>
        </td>
    </tr>
</table>
