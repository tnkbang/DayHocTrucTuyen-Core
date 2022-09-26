function autocomplete(inp, arr) {
    var currentFocus;

    //Bắt sự kiện nhập vào
    inp.addEventListener("input", function (e) {
        var a, b, i, val = this.value;

        //Xóa sạch danh sách tên đã gợi ý
        closeAllLists();

        if (!val) { return false; }
        currentFocus = -1;
        
        a = document.createElement("div");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        
        this.parentNode.appendChild(a);

        //Đọc dữ liệu từ mảng chứa tên lớp
        for (i = 0; i < arr.length; i++) {

            //Nếu nhập hợp lệ
            if (arr[i].toUpperCase().includes(val.toUpperCase())) {

                //Tạo item chứa tên lớp
                b = document.createElement("div");

                //Nếu ký tự trùng hợp không bắt đầu bằng ký tự đầu tiên
                if (arr[i].toUpperCase().indexOf(val.toUpperCase()) != 0) {
                    b.innerHTML = arr[i].substr(0, arr[i].toUpperCase().indexOf(val.toUpperCase()));
                    b.innerHTML += "<strong>" + arr[i].substr(arr[i].toUpperCase().indexOf(val.toUpperCase()), val.length) + "</strong>";
                    b.innerHTML += arr[i].substr(arr[i].toUpperCase().indexOf(val.toUpperCase()) + val.length);
                    b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                }

                //Nếu ký tự trùng hợp bắt đầu bằng ký tự đầu tiên
                else {
                    b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                    b.innerHTML += arr[i].substr(val.length);
                    b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                }

                //Gán sự kiện click cho item
                b.addEventListener("click", function (e) {

                    //Gán tên lớp đã chọn vào input tìm kiếm
                    inp.value = this.getElementsByTagName("input")[0].value;

                    //Khi nhấn vào thì xóa hết gợi ý
                    closeAllLists();

                    //Sau khi đã chọn kết quả gợi ý thì cho phép tìm kiếm
                    isSearch = true;
                });
                a.appendChild(b);
            }
        }
    });

    //Bắt sự kiện dò danh sách bằng các phím mũi tên
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");

        //Nếu nhấn nút mũi tên lên thì tăng vị trí trỏ
        if (e.keyCode == 40) {
            currentFocus++;
            addActive(x);

            //Khi di chuyển mũi tên thì không kích hoạt tìm kiếm
            isSearch = false;
        }

        //Nếu mũi tên xuống thì giảm vị trí trỏ
        else if (e.keyCode == 38) {
            currentFocus--;
            addActive(x);

            //Khi di chuyển mũi tên thì không kích hoạt tìm kiếm
            isSearch = false;
        }

        //Nếu nhấn enter thì gọi sự kiện click
        else if (e.keyCode == 13) {
            e.preventDefault();
            if (currentFocus > -1) {
                if (x) x[currentFocus].click();
            }
        }
    });

    //Hàm gán vị trí trỏ
    function addActive(x) {
        if (!x) return false;

        //Xóa tất cả item chứa active
        removeActive(x);

        //Kiểm tra nếu di chuyển vượt quá giới hạn
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);

        //Thêm active cho item đang trỏ
        x[currentFocus].classList.add("autocomplete-active");
    }

    //Hàm xóa vị trí trỏ
    function removeActive(x) {
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }

    //Hàm xóa hết danh sách đề xuất
    function closeAllLists(elmnt) {
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }

    //bắt sự kiện khi click vào item trong list đề xuất
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
}

//Biến kiểm tra gọi hàm tìm kiếm
var isSearch = true;

