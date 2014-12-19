<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecialtyBulkUploader.ascx.cs" Inherits="Views_Shared_SpecialtyBulkUploader" %>
<div class="field-title-long">
    <asp:FileUpload ID="fuSpecialtyDataUpload" runat="server" CssClass="" />
</div>
<div class="grid_24 error_msg">
    <asp:Label ID="AddSpecialtyDataMessage" runat="server" Text=""></asp:Label>
</div>
<div class="field-control-last">
    <asp:Button ID="btnSpecialtyDataUpload" CssClass="file-uploaded-button" runat="server" Text="Upload" OnClick="btnSpecialtyDataUpload_Click" />
<%--<asp:RegularExpressionValidator ID="fuSpecialtyDataUploadRegularExpressionValidator" ControlToValidate="fuSpecialtyDataUpload"
        runat="server" ErrorMessage="Only CSV files are allowed" ValidationExpression="(.*?)\.(csv|CSV)$"></asp:RegularExpressionValidator>--%>
</div>
