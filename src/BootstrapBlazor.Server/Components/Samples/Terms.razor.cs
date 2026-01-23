using System.Text;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Term 示例
/// </summary>
public partial class Terms : IDisposable
{
    private Term _term = default!;
    private MemoryStream _ms = new MemoryStream();
    private CancellationTokenSource? _cancellationTokenSource;

    private TermOptions Options { get; set; } = new()
    {
        FontSize = 14,
        CursorBlink = true
    };

    private string? InputString { get; set; }

    private async Task OnReceivedAsync(byte[] data)
    {
        var str = System.Text.Encoding.UTF8.GetString(data);
        str = str.Replace("\r", "\r\n");
        await _term.Write(str);
    }

    private async Task OnSend()
    {
        if (!string.IsNullOrEmpty(InputString))
        {
            await _term.WriteLine(InputString);
            InputString = "";
        }
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
            await _term.Open(_ms);

            _ = Task.Run(async () =>
            {
                _cancellationTokenSource ??= new();
                while (_cancellationTokenSource is { IsCancellationRequested: false })
                {
                    try
                    {
                        await _ms.WriteAsync(Encoding.UTF8.GetBytes(DateTime.Now.ToString()), _cancellationTokenSource.Token);
                        _ms.WriteByte(0x0d);
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
