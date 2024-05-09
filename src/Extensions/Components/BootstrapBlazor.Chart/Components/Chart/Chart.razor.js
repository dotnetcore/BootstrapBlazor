import '../../js/chart.js'
import '../../js/chartjs-plugin-datalabels.js'
import Data from '../../../BootstrapBlazor/modules/data.js'
import EventHandler from "../../../BootstrapBlazor/modules/event-handler.js"

Chart.register(ChartDataLabels);

const plugin = {
    id: 'customCanvasBackgroundColor',
    beforeDraw: (chart, args, options) => {
        if (options.color) {
            const { ctx } = chart;
            ctx.save();
            ctx.globalCompositeOperation = 'destination-over';
            ctx.fillStyle = options.color;
            ctx.fillRect(0, 0, chart.width, chart.height);
            ctx.restore();
        }
    }
};

const chartOption = {
    options: {
        responsive: true,
        maintainAspectRatio: true,
        aspectRatio: 2,
        resizeDelay: 0,
        plugins: {
            title: {
                display: true,
                text: null
            }
        },
        tooltips: {
            mode: 'index',
            intersect: false
        },
        hover: {
            mode: 'nearest',
            intersect: true
        },
        scales: {
            x: {
                display: true,
                title: {
                    display: false,
                    text: null
                }
            },
            y: {
                display: true,
                title: {
                    display: false,
                    text: null
                }
            }
        }
    },
    plugins: [plugin]
}

const skipped = (ctx, value) => ctx.p0.skip || ctx.p1.skip ? value : undefined
const down = (ctx, value) => ctx.p0.parsed.y > ctx.p1.parsed.y ? value : undefined

const genericOptions = {
    fill: false,
    interaction: {
        intersect: false
    },
    radius: 0
}

