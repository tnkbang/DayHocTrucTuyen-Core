$(document).ready(function () {
    var isLoad = true;
    var offsetCourses = 0;
    var limitCourses = 9;
    var textInput = $('#mySearchCourses').val();

    //Hàm khởi tạo phần tử lớp học
    function addCoursesItem(data) {
        var string = '<div class="col-lg-4 col-md-6 d-flex align-items-stretch mt-4 mt-md-0" style="padding-bottom: 20px" data-aos="zoom-in" data-aos-delay="100">' +
            '<a href="/courses/room/detail/' + data.maLop + '">' +
            '<div class="course-item">' +
            '<span class="price ' + data.loaiGiaTien + '">' + data.giaTien + '</span>' +
            '<img src="' + data.imgBg + '" class="img-fluid" alt="" />' +
            '<div class="course-content">' +
            '<div class="d-flex justify-content-between align-items-center mb-3">'

        $.each(data.tag, (index, value) => {
            string += '<h4>' + value.tenTag + '</h4>'
        })

        string += '</div>' +
            '<h3><a href="/courses/room/detail/' + data.maLop + '">' + data.tenLop + '</a></h3>' +
            '<p>' + data.moTa + '</p>' +
            '<div class="trainer d-flex justify-content-between align-items-center">' +
            '<div class="trainer-profile d-flex align-items-center">' +
            '<img width="40" height="40" src="' + data.ownerAvt + '" class="img-fluid" alt="" />' +
            '<a href="/profile/info/' + data.ownerMa + '" data-toggle="tooltip" title="' + data.ownerHoTen + '">' + data.ownerTen + '</a>' +
            '</div>' +
            '<div class="trainer-rank d-flex align-items-center">' +
            '<i class="bx bx-user"></i> &nbsp;' + data.thanhVien +
            '&nbsp;&nbsp;' +
            '<i class="bx bx-heart"></i>&nbsp;' + data.camXuc +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</a>' +
            '</div>'
        $('#courses > .container > .row').append(string);
    }

    //Sau khi web tải thì call về server lấy phần tử lần đầu tiên
    $.ajax({
        url: '/default/courses',
        type: 'GET',
        data: { q: textInput, offset: offsetCourses, limit: limitCourses },
        success: function (data) {
            $.each(data.result, function (index, value) {
                addCoursesItem(value)
                offsetCourses++
            })

            //Kiểm tra còn phần tử để tải thêm hay không
            if (data.result.length < limitCourses) {
                isLoad = false;
            }
            $('[data-toggle="tooltip"]').tooltip();
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })

    //Bắt sự kiện khi scroll trang lớp học Default/Courses
    $(window).on('scroll', function () {
        var main = document.querySelector('#courses > .container > .row');

        if ($('html').scrollTop() >= main.scrollHeight - 108 && isLoad) {
            //Gọi về server lấy thêm lớp học
            $.ajax({
                url: '/default/courses',
                type: 'GET',
                data: { q: textInput, offset: offsetCourses, limit: limitCourses },
                success: function (data) {
                    $.each(data.result, function (index, value) {
                        addCoursesItem(value)
                        offsetCourses++
                    })

                    //Kiểm tra còn phần tử để tải thêm hay không
                    if (data.result.length < limitCourses) {
                        isLoad = false;
                    }
                    $('[data-toggle="tooltip"]').tooltip();
                },
                error: function () {
                    getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
                }
            })
        }
    });
});