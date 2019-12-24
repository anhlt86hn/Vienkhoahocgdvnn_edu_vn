function NewWindow_(url, name, w, h, scrollbar, resizable) {
    var LeftPosition = screen.width / 2 - w / 2;
    var TopPosition = screen.height / 2 - h / 2 - 100;
    //    if (TopPosition < 1) top = 1;
    var settings = "width=" + w + ",height=" + h + ",scrollbars=" + scrollbar + ",resizable=" + resizable + "'";
    mywindow = window.open(url, name, settings);
    mywindow.moveTo(LeftPosition, TopPosition);
}

//Đọc, ghi, xoá Cookies
/*
* Sets a Cookie with the given name and value.
*
* name Name of the cookie
* value Value of the cookie
* [expires] Expiration date of the cookie (default: end of current session) - điền số ngày tính từ ngày hiện tại
* [path] Path where the cookie is valid (default: path of calling document) - điền / để chạy khi có link rewrite
* [domain] Domain where the cookie is valid (default: domain of calling document)
* [secure] Boolean value indicating if the cookie transmission requires a secure transmission
*/
function setCookie(name, value, expires, path, domain, secure) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + parseInt(expires));
    document.cookie = name + "=" + escape(value) +
        ((expires) ? "; expires=" + exdate.toGMTString() : "") +
        ((path) ? "; path=" + path : "") +
        ((domain) ? "; domain=" + domain : "") +
        ((secure) ? "; secure" : "");
}
/**
* Gets the value of the specified cookie.
*
* name Name of the desired cookie.
*
* Returns a string containing value of specified cookie,
* or null if cookie does not exist.
*/
function getCookie(name) {
    var dc = document.cookie;
    var prefix = name + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin == -1) {
        begin = dc.indexOf(prefix);
        if (begin != 0) return null;
    }
    else {
        begin += 2;
    }
    var end = document.cookie.indexOf(";", begin);
    if (end == -1) {
        end = dc.length;
    }
    return unescape(dc.substring(begin + prefix.length, end));
}

/**
* Deletes the specified cookie.
*
* name name of the cookie
* [path] path of the cookie (must be same as path used to create cookie)
* [domain] domain of the cookie (must be same as domain used to create cookie)
*/
function deleteCookie(name, path, domain) {
    if (getCookie(name)) {
        document.cookie = name + "=" +
((path) ? "; path=" + path : "") +
((domain) ? "; domain=" + domain : "") +
"; expires=Thu, 01-Jan-70 00:00:01 GMT";
    }
}


function GetWindowWidth() {
    var myWidth = 0, myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myWidth = window.innerWidth;
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myWidth = document.documentElement.clientWidth;
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myWidth = document.body.clientWidth;
        myHeight = document.body.clientHeight;
    }
    return myWidth;
}

function GetWindowHeight() {
    var myWidth = 0, myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myWidth = window.innerWidth;
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myWidth = document.documentElement.clientWidth;
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myWidth = document.body.clientWidth;
        myHeight = document.body.clientHeight;
    }
    return myHeight;
}

//Bật lên thông báo tự tắt sau vài giây (miliSecondDelay: số mili giây đếm ngược đến khi thông báo tắt, noiDungThongBao: nội dung thông báo)
function ThongBao(miliSecondDelay, noiDungThongBao) {
    TaoThongBao();
    document.getElementById('divNoiDungThongBao').innerHTML = noiDungThongBao;
    miliSecondDelay = parseInt(miliSecondDelay) / 1000;
    var int = self.setInterval(
            function () {
                document.getElementById('divDemNguoiThoiGian').innerHTML = miliSecondDelay + "&nbsp;";
                miliSecondDelay--;
                if (miliSecondDelay < 0) {
                    window.clearInterval(int);
                    HuyThongBao();
                }
            }
            , 1000);
}
function TaoThongBao() {
    var left = (GetWindowWidth() - 400) / 2;
    document.getElementById('form1').innerHTML += "<div id='divThongBao'><div id='divKhungThongBao'><div id='divDemNguoiThoiGian'>&nbsp;</div><div id='divNoiDungThongBao'><!----></div></div></div>";
    document.getElementById('divThongBao').style.cssText = "background:#666;position:absolute;top:10%;left:" + left + "px;z-index:9999;display:none;border:solid 1px #fff;border-radius:10px;width:400px";
    document.getElementById('divKhungThongBao').style.cssText = "position:relative;padding:30px 50px 30px 20px";
    document.getElementById('divNoiDungThongBao').style.cssText = "font:normal 18px Tahoma;color:#fff";
    document.getElementById('divDemNguoiThoiGian').style.cssText = "font:normal 50px Tahoma;color:#fff;position:absolute;top:10px;right:-5px;-moz-opacity: 0.5;opacity:.50;filter: alpha(opacity=50)";
    document.getElementById('divThongBao').style.display = "block";
    window.scrollTo(0, 0);
}
function HuyThongBao() {
    document.getElementById('divThongBao').style.display = "none";
    document.getElementById('form1').removeChild(document.getElementById('divThongBao'));
}

