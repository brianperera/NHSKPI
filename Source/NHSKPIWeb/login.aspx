<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<!--[if IE 7]> <html lang="en" class="ie7 login no-js"> <![endif]-->
<!--[if IE 8]> <html lang="en" class="ie8 login no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 login no-js"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en" class="no-js login">
<!--<![endif]-->
<!-- BEGIN HEAD -->
<head id="Head1" runat="server">
    <meta charset="UTF-8"utf-8" />
    <link  href="assets/images/favicon.ico" type="image/x-icon" rel="icon"/>
      <link  href="assets/images/favicon.ico" type="image/x-icon" rel="shortcut icon"/>
    <title>NHS KPI Data Entry Portal</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="assets/css/redmond/jquery-ui-1.10.3.custom.min.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/responsive.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="assets/plugins/font-awesome/css/font-awesome.min.css">
    <!--[if IE 7]>
  <link rel="stylesheet" href="assets/plugins/font-awesome/css/font-awesome-ie7.min.css">
<![endif]-->
</head>
<body id="login">
<div class="nav_bar">
<div class="float-left">
<img alt="" src="assets/images/logo_s.png" />
<span>KPI Data Entry Portal</span>
</div>
<div class="client_name">
<img class="NHS_logo"  alt="" src="assets/images/NHSlogo.gif" />
<asp:Label class="hosName" ID="hosName" runat="server" Text=""></asp:Label>
<asp:Label class="NHS_name" ID="NHS_name" runat="server" Text=""></asp:Label>
</div>
</div>
    <div class="container_24">
        
        
            <form id="form1" runat="server">
            <div class="login_container">
                <img class="lock" src="assets/images/lock.png" alt="" />
                <div class="clear-fix">
                </div>
                <div class="login_content">
                    <h1>
                        KPI Data Entry Portal</h1>
                    <div class="clear-fix">
                        
                    </div>
                    <div class="grid_24 error_msg"><asp:Label ID="lblMessage" CssClass="alert-danger" runat="server" Visible="false"></asp:Label></div>
                    <span><i class="icon-user"></i>username</span>
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <div class="clear-fix">
                    </div>
                    <span><i class="icon-lock"></i>password</span>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <div class="clear-fix">
                    </div>
                    
                    <div class="clear-fix">
                    </div>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="ie_btn" 
                        onclick="btnLogin_Click" CausesValidation="False"   />
                </div>
                 <div class="login_footer">
                     <img src="assets/images/NHSlogo.gif" alt="logo" />
                   <span> Copyright © 2013 by NHS Foundation Trust. All rights reserved. </span>
                 </div>
            </div>
            </form>
       
        
        <div class="grid_12 prefix_1 lgn_messages">
            <div class="grid_11"><h2>New Features</h2><p>1. Bulk target and update facility has been implemented. </p>
            <p>2. Facility to enter comments against each KPI.</p>
            
            </div>
            <div class="grid_11 prefix_1"><h2>Deadlines</h2>
            <p>1. All the data entry has to be completed 15th working day of the month.</p>
            
            </div>
            
        </div>
    </div>
    <!-- END FOOTER -->
    <!-- BEGIN CORE PLUGINS -->

    <script src="assets/scripts/jquery-1.10.2.min.js"></script>

    <script src="assets/scripts/jquery-migrate-1.2.1.min.js"></script>

    <script src="assets/scripts/jquery-ui-1.10.3.custom.min.js"></script>

    <!-- HTML5 shim and Respond.js IE8/7 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="assets/plugins/html5shiv.js"></script>
      <script src="assets/plugins/respond.min.js"></script>
    <![endif]-->
</body>
</html>
