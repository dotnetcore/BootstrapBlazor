// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Components;

public partial class VideoDemo
{
    /// <summary>
    /// 开始播放方法
    /// </summary>
    /// <returns></returns>
    public Task Play() => InvokeVoidAsync("play", Id);

    /// <summary>
    /// 暂停方法
    /// </summary>
    /// <returns></returns>
    public Task Pause() => InvokeVoidAsync("Pause", Id);
}