const getChartOption = function (option) {
    const colors = []
    const chartColors = option.options.colors
    for (const name in option.options.colors) colors.push(name)

    let config = {}
    let colorFunc = null

    let scale = {
        x: {
            display: option.options.showXScales,
            title: {
                display: option.options.x.title != null,
                text: option.options.x.title
            },
            stacked: option.options.x.stacked,
            grid: {
                display: option.options.showXLine
            }
        },
        y: {
            display: option.options.showYScales,
            title: {
                display: option.options.y.title != null,
                text: option.options.y.title
            },
            stacked: option.options.x.stacked,
            position: option.options.y.position,
            grid: {
                display: option.options.showYLine
            }
        }
    }

    if (option.options.xScalesBorderColor) {
        scale.x.border = {
            color: option.options.xScalesBorderColor
        }
    }

    if (option.options.yScalesBorderColor) {
        scale.y.border = {
            color: option.options.yScalesBorderColor
        }
    }

    if (option.options.xScalesGridColor) {
        scale.x.grid = {
            color: option.options.xScalesGridColor,
            borderColor: option.options.xScalesGridBorderColor,
            tickColor: option.options.xScalesGridTickColor
        }
    }

    if (option.options.yScalesGridColor) {
        scale.y.grid = {
            color: option.options.yScalesGridColor,
            borderColor: option.options.yScalesGridBorderColor,
            tickColor: option.options.yScalesGridTickColor
        }
    }

    let legend = {
        display: option.options.showLegend,
        position: option.options.legendPosition
    }

    if (option.options.legendLabelsFontSize > 0) {
        legend.labels = {
            font: {
                size: option.options.legendLabelsFontSize
            }
        }
    }

    if (option.type === 'line') {
        option.data.forEach(function (v, i) {
            if (!v.showPointStyle) {
                v.PointStyle = false;
            }
            v.data.forEach(function (d, j) {
                if (d === null) {
                    option.data[i].data[j] = NaN
                    option.data[i] = {
                        ...option.data[i],
                        ...{
                            segment: {
                                borderColor: ctx => skipped(ctx, 'rgb(0,0,0,0.2)') || down(ctx, 'rgb(192,75,75)'),
                                borderDash: ctx => skipped(ctx, [6, 6])
                            },
                            spanGaps: true
                        }
                    }
                }
            })
        })
        option.options = {
            ...option.options,
            ...genericOptions
        }

        config = chartOption

        if (option.options.barColorSeparately) {
            colorFunc = function (data) {
                data.borderWidth = 1
            }
        }
        else {
            colorFunc = function (data) {
                const color = chartColors[colors.shift()]

                data.backgroundColor = color
                data.borderColor = color
            }
        }
    }
    else if (option.type === 'bar') {
        config = {
            ...chartOption
        }

        if (option.options.barColorSeparately) {
            colorFunc = function (data) {
                data.borderWidth = 1
            }
        }
        else {
            colorFunc = function (data) {
                const color = chartColors[colors.shift()]

                data.backgroundColor = Chart.helpers.color(color).alpha(0.5).rgbString()
                data.borderColor = color
                data.borderWidth = 1
            }
        }
    }
    else if (option.type === 'pie' || option.type === 'doughnut') {
        config = {
            ...chartOption,
            ...{
                options: {
                    scales: {
                        x: {
                            display: false
                        },
                        y: {
                            display: false
                        }
                    }
                }
            }
        }
        colorFunc = function (data) {
            data.backgroundColor = colors.slice(0, data.data.length).map(function (name) {
                return chartColors[name]
            })
            data.borderColor = 'white'
        }

        if (option.type === 'doughnut') {
            config.options = {
                ...config.options,
                ...{
                    cutoutPercentage: 50,
                    animation: {
                        animateScale: true,
                        animateRotate: true
                    }
                }
            }
        }
    }
    else if (option.type === 'bubble') {
        config = {
            ...chartOption,
            ...{
                data: {
                    animation: {
                        duration: 10000
                    },
                },
                options: {
                    tooltips: {
                        mode: 'point'
                    }
                }
            }
        }
        colorFunc = function (data) {
            const color = chartColors[colors.shift()]
            data.backgroundColor = Chart.helpers.color(color).alpha(0.5).rgbString()
            data.borderWidth = 1
            data.borderColor = color
        }
    }

    option.data.forEach(function (v) {
        colorFunc(v)
    })

    if (option.options.y2.title != null) {
        scale.y2 = {
            title: {
                display: true,
                text: option.options.y2.title
            },
            stacked: option.options.x.stacked,
            position: option.options.y2.position,
            ticks: {
                max: option.options.y2.TicksMax,
                min: option.options.y2.TicksMin
            }
        }
    }

    // pie 图除外默认显示 网格线与坐标系
    if (option.type !== 'pie' && option.type !== 'doughnut') {
        if (option.options.showXScales === null) {
            scale.x.display = true
        }
        if (option.showXLine === null) {
            scale.x.grid.display = true
        }
        if (option.options.showYScales === null) {
            scale.y.display = true
        }
        if (option.options.showYLine === null) {
            scale.y.grid.display = true
        }
    }

    return {
        ...config,
        ...{
            type: option.type,
            data: {
                labels: option.labels,
                datasets: option.data
            },
            options: {
                responsive: option.options.responsive,
                maintainAspectRatio: option.options.maintainAspectRatio,
                aspectRatio: option.options.aspectRatio,
                resizeDelay: option.options.resizeDelay,
                plugins: {
                    legend: legend,
                    title: {
                        display: option.options.title != null,
                        text: option.options.title
                    },
                    datalabels: {
                        anchor: option.options.anchor,
                        align: option.options.align,
                        formatter: Math.round,
                        display: option.options.showDataLabel,
                        color: option.options.chartDataLabelColor,
                        font: {
                            weight: 'bold'
                        }
                    },
                    customCanvasBackgroundColor: {
                        color: option.options.canvasBackgroundColor,
                    }
                },
                scales: scale
            }
        }
    }
}

