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
    <link href="assets/css/jquerystep/jquery.steps.css" rel="stylesheet" />
    <link href="assets/css/jquerystep/main.css" rel="stylesheet" />
    <link href="assets/css/jquerystep/normalize.css" rel="stylesheet" />
    <script src="assets/scripts/jquery-1.10.2.min.js"></script>
    <script src="assets/scripts/jquery-migrate-1.2.1.min.js"></script>
    <script src="assets/scripts/jquery-ui-1.10.3.custom.min.js"></script>
    <script src="assets/scripts/modernizr-2.6.2.min.js"></script>
    <script src="assets/scripts/jquery.cookie-1.3.1.js"></script>
    <script src="assets/scripts/jquery.steps.min.js"></script>

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
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="wizard-container" id="divBody">
                <div class="content">
                    <script>
                        $(function () {
                            $("#wizard").steps({
                                headerTag: "h2",
                                bodyTag: "section",
                                transitionEffect: "slideLeft"
                            });
                        });
                    </script>

                    <div id="wizard">
                        <h2>Step 1</h2>
                        <section>
                            <h1>Ward Setup</h1>
                            <p>
                                If the Trust is going to use NHS KPI tool to capture ward level data, 
                                you will have to configure the ward table as per the given <u>template</u>. 
                                Please click <u>template</u> text to download the correct <u>template</u> for you to upload. 
                                The upload file has to be csv or xls. The fields that are highlighted in 
                                <strong style="color: red">Red</strong> are mandatory fields.
                            </p>
                            <asp:TextBox ID="txtName" placeholder="Name" runat="server"></asp:TextBox>
                            <asp:Button ID="btnTrial" runat="server" Text="Brows" />
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
                                The fields that are highlighted in <strong style="color: red">Red</strong> are mandatory fields.
                            </p>
                            <asp:FileUpload ID="FileUpload2" runat="server" />
                            <p>
                                You do not have to upload this data as this is not a mandatory upload. 
                                However it will be more beneficial for the staff to break down the 
                                specialties into divisions (in some hospitals these divisions 
                                are called care groups, sectors etc...) so the data entry process 
                                or the reporting process can be more simplified.
                            </p>
                        </section>

                        <h2>Step 2</h2>
                        <section>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <h1>Setup KPIs</h1>
                            <asp:TextBox ID="txtKPIGroups" placeholder="KPI Groups" runat="server"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="Button" />
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
                            <h1>Adding new KPIs</h1>
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
                        </section>

                        <h2>Step 3</h2>
                        <section>
                            <h1>Security and User settings</h1>
                            <h2>Legitimacy of using this site</h2>
                            <p>
                                In order to verify the legitimacy of using this site behalf of your 
                                organisation (this is generally be the head of information, 
                                deputy director of informatics, director of performance etc...)  
                                , we need your head of the department contact details as we 
                                will be contacting him/her in order to get the approval of 
                                using NHS KPI tool to record your information. 
                                If we don't receive a respond within 28 days the system will be disables 
                                until such action take place.
                            </p>
                            <h3>Legitimacy of using this site</h3>
                            <div>
                                <asp:TextBox ID="txtJobTitle" placeholder="Job Title" runat="server"></asp:TextBox>
                                <asp:TextBox ID="TextBox1" placeholder="Name" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtContactDetails" placeholder="Contact Details" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtEmail" placeholder="Email" runat="server"></asp:TextBox>
                            </div>
                            <h2>Setup users (As you are using the free trail the system will allow to create only 4 users)</h2>
                            <p>
                                Note: the usernames must be the trust email address of each user as the system will be sending an email to each user with a link and their login credentials. 
                            </p>
                            <p>
                                *** Usual setting users can go here
                            </p>
                        </section>

                        <h2>Step 4</h2>
                        <section>
                            <h1>Congratulations!</h1>
                            <p>
                                You are successfully setup the NHS KPI tool. 
                                We have sent you an email link for you to login to the NHS KPI tool. 
                                You must use this link to logon to the system first time. 
                                After the first login you will be able to login to the system 
                                using the sign in option in the hope page.  
                            </p>
                            <asp:Button ID="btnViewDemo" Text="View Demo" runat="server" />
                            <asp:Button ID="btnViewUserManual" Text="View User Manual" runat="server" />
                        </section>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <!-- END FOOTER -->
    <!-- BEGIN CORE PLUGINS -->
    <!-- HTML5 shim and Respond.js IE8/7 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="assets/plugins/html5shiv.js"></script>
      <script src="assets/plugins/respond.min.js"></script>
    <![endif]-->
</body>
</html>
