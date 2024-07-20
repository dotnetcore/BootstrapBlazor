// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// MaskService 遮罩服务
/// </summary>
public class MaskService : BootstrapServiceBase<MaskOption?>
{
    /// <summary>
    /// 显示 Mask 方法
    /// </summary>
    /// <param name="option">遮罩配置信息实体类</param>
    /// <param name="mask"><see cref="Mask"/> 组件实例</param>
    /// <returns></returns>
    public Task Show(MaskOption option, Mask? mask = null) => Invoke(option, mask);

    /// <summary>
    /// 关闭 Mask 方法
    /// </summary>
    /// <param name="mask"><see cref="Mask"/> 组件实例</param>
    /// <returns></returns>
    public Task Close(Mask? mask = null) => Invoke(null, mask);
}
