//通过相对路径导入BootstrapBlazor.Chart的Chart.JS模块
import '/../_content/BootstrapBlazor.Chart/js/bootstrap.blazor.chart.bundle.min.js'

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

    var chart = new Chart(ctx, config);
    window[canvasId] = chart;
}

export function randomize(canvasId, chartData) {
    window[canvasId].data.datasets.forEach(dataset => {
        dataset.data = chartData;
    });
    window[canvasId].update();
}
