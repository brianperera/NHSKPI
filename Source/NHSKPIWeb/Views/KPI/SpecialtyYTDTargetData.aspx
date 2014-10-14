<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="SpecialtyYTDTargetData.aspx.cs" Inherits="Views_KPI_SpecialtyYTDTargetData" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<script src="../../assets/scripts/jquery-1.10.2.min.js" type="text/javascript"></script>

    <script src="../../assets/scripts/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>

    <script src="../../scripts/json2.js" type="text/javascript"></script>--%>

    <script src="../../scripts/specialtyleveldata.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <input id="hdnUserType" type="hidden" runat="server" />
    <div class="grid_24">
        <div class="page-header position-relative">
            <h1>
                Specialty Level Year To Date Data
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
        <div class="grid_3">
            <h3>
                <label>
                    Financial Year:</label>
            </h3>
        </div>
        <div class="grid_1">
            <asp:ImageButton ID="imgBtnPrevoius" runat="server" OnClick="imgBtnPrevoius_Click"
                ImageUrl="~/assets/images/l_arrow.png" /></div>
        <div class="grid_2">
            <h3>
                <asp:Label ID="lblCurentFinancialYear" runat="server"></asp:Label></h3>
        </div>
        <div class="grid_1">
            <asp:ImageButton ID="imgBtnNext" runat="server" OnClick="imgBtnNext_Click" ImageUrl="~/assets/images/r_arrow.png" /></div>
    </div>
    <div class="clear">
    </div>
    <div class="Hgap15">
    </div>
    <div class="grid_24">
        <div id="divHospital">
            <div class="grid_3">
                <label>
                    Specialty:
                </label>
            </div>
            <div class="grid_5">
                <asp:DropDownList ID="ddlSpecialty" runat="server" AutoPostBack="True" 
                    OnSelectedIndexChanged="ddlSpecialty_SelectedIndexChanged" 
                    CausesValidation="True" ValidationGroup="wardLevel">
                </asp:DropDownList>
                <div class="clear">
                </div>
                <asp:Label ID="lblInvalidHospital" runat="server" ForeColor="Red" Style="display: none"></asp:Label>
                <div class="clear">
                </div>
                <asp:RequiredFieldValidator ID="rfHospital" runat="server" Font-Bold="True" ValidationGroup="wardLevel"
                    Font-Names="10" ControlToValidate="ddlSpecialty" ForeColor="Red" ErrorMessage="Specialty is required"
                    Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="grid_3">
            <label>
                KPI Description:
            </label>
        </div>
        <div class="grid_6">
            <asp:DropDownList ID="ddlKPI" runat="server" AutoPostBack="True" 
                OnSelectedIndexChanged="ddlKPI_SelectedIndexChanged" CausesValidation="True" 
                ValidationGroup="wardLevel">
            </asp:DropDownList>
            <div class="clear">
            </div>
            <asp:Label ID="lblInvalidKPI" runat="server" ForeColor="Red" Style="display: none"></asp:Label>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfKPI" runat="server" Font-Bold="True" ValidationGroup="wardLevelStatic"
                Font-Names="10" ControlToValidate="ddlKPI" ForeColor="Red" ErrorMessage="KPI is required"
                Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="clear">
    </div>
    <div class="Hgap15">
    </div>
    <div id="divDynamicTarget" runat="server">
        <div class="grid_24">
            <div class="grid_1">
                <a class="btn btn-xs btn-primary" id="btnhideward"><i class="icon-minus"></i></a>
                <a class="btn btn-xs btn-primary" id="btnshowward" style="display: none;"><i class="icon-plus">
                </i></a>
            </div>
            <div class="grid_12">
                <h3>
                    YTD Data</h3>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="Hgap15">
        </div>
        <div id="ward">         
                
                <div class="grid_24" id="divNumarator" runat="server">
                    <div class="grid_3">
                        <label>
                            
                                Numerator
                        </label>
                    </div>
                    <div class="grid_1">
                        <asp:TextBox ID="txtGreenApril" runat="server" Width="40px" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revGreenApril" runat="server" ControlToValidate="txtGreenApril"
                            Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                            ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                </div>              
                <div class="grid_24" id="divDenominator" runat="server">
                    <div class="grid_3">
                        <label>
                            
                                Denominator
                        </label>
                    </div>
                    <div class="grid_1">
                        <asp:TextBox ID="txtAmberApril" runat="server" Width="40px" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revAmberApril" runat="server" ControlToValidate="txtAmberApril"
                            Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                            ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                </div>
                
            
        </div>
        <div class="clear">
        </div>
        <div class="Hgap15">
        </div>
    </div>
    <div class="grid_24" id="divButtons" runat="server">
        <div class="clear">
        </div>
        <div class="Hgap15">
        </div>
        <div class="grid_16">
            <asp:Button ID="btnSave" runat="server" Text="Save" Visible="True" OnClick="btnSave_Click"
                ValidationGroup="wardLevel" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="True" 
                OnClick="btnUpdate_Click" ValidationGroup="wardLevel" />
        </div>
    </div>
</asp:Content>
