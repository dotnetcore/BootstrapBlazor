// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// NotSupportFilter component
/// </summary>
public partial class NotSupportFilter
{
    /// <summary>
    /// 获得/设置 不支持过滤类型提示信息 默认 null 读取资源文件内容
    /// </summary>
    [Parameter]
    public string? NotSupportedMessage { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NotSupportedMessage ??= Localizer[nameof(NotSupportedMessage)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        return new();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void Reset()
    {

    }
}
