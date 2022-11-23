// Page Loader : hide loader when all are loaded
$(window).load(function () {
    "use strict";
    $('.wavy-wraper').addClass('hidden');
});

//Kiểm tra nhập lại mật khẩu
function passValidate(id_pass, re_pass, kt_pass) {
    var pass = document.getElementById(id_pass);
    var pass_re = document.getElementById(re_pass);
    var tb = document.getElementById(kt_pass);
    if (pass.value == pass_re.value) {
        pass_re.style.borderColor = 'green';
        pass.style.borderColor = 'green';
        tb.style.display = 'none';
        return true;
    }
    else {
        pass_re.style.borderColor = 'red';
        pass.style.borderColor = 'red';
        tb.innerHTML = 'Xác nhận mật khẩu sai !';
        tb.style.display = 'block';
        $('#' + kt_pass).delay(1000).hide(1);
        return false;
    }
}

//Hàm đăng ký
function dangky() {
    $.getJSON('/account/createaccount' + '?holot=' + $('#holotCreate').val() + '&ten=' + $('#tenCreate').val() + '&email=' + $('#emailCreate').val() + '&matkhau=' + $('#passCreate').val(), function (data) {
        if (data.tt) {
            window.location.href = "/";
        }
        else {
            if (data.erro == "email") {
                document.getElementById('emailCreate').style.borderColor = 'red';
                document.getElementById('erroCreate').innerHTML = data.mess;
                $('#erroCreate').show('slow');
                $('#erroCreate').delay(2000).hide('slow');
                $('#emailCreate').focus();
            }
            else {
                document.getElementById('erroCreate').innerHTML = data.mess;
                $('#erroCreate').show('slow');
                $('#erroCreate').delay(2000).hide('slow');
            }
        }
    })
}

//Hàm đăng nhập
function dangnhap() {
    var remember = document.getElementById('rememberLogin');
    $.getJSON('/account/getlogin' + '?email=' + $('#emailLogin').val() + '&pass=' + $('#passLogin').val() + '&re=' + remember.checked, function (data) {
        if (data.tt) {
            window.location.href = $('#urlreturnLogin').val();
        }
        else {
            document.getElementById('erroLogin').innerHTML = data.mess;
            $('#erroLogin').show('slow');
            $('#erroLogin').delay(3000).hide('slow');
        }
    })
}

//Hàm đăng xuất
function dangxuat() {
    $.getJSON('/account/logout', function (data) {
        location.replace('/account/login');
    })
}

//Bắt sự kiện khi nhấn đăng nhập
$('#formLogin').on('submit', function () {
    event.preventDefault();
    dangnhap()
})

//Thay đổi text của label nhập lại mật khẩu
$('#nhaplaiCreate').on('focus', function () {
    document.getElementById('lblnhaplaiCreate').innerHTML = 'Xác nhận';
})
$('#nhaplaiCreate').on('focusout', function () {
    if ($('#nhaplaiCreate').val() == null || $('#nhaplaiCreate').val() == '') {
        document.getElementById('lblnhaplaiCreate').innerHTML = 'Nhập lại mật khẩu';
    }
})

//Thay đổi text của label nhập lại mật khẩu tại trang reset pass
$('#nhaplaiResetPassword').on('focus', function () {
    document.getElementById('lblnhaplaiResetPassword').innerHTML = 'Xác nhận';
})
$('#nhaplaiResetPassword').on('focusout', function () {
    if ($('#nhaplaiResetPassword').val() == null || $('#nhaplaiResetPassword').val() == '') {
        document.getElementById('lblnhaplaiResetPassword').innerHTML = 'Nhập lại mật khẩu';
    }
})

//Bắt sự kiện khi nhấn đăng ký
$('#formCreate').on('submit', function () {
    event.preventDefault();

    if (!passValidate('passCreate', 'nhaplaiCreate', 'ktnhaplaiCreate')) {
        return;
    }
    dangky()
})

//Bắt sự kiện check box hiển thị mật khẩu
$('.viewPass').on('click', function () {
    if (this.checked) {
        document.querySelector('.passInput').type = 'text';
    }
    else {
        document.querySelector('.passInput').type = 'password';
    }
})

//Bắt sự kiện nhấn xác nhận reset mật khẩu
$('#formForgotPassword').on('submit', function () {
    event.preventDefault();
    $('.process-wraper').removeClass('hidden');

    $.ajax({
        url: '/account/getforgotpassword',
        type: 'POST',
        data: { email: $('#emailForgotPassword').val() },
        success: function (data) {
            if (!data.tt) {
                $('#errorForgotPassword').html(data.mess);
                $('#errorForgotPassword').show('slow');
                $('#errorForgotPassword').delay(2000).hide('slow');
                $('#errorForgotPassword').focus();
            }
            else {
                $('.reset_email').html($('#emailForgotPassword').val());
                $('.popup-wraper1').addClass('active');
            }

            $('.process-wraper').addClass('hidden');
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })
})

//Bắt sự kiện đổi mật khẩu
$('#formResetPassword').on('submit', function () {
    event.preventDefault();

    if (!passValidate('passResetPassword', 'nhaplaiResetPassword', 'ktnhaplaiResetPassword')) {
        return;
    }

    $('.process-wraper').removeClass('hidden');

    $.ajax({
        url: '/account/setresetpassword',
        type: 'POST',
        data: { email: $('#emailResetPassword').val(), pass: $('#passResetPassword').val() },
        success: function (data) {
            if (!data.tt) {
                getThongBao('error', 'Lỗi', "Vui lòng thực hiện lại sau!");
            }
            else {
                getThongBao('success', 'Thành công', "Bạn đã thay đổi mật khẩu!");
                setTimeout(() => {
                    window.location.href = '/'
                }, 1000);
            }

            $('.process-wraper').addClass('hidden');
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })
})

//Bắt sự kiện đóng popup
$('.popup-closed, .popup-hide').on('click', function () {
    $('.popup-wraper1').removeClass('active');
});