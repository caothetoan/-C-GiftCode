function PopUpShowing(sender, eventArgs) {
    var popUp = eventArgs.get_popUp();
    
    var gridWidth = sender.get_element().offsetWidth;
    var gridHeight = sender.get_element().offsetHeight;
    var popUpWidth = popUp.style.width.substr(0, popUp.style.width.indexOf("px"));
    var popUpHeight = popUp.style.height.substr(0, popUp.style.height.indexOf("px"));
    popUp.center();
    //popUp.style.left = ((gridWidth - popUpWidth) / 2 + sender.get_element().offsetLeft).toString() + "px";
}

function ConfirmDelete(sender, args) {
    var callback = Function.createDelegate(sender, function (argument) {
        if (argument) {
            this.click();
        }
    });
    var text = "Bạn có chắc chắc muốn xóa không?";
    radconfirm(text, callback, 300, 100, null, "Xóa dữ liệu");
    args.set_cancel(true);
}
function GetRadWindow() {
    var wnd = null;
    if (window.radWindow)
        wnd = window.radWindow;
    else if (window.frameElement && window.frameElement.radWindow)
        wnd = window.frameElement.radWindow;
    
    return wnd;
}


function CloseAndRebind() {
    GetRadWindow().Close();
    GetRadWindow().BrowserWindow.RebindGrid();
}

function CancelEdit() {
    GetRadWindow().Close();
}

function StopPropagation(e) {
    if (!e) {
        e = window.event;
    }
    e.cancelBubble = true;
}

function ShowMessageBox(message) {
    try {
        radalert(message, 400, 150, "Thông báo");
    } catch (e) {
        alert(message);
    }
}

function ValidateInput(sender) {
    var html = sender.get_value();
    html = html.replace("<", "&lt;");
    html = html.replace(">", "&gt;");
    sender.set_value(html);
}