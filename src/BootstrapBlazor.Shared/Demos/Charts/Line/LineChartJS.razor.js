//通过相对路径导入BootstrapBlazor.Chart的Chart.JS模块
import '../../../../../_content/BootstrapBlazor.Chart/js/chart.js'
import Data from '../../../../../_content/BootstrapBlazor/modules/data.js'

export function lineChart(canvasId, chartData) {
    const ctx = document.getElementById(canvasId);

    const data = {
        labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
        datasets: [{
            label: 'BootStrapBlazor.Chart',
            data: chartData,
            fill: false,
            borderColor: 'rgb(75, 192, 192)',
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

    Data.set(canvasId, new Chart(ctx, config))
}

export function randomize(canvasId, chartData) {
    const chart = Data.get(canvasId)
    if (chart) {
        chart.data.datasets.forEach(dataset => {
            dataset.data = chartData;
        });
        chart.update();
    }
}

export function dispose(canvasId) {
    Data.remove(canvasId)
}
