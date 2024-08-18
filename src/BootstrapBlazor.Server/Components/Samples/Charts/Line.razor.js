//通过相对路径导入BootstrapBlazor.Chart的Chart.JS模块
import '../../../_content/BootstrapBlazor.Chart/js/chart.js'
import Data from '../../../_content/BootstrapBlazor/modules/data.js'

export function init(id, chartData) {
    const ctx = document.getElementById(id);

    const data = {
        labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
        datasets: [{
            label: 'BootStrapBlazor.Chart',
            data: chartData,
            fill: false,
            borderColor: 'rgb(75, 192, 192)',
            pointStyle: 'circle',
            pointRadius: 5,
            pointHoverRadius: 10
        }]
    };

    const config = {
        type: 'line',
        data: data,
        options: {
            plugins: {
                legend: {
                    labels: {
                        font: {
                            size: 26,
                            style: 'italic'
                        }
                    }
                }
            },
            animations: {
                tension: {
                    duration: 1000,
                    easing: 'linear',
                    from: 1,
                    to: 0,
                    loop: true
                }
            },
            scales: {
                y: {
                    min: 0,
                    max: 100
                }
            }
        }
    };

    Data.set(id, new Chart(ctx, config))
}

export function randomize(id, chartData) {
    const chart = Data.get(id)
    if (chart) {
        chart.data.datasets.forEach(dataset => {
            dataset.data = chartData;
        });
        chart.update();
    }
}

export function dispose(id) {
    Data.remove(id)
}

export function customTooltip(id, invoke, method) {
    // chart.js documentation: https://www.chartjs.org/docs/master/samples/tooltip/content.html
    const chart = BootstrapBlazor.Chart;
    chart.setOptionsById(id, {
        options: {
            interaction: {
                intersect: false,
                mode: 'index',
            },
            plugins: {
                tooltip: {
                    callbacks: {
                        footer: (tooltipItems) => {
                            let sum = 0;

                            tooltipItems.forEach(function (tooltipItem) {
                                sum += tooltipItem.parsed.y;
                            });
                            invoke.invokeMethodAsync(method, sum);
                            return 'Sum: ' + sum;
                        }
                    }
                }
            }
        }
    });
}
