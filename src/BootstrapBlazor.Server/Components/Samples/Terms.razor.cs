using System.Text;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Term 示例
/// </summary>
public partial class Terms : IDisposable
{
    private Term _term = default!;
    private Term _termStream = default!;
    private MemoryStream _ms = new MemoryStream();
    private CancellationTokenSource? _cancellationTokenSource;

    private async Task OnReceivedAsync(byte[] data)
    {
        var str = System.Text.Encoding.UTF8.GetString(data);
        await _term.Write(str.Replace("\r", "\r\n"));
    }

    private async Task OnCheck()
    {
        await _term.WriteLine($"Check\r\n{DateTime.Now}");
    }

    private async Task OnTest()
    {
        await _term.WriteLine($"Test\r\n{DateTime.Now}");
    }

    private async Task OnClean()
    {
        await _term.Clear();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await _termStream.Open(_ms);

            _ = Task.Run(async () =>
            {
                _cancellationTokenSource ??= new();
                while (_cancellationTokenSource is { IsCancellationRequested: false })
                {
                    try
                    {
                        // 模拟流内数据变化
                        _ms.SetLength(0);
                        await _ms.WriteAsync(Encoding.UTF8.GetBytes($"{DateTime.Now}\r\n"), _cancellationTokenSource.Token);
                        _ms.Seek(0, SeekOrigin.Begin);
                        await Task.Delay(2000);
                    }
                    catch { }
                }
            });
        }
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_cancellationTokenSource is { IsCancellationRequested: false })
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }

            _ms.Close();
            _ms.Dispose();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