/*DisplayProductIndex*/
function ScrollTo(id, top) {
    if (top == null) top = 0;
    if (id == null || id == "")
        jQuery('html, body').animate({ scrollTop: 0 + top }, 1000);
    else
        jQuery('html, body').animate({ scrollTop: jQuery(id).offset().top + top - 100 }, 1000);
}

/*Xoá chữ trong các textbox - parrentId: id của khối chứa các textbox*/
function ResetAllTextBox(parrentId) {
    jQuery(parrentId + " input[type=text]").each(
        function () {
            jQuery(this).attr('value', '');
        }
        );

    jQuery(parrentId + " textarea").each(
        function () {
            jQuery(this).attr('value', '');
        }
        );

    ScrollTo();
}

/*Xoá chữ trong các textbox - parrentId: id của khối chứa các textbox*/
function ResetAllTextBox(parrentId, scrollToTop) {
    jQuery(parrentId + " input[type=text]").each(
        function () {
            jQuery(this).attr('value', '');
        }
        );

    jQuery(parrentId + " textarea").each(
        function () {
            jQuery(this).attr('value', '');
        }
        );
    if (scrollToTop)
        ScrollTo();
}

function CheckEmail(idInput) {
    var email = jQuery(idInput);
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!filter.test(email.val())) {
        alert('Email không hợp lệ!');
        email.focus;
        return false;
    }
    return true;
}
function CheckEmailValue(email) {
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!filter.test(email)) {
        alert('Email không hợp lệ!');
        return false;
    }
    return true;
}

function loading(loading) {
    if (!document.getElementById("AjaxLoading")) {
        var left = (GetWindowWidth() - 36) / 2;
        var ajaxLoading = '<div id="AjaxLoading" onclick="loading(false)" style="background:#fff url(' + weburl + 'cms/display/cs/AjaxLoading.gif.ashx) no-repeat center;display:none;width:36px;height:36px;line-height:36px;position:fixed;_position:absolute;top:40%;left:' + left + 'px;z-index:9999;border:solid 1px #fff;border-radius:36px"><!----></div>';
        jQuery("body").append(ajaxLoading);
    }

    if (typeof loading == 'undefined' || loading) {
        jQuery("#AjaxLoading").show();
    } else {
        jQuery("#AjaxLoading").fadeOut();
    }
}


function loading2(idLoading, loading) {
    if (jQuery(idLoading + " .AjaxLoading2").length < 1) {
        var left = (jQuery(idLoading).outerWidth() - 36) / 2;

        var ajaxLoading = '<div class="AjaxLoading2" style="background:#fff url(' + weburl + 'cms/display/cs/AjaxLoadingS.gif.ashx) no-repeat center;display:none;width:36px;height:36px;line-height:36px;position:absolute;top:50%;left:' + left + 'px;z-index:9999;border:solid 1px #fff;border-radius:36px"><!----></div>';

        jQuery(idLoading).append(ajaxLoading).css({ "position": "relative", "z-index": "1" });
    }

    if (typeof loading == 'undefined' || loading) {
        jQuery(idLoading + " .AjaxLoading2").show();
    } else {
        jQuery(idLoading + " .AjaxLoading2").fadeOut();
    }
}


/*DisplayCommonControlShare*/
function AddtoSocialMedia(SocialBrand, url, title) {
    var PageURL = document.URL;
    var PageTitle = document.title;
    window.open(url + encodeURIComponent(PageURL) + title + (jQuery.trim(title).length == 0 ? "" : encodeURIComponent(PageTitle)), "win");
}




