<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master" AutoEventWireup="true" CodeFile="DashboardSpecialty.aspx.cs" Inherits="Views_Dashboard_DashboardSpecialty" %>
<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script>
     $(function () {
         $("#tabs").tabs();
     });

     function DashboardLoad(i) {
         if (i == 1) {
             window.location = "Dashboard.aspx";
         }
         else if (i == 2) {
             window.location = "DashboardSpecialty.aspx";
         }
     }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" Runat="Server">


<div id="tabs">
<ul>
<li id="liWard" runat="server" visible= "false" onclick="DashboardLoad(1)"><a href="#tabs-1">Ward Level</a></li>
<li id="liSpecialty" runat="server" visible= "false" onclick="DashboardLoad(2)" class="ui-tabs-active"><a href="#tabs-2">Specialty Level</a></li>
</ul>
<div id="tabs-2">

<div class="grid_24">
        <div class="page-header">
            <h1>
                Data Entry Summary by Specialty
            </h1>
        </div>
    </div>
    <div class="clear Hgap30 ">
    </div>
    <div class="grid_sub_24">
    <div class="grid_3 " ><label >Specialty Group:</label></div>
    <div class="grid_4 "><asp:DropDownList runat="server" ID="ddlWardGroup"></asp:DropDownList></div>
    <div class="grid_2 ">
        <asp:Button ID="btnSearch" runat="server" Text="Search" 
            onclick="btnSearch_Click" /></div>
        
    </div>
    <div class="clear Hgap10 ">
    </div>
    <div id="divDashBoard" runat="server" class="data_summery ">
    </div>

       </div>
    <div class="clear Hgap10 ">
    </div>
</div
</asp:Content>

