// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 内部组件
/// </summary>
public partial class FormInlineSwitch
{
    /// <summary>
    /// 获得/设置 用户自定义属性
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? AdditionalAttributes { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<FormInlineSwitch>? Localizer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public RowType Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<RowType> ValueChanged { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Items { get; set; }

    private RowType FormRowType
    {
        get => Value; set
        {
            if (Value != value)
            {
                Value = value;
                if (ValueChanged.HasDelegate)
                {
                    ValueChanged.InvokeAsync(Value);
                }
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Enum.GetNames<RowType>().Select(i => new SelectedItem(i, Localizer[i]));
    }
}
