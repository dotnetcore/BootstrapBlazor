// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class FilterLogicItem
{
    private FilterLogic _value;
    private FilterLogic Value
    {
        get
        {
            _value = Logic;
            return _value;
        }
        set
        {
            _value = value;
            if (LogicChanged.HasDelegate) LogicChanged.InvokeAsync(value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public FilterLogic Logic { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<FilterLogic> LogicChanged { get; set; }

    private IEnumerable<SelectedItem>? Items { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<FilterLogicItem>? Localizer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = new List<SelectedItem>()
            {
                new SelectedItem("And",Localizer["And"]?.Value ?? "And"),
                new SelectedItem("Or",Localizer["Or"]?.Value ?? "Or")
            };
    }
}
