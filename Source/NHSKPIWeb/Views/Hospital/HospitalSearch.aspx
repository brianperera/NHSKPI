<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="HospitalSearch.aspx.cs" Inherits="Views_Hospital_HospitalSearch" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            Hospital Search
        </h1>
    </div>
    <div class="grid_12">
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblHospitalName" runat="server" Text="Hospital Name :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtHospitalName" runat="server" MaxLength="250"></asp:TextBox>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblHospitalCode" runat="server" Text="Hospital Code :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtHospitalCode" runat="server" MaxLength="10"></asp:TextBox>
            <div class="clear">
            </div>
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
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </div>
        <div class="clear">
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
                        Hospital(s) Search Result</h3>
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
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataTextField="Name" HeaderText="Name"
                    DataNavigateUrlFormatString="HospitalUpdate.aspx?Id={0}" />
                <asp:BoundField DataField="Code" HeaderText="Code" />
                <asp:BoundField DataField="Address" HeaderText="Address" />
                <asp:BoundField DataField="PhoneNumber" HeaderText="Phone" />
            </Columns>
        </asp:GridView>
        <div class="Hgap10">
        </div>
    </div>
</asp:Content>
