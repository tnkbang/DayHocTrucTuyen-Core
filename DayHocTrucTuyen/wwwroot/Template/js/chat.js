const connection = new signalR.HubConnectionBuilder()
    .withUrl("/models/chathub")
    .build();

connection.start().catch(err => console.error(err.toString()));

//Nhận ping tin nhắn sau khi tải trang
$(function () {
    getPingMess();
});

//Khi có người dùng kết nối và ngắt kết nối
connection.on('UserConnect', (ma) => {

    //Gán trạng thái online tại popup mini chat
    if (messUserReceived == ma) {
        $('#mess-user-status').removeClass('f-off').addClass('f-online');
        $('#hidden-mess-user-status').removeClass('f-off').addClass('f-online');
    }

    //Gán trạng thái online tại lớp học
    $('.room-member-status').filter('.' + ma).removeClass('f-off').addClass('f-online');
})
connection.on('UserDisconnect', (ma) => {

    //Xóa trạng thái online tại mini chat
    if (messUserReceived == ma) {
        $('#mess-user-status').removeClass('f-online').addClass('f-off');
        $('#hidden-mess-user-status').removeClass('f-online').addClass('f-off');
    }

    //Xóa trạng thái online tại lớp học
    $('.room-member-status').filter('.' + ma).removeClass('f-online').addClass('f-off');
})

//Kiểm tra người dùng đang có online hay không (mini chat)
connection.on('CheckOnline', (trangthai) => {
    if (trangthai) {
        $('#mess-user-status').removeClass('f-off').addClass('f-online');
        $('#hidden-mess-user-status').removeClass('f-off').addClass('f-online')
    }
    else {
        $('#mess-user-status').removeClass('f-online').addClass('f-off');
        $('#hidden-mess-user-status').removeClass('f-online').addClass('f-off')
    }
})

//Tự động nhận chat trên popup
connection.on('ReceivedChat', (ma, img, mess, time) => {
    if (messUserReceived == ma) {
        setChat('app', 'you', img, mess, time);
    }

    //Làm mới thông báo
    getPingMess();

    //Báo có tin trong mini chat
    var main = document.getElementById('mess-content');
    var down = document.querySelector('.mess-scroll-bottom');
    if (main.scrollTop + 100 < main.scrollHeight - main.clientHeight) {
        down.style.display = 'inline-block';
        down.innerHTML = '<i class="fa fa-angle-double-down"></i> Tin nhắn mới';
    } else {
        down.style.display = 'none';
        $("#mess-content").animate({ scrollTop: main.scrollHeight - main.clientHeight }, "slow");
    }
})

//add tin nhắn
function setChat(type, user, avt, mess, time) {
    var main = document.getElementById('mess-content');
    var li = document.createElement('li');
    li.classList = user;
    var user = document.createElement('div');
    user.classList = 'chat-thumb';
    var img = document.createElement('img');
    img.src = avt;
    img.classList = 'wh-30';
    user.appendChild(img);
    li.appendChild(user);
    var model = document.createElement('div');
    model.classList = 'notification-event';
    var spnmodel = document.createElement('span');
    spnmodel.classList = 'chat-message-item';
    spnmodel.innerText = mess;
    model.appendChild(spnmodel);
    var spntime = document.createElement('span');
    spntime.classList = 'notification-date';
    var i = document.createElement('i');
    i.classList = 'timeago';
    i.title = time;
    i.innerText = time;
    spntime.appendChild(i);
    model.appendChild(spntime);
    li.appendChild(model);

    if (type == 'pre') {
        main.prepend(li);
    }
    else {
        main.appendChild(li);
    }

    //Tăng số lượng tin nhắn
    offsetChat++

    //Hiển thị theo khoảng thời gian
    $("i.timeago").timeago();
}

//Biến toàn cục
var messUserReceived;
var isLoad = true;
var offsetChat = 0;

//Hàm gửi tin nhắn
function send_mess() {
    var noidung = document.getElementById('mess-new-text');
    if (!noidung.value) {
        getThongBao('error', 'Lỗi', 'Nội dung tin nhắn không được để trống!');
        return false;
    }
    $.ajax({
        url: '/user/mess/sendnewtinnhan',
        type: 'POST',
        data: { maNN: messUserReceived, noidung: noidung.value, trangthai: true },
        success: function (data) {
            if (!data.tt) {
                getThongBao('error', 'Lỗi !', 'Mã lệnh javascript đã bị thay đổi. Vui lòng tải lại trang !');
            }
            else {
                connection.invoke("SendToUser", messUserReceived, noidung.value);
                setChat('app', 'me', data.img_Avt, data.noi_Dung, data.thoi_Gian);
                noidung.value = null;
            }

            //Thanh cuộn cuối phần tử tin nhắn
            var messContent = document.getElementById('mess-content');
            messContent.scrollTop = messContent.scrollHeight - messContent.clientHeight;
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })
}
//Xử lý nút gửi tin trên popup tin nhắn
$('#mess-send').on('click', function () {
    send_mess()
})

//Xử lý nhấn enter gửi tin
$('#mess-new-text').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        send_mess()
    }
});

