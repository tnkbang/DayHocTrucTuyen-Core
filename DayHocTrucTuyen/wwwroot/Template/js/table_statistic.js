//Xử lý trang lịch sử giao dịch
//Xử lý table
var $tableTransHis = $('#table-transhis')

//Hàm xử lý hiển thị tăng hoặc giảm
function setFormatUpOrDown(value, row, index) {
    if (row.thuVao) {
        return '<i class="fa fa-long-arrow-up text-success"></i> ' + row.soTien + ' VNĐ';
    }
    return '<i class="fa fa-long-arrow-down text-danger"></i> ' + row.soTien + ' VNĐ';
}

//Thêm các th row cho bảng lịch sử giao dịch
$tableTransHis.bootstrapTable({
    columns: [{
        field: 'thoiGian',
        sortable: true,
        title: 'Thời gian'
    }, {
        field: 'soTien',
        title: 'Số tiền',
        sortable: true,
        formatter: setFormatUpOrDown
    }, {
        field: 'soDu',
        title: 'Số dư',
        formatter: (value, row, index) => { return row.soDu + ' VNĐ' }
    }, {
        field: 'ghiChu',
        title: 'Ghi chú'
    }]
})

//Gọi ajax về server lấy dữ liệu cho bảng lịch sử giao dịch
function ajaxGetTransHis(params) {
    var url = '/user/manage/gettranshistable'
    $.get(url + '?' + $.param(params.data)).then(function (res) {
        params.success(res)
        $('[data-toggle="tooltip"]').tooltip();
    })
    $($('input[type="search"]').parent()[0]).addClass('col-sm-12 col-md-4 col-lg-4 p-0')
}

//---------------------------------------
//Xử lý trang quản lý bài thi
//Xử lý table
var $tablePointExam = $('#table-point-exam')

//Thêm các th row cho bảng xem điểm bài thi
$tablePointExam.bootstrapTable({
    height: 400,
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
        field: 'luotThi',
        title: 'Lượt thi'
    }, {
        field: 'diemCaoNhat',
        sortable: true,
        title: 'Điểm cao nhất'
    }]
})

//Gọi ajax về server lấy dữ liệu cho bảng lịch sử giao dịch
function ajaxGetPointExam(params) {
    var exam = document.getElementById('modal-statistic-exam-title').dataset.examcode;
    var url = '/courses/exam/getpointexam'
    $.get(url + '?examcode=' + exam + '&' + $.param(params.data)).then(function (res) {
        params.success(res)
        $('[data-toggle="tooltip"]').tooltip();
    })
    $($('input[type="search"]').parent()[0]).addClass('col-sm-12 col-md-4 col-lg-4 p-0')
}