/*DisplayCommonControlsSupportOnline*/
/*Thêm hàm indexOf cho Array trong IE8*/
if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (obj, start) {
        for (var i = (start || 0), j = this.length; i < j; i++) {
            if (this[i] === obj) { return i; }
        }
        return -1;
    }
}
function ChangeYahooStatus(firstPart, textOnline, textOffline) {
    //Yêu cầu: thẻ a chứa ảnh trạng thái để dạng có background:
    //Tên class yahooOn --> có background là ảnh yahoo online
    //Tên class yahooOff --> có background là ảnh yahoo offline
    //firstPart=ymsgr:sendim? //Phần đầu trong href của thẻ a để tìm ra nick yahoo

    //Lấy tất cả các thẻ a có định dạng theo link nick chát Yahoo
    var listO = jQuery("a[href*='" + firstPart + "']");

    var nick;
    //Chuyển lại các nick đã tìm được vào mảng (các nick trùng lặp thì chỉ đưa vào mảng 1 lần)
    var list = [];

    for (var i = 0; i < listO.length; i++) {
        //Lấy ra nick
        nick = listO[i].href;
        nick = nick.substring(nick.indexOf("?") + 1, nick.length);
        //Nếu nick chưa có trong mảng thì đưa vào mảng        
        if (list.indexOf(nick) < 0)
            list.push(nick);
    }

    //Duyệt lần lượt từng nick trong mảng và kiểm tra + thay đổi trạng thái hiển thị
    for (var i = 0; i < list.length; i++) {
        nick = list[i];
        jQuery.ajax({
            url: "http://opi.yahoo.com/online?u=" + nick + "&m=t&t=1",
            type: 'GET',
            title: nick,
            success: function (res) {
                var currentNick = this.title;
                var status = res.responseText.replace(/(<([^>]+)>)/ig, "").replace(/\s*/g, "");

                if (status == "01") {
                    jQuery("a[href='" + firstPart + currentNick + "']").each(function () {
                        if (textOnline)
                            jQuery(this).html(textOnline);
                        jQuery(this).removeClass("checking").addClass("yahooOn").removeClass("yahooOff");

                    });
                }
                else {
                    jQuery("a[href='" + firstPart + currentNick + "']").each(function () {
                        if (textOffline)
                            jQuery(this).html(textOffline);
                        jQuery(this).removeClass("checking").addClass("yahooOff").removeClass("yahooOn");
                    });
                }
            },
            error: function (error) {//Lỗi xảy ra                      
            }
        });
    }
}


function ChangeSkypeStatus(firstPart) {
    //Yêu cầu: thẻ a chứa ảnh trạng thái để dạng có background:
    //Tên class skypeOn --> có background là ảnh skype online
    //Tên class skypeOff --> có background là ảnh skype offline
    //firstPart=skype: //Phần đầu trong href của thẻ a để tìm ra nick skype

    //Lấy tất cả các thẻ a có định dạng theo link nick chát skype
    var listO = jQuery("a[href*='" + firstPart + "']");

    var nick;
    //Chuyển lại các nick đã tìm được vào mảng (các nick trùng lặp thì chỉ đưa vào mảng 1 lần)
    var list = [];
    for (var i = 0; i < listO.length; i++) {
        //Lấy ra nick
        nick = listO[i].href;
        nick = nick.substring(nick.indexOf(":") + 1, nick.length);
        //Nếu nick chưa có trong mảng thì đưa vào mảng
        if (list.indexOf(nick) < 0)
            list.push(nick);
    }

    //Duyệt lần lượt từng nick trong mảng và kiểm tra + thay đổi trạng thái hiển thị
    for (var i = 0; i < list.length; i++) {
        nick = list[i];

        jQuery.ajax({
            url: "http://mystatus.skype.com/" + nick + ".num",
            type: 'GET',
            title: nick,
            success: function (res) {
                var currentNick = this.title;
                var status = res.responseText.replace(/(<([^>]+)>)/ig, "").replace(/\s*/g, "");

                if (status == "01" || status == "1") {//Offline
                    jQuery("a[href='" + firstPart + currentNick + "']").each(function () {
                        jQuery(this).removeClass("skypeOn").addClass("skypeOff");
                    });
                }
                else {
                    jQuery("a[href='" + firstPart + currentNick + "']").each(function () {
                        jQuery(this).removeClass("skypeOff").addClass("skypeOn");
                    });
                }
            },
            error: function (error) {//Lỗi xảy ra                    
            }
        });
    }

    //Thêm script kiểm tra skype (hiện thông báo download/chat)
    var checkskype = '<script type="text/javascript" src="http://download.skype.com/share/skypebuttons/js/skypeCheck.js"></script>';
    jQuery("body").append(checkskype);
}
function CropImage(oSelector, cropType) {
    //Xét kiểu cắt: 1: bỏ phần thừa, 0: không bỏ phần thừa( co ảnh lại và để trống 2 phía)
    if (typeof cropType == 'undefined')
        cropType = 1;


    /*Ý nghĩa hàm: Chỉnh width, height ảnh sao cho ảnh đại diện không bị méo khi hiển thị*/
    var listFrame = jQuery('' + oSelector + ''); //Tìm tất cả các đối tượng cần crop ảnh theo điều kiện tìm kiếm            
    /*
    Chú ý: các khung chứa ảnh cần có cấu trúc như sau

    <div class='khungAnh'>
    <img src="img/pic1.jpg"/>
    </div>

    Trong đó: class khungAnh phải fix width, height, overflow:hidden, position:relative - Mục đích: tạo khung chứa ảnh
    */

    var maxwidth; var maxheight; var child;
    for (var i = 0; i < listFrame.length; i++) {//Duyệt qua tất cả các đối tượng tìm thấy

        maxwidth = jQuery(listFrame[i]).outerWidth(); //Lấy chiều rộng của khung
        maxheight = jQuery(listFrame[i]).outerHeight(); //Lấy chiều cao của khung        
        child = jQuery(listFrame[i]).find("img"); //Tìm con trong khung (trường hợp này là thẻ img)

        CropAnImage(child, cropType, maxwidth, maxheight);
    }
}

