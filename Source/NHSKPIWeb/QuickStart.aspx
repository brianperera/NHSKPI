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
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            <p>
                                You can upload this details at later stage too, 
                                but you must do it before start using the tool to 
                                capture the data at ward level.
                            </p>

                            <h1>Specialty Setup</h1>
                            <p>
                                If the Trust is going to use NHS KPI tool to capture specialty level data, 
                                you will have to configure the specialty table as per the given <u>template</u>. 
                                Please click <u>template</u> text to download the correct <u>template</u> for you to upload. 
                                NHS KPI will be using the NHS data dictionary defined Treatment Functions. 
                                In order to get the maximum benefit from the tool you may populate the extra 
                                fields in the <u>template</u>. The upload file has to be csv or xls. 
                                The fields that are highlighted in <strong style="color:red">Red</strong> are mandatory fields.
                            </p>
                            <asp:FileUpload ID="FileUpload2" runat="server" />
                            <p>
                                You do not have to upload this data as this is not a mandatory upload. 
                                However it will be more beneficial for the staff to break down the 
                                specialties into divisions (in some hospitals these divisions 
                                are called care groups, sectors etc...) so the data entry process 
                                or the reporting process can be more simplified.
                            </p> 
                        </asp:WizardStep>
                        <asp:WizardStep ID="WizardStep2" runat="server" Title="Step 2">
                            <h1>Setup KPIs</h1>
                            KPI Groups:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="Button" />
                            <p>
                                These KPI groups will help you do categorise the KPIs into separate 
                                focus groups such as Patient Experience, 
                                Human Resources, Governance, Infection control etc... 
                                Each hospital has their own focus groups so you can create your own 
                                based on your Trust's requirements.
                            </p>
                            <p>
                                Note: If you are not sure what focus groups to be setup, 
                                the system will allow you to create them when you 
                                setting up KPIs in the "Manage KPI".
                            </p>
                            <div>
                                <div>Governance</div>
                                <div>Patient Experience</div>
                                <div>HR</div>
                                <div>Infection Control</div>
                            </div>
                            <h1>Setup KPIs</h1>
                            <p>
                                NHS KPI control the list of KPIs in the tool in order to keep the 
                                consistency and to avoid duplication. If you want to see the list 
                                of KPIs please click <u>here</u>. We have add all the possible KPIs that 
                                many hospitals have been using at the moment. However If you are 
                                required to add new KPIs to the system, please <u>contact us</u> and 
                                follow the instructions in the pop up box. If the new KPI is 
                                approved by the NHS KPI team they will be added to the list 
                                within 24 hours and you will be notified accordingly.

                            </p>                                   
                        </asp:WizardStep>
                        <asp:WizardStep ID="WizardStep3" runat="server" Title="Step 2">
                                               
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
