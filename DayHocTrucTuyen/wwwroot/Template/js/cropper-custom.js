window.addEventListener('DOMContentLoaded', function () {
    var avatar = document.getElementById('img-user-avt');
    var image = document.getElementById('cropper-avt-image');
    var input = document.getElementById('user-edit-avt');
    var $progress = $('.progress');
    var $progressBar = $('.progress-bar');
    var $modal = $('.popup-wraper2');
    var cropper;

    //Ẩn thanh tiến trình
    $progress.hide()

    //Bắt sự kiện khi chọn ảnh upload
    input.addEventListener('change', function (e) {
        var files = e.target.files;
        var done = function (url) {
            input.value = '';
            image.src = url;
            $modal.addClass('active')
            cropper = new Cropper(image, {
                aspectRatio: 1,
                viewMode: 3,
            });
        };
        var reader;
        var file;

        //Gán đã chọn vào vùng cropper
        if (files && files.length > 0) {
            file = files[0];

            //Kiểm tra đúng định dạng ảnh
            var anh = /(\.jpg|\.jpeg|\.png)$/i;
            if (!anh.exec(file.name)) {
                getThongBao('error', 'Lỗi', 'Định dạng ảnh không chính xác !')
                return;
            }

            if (URL) {
                done(URL.createObjectURL(file));
            } else if (FileReader) {
                reader = new FileReader();
                reader.onload = function (e) {
                    done(reader.result);
                };
                reader.readAsDataURL(file);
            }
        }
    });

    //Bắt sự kiện xác nhận sau khi đã chọn vùng cắt ảnh
    document.getElementById('cropper-confirm').addEventListener('click', function () {
        var initialAvatarURL;
        var canvas;

        if (cropper) {
            canvas = cropper.getCroppedCanvas({
                width: 160,
                height: 160,
            });
            initialAvatarURL = avatar.src;
            avatar.src = canvas.toDataURL();
            $progress.show();
            canvas.toBlob(function (blob) {
                var formData = new FormData();
                formData.append('avt', blob, 'avatar.jpg');

                //Gọi về server lưu ảnh
                $.ajax({
                    url: '/Profile/setAvt',
                    type: 'POST',
                    data: formData,
                    contentType: false,
                    processData: false,
                    xhr: function () {
                        var xhr = new XMLHttpRequest();
                        xhr.upload.onprogress = function (e) {
                            var percent = '0';
                            var percentage = '0%';

                            if (e.lengthComputable) {
                                percent = Math.round((e.loaded / e.total) * 100);
                                percentage = percent + '%';
                                $progressBar.width(percentage).attr('aria-valuenow', percent).text(percentage);
                            }
                        };
                        return xhr;
                    },
                    success: function (data) {
                        if (!data.tt) {
                            getThongBao('error', 'Lỗi !', 'Mã lệnh javascript đã bị thay đổi. Vui lòng tải lại trang !');
                        }
                        else {
                            $modal.removeClass('active')
                            getThongBao('success', 'Thành công', "Cập nhật ảnh đại diện thành công !")
                        }
                    },
                    error: function () {
                        cropper.destroy();
                        cropper = null;
                        avatar.src = initialAvatarURL;
                        getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
                    },
                    complete: function () {
                        $progress.hide();
                        cropper.destroy();
                        cropper = null;
                    }
                })
            });
        }
    });

    //Bắt sự kiện nhấn đóng popup
    $('.popup-closed, .cancel').on('click', function () {
        $modal.removeClass('active');
        cropper.destroy();
        cropper = null;
    });
});