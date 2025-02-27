// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// UniverSheet 组件示例代码
/// </summary>
public partial class UniverSheets
{
    private readonly Dictionary<string, string> Plugins = new()
    {
        { "ReportPlugin", "report/plugin.js" }
    };

    private UniverSheet _sheet = default!;

    private async Task OnPush()
    {
        await _sheet.PushDataAsync(new UniverSheetData()
        {
            MessageName = "MessageName",
            CommandName = "CommandName",
            Data = new
            {
                Id = 01,
                Name = "Test-Name"
            }
        });
    }
}
