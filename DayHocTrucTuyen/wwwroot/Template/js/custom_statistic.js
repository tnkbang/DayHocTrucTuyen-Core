var theme = {
    color: [
        '#26B99A', '#3498DB', '#fa6342', '#34495E',
        '#8c6ad2', '#8abb6f', '#759c6a', '#bfd3b7'
    ],

    title: {
        itemGap: 8,
        textStyle: {
            fontWeight: 'normal',
            color: '#535165'
        }
    },

    dataRange: {
        color: ['#1f610a', '#97b58d']
    },

    toolbox: {
        color: ['#408829', '#408829', '#408829', '#408829']
    },

    tooltip: {
        backgroundColor: 'rgba(0,0,0,0.5)',
        axisPointer: {
            type: 'line',
            lineStyle: {
                color: '#408829',
                type: 'dashed'
            },
            crossStyle: {
                color: '#408829'
            },
            shadowStyle: {
                color: 'rgba(200,200,200,0.3)'
            }
        }
    },

    dataZoom: {
        dataBackgroundColor: '#eee',
        fillerColor: 'rgba(64,136,41,0.2)',
        handleColor: '#408829'
    },
    grid: {
        borderWidth: 0
    },

    categoryAxis: {
        axisLine: {
            lineStyle: {
                color: '#408829'
            }
        },
        splitLine: {
            lineStyle: {
                color: ['#eee']
            }
        }
    },

    valueAxis: {
        axisLine: {
            lineStyle: {
                color: '#408829'
            }
        },
        splitArea: {
            show: true,
            areaStyle: {
                color: ['rgba(250,250,250,0.1)', 'rgba(200,200,200,0.1)']
            }
        },
        splitLine: {
            lineStyle: {
                color: ['#eee']
            }
        }
    },
    timeline: {
        lineStyle: {
            color: '#408829'
        },
        controlStyle: {
            normal: { color: '#408829' },
            emphasis: { color: '#408829' }
        }
    },

    k: {
        itemStyle: {
            normal: {
                color: '#68a54a',
                color0: '#a9cba2',
                lineStyle: {
                    width: 1,
                    color: '#408829',
                    color0: '#86b379'
                }
            }
        }
    },
    map: {
        itemStyle: {
            normal: {
                areaStyle: {
                    color: '#ddd'
                },
                label: {
                    textStyle: {
                        color: '#c12e34'
                    }
                }
            },
            emphasis: {
                areaStyle: {
                    color: '#99d2dd'
                },
                label: {
                    textStyle: {
                        color: '#c12e34'
                    }
                }
            }
        }
    },
    force: {
        itemStyle: {
            normal: {
                linkStyle: {
                    strokeColor: '#408829'
                }
            }
        }
    },
    chord: {
        padding: 4,
        itemStyle: {
            normal: {
                lineStyle: {
                    width: 1,
                    color: 'rgba(128, 128, 128, 0.5)'
                },
                chordStyle: {
                    lineStyle: {
                        width: 1,
                        color: 'rgba(128, 128, 128, 0.5)'
                    }
                }
            },
            emphasis: {
                lineStyle: {
                    width: 1,
                    color: 'rgba(128, 128, 128, 0.5)'
                },
                chordStyle: {
                    lineStyle: {
                        width: 1,
                        color: 'rgba(128, 128, 128, 0.5)'
                    }
                }
            }
        }
    },
    gauge: {
        startAngle: 225,
        endAngle: -45,
        axisLine: {
            show: true,
            lineStyle: {
                color: [[0.2, '#86b379'], [0.8, '#68a54a'], [1, '#408829']],
                width: 8
            }
        },
        axisTick: {
            splitNumber: 10,
            length: 12,
            lineStyle: {
                color: 'auto'
            }
        },
        axisLabel: {
            textStyle: {
                color: 'auto'
            }
        },
        splitLine: {
            length: 18,
            lineStyle: {
                color: 'auto'
            }
        },
        pointer: {
            length: '90%',
            color: 'auto'
        },
        title: {
            textStyle: {
                color: '#333'
            }
        },
        detail: {
            textStyle: {
                color: 'auto'
            }
        }
    },
    textStyle: {
        fontFamily: 'Arial, Verdana, sans-serif'
    }
};

$(document).ready(function () {
    //Nếu đang mở trang xem lịch sử giao dịch
    if ($('#echart_transhistory').length) {
        $.ajax({
            url: '/user/manage/gettranshisofyear',
            type: 'GET',
            success: function (data) {
                setChartTransHis(data.thu, data.chi, data.sodu)
            },
            error: function () {
                getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
            }
        })
    }
});

