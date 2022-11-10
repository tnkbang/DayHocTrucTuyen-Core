
function init_echarts() {

    if (typeof (echarts) === 'undefined') { return; }
    //console.log('init_echarts');


    var theme = {
        color: [
            '#26B99A', '#34495E', '#fa6342', '#3498DB',
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


    //echart Bar

    if ($('#mainb').length) {

        var echartBar = echarts.init(document.getElementById('mainb'), theme);

        echartBar.setOption({
            title: {
                text: 'Graph title',
                subtext: 'Graph Sub-text'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['sales', 'purchases']
            },
            toolbox: {
                show: false
            },
            calculable: false,
            xAxis: [{
                type: 'category',
                data: ['1?', '2?', '3?', '4?', '5?', '6?', '7?', '8?', '9?', '10?', '11?', '12?']
            }],
            yAxis: [{
                type: 'value'
            }],
            series: [{
                name: 'sales',
                type: 'bar',
                data: [2.0, 4.9, 7.0, 23.2, 25.6, 76.7, 135.6, 162.2, 32.6, 20.0, 6.4, 3.3],
                markPoint: {
                    data: [{
                        type: 'max',
                        name: '???'
                    }, {
                        type: 'min',
                        name: '???'
                    }]
                },
                markLine: {
                    data: [{
                        type: 'average',
                        name: '???'
                    }]
                }
            }, {
                name: 'purchases',
                type: 'bar',
                data: [2.6, 5.9, 9.0, 26.4, 28.7, 70.7, 175.6, 182.2, 48.7, 18.8, 6.0, 2.3],
                markPoint: {
                    data: [{
                        name: 'sales',
                        value: 182.2,
                        xAxis: 7,
                        yAxis: 183,
                    }, {
                        name: 'purchases',
                        value: 2.3,
                        xAxis: 11,
                        yAxis: 3
                    }]
                },
                markLine: {
                    data: [{
                        type: 'average',
                        name: '???'
                    }]
                }
            }]
        });

    }


    //echart Line

    if ($('#echart_line').length) {

        var echartLine = echarts.init(document.getElementById('echart_line'), theme);

        echartLine.setOption({
            title: {
                text: '',
                subtext: 'Subtitle'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                x: 220,
                y: 40,
                data: ['Intent', 'Pre-order', 'Deal']
            },
            toolbox: {
                show: true,
                feature: {
                    magicType: {
                        show: true,
                        title: {
                            line: 'Line',
                            bar: 'Bar',
                            stack: 'Stack',
                            tiled: 'Tiled'
                        },
                        type: ['line', 'bar', 'stack', 'tiled']
                    },
                    restore: {
                        show: true,
                        title: "Restore"
                    },
                    saveAsImage: {
                        show: true,
                        title: "Save Image"
                    }
                }
            },
            calculable: true,
            xAxis: [{
                type: 'category',
                boundaryGap: false,
                data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
            }],
            yAxis: [{
                type: 'value'
            }],
            series: [{
                name: 'Deal',
                type: 'line',
                smooth: true,
                itemStyle: {
                    normal: {
                        areaStyle: {
                            type: 'default'
                        }
                    }
                },
                data: [10, 12, 21, 54, 260, 830, 710]
            }, {
                name: 'Pre-order',
                type: 'line',
                smooth: true,
                itemStyle: {
                    normal: {
                        areaStyle: {
                            type: 'default'
                        }
                    }
                },
                data: [30, 182, 434, 791, 390, 30, 10]
            }, {
                name: 'Intent',
                type: 'line',
                smooth: true,
                itemStyle: {
                    normal: {
                        areaStyle: {
                            type: 'default'
                        }
                    }
                },
                data: [1320, 1132, 601, 234, 120, 90, 20]
            }]
        });

    }



    //echart Bar Horizontal

    if ($('#echart_bar_horizontal').length) {

        var echartBar = echarts.init(document.getElementById('echart_bar_horizontal'), theme);

        echartBar.setOption({
            title: {
                text: 'Bar Graph',
                subtext: 'Graph subtitle'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                x: 100,
                data: ['2015', '2016']
            },
            toolbox: {
                show: true,
                feature: {
                    saveAsImage: {
                        show: true,
                        title: "Save Image"
                    }
                }
            },
            calculable: true,
            xAxis: [{
                type: 'value',
                boundaryGap: [0, 0.01]
            }],
            yAxis: [{
                type: 'category',
                data: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun']
            }],
            series: [{
                name: '2015',
                type: 'bar',
                data: [18203, 23489, 29034, 104970, 131744, 630230]
            }, {
                name: '2016',
                type: 'bar',
                data: [19325, 23438, 31000, 121594, 134141, 681807]
            }]
        });

    }

    //echart Pie Collapse

    if ($('#echart_pie2').length) {

        var echartPieCollapse = echarts.init(document.getElementById('echart_pie2'), theme);

        echartPieCollapse.setOption({
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                x: 'center',
                y: 'bottom',
                data: ['rose1', 'rose2', 'rose3', 'rose4', 'rose5', 'rose6']
            },
            toolbox: {
                show: true,
                feature: {
                    magicType: {
                        show: true,
                        type: ['pie', 'funnel']
                    },
                    restore: {
                        show: true,
                        title: "Restore"
                    },
                    saveAsImage: {
                        show: true,
                        title: "Save Image"
                    }
                }
            },
            calculable: true,
            series: [{
                name: 'Area Mode',
                type: 'pie',
                radius: [25, 90],
                center: ['50%', 170],
                roseType: 'area',
                x: '50%',
                max: 40,
                sort: 'ascending',
                data: [{
                    value: 10,
                    name: 'rose1'
                }, {
                    value: 5,
                    name: 'rose2'
                }, {
                    value: 15,
                    name: 'rose3'
                }, {
                    value: 25,
                    name: 'rose4'
                }, {
                    value: 20,
                    name: 'rose5'
                }, {
                    value: 35,
                    name: 'rose6'
                }]
            }]
        });

    }

    //echart Donut

    if ($('#echart_donut').length) {

        var echartDonut = echarts.init(document.getElementById('echart_donut'), theme);

        echartDonut.setOption({
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            calculable: true,
            legend: {
                x: 'center',
                y: 'bottom',
                data: ['Direct Access', 'E-mail Marketing', 'Union Ad', 'Video Ads', 'Search Engine']
            },
            toolbox: {
                show: true,
                feature: {
                    magicType: {
                        show: true,
                        type: ['pie', 'funnel'],
                        option: {
                            funnel: {
                                x: '25%',
                                width: '50%',
                                funnelAlign: 'center',
                                max: 1548
                            }
                        }
                    },
                    restore: {
                        show: true,
                        title: "Restore"
                    },
                    saveAsImage: {
                        show: true,
                        title: "Save Image"
                    }
                }
            },
            series: [{
                name: 'Access to the resource',
                type: 'pie',
                radius: ['35%', '55%'],
                itemStyle: {
                    normal: {
                        label: {
                            show: true
                        },
                        labelLine: {
                            show: true
                        }
                    },
                    emphasis: {
                        label: {
                            show: true,
                            position: 'center',
                            textStyle: {
                                fontSize: '14',
                                fontWeight: 'normal'
                            }
                        }
                    }
                },
                data: [{
                    value: 335,
                    name: 'Direct Access'
                }, {
                    value: 310,
                    name: 'E-mail Marketing'
                }, {
                    value: 234,
                    name: 'Union Ad'
                }, {
                    value: 135,
                    name: 'Video Ads'
                }, {
                    value: 1548,
                    name: 'Search Engine'
                }]
            }]
        });

    }

    //echart Pie

    if ($('#echart_pie').length) {

        var echartPie = echarts.init(document.getElementById('echart_pie'), theme);

        echartPie.setOption({
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                x: 'center',
                y: 'bottom',
                data: ['Direct Access', 'E-mail Marketing', 'Union Ad', 'Video Ads', 'Search Engine']
            },
            toolbox: {
                show: true,
                feature: {
                    magicType: {
                        show: true,
                        type: ['pie', 'funnel'],
                        option: {
                            funnel: {
                                x: '25%',
                                width: '50%',
                                funnelAlign: 'left',
                                max: 1548
                            }
                        }
                    },
                    restore: {
                        show: true,
                        title: "Restore"
                    },
                    saveAsImage: {
                        show: true,
                        title: "Save Image"
                    }
                }
            },
            calculable: true,
            series: [{
                name: '访问来源',
                type: 'pie',
                radius: '55%',
                center: ['50%', '48%'],
                data: [{
                    value: 335,
                    name: 'Direct Access'
                }, {
                    value: 310,
                    name: 'E-mail Marketing'
                }, {
                    value: 234,
                    name: 'Union Ad'
                }, {
                    value: 135,
                    name: 'Video Ads'
                }, {
                    value: 1548,
                    name: 'Search Engine'
                }]
            }]
        });

        var dataStyle = {
            normal: {
                label: {
                    show: false
                },
                labelLine: {
                    show: false
                }
            }
        };

        var placeHolderStyle = {
            normal: {
                color: 'rgba(0,0,0,0)',
                label: {
                    show: false
                },
                labelLine: {
                    show: false
                }
            },
            emphasis: {
                color: 'rgba(0,0,0,0)'
            }
        };

    }

    //echart Mini Pie

    if ($('#echart_mini_pie').length) {

        var echartMiniPie = echarts.init(document.getElementById('echart_mini_pie'), theme);

        echartMiniPie.setOption({
            title: {
                text: 'Devices',
                subtext: 'Computer, Tab, Mobile',
                sublink: '',
                x: 'center',
                y: 'center',
                itemGap: 20,
                textStyle: {
                    color: 'rgba(30,144,255,0.8)',
                    fontFamily: '微软雅黑',
                    fontSize: 25,
                    fontWeight: 'bolder'
                }
            },
            tooltip: {
                show: true,
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 30,
                y: 45,
                itemGap: 12,
                data: ['68%Something #1', '29%Something #2', '3%Something #3'],
            },
            toolbox: {
                show: true,
                feature: {
                    mark: {
                        show: true
                    },
                    dataView: {
                        show: true,
                        title: "Text View",
                        lang: [
                            "Text View",
                            "Close",
                            "Refresh",
                        ],
                        readOnly: false
                    },
                    restore: {
                        show: true,
                        title: "Restore"
                    },
                    saveAsImage: {
                        show: true,
                        title: "Save Image"
                    }
                }
            },
            series: [{
                name: '1',
                type: 'pie',
                clockWise: false,
                radius: [105, 130],
                itemStyle: dataStyle,
                data: [{
                    value: 68,
                    name: '68%Something #1'
                }, {
                    value: 32,
                    name: 'invisible',
                    itemStyle: placeHolderStyle
                }]
            }, {
                name: '2',
                type: 'pie',
                clockWise: false,
                radius: [80, 105],
                itemStyle: dataStyle,
                data: [{
                    value: 29,
                    name: '29%Something #2'
                }, {
                    value: 71,
                    name: 'invisible',
                    itemStyle: placeHolderStyle
                }]
            }, {
                name: '3',
                type: 'pie',
                clockWise: false,
                radius: [25, 80],
                itemStyle: dataStyle,
                data: [{
                    value: 3,
                    name: '3%Something #3'
                }, {
                    value: 97,
                    name: 'invisible',
                    itemStyle: placeHolderStyle
                }]
            }]
        });

    }

}

