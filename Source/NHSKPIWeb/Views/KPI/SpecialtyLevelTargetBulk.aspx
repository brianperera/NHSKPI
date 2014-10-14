<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="SpecialtyLevelTargetBulk.aspx.cs" Inherits="Views_KPI_SpecialtyLevelTargetBulk" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../scripts/specialtyleveltargetbulk.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <asp:HiddenField ID="hdnHospitalId" runat="server" />
    <asp:HiddenField ID="hdnSpecialtyIdList" runat="server" />
    <asp:HiddenField ID="hdnKPIType" runat="server" />
    <asp:HiddenField ID="hdnKPIId" runat="server" />
    <asp:HiddenField ID="hdnYear" runat="server" />
    <div class="grid_24">
        <div class="page-header position-relative">
            <h1>
                KPI Level Bulk Target Upload By Specialty 
            </h1>
        </div>
    </div>
    <div class="Hgap15">
    </div>
    <div id="divSucess" class="grid_24 error_msg" style="display: none">
        <asp:Label ID="lblMessage" CssClass="alert-success" runat="server" Text="Specialty KPI target successfully updated"></asp:Label>
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
                KPI Description:
            </label>
        </div>
        <div class="grid_4">
            <asp:DropDownList ID="ddlKPI" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlKPI_SelectedIndexChanged">
            </asp:DropDownList>
            <div class="clear">
            </div>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfKPI" runat="server" Font-Bold="True" ValidationGroup="wardLevel"
                Font-Names="10" ControlToValidate="ddlKPI" ForeColor="Red" ErrorMessage="Please select the KPI"
                Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="clear">
    </div>
    <div class="Hgap15">
    </div>
    <div class="grid_24">
        <div class="grid_23" id="div1" runat="server">
            <div class="float-right">
                <asp:Button ID="btnSaveUp" runat="server" Text="Save" Visible="false" ValidationGroup="wardLevel"
                    />
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
                <asp:Button ID="btnSaveBottom" runat="server" Text="Save" Visible="false" ValidationGroup="wardLevel" />
            </div>
        </div>
    </div>
</asp:Content>
