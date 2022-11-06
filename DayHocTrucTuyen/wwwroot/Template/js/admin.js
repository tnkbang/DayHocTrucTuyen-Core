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
        formatter: (value, row, index) => { return '<img src="' + row.imgAvt + '" alt="' + row.hoTen + '" />' }
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
    var url = '/admin/user/getlist'
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
        url: '/admin/user/lockuser',
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

//--------------------------------------------------------------------------

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
        sortable: true,
        formatter: (value, row, index) => { return '<a href="/profile/info/' + row.maOwner + '">' + row.tenOwner + '</a>' },
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
    var url = '/admin/room/getlist'
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
        url: '/admin/room/lockroom',
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

//----------------------------------------------------------------------------

//Xử lý trang phê duyệt người dùng
var $tableApprove = $('#table-approve')

//Thêm các th row cho bảng phê duyệt người dùng
$tableApprove.bootstrapTable({
    columns: [{
        field: 'maNd',
        sortable: true,
        title: 'Mã'
    }, {
        field: 'hoTen',
        title: 'Họ tên'
    }, {
        field: 'email',
        title: 'Email',
        visible: false
    }, {
        field: 'gioiTinh',
        title: 'Giới tính'
    }, {
        field: 'sdt',
        title: 'Sđt'
    }, {
        field: 'ngaySinh',
        title: 'Ngày sinh'
    }, {
        field: 'biDanh',
        title: 'Bí danh',
        visible: false
    }, {
        field: 'ngayDK',
        title: 'Ngày đăng ký',
        sortable: true,
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

//Gọi ajax về server lấy dữ liệu cho danh sách phê duyệt
function ajaxGetApprove(params) {
    var url = '/admin/user/getapprove'
    $.get(url + '?' + $.param(params.data)).then(function (res) {
        params.success(res)
        $('[data-toggle="tooltip"]').tooltip();
    })
    $($('input[type="search"]').parent()[0]).addClass('col-sm-12 col-md-4 col-lg-4 p-0')
}

//Xử lý khóa đồng ý hoặc không đồng ý phê duyệt giáo viên
var thisApprove, userApprove;

//Đồng ý phê duyệt
function approveAccept(maUser) {
    thisApprove = true;
    userApprove = maUser;

    var title = document.getElementById('modal-approve-title');
    var content = document.getElementById('modal-approve-content');

    title.innerHTML = 'Đồng ý nâng cấp giáo viên?'
    content.innerHTML = 'Khi người dùng thành giáo viên thì họ có thể tự do tạo lớp học và tiến hành thu phí lớp học. Bạn thật sự chắc chắn về hành động này ?'

    $('.popup-wraper1').addClass('active');
}

//Từ chối phê duyệt
function approveRefuse(maUser) {
    thisApprove = false;
    userApprove = maUser;

    var title = document.getElementById('modal-approve-title');
    var content = document.getElementById('modal-approve-content');

    title.innerHTML = 'Từ chối nâng cấp giáo viên?'
    content.innerHTML = 'Hãy nêu lý do từ chối:'
        + '<textarea class="main-inp" id="approve-text" maxlength="200" placeholder="Nhập nội dung..."></textarea>'

    $('.popup-wraper1').addClass('active');
}

$('#cancel-approve').on('click', function () {
    thisApprove = userApprove = null;
    $('.popup-wraper1').removeClass('active');
})

$('#confirm-approve').on('click', function () {
    event.preventDefault();

    if (!thisApprove && $('#approve-text').val() == '') {
        getThongBao('error', 'Lỗi', 'Hãy nhập nội dung ghi chú !')
        return;
    }

    $.ajax({
        url: '/admin/user/setapprove',
        type: 'POST',
        data: { ma: userApprove, tt: thisApprove, gc: $('#approve-text').val() },
        success: function (data) {

            $tableApprove.bootstrapTable('remove', {
                field: 'maNd',
                values: userApprove
            })

            if (thisApprove) {
                getThongBao('success', 'Thành công', 'Người dùng được nâng cấp thành giáo viên !')
            }
            else {
                getThongBao('success', 'Thành công', 'Đã từ chối nâng cấp lên giáo viên !')
            }

            thisApprove = userApprove = null;
            $('.popup-wraper1').removeClass('active');
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })
})

//-----------------------------------------------------------------------------

//Xử lý trang xem phản hồi và báo cáo
var $reportlist = $('#table-report-list')

//Hàm set hiển thị chỉ mục báo cáo
function setChiMucReport(value, row, index) {
    if (row.chiMuc.length == 11) {
        return 'Lớp ' + '<a href="/courses/room/detail/' + row.chiMuc + '">' + row.tenLop + '</a>';
    }
    return 'Bài đăng của ' + '<a href="/courses/room/detail/' + row.chiMuc.slice(0, 11) + '#' + row.chiMuc + '">' + row.tenLop + '</a>';
}

//Thêm các th row cho bảng phản hồi và báo cáo
$reportlist.bootstrapTable({
    columns: [{
        field: 'state',
        checkbox: true,
        valign: 'middle'
    }, {
        field: 'maBaoCao',
        sortable: true,
        title: 'Mã',
        visible: false
    }, {
        field: 'tenOwner',
        sortable: true,
        title: 'Người báo cáo',
        formatter: (value, row, index) => { return '<a href="/profile/info/' + row.maOwner + '">' + row.tenOwner + '</a>' }
    }, {
        field: 'chiMuc',
        sortable: true,
        title: 'Chỉ mục',
        formatter: setChiMucReport
    }, {
        field: 'noiDung',
        sortable: true,
        title: 'Nội dung'
    }, {
        field: 'ghiChu',
        title: 'Ghi chú'
    }, {
        field: 'thoiGian',
        sortable: true,
        title: 'Thời gian',
        visible: false
    }, {
        field: 'thaoTac',
        title: 'Thao tác',
        align: 'center',
        clickToSelect: false
    }]
})

//Gọi ajax về server lấy dữ liệu cho danh sách báo cáo
function ajaxGetListReport(params) {
    var url = '/admin/room/getreport'
    $.get(url + '?' + $.param(params.data)).then(function (res) {
        params.success(res)
        $('[data-toggle="tooltip"]').tooltip();
    })
    $($('input[type="search"]').parent()[0]).addClass('col-sm-12 col-md-4 col-lg-4 p-0')
}

//Biến lưu người dùng để gửi tin
var thisUserReport;

//Gửi tin nhắn cho người dùng báo cáo
function setSendNoti(maUser) {
    thisUserReport = maUser;

    var title = document.getElementById('modal-send-noti-title');
    var content = document.getElementById('modal-send-noti-content');

    title.innerHTML = 'Gửi thông báo cho người dùng?'
    content.innerHTML = 'Nhập nội dung tin nhắn:'
        + '<textarea class="main-inp mb-0" id="send-noti-text" maxlength="200" placeholder="Nhập nội dung..."></textarea>'

    $('.popup-wraper1').addClass('active');
}

$('#cancel-send-noti').on('click', function () {
    thisUserReport = null;
    $('.popup-wraper1').removeClass('active');
})

$('#confirm-send-noti').on('click', function () {
    event.preventDefault();

    if (!thisUserReport && $('#send-noti-text').val() == '') {
        getThongBao('error', 'Lỗi', 'Hãy nhập nội dung thông báo !')
        return;
    }

    $.ajax({
        url: '/admin/room/sendnoti',
        type: 'POST',
        data: { maUser: thisUserReport, nd: $('#send-noti-text').val() },
        success: function (data) {
            getThongBao('success', 'Thành công', 'Đã gửi thông báo đến người dùng !')

            thisUserReport = null;
            $('.popup-wraper1').removeClass('active');
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })
})

//Hàm xóa báo cáo người dùng
function setRemoveReport(maBaoCao, elm) {
    $.ajax({
        url: '/admin/room/removereport',
        type: 'POST',
        data: { ma: maBaoCao },
        success: function (data) {
            $reportlist.bootstrapTable('remove', {
                field: 'maBaoCao',
                values: maBaoCao
            })

            getThongBao('success', 'Thành công', 'Đã xóa báo cáo !')

            $(elm).tooltip("hide");
            $('[data-toggle="tooltip"]').tooltip();
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })
}

//Xử lý xóa nhiều phần tử đã chọn cho danh sách báo cáo
var $reportRemoveButton = $('#remove-report-select')
var reportSelections = []

//Hàm lấy list mã báo cáo đã chọn
function getReportSelections() {
    return $.map($reportlist.bootstrapTable('getSelections'), function (row) {
        return row.maBaoCao
    })
}

//Gán hoặc xóa mã báo cáo cần xóa vào list khi chọn
$reportlist.on('check.bs.table uncheck.bs.table ' + 'check-all.bs.table uncheck-all.bs.table', function () {
    $reportRemoveButton.prop('disabled', !$reportlist.bootstrapTable('getSelections').length)
    reportSelections = getReportSelections()
})

//Sự kiện nhấn nút xóa
$reportRemoveButton.click(function () {
    var listMaBaoCao = getReportSelections()

    $.ajax({
        url: '/admin/room/removereport',
        type: 'POST',
        data: { ma: listMaBaoCao },
        success: function (data) {
            if (data.tt) {
                $reportlist.bootstrapTable('remove', {
                    field: 'maBaoCao',
                    values: listMaBaoCao
                })

                getThongBao('success', 'Thành công', 'Đã xóa các báo cáo đã chọn !')
                $reportRemoveButton.prop('disabled', true)
            }
            else {
                getThongBao('error', 'Lỗi', 'Source code bị thay đổi !')
            }
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })
})

//---------------------------------------------------------------------------

//Xử lý trang chuyển tiền cho người dùng
var $tableGiveMoney = $('#table-give-money')

//Thêm các th row cho bảng xem yêu cầu chuyển tiền
$tableGiveMoney.bootstrapTable({
    columns: [{
        field: 'maNd',
        sortable: true,
        title: 'Mã'
    }, {
        field: 'hoTen',
        title: 'Họ tên'
    }, {
        field: 'email',
        title: 'Email',
        visible: false
    }, {
        field: 'sdt',
        title: 'Sđt'
    }, {
        field: 'loaiThanhToan',
        title: 'Loại'
    }, {
        field: 'soTaiKhoan',
        title: 'Tài khoản'
    }, {
        field: 'soTien',
        title: 'Số tiền'
    }, {
        field: 'thoiGian',
        sortable: true,
        title: 'Thời gian',
        visible: false
    }, {
        field: 'thaoTac',
        title: 'Thao tác',
        align: 'center',
        clickToSelect: false
    }]
})

//Gọi ajax về server lấy dữ liệu cho danh sách yêu cầu chuyển tiền
function ajaxGetGiveMoney(params) {
    var url = '/admin/user/getgivemoney'
    $.get(url + '?' + $.param(params.data)).then(function (res) {
        params.success(res)
        $('[data-toggle="tooltip"]').tooltip();
    })
    $($('input[type="search"]').parent()[0]).addClass('col-sm-12 col-md-4 col-lg-4 p-0')
}

//Xử lý đã chuyển hoặc không đồng ý chuyển tiền cho người dùng
var thisGiveMoney, userGiveMoney;

//Xác nhận đã chuyển tiền
function giveMoneyAccept(maUser) {
    thisGiveMoney = true;
    userGiveMoney = maUser;

    var title = document.getElementById('modal-give-money-title');
    var content = document.getElementById('modal-give-money-content');

    title.innerHTML = 'Xác nhận đã chuyển tiền?'
    content.innerHTML = 'Khi bạn xác nhận, hệ thống sẽ hiểu rằng bạn đã chuyển tiền cho người dùng này rồi. Bạn thật sự chắc chắn về hành động này ?'

    $('.popup-wraper1').addClass('active');
}

//Từ chối chuyển tiền
function giveMoneyRefuse(maUser) {
    thisGiveMoney = false;
    userGiveMoney = maUser;

    var title = document.getElementById('modal-give-money-title');
    var content = document.getElementById('modal-give-money-content');

    title.innerHTML = 'Từ chối chuyển tiền?'
    content.innerHTML = 'Hãy nêu lý do từ chối:'
        + '<textarea class="main-inp" id="give-money-text" maxlength="200" placeholder="Nhập nội dung..."></textarea>'

    $('.popup-wraper1').addClass('active');
}

$('#cancel-give-money').on('click', function () {
    thisGiveMoney = userGiveMoney = null;
    $('.popup-wraper1').removeClass('active');
})

$('#confirm-give-money').on('click', function () {
    event.preventDefault();

    if (!thisGiveMoney && $('#give-money-text').val() == '') {
        getThongBao('error', 'Lỗi', 'Hãy nhập nội dung ghi chú !')
        return;
    }

    $.ajax({
        url: '/admin/user/setgivemoney',
        type: 'POST',
        data: { ma: userGiveMoney, tt: thisGiveMoney, gc: $('#give-money-text').val() },
        success: function (data) {

            $tableGiveMoney.bootstrapTable('remove', {
                field: 'maNd',
                values: userGiveMoney
            })

            if (thisGiveMoney) {
                getThongBao('success', 'Thành công', 'Đã xác nhận chuyển tiền cho người dùng !')
            }
            else {
                getThongBao('success', 'Thành công', 'Đã từ chối chuyển tiền cho người dùng !')
            }

            thisGiveMoney = userGiveMoney = null;
            $('.popup-wraper1').removeClass('active');
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })
})