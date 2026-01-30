// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// FloatButton 组件示例
/// </summary>
public partial class SlideButtons
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? FooLocalizer { get; set; }

    private Placement Placement { get; set; }

    private IEnumerable<SelectedItem> Items => Foo.GenerateFoo(FooLocalizer).Select(i => new SelectedItem(i.Id.ToString(), i.Name!));

    private Task OnClickPlacement(Placement placement)
    {
        Placement = placement;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private CheckboxState CheckState(string state) => Placement.ToDescriptionString() == state ? CheckboxState.Checked : CheckboxState.UnChecked;
}
