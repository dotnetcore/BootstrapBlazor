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

export function customTotalDataLabel(id) {
    // chartjs-plugin-datalabels documentation: https://chartjs-plugin-datalabels.netlify.app/guide/options.html
    const chart = BootstrapBlazor.Chart;
    chart.setOptionsById(id, {
        options: {
            plugins: {
                datalabels: {
                    labels: {
                        total: {
                            color: '#fff',
                            backgroundColor: 'rgb(75, 192, 192)',
                            borderRadius: 4,
                            padding: { top: 2, bottom: 2, left: 6, right: 6 },
                            font: { weight: 'bold' },
                            formatter: (value, context) => {
                                const total = context.chart.data.datasets.reduce(
                                    (sum, v, index) => context.chart.isDatasetVisible(index) ? sum + (Number(v.data[context.dataIndex]) || 0) : sum, 0);
                                return `Total: ${total}`;
                            }
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