//Hàm bắt sự kiện mở thống kê lớp học
function setTKPostAndMems(malop) {
    if ($('#echart_postandmems').length) {
        $.ajax({
            url: '/courses/room/getpostandmemsofweek',
            type: 'GET',
            data: { maLop: malop },
            success: function (data) {
                setChartPostAndMems(data.mems, data.post)
                $('.popup-wraper5').addClass('active')
            },
            error: function () {
                getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
            }
        })
    }
}

$('#close-statistic').on('click', () => {
    $('.popup-wraper5').removeClass('active')
})

//-------------------------------------------------

//Xử lý trang lịch sử giao dịch
//Hàm hiển thị biểu đồ
function setChartTransHis(thu, chi, sodu) {
    if (typeof (echarts) === 'undefined') { return; }
    var echartLine = echarts.init(document.getElementById('echart_line'), theme);

    echartLine.setOption({
        title: {
            text: '',
            subtext: 'Biểu đồ thống kê'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            x: 220,
            y: 40,
            data: ['Thu', 'Chi', 'Số dư']
        },
        toolbox: {
            show: true,
            feature: {
                magicType: {
                    show: true,
                    title: {
                        line: 'Dòng',
                        bar: 'Cột',
                        stack: 'Xếp chồng',
                        tiled: 'Phân nhóm'
                    },
                    type: ['line', 'bar', 'stack', 'tiled']
                },
                restore: {
                    show: true,
                    title: "Làm mới"
                },
                saveAsImage: {
                    show: true,
                    title: "Lưu ảnh"
                }
            }
        },
        calculable: true,
        xAxis: [{
            type: 'category',
            boundaryGap: false,
            data: ['Thg 1', 'Thg 2', 'Thg 3', 'Thg 4', 'Thg 5', 'Thg 6', 'Thg 7', 'Thg 8', 'Thg 9', 'Thg 10', 'Thg 11', 'Thg 12']
        }],
        yAxis: [{
            type: 'value'
        }],
        series: [{
            name: 'Số dư',
            type: 'bar',
            smooth: true,
            itemStyle: {
                normal: {
                    areaStyle: {
                        type: 'default'
                    }
                }
            },
            data: sodu
        }, {
            name: 'Thu',
            type: 'bar',
            smooth: true,
            itemStyle: {
                normal: {
                    areaStyle: {
                        type: 'default'
                    }
                }
            },
            data: thu
        }, {
            name: 'Chi',
            type: 'bar',
            smooth: true,
            itemStyle: {
                normal: {
                    areaStyle: {
                        type: 'default'
                    }
                }
            },
            data: chi
        }]
    });
}

//--------------------------------------------------

//Xử lý trang lớp học
//Hàm hiển thị biểu đồ
function setChartPostAndMems(mems, post) {
    if (typeof (echarts) === 'undefined') { return; }
    var echartLine = echarts.init(document.getElementById('echart_line'), theme);

    echartLine.setOption({
        title: {
            text: '',
            subtext: 'Thống kê thành viên và bài đăng trong tuần'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            x: 220,
            y: 40,
            data: ['Thành viên', 'Bài đăng']
        },
        toolbox: {
            show: true,
            feature: {
                magicType: {
                    show: true,
                    title: {
                        line: 'Dòng',
                        bar: 'Cột',
                        stack: 'Xếp chồng',
                        tiled: 'Phân nhóm'
                    },
                    type: ['line', 'bar', 'stack', 'tiled']
                },
                restore: {
                    show: true,
                    title: "Làm mới"
                },
                saveAsImage: {
                    show: true,
                    title: "Lưu ảnh"
                }
            }
        },
        calculable: true,
        xAxis: [{
            type: 'category',
            boundaryGap: false,
            data: ['Chủ nhật', 'Thứ 2', 'Thứ 3', 'Thứ 4', 'Thứ 5', 'Thứ 6', 'Thứ 7']
        }],
        yAxis: [{
            type: 'value'
        }],
        series: [{
            name: 'Thành viên',
            type: 'bar',
            smooth: true,
            itemStyle: {
                normal: {
                    areaStyle: {
                        type: 'default'
                    }
                }
            },
            data: mems
        }, {
            name: 'Bài đăng',
            type: 'bar',
            smooth: true,
            itemStyle: {
                normal: {
                    areaStyle: {
                        type: 'default'
                    }
                }
            },
            data: post
        }]
    });
}
