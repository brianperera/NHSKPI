<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="SpecialtyUpdate.aspx.cs" Inherits="Views_Specialty_Specialty" %>
     <%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            Specialty Detail
        </h1>
    </div>
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
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSpecialtyGroup"
                ErrorMessage="Specialty Group is required" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
        </div>
        <div class="grid_8">
            <asp:Label ID="Label2" runat="server" Text="Local Specialty Code :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtSpecialtyCode" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSpecialtyCode"
                ErrorMessage="Local Specialty Code is required" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="grid_8">
            <asp:Label ID="lblSpecialty" runat="server" Text="Local Specialty :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtSpecialty" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvSpecialty" runat="server" ControlToValidate="txtSpecialty"
                ErrorMessage="Local Specialty is required" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="grid_8">
            <asp:Label ID="Label4" runat="server" Text="National Specialty Code :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtNationalSpecialtyCode" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNationalSpecialtyCode"
                ErrorMessage="National Specialty Code is required" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="grid_8">
            <asp:Label ID="Label3" runat="server" Text="National Specialty :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtNationalSpecialty" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNationalSpecialty"
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
        <div class="grid_16 prefix_8">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="False" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                Visible="False" />
        </div>
    </div>
</asp:Content>
