<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuickStart.aspx.cs" Inherits="QuickStart" %>

<%@ Register Src="Views/Shared/SpecialtyBulkUploader.ascx" TagName="SpecialtyBulkUploader" TagPrefix="uc1" %>

<%@ Register Src="Views/Shared/WardBulkUploader.ascx" TagName="WardBulkUploader" TagPrefix="uc2" %>

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
    <%--<script src="assets/scripts/modernizr-2.6.2.min.js"></script>
    <script src="assets/scripts/jquery.cookie-1.3.1.js"></script>--%>
    <script src="assets/scripts/jquery.steps.min.js"></script>

</head>
<body id="login">
    <div class="nav_bar">
        <div class="float-left">
            <img alt="" src="assets/images/logo_s.png" />
            <span>KPI Data Entry Portal</span>
        </div>
        <div class="client_name">
            <%--<img class="NHS_logo" alt="" src="assets/images/NHSlogo.gif" />--%>
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
                                transitionEffect: "slideLeft",
                                enableFinishButton: false,
                                forceMoveForward: false
                            });
                        });
                    </script>

                    <div id="wizard">
                        <h2>1</h2>
                        <section>
                            <div class="wizard-content-left">
                                <h1>Ward Setup</h1>
                                <p>
                                    If the Trust is going to use NHS KPI tool to capture ward level data, 
                                you will have to configure the ward table as per the given <a href="./assets/doc/Ward_Upload_Template.csv">template</a>. 
                                Please click <a href="./assets/doc/Ward_Upload_Template.csv">template</a> text to download the correct <a href="./assets/doc/Ward_Upload_Template.csv">template</a> for you to upload. 
                                The upload file has to be a csv. The fields that are highlighted in 
                                <strong style="color: red">Red</strong> are mandatory fields.
                                </p>
                                <asp:UpdatePanel runat="server" ID="fuWardDataUploadUpdatePanel">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="WardBulkUploader1" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <uc2:WardBulkUploader ID="WardBulkUploader1" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="clear"></div>
                                <p>
                                    You can upload this details at later stage too, 
                                but you must do it before start using the tool to 
                                capture the data at ward level.
                                </p>
                            </div>
                            <div class="wizard-content-right">
                                <h1>Specialty Setup</h1>
                                <p>
                                    If the Trust is going to use NHS KPI tool to capture specialty level data, 
                                you will have to configure the specialty table as per the given <a href="./assets/doc/Specialty_Upload_Template.csv">template</a>. 
                                Please click <a href="./assets/doc/Specialty_Upload_Template.csv">template</a> text to download the correct <a href="./assets/doc/Specialty_Upload_Template.csv">template</a> for you to upload. 
                                NHS KPI will be using the NHS data dictionary defined Treatment Functions. 
                                In order to get the maximum benefit from the tool you may populate the extra 
                                fields in the <a href="./assets/doc/Specialty_Upload_Template.csv">template</a>. The upload file has to be a csv. 
                                The fields that are highlighted in <strong style="color: red">Red</strong> are mandatory fields.
                                </p>

                                <asp:UpdatePanel runat="server" ID="fuSpecialtyDataUploadUpdatePanel">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="SpecialtyBulkUploader1" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <uc1:SpecialtyBulkUploader ID="SpecialtyBulkUploader1" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="clear"></div>
                                <p>
                                    You do not have to upload this data as this is not a mandatory upload. 
                                However it will be more beneficial for the staff to break down the 
                                specialties into divisions (in some hospitals these divisions 
                                are called care groups, sectors etc...) so the data entry process 
                                or the reporting process can be more simplified.
                                </p>

                            </div>
                        </section>
                        <h2>2</h2>
                        <section>
                            <div class="wizard-content-left">
                                <h1>Setup KPIs</h1>
                                <div class="paragraph-break"></div>
                                <div class="field-title">
                                    KPI Groups:
                                </div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanelKPIGroups">
                                    <ContentTemplate>
                                        <div class="field-control-middle">
                                            <span>
                                                <asp:TextBox ID="txtKpiGroupName" CssClass="magin_0" placeholder="KPI Group" runat="server"></asp:TextBox>
                                            </span>
                                            <div class="paragraph-break">
                                                <asp:ListBox ID="lbKpiGroups" CssClass="listbox" Height="80" Width="160" runat="server"></asp:ListBox>
                                            </div>
                                            <div class="field-control-last">
                                                <asp:Button ID="btnRemoveKpiGroup" CssClass="file-uploaded-button" runat="server" Text="Remove" OnClick="btnRemoveKpiGroup_Click"/>
                                            </div>
                                        </div>
                                        <div class="grid_24 error_msg">
                                            <asp:Label ID="lblAddKpiGroupMessage" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="field-control-last">
                                            <asp:Button ID="btnAddKpiGroup" CssClass="file-uploaded-button" runat="server" Text="Add" OnClick="btnAddKpiGroup_Click" />                                            
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="clear"></div>
                                <p class="paragraph-break">
                                    These KPI groups will help you do categorise the KPIs into separate 
                                focus groups such as Patient Experience, 
                                Human Resources, Governance, Infection control etc... 
                                Each hospital has their own focus groups so you can create your own 
                                based on your Trust's requirements.
                                </p>
                                <div class="notice fg-white">
                                    <strong>Note: </strong>If you are not sure what focus groups to be setup, 
                                the system will allow you to create them when you 
                                setting up KPIs in the "Manage KPI".
                                </div>
                            </div>
                            <div class="wizard-content-right">
                                <h1>Adding new KPIs</h1>
                                <p>
                                    NHS KPI control the list of KPIs in the tool in order to keep the 
                                consistency and to avoid duplication. If you want to see the list 
                                of KPIs please click <u>here</u>. We have add all the possible KPIs that 
                                many hospitals have been using at the moment. However If you are 
                                required to add new KPIs to the system, please send us a email to helpdesk@nhskpi.net. 
                                If the new KPI is approved by the NHS KPI team they will be added to the list 
                                within 24 hours and you will be notified accordingly.

                                </p>
                            </div>
                        </section>

                        <h2>3</h2>
                        <section>
                            <div class="wizard-content-left">
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

                                <asp:UpdatePanel runat="server" ID="ManagerDetailsUpdatePanel">
                                    <ContentTemplate>
                                        <h2>Details of the head of the department</h2>

                                        <div class="field-title">Job Title</div>
                                        <div class="field-control-last">
                                            <asp:TextBox ID="txbManagerJobTitle" Width="200" placeholder="Job Title" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="field-title">Name</div>
                                        <div class="field-control-last">
                                            <asp:TextBox ID="txbManagerName" Width="200" placeholder="Name" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="field-title">Contact Details</div>
                                        <div class="field-control-last">
                                            <asp:TextBox ID="txbManagerContactDetails" Width="200" placeholder="Contact Details" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="field-title">Email</div>
                                        <div class="field-control-last">
                                            <asp:TextBox ID="txbManagerEmail" Width="200" placeholder="Email" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="field-title">&nbsp;</div>
                                        <div class="field-control-last">
                                            <asp:Button ID="btnAddUpdateManagerDetails" CssClass="file-uploaded-button" runat="server" Text="Add" OnClick="btnAddUpdateManagerDetails_Click" />
                                        </div>
                                        <div class="grid_24 error_msg">
                                            <asp:Label ID="lbAddUpdateManagerDetailsMessage" runat="server" Text=""></asp:Label>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="wizard-content-right second-column-sub-header">
                                <asp:UpdatePanel runat="server" ID="AddUsersUpdatePanel">
                                    <ContentTemplate>
                                        <h2>Setup users</h2>
                                        <p>(As you are using the free trail the system will allow to create only 4 users)</p>
                                        <div class="small-column-left-1">
                                            <div class="field-title">Username</div>
                                            <div class="field-control-last">
                                                <asp:TextBox ID="txbUsername" Width="200" placeholder="Username" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                    ControlToValidate="txbUsername" Display="Dynamic" ValidationGroup="AddUsers"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="field-title">First Name</div>
                                            <div class="field-control-last">
                                                <asp:TextBox ID="txbFirstName" Width="200" placeholder="First Name" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                    ControlToValidate="txbFirstName" Display="Dynamic" ValidationGroup="AddUsers"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="field-title">Last Name</div>
                                            <div class="field-control-last">
                                                <asp:TextBox ID="txbLastName" Width="200" placeholder="Last Name" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                    ControlToValidate="txbLastName" Display="Dynamic" ValidationGroup="AddUsers"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="field-title">Email</div>
                                            <div class="field-control-last">
                                                <asp:TextBox ID="txbEmail" Width="200" placeholder="Email" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                    ControlToValidate="txbEmail" Display="Dynamic" ValidationGroup="AddUsers"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="field-title">Tel</div>
                                            <div class="field-control-last">
                                                <asp:TextBox ID="txbMobile" Width="200" placeholder="Tel" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="field-title">Role</div>
                                            <div class="field-control-last">
                                                <asp:DropDownList ID="ddlRoleName" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvRoleName" runat="server" ErrorMessage="Role Name is required"
                                                    ControlToValidate="ddlRoleName" Display="Dynamic" ValidationGroup="AddUsers"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clear"></div>
                                            <div class="field-title">&nbsp;</div>
                                            <div class="field-control-last">
                                                <asp:Button ID="btnAddUsers" CssClass="file-uploaded-button" runat="server" Text="Add" OnClick="btnAddUsers_Click" />
                                            </div>
                                            <div class="grid_24 error_msg">
                                                <asp:Label ID="lbAddUserMessage" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="small-column-left-2">
                                            <div class="">
                                                <strong>Note: </strong>The usernames must be the trust email address of each user as the system will be sending an email to each user with a link and their login credentials. 
                                            </div>
                                        </div>
                                        <div class="small-column-right-2">
                                            <asp:GridView ID="UserListGridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="UserListGridView_RowDataBound" PageSize="4">
                                                <Columns>
                                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                                                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                                                    <asp:BoundField DataField="RoleId" HeaderText="Role" />
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings PageButtonCount="4" />
                                                <PagerStyle CssClass="wizard-user-list-pagging" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <RowStyle ForeColor="#000066" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </section>

                        <h2>4</h2>
                        <section>
                            <div class="page-center-highlight">
                                <h1>Congratulations!</h1>
                                <p>
                                    You are successfully setup the NHS KPI tool. 
                                We have sent you an email link for you to login to the NHS KPI tool. 
                                You must use this link to logon to the system first time. 
                                After the first login you will be able to login to the system 
                                using the sign in option in the hope page.  
                                </p>
                                <div class="page-center-button-group">
                                    <asp:Button ID="btnViewDemo" CssClass="file-uploaded-button" Text="View Demo" runat="server" />
                                    <asp:Button ID="btnViewUserManual" CssClass="file-uploaded-button" Text="View User Manual" runat="server" />
                                    <div class="page-center-button-right">
                                    </div>
                                </div>
                            </div>
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
