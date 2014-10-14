<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="error_404.aspx.cs" Inherits="Views_Shared_error_404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="well error404">
        <h1 class="grey lighter smaller">
            <span class="red bigger-125"><i class="icon-remove-sign"></i></span> <span class="red"> 404 </span>Page Not Found
        </h1>
        <hr/>
        <h3 class="lighter smaller">
            We looked everywhere but we couldn't find it!</h3>
        <div>           
            <div class="space">
            </div>
            <h4 class="smaller">
                Try one of the following:</h4>
            <ul class="unstyled spaced inline bigger-110">
                <li><i class="icon-hand-right blue"></i>Re-check the url for typos </li>
                <li><i class="icon-hand-right blue"></i>Read the faq </li>
                <li><i class="icon-hand-right blue"></i>Tell us about it </li>
            </ul>
        </div>
        <hr/>
        
        <div class="space">
        </div>
        <div class="row-fluid">
            <asp:Button ID="Button1" runat="server" Text="Back" />
            <asp:Button ID="Button2" runat="server" Text="Dashboard" />
                
           
        </div>
    </div>
</asp:Content>