//Xử lý gửi tin nhắn từ trang info
function infoSendMess(gui, nhan) {
    event.preventDefault();
    var noidung = document.getElementById('info-send-mess-text');
    if (!noidung.value) {
        getThongBao('error', 'Lỗi', 'Nội dung tin nhắn không được để trống !');
        return false;
    }
    if (gui == nhan) {
        getThongBao('warning', 'Lỗi', 'Không thể gửi tin cho chính mình !');
        return false;
    }
    $.ajax({
        url: '/user/mess/sendnewtinnhan',
        type: 'POST',
        data: { maNN: nhan, noidung: noidung.value, trangthai: false },
        success: function (data) {
            if (!data.tt) {
                getThongBao('error', 'Lỗi !', 'Mã lệnh javascript đã bị thay đổi. Vui lòng tải lại trang !');
            }
            else {
                connection.invoke("SendToUser", nhan, noidung.value);
                getThongBao('success', 'Thành công !', 'Đã gửi tin nhắn !');
                noidung.value = null;
                $('.popup-wraper1').removeClass('active');
            }
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })
}

//Gán danh sách người dùng đang onl tại trang lớp học
connection.on('ListOnline', (list) => {
    $.each(list, function (index, value) {
        $('.room-member-status').filter('.' + value).removeClass('f-off').addClass('f-online');
    })
})


//Gán thông báo cho tin nhắn pc
function addPingMess(usend, img, name, noidung, time) {
    $('#menu-ping-mess').append(
        '<li class="' + usend + '">' +
        '<a class="show-mesg" title="">' +
        '<figure>' +
        '<img class="wh-35" src="' + img + '" alt="" />' +
        '</figure>' +
        '<div class="mesg-meta">' +
        '<h6>' + name + '</h6>' +
        '<span><i class="ti-check"></i> ' + noidung + '</span>' +
        '<i class="timeago" title="' + time + '">' + time + '</i>' +
        '</div>' +
        '</a>' +
        '</li>'
    );
}

//Gán thông báo cho tin nhắn mobi
function addPingMessMobi(usend, img, name, noidung, time) {
    $('#mobi-menu-ping-mess').append(
        '<li class="' + usend + ' list-none mobi-ping-mess">' +
        '<div class="mesg-meta mobi-msg">' +
        '<img class="wh-35" src="' + img + '" alt="" />' +
        '<h6>' + name + '</h6>' +
        '<span><i class="ti-check"></i> ' + noidung + '</span>' +
        '<i class="timeago" title="' + time + '">' + time + '</i>' +
        '</div>' +
        '</li>'
    );
}

function getPingMess() {
    $.ajax({
        url: '/user/mess/getalltinchuaxem',
        type: 'POST',
        success: function (data) {

            $('#dot-tin-nhan').html(data.sl);
            $('.sl-ping-mess').html(data.sl);
            $('.mobi-sl-ping-mess').html('(' + data.sl + ')');

            $('#menu-ping-mess').html('');
            $(".list-none").remove();

            if (data.sl == 0) {
                $('#dot-tin-nhan').hide();

                $('#menu-ping-mess').append(
                    '<span class="d-flex justify-content-center">Hiện không có tin nhắn mới nào!</span>'
                );
                $('#mobi-menu-ping-mess').append(
                    '<li class="list-none">Không có tin nhắn mới nào!</li>'
                );
            }
            else {
                $('#dot-tin-nhan').show();

                $.each(data.list, function (index, value) {
                    addPingMess(value.usend, value.img, value.name, value.noidung, value.time);
                    addPingMessMobi(value.usend, value.img, value.name, value.noidung, value.time);
                })

                //Hiển thị theo khoảng thời gian
                $("i.timeago").timeago();
            }
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })
}

//Hiển thị popup chat khi nhấn trên thông báo chat với pc
$('#menu-ping-mess').on('click', 'li', function () {
    openPopupChat(this.classList[0])
});

//Hiển thị popup chat khi nhấn trên thông báo chat với mobile
$("#menu").on("click", ".mobi-ping-mess", function () {
    var mobi_menu = $("#menu").data("mmenu");
    mobi_menu.close();
    openPopupChat(this.classList[0])
});

//Hàm mở popup chat
function openPopupChat(maUser) {
    var maNG = maUser;
    messUserReceived = maNG;

    //Làm mới lại tin nhắn
    isLoad = true;
    offsetChat = 0;

    $.ajax({
        url: '/user/mess/gettinnhantuuser',
        type: 'POST',
        data: { maNG: maNG },
        success: function (data) {
            if (!data.tt) {
                getThongBao('error', 'Lỗi !', 'Mã lệnh javascript đã bị thay đổi. Vui lòng tải lại trang !');
            }
            else {
                document.getElementById('mess-content').innerHTML = null;
                document.getElementById('mess-user-name').innerHTML = data.uSend.ho_Lot + " " + data.uSend.ten;
                document.getElementById('hidden-mess-user-name').innerHTML = data.uSend.ho_Lot + " " + data.uSend.ten;

                $.each(data.tinNhan, function (index, value) {
                    if (value.nguoi_Gui == maNG) setChat('app', 'you', data.uSend.img_Avt, value.noi_Dung, value.thoi_Gian);
                    else setChat('app', 'me', data.uReceived.img_Avt, value.noi_Dung, value.thoi_Gian);
                })
            }
            document.getElementById('mess-view-info').onclick = function () { location.replace('/profile/info/' + data.uSend.ma_ND) }

            //Thanh cuộn cuối phần tử tin nhắn
            var messContent = document.getElementById('mess-content');
            messContent.scrollTop = messContent.scrollHeight - messContent.clientHeight;
            document.querySelector('.mess-scroll-bottom').style.display = 'none';

            //Nhận lại ping mess
            getPingMess();
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })

    //Kiểm tra online
    connection.invoke('CheckOnline', messUserReceived);

    $('.chat-box').addClass("show");
    $('.hidden-chat').removeClass("show");
}

//Tạm ẩn mini chat
$('.temp-hide-chat').on('click', function () {
    $('.chat-box').removeClass("show");
    $('.hidden-chat').addClass("show");
});

//Hiển thị mini chat từ trạng thái tạm ẩn
$('.temp-show-chat').on('click', function () {
    $('.chat-box').addClass("show");
    $('.hidden-chat').removeClass("show");
});

//Đóng popup mini chat
$('.close-mesage').on('click', function () {
    $('.chat-box').removeClass("show");
    $('.hidden-chat').removeClass("show");
    return false;
});

//Làm mới thông báo tin nhắn
$('#icon-tin-nhan').on('click', function () {
    getPingMess();
})

//Hàm set đã xem tất cả tin nhắn
function setXemTatCaTinNhan(maND) {
    event.preventDefault();
    $.ajax({
        url: '/user/mess/setxemtatcatinnhan',
        type: 'POST',
        data: { maND: maND },
        success: function (data) {
            if (!data.tt) {
                getThongBao('error', 'Lỗi !', 'Mã lệnh javascript đã bị thay đổi. Vui lòng tải lại trang !');
            }
            else {
                getPingMess();
                getThongBao('success', 'Thành công !', 'Đã đánh dấu là đã xem tất cả thông báo !');
            }
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })
}

//Bắt sự kiện khi scroll mini chat
$('#mess-content').on('scroll', function () {
    var main = document.getElementById('mess-content');
    var down = document.querySelector('.mess-scroll-bottom');
    if (main.scrollTop + 1 < main.scrollHeight - main.clientHeight) {
        down.style.display = 'inline-block';
    } else {
        down.style.display = 'none';
        $('.mess-scroll-bottom').html('<i class="fa fa-angle-double-down"></i> Về cuối');
    }

    //Xử lý tải thêm tin nhắn
    if (main.scrollTop == 0 && isLoad) {

        //Lưu trữ vị trí scoll trước khi lấy tin mới
        var tempscoll = main.scrollHeight;

        //Gọi về server lấy tin
        $.ajax({
            url: '/user/mess/gettinnhantuuser',
            type: 'POST',
            data: { maNG: messUserReceived, offset: offsetChat, limit: 10 },
            success: function (data) {
                if (!data.tt) {
                    getThongBao('error', 'Lỗi !', 'Mã lệnh javascript đã bị thay đổi. Vui lòng tải lại trang !');
                }
                else {
                    $.each(data.tinNhan, function (index, value) {
                        if (value.nguoi_Gui == messUserReceived) setChat('pre', 'you', data.uSend.img_Avt, value.noi_Dung, value.thoi_Gian);
                        else setChat('pre', 'me', data.uReceived.img_Avt, value.noi_Dung, value.thoi_Gian);
                    })

                    //Trả scoll về vị trí trước khi lấy thêm tin nhắn
                    main.scrollTop = main.scrollHeight - tempscoll;

                    //Set trạng thái load
                    if (data.tinNhan.length == 0) {
                        isLoad = false;
                        main.firstChild.insertAdjacentHTML('beforebegin', '<p class="full-chat">Đã hết tin</p>')
                    }
                }
            },
            error: function () {
                getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
            }
        })
    }
});

//Bắt sự kiện đi cuối mini chat
$('.mess-scroll-bottom').on('click', function () {
    var main = document.getElementById('mess-content');
    $("#mess-content").animate({ scrollTop: main.scrollHeight - main.clientHeight }, "slow");
})