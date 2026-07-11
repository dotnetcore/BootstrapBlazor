import '../../../_content/BootstrapBlazor.Chart/Components/Chart/Chart.razor.js'

export function customCategoryLabel(id) {
    // chart.js documentation: https://www.chartjs.org/docs/latest/axes/labelling.html
    const chart = BootstrapBlazor.Chart;
    chart.setOptionsById(id, {
        options: {
            scales: {
                x: {
                    ticks: {
                        callback: function (value) {
                            return `Day ${this.getLabelForValue(value)}`;
                        }
                    }
                }
            }
        }
    });
}

export function customTooltip(id) {
    // chart.js documentation: https://www.chartjs.org/docs/latest/configuration/tooltip.html
    const chart = BootstrapBlazor.Chart;
    chart.setOptionsById(id, {
        options: {
            plugins: {
                tooltip: {
                    callbacks: {
                        title: tooltipItems => `Day ${tooltipItems[0].label}`,
                        label: context => ` ${context.dataset.label}: ${context.parsed.y} units`
                    }
                }
            }
        }
    });
}
