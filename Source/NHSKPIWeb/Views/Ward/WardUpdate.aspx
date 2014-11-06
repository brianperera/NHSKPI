<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="WardUpdate.aspx.cs" Inherits="Views_Ward_WardUpdate" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<%@ Register src="../Shared/WardBulkUploader.ascx" tagname="WardBulkUploader" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <script src="../../Scripts/GeneralUtility.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="page-header position-relative">
        <h1>
            Ward Details
        </h1>
    </div>

    <asp:UpdatePanel runat="server" ID="wardDataEntryUpdatePanel">
        <ContentTemplate>
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
            <asp:RequiredFieldValidator validationgroup="singleUpload" ID="rfvWardCode" runat="server" ErrorMessage=" Ward Code is required"
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
            <asp:RequiredFieldValidator ID="rfvWardName" validationgroup="singleUpload"  runat="server" ErrorMessage="Ward Name is required"
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
            <asp:RequiredFieldValidator ID="rfvHospitalName" validationgroup="singleUpload"  runat="server" ErrorMessage="Hospital Name is required"
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
            <asp:RequiredFieldValidator ID="rfvWardGroupName" validationgroup="singleUpload"  runat="server" ErrorMessage="Ward Group is required"
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
        <div class="grid_8">
            <asp:Label ID="Label2" runat="server" Text="Bulk Upload Ward Data :"></asp:Label>
        </div>
        <div class="grid_16">
            <input id="ChbWardBulkUpload" type="checkbox" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_16 prefix_8">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Visible="False" />
            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                Visible="False" />
        </div>
    </div>            
        </ContentTemplate>
    </asp:UpdatePanel>

      <div class="grid_12 ward-bulk-upload">
        <div class="grid_8">
            <asp:Label ID="Label1" runat="server" Text="Bulk Upload Ward Data :"></asp:Label>
        </div>
        <div class="grid_16">
            <uc1:WardBulkUploader ID="WardBulkUploader1" validationgroup="bulkUpload"  runat="server" />
        </div>
    </div>

</asp:Content>
