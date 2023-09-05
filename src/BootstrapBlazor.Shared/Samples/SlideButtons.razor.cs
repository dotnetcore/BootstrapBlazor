// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// FloatButton 组件示例
/// </summary>
public partial class SlideButtons
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    private Placement Placement { get; set; }

    private IEnumerable<SelectedItem> Items => Foo.GenerateFoo(LocalizerFoo).Select(i => new SelectedItem(i.Id.ToString(), i.Name!));

    private Task OnClickPlacement(Placement placement)
    {
        Placement = placement;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private CheckboxState CheckState(string state) => Placement.ToDescriptionString() == state ? CheckboxState.Checked : CheckboxState.UnChecked;

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = nameof(FileViewer.Filename),
            Description = "Excel/Word 文件路径或者URL",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
