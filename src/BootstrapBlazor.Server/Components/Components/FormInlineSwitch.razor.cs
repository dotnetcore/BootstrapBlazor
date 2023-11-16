// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Components;

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
    private IStringLocalizer<Rows>? LocalizerRows { get; set; }

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
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        Items = Enum.GetNames<RowType>().Select(i => new SelectedItem(i, LocalizerRows[i]));
    }
}
