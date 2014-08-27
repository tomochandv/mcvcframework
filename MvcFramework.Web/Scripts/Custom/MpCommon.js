// 2014 심민섭.
// 웹상에서 많이 쓰이는 기능들 모음.
// 사용법 : Common.function();
// 특징 : Jquery, Jquery Ui 가 바인딩 되어있어야함.

var Common = {
    //TextBox 빈값 체크
    Nullcheck: function (id, message) {
        var text = $('#' + id).val();
        if (text == '') {
            Common.Alert(message, message, 'I');
            return false;
        }
        else {
            return true;
        }
    },
    NullcheckSelector: function (selector, message) {
        var result = true;
        selector.each(function () {
            if ($(this).val() == '') {
                result = false;
            }
        });
        if (!result) {
            Common.Alert(message, message, 'I');
        }
        return result;
    },
    // 레이어 팝업 프레임.
    OpenPopupFrame: function (url, title, width, height) {
        var windowID = $(url.split('/')).last()[0].split('.')[0];
        var $dialog = $("#" + windowID);

        $('body').after('<iframe id="' + windowID + '" style="padding:0;" src="' + url + '">  </iframe>');
        $dialog = $("#" + windowID)
        $dialog.dialog(
         {
             autoOpen: false,
             title: title,
             position: 'center',
             sticky: false,
             width: width,
             height: height,
             draggable: true,
             resizable: false,
             modal: true,
             close: function () {
                 $(this).dialog('destroy');
                 $("#" + windowID).remove();
             }
         });
        $dialog.load(function () {
            $dialog.dialog('open');
            $dialog.css("width", "100%");
        });
    },
    //팝업div
    OpenPopup: function (id, title, width, height) {
        var $dialog = $("#" + id);
        $dialog.dialog(
         {
             autoOpen: false,
             title: title,
             position: 'center',
             sticky: false,
             width: width,
             height: height,
             draggable: true,
             resizable: false,
             modal: true,
             close: function () {
                 $(this).dialog('destroy');
             }
         });
        $dialog.dialog('open');
    },
    // replace all
    ReplaceAll: function (fulltxt, oTxt, nTxt) {
        return fulltxt.split(oTxt).join(nTxt);
    },
    //Ajax
    Ajax: function (url, data) {
        var returnData;
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            async: false,
            success: function (data) {
                returndata = data;
            },
            error: function (e) {
                console.log(e);
                alert('error');
            }
        });
        return returndata;
    },
    //MobileDevice Check
    Mobile: function(){
            var isMobile = {
                Android: function () {
                    return navigator.userAgent.match(/Android/i);
                },
                BlackBerry: function () {
                    return navigator.userAgent.match(/BlackBerry/i);
                },
                iOS: function () {
                    return navigator.userAgent.match(/iPhone|iPad|iPod/i);
                },
                Opera: function () {
                    return navigator.userAgent.match(/Opera Mini/i);
                },
                Windows: function () {
                    return navigator.userAgent.match(/IEMobile/i);
                },
                any: function () {
                    return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
                }
            }
            if(isMobile.any()){
                return true;
            }
            else{
                return false;
            }
    },
    Alert: function (title, message, type) {
        $.pnotify.defaults.styling = "bootstrap3";
        $.pnotify.defaults.history = false;
        if (type == "S") {
            $.pnotify({
                title: title,
                text: message,
                type: 'success',
                width: '300px',
                hide: false
            });
        }
        if (type == "E") {
            $.pnotify({
                title: title,
                text: message,
                type: 'error',
                width: '300px',
                hide: false
            });
        }
        if (type == "I") {
            $.pnotify({
                title: title,
                text: message,
                type: 'info',
                width: '300px',
                hide: false
            });
        }
    }
}

