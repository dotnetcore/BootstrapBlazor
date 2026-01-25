// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">FilterLogicItem 组件用于选择过滤条件的逻辑运算符</para>
/// <para lang="en">FilterLogicItem component used to select logical operator for filter conditions</para>
/// </summary>
public partial class FilterLogicItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 逻辑运算符</para>
    /// <para lang="en">Gets or sets Logical Operator</para>
    /// </summary>
    [Parameter]
    public FilterLogic Logic { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 逻辑运算符改变回调方法</para>
    /// <para lang="en">Gets or sets Logical Operator Change Callback Method</para>
    /// </summary>
    [Parameter]
    public EventCallback<FilterLogic> LogicChanged { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<FilterLogicItem>? Localizer { get; set; }

    private readonly List<SelectedItem> _items = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _items.AddRange(
        [
            new SelectedItem("And",Localizer["And"].Value),
            new SelectedItem("Or",Localizer["Or"].Value)
        ]);
    }

    private async Task OnValueChanged(FilterLogic val)
    {
        Logic = val;
        if (LogicChanged.HasDelegate)
        {
            await LogicChanged.InvokeAsync(Logic);
        }
    }
}
