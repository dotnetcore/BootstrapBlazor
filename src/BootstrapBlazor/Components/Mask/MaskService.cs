// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">MaskService 遮罩服务</para>
///  <para lang="en">MaskService</para>
/// </summary>
public class MaskService : BootstrapServiceBase<MaskOption?>
{
    /// <summary>
    ///  <para lang="zh">显示 Mask 方法</para>
    ///  <para lang="en">Show Mask Method</para>
    /// </summary>
    /// <param name="option"><para lang="zh">遮罩配置信息实体类</para><para lang="en">Mask Configuration Information Entity Class</para></param>
    /// <param name="mask"><para lang="zh"><see cref="Mask"/> 组件实例</para><para lang="en"><see cref="Mask"/> Component Instance</para></param>
    /// <returns></returns>
    public Task Show(MaskOption option, Mask? mask = null) => Invoke(option, mask);

    /// <summary>
    ///  <para lang="zh">关闭 Mask 方法</para>
    ///  <para lang="en">Close Mask Method</para>
    /// </summary>
    /// <param name="mask"><para lang="zh"><see cref="Mask"/> 组件实例</para><para lang="en"><see cref="Mask"/> Component Instance</para></param>
    /// <param name="all"><para lang="zh">是否关闭所有遮罩 默认 false 仅关闭当前或者指定遮罩</para><para lang="en">Whether to close all masks. Default false. Only close current or specified mask</para></param>
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
