<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AutoSelectionDropDown.ascx.cs" Inherits="Views_Shared_AutoSelectionDropDown" %>
<script>
    
</script>
<div class="AutocompleteControl">
    <asp:TextBox ID="txbAutoCompleteTextbox" placeholder="Hospital" runat="server"></asp:TextBox>
    <asp:DropDownList ID="ddlHospitalName" placeholder="Company Name" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlHospitalName_SelectedIndexChanged">
    </asp:DropDownList>
</div>
<div style="visibility: hidden" id="AutoCompleteValues" runat="server"></div>
