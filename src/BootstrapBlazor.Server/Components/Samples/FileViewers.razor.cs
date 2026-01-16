// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// FileViewers
/// </summary>
public partial class FileViewers
{
    [NotNull]
    private string? WordSampleFile { get; set; }

    [NotNull]
    private string? ExcelSampleFile { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        WordSampleFile = CombineFilename("sample.docx");
        ExcelSampleFile = CombineFilename("sample.xlsx");

        FileList.Add("sample.xlsx");
        FileList.Add("sample.docx");
        Url = FileList[0];

        Items = FileList.Select(i => new SelectedItem(i, i)).ToList();
    }

    /// <summary>
    /// CombineFilename
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    private string CombineFilename(string filename)
    {
#if DEBUG
        filename = Path.Combine(WebsiteOption.Value.WebRootPath, "samples", filename);
#else
        filename = Path.Combine(WebsiteOption.Value.ContentRootPath, "wwwroot", "samples", filename);
#endif
        return filename;
    }

    [NotNull]
    private string? Url { get; set; }

    [NotNull]
    private FileViewer? fileViewer { get; set; }

    [NotNull]
    private List<SelectedItem>? Items { get; set; }

    private List<string> FileList { get; } = [];

    private async Task ChangeURL(SelectedItem e)
    {
        Url = e.Value;
        StateHasChanged();
        await fileViewer.Reload(CombineFilename(e.Value));
    }

    private async Task Apply()
    {
        await fileViewer.Reload(CombineFilename(Url));
    }
}
