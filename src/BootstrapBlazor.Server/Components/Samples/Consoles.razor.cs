// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Consoles
/// </summary>
public sealed partial class Consoles
{
    private ConsoleMessageCollection Messages { get; set; } = new(8);

    private ConsoleMessageCollection ColorMessages { get; set; } = new(12);

    private CancellationTokenSource? CancelTokenSource { get; set; }

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
    /// OnAfterRender
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            Task.Run(async () =>
            {
                CancelTokenSource ??= new();
                while (CancelTokenSource is { IsCancellationRequested: false })
                {
                    Messages.Add(new ConsoleMessageItem { Message = $"{DateTimeOffset.Now}: Dispatch Message" });
                    ColorMessages.Add(new ConsoleMessageItem { Message = $"{DateTimeOffset.Now}: Dispatch Message", Color = GetColor() });
                    await InvokeAsync(StateHasChanged);

                    try
                    {
                        if (CancelTokenSource != null)
                        {
                            await Task.Delay(2000, CancelTokenSource.Token);
                        }
                    }
                    catch { }
                }
            });
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
            Messages.Dispose();
            ColorMessages.Dispose();

            if (CancelTokenSource != null)
            {
                CancelTokenSource.Cancel();
                CancelTokenSource.Dispose();
                CancelTokenSource = null;
            }
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

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(BootstrapBlazor.Components.Console.Items),
            Description = "组件数据源",
            Type = "IEnumerable<ConsoleMessageItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Height",
            Description = "组件高度",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = nameof(BootstrapBlazor.Components.Console.IsAutoScroll),
            Description = "是否自动滚屏",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new(){
            Name = "ShowAutoScroll",
            Description = "是否显示自动滚屏选项",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "OnClear",
            Description = "组件清屏回调方法",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = "HeaderText",
            Description = "Header 显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "系统监控"
        },
        new()
        {
            Name = "HeaderTemplate",
            Description = "Header 模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ItemTemplate",
            Description = "Item 模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new(){
            Name = "LightTitle",
            Description = "指示灯 Title",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "通讯指示灯"
        },
        new()
        {
            Name = "IsFlashLight",
            Description = "指示灯是否闪烁",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "LightColor",
            Description = "指示灯颜色",
            Type = "Color",
            ValueList = " — ",
            DefaultValue = "Color.Success"
        },
        new()
        {
            Name = "ShowLight",
            Description = "是否显示指示灯",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ClearButtonText",
            Description = "按钮显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "清屏"
        },
        new()
        {
            Name = "ClearButtonIcon",
            Description = "按钮显示图标",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-fw fa-solid fa-xmark"
        },
        new()
        {
            Name = "ClearButtonColor",
            Description = "按钮颜色",
            Type = "Color",
            ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            DefaultValue = "Secondary"
        },
        new()
        {
            Name = "FooterTemplate",
            Description = "Footer 模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
