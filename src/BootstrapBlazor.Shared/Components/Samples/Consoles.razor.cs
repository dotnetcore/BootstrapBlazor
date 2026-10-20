// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// Consoles
/// </summary>
public sealed partial class Consoles
{
    private ConsoleMessageCollection Messages { get; set; } = new(8);

    private ConsoleMessageCollection ColorMessages { get; set; } = new(12);

    private PeriodicTimer? _timer;

    private bool _disposed;

    /// <summary>
    /// OnClear
    /// </summary>
    private Task OnClear()
    {
        Messages.Clear();
        return Task.CompletedTask;
    }

    /// <summary>
    /// GetColor
    /// </summary>
    /// <returns></returns>
    private static Color GetColor()
    {
        var second = DateTime.Now.Second;
        return (second % 3) switch
        {
            1 => Color.Danger,
            2 => Color.Info,
            _ => Color.None
        };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(2));
            _timer = timer;
            do
            {
                await InvokeAsync(() =>
                {
                    if (!_disposed)
                    {
                        Messages.Add(new ConsoleMessageItem { Message = $"{DateTimeOffset.Now}: Dispatch Message" });
                        ColorMessages.Add(new ConsoleMessageItem { Message = $"{DateTimeOffset.Now}: Dispatch Message", Color = GetColor() });
                        StateHasChanged();
                    }
                });
            }
            while (await timer.WaitForNextTickAsync());
        }
    }

    /// <summary>
    /// Dispose
    /// </summary>
    /// <param name="disposing"></param>
    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _disposed = true;
            _timer?.Dispose();
            Messages.Dispose();
            ColorMessages.Dispose();
        }
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    [Inject]
    [NotNull]
    private IStringLocalizer<Consoles>? Localizer { get; set; }

    /// <summary>
    /// GetItemAttributes
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetItemAttributes() =>
    [
        new()
        {
            Name = "Message",
            Description = "控制台输出消息",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Color",
            Description = "消息颜色",
            Type = "Color",
            ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            DefaultValue = "None"
        }
    ];
}
