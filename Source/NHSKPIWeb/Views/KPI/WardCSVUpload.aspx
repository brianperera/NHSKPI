<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="WardCSVUpload.aspx.cs" Inherits="Views_KPI_WardCSVUpload" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            CSV Upload
        </h1>
    </div>
    <div class="grid_24 error_msg info_csv">
        <span class="alert-info">
            <div class="grid_3">
                <asp:Label ID="Label1" runat="server" Text="Ward Code"></asp:Label>
            </div>
            <div class="grid_20">
                <asp:Label ID="Label2" runat="server" Text="Valid ward code should be entered. Invalid ward codes will fail the upload process."></asp:Label>
            </div>
            <div class="clear Hgap5">
            </div>
            <div class="grid_3">
                <asp:Label ID="Label3" runat="server" Text="KPI No"></asp:Label>
            </div>
            <div class="grid_20">
                <asp:Label ID="Label4" runat="server" Text="Valid KPI number should be entered. Invalid KPI numbers will fail the upload process."></asp:Label>
            </div>
            <div class="clear Hgap5">
            </div>
            <div class="grid_3">
                <asp:Label ID="Label5" runat="server" Text="Target Month"></asp:Label>
            </div>
            <div class="grid_20">
                <asp:Label ID="Label6" runat="server" Text="Format should be dd/MM/yyyy. eg: 01/04/2014. Any other formats will protantially fail or upload invalid date into the database."></asp:Label>
            </div>
            <div class="clear Hgap5">
            </div>
            <div class="grid_3">
                <asp:Label ID="Label7" runat="server" Text="Numerator"></asp:Label>
            </div>
            <div class="grid_20">
                <asp:Label ID="Label8" runat="server" Text="Must enter a value into the numerator field. Should not leave as a blank field."></asp:Label>
            </div>
            <div class="clear Hgap5">
            </div>
            <div class="grid_3">
                <asp:Label ID="Label9" runat="server" Text="Denominator"></asp:Label>
            </div>
            <div class="grid_20">
                <asp:Label ID="Label10" runat="server" Text="Can leave blank."></asp:Label>
            </div>
            <div class="clear Hgap5">
            </div>
            <div class="grid_3">
                <asp:Label ID="Label11" runat="server" Text="YTD Value"></asp:Label>
            </div>
            <div class="grid_20">
                <asp:Label ID="Label12" runat="server" Text="Can leave blank."></asp:Label>
            </div>
            <div class="grid_3">
                <asp:Label ID="Label13" runat="server" Text="CSV Template"></asp:Label>
            </div>
            <div class="grid_20">
                <asp:HyperLink ID="HyperLink1" NavigateUrl="~/assets/doc/Ward.csv" runat="server"  Target="_blank">Click here to download</asp:HyperLink>
            </div>
            <div class="clear">
            </div>
        </span>
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
