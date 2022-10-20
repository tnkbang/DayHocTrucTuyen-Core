//Xử lý trang quản lý người dùng
var $userlist = $('#table-user-list')

//Thêm các th row cho bảng danh sách người dùng
$userlist.bootstrapTable({
    columns: [{
        field: 'maNd',
        sortable: true,
        title: 'Mã'
    }, {
        field: 'imgAvt',
        title: 'Ảnh',
        formatter: (value, row, index) => { return '<img src="' + row.imgAvt + '" alt="' + row.hoLot + ' ' + row.ten + '" />' }
    }, {
        field: 'hoTen',
        sortable: true,
        title: 'Họ tên'
    }, {
        field: 'email',
        sortable: true,
        title: 'Email'
    }, {
        field: 'gioiTinh',
        sortable: true,
        title: 'Giới tính',
        visible: false
    }, {
        field: 'sdt',
        title: 'Sđt',
        visible: false
    }, {
        field: 'biDanh',
        title: 'Bí danh',
        visible: false
    }, {
        field: 'loai',
        sortable: true,
        title: 'Loại'
    }, {
        field: 'trangThai',
        sortable: true,
        title: 'Trạng thái'
    }, {
        field: 'thaoTac',
        title: 'Thao tác',
        align: 'center',
        clickToSelect: false
    }]
})

//Gọi ajax về server lấy dữ liệu cho danh sách người dùng
function ajaxGetListUser(params) {
    var url = '/Admin/User/getList'
    $.get(url + '?' + $.param(params.data)).then(function (res) {
        params.success(res)
        $('[data-toggle="tooltip"]').tooltip();
    })
    $($('input[type="search"]').parent()[0]).addClass('col-sm-12 col-md-4 col-lg-4 p-0')
}

//Xử lý khóa hoặc mở khóa người dùng
var thisUserLock;
function setUserLock(maUser, elm) {
    thisUserLock = maUser;

    var title = document.getElementById('modal-lock-user-title');
    var content = document.getElementById('modal-lock-user-content');
    var btn = document.getElementById('confirm-lock-user');

    if ($(elm).find(">:first-child").hasClass('fa-lock')) {
        title.innerHTML = 'Bạn thật sự muốn khóa?'
        content.innerHTML = 'Khi khóa người dùng, tài khoản này sẽ không thể đăng nhập vào hệ thống và thực hiện các chức năng. Bạn thật sự chắc chắn về hành động này ?'
        btn.innerHTML = 'Khóa'
    }
    else {
        title.innerHTML = 'Bạn thật sự muốn mở khóa?'
        content.innerHTML = 'Khi mở khóa người dùng, tài khoản này sẽ khôi phục hoạt động và có thể thao tác với các chức năng trong hệ thống. Bạn thật sự chắc chắn về hành động này ?'
        btn.innerHTML = 'Mở khóa'
    }

    $('.popup-wraper1').addClass('active');
}

$('#cancel-lock-user').on('click', function () {
    thisUserLock = null;
    $('.popup-wraper1').removeClass('active');
})

