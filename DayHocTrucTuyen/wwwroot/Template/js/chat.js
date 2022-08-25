const connection = new signalR.HubConnectionBuilder()
    .withUrl("/Models/ChatHub")
    .build();

connection.start().catch(err => console.error(err.toString()));

//Khi có người dùng kết nối và ngắt kết nối
connection.on('UserConnect', (ma) => {
    if (messUserReceived == ma) {
        $('#mess-user-status').removeClass('f-off').addClass('f-online');
    }
})
connection.on('UserDisconnect', (ma) => {
    if (messUserReceived == ma) {
        $('#mess-user-status').removeClass('f-online').addClass('f-off');
    }
})

//Kiểm tra người dùng đang có online hay không
connection.on('CheckOnline', (trangthai) => {
    if (trangthai) { $('#mess-user-status').removeClass('f-off').addClass('f-online') }
    else { $('#mess-user-status').removeClass('f-online').addClass('f-off') }
})

//Tự động nhận chat trên popup
connection.on('ReceivedChat', (ma, img, mess, time) => {
    if (messUserReceived == ma) {
        setChat('you', img, mess, time);
    }
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

    //Thanh cuộn về cuối trang
    main.scrollTop = main.scrollHeight - main.clientHeight;
}

//Mã người nhận tin nhắn
var messUserReceived;

//Hiển thị popup chat khi nhấn trên thông báo chat
$('.friendz-list > li, .chat-users > li, .drops-menu > li > a.show-mesg').on('click', function () {
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
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })

    //Kiểm tra online
    connection.invoke('CheckOnline', messUserReceived);

    $('.chat-box').addClass("show");
    return false;
});
$('.close-mesage').on('click', function () {
    $('.chat-box').removeClass("show");
    return false;
});

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
        data: { maNN: messUserReceived, noidung: noidung.value },
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
        data: { maNN: nhan, noidung: noidung.value },
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

console.log($('.room-member-status')[0].parentElement.firstElementChild.title)
console.log($('.room-member-status')[1].parentElement.firstElementChild.title)
console.log($('.room-member-status')[2].parentElement.firstElementChild.title)