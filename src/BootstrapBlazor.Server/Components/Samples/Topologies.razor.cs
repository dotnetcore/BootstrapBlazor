// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.JSInterop;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 人机交互界面
/// </summary>
public partial class Topologies
{
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
            Content = Content.Replace("{AssetPath}", WebsiteOption.Value.AssetRootPath);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Interop, nameof(ToggleFan));

    private Task OnReset()
    {
        TopologyElement.Reset();
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
        DataService.OnDataChange = data => TopologyElement.PushData(data);
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
}
