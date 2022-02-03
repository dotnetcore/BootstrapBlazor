// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Toast 弹出窗参数配置类
/// </summary>
public class ToastOption : PopupOptionBase
{
    /// <summary>
    /// 获得/设置 弹窗载体
    /// </summary>
    internal ToastBox? ToastBox { get; set; }

    /// <summary>
    /// 获得/设置 弹出框类型
    /// </summary>
    public ToastCategory Category { get; set; }

    /// <summary>
    /// 获得/设置 显示标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 关闭当前弹窗方法
    /// </summary>
    public async ValueTask Close()
    {
        if (ToastBox != null)
        {
            await ToastBox.Close();
        }
    }
}
