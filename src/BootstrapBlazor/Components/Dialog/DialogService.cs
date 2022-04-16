// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
