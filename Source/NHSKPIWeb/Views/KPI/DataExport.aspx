<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="DataExport.aspx.cs" Inherits="Views_KPI_DataExport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
   
&nbsp;
    &nbsp;&nbsp;<br />
    <asp:DropDownList ID="DataType_DropDownList" runat="server" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="DataType_DropDownList_SelectedIndexChanged" Width="246px">
        <asp:ListItem>Ward Data</asp:ListItem>
        <asp:ListItem>Specility Data</asp:ListItem>
    </asp:DropDownList>
    <br />
   
    <asp:CheckBox ID="CheckBox_SelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_SelectAll_CheckedChanged" Text="Select All" Font-Bold="True" Font-Size="Small" />
   
    <br />
    <asp:CheckBoxList ID="ColumnList_CheckBoxList" runat="server" RepeatColumns="4" AutoPostBack="True" OnSelectedIndexChanged="ColumnList_CheckBoxList_CheckedChanged">
    </asp:CheckBoxList>
   
    <br />
    <asp:Button ID="Export_Data_Button" runat="server" Text="Export  Data" OnClick="Export_Data_Button_Click" />
&nbsp;<br />
    <asp:Label ID="Message_Label" runat="server" ForeColor="#CC0000" Height="19px" Text="Label" Width="164px"></asp:Label>
   
    <br />
    <br />
    <br />
   
</asp:Content>
