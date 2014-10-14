<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="SpecialtyLevelTarget.aspx.cs" Inherits="Views_KPI_SpecialtyLevelTarget"
    Title="NHS KPI Data Entry Portal" %>

<%@ MasterType VirtualPath="~/Views/Shared/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../scripts/specialtyleveltarget.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <input id="hdnUserType" type="hidden" runat="server" />
            <div class="grid_24">
                <div class="page-header position-relative">
                    <h1>
                        Specialty Level Target
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
                    <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/assets/images/r_arrow.png"
                        OnClick="imgBtnNext_Click" /></div>
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
                    <asp:DropDownList ID="ddlKPI" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlKPI_SelectedIndexChanged">
                    </asp:DropDownList>
                    <div class="clear">
                    </div>
                    <div class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="rfKPI" runat="server" Font-Bold="True" ValidationGroup="specialtyLevel"
                        Font-Names="10" ControlToValidate="ddlKPI" ForeColor="Red" ErrorMessage="KPI is required"
                        Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                </div>
                <div class="grid_3">
                    <label>
                        Specialty:
                    </label>
                </div>
                <div class="grid_5">
                    <asp:DropDownList ID="ddlSpecialty" runat="server" OnSelectedIndexChanged="ddlSpecialty_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <div class="clear">
                    </div>
                    <div class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="rfWard" runat="server" Font-Bold="True" ValidationGroup="specialtyLevel"
                        Font-Names="10" ControlToValidate="ddlSpecialty" ForeColor="Red" ErrorMessage="Specialty is required"
                        Display="Dynamic"></asp:RequiredFieldValidator>
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
                                onclick="return showhide('monthlyMinus','monthlyPlus','monthlyDynamic','plus');" />
                            <img id="monthlyPlus" runat="server" style="display: none" alt="" src="../../assets/images/plus.png"
                                onclick="return showhide('monthlyMinus','monthlyPlus','monthlyDynamic','minus');" /></a>
                    </div>
                    <div class="grid_12">
                        <h3 class="titl_pd">
                            Monthly Dynamic Target</h3>
                    </div>
                </div>
                <div class="clear">
                </div>
                <div class="Hgap15">
                </div>
                <div id="monthlyDynamic" runat="server">
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
                    <div class="grid_24">
                        <div class="grid_3">
                            <label>
                                Target Description</label>
                        </div>
                        <div class="grid_20">
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionApril" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionMay" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionJune" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionJuly" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionAug" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionSep" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionOct" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionNov" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionDec" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionJan" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionFeb" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionMarch" runat="server" MaxLength="50"></asp:TextBox></div>
                        </div>
                    </div>
                    <div class="grid_24">
                        <div class="grid_3">
                            <label>
                                Target Green
                            </label>
                        </div>
                        <div class="grid_20">
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenApril" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenApril" runat="server" ControlToValidate="txtGreenApril"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenMay" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenMay" runat="server" ControlToValidate="txtGreenMay"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenJune" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenJune" runat="server" ControlToValidate="txtGreenJune"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenJuly" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenJuly" runat="server" ControlToValidate="txtGreenJuly"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenAug" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenAug" runat="server" ControlToValidate="txtGreenAug"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenSep" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenSep" runat="server" ControlToValidate="txtGreenSep"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenOct" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenOct" runat="server" ControlToValidate="txtGreenOct"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenNov" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenNov" runat="server" ControlToValidate="txtGreenNov"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenDec" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenDec" runat="server" ControlToValidate="txtGreenDec"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenJan" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenJan" runat="server" ControlToValidate="txtGreenJan"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenFeb" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenFeb" runat="server" ControlToValidate="txtGreenFeb"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenMarch" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenMarch" runat="server" ControlToValidate="txtGreenMarch"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                        </div>
                    </div>
                    <div class="grid_24">
                        <div class="grid_3">
                            <label>
                                Target Amber
                            </label>
                        </div>
                        <div class="grid_20">
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberApril" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberApril" runat="server" ControlToValidate="txtAmberApril"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberMay" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberMay" runat="server" ControlToValidate="txtAmberMay"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberJune" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberJune" runat="server" ControlToValidate="txtAmberJune"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberJuly" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberJuly" runat="server" ControlToValidate="txtAmberJuly"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberAug" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberAug" runat="server" ControlToValidate="txtAmberAug"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberSep" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberSep" runat="server" ControlToValidate="txtAmberSep"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberOct" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberOct" runat="server" ControlToValidate="txtAmberOct"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberNov" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberNov" runat="server" ControlToValidate="txtAmberNov"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberDec" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberDec" runat="server" ControlToValidate="txtAmberDec"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberJan" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberJan" runat="server" ControlToValidate="txtAmberJan"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberFeb" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberFeb" runat="server" ControlToValidate="txtAmberFeb"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberMarch" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberMarch" runat="server" ControlToValidate="txtAmberMarch"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
                <div class="Hgap15">
                </div>
                <div class="grid_24">
                    <div class="grid_1">
                        <a>
                            <img id="ytdMinus" runat="server" alt="" src="../../assets/images/minus.png" onclick="return showhide('ytdMinus','ytdPlus','ytdDynamic','plus');" />
                            <img id="ytdPlus" runat="server" style="display: none" alt="" src="../../assets/images/plus.png"
                                onclick="return showhide('ytdMinus','ytdPlus','ytdDynamic','minus');" /></a>
                    </div>
                    <div class="grid_12">
                        <h3 class="titl_pd">
                            YTD Dynamic Target</h3>
                    </div>
                </div>
                <div class="clear">
                </div>
                <div class="Hgap15">
                </div>
                <div id="ytdDynamic" runat="server">
                    <div class="grid_24">
                        <div class="grid_3">
                            <label>
                            </label>
                        </div>
                        <div class="grid_20">
                            <div class="grid_2">
                                <asp:Label ID="Label13" runat="server" Text="Apr"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label14" runat="server" Text="May"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label15" runat="server" Text="Jun"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label16" runat="server" Text="Jul"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label17" runat="server" Text="Aug"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label18" runat="server" Text="Sep"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label19" runat="server" Text="Oct"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label20" runat="server" Text="Nov"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label21" runat="server" Text="Dec"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label22" runat="server" Text="Jan"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label23" runat="server" Text="Feb"></asp:Label></div>
                            <div class="grid_2">
                                <asp:Label ID="Label24" runat="server" Text="Mar"></asp:Label></div>
                        </div>
                    </div>
                    <div class="grid_24">
                        <div class="grid_3">
                            <label>
                                Target Description</label>
                        </div>
                        <div class="grid_20">
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionAprilYTD" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionMayYTD" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionJuneYTD" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionJulyYTD" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionAugYTD" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionSepYTD" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionOctYTD" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionNovYTD" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionDecYTD" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionJanYTD" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionFebYTD" runat="server" MaxLength="50"></asp:TextBox></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtDescriptionMarchYTD" runat="server" MaxLength="50"></asp:TextBox></div>
                        </div>
                    </div>
                    <div class="grid_24">
                        <div class="grid_3">
                            <label>
                                Target Green
                            </label>
                        </div>
                        <div class="grid_20">
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenAprilYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenAprilYTD" runat="server" ControlToValidate="txtGreenAprilYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenMayYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenMayYTD" runat="server" ControlToValidate="txtGreenMayYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenJuneYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenJuneYTD" runat="server" ControlToValidate="txtGreenJuneYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenJulyYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenJulyYTD" runat="server" ControlToValidate="txtGreenJulyYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenAugYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenAugYTD" runat="server" ControlToValidate="txtGreenAugYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenSepYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenSepYTD" runat="server" ControlToValidate="txtGreenSepYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenOctYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenOctYTD" runat="server" ControlToValidate="txtGreenOctYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenNovYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenNovYTD" runat="server" ControlToValidate="txtGreenNovYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenDecYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenDecYTD" runat="server" ControlToValidate="txtGreenDecYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenJanYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenJanYTD" runat="server" ControlToValidate="txtGreenJanYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenFebYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenFebYTD" runat="server" ControlToValidate="txtGreenFebYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtGreenMarchYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revGreenMarchYTD" runat="server" ControlToValidate="txtGreenMarchYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                        </div>
                    </div>
                    <div class="grid_24">
                        <div class="grid_3">
                            <label>
                                Target Amber
                            </label>
                        </div>
                        <div class="grid_20">
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberAprilYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberAprilYTD" runat="server" ControlToValidate="txtAmberAprilYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberMayYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberMayYTD" runat="server" ControlToValidate="txtAmberMayYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberJuneYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberJuneYTD" runat="server" ControlToValidate="txtAmberJuneYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberJulyYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberJulyYTD" runat="server" ControlToValidate="txtAmberJulyYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberAugYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberAugYTD" runat="server" ControlToValidate="txtAmberAugYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberSepYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberSepYTD" runat="server" ControlToValidate="txtAmberSepYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberOctYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberOctYTD" runat="server" ControlToValidate="txtAmberOctYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberNovYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberNovYTD" runat="server" ControlToValidate="txtAmberNovYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberDecYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberDecYTD" runat="server" ControlToValidate="txtAmberDecYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberJanYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberJanYTD" runat="server" ControlToValidate="txtAmberJanYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberFebYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberFebYTD" runat="server" ControlToValidate="txtAmberFebYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                            <div class="grid_2">
                                <asp:TextBox ID="txtAmberMarchYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revAmberMarchYTD" runat="server" ControlToValidate="txtAmberMarchYTD"
                                    Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                    ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="Hgap15">
            </div>
            <div id="divStaticTarget" runat="server">
                <div class="grid_24">
                    <div class="grid_1">
                        <a class="btn btn-xs btn-primary" id="btnstatichide"><i class="icon-minus"></i></a>
                        <a class="btn btn-xs btn-primary" id="btnstaticshow" style="display: none;"><i class="icon-plus">
                        </i></a>
                    </div>
                    <div class="grid_12">
                        <h3 class="titl_pd">
                            Monthly Static Target</h3>
                    </div>
                </div>
                <div class="clear">
                </div>
                <div class="Hgap15">
                </div>
                <div id="Static">
                    <div class="grid_24">
                        <div class="grid_3">
                            <label>
                                Target Description
                            </label>
                        </div>
                        <div class="grid_5">
                            <asp:TextBox ID="txtStaticTargetDescription" runat="server" MaxLength="50"></asp:TextBox>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="grid_3">
                        </div>
                    </div>
                    <div class="grid_24">
                        <div class="grid_3">
                            <label>
                                Target Green
                            </label>
                        </div>
                        <div class="grid_2">
                            <asp:TextBox ID="txtStaticTargetGreen" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revStaticTargetGreen" runat="server" ControlToValidate="txtStaticTargetGreen"
                                Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                    </div>
                    <div class="grid_24">
                        <div class="grid_3">
                            <label>
                                Target Amber
                            </label>
                        </div>
                        <div class="grid_2">
                            <asp:TextBox ID="txtStaticTargetAmber" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revStaticTargetAmber" runat="server" ControlToValidate="txtStaticTargetAmber"
                                Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                    </div>
                </div>
                <div class="clear">
                </div>
                <div class="Hgap30">
                </div>
                <div class="grid_24">
                    <div class="grid_1">
                        <a class="btn btn-xs btn-primary" id="A3"><i class="icon-minus"></i></a><a class="btn btn-xs btn-primary"
                            id="A4" style="display: none;"><i class="icon-plus"></i></a>
                    </div>
                    <div class="grid_12">
                        <div class="grid_5">
                            <h3 class="titl_pd">
                                YTD Static Target</h3>
                        </div>
                        <div class="grid_2">
                            <input class="" id="btnCopyMonthly" type="button" runat="server" value="Copy Monthly"
                                visible="True" />
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
                <div class="Hgap15">
                </div>
                <div id="Div2">
                    <div class="grid_24">
                        <div class="grid_3">
                        </div>
                        <div class="grid_1">
                        </div>
                    </div>
                    <div class="grid_24">
                        <div class="grid_3">
                            <label>
                                Target Description
                            </label>
                        </div>
                        <div class="grid_5">
                            <asp:TextBox ID="txtStaticTargetDescriptionYTD" runat="server" MaxLength="50"></asp:TextBox>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="grid_3">
                        </div>
                    </div>
                    <div class="grid_24">
                        <div class="grid_3">
                            <label>
                                Target Green
                            </label>
                        </div>
                        <div class="grid_2">
                            <asp:TextBox ID="txtStaticTargetGreenYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revStaticTargetGreenYTD" runat="server" ControlToValidate="txtStaticTargetGreenYTD"
                                Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                    </div>
                    <div class="grid_24">
                        <div class="grid_3">
                            <label>
                                Target Amber
                            </label>
                        </div>
                        <div class="grid_2">
                            <asp:TextBox ID="txtStaticTargetAmberYTD" runat="server" MaxLength="5" onkeypress="return isDecimalKey(event);"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revStaticTargetAmberYTD" runat="server" ControlToValidate="txtStaticTargetAmberYTD"
                                Display="Dynamic" ErrorMessage="Invalid." ValidationExpression="^\d{1,8}(\.\d{1,2})?$"
                                ValidationGroup="specialtyLevel"></asp:RegularExpressionValidator></div>
                    </div>
                </div>
            </div>
            <div class="grid_24" id="divButtons" runat="server">
                <div class="clear">
                </div>
                <div class="Hgap15">
                </div>
                <div class="grid_16">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Visible="True" OnClick="btnSave_Click"
                        ValidationGroup="specialtyLevel" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="True" OnClick="btnUpdate_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
