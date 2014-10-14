<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="PasswordChange.aspx.cs" Inherits="Views_User_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            Change Password
        </h1>
    </div>
    <div class="grid_12">
        <div class="grid_24 error_msg">
            <asp:Label ID="lblAddMessage" runat="server" Text=""></asp:Label>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblExistingPassword" runat="server" Text="Current Password :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtCurrentPassword" runat="server" MaxLength="250" TextMode="Password"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvCurrentPassword" runat="server" ErrorMessage="Current Password is required"
                ControlToValidate="txtCurrentPassword" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblPassword" runat="server" Text="New Password :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="New Password is required"
                ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:CompareValidator ID="cvConfirmPassword" runat="server" ErrorMessage="Confirm Password is Invalid."
                ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" 
                Display="Dynamic"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="rfvPassword0" runat="server" ErrorMessage="Confirm Password is required"
                ControlToValidate="txtConfirmPassword" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="grid_16 prefix_8">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </div>
    </div>
</asp:Content>