const updateChart = function (config, option) {
    if (option.updateMethod === "addDataset") {
        config.data.datasets.push(option.data.datasets.pop())
    }
    else if (option.updateMethod === "removeDataset") {
        config.data.datasets.pop()
    }
    else if (option.updateMethod === "addData") {
        if (config.data.datasets.length > 0) {
            config.data.labels.push(option.data.labels.pop())
            config.data.datasets.forEach(function (dataset, index) {
                dataset.data.push(option.data.datasets[index].data.pop())
                if (option.type === 'pie' || option.type === 'doughnut') {
                    dataset.backgroundColor.push(option.data.datasets[index].backgroundColor.pop())
                }
            })
        }
    }
    else if (option.updateMethod === "removeData") {
        config.data.labels.pop() // remove the label first

        config.data.datasets.forEach(function (dataset) {
            dataset.data.pop()
            if (option.type === 'pie' || option.type === 'doughnut') {
                dataset.backgroundColor.pop()
            }
        })
    }
    else if (option.updateMethod === "setAngle") {
        if (option.type === 'doughnut') {
            if (option.angle === 360) {
                config.options.circumference = 360
                config.options.rotation = -360
            }
            else {
                config.options.circumference = 180
                config.options.rotation = -90
            }
        }
    }
    else if (option.updateMethod === "reload") {
        config.data = option.data
        config.options = option.options
    }
    else {
        config.data.datasets.forEach((dataset, index) => {
            dataset.data = option.data.datasets[index].data
        })
    }
}

export function init(id, invoke, method, option) {
    const op = getChartOption(option);
    op.options.onClick = (event, elements, chart) => {
        if (elements.length > 0) {
            if (option.options.onClickDataMethod) {
                invoke.invokeMethodAsync(option.options.onClickDataMethod, elements[0].datasetIndex, elements[0].index);
            }
        }
    };
    const el = document.getElementById(id);
    const chart = new Chart(el.getElementsByTagName('canvas'), op)
    Data.set(id, chart)

    if (op.options.height !== null) {
        chart.canvas.parentNode.style.height = op.options.height
    }
    if (op.options.width !== null) {
        chart.canvas.parentNode.style.width = op.options.width
    }
    el.querySelector('.chart-loading').classList.add('d-none')
    invoke.invokeMethodAsync(method)

    chart.resizeHandler = () => {
        chart.resize();
    }

    EventHandler.on(window, 'resize', chart.resizeHandler)
}

export function update(id, option, method, angle) {
    const chart = Data.get(id)
    const op = getChartOption(option)
    op.angle = angle
    op.updateMethod = method
    updateChart(chart.config, op)
    chart.update()
}

function canvasToBlob(canvas, mimeType) {
    return new Promise((resolve, reject) => {
        canvas.toBlob(blob => {
            var reader = new FileReader();
            reader.onload = function (event) {
                var byteArray = new Uint8Array(event.target.result);
                resolve(byteArray);
            };
            reader.onerror = () => reject(new Error('Failed to read blob as array buffer'));
            reader.readAsArrayBuffer(blob);
        }, mimeType);
    });
}

export function toImage(id, mimeType) {
    return new Promise(async (resolve, reject) => {
        var div = document.getElementById(id);
        if (div) {
            var canvas = div.querySelector('canvas');
            if (canvas) {
                try {
                    const blobArray = await canvasToBlob(canvas, mimeType);
                    resolve(blobArray);
                } catch (error) {
                    reject(error);
                }
            } else {
                reject(new Error('No canvas found'));
            }
        } else {
            reject(new Error('No element with given id found'));
        }
    });
}

export function dispose(id) {
    const chart = Data.get(id)
    Data.remove(id)

    if (chart) {
        EventHandler.off(window, 'resize', chart.resizeHandler)
        chart.destroy()
    }
}
