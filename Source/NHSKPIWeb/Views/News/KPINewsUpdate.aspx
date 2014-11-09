﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master" Title="NHS KPI Data Entry Portal"
    AutoEventWireup="true" CodeFile="KPINewsUpdate.aspx.cs" Inherits="Views_News_KPINewsUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            KPI News Detail
        </h1>
    </div>
    <div class="grid_12">
        <div class="grid_24 error_msg">
            <asp:Label ID="lblAddMessage" runat="server" Text=""></asp:Label>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="LblHospital" runat="server" Text="Hospital :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:DropDownList ID="ddlHospital" runat="server"/>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblKPINewsTitle" runat="server" Text="KPI News Title :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtKPINewsTitle" TextMode="multiline" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvKPIGroupName" runat="server" ErrorMessage="KPI News Title is required"
                ControlToValidate="txtKPINewsTitle" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblIsActive" runat="server" Text="Description :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox id="txtDescription" TextMode="multiline" runat="server" />
        </div>
        <div class="grid_16 prefix_8">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="False" />
            <%--<asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                Visible="False" />--%>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>