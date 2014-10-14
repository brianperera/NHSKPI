<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Comment.aspx.cs" Inherits="Views_User_Comment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../assets/scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../../assets/scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <script src="../../assets/scripts/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../assets/scripts/jquery.simpletip-1.3.1.min.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <%--  <title></title>--%>
    <meta charset="utf-8" />
    <title>NHS</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="../../assets/images/favicon.ico" type="image/x-icon" rel="icon" />
    <link href="../../assets/images/favicon.ico" type="image/x-icon" rel="shortcut icon" />
    <link href="../../assets/css/redmond/jquery-ui-1.10.3.custom.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../../assets/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/responsive.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../assets/plugins/font-awesome/css/font-awesome.min.css" />
    <link href="../../assets/css/modern-menu.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/comment.js" type="text/javascript"></script>
    <script>
        $(function () {
            $(document).tooltip();
        });
    </script>
    <style>
        label
        {
            display: inline-block;
            width: 5em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="divComments">
            <div class="container_96">
                <div class="container_24">
                    <div class="grid_24">
                        <div class="page-header">
                            <h1>
                                KPI Comments
                            </h1>
                        </div>
                        <div class="grid_24 error_msg">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="grid_6">
                            <asp:Label ID="lblKPINumber" runat="server" Text="KPI Number:"></asp:Label>
                        </div>
                        <div class="grid_17">
                            <asp:Label ID="lblKPINumberformat" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="grid_6">
                            <asp:Label ID="lblKPIName" runat="server" Text="KPI Name:"></asp:Label>
                        </div>
                        <div class="grid_17">
                            <asp:Label ID="lblKPINameFormat" runat="server" Text=""></asp:Label></div>
                        <div class="clear">
                        </div>
                        <div class="grid_6 hidden">
                            <asp:Label ID="lblCommentsType" runat="server" Text="Comment Type:"></asp:Label>
                        </div>
                        <div class="grid_17 hidden">
                            <asp:DropDownList ID="ddlCommentsType" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="grid_17">
                            <asp:Label ID="lblComment" runat="server" Text="Add your comments in the below text box:"></asp:Label>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="grid_23">
                            <asp:TextBox ID="txtComments" runat="server" Height="120px" TextMode="MultiLine"
                                MaxLength="500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvComments" runat="server" ErrorMessage="Comment is required"
                                ControlToValidate="txtComments" Display="Dynamic" ValidationGroup="Gentral"></asp:RequiredFieldValidator></div>
                        <div class="clear">
                        </div>
                        <div class="grid_3 ">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                ValidationGroup="Gentral" />
                        </div>
                        <div class="clear Hgap10">
                        </div>
                        <div class="grid_3">
                            <asp:Label ID="lblUser" runat="server" Text="User:"></asp:Label>
                        </div>
                        <div class="grid_6">
                            <asp:DropDownList ID="ddlUser" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="grid_6">
                            <asp:Label ID="lblCreatedDate" runat="server" Text="Created Date:"></asp:Label>
                        </div>
                        <div class="grid_8">
                            <input type="text" id="txtCreatedDate" runat="server" />
                            <div class="clear">
                            </div>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Created Date is Invalid!"
                                ControlToValidate="txtCreatedDate" Display="Dynamic" ValidationExpression="^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$"
                                ValidationGroup="Gentrals"></asp:RegularExpressionValidator>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="grid_3 ">
                            <asp:Button ID="btnSearchComment" runat="server" Text="Search" OnClick="btnSearchComment_Click"
                                ValidationGroup="Gentrals" />
                        </div>
                        <div class="clear">
                        </div>
                        <asp:HiddenField ID="hfComments" runat="server" />
                        <asp:Label ID="lblCommentsHistory" runat="server" Text="Comments History:"></asp:Label>
                        <div class="clear">
                        </div>
                        <asp:GridView CssClass="grid" ID="gvCommentsHistory" runat="server" AutoGenerateColumns="False"
                            AllowSorting="True" OnRowDataBound="gvCommentsHistory_RowDataBound" OnSorting="gvCommentsHistory_Sorting"
                            OnRowDeleting="gvCommentsHistory_RowDeleting" DataKeyNames="Id,CreatedBy">
                            <EmptyDataTemplate>
                                <div class="center bold error grid_empty">
                                    No Results found.</div>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" SortExpression="CreatedDate"
                                    DataFormatString="{0:dd-MM-yyyy hh:mm tt}" HtmlEncode="False" />
                                <asp:BoundField DataField="FirstName" HeaderText="Created By" SortExpression="FirstName" />
                                <asp:BoundField DataField="Comments" HeaderText="Comments" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkDelete" CommandArgument='<%# Eval("Id") %>' CommandName="Delete"
                                            runat="server">
                                            Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
