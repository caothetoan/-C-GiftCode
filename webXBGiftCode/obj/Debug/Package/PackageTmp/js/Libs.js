var libs = new function () {
    this.StatusAnswer = window.StatusAnswer;
    this.CurrentStep = window.CurrentStep;

    this.NavMenu = function (id) {
        libs.HideAll(id);
        if (id == "dangnhap" && jsConfig.isLogin == 1) {
            $(".nhannhiemvu").fadeIn(jsConfig.timeShow);
            $(".icon_buoc").removeClass("active");
            $(".buoc1").removeClass("active");
            $("#nhannhiemvu").parent().addClass("active");
            $("#nhannhiemvu").parent().next().addClass("active");

        } else {
            $("." + id).fadeIn(jsConfig.timeShow);
            if (id == "nhannhiemvu") {
                $("#comment").hide();

            }
            if (id == "nhanqua") {
                $("#giftCode").hide();
            }
        }

    };
    this.HideAll = function (id) {
        $(".dangnhap").hide();
        $(".nhannhiemvu").hide();
        $(".uploadanh").hide();
        $(".nhanqua").hide();
        $(".icon_buoc").removeClass("active");
        $(".buoc1").removeClass("active");
        $("#" + id).parent().addClass("active");
        $("#" + id).parent().next().addClass("active");
    };
    this.CopyNoiDung = function (obj) {
        var text = $("#box_nd").html();

        var textToClipboard = text;
        var success = true;
        if (window.clipboardData) { // Internet Explorer
            window.clipboardData.setData("Text", textToClipboard);
        }
        else {
            var forExecElement = CreateElementForExecCommand(textToClipboard);
            SelectContent(forExecElement);

            try {
                if (window.netscape && netscape.security) {
                    netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
                }
                success = document.execCommand("copy", false, null);
            }
            catch (e) {
                success = false;
            }
            document.body.removeChild(forExecElement);
        }

        if (success) {
            console.log("The text is on the clipboard, try to paste it!");
        }
        else {
            console.log("Your browser doesn't allow clipboard access!");
            var msg = "Trình duyệt của bạn không cho phép sao chép nội dung này. Vui lòng bôi đen nội dung, bấm chuột phải và chọn Copy"
            //libs.Alert(msg);
            $("#nhannhiemvu_baoloi").html(msg).show();
        }

    };



    function CreateElementForExecCommand(textToClipboard) {
        var forExecElement = document.createElement("div");
        forExecElement.style.position = "absolute";
        forExecElement.style.left = "-10000px";
        forExecElement.style.top = "-10000px";
        forExecElement.textContent = textToClipboard;
        document.body.appendChild(forExecElement);
        forExecElement.contentEditable = true;

        return forExecElement;
    };

    function SelectContent(element) {
        var rangeToSelect = document.createRange();
        rangeToSelect.selectNodeContents(element);

        var selection = window.getSelection();
        selection.removeAllRanges();
        selection.addRange(rangeToSelect);
    };

    this.Login = function () {
        var UserName = $("#txtUserName").val();
        var Password = $("#txtPassword").val();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: jsConfig.UrlRoot + "handler/Account.ashx?act=Login",
            data: {
                UserName: UserName,
                Password: Password
            },
            cache: false,
            success: function (obj) {
                var msg = 'Có lỗi xảy ra.';
                if (obj)
                    msg = obj.ResponseMessage;

                if (console)
                    console.log(msg);

                if (obj.Code > 0) {//"Đã đăng nhập"

                    libs.NavMenu("nhannhiemvu");
                    libs.GetAccountInfo();
                } else {// không đăng nhập được

                    libs.Alert(msg);
                }
            },
            error: function (err) {
                console.log("error");
            }
        });
    };
    this.Alert = function (s) {
        alert(s);
    };
    this.NhanNoiDung = function () {
        $("#comment").show();
        libs.UpdateCurrentPoint(2);//Nhận nội dung
        $.ajax({
            type: "POST",
            url: jsConfig.webApi + "Libs/GetQuestion",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (obj) {

                if (obj._ResponseStatus >= 0) {
                    $("#nhannhiemvu_baoloi").html("");
                    console.log(obj._QuestionId);

                    if (obj._ResponseStatus == -1) { // session expire
                        $("#nhannhiemvu_baoloi").html("Vui lòng đăng nhập lại").show();
                        return;
                    }
                    if (obj._QuestionId == 0) { // hết Giftcode
                        $("#nhannhiemvu_baoloi").html("Hết Giftcode. Vui lòng quay lại sau 18h hàng ngày.").show();
                        return;
                    }

                    $("#box_nd").html(obj._QuestionContent);
                    $("#currQuestion").val(obj._QuestionId);
                    //$("#<%=currQuestion.ClientID %>").val(obj._QuestionId);
                }
                else {
                    console.log("Lỗi GetQuestion: " + obj._ResponseStatus);
                    var ERR_UNKNOW = -99,
                         ERR_NOT_EXIST_ACCOUNT = -50,
                         ERR_NOT_EXIST_GIFTCODE = -49,
                         ERR_ACCOUNT_PENDING = -46,
                        ERR_ACCOUNT_NOT_LOGIN = -1;

                    switch (obj._ResponseStatus) {
                        case ERR_ACCOUNT_NOT_LOGIN:
                            $("#nhannhiemvu_baoloi").html("Vui lòng đăng nhập lại");
                            break;
                        case ERR_NOT_EXIST_GIFTCODE:
                            $("#nhannhiemvu_baoloi").html("Không có Giftcode. Vui lòng quay lại sau 18h hàng ngày.");
                            break;
                        case ERR_ACCOUNT_PENDING:
                            // $("#nhannhiemvu_baoloi").html("Nội dung đang chờ kiểm duyệt");

                            $("#box_nd").html(obj._QuestionContent);
                            $("#currQuestion").val(obj._QuestionId);
                            break;
                        default:
                            $("#nhannhiemvu_baoloi").html("Có lỗi xảy ra");
                            break;
                    }
                    $("#nhannhiemvu_baoloi").show();

                }
            },
            error: function (err) {
                console.log("error: " + err);
            }
        });

    };
    this.UpdateCurrentPoint = function (CurrentPoint) {

        $.ajax({
            type: "GET",
            url: jsConfig.webApi + "Libs/UpdateCurrentStep/?_CurrentStep=" + CurrentPoint,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (obj) {
                libs.CurrentStep = window.CurrentStep = CurrentPoint;
            },
            error: function (err) {
                //console.log("error");
            }
        });
    };
    this.GoStore = function () {
        libs.NavMenu('uploadanh');
        libs.UpdateCurrentPoint(3);//Nhận nội dung
        setTimeout(function () {
            //window.open('https://play.google.com/store/apps/details?id=com.vtcol.dhbc', '_blank');
	 window.open('http://xuanbac.goplay.vn/tai-game', '_blank');
        }, 800);
    };
    this.MsgSurveyNotJoin = "Bạn chưa gửi ảnh. Vui lòng làm nhiệm vụ";
    this.MsgSurveyWaiting = "Ảnh của bạn chưa được kiểm duyệt. Vui lòng quay lại sau 18h hàng ngày";
    this.MsgSurveyDeline = "Ảnh của bạn bị từ chối. Vui lòng làm lại nhiệm vụ";
    this.MsgSurveyAccept = "Chúc mừng bạn đã nhận được giftcode";
    this.MsgContinueSurveyWaiting = "Bạn cần phải được kiểm duyệt nhiệm vụ đang làm mới được làm tiếp nhiệm vụ mới";
    this.DoMission = function () {
        var giftCodeObj = $("#giftCode");
        var nhanquabaoloi = $("#nhanqua_baoloi");
        switch (libs.StatusAnswer) {
            case 3: case 2:
                libs.NavMenu('nhannhiemvu');
                break;
            case 0:
                nhanquabaoloi.html(libs.MsgSurveyNotJoin);
                libs.NavMenu('nhannhiemvu');
                break;
            case 1:
                nhanquabaoloi.html(libs.MsgContinueSurveyWaiting);
                break;
            default:
                break;
        }
    };

    // get giftcode trả về cho user
    this.GetGiftCodeInfo = function () {
        var giftCodeObj = $("#giftCode");
        var nhanquabaoloi = $("#nhanqua_baoloi");
        /*if (libs.CurrentStep < 4)
            libs.GetAccountInfo();*/
      
        $.ajax({
            type: "GET",
            url: jsConfig.webApi + "Libs/GetGifCodeDiary",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (obj) {
                var html = "";
                
                // nếu ảnh được duyệt thì get giftcode
                switch (libs.StatusAnswer) {
                    case 3:
                        nhanquabaoloi.html(libs.MsgSurveyDeline);
                        return;
                    case 0:
                        nhanquabaoloi.html(libs.MsgSurveyNotJoin);
                        return;
                    case 1:
                        nhanquabaoloi.html(libs.MsgSurveyWaiting);
                        return;
                    case 2:
                        nhanquabaoloi.html(libs.MsgSurveyAccept);
                    default:
                        break;
                }
                
                var listCode = obj.listCode;
              
                if (listCode.length == 0) {
                    html = "Ảnh của bạn đang chờ kiểm duyệt. Vui lòng quay lại sau 18h để nhận giftcode.";

                    nhanquabaoloi.html(html);

                    /*if (libs.StatusAnswer != 2)
                        return;*/
                    return;
                }
                html = "<table width=\"100%\" cellspacing=\"1\" cellpadding=\"5\" border=\"0\" bgcolor=\"#bca9d8\"> <tbody>" +
                    "<tr style=\"font-weight: bold\">" +
                    "<td bgcolor=\"#e1d7ee\" style=\"text-align: center\">Stt</td>" +
                    "<td bgcolor=\"#e1d7ee\" style=\"text-align: center\">Câu hỏi</td>" +
                    "<td bgcolor=\"#e1d7ee\" style=\"text-align: center\">GiftCode</td>" +
                    /* "<td bgcolor=\"#e1d7ee\" style=\"text-align: center\">Trạng thái</td>" +*/
                    "<td bgcolor=\"#e1d7ee\" style=\"text-align: center\">Ngày nhận</td>" +
                    "</tr>";

                for (var i = 0; i < listCode.length; i++) {
                    var item = listCode[i];
                    html += "<tr>" +
                         "<td bgcolor=\"#FFFFFF\" style=\"text-align: center\">" + item._RowNumber + "</td>" +
                        "<td bgcolor=\"#FFFFFF\">" + item._Content + "</td>" +
                        "<td bgcolor=\"#FFFFFF\">" + item._GiftCode + "</td>" +
                        /*"<td bgcolor=\"#FFFFFF\">" + item._Status + "</td>" +*/
                        "<td bgcolor=\"#FFFFFF\">" + item._CreatedDatetime + "</td>" +
                        "</tr>";
                }

                html += "</tbody></table>";

                nhanquabaoloi.html("");
                $("#giftCode").html(html);
                $("#giftCode").show();
            },
            error: function (err) {
                console.log("error");
            }
        });
    };

    this.GetAccountInfo = function () {
        /* $(".dangnhap").hide();
         $(".nhannhiemvu").hide();
         $(".uploadanh").hide();
         $(".nhanqua").hide();*/
        $.ajax({
            type: "GET",
            url: jsConfig.webApi + "Libs/GetAccountInfor",
            contentType: "application/json; charset=utf-8",
            data: { AccountId: window.SessionAccountId },
            dataType: "json",
            cache: false,
            success: function (obj) {
                if (obj._Username == null || obj._Username == '') {
                    libs.NavMenu("dangnnhap");
                    return;
                }

                //var curr = parseInt(obj._CurrentPoint);
                var curr = parseInt(obj._CurrentStep);
                libs.CurrentStep = window.CurrentStep = curr;
                libs.StatusAnswer = window.StatusAnswer = parseInt(obj._StatusAnswer);

                switch (curr) {

                    case 1:
                        libs.NavMenu("dangnnhap");
                        break;
                    case 0: case 2:
                        libs.NavMenu("nhannhiemvu");
                        break;
                    case 3:
                        libs.NavMenu("uploadanh");
                        break;
                    case 4:
                        libs.NavMenu("nhanqua");
                        break;
                    default:
                        libs.NavMenu("nhannhiemvu");
                        break;
                }
            },
            error: function (err) {
                console.log("error");
            }
        });

    };
}();