function CropAnImage(child, cropType, maxwidth, maxheight) {
    var animateTime = 0;
    if (!maxwidth || !maxheight) {//Nếu ko có maxwidth hoặc maxheght -> Tự tìm bố nó để lấy
        var parent = jQuery(child).parent();
        maxwidth = parent.outerWidth();
        maxheight = parent.outerHeight();
    }

    var width; var height;
    width = jQuery(child).outerWidth(); //Lấy width của img
    height = jQuery(child).outerHeight(); //Lấy height của img     
    if (cropType == 1) {
        if ((width / height) > (maxwidth / maxheight))//Nếu ảnh thừa width --> fix height
        {
            //Tính width thiếu theo tỉ lệ
            width = width * (maxheight / height);

            //Đặt lại position để ảnh rơi vào giữa khung
            var left = width - maxwidth;
            left = left / 2;

            jQuery(child).css({ "position": "absolute", "z-index": 1, "top": 0, "left": 0 })
            jQuery(child).animate({ "left": -left + "px", "top": 0, "height": maxheight + "px", "width": width + "px" }, animateTime);

        }
        else {//Nếu thừa height --> fix width

            //Tính height thiếu theo tỉ lệ
            height = height * (maxwidth / width);

            //Đặt lại position để ảnh rơi vào giữa khung
            var top = height - maxheight;
            top = top / 2;

            jQuery(child).css({ "position": "absolute", "z-index": 1, "top": 0, "left": 0 })
            jQuery(child).animate({ "top": -top + "px", "left": 0, "width": maxwidth + "px", "height": height + "px" }, animateTime);
        }
    }
    else {
        if ((width / height) > (maxwidth / maxheight))//Nếu ảnh thừa width --> fix width
        {
            //Tính height thiếu theo tỉ lệ
            height = height * (maxwidth / width);

            //Đặt lại position để ảnh rơi vào giữa khung
            var top = maxheight - height;
            top = top / 2;

            jQuery(child).css({ "position": "absolute", "z-index": 1, "top": 0, "left": 0 })
            jQuery(child).animate({ "top": top + "px", "left": 0, "width": maxwidth + "px", "height": height + "px" }, animateTime);
        }
        else {//Nếu thừa height --> fix height
            //Tính width thiếu theo tỉ lệ
            width = width * (maxheight / height);

            //Đặt lại position để ảnh rơi vào giữa khung
            var left = maxwidth - width;
            left = left / 2;

            jQuery(child).css({ "position": "absolute", "z-index": 1, "top": 0, "left": 0 })
            jQuery(child).animate({ "left": left + "px", "top": 0, "height": maxheight + "px", "width": width + "px" }, animateTime);
        }
    }
}
function ShowScrollToTop() {
    /*Kiểm tra điều kiện width*/
    var show1 = false;
    var khungQCT = jQuery('#ScrollToTop');
    /*980 là chiều rộng của website*/
    /*Ẩn nút cuộn lên dầu nếu chiều rộng của trình duyệt nhỏ hơn chiều rộng website + chiều rộng nút cuộn lên đầu*/
    if (GetWindowWidth() < (980 + khungQCT.outerWidth()))
        show1 = false;
    else
        show1 = true;

    /*Kiểm tra điều kiện scroll*/
    var show2 = false;
    if (f_scrollTop() > 100)
        show2 = true;
    else
        show2 = false;

    if (show1 && show2)
        khungQCT.fadeIn();
    else
        khungQCT.fadeOut();
}

