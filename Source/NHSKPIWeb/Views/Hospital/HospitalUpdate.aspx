<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="HospitalUpdate.aspx.cs" Inherits="Views_Hospital_HospitalUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<script src="../../assets/scripts/jquery-1.10.2.min.js" type="text/javascript"></script>

    <script src="../../assets/scripts/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>

    <script src="../../scripts/json2.js" type="text/javascript"></script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            Hospital Details
        </h1>
    </div>
    <div class="grid_24 error_msg">
        <asp:Label ID="lblAddMessage" runat="server" Text=""></asp:Label>
    </div>
    <div class="grid_12">
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblHospitalName" runat="server" Text="Hospital Name :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtHospitalName" runat="server" MaxLength="250"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvHospitalName" runat="server" ControlToValidate="txtHospitalName"
                ErrorMessage="Hospital Name is required" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblHospitalCode" runat="server" Text="Hospital Code :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtHospitalCode" runat="server" MaxLength="10"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvHospitalCode" runat="server" ControlToValidate="txtHospitalCode"
                ErrorMessage="Hospital Code is required" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblHospitalType" runat="server" Text="Hospital Type :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:DropDownList ID="ddlType" runat="server">
                <asp:ListItem Value="Null">--Select Hospital Type --</asp:ListItem>
                <asp:ListItem>NHS Trust</asp:ListItem>
                <asp:ListItem>NHS Foundation Trust</asp:ListItem>
            </asp:DropDownList>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvHospitalType" runat="server" ControlToValidate="ddlType"
                ErrorMessage="Hospital Type is required" Display="Dynamic" InitialValue="Null"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblAddress" runat="server" Text="Address :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtAddress" runat="server" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                ErrorMessage="Hospital Address is required" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblPhoneNumber" runat="server" Text="Phone Number :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="15"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" ControlToValidate="txtPhoneNumber"
                ErrorMessage="Phone Number is required" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPhoneNumber" runat="server" 
                ControlToValidate="txtPhoneNumber" Display="Dynamic" 
                
                ErrorMessage="This  Phone Number format is wrong" 
                ValidationExpression="^(\d*[-\s]?\d*[-\s]?)*$"></asp:RegularExpressionValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8" visible="false">
            <asp:Label ID="lblLogoPath" runat="server" Text="Logo Path:" Visible="False"></asp:Label>
        </div>
        <div class="grid_16">
            &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" Width="186px" Visible="False" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblIsActive" runat="server" Text="Is Active :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_16 prefix_8">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="False" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                Visible="False" UseSubmitBehavior="False" />      
      
        </div>
        <div class="clear">
        </div>
    </div>    
   
    
   
</asp:Content>
