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
    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<UniverSheets>? Localizer { get; set; }

    private readonly Dictionary<string, string> Plugins = new()
    {
        { "ReportPlugin", "univer-sheet/plugin.js" }
    };

    private UniverSheet _sheetExcel = default!;

    private UniverSheet _sheetPlugin = default!;

    private async Task OnReadyAsync() => await ToastService.Information(Localizer["ToastOnReadyTitle"], Localizer["ToastOnReadyContent"]);

    private static Task<UniverSheetData?> OnPostDataAsync(UniverSheetData data)
    {
        // 这里可以根据 data 的内容进行处理然后返回处理后的数据
        // 本例返回与时间相关的数据
        var result = new UniverSheetData()
        {
            MessageName = data.MessageName,
            CommandName = data.CommandName,
            Data = new
            {
                key = "datetime",
                Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }
        };
        return Task.FromResult<UniverSheetData?>(result);
    }

    private async Task OnPushExcelData()
    {
        await _sheetExcel.PushDataAsync(new UniverSheetData()
        {
            MessageName = "MessageName",
            CommandName = "CommandName",
            Data = new object[]
            {
                new object[] { "1", "2", "3", "4", "5" },
                new object[] { "1", "2", "3", "4", "5" },
            }
        });
    }

    private async Task OnPushPluginData()
    {
        await _sheetPlugin.PushDataAsync(new UniverSheetData()
        {
            MessageName = "MessageName",
            CommandName = "CommandName",
            Data = new
            {
                Id = "1",
                Name = "Test",
                Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }
        });
    }
}
