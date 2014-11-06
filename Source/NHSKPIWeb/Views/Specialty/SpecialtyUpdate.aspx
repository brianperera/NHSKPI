<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="SpecialtyUpdate.aspx.cs" Inherits="Views_Specialty_Specialty" %>
     <%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<%@ Register src="../Shared/SpecialtyBulkUploader.ascx" tagname="SpecialtyBulkUploader" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
        <script src="../../Scripts/GeneralUtility.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="page-header position-relative">
        <h1>
            Specialty Detail
        </h1>
    </div>
<asp:UpdatePanel runat="server" ID="wardDataEntryUpdatePanel">
        <ContentTemplate>
    <div class="grid_24 error_msg">
        <asp:Label ID="lblAddMessage" runat="server" Text=""></asp:Label>
    </div>
    <div class="grid_12">
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="Label5" runat="server" Text="Specialty Group :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:DropDownList ID="ddlSpecialtyGroup" runat="server">
            </asp:DropDownList>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" validationgroup="singleUpload" runat="server" ControlToValidate="ddlSpecialtyGroup"
                ErrorMessage="Specialty Group is required" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
        </div>
        <div class="grid_8">
            <asp:Label ID="Label2" runat="server" Text="Local Specialty Code :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtSpecialtyCode" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" validationgroup="singleUpload" runat="server" ControlToValidate="txtSpecialtyCode"
                ErrorMessage="Local Specialty Code is required" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="grid_8">
            <asp:Label ID="lblSpecialty" runat="server" Text="Local Specialty :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtSpecialty" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvSpecialty" validationgroup="singleUpload" runat="server" ControlToValidate="txtSpecialty"
                ErrorMessage="Local Specialty is required" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="grid_8">
            <asp:Label ID="Label4" runat="server" Text="National Specialty Code :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtNationalSpecialtyCode" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" validationgroup="singleUpload" runat="server" ControlToValidate="txtNationalSpecialtyCode"
                ErrorMessage="National Specialty Code is required" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="grid_8">
            <asp:Label ID="Label3" runat="server" Text="National Specialty :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtNationalSpecialty" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" validationgroup="singleUpload" runat="server" ControlToValidate="txtNationalSpecialty"
                ErrorMessage="National Specialty is required" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="Label1" runat="server" Text="Is Active : "></asp:Label>
        </div>
        <div class="grid_16">
            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="Label7" runat="server" Text="Bulk Upload Specialty Data :"></asp:Label>
        </div>
        <div class="grid_16">
            <input id="ChbWardBulkUpload" type="checkbox" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_16 prefix_8">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="False" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                Visible="False" />
        </div>
    </div>
     </ContentTemplate>
    </asp:UpdatePanel>

      <div class="grid_12 ward-bulk-upload">
        <div class="grid_8">
            <asp:Label ID="Label6" runat="server" Text="Bulk Upload Specialty Data :"></asp:Label>
        </div>
        <div class="grid_16">
            <uc1:SpecialtyBulkUploader ID="SpecialtyBulkUploader1" runat="server" />
        </div>
    </div>
</asp:Content>
