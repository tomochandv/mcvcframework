/*
 * Hot Key
 * 편의성을 휘한 hotKey class
 */
var Key = {
    //로그아웃 ( shift + x )
    LogOut: function (e) {
        if ((e.shiftKey) && (e.keyCode == 120 || e.keyCode == 88)) {
            location.href = "/Login/LogOut";
        }
    },
    //사이드 메뉴 토글 ( shift + m)
    Menu: function (e) {
        if ((e.shiftKey) && (e.keyCode == 109 || e.keyCode == 77)) {
            Master.SlidebarToggle();
        }
    },
    //홈으로 가기 ( shift + h)
    Home: function (e) {
        if ((e.shiftKey) && (e.keyCode == 104 || e.keyCode == 72)) {
            location.href = "/Home/Index";
        }
    },
    UserSettingKey: function (e) {
        if (typeof (Storage) !== "undefined") {
            obj = $.parseJSON(localStorage.getItem("UserKey"));
        }
        else {
            obj = Common.Ajax("/Webbase/UserKey", null);
        }
        if (obj != null) {
            var url = '';
            $.each(obj, function (index, value) {
                if ((e.shiftKey) && e.keyCode == 33 && value.KEY_CODE == 1) {
                    url = value.PAGE_URL + "/" + value.UP_MENU_ID; return;
                }
                if ((e.shiftKey) && e.keyCode == 64 && value.KEY_CODE == 2) {
                    url = value.PAGE_URL + "/" + value.UP_MENU_ID; return;
                }
                if ((e.shiftKey) && e.keyCode == 35 && value.KEY_CODE == 3) {
                    url = value.PAGE_URL + "/" + value.UP_MENU_ID; return;
                }
                if ((e.shiftKey) && e.keyCode == 36 && value.KEY_CODE == 4) {
                    url = value.PAGE_URL + "/" + value.UP_MENU_ID; return;
                }
                if ((e.shiftKey) && e.keyCode == 37 && value.KEY_CODE == 5) {
                    url = value.PAGE_URL + "/" + value.UP_MENU_ID; return;
                }
                if ((e.shiftKey) && e.keyCode == 94 && value.KEY_CODE == 6) {
                    url = value.PAGE_URL + "/" + value.UP_MENU_ID; return;
                }
                if ((e.shiftKey) && e.keyCode == 38 && value.KEY_CODE == 7) {
                    url = value.PAGE_URL + "/" + value.UP_MENU_ID; return;
                }
                if ((e.shiftKey) && e.keyCode == 42 && value.KEY_CODE == 8) {
                    url = value.PAGE_URL + "/" + value.UP_MENU_ID; return;
                }
                if ((e.shiftKey) && e.keyCode == 40 && value.KEY_CODE == 9) {
                    url = value.PAGE_URL + "/" + value.UP_MENU_ID; return;
                }
                if ((e.shiftKey) && e.keyCode == 41 && value.KEY_CODE == 0) {
                    url = value.PAGE_URL + "/" + value.UP_MENU_ID; return;
                }
            });
            if (url != '') {
                location.href = url;
            }
        }
        //console.log(e.keyCode);
        
    }
}

$(function () {
    $("html").keypress(function (event) {
        Key.LogOut(event);
        Key.Menu(event);
        Key.Home(event);
        Key.UserSettingKey(event);
    });
});