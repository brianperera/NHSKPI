<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="Configuration.aspx.cs" Inherits="Views_SystemAdministration_Configuration" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            Configuration
        </h1>
    </div>
    <div class="grid_24 error_msg">
        <asp:Label ID="lblAddMessage" runat="server" Text=""></asp:Label>
    </div>
    <div class="grid_12 fullWidth">
        <div class="clear">
        </div>
        <div class="grid_3">
            <asp:Label ID="lblModule" runat="server" Text="Module :"></asp:Label>
        </div>
        <div class="grid_16 rdo_space fullWidth moduleConfiguration">
            <asp:RadioButtonList ID="rdoModule" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="1">Ward Only</asp:ListItem>
                <asp:ListItem Value="2">Specialty Only</asp:ListItem>
                <asp:ListItem Value="3">Ward And Specialty</asp:ListItem>
            </asp:RadioButtonList>
            <asp:CheckBoxList ID="otherModules" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="1">Email Facilities</asp:ListItem>
                <asp:ListItem Value="2">Reminders</asp:ListItem>
                <asp:ListItem Value="3">Download Data Sets</asp:ListItem>
                <asp:ListItem Value="3">Bench Marking Module</asp:ListItem>
            </asp:CheckBoxList>
            <div class="clear">
            </div>
        </div>
        <div class="clear"></div>
        <div class="clear">
        </div>
        <div class="clear">
        </div>
        <div class="grid_16 prefix_3">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
        </div>
    </div>
</asp:Content>
