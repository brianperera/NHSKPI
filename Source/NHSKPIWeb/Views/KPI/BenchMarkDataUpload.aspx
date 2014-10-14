<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="BenchMarkDataUpload.aspx.cs" Inherits="Views_KPI_BenchMarkDataUpload" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            Benchmark CSV Data Upload
        </h1>
    </div>
    
    <div class="grid_24 error_msg">
        <asp:Label ID="lblAddMessage" runat="server" Text=""></asp:Label>
    </div>
    <div class="grid_12">
        <div class="clear">
        </div>
        <div class="grid_3">
            <asp:Label ID="lblFile" runat="server" Text="File :"></asp:Label>
        </div>
        <div class="grid_16 rdo_space">
            <asp:FileUpload ID="fuFile" runat="server" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_3">
        </div>
        <div class="clear">
        </div>
        <div class="grid_16">
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="fuFile"
                runat="server" ErrorMessage="Only CSV file is allowed" ValidationExpression="(.*?)\.(csv|CSV)$"></asp:RegularExpressionValidator>
        </div>
        <div class="clear">
        </div>
        <div class="clear">
        </div>
        <div class="grid_16 prefix_3">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
        </div>
    </div>
</asp:Content>
