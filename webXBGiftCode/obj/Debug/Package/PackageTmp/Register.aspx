<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="webXBGiftCode.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="nd_main">
                  <% if (!IsOnline)
                     {
                  %>
                <div class="dangnhap" id="step1">
                    <div id="frmLogin" class="w284">

                        <div class="tentaikhoan">
                            <i class="fa fa-user"></i>
                            <asp:TextBox runat="server" type="text" ID="txtUserName" name="txtUserName" value="" placeholder="Tên tài khoản" />
                        </div>
                        <!-- ten tai khoan -->


                        <div class="tentaikhoan">
                            <i class="fa fa-lock"></i>
                            <asp:TextBox runat="server" TextMode="Password" ID="txtPassword" name="txtPassword" value="" placeholder="Mật khẩu" />
                        </div>
                        <!-- ten tai khoan -->
                          <div class="tentaikhoan">
                            <i class="fa fa-lock"></i>
                              <asp:TextBox  runat="server" TextMode="Password" ID="txtPasswordAgain" name="txtPasswordAgain" value="" placeholder="Nhập lại mật khẩu" />
                        </div>
                        <!-- ten tai khoan -->

                        
                        <div class="quenmatkhau"><a href="<%= Libs.UrlHelper.GetUrlLogin %>">Đăng nhập </a></div>
                        <div class="clear"></div>

                        <div class="btdn">
                           
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn_submit" Text="Đăng kí" OnClick="lbtnRegister_OnClick" />
                        </div>
                        <!-- kt bt dang ki -->
                          <div id="dangnhap_baoloi" class="">
                              <asp:Label runat="server" ID="lbMsg" Style="color: #FF0808;margin-top: 6px;font-size: 12px;"></asp:Label>
                          </div>

                    </div>
                    <!-- kt w284-->

                </div>
                <!-- dangn nhap -->
                <% } %>
          </div>
    <script>
        var txtUserName = $("#<%=txtUserName.ClientID%>");

        txtUserName.focus();
        

        /*$("#frmLogin").bind('keypress', function (e) {
            if (e.keyCode == 13 && e.shiftKey == 0) {
                
                $("#form1").submit();
                    
                //document.form1.submit();

                         
                            return false;
                        }
             //return true;
         });*/
    </script>
</asp:Content>
