namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Term 示例
/// </summary>
public partial class Terms
{
    private Term? Term { get; set; }

    private TermOptions Options { get; set; } = new()
    {
        FontSize = 14,
        CursorBlink = true
    };

    private string? InputString { get; set; }

    private async Task OnData(byte[] data)
    {
        if (Term != null)
        {
            var str = System.Text.Encoding.UTF8.GetString(data);
            str = str.Replace("\r", "\r\n");
            await Term.Write(str);
        }
    }

    private async Task OnSend()
    {
        if (Term != null && !string.IsNullOrEmpty(InputString))
        {
            await Term.WriteLine(InputString);
            InputString = "";
        }
    }

    private async Task OnClean()
    {
        if (Term != null)
        {
            await Term.Clear();
        }
    }

    private Term? TermStream { get; set; }

    /// <summary>
    /// OnAfterRenderAsync
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (TermStream != null)
            {
                await TermStream.Open(new MockShellStream());
            }
        }
    }

    private Task OnDataAsync(byte[] data)
    {
        return Task.CompletedTask;
    }

    class MockShellStream : Stream
    {
        private readonly List<byte> _buffer = [];

        private readonly AutoResetEvent _autoResetEvent = new(false);

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => throw new NotSupportedException();

        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        public override void Flush()
        {

        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            var data = new byte[count];
            Array.Copy(buffer, offset, data, 0, count);
            var str = System.Text.Encoding.UTF8.GetString(data);
            str = str.Replace("\r", "\r\n");
            _buffer.AddRange(System.Text.Encoding.UTF8.GetBytes(str));

            var command = System.Text.Encoding.UTF8.GetString(data);
            if (command == "\r")
            {
                _buffer.AddRange(System.Text.Encoding.UTF8.GetBytes("Using Stream implementing a mock shell demo\r\n"));
                _buffer.AddRange(System.Text.Encoding.UTF8.GetBytes("> "));
            }
            _autoResetEvent.Set();
            await Task.CompletedTask;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (_buffer.Count == 0)
                {
                    _autoResetEvent.WaitOne();
                }
            }, cancellationToken);

            var readCount = Math.Min(count, _buffer.Count);
            if (readCount > 0)
            {
                var data = _buffer.Take(readCount).ToArray();
                Array.Copy(data, 0, buffer, offset, readCount);
                _buffer.RemoveRange(0, readCount);
            }
            return readCount;
        }
    }
}