function IncreaseTextSize() {
    var size = parseInt(jQuery(".TextSize").css("font-size"));
    size++;
    var lineheight = parseInt(jQuery(".TextSize").css("line-height"));
    lineheight++;
    jQuery(".TextSize").css({ "font-size": size + "px", "line-height": lineheight + "px" });
}

function DecreaseTextSize() {
    var size = parseInt(jQuery(".TextSize").css("font-size"));
    size--;
    var lineheight = parseInt(jQuery(".TextSize").css("line-height"));
    lineheight--;
    jQuery(".TextSize").css({ "font-size": size + "px", "line-height": lineheight + "px" });
}

function ResetTextSize() {
    jQuery(".TextSize").css({ "font-size": "14px", "line-height": "20px" });
}


function f_clientWidth() {
    return f_filterResults(
		window.innerWidth ? window.innerWidth : 0,
		document.documentElement ? document.documentElement.clientWidth : 0,
		document.body ? document.body.clientWidth : 0
	);
}
function f_clientHeight() {
    return f_filterResults(
		window.innerHeight ? window.innerHeight : 0,
		document.documentElement ? document.documentElement.clientHeight : 0,
		document.body ? document.body.clientHeight : 0
	);
}
function f_scrollLeft() {
    return f_filterResults(
		window.pageXOffset ? window.pageXOffset : 0,
		document.documentElement ? document.documentElement.scrollLeft : 0,
		document.body ? document.body.scrollLeft : 0
	);
}
function f_scrollTop() {
    return f_filterResults(
		window.pageYOffset ? window.pageYOffset : 0,
		document.documentElement ? document.documentElement.scrollTop : 0,
		document.body ? document.body.scrollTop : 0
	);
}
function f_filterResults(n_win, n_docel, n_body) {
    var n_result = n_win ? n_win : 0;
    if (n_docel && (!n_result || (n_result > n_docel)))
        n_result = n_docel;
    return n_body && (!n_result || (n_result > n_body)) ? n_body : n_result;
}


(function ($) {
    $.fn.ShowPopup = function (options) {
        // This is the easiest way to have default options.
        var settings = $.extend({
            // These are the defaults.
            target: null
        }, options);

        return this.each(function () {
            jQuery(this).click(function () {

                var content;
                if (settings.target)
                    content = jQuery(settings.target).html();
                else
                    content = jQuery(jQuery(this).attr("href")).html();


                var fade = '<div id="fadePopup"><!----></div>';
                var light = '<div id="lightPopup"><span class="btClosePopup">&nbsp;</span><div id="lightPopupContent">' + content + '</div></div>';

                jQuery('#fadePopup').remove();
                jQuery('#lightPopup').remove();

                jQuery('body').append(fade);
                jQuery('body').append(light);

                jQuery("#fadePopup").click(function () { $.fn.ShowPopup.hide(); });
                jQuery("#lightPopup .btClosePopup").click(function () { $.fn.ShowPopup.hide(); });

                var left = (GetWindowWidth() - jQuery('#lightPopup').outerWidth()) / 2;
                jQuery('#lightPopup').css("left", left);
                jQuery('#lightPopup').fadeIn();
                jQuery('#fadePopup').fadeIn();

                return false;
            });
        });
    };

    $.fn.ShowPopup.hide = function () {
        jQuery('#lightPopup').fadeOut();
        jQuery('#fadePopup').fadeOut();
    };
}(jQuery));


