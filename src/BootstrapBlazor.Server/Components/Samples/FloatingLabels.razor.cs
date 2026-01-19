// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// FloatingLabel 组件示例代码
/// </summary>
public partial class FloatingLabels
{
    [NotNull]
    private Foo? Model { get; set; }

    [NotNull]
    private Foo? BindValueModel { get; set; }

    [NotNull]
    private Foo? FormatStringModel { get; set; }

    private byte[] ByteArray { get; set; } = [0x01, 0x12, 0x34, 0x56];

    private static string ByteArrayFormatter(byte[] source) => Convert.ToBase64String(source);

    private static string DateTimeFormatter(DateTime source) => source.ToString("yyyy-MM-dd");

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Model = new Foo() { Name = Localizer["FloatingLabelsTestName"] };
        BindValueModel = new Foo() { Name = Localizer["FloatingLabelsTestName"] };
        FormatStringModel = new Foo() { Name = Localizer["FloatingLabelsTestName"] };
    }
}
