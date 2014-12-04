<%@ Page Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master" Title="NHS KPI Data Entry Portal"
    AutoEventWireup="true" CodeFile="NotificationConfig.aspx.cs" Inherits="Views_Notification_NotificationConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-header position-relative">
                <h1>Setup Hospital Notification Rules
                </h1>
            </div>
            <div class="grid_12 ward-bulk-upload">
                <div class="grid_8">
                    <asp:Label ID="lblDeadline" runat="server" Text="Reminder 1"></asp:Label>
                </div>
                <div class="grid_16">
                    <asp:DropDownList ID="ddlReminder1" Width="50px" CssClass="repeatPanelControls" runat="server">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList>
                    <%--Repeat Monthly
                    <asp:CheckBox ID="chkRepeatMonthly" runat="server" CssClass="repeatDealineCheckbox" Checked="True" />--%>
                </div>
                <div class="grid_8">
                    <asp:Label ID="Label1" runat="server" Text="Reminder 2"></asp:Label>
                </div>
                <div class="grid_16">
                    <asp:DropDownList ID="ddlReminder2" Width="50px" CssClass="repeatPanelControls" runat="server">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList>
                    <%--Repeat Monthly
                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="repeatDealineCheckbox" Checked="True" />--%>
                </div>
                <div class="grid_8">
                    <asp:Label ID="Label2" runat="server" Text="Manager Escalation"></asp:Label>
                </div>
                <div class="grid_16">
                    <asp:DropDownList ID="ddlEscalation" Width="50px" CssClass="repeatPanelControls" runat="server">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList>
                    <%--Repeat Monthly
                    <asp:CheckBox ID="CheckBox2" runat="server" CssClass="repeatDealineCheckbox" Checked="True" />--%>
                </div>
                <div class="grid_8">
                    <asp:Label ID="Label4" runat="server" Text="Reminder E-Mail"></asp:Label>
                </div>
                <div class="grid_16">
                    <asp:TextBox ID="txtReminderEmailAddress" placeholder="Reminder E-Mail" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtReminderEmailAddress"
                        Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                            runat="server" ErrorMessage="Invalid" ControlToValidate="txtReminderEmailAddress" Display="Dynamic"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </div>
                <div class="grid_8">
                    <asp:Label ID="Label5" runat="server" Text="Manager Escalation E-Mail"></asp:Label>
                </div>
                <div class="grid_16">
                    <asp:TextBox ID="txtEscalationEmailAddress" placeholder="Manager Escalation E-Mail" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtEscalationEmailAddress"
                        Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                            runat="server" ErrorMessage="Invalid" ControlToValidate="txtEscalationEmailAddress" Display="Dynamic"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </div>
                <div class="grid_8">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                </div>
            </div>


            <div class="page-header position-relative">
                <h1>Setup Email Bucket for Hospital (Dashboard Emails)
                </h1>
            </div>
            <div class="grid_16">

                <asp:UpdatePanel runat="server" ID="UpdatePanelKPIGroups">
                    <ContentTemplate>
                        <div class="field-control-middle">
                            <span>
                                <asp:TextBox ID="txtEmail" CssClass="magin_0" placeholder="Email" runat="server"></asp:TextBox>
                            </span>
                            <div class="paragraph-break">
                                <asp:ListBox ID="lbEmailList" CssClass="listbox" Height="80" Width="160" runat="server">
                                    
                                </asp:ListBox>
                            </div>
                        </div>
                        <div class="grid_24 error_msg">
                            <asp:Label ID="lblAddEmailMessage" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="field-control-last">
                            <asp:Button ID="btnAddEmail" CssClass="file-uploaded-button" runat="server" Text="Add" OnClick="btnAddEmail_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
