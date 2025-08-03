// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Toolbar 组件示例代码
/// </summary>
public partial class Toolbars
{
    private readonly List<SelectedItem> _items1 = [];
    private string _item1 = "1,2";

    private readonly List<SelectedItem> _items2 = [];
    private string _item2 = "1";

    private readonly List<SelectedItem> _items3 = [];
    private string _item3 = "1";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _items1.Add(new SelectedItem("1", "Text1"));
        _items1.Add(new SelectedItem("2", "Text2"));
        _items1.Add(new SelectedItem("3", "Text3"));

        _items2.Add(new SelectedItem("1", "Text1"));
        _items2.Add(new SelectedItem("2", "Text2"));
        _items2.Add(new SelectedItem("3", "Text3"));

        _items3.Add(new SelectedItem("1", "Text1"));
        _items3.Add(new SelectedItem("2", "Text2"));
        _items3.Add(new SelectedItem("3", "Text3"));
    }

    private static string GetIconByItem(string v) => v switch
    {
        "1" => "fa-solid fa-align-left",
        "2" => "fa-solid fa-align-center",
        _ => "fa-solid fa-align-right"
    };

    private static string GetRadioIconByItem(string v) => v switch
    {
        "1" => "fa-solid fa-bold",
        "2" => "fa-solid fa-italic",
        _ => "fa-solid fa-font"
    };
}
