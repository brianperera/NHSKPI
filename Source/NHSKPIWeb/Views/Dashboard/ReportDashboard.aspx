<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="ReportDashboard.aspx.cs" Inherits="Views_Dashboard_ReportDashboard" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
                <h1>KPI Report Dashboard
                </h1>
            </div>
            <div class="grid_12">
                <div class="grid_8">
                    <asp:Label ID="LblNewsType" runat="server" Text="TODO"></asp:Label>
                </div>
                <div class="grid_16">
                    TODO
                    <div class="clear">
                    </div>
                </div>
                <div class="clear">
                </div>


                <div class="grid_16 prefix_8">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"/>
                </div>
                <div class="clear">
                </div>
            </div>
</asp:Content>
