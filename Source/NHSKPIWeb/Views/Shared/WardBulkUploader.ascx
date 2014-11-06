<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WardBulkUploader.ascx.cs" Inherits="Views_Shared_WardBulkUploader" %>
<div class="field-title-long">
    <asp:FileUpload ID="fuWardDataUpload" runat="server" CssClass="" />
</div>
<div class="grid_24 error_msg">
    <asp:Label ID="lblAddWardDataMessage" runat="server" Text=""></asp:Label>
</div>
<div class="field-control-last">
    <asp:Button ID="btnUploadWardFile" CssClass="file-uploaded-button" runat="server" Text="Upload" OnClick="btnUploadWardFile_Click" />
    <asp:RegularExpressionValidator ID="fuWardDataUploadRegularExpressionValidator" ControlToValidate="fuWardDataUpload"
        runat="server" ErrorMessage="Only CSV files are allowed" ValidationExpression="(.*?)\.(csv|CSV)$"></asp:RegularExpressionValidator>
</div>
