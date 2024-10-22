// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Dialog 组件服务
/// </summary>
public class DialogService : BootstrapServiceBase<DialogOption>
{
    /// <summary>
    /// 显示 Dialog 方法
    /// </summary>
    /// <param name="option">弹窗配置信息实体类</param>
    /// <param name="dialog">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    /// <returns></returns>
    public Task Show(DialogOption option, Dialog? dialog = null) => Invoke(option, dialog);
}
