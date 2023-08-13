// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class DialButtons
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    private DialMode Mode { get; set; }

    //private IEnumerable<SelectedItem> Items => Foo.GenerateFoo(LocalizerFoo, _buttonCount).Select(i => new DialButtonItem(i.Id.ToString(), i.Name!));

    private int _buttonCount = 5;

    private Task OnClick(DialMode mode)
    {
        Mode = mode;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private CheckboxState CheckState(string state) => Mode.ToString() == state ? CheckboxState.Checked : CheckboxState.UnChecked;

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
