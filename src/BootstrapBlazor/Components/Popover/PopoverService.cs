// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Popover 服务类
/// </summary>
public class PopoverService : BootstrapServiceBase<PopoverConfirmOption>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    /// <param name="popover">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    public Task Show(PopoverConfirmOption option, PopoverConfirm? popover = null) => Invoke(option, popover);
}
