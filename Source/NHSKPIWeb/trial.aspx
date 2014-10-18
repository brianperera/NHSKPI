<%@ Page Language="C#" AutoEventWireup="true" CodeFile="trial.aspx.cs" Inherits="login" %>

<!DOCTYPE html>
<!--[if IE 7]> <html lang="en" class="ie7 no-js"> <![endif]-->
<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en" class="no-js">
<!--<![endif]-->
<!-- BEGIN HEAD -->
<head id="Head1" runat="server">
    <link href="assets/images/favicon.ico" type="image/x-icon" rel="icon" />
    <link href="assets/images/favicon.ico" type="image/x-icon" rel="shortcut icon" />
    <title>NHS KPI Data Entry Portal</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="assets/css/redmond/jquery-ui-1.10.3.custom.min.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/responsive.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="assets/plugins/font-awesome/css/font-awesome.min.css" />
    <!--[if IE 7]>
  <link rel="stylesheet" href="assets/plugins/font-awesome/css/font-awesome-ie7.min.css">
<![endif]-->
</head>
<body class="trial" id="">
    <div class="nav_bar">
        <div class="float-left">
            <img alt="" src="assets/images/logo_s.png" />
            <span>KPI Data Entry Portal</span>
        </div>
        <div class="client_name">
            <img class="NHS_logo" alt="" src="assets/images/NHSlogo.gif" />
            <asp:Label class="hosName" ID="hosName" runat="server" Text=""></asp:Label>
            <asp:Label class="NHS_name" ID="NHS_name" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <div class="container_24" id="divBody" runat="server">
        <div class="Hgap15">
        </div>
        <div class="full-page-background">
<div class="grid_19 trial_wrap page-background_wrap">
            <div class="trial_info">
                <h1>
                    Make dashboards incredibly easy to produce</h1>
                <h2>
                    With this free trial you can start collecting all the necessary KPIs instantly.</h2>
                <ul>
                    <li>Say goodbye to multiple Excel sheets</li>
                    <li>No more waiting and data duplication</li>
                    <li>Centrally located KPIS data</li>
                    <li>Easy refresh - Point to Point connection available </li>
                    <li>Customised data download templates</li>
                    <li>Automated reminders and escalations </li>
                    <li>Decentralised data collection</li>
                </ul>
            </div>
            <div class="img_wrap page-background">
                <img alt="" src="assets/images/trial.png" height:"200px" />
            </div>
            </div>
        <div class="grid_5 trial_details">
            <form id="Form1" runat="server">
            <div class="grid_24 error_msg">
                <asp:Label ID="lblAddMessage" runat="server" Text=""></asp:Label>
            </div>
            <div class="trial_details_wrap">
                <h3>
                    Try KPI Portal for FREE</h3>
                <asp:TextBox ID="txtName" placeholder="Name" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtName"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlHospitalName" placeholder="Company Name" runat="server" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Text="Select Hospital" Value ="-2"></asp:ListItem>
                    <asp:ListItem Text="Other" Value ="-1"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtCompanyName" placeholder="Company Name" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtCompanyName"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtPhoneName" placeholder="Phone Number" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtPhoneName"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtEmailAddress" placeholder="Email Address" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtEmailAddress"
                    Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                        runat="server" ErrorMessage="Invalid" ControlToValidate="txtEmailAddress" Display="Dynamic"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtUserName" placeholder="User Name" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txtUserName"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtPassword" placeholder="Password" runat="server" 
                    TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="txtPassword"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtRetypePassword" placeholder="Re Type Password" 
                    runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txtRetypePassword"
                    Display="Dynamic"></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator1"
                        runat="server" ErrorMessage="Invalid" ControlToCompare="txtPassword" ControlToValidate="txtRetypePassword"
                        Display="Dynamic"></asp:CompareValidator>
                <asp:Button CssClass="btnTry" ID="btnTrial" runat="server" Text="Proceed" OnClick="btnTrial_Click" />
                <h4>
                    By clicking “Start Trial” I agree that:</h4>
                <ul>
                    <li>I hereby grant my consent to receive emails, communications, announcements and invitations
                        from KPI Data Entry Portal. You can withdraw your consent at any time.</li></ul>
                <h4 class="inqu">
                    Inquiries</h4>
                <p>
                    <a href="#">inquiries@nhskpi.net</a></p>
            </div>
            </form>
        </div>
        </div>
    </div>
    <div class="container_24" id="divMessage" runat="server" visible="false">
    <asp:Label ID="lblMessage" CssClass="alert-success" runat="server" Text="Your Trail Version Request Successfully Submitted. Administrator will review your request and confirm by email."></asp:Label>
    </div>
    <!-- END FOOTER -->
    <!-- BEGIN CORE PLUGINS -->
    <script type="text/javascript" src="assets/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="assets/scripts/jquery-migrate-1.2.1.min.js"></script>
    <script type="text/javascript" src="assets/scripts/jquery-ui-1.10.3.custom.min.js"></script>
    <script src="scripts/GeneralUtility.js"></script>
    <!-- HTML5 shim and Respond.js IE8/7 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="assets/plugins/html5shiv.js"></script>
      <script src="assets/plugins/respond.min.js"></script>
    <![endif]-->
</body>
</html>
