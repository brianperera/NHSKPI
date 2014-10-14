<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="KPIDetailUpdate.aspx.cs" Inherits="Views_KPI_KPIDetailUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            KPI Info Details
        </h1>
    </div>
    <div class="grid_24 error_msg">
            <asp:Label ID="lblAddMessage" runat="server" Text=""></asp:Label>
        </div>
        <div class="clear">
        </div>
    <div class="grid_12">
        
        <div class="grid_8">
            <asp:Label ID="lblKPIId" runat="server" Text="KPI Name :"></asp:Label>
        </div>
        <div class="grid_15">
            <asp:Label ID="lblKPIName" runat="server" Text=""></asp:Label>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblWeight" runat="server" Text="Weight :"></asp:Label>
        </div>
        <div class="grid_15">
            <asp:TextBox ID="txtWeight" runat="server"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvWeight" runat="server" ErrorMessage="Weight is required"
                ControlToValidate="txtWeight" Display="Dynamic"></asp:RequiredFieldValidator>
            &nbsp;<asp:RegularExpressionValidator ID="revWeight" runat="server" ControlToValidate="txtWeight"
                ErrorMessage="Invalid weight" ValidationExpression="[0-9]+" Display="Dynamic"></asp:RegularExpressionValidator>
            &nbsp;</div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblDisplayOrder" runat="server" Text="DisplayOrder :"></asp:Label>
        </div>
        <div class="grid_15">
            <asp:TextBox ID="txtDisplayOrder" runat="server"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvDisplayOrder" runat="server" ErrorMessage="Display Order is required"
                ControlToValidate="txtDisplayOrder" Display="Dynamic"></asp:RequiredFieldValidator>
            &nbsp;<asp:RegularExpressionValidator ID="revDisplayOrder" runat="server" ControlToValidate="txtDisplayOrder"
                ErrorMessage="Invalid display order" ValidationExpression="[0-9]+" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblFormatCode" runat="server" Text="Format Code :"></asp:Label>
        </div>
        <div class="grid_15">
            <asp:TextBox ID="txtFormatCode" runat="server" MaxLength="20"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvFormatCode" runat="server" ErrorMessage="Format Code is required"
                ControlToValidate="txtFormatCode" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtFormatCode" Display="Dynamic" 
                ErrorMessage="Format Code is invalid" 
                ValidationExpression="0[//][0-9][.][0-9][//][0-9][.]0[%][//][0-9][0-9][,]000"></asp:RegularExpressionValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblThresholdDetails" runat="server" Text="Threshold Details :"></asp:Label>
        </div>
        <div class="grid_15">
            <asp:TextBox ID="txtThresholdDetails" runat="server" MaxLength="50"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvThresholdDetails" runat="server" ErrorMessage="Threshold Details is required"
                ControlToValidate="txtThresholdDetails" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblIndicatorLead" runat="server" Text="Indicator Lead :"></asp:Label>
        </div>
        <div class="grid_15">
            <asp:TextBox ID="txtIndicatorLead" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvIndicatorLead" runat="server" ErrorMessage="IndicatorLead is required"
                ControlToValidate="txtIndicatorLead" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblCommentsLead" runat="server" Text="Comments Lead :"></asp:Label>
        </div>
        <div class="grid_15">
            <asp:TextBox ID="txtCommentsLead" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvCommentsLead" runat="server" ErrorMessage="Comments Lead is required"
                ControlToValidate="txtCommentsLead" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblResponsibleDivision" runat="server" Text="Responsible Division :"></asp:Label>
        </div>
        <div class="grid_15">
            <asp:TextBox ID="txtResponsibleDivision" runat="server" MaxLength="100"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:RequiredFieldValidator ID="rfvResponsibleDivision" runat="server" ErrorMessage="Responsible Division is required"
                ControlToValidate="txtResponsibleDivision" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="clear">
        </div>
        <div class="grid_16 prefix_8">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="False" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                Visible="False" />
        </div>
        <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    </div>
    <div class="grid_12">
        <div class="grid_8">
            <asp:Label ID="lblRangeTarget" runat="server" Text="Range Target :"></asp:Label>
        </div>
        <div class="grid_2">
            <asp:CheckBox ID="chkRangeTarget" runat="server" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="LBLHigherTheBetterFlag" runat="server" Text="Higher The Better Flag :"></asp:Label>
        </div>
        <div class="grid_2">
            <asp:CheckBox ID="chkHigherTheBetterFlag" runat="server" Checked="True" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblSeparateYTDFigure" runat="server" Text="Separate YTD Figure :"></asp:Label>
        </div>
        <div class="grid_2">
            <asp:CheckBox ID="chkSeparateYTDFigure" runat="server" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblAverageYTDFigure" runat="server" Text="Average YTD Figure :"></asp:Label>
        </div>
        <div class="grid_2">
            <asp:CheckBox ID="chkAverageYTDFigure" runat="server" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblCanSummariesFlag" runat="server" Text="Can Summaries Flag :"></asp:Label>
        </div>
        <div class="grid_2">
            <asp:CheckBox ID="chkCanSummariesFlag" runat="server" Checked="True" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblManuallyEntered" runat="server" Text="Manually Entered :"></asp:Label>
        </div>
        <div class="grid_2">
            <asp:CheckBox ID="chkManuallyEntered" runat="server" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblVisibility" runat="server" Text="Visibility :"></asp:Label>
        </div>
        <div class="grid_2">
            <asp:CheckBox ID="chkVisibility" runat="server" Checked="True" />
        </div>
    </div>
</asp:Content>
