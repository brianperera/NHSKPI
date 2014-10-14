<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="WardLevelTargetData.aspx.cs" Inherits="Views_KPI_WardLevelTargetData"
    Title="NHS KPI Data Entry Portal" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../scripts/wardleveldata.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function popUp(width, height, tras, url) {
            testwindow = window.open(url, tras, 'width=700,height=550,scrollbars=yes,menubar=no,addressbar=no,titlebar=no,toolbar=no');
            testwindow.focus();


        }          
            
            
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <input id="hdnUserType" type="hidden" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="grid_24">
                <div class="page-header position-relative">
                    <h1>
                        Ward Level Data
                    </h1>
                </div>
            </div>
            <div class="Hgap15">
            </div>
            <div class="grid_24 error_msg">
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </div>
            <div class="Hgap15">
            </div>
            <div class="grid_24">
                <div class="grid_3">
                    <h3>
                        <label>
                            Financial Year:</label>
                    </h3>
                </div>
                <div class="grid_1">
                    <asp:ImageButton ID="imgBtnPrevoius" runat="server" OnClick="imgBtnPrevoius_Click"
                        ImageUrl="~/assets/images/l_arrow.png" /></div>
                <div class="grid_2 center">
                    <h3>
                        <asp:Label ID="lblCurentFinancialYear" runat="server"></asp:Label></h3>
                </div>
                <div class="grid_1 right">
                    <asp:ImageButton ID="imgBtnNext" runat="server" OnClick="imgBtnNext_Click" ImageUrl="~/assets/images/r_arrow.png" /></div>
            </div>
            <div class="clear">
            </div>
            <div class="Hgap15">
            </div>
            <div class="grid_24">
                <div class="grid_3">
                    <label>
                        KPI Description:
                    </label>
                </div>
                <div class="grid_6">
                    <asp:DropDownList ID="ddlKPI" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlKPI_SelectedIndexChanged"
                        ValidationGroup="wardLevel" CausesValidation="True">
                    </asp:DropDownList>
                    <asp:Button ID="btnComments" runat="server" Text="Comment" Visible="false" />
                    <div class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="rfKPI" runat="server" Font-Bold="True" ValidationGroup="wardLevel"
                        Font-Names="10" ControlToValidate="ddlKPI" ForeColor="Red" ErrorMessage="KPI is required"
                        Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                </div>
                <div class="grid_3">
                    <label>
                        Ward Name:
                    </label>
                </div>
                <div class="grid_5">
                    <asp:DropDownList ID="ddlWardName" runat="server" OnSelectedIndexChanged="ddlWardName_SelectedIndexChanged"
                        AutoPostBack="True" ValidationGroup="wardLevel" CausesValidation="True">
                    </asp:DropDownList>
                    <div class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="rfWard" runat="server" Font-Bold="True" ValidationGroup="wardLevel"
                        Font-Names="10" ControlToValidate="ddlWardName" ForeColor="Red" ErrorMessage="Ward Name is required"
                        Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="Hgap15">
            </div>
            <div id="divDynamicTarget" runat="server">
                <div class="grid_24">
                    <div class="grid_1">
                        <a>
                            <img id="monthlyMinus" runat="server" alt="" src="../../assets/images/minus.png"
                                onclick="return showhide('monthlyMinus','monthlyPlus','monthlyData','plus');" />
                            <img id="monthlyPlus" runat="server" style="display: none" alt="" src="../../assets/images/plus.png"
                                onclick="return showhide('monthlyMinus','monthlyPlus','monthlyData','minus');" /></a>
                    </div>
                    <div class="grid_12">
                        <h3 class="titl_pd">
                            Monthly Data</h3>
                    </div>
                </div>
                <div class="clear">
                </div>
                <div class="Hgap15">
                </div>
                <div id="monthlyData" runat="server">
                    <div class="grid_24">
                        <div class="grid_3">
                            <label>
                            </label>
                        </div>
                        <div class="grid_20">
                            <div class="grid_2">
                                <asp:Label ID="Label1" runat="server" Text="Apr"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label2" runat="server" Text="May"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label3" runat="server" Text="Jun"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label4" runat="server" Text="Jul"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label5" runat="server" Text="Aug"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label6" runat="server" Text="Sep"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label7" runat="server" Text="Oct"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label8" runat="server" Text="Nov"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label9" runat="server" Text="Dec"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label10" runat="server" Text="Jan"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label11" runat="server" Text="Feb"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label12" runat="server" Text="Mar"></asp:Label></div>
                        </div>
                    </div>
                    <div class="grid_24" id="divNumarator" runat="server">
                        <div class="grid_3">
                            <asp:Label ID="lblNumeratorDes" runat="server" Text="Numerator"></asp:Label>
                        </div>
                        <div class="grid_20">
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenApril" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenApril" runat="server" ControlToValidate="txtGreenApril"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenMay" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenMay" runat="server" ControlToValidate="txtGreenMay"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenJune" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenJune" runat="server" ControlToValidate="txtGreenJune"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenJuly" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenJuly" runat="server" ControlToValidate="txtGreenJuly"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenAug" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenAug" runat="server" ControlToValidate="txtGreenAug"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenSep" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenSep" runat="server" ControlToValidate="txtGreenSep"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenOct" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenOct" runat="server" ControlToValidate="txtGreenOct"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenNov" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenNov" runat="server" ControlToValidate="txtGreenNov"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenDec" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenDec" runat="server" ControlToValidate="txtGreenDec"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenJan" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenJan" runat="server" ControlToValidate="txtGreenJan"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenFeb" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenFeb" runat="server" ControlToValidate="txtGreenFeb"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenMarch" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenMarch" runat="server" ControlToValidate="txtGreenMarch"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                        </div>
                    </div>
                    <div class="grid_24" id="divDenominator" runat="server">
                        <div class="grid_3">
                            <asp:Label ID="lblDenominatorDes" runat="server" Text="Denominator"></asp:Label>
                        </div>
                        <div class="grid_20">
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberApril" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberApril" runat="server" ControlToValidate="txtAmberApril"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberMay" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberMay" runat="server" ControlToValidate="txtAmberMay"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberJune" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberJune" runat="server" ControlToValidate="txtAmberJune"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberJuly" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberJuly" runat="server" ControlToValidate="txtAmberJuly"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberAug" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberAug" runat="server" ControlToValidate="txtAmberAug"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberSep" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberSep" runat="server" ControlToValidate="txtAmberSep"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberOct" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberOct" runat="server" ControlToValidate="txtAmberOct"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberNov" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberNov" runat="server" ControlToValidate="txtAmberNov"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberDec" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberDec" runat="server" ControlToValidate="txtAmberDec"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberJan" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberJan" runat="server" ControlToValidate="txtAmberJan"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberFeb" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberFeb" runat="server" ControlToValidate="txtAmberFeb"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberMarch" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberMarch" runat="server" ControlToValidate="txtAmberMarch"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                        </div>
                    </div>
                    <div class="grid_24" id="divYTD" runat="server">
                        <div class="grid_3">
                            <asp:Label ID="lblYTDValueDes" runat="server" Text="YTD Value"></asp:Label>
                        </div>
                        <div class="grid_20">
                            <div class="grid_2">
                                <asp:TextBox ID="txtYTDApril" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revYTDApril" runat="server" ControlToValidate="txtYTDApril"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtYTDMay" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revYTDMay" runat="server" ControlToValidate="txtYTDMay"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtYTDJune" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revYTDJune" runat="server" ControlToValidate="txtYTDJune"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtYTDJuly" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revYTDJuly" runat="server" ControlToValidate="txtYTDJuly"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtYTDAug" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revYTDAug" runat="server" ControlToValidate="txtYTDAug"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtYTDSep" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revYTDSep" runat="server" ControlToValidate="txtYTDSep"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtYTDOct" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revYTDOct" runat="server" ControlToValidate="txtYTDOct"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtYTDNov" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revYTDNov" runat="server" ControlToValidate="txtYTDNov"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtYTDDec" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revYTDDec" runat="server" ControlToValidate="txtYTDDec"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtYTDJan" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revYTDJan" runat="server" ControlToValidate="txtYTDJan"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtYTDFeb" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revYTDFeb" runat="server" ControlToValidate="txtYTDFeb"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtYTDMarch" runat="server" MaxLength="10" onkeypress="return isDecimalKey(event);"
                                    ValidationGroup="wardLevel"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revYTDMarch" runat="server" ControlToValidate="txtYTDMarch"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="wardLevel"></asp:RegularExpressionValidator></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="Hgap15">
            </div>
            <div class="grid_24" id="divButtons" runat="server">
                <div class="clear">
                </div>
                <div class="Hgap15">
                </div>
                <div class="grid_16">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Visible="True" OnClick="btnSave_Click"
                        ValidationGroup="wardLevel" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="True" ValidationGroup="wardLevel"
                        OnClick="btnUpdate_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
