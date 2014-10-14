<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master" AutoEventWireup="true" CodeFile="HospitalYTDTarget.aspx.cs" Inherits="Views_KPI_HospitalYTDTarget" Title="NHS KPI Data Entry Portal" %>
<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<script src="../../assets/scripts/jquery-1.10.2.min.js" type="text/javascript"></script>

    <script src="../../assets/scripts/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>
    
        <script src="../../scripts/json2.js" type="text/javascript"></script>--%>

    <script src="../../scripts/hospitalytdtarget.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <input id="hdnUserType" type="hidden" runat="server" />
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="grid_24">
        <div class="page-header position-relative">
            <h1>
                Ward Level Year To Date Target
            </h1>
        </div>
    </div>
     <div class="Hgap15"></div>
    <div class="grid_24 error_msg">
     
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>
     <div class="Hgap15"></div>
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
                Hospital Name:
            </label>
        </div>
        <div class="grid_5">           
           <asp:DropDownList ID="ddlHospitalName"
                runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlHospitalName_SelectedIndexChanged" 
                ValidationGroup="wardLevelStatic">
            </asp:DropDownList>
            <div class="clear">
            </div>
            <asp:Label ID="lblInvalidHospital" runat="server" ForeColor="Red" Style="display: none"></asp:Label>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfHospital" runat="server" Font-Bold="True" ValidationGroup="wardLevel"
                Font-Names="10" ControlToValidate="ddlHospitalName" ForeColor="Red" ErrorMessage="Hospital is required"
                Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        </div>
        <div class="grid_3">
            <label>
                KPI Description:
            </label>
        </div>
        <div class="grid_6">           
           <asp:DropDownList ID="ddlKPI"
                runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlKPI_SelectedIndexChanged" 
                ValidationGroup="wardLevelStatic">
            </asp:DropDownList>
            <div class="clear">
            </div>
            <asp:Label ID="lblInvalidKPI" runat="server" ForeColor="Red" Style="display: none"></asp:Label>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfKPI" runat="server" Font-Bold="True" ValidationGroup="wardLevelStatic"
                Font-Names="10" ControlToValidate="ddlKPI" ForeColor="Red" ErrorMessage="KPI is required"
                Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="clear">
    </div>
    <div class="Hgap15">
    </div>
   
     <div class="clear">
    </div>
     <div class="Hgap15">
    </div>
    <div id="divStaticTarget" runat="server">
        <div class="grid_24">
            <div class="grid_1">
                <a class="btn btn-xs btn-primary" id="btnstatichide"><i class="icon-minus"></i></a>
                <a class="btn btn-xs btn-primary" id="btnstaticshow" style="display: none;"><i class="icon-plus">
                </i></a>
            </div>
            <div class="grid_12">
                <h3>
                    Hospital YTD Target</h3>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="Hgap15">
        </div>
        <div id="static">
            <div class="grid_24">
                <div class="grid_3">
                    <label>
                        
                    </label>
                </div>
                <div class="grid_1">
                    <asp:Label ID="Label13" runat="server"></asp:Label></div>
            </div>
            <div class="grid_24">
                <div class="grid_3">
                    <label>
                        Target Description
                    </label>
                </div>
                <div class="grid_5">
                    <asp:TextBox ID="txtYTDTargetDescription" runat="server" Width="40px" 
                        ValidationGroup="wardLevelStatic"></asp:TextBox>
                    <div class="clear">
                    </div>

                </div>
                <div class="grid_3">
                </div>
            </div>
            <div class="grid_24">
                <div class="grid_3">
                    <label>
                        Target Green
                    </label>
                </div>
                <div class="grid_1">
                    <asp:TextBox ID="txtYTDTargetGreen" runat="server" MaxLength="5" 
                        onkeypress="return isDecimalKey(event);" ValidationGroup="wardLevelStatic"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="revYTDTargetGreen" runat="server" ControlToValidate="txtYTDTargetGreen"
                           Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                           ValidationGroup="wardLevelStatic"></asp:RegularExpressionValidator>
                    </div>
            </div>
            <div class="grid_24">
                <div class="grid_3">
                    <label>
                        Target Amber
                    </label>
                </div>
                <div class="grid_1">
                    <asp:TextBox ID="txtYTDTargetAmber" runat="server" MaxLength="5" 
                        onkeypress="return isDecimalKey(event);" ValidationGroup="wardLevelStatic"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="revYTDTargetAmber" runat="server" ControlToValidate="txtYTDTargetAmber"
                           Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                           ValidationGroup="wardLevelStatic"></asp:RegularExpressionValidator>
                    </div>
            </div>
        </div>
    </div>
    <div class="grid_24" id="divButtons" runat="server">
        <div class="clear">
        </div>
        <div class="Hgap15">
        </div>
        <div class="grid_16">
            <asp:Button ID="btnSave" runat="server" Text="Save" Visible="True" OnClick="btnSave_Click"
                ValidationGroup="wardLevelStatic" />           
            <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="True" 
                onclick="btnUpdate_Click" ValidationGroup="wardLevelStatic" />
        </div>
    </div>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

