using BootstrapBlazor.Components;
using BootstrapBlazor.WebConsole.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.WebConsole.Pages
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

        private Task<ChartDataSource> OnInit() => OnInit(7);

        private JSInterop<Charts>? Interope { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender && JSRuntime != null)
            {
                if (Interope == null) Interope = new JSInterop<Charts>(JSRuntime);
                Interope.Invoke(this, "", "_initChart", nameof(ShowToast));
            }
        }

        private Task<ChartDataSource> OnInit(int length)
        {
            var ds = new ChartDataSource();
            ds.Options.Title.Text = "图表标题";
            ds.Options.XAxes.Add(new ChartAxes() { LabelString = "天数" });
            ds.Options.YAxes.Add(new ChartAxes() { LabelString = "数值" });

            ds.Labels = Enumerable.Range(1, length).Select(i => i.ToString());

            ds.Data.Add(new ChartDataset()
            {
                Label = "第一组数据",
                Data = Enumerable.Range(1, length).Select(i => Randomer.Next(20, 37)).Cast<object>()
            });
            ds.Data.Add(new ChartDataset()
            {
                Label = "第二组数据",
                Data = Enumerable.Range(1, length).Select(i => Randomer.Next(20, 37)).Cast<object>()
            });
            return Task.FromResult(ds);
        }

        private Task<ChartDataSource> OnPieInit()
        {
            var ds = new ChartDataSource();
            ds.Options.Title.Text = "图表标题";
            ds.Options.XAxes.Add(new ChartAxes() { LabelString = "天数" });
            ds.Options.YAxes.Add(new ChartAxes() { LabelString = "数值" });

            ds.Labels = Enumerable.Range(1, 5).Select(i => i.ToString());

            ds.Data.Add(new ChartDataset()
            {
                Label = "第一组数据",
                Data = Enumerable.Range(1, 5).Select(i => Randomer.Next(20, 37)).Cast<object>()
            });
            return Task.FromResult(ds);
        }

        private Task<ChartDataSource> OnBubbleInit()
        {
            var ds = new ChartDataSource();
            ds.Options.Title.Text = "图表标题";
            ds.Options.XAxes.Add(new ChartAxes() { LabelString = "天数" });
            ds.Options.YAxes.Add(new ChartAxes() { LabelString = "数值" });

            ds.Labels = Enumerable.Range(1, 5).Select(i => i.ToString());

            ds.Data.Add(new ChartDataset()
            {
                Label = "第一组数据",
                Data = Enumerable.Range(1, 5).Select(i => new
                {
                    x = Randomer.Next(10, 40),
                    y = Randomer.Next(10, 40),
                    r = Randomer.Next(1, 20)
                })
            });
            ds.Data.Add(new ChartDataset()
            {
                Label = "第二组数据",
                Data = Enumerable.Range(1, 5).Select(i => new
                {
                    x = Randomer.Next(10, 40),
                    y = Randomer.Next(10, 40),
                    r = Randomer.Next(1, 20)
                })
            });
            return Task.FromResult(ds);
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
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Width",
                Description = "组件宽度支持单位 如: 100px 75%",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnInit",
                Description = "组件数据初始化委托方法",
                Type = "Func<Task<ChartDataSource>>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ChartType",
                Description = "设置图表类型",
                Type = "ChartType",
                ValueList = "Line|Bar",
                DefaultValue = "Line"
            }
        };
    }
}
