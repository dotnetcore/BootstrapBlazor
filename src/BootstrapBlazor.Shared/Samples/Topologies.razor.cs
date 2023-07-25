// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 人机交互界面
/// </summary>
public partial class Topologies
{
    /// <summary>
    /// 获得/设置 EChart DOM 元素实例
    /// </summary>
    private ElementReference Element { get; set; }

    private string? Content { get; set; }

    [NotNull]
    private Topology? TopologyElement { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var assembly = typeof(Topologies).Assembly;
        var strName = assembly.GetName().Name + ".topology.json";
        var stream = assembly.GetManifestResourceStream(strName);
        if (stream != null)
        {
            using var reader = new StreamReader(stream);
            Content = reader.ReadToEnd();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Interop, nameof(ToggleFan));

    private Task OnResize()
    {
        TopologyElement.Resize();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 切换风扇状态方法
    /// </summary>
    /// <param name="tagName"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task ToggleFan(string tagName)
    {
        var open = DataService.IsOpen;
        var op = new SwalOption()
        {
            Title = open ? "关闭风扇" : "打开风扇",
            Content = open ? "您确定要关闭风扇吗？" : "您确定要打开风扇吗？",
            Category = SwalCategory.Information
        };
        open = !open;
        var ret = await SwalService.ShowModal(op);
        if (ret)
        {
            await DataService.UpdateStatus(open);
        }
    }

    private async Task OnBeforePushData()
    {
        await InvokeVoidAsync("execute");

        // 推送数据
        var data = DataService.GetDatas();
        await TopologyElement.PushData(data);

        // 数据订阅
        DataService.OnDataChange = async datas => await TopologyElement.PushData(datas);
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = nameof(Topology.Content),
            Description = "Load Graphical Json Content",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(Topology.Interval),
            Description = "Polling interval in polling mode",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "2000"
        },
        new AttributeItem() {
            Name = nameof(Topology.OnQueryAsync),
            Description = "Get push data callback delegate method",
            Type = "Func<CancellationToken, Task<IEnumerable<TopologyItem>>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(Topology.OnBeforePushData),
            Description = "Callback method before starting to push data",
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