(function ($) {
    var settings;//Lưu các tham số khởi tạo khi gọi hàm
    var listChildren;//Lưu danh sách các con trong lõi khung cuộn
    var currentChildIndex = 0;//Lưu chỉ số của con hiện tại (con trong lõi khung cuộn đang đứng đầu trong danh sách các các được hiển thị)
    var totalWidth = 3;//Lưu tổng width của lõi khung cuộn

    $.fn.InitScroll = function (options) {
        // This is the easiest way to have default options.
        settings = $.extend({
            // These are the defaults.
            ScrollFrame: null,//Bắt buộc phải truyền vào lúc khởi tạo
            ScrollFrameContent: null,//Bắt buộc phải truyền vào lúc khởi tạo
            LeftButton: null,
            RightButton: null,
            FrameWidth: null,
            AnimateTime: 300
        }, options);

        if (!settings.FrameWidth)
            settings.FrameWidth = jQuery(settings.ScrollFrame).outerWidth();

        //Gán sự kiện cho các nút
        if (settings.LeftButton)
            jQuery(settings.LeftButton).click(function () { $.fn.InitScroll.Scroll(-1); });
        if (settings.RightButton)
            jQuery(settings.RightButton).click(function () { $.fn.InitScroll.Scroll(1); });


        //Gán width cho khối overflowhidden ngoài
        jQuery(settings.ScrollFrame).css({ "width": settings.FrameWidth, "position": "relative", "z-index": "1", "overflow": "hidden" });

        //Gán css cho khối lõi ở trong
        jQuery(settings.ScrollFrameContent).css({ "position": "absolute", "z-index": "1", "top": "0", "left": 0 });

        //Tính tổng width cho khối lõi bên trong (khối chứa nội dung cần bị cuộn)
        listChildren = jQuery(settings.ScrollFrameContent).children();
        jQuery(settings.ScrollFrameContent).children().each(function () {
            totalWidth += jQuery(this).outerWidth(true);
        });
        jQuery(settings.ScrollFrameContent).css("width", totalWidth);

        //Ẩn hai nút cuộn trái, phái nếu không cần cuộn
        if (totalWidth < settings.FrameWidth) {
            jQuery(settings.LeftButton).hide();
            jQuery(settings.RightButton).hide();
        }

        //Khởi tạo về đầu khối cuộn
        jQuery(settings.ScrollFrameContent).animate({ "left": 0 }, settings.AnimateTime);
    };

    //Hàm cuộn
    $.fn.InitScroll.Scroll = function (step) {
        if (jQuery(listChildren[currentChildIndex + step]).position()) {
            currentChildIndex += step;
            var left = -jQuery(listChildren[currentChildIndex]).position().left;

            if (settings.FrameWidth - left > totalWidth) {
                left = settings.FrameWidth - totalWidth;
                currentChildIndex -= step;
            }
            jQuery(settings.ScrollFrameContent).animate({ "left": left }, settings.AnimateTime);
        } else {
            jQuery(settings.ScrollFrameContent).animate({ "left": 0 }, settings.AnimateTime);
        }
    };
}(jQuery));


function InitCheckInputContact(selector) {
    jQuery(selector + " .required").change(function () {
        if (this.value.length < 1) {
            jQuery(this).addClass("notfilled");
            //jQuery(this).parent().append(errdiv);
            this.focus();
        } else {
            jQuery(this).removeClass("notfilled");
        }
    });
}

function ResetInputContact(selector) {
    jQuery(selector + " .errorms").remove();
    jQuery(selector + " .notfilled").removeClass("notfilled");

    jQuery(selector + " input").val("");
    jQuery(selector + " textarea").val("");
}

function CheckInputContact(selector) {
    var firstId = '';
    var pass = true;
    var errdiv = '<div class="errorms">Đây là ô bắt buộc phải điền</div>';
    jQuery(selector + " .errorms").remove();
    jQuery(selector + " .notfilled").removeClass("notfilled");

    jQuery(selector + " .required").each(function () {
        if (this.value.length < 1) {
            pass = false;
            jQuery(this).addClass("notfilled");

            //jQuery(this).parent().append(errdiv);
            //this.focus();
            if (firstId.length < 1)
                firstId = this.id;
        }
    });
    if (!pass)
        document.getElementById(firstId).focus();
    else {
        if (jQuery(selector + ' #tbEmail').length > 0)
            if (!CheckEmail(selector + ' #tbEmail')) {
                pass = false;
                document.getElementById('#tbEmail').focus();
            }
    }

    return pass;
}