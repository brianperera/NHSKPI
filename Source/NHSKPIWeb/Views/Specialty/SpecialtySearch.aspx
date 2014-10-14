<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="SpecialtySearch.aspx.cs" Inherits="Views_Specialty_SpecialtySearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            Specialty Search
        </h1>
    </div>
    <div class="grid_12">
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblSpecialty" runat="server" Text="Local Specialty :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtSpecialty" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="Label1" runat="server" Text="Is Active :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" />
        </div>
        <div class="clear">
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
                        Local Specialty(s) Search Result</h3>
                </div>
                <div class="gridright">
                </div>
            </div>
        </div>
        <asp:GridView CssClass="grid" ID="gvSearchResult" runat="server" AutoGenerateColumns="False">
            <EmptyDataTemplate>
                <div class="center bold error grid_empty">
                    No Results found.</div>
            </EmptyDataTemplate>
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataTextField="Specialty" HeaderText="Specialty"
                    DataNavigateUrlFormatString="SpecialtyUpdate.aspx?Id={0}" />
            </Columns>
        </asp:GridView>
        <div class="Hgap10">
        </div>
    </div>
</asp:Content>
