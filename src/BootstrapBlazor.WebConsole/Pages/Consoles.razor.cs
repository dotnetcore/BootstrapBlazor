using BootstrapBlazor.WebConsole.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class Consoles : IDisposable
    {
        private readonly BlockingCollection<string> _messages = new BlockingCollection<string>(new ConcurrentQueue<string>());

        private CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();

        private IEnumerable<string> Messages => _messages;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var _ = Task.Run(async () =>
            {
                do
                {
                    if (!_messages.IsAddingCompleted)
                    {
                        _messages.Add($"{DateTimeOffset.Now}: Dispatch Message");

                        if (_messages.Count > 8)
                        {
                            _messages.TryTake(out var _);
                        }
                        await InvokeAsync(StateHasChanged);
                    }
                    await Task.Delay(2000, _cancelTokenSource.Token);
                }
                while (!_cancelTokenSource.IsCancellationRequested);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem(){
                    Name = "Items",
                    Description = "组件数据源",
                    Type = "IEnumerable<string>",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem(){
                    Name = "Height",
                    Description = "组件高度",
                    Type = "int",
                    ValueList = " — ",
                    DefaultValue = "126"
                }
            };
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _messages.CompleteAdding();
                _cancelTokenSource.Cancel();
                _cancelTokenSource.Dispose();
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
