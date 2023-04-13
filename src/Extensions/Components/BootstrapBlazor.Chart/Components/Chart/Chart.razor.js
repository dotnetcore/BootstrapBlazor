import '../../js/chart.js'
import Data from '../../../BootstrapBlazor/modules/data.js'
import { addLink } from "../../../BootstrapBlazor/modules/utility.js"

const chartOption = {
    options: {
        legend: {
            display: true
        },
        borderWidth: 3,
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
    }
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
    let scale = {}
    let colorFunc = null
    if (option.type === 'line') {
        option.data.forEach(function (v, i) {
            v.data.forEach(function (d, j) {
                if (d === null) {
                    option.data[i].data[j] = NaN
                    option.data[i].segment = {
                        borderColor: ctx => skipped(ctx, 'rgb(0,0,0,0.2)'),
                        borderDash: ctx => skipped(ctx, [6, 6])
                    }
                }
            })
        })
        option.options = {
            ...option.options,
            ...genericOptions
        }

        if (option.options.borderWidth > 0) {
            chartOption.options.borderWidth = option.options.borderWidth
        }
        config = chartOption
        colorFunc = function (data) {
            const color = chartColors[colors.shift()]

            data.backgroundColor = color
            data.borderColor = color
        }
    }
    else if (option.type === 'bar') {
        config = {
            ...chartOption
        }
        colorFunc = function (data) {
            const color = chartColors[colors.shift()]

            data.backgroundColor = Chart.helpers.color(color).alpha(0.5).rgbString()
            data.borderColor = color
            data.borderWidth = 1
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

    scale = {
        x: {
            title: {
                display: option.options.x.title != null,
                text: option.options.x.title
            },
            stacked: option.options.x.stacked
        },
        y: {
            title: {
                display: option.options.y.title != null,
                text: option.options.y.title
            },
            stacked: option.options.x.stacked,
            position: option.options.y.position
        }
    }

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
                    legend: {
                        display: option.options.displayLegend
                    },
                    title: {
                        display: option.options.title != null,
                        text: option.options.title
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

export async function init(el, obj, method, option) {
    await addLink('_content/BootstrapBlazor.Chart/css/bootstrap.blazor.chart.css')
    el.classList.add('is-loading')
    el.classList.remove('d-none')

    const op = getChartOption(option)
    const chart = new Chart(el.getElementsByTagName('canvas'), op)
    Data.set(el, chart)

    if (option.options.height !== null) {
        chart.canvas.parentNode.style.height = option.options.height
    }
    if (option.options.width !== null) {
        chart.canvas.parentNode.style.width = option.options.width
    }
    el.classList.remove('is-loading')
    obj.invokeMethodAsync(method)
}

export function update(el, option, method, angle) {
    const chart = Data.get(el)
    const op = getChartOption(option)
    op.angle = angle
    op.updateMethod = method
    updateChart(chart.config, op)
    chart.update()
}

export function dispose(el) {
    Data.remove(el)
}