$('#confirm-lock-user').on('click', function () {
    event.preventDefault();

    $.ajax({
        url: '/Admin/User/LockUser',
        type: 'POST',
        data: { ma: thisUserLock },
        success: function (data) {
            if (data.tt) {
                $userlist.bootstrapTable('updateByUniqueId', {
                    id: thisUserLock,
                    row: {
                        trangThai: 'Hoạt động',
                        thaoTac: data.thaoTac
                    }
                })

                getThongBao('success', 'Thành công', 'Mở khóa người dùng thành công !')
            }
            else {
                $userlist.bootstrapTable('updateByUniqueId', {
                    id: thisUserLock,
                    row: {
                        trangThai: 'Bị khóa',
                        thaoTac: data.thaoTac
                    }
                })

                getThongBao('success', 'Thành công', 'Khóa người dùng thành công !')
            }

            thisUserLock = null;
            $('[data-toggle="tooltip"]').tooltip();
            $('.popup-wraper1').removeClass('active');
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })
})

//Xử lý trang quản lý lớp học
var $roomlist = $('#table-room-list')

//Thêm các th row cho bảng danh sách lớp học
$roomlist.bootstrapTable({
    columns: [{
        field: 'maLop',
        sortable: true,
        title: 'Mã'
    }, {
        field: 'tenLop',
        sortable: true,
        title: 'Tên lớp'
    }, {
        field: 'tenOwner',
        title: 'Tác giả',
        formatter: (value, row, index) => { return '<a href="/Profile/Info?id=' + row.maOwner + '">' + row.tenOwner + '</a>' },
        visible: false
    }, {
        field: 'giaTien',
        sortable: true,
        title: 'Giá tiền',
        formatter: (value, row, index) => { return row.giaTien + " VNĐ" },
    }, {
        field: 'moTa',
        title: 'Mô tả',
        visible: false
    }, {
        field: 'ngayTao',
        sortable: true,
        title: 'Ngày tạo'
    }, {
        field: 'biDanh',
        title: 'Bí danh',
        visible: false
    }, {
        field: 'trangThai',
        sortable: true,
        title: 'Trạng thái'
    }, {
        field: 'thaoTac',
        title: 'Thao tác',
        align: 'center',
        clickToSelect: false
    }]
})

//Gọi ajax về server lấy dữ liệu cho danh sách lớp học
function ajaxGetListRoom(params) {
    var url = '/Admin/Room/getList'
    $.get(url + '?' + $.param(params.data)).then(function (res) {
        params.success(res)
        $('[data-toggle="tooltip"]').tooltip();
    })
    $($('input[type="search"]').parent()[0]).addClass('col-sm-12 col-md-4 col-lg-4 p-0')
}

//Xử lý khóa hoặc mở khóa lớp học
var thisRoomLock;
function setRoomLock(maLop, elm) {
    thisRoomLock = maLop;

    var title = document.getElementById('modal-lock-room-title');
    var content = document.getElementById('modal-lock-room-content');
    var btn = document.getElementById('confirm-lock-room');

    if ($(elm).find(">:first-child").hasClass('fa-lock')) {
        title.innerHTML = 'Bạn thật sự muốn khóa?'
        content.innerHTML = 'Khi khóa lớp học, những thành viên thuộc lớp sẽ không thấy nội dung của lớp và thực hiện các chức năng. Bạn thật sự chắc chắn về hành động này ?'
        btn.innerHTML = 'Khóa'
    }
    else {
        title.innerHTML = 'Bạn thật sự muốn mở khóa?'
        content.innerHTML = 'Khi mở khóa lớp học, những thành viên thuộc lớp có thể thấy nội dung của lớp và tiến hành thực hiện các chức năng của lớp. Bạn thật sự chắc chắn về hành động này ?'
        btn.innerHTML = 'Mở khóa'
    }

    $('.popup-wraper1').addClass('active');
}

$('#cancel-lock-room').on('click', function () {
    thisRoomLock = null;
    $('.popup-wraper1').removeClass('active');
})

$('#confirm-lock-room').on('click', function () {
    event.preventDefault();

    $.ajax({
        url: '/Admin/Room/LockRoom',
        type: 'POST',
        data: { ma: thisRoomLock },
        success: function (data) {
            if (data.tt) {
                $roomlist.bootstrapTable('updateByUniqueId', {
                    id: thisRoomLock,
                    row: {
                        trangThai: 'Hoạt động',
                        thaoTac: data.thaoTac
                    }
                })

                getThongBao('success', 'Thành công', 'Mở khóa lớp học thành công !')
            }
            else {
                $roomlist.bootstrapTable('updateByUniqueId', {
                    id: thisRoomLock,
                    row: {
                        trangThai: 'Bị khóa',
                        thaoTac: data.thaoTac
                    }
                })

                getThongBao('success', 'Thành công', 'Khóa lớp học thành công !')
            }

            thisRoomLock = null;
            $('[data-toggle="tooltip"]').tooltip();
            $('.popup-wraper1').removeClass('active');
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })
})