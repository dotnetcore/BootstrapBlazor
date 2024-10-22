// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
            new SelectedItem("And",Localizer["And"].Value),
            new SelectedItem("Or",Localizer["Or"].Value)
        };
    }
}
