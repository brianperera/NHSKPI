<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="UserUpdate.aspx.cs" Inherits="Views_User_UserUpdate" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<script src="../../assets/scripts/jquery-1.10.2.min.js" type="text/javascript"></script>

    <script src="../../assets/scripts/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>

    <script src="../../scripts/json2.js" type="text/javascript"></script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            User Detail
        </h1>
    </div>
    <div class="grid_12">
        <div class="grid_24 error_msg">
            <asp:Label ID="lblAddMessage" runat="server" Text=""></asp:Label>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblUserName" runat="server" Text="User Name :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtUserName" runat="server" MaxLength="50"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="User Name is required"
                ControlToValidate="txtUserName" Display="Dynamic" ValidationGroup="General"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8" id="divPasswordLabel" runat="server">
            <asp:Label ID="lblPassWord" runat="server" Text="Password :"></asp:Label>
        </div>
        <div class="grid_16" id="divPasswordText" runat="server">
            <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password" MaxLength="250"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvPassWord" runat="server" ErrorMessage="Password is required"
                ControlToValidate="txtPassWord" Display="Dynamic" ValidationGroup="General"></asp:RequiredFieldValidator>
        </div>
        <div class="clear" id="divPasswordClear" runat="server">
        </div>
        <div class="grid_8" id="divConfirmPasswordLabel" runat="server">
            <asp:Label ID="lblConfrimPassword" runat="server" Text="Confirm Password :"></asp:Label>
        </div>
        <div class="grid_16" id="divConfirmPasswordText" runat="server">
            <asp:TextBox ID="txtConfrimPassword" runat="server" TextMode="Password" MaxLength="250"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvConfrimPassword" runat="server" ErrorMessage="Confrim Password is required"
                ControlToValidate="txtConfrimPassword" Display="Dynamic" ValidationGroup="General"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToCompare="txtPassWord"
                ControlToValidate="txtConfrimPassword" ErrorMessage="Passwords does not match each other."
                Display="Dynamic" ValidationGroup="General"></asp:CompareValidator>
        </div>
        <div class="clear" id="divConfirmPasswordClear" runat="server">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblFirstName" runat="server" Text="First Name :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtFirstName" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="First Name is required"
                ControlToValidate="txtFirstName" Display="Dynamic" ValidationGroup="General"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblLastName" runat="server" Text="Last Name :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtLastName" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Last Name is required"
                ControlToValidate="txtLastName" Display="Dynamic" ValidationGroup="General"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblEmail" runat="server" Text="Email :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtEmail" runat="server" MaxLength="150"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email is required"
                ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="General"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Email is invalid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                Display="Dynamic" ValidationGroup="General"></asp:RegularExpressionValidator>
            </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblMobileNo" runat="server" Text=" Contact Tel No :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="15"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvMobileNo" runat="server" ErrorMessage=" Contact Tel No is required"
                ControlToValidate="txtMobileNo" Display="Dynamic" ValidationGroup="General"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rfvContactTelNo" runat="server" ControlToValidate="txtMobileNo"
                ErrorMessage="This Contact Tel No format is wrong"
                ValidationExpression="^(\d*[-\s]?\d*[-\s]?)*$" Display="Dynamic" ValidationGroup="General"></asp:RegularExpressionValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblRoleName" runat="server" Text="Role Name :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:DropDownList ID="ddlRoleName" runat="server" >
            </asp:DropDownList>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvRoleName" runat="server" ErrorMessage="Role Name is required"
                ControlToValidate="ddlRoleName" Display="Dynamic" ValidationGroup="General"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8 hidden">
            <asp:Label ID="lblHospitalName" runat="server" Text="Hospital Name :"></asp:Label>
        </div>
        <div class="grid_16 hidden">
            <asp:DropDownList ID="ddlHospitalName" runat="server">
            </asp:DropDownList>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvHospitalName" runat="server" ErrorMessage="Hospital Name is required"
                ControlToValidate="ddlHospitalName" Display="Dynamic" InitialValue="0" ValidationGroup="General"></asp:RequiredFieldValidator>
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
        <div class="grid_8">
            <asp:Label ID="lblIsActiveDirectoryUser" runat="server" Text="Is Active Directory User :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:CheckBox ID="chkIsActiveDirectoryUser" runat="server" Checked="True" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_16 prefix_8">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="False"
                ValidationGroup="General" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                Visible="False" ValidationGroup="General" />
        </div>

        <script>
            $(document).ready(function() {
                $("#ctl00_Contentbody_rfvHospitalName").hide()
                $("#ctl00_Contentbody_ddlRoleName").change(function() {

                    if ($("#ctl00_Contentbody_ddlRoleName").val() == 1) {
                        ValidatorEnable(ctl00_Contentbody_rfvHospitalName, false);
                        $("#ctl00_Contentbody_rfvHospitalName").hide()
                    }
                    else {
                        $("#ctl00_Contentbody_rfvHospitalName").show()
                    }
                });
            });
        </script>

    </div>
</asp:Content>
