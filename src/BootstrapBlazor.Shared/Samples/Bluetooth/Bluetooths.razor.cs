// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class Bluetooths
{
    Printer printer { get; set; } = new Printer();

    /// <summary>
    /// 显示内置界面
    /// </summary>
    bool ShowUI { get; set; } = false;

    private string? message;
    private string? statusmessage;
    private string? errmessage;

    private Task OnResult(string message)
    {
        this.message = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUpdateStatus(string message)
    {
        this.statusmessage = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnError(string message)
    {
        this.errmessage = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnGetDevices(List<string>? devices)
    {
        this.message = "";
        if (devices == null || devices!.Count == 0) return Task.CompletedTask;
        this.message += $"已配对设备{devices.Count}:{Environment.NewLine}";
        devices.ForEach(a => this.message += $"   {a}{Environment.NewLine}");
        //this.message = this.message.Replace(Environment.NewLine, "<br/>");
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SwitchUI()
    {
        ShowUI = !ShowUI;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Placement",
            Description = "位置",
            Type = "Placement",
            ValueList = "Auto / Top / Left / Bottom / Right",
            DefaultValue = "Auto"
        }
    };
}
