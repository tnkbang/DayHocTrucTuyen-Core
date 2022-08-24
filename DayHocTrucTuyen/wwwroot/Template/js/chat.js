const connection = new signalR.HubConnectionBuilder()
    .withUrl("/Models/ChatHub")
    .build();

connection.start().catch(err => console.error(err.toString()));

connection.on('Send', (nick, message) => {
    //appendLine(nick, message);
    console.log(nick + " : " + message);
});

$('#mess-send').on('click', function () {
    var noidung = document.getElementById('mess-new-text');
    connection.invoke("SendToUser", messUserReceived, noidung.value);
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
}

//Mã người nhận tin nhắn
var messUserReceived;
//--- side message box	
$('.friendz-list > li, .chat-users > li, .drops-menu > li > a.show-mesg').on('click', function () {
    var maNG = this.children[0].id;
    messUserReceived = maNG;

    var firstLoad = true;
    setInterval(function () {
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
                document.getElementById('mess-view-info').onclick = function () { location.replace('/Profile/Info?User=' + data.uSend.ma_ND) }
                $("i.timeago").timeago();

                if (firstLoad) {
                    //Thanh cuộn cuối phần tử tin nhắn
                    var messContent = document.getElementById('mess-content');
                    messContent.scrollTop = messContent.scrollHeight - messContent.clientHeight;
                    firstLoad = false;
                }
            },
            error: function () {
                getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
            }
        })
    }, 500);
    $('.chat-box').addClass("show");
    return false;
});
$('.close-mesage').on('click', function () {
    $('.chat-box').removeClass("show");
    return false;
});

//$('#mess-send').on('click', function () {
//    var noidung = document.getElementById('mess-new-text');
//    if (!noidung.value) {
//        getThongBao('error', 'Lỗi', 'Nội dung tin nhắn không được để trống!');
//        return false;
//    }
//    $.ajax({
//        url: '/User/Mess/sendNewTinNhan',
//        type: 'POST',
//        data: { maNN: messUserReceived, noidung: noidung.value },
//        success: function (data) {
//            if (!data.tt) {
//                getThongBao('error', 'Lỗi !', 'Mã lệnh javascript đã bị thay đổi. Vui lòng tải lại trang !');
//            }
//            else {
//                setChat('me', data.img_Avt, data.noi_Dung, data.thoi_Gian);
//                noidung.value = null;
//            }
//            $("i.timeago").timeago();

//            //Thanh cuộn cuối phần tử tin nhắn
//            var messContent = document.getElementById('mess-content');
//            messContent.scrollTop = messContent.scrollHeight - messContent.clientHeight;
//        },
//        error: function () {
//            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
//        }
//    })
//})