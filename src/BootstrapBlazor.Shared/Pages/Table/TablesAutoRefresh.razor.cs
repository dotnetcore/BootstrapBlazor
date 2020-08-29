using BootstrapBlazor.Components;
using System;
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

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <returns></returns>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender) RunAutoRefreshTask();
        }

        private void RunAutoRefreshTask()
        {
            Task.Run(async () =>
            {
                var i = Items.Count;
                while (!CancelTokenSource.IsCancellationRequested)
                {
                    if (AutoRefreshTable.IsRendered)
                    {
                        Items.Insert(0, new BindItem()
                        {
                            Id = i++,
                            Name = $"张三 {i:d4}",
                            DateTime = DateTime.Now.AddDays(i - 1),
                            Address = $"上海市普陀区金沙江路 {random.Next(1000, 2000)} 弄",
                            Count = random.Next(1, 100),
                            Complete = random.Next(1, 100) > 50,
                            Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel
                        });

                        if (Items.Count > 90) Items.RemoveRange(90, 1);

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
