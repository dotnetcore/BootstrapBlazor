// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using System.Collections.Concurrent;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Consoles : IDisposable
{
    private ConcurrentQueue<ConsoleMessageItem> Messages { get; set; } = new();
    private ConcurrentQueue<ConsoleMessageItem> ColorMessages { get; set; } = new();
    private readonly CancellationTokenSource _cancelTokenSource = new();

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
                _locker.WaitOne();
                Messages.Enqueue(new ConsoleMessageItem { Message = $"{DateTimeOffset.Now}: Dispatch Message" });

                ColorMessages.Enqueue(new ConsoleMessageItem { Message = $"{DateTimeOffset.Now}: Dispatch Message", Color = GetColor() });

                if (Messages.Count > 8)
                {
                    Messages.TryDequeue(out var _);
                }

                if (ColorMessages.Count > 12)
                {
                    ColorMessages.TryDequeue(out var _);
                }
                await InvokeAsync(StateHasChanged);
                _locker.Set();
                await Task.Delay(2000, _cancelTokenSource.Token);
            }
            while (!_cancelTokenSource.IsCancellationRequested);
        });
    }

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

    private readonly AutoResetEvent _locker = new(true);

    private void OnClear()
    {
        _locker.WaitOne();
        while (!Messages.IsEmpty)
        {
            Messages.TryDequeue(out var _);
        }
        _locker.Set();
    }

    private static IEnumerable<AttributeItem> GetItemAttributes()
    {
        return new AttributeItem[]
        {
                new AttributeItem(){
                    Name = "Message",
                    Description = "控制台输出消息",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem(){
                    Name = "Color",
                    Description = "消息颜色",
                    Type = "Color",
                    ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
                    DefaultValue = "None"
                }
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes()
    {
        return new AttributeItem[]
        {
                new AttributeItem(){
                    Name = "Items",
                    Description = "组件数据源",
                    Type = "IEnumerable<ConsoleMessageItem>",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem(){
                    Name = "Height",
                    Description = "组件高度",
                    Type = "int",
                    ValueList = " — ",
                    DefaultValue = "0"
                },
                new AttributeItem(){
                    Name = "ShowAutoScroll",
                    Description = "是否显示自动滚屏选项",
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem(){
                    Name = "OnClear",
                    Description = "组件清屏回调方法",
                    Type = "int",
                    ValueList = " — ",
                    DefaultValue = "0"
                },
                new AttributeItem(){
                    Name = "HeaderText",
                    Description = "Header 显示文字",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = "系统监控"
                },
                new AttributeItem(){
                    Name = "LightTitle",
                    Description = "指示灯 Title",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = "通讯指示灯"
                },
                new AttributeItem(){
                    Name = "ClearButtonText",
                    Description = "按钮显示文字",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = "清屏"
                },
                new AttributeItem(){
                    Name = "ClearButtonIcon",
                    Description = "按钮显示图标",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = "fa fa-fw fa-times"
                },
                new AttributeItem(){
                    Name = "ClearButtonColor",
                    Description = "按钮颜色",
                    Type = "Color",
                    ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
                    DefaultValue = "Secondary"
                }
        };
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
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
