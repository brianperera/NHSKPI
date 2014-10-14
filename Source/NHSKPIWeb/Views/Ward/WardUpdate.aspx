<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="WardUpdate.aspx.cs" Inherits="Views_Ward_WardUpdate" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            Ward Details
        </h1>
    </div>
    <div class="grid_12">
        <div class="grid_24 error_msg">
            <asp:Label ID="lblAddMessage" runat="server" Text=""></asp:Label>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblWardCode" runat="server" Text="Ward Code :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtWardCode" runat="server" MaxLength="20"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvWardCode" runat="server" ErrorMessage=" Ward Code is required"
                ControlToValidate="txtWardCode" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblWardName" runat="server" Text="Ward Name :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtWardName" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvWardName" runat="server" ErrorMessage="Ward Name is required"
                ControlToValidate="txtWardName" Display="Dynamic"></asp:RequiredFieldValidator>
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
                ControlToValidate="ddlHospitalName" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblWardGroupName" runat="server" Text="Ward Group Name :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:DropDownList ID="ddlWardGroupName" runat="server">
            </asp:DropDownList>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvWardGroupName" runat="server" ErrorMessage="Ward Group is required"
                ControlToValidate="ddlWardGroupName" Display="Dynamic"></asp:RequiredFieldValidator>
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
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Visible="False" />
            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                Visible="False" />
        </div>
    </div>
</asp:Content>
