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
    private IWebHostEnvironment? WebHost { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<UniverSheets>? Localizer { get; set; }

    private readonly Dictionary<string, string> _plugins = new()
    {
        { "ReportPlugin", "univer-sheet/plugin.js" }
    };

    [NotNull]
    private UniverSheet? _sheetExcel = null;

    [NotNull]
    private UniverSheet? _sheetPlugin = null;

    private static string? _reportData = null;

    private string? _jsonData = null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        var reportFile = Path.Combine(WebHost.WebRootPath, "univer-sheet", "report.json");
        if (File.Exists(reportFile))
        {
            _reportData = await File.ReadAllTextAsync(reportFile);
        }
    }

    private async Task OnReadyAsync() => await ToastService.Information(Localizer["ToastOnReadyTitle"], Localizer["ToastOnReadyContent"]);

    private static Task<UniverSheetData> OnPostDataAsync(UniverSheetData data)
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
        return Task.FromResult(result);
    }

    private async Task OnPushExcelData()
    {
        await _sheetExcel.PushDataAsync(new UniverSheetData()
        {
            CommandName = "SetWorkbook",
            Data = _reportData
        });
    }

    private async Task OnSaveExcelData()
    {
        var result = await _sheetExcel.PushDataAsync(new UniverSheetData()
        {
            CommandName = "GetWorkbook"
        });
        _jsonData = result?.Data?.ToString();
        StateHasChanged();
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
