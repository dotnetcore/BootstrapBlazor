// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// InputGroup 组件示例
/// </summary>
public partial class InputGroups
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    [NotNull]
    private Foo? Model { get; set; }

    private string BindValue { get; set; } = string.Empty;

    private static List<string> StaticItems => ["1", "12", "123", "1234", "12345", "123456", "abc", "abcdef", "ABC", "aBcDeFg", "ABCDEFG"];

    [NotNull]
    private IEnumerable<Foo>? AufoFillItems { get; set; }

    [NotNull]
    private static List<SelectedItem>? Items2 =>
    [
        new ("Beijing", "北京"),
        new ("Shanghai", "上海"),
        new ("Guangzhou", "广州"),
        new ("Shenzhen", "深圳"),
        new ("Chengdu", "成都"),
        new ("Wuhan", "武汉"),
        new ("Dalian", "大连"),
        new ("Hangzhou", "杭州"),
        new ("Lianyungang", "连云港")
    ];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        AufoFillItems = Foo.GenerateFoo(LocalizerFoo);
        Model = Foo.Generate(LocalizerFoo);
    }

    private string StringAt { get; set; } = "@";

    private string StringMailServer { get; set; } = "163.com";

    private readonly IEnumerable<SelectedItem> Items = new SelectedItem[]
    {
        new("", "请选择 ..."),
        new("Beijing", "北京"),
        new("Shanghai", "上海")
    };
    private string? GroupFormClassString => CssBuilder.Default("row g-3").AddClass("form-inline", FormRowType == RowType.Inline).Build();

    private RowType FormRowType { get; set; }

    private bool SwitchValue { get; set; }
}
