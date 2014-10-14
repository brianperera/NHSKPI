<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="SpecialtyLevelDataBulkKPI.aspx.cs" Inherits="Views_KPI_SpecialtyLevelDataBulkKPI" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../scripts/specialtyleveldatabulkkpi.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function popUp(width, height, tras, url) {
            testwindow = window.open(url, tras, 'width=700,height=550,scrollbars=yes,menubar=no,addressbar=no,titlebar=no,toolbar=no');
            testwindow.focus();


        }          
            
            
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <asp:HiddenField ID="hdnHospitalId" runat="server" />
    <asp:HiddenField ID="hdnKPIIdList" runat="server" />
    <asp:HiddenField ID="hdnSpecialtyId" runat="server" />
    <asp:HiddenField ID="hdnYear" runat="server" />
    <div class="grid_24">
        <div class="page-header position-relative">
            <h1>
                Specialty Level Bulk Data Upload By KPI
            </h1>
        </div>
    </div>
    <div class="Hgap15">
    </div>
    <div id="divSucess" class="grid_24 error_msg" style="display: none">
        <asp:Label ID="lblMessage" CssClass="alert-success" runat="server" Text="Specialty KPI Data successfully updated"></asp:Label>
    </div>
    <div class="Hgap15">
    </div>
    <div class="grid_24">
        <div class="grid_3">
            <h3>
                <label>
                    Financial Year:</label>
            </h3>
        </div>
        <div class="grid_1">
            <asp:ImageButton ID="imgBtnPrevoius" runat="server" OnClick="imgBtnPrevoius_Click"
                ImageUrl="~/assets/images/l_arrow.png" /></div>
        <div class="grid_2 center">
            <h3>
                <asp:Label ID="lblCurentFinancialYear" runat="server"></asp:Label></h3>
        </div>
        <div class="grid_1 right">
            <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/assets/images/r_arrow.png"
                OnClick="imgBtnNext_Click" /></div>
    </div>
    <div class="clear">
    </div>
    <div class="Hgap15">
    </div>
    <div class="grid_24">
        <div class="grid_3">
            <label>
                Specialty:
            </label>
        </div>
        <div class="grid_5">
            <asp:DropDownList ID="ddlSpecialty" runat="server" OnSelectedIndexChanged="ddlSpecialty_SelectedIndexChanged"
                AutoPostBack="True">
            </asp:DropDownList>
            <div class="clear">
            </div>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfSpecialty" runat="server" Font-Bold="True" ValidationGroup="specialtyLevel"
                Font-Names="10" ControlToValidate="ddlSpecialty" ForeColor="Red" ErrorMessage="Please select the Specialty"
                InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="clear">
    </div>
    <div class="Hgap15">
    </div>
    <div class="grid_24">
        <div class="grid_23" id="div1" runat="server">
            <div class="float-right">
                <asp:Button ID="btnSaveUp" runat="server" Text="Save" Visible="false" ValidationGroup="specialtyLevel"
                    OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <div class="Hgap15">
    </div>
    <div id="divDynamicTarget" runat="server">
    </div>
    <div class="grid_24">
        <div class="grid_23" id="divButtons" runat="server">
            <div class="clear">
            </div>
            <div class="Hgap15">
            </div>
            <div class="float-right">
                <asp:Button ID="btnSaveBottom" runat="server" Text="Save" Visible="false" ValidationGroup="specialtyLevel"
                    OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
</asp:Content>
