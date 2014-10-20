<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuickStart.aspx.cs" Inherits="QuickStart" %>

<!DOCTYPE html>

<!--[if IE 7]> <html lang="en" class="ie7 login no-js"> <![endif]-->
<!--[if IE 8]> <html lang="en" class="ie8 login no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 login no-js"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en" class="no-js login">
<!--<![endif]-->
<!-- BEGIN HEAD -->
<head id="Head1" runat="server">
    <meta charset="UTF-8" />
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
            <img class="NHS_logo" alt="" src="assets/images/NHSlogo.gif" />
            <asp:Label class="hosName" ID="hosName" runat="server" Text=""></asp:Label>
            <asp:Label class="NHS_name" ID="NHS_name" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <div class="container_24">


        <form id="form1" runat="server">
            <div class="wizard-container" id="divBody">
                <asp:Wizard ID="Wizard1" runat="server">
                    <LayoutTemplate>
                        <div class="wizard-header-placeholder">
                            <asp:PlaceHolder ID="headerPlaceHolder" runat="server" />
                        </div>
                        <div class="wizard-sidebar-placeholder">
                            <asp:PlaceHolder ID="sideBarPlaceHolder" runat="server" />
                        </div>
                        <div class="wizard-steps-placeholder">
                            <asp:PlaceHolder ID="WizardStepPlaceHolder" runat="server" />
                        </div>
                        <div class="wizard-navigation-placeholder">
                            <asp:PlaceHolder ID="navigationPlaceHolder" runat="server" />
                        </div>
                    </LayoutTemplate>

                    <NavigationButtonStyle CssClass="next-button" />
                    

                    <SideBarTemplate>
                        <asp:ListView ID="sideBarList" runat="server">
                            <LayoutTemplate>
                                <div id="ItemPlaceHolder" runat="server" />
                            </LayoutTemplate>

                            <ItemTemplate>
                                <asp:Button ID="sideBarButton" CssClass="round-button-circle" runat="server" />
                            </ItemTemplate>
                            <SelectedItemTemplate>
                                <asp:Button ID="sideBarButton" CssClass="round-button-circle-selected" runat="server" />
                            </SelectedItemTemplate>
                            

                        </asp:ListView>
                    </SideBarTemplate>

                    <WizardSteps>
                        <asp:WizardStep ID="WizardStep1" runat="server" Title="Step 1">
                            <h1>Ward Setup</h1>
                            <p>
                                If the Trust is going to use NHS KPI tool to capture ward level data, 
                                you will have to configure the ward table as per the given <u>template</u>. 
                                Please click <u>template</u> text to download the correct <u>template</u> for you to upload. 
                                The upload file has to be csv or xls. The fields that are highlighted in 
                                <strong style="color:red">Red</strong> are mandatory fields.
                            </p>
                            <asp:TextBox ID="txtName" placeholder="Name" runat="server"></asp:TextBox>
                            <asp:Button ID="btnTrial" runat="server" Text="Brows" />
                            <p>
                                You can upload this details at later stage too, 
                                but you must do it before start using the tool to 
                                capture the data at ward level.
                            </p>
                        </asp:WizardStep>
                        <asp:WizardStep ID="WizardStep2" runat="server" Title="Step 2">
                            Step 2 Content.
                        </asp:WizardStep>
                        <asp:WizardStep ID="WizardStep3" runat="server" Title="Step 2">
                            Step 3 Content.
                        </asp:WizardStep>
                        <asp:WizardStep ID="WizardStep4" runat="server" Title="Step 2">
                            Step 4 Content.
                        </asp:WizardStep>
                    </WizardSteps>
                </asp:Wizard>
            </div>
        </form>
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
