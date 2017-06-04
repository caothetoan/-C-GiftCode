<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="webXBGiftCode.Default" %>

<%@ Import Namespace="Libs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <title>
        <%=SurveyName %> |  <%=Config.SiteName %> 
        
    </title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="Hãy tìm hiểu xem tên bạn thực sự có nghĩa gì!">
<meta name="author" content="HayLam.vn">
<meta property="og:title" content="Tên bạn thực sự có nghĩa gì?">
<meta property="og:description" content="Hãy tìm hiểu xem tên bạn thực sự có nghĩa gì!">
<meta property="og:type" content="website">
<meta property="og:url" content="http://vi.nametests.com/test/ten-ban-thuc-su-co-nghia-gi/311/">
<meta property="og:image" content="http://img.nametests.com/media/quiz_ogimg/name2_xxoMGiE.png">
<meta property="og:image:type" content="image/jpeg">
<meta property="og:image:width" content="800">
<meta property="og:image:height" content="420">
<meta name="twitter:title" content="Tên bạn thực sự có nghĩa gì?">
<meta name="twitter:card" content="photo">
<meta name="twitter:image" content="http://img.nametests.com/media/quiz_ogimg/name2_xxoMGiE.png">

    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <script src="Scripts/jquery-1.7.1.js"></script>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=<%=Config.Version %>" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var SessionAccountId = <%=AccountId%>;
        var StatusAnswer = <%=StatusAnswer%>;   
        var CurrentStep = <%=CurrentStep%>;   
    </script>

    <%--  <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&amp;subset=all" rel="stylesheet" type="text/css" />
    --%>

    <script src="jsConfig.js?v=<%=Config.Version %>"></script>
    <script src="js/Libs.js?v=<%=Config.Version %>"></script>
    <script type="text/javascript" src="js/popup.js"> </script>
    <script src="js/toggle_menu.js" type="text/javascript"></script>
    <script src="js/dropzone.js"></script>
    <script src="js/jquery.zclip.js"></script>

    <script>
        window.fbAsyncInit = function () {
            FB.init({
                appId: '<%=Config.FbAppId%>',// 132607496783912  1629023360662076
                xfbml: true,
                version: 'v2.3'
            });
        };

        /* if (window.location.host == "www.haylam.vn" || window.location.host == "haylam.vn") {
            var lang = navigator.language || navigator.userLanguage;
            if (lang.indexOf("-") > -1) {
                lang = lang.split('-');
                lang = lang[0];
                lang = lang.toLowerCase();
            }
            window.location.replace("//" + lang + ".haylam.vn");
        }*/

    </script>
    <script type="text/javascript">
    

        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s);
            js.id = id;
            js.src = "//connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v2.3";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));


    

        function shareFB(thisuri) {
            window.open('https://www.facebook.com/sharer/sharer.php?u=' + encodeURIComponent(thisuri),
                    'facebook-share-dialog',
                    'width=572,height=567');
        }
        function shareFB_self(thisuri) {
            window.open('https://www.facebook.com/sharer/sharer.php?u=' + encodeURIComponent(thisuri), '_self',
                    'facebook-share-dialog',
                    'width=572,height=567');
        }
    </script>
