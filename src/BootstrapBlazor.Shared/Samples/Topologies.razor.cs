// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 图标库
/// </summary>
public partial class Topologies : IDisposable
{
    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    [Inject]
    [NotNull]
    private FanControllerDataService? DataService { get; set; }

    [Inject]
    [NotNull]
    private SwalService? SwalService { get; set; }

    private string? Content { get; set; }

    [NotNull]
    private Topology? TopologyElement { get; set; }

    private JSInterop<Topologies>? Interop { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        var assembly = typeof(Topologies).Assembly;
        string strName = assembly.GetName().Name + ".topology.json";
        var stream = assembly.GetManifestResourceStream(strName);
        if (stream != null)
        {
            using var reader = new StreamReader(stream);
            Content = reader.ReadToEnd();
        }
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Interop = new JSInterop<Topologies>(JSRuntime);
            await Interop.InvokeVoidAsync(this, null, "bb_topology_demo", nameof(ToggleFan));

            // 推送数据
            var data = DataService.GetDatas();
            await TopologyElement.PushData(data);

            // 数据订阅
            DataService.OnDataChange = async datas => await TopologyElement.PushData(datas);
        }
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
        await JSRuntime.InvokeVoidAsync("$.bb_topology_demo_setOptions");
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = nameof(Topology.Content),
            Description = "加载图形 Json 内容",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(Topology.Interval),
            Description = "轮询模式下轮询间隔",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "2000"
        },
        new AttributeItem() {
            Name = nameof(Topology.OnQueryAsync),
            Description = "获取推送数据回调委托方法",
            Type = "Func<CancellationToken, Task<IEnumerable<TopologyItem>>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(Topology.OnBeforePushData),
            Description = "开始推送数据前回调方法",
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Interop != null)
            {
                Interop.Dispose();
                Interop = null;
            }
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
