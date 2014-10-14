<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="WardGroupUpdate.aspx.cs" Inherits="Views_Ward_WardGroupUpdate" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            Ward Group Detail
        </h1>
    </div>
    <div class="grid_12">
        <div class="grid_24 error_msg">
            <asp:Label ID="lblAddMessage" runat="server"></asp:Label>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblWardGroupName" runat="server" Text="Ward Group Name :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtWardGroupName" runat="server" MaxLength="50"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvWardGroupName" runat="server" ErrorMessage="Ward Group is required"
                ControlToValidate="txtWardGroupName" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8 hidden">
            <asp:Label ID="lblHospitalName" runat="server" Text="Hospital Name :"></asp:Label>
        </div>
        <div class="grid_16 hidden">
            <asp:DropDownList ID="ddlHospitalName" runat="server">
            </asp:DropDownList>
             <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvHospitalName" runat="server" ErrorMessage="Hospital Name is required"
                ControlToValidate="ddlHospitalName" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblDescription" runat="server" Text="Description :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
             <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="Description is required"
                ControlToValidate="txtDescription" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblIsActive" runat="server" Text="Is Active :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_16 prefix_8">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="False" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                Visible="False" />
        </div>
    </div>
</asp:Content>