</head>
<body>
    <form runat="server" id="frm1">
        <input type="hidden" id="currQuestion" value="0" runat="server" />

        <div class="slideout-menu open">

            <%if (IsOnline)
              {              
            %>
            <h3>
                <a href="javascript:;">
                    <%=Session["AccountName"] %>
                </a>
            </h3>
            <p><a href="javascript:;" class="slideout-menu-toggle"><i class="fa fa-times bar"></i></a></p>
            <div class="info">
                <ul>
                    <%-- <li>
                        <a href="javascript:;">Hiện có: 1000 RT  </a>
                    </li>--%>
                    <li>
                        <asp:LinkButton runat="server" ID="lbtnLogout" OnClick="lbtnLogout_OnClick">
                        Thoát tài khoản <i class="fa fa-sign-out"></i>

                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
            <% 
              } %>
            <ul>
                <li><a href="/">Trang Chủ <i class="fa fa-angle-right"></i></a></li>
                <li><a href="" target="_blank">Giới Thiệu <i class="fa fa-angle-right"></i></a></li>
                <%--<li><a href="javascript:;">Hướng dẫn <i class="fa fa-angle-right"></i></a></li>--%>
                <li><a href="<%=Libs.UrlHelper.UrlFanpage %>" target="_blank">Fanpage <i class="fa fa-angle-right"></i></a></li>
                <li><a href="javascript:;">Nạp thẻ<i class="fa fa-angle-right"></i></a></li>
            </ul>
        </div>
        <!--/.slideout-menu-->


        <div class="all">
            <div class="mn_top">
                <div class="w980">
                    <div class="logo_mn_top">
                        <a href="<%=Libs.Config.SiteUrl %>">
                            <img src="images/logoGo.png">
                        </a>
                    </div>
                    <!-- logo_mn_top -->
                    <%-- <div class="mn_list">
                    <ul>
                        <li><a href="javascript:;">Game </a></li>
                        <li><a href="javascript:;">Tin tức </a></li>
                        <li><a href="javascript:;">Hỗ trợ </a></li>
                        <li><a href="javascript:;">Nạp GO </a></li>
                        <li><a href="javascript:;">Giftcode </a></li>
                      
                        <div class="clear"></div>
                    </ul>
                  
                </div>--%>
                    <!-- mn_list -->
                    <% if (IsOnline)
                       {
                    %>
                    <div class="dkdn">
                        <ul>
                            <%-- <li><a href="<%=Libs.UrlHelper.UrlRegister %>">Đăng kí </a></li>
                        <li><a href="javascript:;">Đăng nhập </a></li>--%>
                            <li><a href="javascript:;">
                                <%=Session["AccountName"] %>
                            </a>
                            </li>

                            <li>
                                <asp:LinkButton runat="server" ID="lbtLogout2" OnClick="lbtnLogout_OnClick">
                        Thoát <i class="fa fa-sign-out"></i>

                                </asp:LinkButton>
                            </li>

                            <div class="clear"></div>
                        </ul>
                    </div>
                    <!-- dkdn -->
                    <% } %>
                    <div class="menu_mb" style="display: none">
                        <a href="javascript:;" class="slideout-menu-toggle"><i class="fa fa-bars"></i></a>
                    </div>

                </div>
                <!-- w980 -->
            </div>


            <div class="clear"></div>

            <%--    <div class="cacbuoc">
                <ul>
                    <li><i class="fa fa-circle"></i></li>
                    <li><i class="fa fa-circle"></i></li>
                    <li>
                        <div class="icon_buoc active">
                            <a href="javascript:;" id="dangnhap" $1$onclick="libs.NavMenu(this.id);"#1#><i class="fa fa-user"></i></a>
                        </div>
                        <p class="buoc1 active">Đăng nhập tài khoản </p>
                    </li>
                    <li><i class="fa fa-circle"></i></li>
                    <li><i class="fa fa-circle"></i></li>
                    <li>
                        <div class="icon_buoc ">
                            <a href="javascript:;" id="nhannhiemvu" $1$onclick="libs.NavMenu(this.id);"#1#><i class="fa fa-tasks"></i></a>
                        </div>
                        <p class="buoc1">Bói </p>
                    </li>
                   $1$ <li><i class="fa fa-circle"></i></li>
                    <li><i class="fa fa-circle"></i></li>
                    <li>
                        <div class="icon_buoc">
                            <a href="javascript:;" id="uploadanh" $2$onclick="libs.NavMenu(this.id);"#2#><i class="fa fa-upload"></i></a>
                        </div>
                        <p class="buoc1">Xác nhận nhiệm vụ </p>
                    </li>
                    <li><i class="fa fa-circle"></i></li>
                    <li><i class="fa fa-circle"></i></li>
                    <li>
                        <div class="icon_buoc">
                            <a href="javascript:;" id="nhanqua" $2$onclick="libs.NavMenu(this.id);"#2#><i class="fa fa-gift"></i></a>
                        </div>
                        <p class="buoc1">Nhận quà </p>
                    </li>
                    <li><i class="fa fa-circle"></i></li>
                    <li><i class="fa fa-circle"></i></li>#1#

                    <div class="clear"></div>
                </ul>

                <div class="buoc_mb">
                    <div class="buoc_mb1">Bước 1: Đăng nhập tài khoản </div>
                    <div class="buoc_mb2">Bước 2: Bói </div>
                   $1$ <div class="buoc_mb3">Bước 3: Xác nhận nhiệm vụ </div>
                    <div class="buoc_mb4">Bước 4: Nhận quà </div>#1#
                    <div class="clear"></div>
                </div>
                <!-- buoc_mb-->

            </div>
            <!-- cacbuoc-->
            --%>
            <div class="nd_main">
                <h1>Tên của bạn thực sự có nghĩa là gì?</h1>

                <% if (!IsOnline)
                   {
                %>
                <div class="dangnhap" id="step1">
                    <div id="frmLogin" class="w284">

                        <div class="tentaikhoan">
                            <i class="fa fa-user"></i>
                            <asp:TextBox type="text" ID="txtUserName" name="txtUserName" value="" placeholder="Tên tài khoản" TabIndex="0" runat="server" />
                        </div>
                        <!-- ten tai khoan -->


                        <div class="tentaikhoan">
                            <i class="fa fa-lock"></i>
                            <asp:TextBox TextMode="Password" ID="txtPassword" name="txtPassword" value="" placeholder="Mật khẩu" runat="server" />
                        </div>
                        <!-- ten tai khoan -->


                        <div class="checkbox quenmatkhau" style="float: left">
                            <%--<input id="cbRemember" type="checkbox" />
                           <label for="cbRemember"> Nhớ tài khoản</label>--%>
                            <a href="<%=Libs.UrlHelper.UrlRegister %>">Đăng kí</a>
                        </div>
                        <!-- ten tai khoan -->

                        <div class="quenmatkhau">
                            <a href="<%= Libs.UrlHelper.UrlForgotPassword %>" target="_blank">Quên mật khẩu? </a>
                        </div>
                        <div class="clear"></div>

                        <div class="btdn">
                            <%-- <a href="javascript:;" onclick="libs.Login()">Đăng nhập </a>
                            --%>
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn_submit" Text="Đăng nhập" OnClick="lbtnLogin_OnClick" />
                        </div>
                        <!-- kt bt dang ki -->
                        <div id="dangnhap_baoloi" class="">
                            <asp:Label runat="server" ID="lbMsg" Style="color: #FF0808; margin-top: 6px; font-size: 12px;"></asp:Label>
                        </div>

                    </div>
                    <!-- kt w284-->

                </div>
                <!-- dangn nhap -->
                <% } %>

                <%--  <%else
                   {%>--%>

                <div class="nhannhiemvu" id="step2">
                    
                   <%-- <div class="huongdan">
                        <img src="images/huongdan.png" width="119" height="123">
                        <ul>
                            <li class="active">Tên của bạn thực sự có nghĩa là gì
                               $1$ <br />
                                Click vào nút <span style="font-weight: bold;">KẾT QUẢ</span> để dự đoán#1#
                            </li>

                        </ul>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 upper-share-buttons">
                            <a class="share-fb" onclick="shareFB('http://vi.nametests.com/test/result/tina/130726498/index_new/')" title="Chia sẻ trên Facebook">Chia sẻ trên Facebook</a>
                        </div>

                    </div>--%>
                    <!-- huong dan -->
                    <p class="help-block">Vui lòng nhập tên của bạn</p>
                   <div id="" class="">
                        <input id="txtInput" value="" placeholder="Nhập nội dung" class="form-control user-success" />
                    </div>

                    <div class="btdn">
                         

                        <a onclick="libs.NhanNoiDung()" href="javascript:;">Kết quả </a>
                    </div>
                    <!-- btdn-->


                    <div class="huongdan detail_nnd" id="comment">

                        <div class="box_nd" id="box_nd">
                        </div>
                        <!-- boxx_nd -->
                        <div class="nut">
                            <%--<a class="nut_copy" href="javascript:;" onclick="libs.CopyNoiDung(this)">Copy nội dung </a>--%>
                            <%--<a class="nut_copy bt_blue" href="javascript:;" onclick="shareFB()">Chia sẻ</a>--%>

                            <div class="row">
                                <div class="col-xs-12 upper-share-buttons">
                                    <a class="share-fb" onclick="shareFB('http://vi.nametests.com/test/result/tina/130726498/index_new/')" title="Chia sẻ trên Facebook">Chia sẻ trên Facebook</a>
                                </div>

                            </div>
                        </div>
                        <!-- nut -->

                    </div>
                    <!-- detail _ nnd -->
                    <div id="nhannhiemvu_baoloi" class="baoloi"></div>

                </div>
                <!-- nhan nhiem vu -->
                <%--
                <div class="uploadanh" id="step3">
                    <p>Upload ảnh chụp comment đánh giá của bạn để Ban Quản Trị kiểm duyệt 
                    <br>Lưu ý: Bạn chỉ tải ảnh 1 lần duy nhất.</p>
                    <div class="box_nut">
                        <div class="bt_taianh">
                            <a class="bt_taianh" href="javascript:;" id="yourBtn" onclick="getFile()">
                                <i class="fa fa-plus"></i>
                                <br />
                                Tải ảnh lên
                            </a>
                        </div>
                    
                        <style>
                            .hide {
                                display: none;
                            }
                        </style>
                      
    
                            <asp:FileUpload runat="server" ID="fileUpload" CssClass="hide" onchange="check_extension(this.value,'btnUpload');" />
                           
                         <asp:Button CssClass="lonhon " runat="server" ID="btnUpload" Text="Tải ảnh" OnClick="UploadButton_Click" disabled="disabled" />
                            <!-- bt_taianh -->
                        
                        <div class="thanhtrangthai">
                            <span> </span>
                            <br />
                            <progress value="0" max="100" id="progress" class="hide"></progress>
                        </div>
                         <div id="uploadanh_img" class=""></div>
                        <!-- thanh trang thai -->
                        <div id="uploadanh_baoloi" class="baoloi" runat="server"></div>

                        <div class="bt_bo">
                            $1$<a href="javascript:;" onclick="libs.DoMission()">Thêm nhiệm vụ </a>#1#
                            <p></p>
                            <a id="uploadanh_next" class="lonhon hide" href="javascript:;" onclick="libs.NavMenu('nhanqua');libs.UpdateCurrentPoint(4);">Tiếp theo </a>
                        </div>

                    </div>
                    <!-- box_nut -->
                </div>
                <!-- upploadanh -->

                <div class="uploadanh2 nhanqua" id="step4">
                    <p>Click vào nút nhận quà để nhận quà </p>

                    <div class="box_nut">
                        <div class="bt_taianh">
                            <a class="bt_taianh" href="javascript:;" onclick="libs.GetGiftCodeInfo();">
                                <i class="fa fa-gift"></i>
                                <br />
                                Nhận quà
                            </a>
                        </div>
                        <!-- bt_taianh -->



                        <div id="nhanqua_baoloi" class="baoloi"></div>

                      
                        <div class="huongdan_giftcode detail_nnd_giftCode" id="giftCode">
                            <p>Danh sách giftcode</p>

                        </div>
                          <div class="bt_bo">
                            <a href="javascript:;" onclick="libs.DoMission()">Thêm nhiệm vụ </a>
                        </div>

                    </div>
                    <!-- box_nut -->
                </div>
                <!-- upploadanh -->--%>

                <%--  <% } 
                  %>--%>
            </div>
            <!--nd_main -->


            <!-- footer -->

        </div>
        <script type="text/javascript">
           
            $(function () {
                if (SessionAccountId > 0) {// đã đăng nhập
                    $(".dangnhap").hide();
                    $(".nhannhiemvu").hide();
                    $(".uploadanh").hide();
                    $(".nhanqua").hide();
                    
                    //libs.GetAccountInfo();
                    
                    jsConfig.isLogin = 1;
                    
                    switch (CurrentStep) {

                        case 1:
                            libs.NavMenu("dangnnhap");
                            break;
                        case 0: case 2:
                            libs.NavMenu("nhannhiemvu");
                            break;
                        case 3:
                            if(StatusAnswer == 0)
                                libs.NavMenu("nhannhiemvu");
                            else
                                libs.NavMenu("uploadanh");
                            break;
                        case 4:
                            libs.NavMenu("nhanqua");
                            break;
                        default:
                            libs.NavMenu("nhannhiemvu");
                            break;
                    }
                    $("#txtInput").focus();
                    
                } else {
                    jsConfig.isLogin = 0;

                    // bind enter key to submit login
                    
                    var txtUserName = $("#<%=txtUserName.ClientID%>");
                    //var txtUserName = $("#txtUserName");
                    txtUserName.focus();

                    /* $("#frmLogin").bind('keypress', function (e) {
                         if (e.keyCode == 13 && e.shiftKey == 0) {
                            
                             libs.Login();
                             return false;
                         }
                         //return true;
                     });*/                   
                }
            });
          


        </script>
    </form>
</body>
</html>
