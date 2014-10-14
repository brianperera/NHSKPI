<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="KPIUpdate.aspx.cs" Inherits="Views_KPI_KPIUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            KPI Details
        </h1>
    </div>
    <div class="grid_24 error_msg">
        <asp:Label ID="lblAddMessage" runat="server" Text=""></asp:Label>
    </div>
    <div class="clear">
    </div>
    <div class="grid_11 well">
        <div class="clear Hgap20">
        </div>
        <div class="grid_9">
            <asp:Label ID="lblKPINo" runat="server" Text="KPI No :"></asp:Label>
        </div>
        <div class="grid_8">
            <asp:TextBox ID="txtKPINo" runat="server" MaxLength="50"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvtxtKPINo" runat="server" ErrorMessage="KPI No is required"
                ControlToValidate="txtKPINo" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtKPINo"
                ErrorMessage="Invalid KPI No" ValidationExpression="[0-9]+" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_9">
            <asp:Label ID="lblKPIDescription" runat="server" Text="KPI Description :"></asp:Label>
        </div>
        <div class="grid_8">
            <asp:TextBox ID="txtKPIDescription" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvKPIDescription" runat="server" ControlToValidate="txtKPIDescription"
                ErrorMessage="KPI Description is required" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_9">
            <asp:Label ID="lblKPIGroupName" runat="server" Text="KPI Group Name :"></asp:Label>
        </div>
        <div class="grid_8">
            <asp:DropDownList ID="ddlKPIGroupName" runat="server">
            </asp:DropDownList>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvKPIGroupName" runat="server" ErrorMessage="KPI Group Name is required"
                ControlToValidate="ddlKPIGroupName" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_9">
            <asp:Label ID="lblTargetApplyFor" runat="server" Text="Target Apply For :"></asp:Label>
        </div>
        <div class="grid_8">
            <asp:DropDownList ID="ddlTargetApplyFor" runat="server">
            </asp:DropDownList>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvTargetApplyFor" runat="server" ErrorMessage="Target Apply For is required"
                ControlToValidate="ddlTargetApplyFor" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="clear">
        </div>
        <div class="grid_9">
            <asp:Label ID="lblWeight" runat="server" Text="Weight :"></asp:Label>
        </div>
        <div class="grid_8">
            <asp:TextBox ID="txtWeight" runat="server"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RegularExpressionValidator ID="revWeight" runat="server" ControlToValidate="txtWeight"
                ErrorMessage="Invalid weight" ValidationExpression="[0-9]+" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_9">
            <asp:Label ID="lblDisplayOrder" runat="server" Text="Display Order :"></asp:Label>
        </div>
        <div class="grid_8">
            <asp:TextBox ID="txtDisplayOrder" runat="server"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RegularExpressionValidator ID="revDisplayOrder" runat="server" ControlToValidate="txtDisplayOrder"
                ErrorMessage="Invalid display order" ValidationExpression="[0-9]+" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_9">
            <asp:Label ID="lblFormatCode" runat="server" Text="Format Code :"></asp:Label>
        </div>
        <div class="grid_8">
            <asp:TextBox ID="txtFormatCode" runat="server" MaxLength="20"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvFormatCode" runat="server" ErrorMessage="Format Code is required"
                ControlToValidate="txtFormatCode" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revFormatCode" runat="server" ControlToValidate="txtFormatCode"
                Display="Dynamic" ErrorMessage="Format Code is invalid Eg 0/#.#/#.0%/##,000"
                ValidationExpression="[0]*[//]*[#]*[.]*[#]*[//]*[#]*[.]*[0]*[%]*[//]*[#]*[#]*[,]*[0]*[0]*[0]*"></asp:RegularExpressionValidator>
        </div>
        <div class="grid_7">
            <label class="eg_text">
                Eg: 0/#.#/#.0%/##,000</label></div>
        <div class="clear">
        </div>
        <div class="grid_9">
            <asp:Label ID="lblThresholdDetails" runat="server" Text="Threshold Details :"></asp:Label>
        </div>
        <div class="grid_8">
            <asp:TextBox ID="txtThresholdDetails" runat="server" MaxLength="50"></asp:TextBox>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_9">
            <asp:Label ID="lblIndicatorLead" runat="server" Text="Indicator Lead :"></asp:Label>
        </div>
        <div class="grid_8">
            <asp:TextBox ID="txtIndicatorLead" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_9">
            <asp:Label ID="lblCommentsLead" runat="server" Text="Comments Lead :"></asp:Label>
        </div>
        <div class="grid_8">
            <asp:TextBox ID="txtCommentsLead" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_9">
            <asp:Label ID="lblResponsibleDivision" runat="server" Text="Responsible Division :"></asp:Label>
        </div>
        <div class="grid_8">
            <asp:TextBox ID="txtResponsibleDivision" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8 prefix_9">
            <asp:Button ID="btnSave" runat="server" Text="Create KPI" OnClick="btnSave_Click"
                Visible="False" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                Visible="False" />
        </div>
        <div class="clear Hgap20">
        </div>
    </div>
    <div class="grid_10 well push_1">
        <div class="clear Hgap20">
        </div>
        <div class="grid_12">
            <h3>
                <label>
                    Setting KPI Flags</label></h3>
        </div>
        <div class="clear Hgap10">
        </div>
        <div class="grid_11">
            <asp:Label ID="lblStaticTarget" runat="server" Text="Static Target :"></asp:Label>
        </div>
        <div class="grid_10">
            <asp:CheckBox ID="chkStaticTarget" runat="server" Checked="True" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_11">
            <asp:Label ID="lblNumeratorOnlyFlag" runat="server" Text="Numerator Only Flag :"></asp:Label>
        </div>
        <div class="grid_11">
            <asp:CheckBox ID="chkNumeratorOnlyFlag" runat="server" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_11">
            <asp:Label ID="lblRangeTarget" runat="server" Text="Range Target :"></asp:Label>
        </div>
        <div class="grid_11">
            <asp:CheckBox ID="chkRangeTarget" runat="server" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_11">
            <asp:Label ID="LBLHigherTheBetterFlag" runat="server" Text="Higher The Better Flag :"></asp:Label>
        </div>
        <div class="grid_10">
            <asp:CheckBox ID="chkHigherTheBetterFlag" runat="server" Checked="True" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_11">
            <asp:Label ID="lblSeparateYTDFigure" runat="server" Text="Separate YTD Figure :"></asp:Label>
        </div>
        <div class="grid_11">
            <asp:CheckBox ID="chkSeparateYTDFigure" runat="server" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_11">
            <asp:Label ID="lblAverageYTDFigure" runat="server" Text="Average YTD Figure :"></asp:Label>
        </div>
        <div class="grid_11">
            <asp:CheckBox ID="chkAverageYTDFigure" runat="server" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_11">
            <asp:Label ID="lblCanSummariesFlag" runat="server" Text="Summarise :"></asp:Label>
        </div>
        <div class="grid_11">
            <asp:CheckBox ID="chkCanSummariesFlag" runat="server" Checked="True" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_11">
            <asp:Label ID="lblManuallyEntered" runat="server" Text="Manually Entered :"></asp:Label>
        </div>
        <div class="grid_11">
            <asp:CheckBox ID="chkManuallyEntered" runat="server" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_11">
            <asp:Label ID="lblVisibility" runat="server" Text="Visibility :"></asp:Label>
        </div>
        <div class="grid_11">
            <asp:CheckBox ID="chkVisibility" runat="server" Checked="True" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_11">
            <asp:Label ID="lblIsActive" runat="server" Text="Is Active :"></asp:Label>
        </div>
        <div class="grid_11">
            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" />
        </div>
        <div class="clear Hgap20">
        </div>
    </div>
    <div class="grid_10 well push_1">
        <div class="grid_11">
            <asp:Label ID="Label1" runat="server" Text="Numerator Description:"></asp:Label>
        </div>
        <div class="grid_10">
            <asp:TextBox ID="txtNumeratorDescription" runat="server" TextMode="multiline" 
                Rows="1" Columns="5" MaxLength="250"></asp:TextBox>
        </div>
        <div class="clear">
        </div>
        <div class="grid_11">
            <asp:Label ID="Label2" runat="server" Text="Denominator Description:"></asp:Label>
        </div>
        <div class="grid_10">
            <asp:TextBox ID="txtDenominatorDescription" runat="server" TextMode="multiline" 
                Rows="1" Columns="5" MaxLength="250"></asp:TextBox>
        </div>
        <div class="clear">
        </div>
        <div class="grid_11">
            <asp:Label ID="Label3" runat="server" Text="YTD Value Description:"></asp:Label>
        </div>
        <div class="grid_10">
            <asp:TextBox ID="txtYTDValueDescription" runat="server" TextMode="multiline" 
                Rows="1" Columns="5" MaxLength="250"></asp:TextBox>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
