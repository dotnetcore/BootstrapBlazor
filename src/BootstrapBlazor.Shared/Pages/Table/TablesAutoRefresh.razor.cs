using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Table
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TablesAutoRefresh : IDisposable
    {
        private CancellationTokenSource CancelTokenSource { get; set; } = new CancellationTokenSource();

#nullable disable
        private Table<BindItem> AutoRefreshTable { get; set; }
#nullable restore

        private List<BindItem> AutoItems { get; set; } = new List<BindItem>();

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            AutoItems = Items.Take(2).ToList();
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <returns></returns>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender) RunAutoRefreshTask();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private Task<QueryData<BindItem>> OnRefreshQueryAsync(QueryPageOptions options)
        {
            // 设置记录总数
            var total = AutoItems.Count();

            // 内存分页
            var items = AutoItems.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

            return Task.FromResult(new QueryData<BindItem>()
            {
                Items = items,
                TotalCount = total
            });
        }

        private void RunAutoRefreshTask()
        {
            Task.Run(async () =>
            {
                var i = AutoItems.Count;
                while (!CancelTokenSource.IsCancellationRequested)
                {
                    if (AutoRefreshTable.IsRendered)
                    {
                        AutoItems.Insert(0, new BindItem()
                        {
                            Id = i++,
                            Name = $"张三 {i:d4}",
                            DateTime = DateTime.Now.AddDays(i - 1),
                            Address = $"上海市普陀区金沙江路 {random.Next(1000, 2000)} 弄",
                            Count = random.Next(1, 100),
                            Complete = random.Next(1, 100) > 50,
                            Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel
                        });

                        if (AutoItems.Count > 10) AutoItems.RemoveRange(10, 1);

                        await InvokeAsync(AutoRefreshTable.QueryAsync);
                    }
                    await Task.Delay(2000, CancelTokenSource.Token);
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                CancelTokenSource.Cancel();
                CancelTokenSource.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
