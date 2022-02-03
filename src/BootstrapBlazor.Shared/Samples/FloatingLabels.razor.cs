// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class FloatingLabels
{
    private byte[] ByteArray { get; set; } = new byte[] { 0x01, 0x12, 0x34, 0x56 };

    private static string ByteArrayFormatter(byte[] source) => Convert.ToBase64String(source);

    [NotNull]
    private Foo? Model { get; set; }

    private static string DateTimeFormatter(DateTime source) => source.ToString("yyyy-MM-dd");

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Model = new Foo() { Name = Localizer["TestName"] };
    }

    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
            new AttributeItem() {
                Name = "ChildContent",
                Description = Localizer["Att1"].Value,
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowLabel",
                Description = Localizer["Att2"].Value,
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "DisplayText",
                Description = Localizer["Att3"].Value,
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "FormatString",
                Description = Localizer["Att4"].Value,
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Formatter",
                Description = Localizer["Att5"].Value,
                Type = "RenderFragment<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name = "type",
                Description = Localizer["Att6"].Value,
                Type = "string",
                ValueList = "text / number / email / url / password",
                DefaultValue = "text"
            },
            new AttributeItem()
            {
                Name = "IsDisabled",
                Description = Localizer["Att7"].Value,
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            }
        };
}
