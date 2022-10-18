
var $userlist = $('#table-user-list')

function thaotacFormatter(value, row, index) {
    var result = '<button data-toggle="tooltip" title="Xem" class="pd-setting-ed mr-1" onclick="window.location.href=\'/Profile/Info?id=' + row.maNd + '\'"><i class="fa fa-eye" aria-hidden="true"></i></button>';
    if (row.trangThai) {
        result += '<button data-toggle="tooltip" title="Khóa" class="pd-setting-ed" onclick="setUserLock(\'' + row.maNd + '\', this)" ><i data-toggle="modal" class="fa fa-lock" aria-hidden="true"></i></button>'
    }
    else {
        result += '<button data-toggle="tooltip" title="Mở khóa" class="pd-setting-ed" onclick="setUserLock(\'' + row.maNd + '\', this)" ><i data-toggle="modal" class="fa fa-unlock" aria-hidden="true"></i></button>'
    }
    return result;
}

window.operateEvents = {
    'click .like': function (e, value, row, index) {
        alert('You click like action, row: ' + JSON.stringify(row))
    },
    'click .remove': function (e, value, row, index) {
        console.log(row.maNd)
        $userlist.bootstrapTable('updateByUniqueId', {
            id: row.maNd,
            row: {
                hoLot: 'đã thay đổi',
                email: 'email đã đổi'
            }
        })
    }
}
// your custom ajax request here
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
        field: 'ngayTao',
        sortable: true,
        title: 'Ngày tạo'
    }, {
        field: 'trangThai',
        sortable: true,
        title: 'Trạng thái',
        formatter: (value, row, index) => { return row.trangThai ? 'Hoạt động' : 'Bị khóa' }
    }, {
        field: 'thaoTac',
        title: 'Thao tác',
        align: 'center',
        clickToSelect: false,
        events: window.operateEvents,
        formatter: thaotacFormatter
    }]
})
function ajaxRequest(params) {
    var url = '/Admin/User/getList'
    $.get(url + '?' + $.param(params.data)).then(function (res) {
        params.success(res)
        $('[data-toggle="tooltip"]').tooltip();
    })
}