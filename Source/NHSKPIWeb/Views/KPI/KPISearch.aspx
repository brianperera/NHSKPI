<%@ Page Title="NHS KPI Data Entry Portal" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="KPISearch.aspx.cs" Inherits="Views_KPI_KPISearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<script src="../../assets/scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>

    <script src="../../assets/scripts/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>--%>

    <script src="../../scripts/comment.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contentbody" runat="Server">
    <div class="page-header position-relative">
        <h1>
            KPI Search
        </h1>
    </div>
    <div class="grid_12">
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblKPINo" runat="server" Text="KPI No :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtKPINo" runat="server"></asp:TextBox>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblKPIDescription" runat="server" Text="KPI Description :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:TextBox ID="txtKPIDescription" runat="server"></asp:TextBox>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblKPIGroupName" runat="server" Text="KPI Group Name :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:DropDownList ID="ddlKPIGroupName" runat="server">
            </asp:DropDownList>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_8">
            <asp:Label ID="lblIsActive" runat="server" Text="Is Active :"></asp:Label>
        </div>
        <div class="grid_16">
            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" />
        </div>
        <div class="clear">
        </div>
        <div class="grid_16 prefix_8">
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </div>
    </div>
    <div class="grid_24">
        <div class="Hgap10">
        </div>
        <div class="gridtable">
            <div class="gridwrap">
                <div class="gridleft">
                </div>
                <div class="gridtitle">
                    <h3>
                        KPI(s) Search Result<asp:HiddenField ID="hfKPINo" runat="server" />
                    </h3>
                </div>
                <div class="gridright">
                </div>
            </div>
        </div>
        <asp:GridView CssClass="grid" ID="gvSearchResult" runat="server" AutoGenerateColumns="False"
            OnRowDataBound="gvSearchResult_RowDataBound" DataKeyNames="KPINo">
            <EmptyDataTemplate>
                <div class="center bold error grid_empty">
                    No Results found.</div>
            </EmptyDataTemplate>
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataTextField="KPINo" HeaderText="KPI No"
                    DataNavigateUrlFormatString="KPIUpdate.aspx?Id={0}" />
                <asp:BoundField DataField="KPIDescription" HeaderText="KPI Description" />
                <asp:BoundField DataField="KPIGroupName" HeaderText="KPI Group" />
                <asp:TemplateField HeaderText="Comment">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbButton" runat="server" Text="Comment">             
                
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
             <AlternatingRowStyle CssClass="altrow" />
        </asp:GridView>
        <div class="Hgap10">
        </div>

        <script language="javascript" type="text/javascript">
            function popUp(width, height, tras, url) {
                testwindow = window.open(url, tras, 'width=700,height=550,scrollbars=yes,menubar=no,addressbar=no,titlebar=no,toolbar=no');
                testwindow.focus();


            }          
            
            
        </script>

    </div>
</asp:Content>
