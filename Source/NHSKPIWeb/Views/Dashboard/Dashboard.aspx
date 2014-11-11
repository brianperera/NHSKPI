<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Views_Dashboard_Dashboard" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server" Visible="false">
    <div id="tabs">
        <ul>
            <li id="liWard" runat="server" visible="false" onclick="DashboardLoad(1)"><a href="#tabs-1">
                Ward Level</a></li>
            <li id="liSpecialty" runat="server" visible="false" onclick="DashboardLoad(2)"><a
                href="#tabs-2">Specialty Level</a></li>
        </ul>
        <div id="tabs-1">
            <div class="grid_24">
                <div class="page-header">
                    <h1>
                        Data Entry Summary by Ward
                    </h1>
                </div>
            </div>
            <div class="clear Hgap30 ">
            </div>
            <div class="grid_sub_24">
                <div class="grid_3 ">
                    <label>
                        Ward Group:</label></div>
                <div class="grid_4 ">
                    <asp:DropDownList runat="server" ID="ddlWardGroup">
                    </asp:DropDownList>
                </div>
                <div class="grid_2 ">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /></div>
            </div>
            <div class="clear Hgap10 ">
            </div>
            <div id="divDashBoard" runat="server" class="data_summery ">
            </div>
        </div>
        <div class="clear Hgap10 ">
        </div>
    </div>
    <script>
        function ShowHospitalNewsPopup() {
            $(function () {
                var dlg = $("#KPI-Hospital-News").dialog({
                    resizable: false,
                    modal: false

                });
                dlg.parent().appendTo(jQuery("form:first"));
            });
        }
        function CloseHospitalNewsPopup() {
            $("#KPI-Hospital-News").dialog("close");
        }
    </script>
    <div style="display:none">
        <div id="KPI-Hospital-News" class="KPI-Hospital-News" title="<%= this.KPINewsHeader %>">
        <asp:ListView ID="LstVwKPIHospitalNews" runat="server">
            <ItemTemplate>
                <div class="Hospital-News-Article">
                    <h4><%#Eval("Title")%></h4>
                    <p><%#Eval("Description")%></p>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
    </div>
</asp:Content>
