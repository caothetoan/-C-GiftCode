<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="webXBGiftCode.Cms.Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CMSContentPlaceHolder" runat="server">
    <asp:Login ID="CMSAdminLogin" runat="server" Width="100%" EnableViewState="false"
        OnAuthenticate="Authenticate">
        <LayoutTemplate>
            <div class="LoginForm">
                <ul>
                    <li class="label">Địa chỉ e-mail:</li>
                    <li class="data" style="float: left">
                        <%--<telerik:RadTextBox ID="UserName" runat="server" Skin="Windows7" Width="200px">
                        </telerik:RadTextBox>--%>
                        <asp:TextBox ID="UserName" runat="server">
                        </asp:TextBox>
                    </li>
                    <li class="validator">
                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="UserName"
                            CssClass="validation_error" ValidationGroup="LogInalidationGroup">*</asp:RequiredFieldValidator>
                    </li>
                    <li class="clear"></li>
                    <li class="label">Mật khẩu:</li>
                    <li class="data">
                        <%--<telerik:RadTextBox ID="Password" runat="server" Skin="Windows7" Width="200px" TextMode="Password">
                        </telerik:RadTextBox>--%>
                        <asp:TextBox ID="Password" runat="server" TextMode="Password">
                        </asp:TextBox>
                    </li>
                    <li class="validator">
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="Password"
                            CssClass="validation_error" ValidationGroup="LogInalidationGroup">*</asp:RequiredFieldValidator>
                    </li>
                    <li class="remember">
                        <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time."></asp:CheckBox>
                    </li>
                    <li class="error">
                        <p>
                            <asp:Literal ID="lblError" runat="server"></asp:Literal></p>
                    </li>
                    <li class="submit">
                        <telerik:RadButton ID="LoginButton" Skin="Windows7" runat="server" CommandName="Login"
                            ValidationGroup="LogInalidationGroup" Text="Đăng nhập">
                            <Icon PrimaryIconUrl="Images/Key.png" PrimaryIconLeft="4" PrimaryIconTop="4" />
                        </telerik:RadButton>
                    </li>
                </ul>
            </div>
        </LayoutTemplate>
    </asp:Login>
    <script type="text/javascript">
        function InitLoginForm() {
            var top = parseInt(($(document).height() - $(".LoginForm").height()) / 2);
            $(".LoginForm").css("top", top);
        }

        window.onload = function () { InitLoginForm(); };
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () { InitLoginForm(); });
    </script>
</asp:Content>
