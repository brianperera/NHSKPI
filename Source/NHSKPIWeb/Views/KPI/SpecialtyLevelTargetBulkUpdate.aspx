﻿<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="SpecialtyLevelTargetBulkUpdate.aspx.cs" Inherits="Views_KPI_SpecialtyLevelTargetBulkUpdate" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            Specialty Level Bulk Target/Data Search
        </h1>
    </div>
    <div class="grid_12">
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblKPI" runat="server" Text="KPI Description :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:DropDownList ID="ddlKPI" runat="server">
            </asp:DropDownList>
        </div>
        <div class="clear">
        </div>
        <div class="clear Hgap10">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblFinancialYear" runat="server" Text="Financial Year :"></asp:Label>
        </div>
        <div class="grid_16">
            <div class="grid_2">
                <asp:ImageButton ID="imgBtnPrevoius" runat="server" OnClick="imgBtnPrevoius_Click"
                    ImageUrl="~/assets/images/l_arrow.png" /></div>
            <div class="grid_6 center">
                <h3>
                    <asp:Label ID="lblCurentFinancialYear" runat="server"></asp:Label></h3>
            </div>
            <div class="grid_2 right">
                <asp:ImageButton ID="imgBtnNext" runat="server" OnClick="imgBtnNext_Click" ImageUrl="~/assets/images/r_arrow.png" /></div>
        </div>
        <div class="clear Hgap10">
        </div>
        <div class="grid_16 prefix_8">
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </div>
    </div>
    <div class="grid_24">
        <div class="Hgap10">
        </div>
        <div class="gridtable">
            <div class="gridwrap">
                <div class="gridleft">
                </div>
                <div class="gridtitle">
                    <h3>
                        Specialty Level Bulk Target/Data Search Result</h3>
                </div>
                <div class="gridright">
                </div>
            </div>
        </div>
        <asp:GridView CssClass="grid" ID="gvSearchResult" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSearchResult_RowDataBound" DataKeyNames="ManuallyEntered">
            <EmptyDataTemplate>
                <div class="center bold error grid_empty">
                    No Results found.</div>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Hospital Name" Visible="false" />
                <asp:BoundField DataField="KPIDescription" HeaderText="KPI" />
                <asp:BoundField DataField="FinancialYear" HeaderText="Financial Year" />
                <asp:HyperLinkField DataNavigateUrlFields="HospitalId,KPIId,FinancialYear" DataNavigateUrlFormatString="SpecialtyLevelTargetBulk.aspx?HospitalId={0}&amp;KpiId={1}&amp;FinancailYear={2}"
                    HeaderText="Target" Text="Target" />
                <asp:HyperLinkField DataNavigateUrlFields="HospitalId,KPIId,FinancialYear" DataNavigateUrlFormatString="SpecialtyLevelDataBulk.aspx?HospitalId={0}&amp;KpiId={1}&amp;FinancailYear={2}"
                    HeaderText="Data" Text="Data" />
            </Columns>
            <AlternatingRowStyle CssClass="altrow" />
        </asp:GridView>
        <div class="Hgap10">
        </div>
    </div>
</asp:Content>
