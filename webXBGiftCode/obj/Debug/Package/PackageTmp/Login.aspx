<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="webXBGiftCode.Login" %>
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
                            <asp:TextBox runat="server" type="text" id="txtUserName" name="txtUserName" value="" placeholder="Tên tài khoản" tabindex="0" />
                        </div>
                        <!-- ten tai khoan -->


                        <div class="tentaikhoan">
                            <i class="fa fa-lock"></i>
                            <asp:TextBox runat="server" TextMode="Password" id="txtPassword" name="txtPassword" value="" placeholder="Mật khẩu" />
                        </div>
                       

                        <div class="quenmatkhau" style="float: left">
                            <%--<input type="checkbox">
                            Nhớ tài khoản--%>
                            <a href="<%= Libs.UrlHelper.UrlRegister %>">Đăng kí</a>
                        </div>
                        <!-- ten tai khoan -->

                        <div class="quenmatkhau">
                            <a href="<%= Libs.UrlHelper.UrlForgotPassword %>" target="_blank">Quên mật khẩu? </a>
                        </div>
                        <div class="clear"></div>

                        <div class="btdn">
                          
                           
                             <asp:Button ID="btnSubmit" runat="server" CssClass="btn_submit" Text="Đăng nhập" OnClick="lbtnLogin_OnClick" />
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