// based ready dom, initialize echarts instance 
var myChart = echarts.init(document.getElementById('main'));

// Specify configurations and data graphs 
var option = {
    title: {
        text: '',
        subtext: ''
    },
    tooltip: {
        trigger: 'axis'
    },
    legend: {
        x: 10,
        y: 0,
        orient: 'horizontal',
        data: ['Organic visitor', 'Return visitor'],
    },

    xAxis: [
        {
            type: 'category',
            boundaryGap: false,
            data: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday']
        }
    ],
    yAxis: [
        {
            type: 'value',
            axisLabel: {
                formatter: '{value} '
            }
        }
    ],
    series: [
        {
            name: 'Organic visitor',
            type: 'line',
            data: [11, 11, 12, 13, 12, 13, 10],
            markPoint: {
                data: [
                    { type: 'max', name: 'Max' },
                    { type: 'min', name: 'Min' }
                ]
            },
            markLine: {
                data: [
                    { type: 'average', name: 'Average' },
                ]
            }
        },
        {
            name: 'Return visitor',
            type: 'line',
            data: [1, 2, 2, 5, 3, 2, 0],
            markPoint: {
                data: [
                    { name: 'Week Low', value: -2, xAxis: 1, yAxis: -1.5 }
                ]
            },
            markLine: {
                data: [
                    { type: 'average', name: 'Average Value' }
                ]
            }
        }
    ]
};

// Use just the specified configurations and data charts. 
myChart.setOption(option);

option = {
    xAxis: {
        type: 'category',
        data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
    },
    yAxis: {
        type: 'value'
    },
    series: [{
        data: [820, 932, 901, 934, 1290, 1330, 1320],
        type: 'line',
        smooth: true
    }]
};

$(document).ready(function () {
    init_echarts();
});