//Tự khởi động khi load xong
$(document).ready(function () {
    $.ajax({
        url: '/Courses/Room/getAllCourses',
        type: 'POST',
        success: function (data) {
            if (!data.tt) {
                getThongBao('error', 'Lỗi !', 'Mã lệnh javascript đã bị thay đổi. Vui lòng tải lại trang !');
            }
            else {
                //Chạy hàm đề xuất
                autocomplete(document.getElementById("mySearchCourses"), data.lst);
            }
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })


    //Hàm set tìm kiếm
    function setTimLopHoc(e){
        e.preventDefault();

        var link = window.location.href;
        var key = $('#mySearchCourses').val();

        //Nếu chưa tìm kiếm
        if (link.indexOf('?') == -1) {
            //Nếu nhấn tìm kiếm và không nhập gì
            if (!key) {
                window.location.replace(link);
            }
            else {
                window.location.replace(link + '?q=' + key);
            }
        }
        else {
            if (!key) {
                window.location.replace(link.slice(0, link.indexOf('?')));
            }
            else {
                window.location.replace(link.slice(0, link.indexOf('?')) + '?q=' + key);
            }
        }
    }

    //Tìm kiếm khi nhấn enter
    $('#mySearchCourses').on('keydown', function (e) {
        if (e.keyCode == 13) {
            if (isSearch) {
                setTimLopHoc(e);
            }
        }
    });

    //Tìm kiếm khi nhấn nút tìm
    $('#form-search-room').on('submit', function (e) {
        setTimLopHoc(e);
    })


    //Gọi hàm xử lý nhận diện giọng nói khi web load xong
    speech_recognition();
});


//Hàm xử lý nhận diện giọng nói
function speech_recognition() {
    const searchForm = document.querySelector("#form-search-room");
    const searchFormInput = searchForm.querySelector("input");
    //Sử dụng nhận diện giọng nói dựa trên web API
    const SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;

    //Nếu trình duyệt hỗ trợ
    if (SpeechRecognition) {
        const recognition = new SpeechRecognition();
        recognition.continuous = true;
        // recognition.lang = "en-US";

        //Thêm micro vào input
        searchForm.insertAdjacentHTML("beforeend", '<button class="record" style="right: 40px" type="button"><i class="fa fa-microphone"></i></button>');
        const micBtn = searchForm.querySelector(".record");
        const micIcon = micBtn.firstElementChild;

        //Xử lý khi nhấn vào mic
        micBtn.addEventListener("click", function () {
            $('.text-record').text('Đang nghe...');

            //Nếu mic chưa mở thì bắt đầu
            if (micIcon.classList.contains("fa-microphone")) {
                recognition.start();
            }
            else {
                recognition.stop();
            }
        });

        //Bắt sự kiện onstart
        recognition.addEventListener("start", function () {
            micIcon.classList.remove("fa-microphone");
            micIcon.classList.add("fa-microphone-slash");
            searchFormInput.focus();
            $('.call-wraper').addClass('active');
            //console.log("Đang lắng nghe");
        });

        //Bắt sự kiện kết thúc lắng nghe
        recognition.addEventListener("end", function () {
            micIcon.classList.remove("fa-microphone-slash");
            micIcon.classList.add("fa-microphone");
            searchFormInput.focus();
            $('.call-wraper').removeClass('active');
            //console.log("Đã kết thúc lắng nghe");
        });

        //Bắt sự kiện kết quả trả về trong quá trình lắng nghe
        recognition.addEventListener("result", function () {
            const current = event.resultIndex;
            const transcript = event.results[current][0].transcript;

            if (transcript.toLowerCase().trim() === "dừng lại") {
                recognition.stop();
            }
            else if (transcript.toLowerCase().trim() === "làm mới") {
                searchFormInput.value = "";
                $('.text-record').text('Đang nghe...');
            }
            else {
                $('.text-record').text(transcript);

                searchFormInput.value = transcript;
                setTimeout(() => {
                    recognition.stop();
                }, 500)
            }

            // setTimeout(() => {
            //   searchForm.submit();
            // }, 500);
        });

        //Xử lý nút tắt popup
        $('.close-record').on('click', function () {
            recognition.stop();
        })
    }
    else {
        getThongBao('error', 'Lỗi', 'Trình duyệt của bạn không hỗ trợ tính năng này !')
    }
}