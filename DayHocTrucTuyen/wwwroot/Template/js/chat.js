const connection = new signalR.HubConnectionBuilder()
    .withUrl("/Models/ChatHub")
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
    }

    //Gán trạng thái online tại lớp học
    $('.room-member-status').filter('.' + ma).removeClass('f-off').addClass('f-online');
})
connection.on('UserDisconnect', (ma) => {

    //Xóa trạng thái online tại mini chat
    if (messUserReceived == ma) {
        $('#mess-user-status').removeClass('f-online').addClass('f-off');
    }

    //Xóa trạng thái online tại lớp học
    $('.room-member-status').filter('.' + ma).removeClass('f-online').addClass('f-off');
})

//Kiểm tra người dùng đang có online hay không (mini chat)
connection.on('CheckOnline', (trangthai) => {
    if (trangthai) { $('#mess-user-status').removeClass('f-off').addClass('f-online') }
    else { $('#mess-user-status').removeClass('f-online').addClass('f-off') }
})

//Tự động nhận chat trên popup
connection.on('ReceivedChat', (ma, img, mess, time) => {
    if (messUserReceived == ma) {
        setChat('you', img, mess, time);
    }

    //Làm mới thông báo
    getPingMess();

    //Báo có tin trong mini chat
    $('.mess-scroll-bottom').html('<i class="fa fa-angle-double-down"></i> Tin nhắn mới');
})

//add tin nhắn
function setChat(user, avt, mess, time) {
    var main = document.getElementById('mess-content');
    var li = document.createElement('li');
    li.classList = user;
    var user = document.createElement('div');
    user.classList = 'chat-thumb';
    var img = document.createElement('img');
    img.src = avt;
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
    main.appendChild(li);

    //Hiển thị theo khoảng thời gian
    $("i.timeago").timeago();
}

//Mã người nhận tin nhắn
var messUserReceived;

//Xử lý nút gửi tin trên popup tin nhắn
$('#mess-send').on('click', function () {
    var noidung = document.getElementById('mess-new-text');
    if (!noidung.value) {
        getThongBao('error', 'Lỗi', 'Nội dung tin nhắn không được để trống!');
        return false;
    }
    $.ajax({
        url: '/User/Mess/sendNewTinNhan',
        type: 'POST',
        data: { maNN: messUserReceived, noidung: noidung.value, trangthai: true },
        success: function (data) {
            if (!data.tt) {
                getThongBao('error', 'Lỗi !', 'Mã lệnh javascript đã bị thay đổi. Vui lòng tải lại trang !');
            }
            else {
                connection.invoke("SendToUser", messUserReceived, noidung.value);
                setChat('me', data.img_Avt, data.noi_Dung, data.thoi_Gian);
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
})

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
        url: '/User/Mess/sendNewTinNhan',
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


//Gán thông báo cho tin nhắn
function addPingMess(usend, img, name, noidung, time) {
    $('#menu-ping-mess').append(
        '<li>' +
        '<a class="show-mesg" title="" id="' + usend + '">' +
        '<figure>' +
        '<img width="40" height="40" src="' + img + '" alt="" />' +
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

function getPingMess() {
    $.ajax({
        url: '/User/Mess/getAllTinChuaXem',
        type: 'POST',
        success: function (data) {

            $('#dot-tin-nhan').html(data.sl);
            $('.sl-ping-mess').html(data.sl);
            $('#menu-ping-mess').html('');

            if (data.sl == 0) {
                $('#dot-tin-nhan').hide();

                $('#menu-ping-mess').append(
                    '<span class="d-flex justify-content-center">Hiện không có tin nhắn mới nào!</span>'
                );
            }
            else {
                $('#dot-tin-nhan').show();

                $.each(data.list, function (index, value) {
                    addPingMess(value.usend, value.img, value.name, value.noidung, value.time);
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

//Hiển thị popup chat khi nhấn trên thông báo chat
$('#menu-ping-mess').on('click', 'li', function () {
    var maNG = this.children[0].id;
    messUserReceived = maNG;

    $.ajax({
        url: '/User/Mess/getTinNhanTuUser',
        type: 'POST',
        data: { maNG: maNG },
        success: function (data) {
            if (!data.tt) {
                getThongBao('error', 'Lỗi !', 'Mã lệnh javascript đã bị thay đổi. Vui lòng tải lại trang !');
            }
            else {
                document.getElementById('mess-content').innerHTML = null;
                document.getElementById('mess-user-name').innerHTML = data.uSend.ho_Lot + " " + data.uSend.ten;
                $.each(data.tinNhan, function (index, value) {
                    if (value.nguoi_Gui == maNG) setChat('you', data.uSend.img_Avt, value.noi_Dung, value.thoi_Gian);
                    else setChat('me', data.uReceived.img_Avt, value.noi_Dung, value.thoi_Gian);
                })
            }
            document.getElementById('mess-view-info').onclick = function () { location.replace('/Profile/Info?id=' + data.uSend.ma_ND) }

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
});

//Đóng popup mini chat
$('.close-mesage').on('click', function () {
    $('.chat-box').removeClass("show");
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
        url: '/User/Mess/setXemTatCaTinNhan',
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
});

//Bắt sự kiện đi cuối mini chat
$('.mess-scroll-bottom').on('click', function () {
    var main = document.getElementById('mess-content');
    $("#mess-content").animate({ scrollTop: main.scrollHeight - main.clientHeight }, "slow");
})