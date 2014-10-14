<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="BenchMarkReport.aspx.cs" Inherits="Views_KPI_BenchMarkReport" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!--[if lte IE 8]><script language="javascript" type="text/javascript" src="../../assets/scripts/excanvas.min.js"></script><![endif]-->
    <script src="../../assets/scripts/visualize.jQuery.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://filamentgroup.github.com/EnhanceJS/enhance.js"></script>
    <script type="text/javascript">
        $(function () {
            if ($('#<%=rdoChartType.ClientID %> input:checked').val() == '1') {
                $('#Chart').visualize({ type: 'bar', width: '900px', height: '400px', lineWeight: '2' });
            }
            else {
                $('#Chart').visualize({ type: 'line', width: '900px', height: '400px', lineWeight: '2' });
            }
        });

        var max = 5
        function ValidateTrustName() {
            if (Page_ClientValidate()) {
                var CHK = document.getElementById("<%=chkHospital.ClientID%>");
                var checkbox = CHK.getElementsByTagName("input");
                var counter = 0;
                for (var i = 0; i < checkbox.length; i++) {
                    if (checkbox[i].checked) {
                        counter++;
                    }
                }
                if (max < counter) {
                    alert("Allow to select only 5 at a time!");
                    return false;
                }
                return true;
            }
            else {
                return false;
            }
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="grid_24">
        <div class="page-header position-relative">
            <h1>
                Benchmark Report
            </h1>
        </div>
    </div>
    <div class="Hgap15">
    </div>
    <div class="grid_24 error_msg">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>
    <div class="Hgap15">
    </div>
    <div class="grid_24">
        <div class="grid_2">
            <label>
                KPI:</label>
        </div>
        <div class="grid_4">
            <asp:DropDownList ID="ddlKPI" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlKPI"
                Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
        </div>
        <div class="clear Hgap10">
        </div>
        <div class="grid_2">
            <label>
                Chart Type:</label>
        </div>
        <div class="grid_8">
            <asp:RadioButtonList CssClass="rdoDateRange" ID="rdoChartType" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="1" Selected="True">Bar</asp:ListItem>
                <asp:ListItem Value="2">Inline</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div class="clear Hgap10">
        </div>
        <div class="grid_2">
            <label>
                Date Range:</label>
        </div>
        <div class="grid_8">
            <asp:RadioButtonList CssClass="rdoDateRange" ID="rdoDateRange" runat="server" RepeatDirection="Horizontal"
                AutoPostBack="True" OnSelectedIndexChanged="rdoDateRange_SelectedIndexChanged">
                <asp:ListItem Value="1" Selected="True">Financial Year</asp:ListItem>
                <asp:ListItem Value="2">Rolling 12 Months</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div class="clear Hgap10">
        </div>
        <div class="grid_2" id="divFinancialYearLabel" runat="server">
            <label>
                Financial year:</label>
        </div>
        <div class="grid_8" id="divFinancialYearControl" runat="Server">
            <div class="grid_2">
                <asp:ImageButton ID="imgBtnPrevoius" runat="server" OnClick="imgBtnPrevoius_Click"
                    CausesValidation="false" ImageUrl="~/assets/images/l_arrow.png" /></div>
            <div class="grid_6 center">
                <h3>
                    <asp:Label ID="lblCurentFinancialYear" runat="server"></asp:Label></h3>
            </div>
            <div class="grid_2 right">
                <asp:ImageButton ID="imgBtnNext" runat="server" OnClick="imgBtnNext_Click" CausesValidation="false"
                    ImageUrl="~/assets/images/r_arrow.png" /></div>
        </div>
        <div class="clear Hgap10" id="divFinancialYearSpace" runat="server">
        </div>
        <div class="grid_2">
            <label>
                Trust Name(s):</label>
        </div>
        <div class="grid_20 chk_List">
            <div class="chk_List_wrap">
                <asp:CheckBoxList ID="chkHospital" CssClass="rdoDateRange" runat="server" RepeatColumns="3">
                </asp:CheckBoxList>
            </div>
        </div>
        <div class="clear Hgap10">
        </div>
        <div class="grid_4 prefix_2">
            <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClientClick="return ValidateTrustName()"
                OnClick="btnGenerate_Click" />
        </div>
    </div>
    <div class="clear Hgap30">
    </div>
    <div id="divReportTableContent" runat="server" class="grid_24">
        <div class="Hgap15">
        </div>
    </div>
    <div class="clear Hgap20">
    </div>
</asp:Content>
