<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AutoSelectionDropDown.ascx.cs" Inherits="Views_Shared_AutoSelectionDropDown" %>
<asp:DropDownList ID="ddlHospitalName" placeholder="Company Name" runat="server" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Text="Select Hospital" Value ="-1"></asp:ListItem>
</asp:DropDownList>
