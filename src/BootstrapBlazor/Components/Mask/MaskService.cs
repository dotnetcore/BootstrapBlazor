// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
    /// <param name="all">是否关闭所有遮罩 默认 false 仅关闭当前或者指定遮罩</param>
    /// <returns></returns>
    public async Task Close(Mask? mask = null, bool all = false)
    {
        if (all)
        {
            foreach (var (key, callback) in Cache)
            {
                await callback(null);
            }
        }
        else
        {
            await Invoke(null, mask);
        }
    }
}
