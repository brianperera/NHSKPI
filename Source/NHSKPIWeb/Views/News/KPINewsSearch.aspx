<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="KPINewsSearch.aspx.cs" Inherits="Views_News_KPINewsSearch" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            KPI News
        </h1>
    </div>
    <div class="grid_24">
        <div class="Hgap10">
        </div>
        <asp:GridView CssClass="grid" ID="gvSearchResult" runat="server" AutoGenerateColumns="False">
            <EmptyDataTemplate>
                <div class="center bold error grid_empty">
                    No News Found.</div>
            </EmptyDataTemplate>
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id,NewsType" DataTextField="Title" HeaderText="Title"
                    DataNavigateUrlFormatString="KPINewsUpdate.aspx?Id={0}&Type={1}" />
                <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" />
                <asp:BoundField DataField="IsActive" HeaderText="IsActive" />
            </Columns>
        </asp:GridView>
        <div class="Hgap10">
        </div>
    </div>
</asp:Content>
