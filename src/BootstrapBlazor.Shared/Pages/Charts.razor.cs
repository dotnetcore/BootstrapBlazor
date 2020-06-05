using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class Charts
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private ToastService? ToastService { get; set; }

        [Inject]
        private IJSRuntime? JSRuntime { get; set; }

        private static Random Randomer { get; set; } = new Random();

        private JSInterop<Charts>? Interope { get; set; }

        private Chart? LineChart { get; set; }

        private Chart? BarChart { get; set; }

        private Chart? PieChart { get; set; }

        private Chart? DoughnutChart { get; set; }

        private Chart? BubbleChart { get; set; }

        private bool IsCricle { get; set; }

        private IEnumerable<string> Colors { get; set; } = new List<string>() { "Red", "Blue", "Green", "Orange", "Yellow", "Tomato", "Pink", "Violet" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && JSRuntime != null)
            {
                if (Interope == null) Interope = new JSInterop<Charts>(JSRuntime);
                await Interope.Invoke(this, "", "_initChart", nameof(ShowToast));
            }
        }

        private Task<ChartDataSource> OnInit(int dsCount, int daCount)
        {
            var ds = new ChartDataSource();
            ds.Options.XAxes.Add(new ChartAxes() { LabelString = "天数" });
            ds.Options.YAxes.Add(new ChartAxes() { LabelString = "数值" });

            ds.Labels = Enumerable.Range(1, daCount).Select(i => i.ToString());

            for (var index = 0; index < dsCount; index++)
            {
                ds.Data.Add(new ChartDataset()
                {
                    Label = $"数据集 {index}",
                    Data = Enumerable.Range(1, daCount).Select(i => Randomer.Next(20, 37)).Cast<object>()
                });
            }
            return Task.FromResult(ds);
        }

        private CancellationTokenSource _chartCancellationTokenSource = new CancellationTokenSource();

        private Task OnPlayChart()
        {
            _chartCancellationTokenSource = new CancellationTokenSource();
            return Task.Run(async () =>
            {
                while (!_chartCancellationTokenSource.IsCancellationRequested)
                {
                    await Task.Delay(800, _chartCancellationTokenSource.Token);
                    if (!_chartCancellationTokenSource.IsCancellationRequested) RandomData(BarChart);
                }
            });
        }

        private void OnStopChart()
        {
            _chartCancellationTokenSource.Cancel();
        }

        private Task<ChartDataSource> OnPieInit(int dsCount, int daCount)
        {
            var ds = new ChartDataSource();
            ds.Options.XAxes.Add(new ChartAxes() { LabelString = "天数" });
            ds.Options.ShowXAxesLine = false;

            ds.Options.YAxes.Add(new ChartAxes() { LabelString = "数值" });
            ds.Options.ShowYAxesLine = false;

            ds.Labels = Colors.Take(daCount);

            for (var index = 0; index < dsCount; index++)
            {
                ds.Data.Add(new ChartDataset()
                {
                    Label = $"数据集 {index}",
                    Data = Enumerable.Range(1, daCount).Select(i => Randomer.Next(20, 37)).Cast<object>()
                });
            }
            return Task.FromResult(ds);
        }

        private Task<ChartDataSource> OnBubbleInit(int dsCount, int daCount)
        {
            var ds = new ChartDataSource();
            ds.Labels = Enumerable.Range(1, daCount).Select(i => i.ToString());

            for (var index = 0; index < dsCount; index++)
            {
                ds.Data.Add(new ChartDataset()
                {
                    Label = $"数据集 {index}",
                    Data = Enumerable.Range(1, daCount).Select(i => new
                    {
                        x = Randomer.Next(10, 40),
                        y = Randomer.Next(10, 40),
                        r = Randomer.Next(1, 20)
                    })
                });
            }
            return Task.FromResult(ds);
        }

        private void RandomData(Chart? chart)
        {
            chart?.Update();
        }

        private void AddDataSet(Chart? chart, ref int dsCount)
        {
            if (dsCount < Colors.Count())
            {
                dsCount++;
                chart?.Update("addDataset");
            }
        }

        private void RemoveDataSet(Chart? chart, ref int dsCount)
        {
            if (dsCount > 1)
            {
                dsCount--;
                chart?.Update("removeDataset");
            }
        }

        private void AddData(Chart? chart, ref int daCount)
        {
            var limit = (chart?.ChartType ?? ChartType.Line) switch
            {
                ChartType.Line => 14,
                ChartType.Bar => 14,
                ChartType.Bubble => 14,
                _ => Colors.Count()
            };

            if (daCount < limit)
            {
                daCount++;
                chart?.Update("addData");
            }
        }

        private void RemoveData(Chart? chart, ref int daCount)
        {
            var limit = (chart?.ChartType ?? ChartType.Line) switch
            {
                ChartType.Line => 7,
                ChartType.Bar => 7,
                ChartType.Bubble => 4,
                _ => 2
            };
            if (daCount > limit)
            {
                daCount--;
                chart?.Update("removeData");
            }
        }

        private void ToggleCircle()
        {
            IsCricle = !IsCricle;
            DoughnutChart?.SetAngle(IsCricle ? 360 : 0);
            DoughnutChart?.Update("setAngle");
        }

        /// <summary>
        /// 
        /// </summary>
        [JSInvokable]
        public void ShowToast()
        {
            ToastService?.Show(new ToastOption() { Title = "友情提示", Content = "屏幕宽度过小，如果是手机请横屏观看" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (disposing) Interope?.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnInit",
                Description="组件数据初始化委托方法",
                Type ="Func<Task<ChartDataSource>>"
            },
            new EventItem()
            {
                Name = "OnAfterInit",
                Description="客户端绘制图表完毕后回调此委托方法",
                Type ="Action"
            },
        };

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Angle",
                Description = "Bubble 模式下显示角度 180 为 半圆 360 为正圆",
                Type = "int",
                ValueList = " — ",
                DefaultValue = " — ",
            },
            new AttributeItem() {
                Name = "Width",
                Description = "组件宽度支持单位 如: 100px 75%",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ChartType",
                Description = "设置图表类型",
                Type = "ChartType",
                ValueList = "Line|Bar|Pie|Doughnut|Bubble",
                DefaultValue = "Line"
            },
        };
    }
}
