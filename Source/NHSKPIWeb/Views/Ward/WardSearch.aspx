<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="WardSearch.aspx.cs" Inherits="Views_Ward_WardSearch" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            Ward Search
        </h1>
    </div>
    <div class="grid_12">
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblWardCode" runat="server" Text="Ward Code :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtWardCode" runat="server" MaxLength="20"></asp:TextBox>
            <div class="clear">
            </div>
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
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
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
                        Ward(s) Search Result</h3>
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
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataTextField="WardName" HeaderText="Name"
                    DataNavigateUrlFormatString="WardUpdate.aspx?Id={0}" />
                <asp:BoundField DataField="WardCode" HeaderText="Code" />
                
                <asp:BoundField DataField="WardGroupName" HeaderText="Ward Group" />
            </Columns>
             <AlternatingRowStyle CssClass="altrow" />
        </asp:GridView>
        <div class="Hgap10">
        </div>
    </div>
</asp:Content>
