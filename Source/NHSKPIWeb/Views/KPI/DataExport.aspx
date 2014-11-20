<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="DataExport.aspx.cs" Inherits="Views_KPI_DataExport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
   
    <asp:ListBox ID="ListBox1" Width="200px" runat="server"></asp:ListBox>
   
&nbsp;
    <asp:Button ID="Button1" runat="server" Text=">" />
    &nbsp;<asp:Button ID="Button2" runat="server" Text="<" />
&nbsp;<asp:ListBox ID="ListBox2" Width="200px" runat="server"></asp:ListBox>
   
    <br />
    <asp:Button ID="Button3" runat="server" Text="Export Ward Data" />
&nbsp;<asp:Button ID="Button4" runat="server" Text="Export Specialty Data" />
   
</asp:Content>
