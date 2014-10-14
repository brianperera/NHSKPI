<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="KPIGroupUpdate.aspx.cs" Inherits="Views_KPI_KPIGroupUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            KPI Group Detail
        </h1>
    </div>
    <div class="grid_12">
        <div class="grid_24 error_msg">
            <asp:Label ID="lblAddMessage" runat="server" Text=""></asp:Label>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblKPIGroupName" runat="server" Text="KPI Group Name :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtKPIGroupName" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvKPIGroupName" runat="server" ErrorMessage="KPI Group Name is required"
                ControlToValidate="txtKPIGroupName" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblIsActive" runat="server" Text="Is Active :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" />
        </div>
        <div class="grid_16 prefix_8">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="False" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                Visible="False" />
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
