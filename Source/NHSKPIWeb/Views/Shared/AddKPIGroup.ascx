<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddKPIGroup.ascx.cs" Inherits="Views_Shared_AddKPIGroup" %>



        <div id="add-ward-popup" title="Add Ward Group">
        <div class="grid_12">
            <div class="grid_small_left">
                <asp:Label ID="lblKPIGroupName" runat="server" Text="KPI Group Name :"></asp:Label>
            </div>
            <div class="grid_small_right">
                <asp:TextBox ID="txtKPIGroupName" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="*" ControlToValidate="txtKPIGroupName" ValidationGroup="KPIGroup"></asp:RequiredFieldValidator>
            </div>
            <div class="clear">
            </div>
            <div class="grid_small_left">
                <asp:Label ID="Label5" runat="server" Text="Is Active :"></asp:Label>
            </div>
            <div class="grid_small_right">
                <asp:CheckBox ID="chbIsActive" runat="server" Checked="True" />
            </div>
            <div class="grid_24 error_msg">
                <asp:Label ID="lblAddKpiGroupMessage" runat="server"></asp:Label>
            </div>
            <div class="clear">
            </div>
            <div class="">
                <asp:Button ID="BtnAdd" runat="server" Text="Add" Visible="true" OnClick="BtnAdd_Click" />
                <input id="BtnCancel" type="button" value="Cancel" onclick="CloseAddWardPopup()" />
            </div>
        </div>

    </div>
