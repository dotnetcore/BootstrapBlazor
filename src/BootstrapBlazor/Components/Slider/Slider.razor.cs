// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Range 组件
/// </summary>
public partial class Slider<TValue>
{
    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassString => CssBuilder.Default("form-range")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 最小值 默认为 null 未设置
    /// </summary>
    [Parameter]
    [NotNull]
    public TValue? Min { get; set; }

    /// <summary>
    /// 获得/设置 最大值 默认为 null 未设置
    /// </summary>
    [Parameter]
    [NotNull]
    public TValue? Max { get; set; }

    /// <summary>
    /// 获得/设置 步长 默认为 null 未设置
    /// </summary>
    [Parameter]
    [NotNull]
    public TValue? Step { get; set; }

    private string? MinString => Min.ToString() == "0" ? GetRangeMinString : Min.ToString();

    private string? GetRangeMinString => _range?.Minimum.ToString();

    private string? MaxString => Max.ToString() == "0" ? GetRangeMaxString : Max.ToString();

    private string? GetRangeMaxString => _range?.Maximum.ToString();

    private string? StepString => Step.ToString() == "0" ? null : Step.ToString();

    private RangeAttribute? _range = null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (FieldIdentifier.HasValue)
        {
            _range = FieldIdentifier.Value.GetRange();
        }
    }
}
