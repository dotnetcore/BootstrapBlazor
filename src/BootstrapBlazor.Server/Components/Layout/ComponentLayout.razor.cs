// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Server.Components.Layout;

/// <summary>
/// 
/// </summary>
public partial class ComponentLayout : IAsyncDisposable
{
    [NotNull]
    private string? RazorFileName { get; set; }

    [NotNull]
    private string? CSharpFileName { get; set; }

    [NotNull]
    private string? VideoFileName { get; set; }

    [NotNull]
    private string? Title { get; set; }

    [NotNull]
    private string? Video { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<ComponentLayout>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? Navigator { get; set; }

    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    [NotNull]
    private Tab? Tab { get; set; }

    /// <summary>
    /// Instance of <see cref="JSModule"/>
    /// </summary>
    private JSModule? Module { get; set; }

    [Inject]
    [NotNull]
    private IOptions<IconThemeOptions>? IconThemeOptions { get; set; }

    private string GVPUrl => $"{WebsiteOption.CurrentValue.GiteeRepositoryUrl}/badge/star.svg?theme=gvp";

    private List<SelectedItem> IconThemes { get; } = [];

    [NotNull]
    private string? IconThemeKey { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        IconThemes.AddRange(new SelectedItem[]
        {
            new("fa", "Font Awesome"),
            new("mdi", "Material Design")
        });
        IconThemeKey = IconThemeOptions.Value.ThemeKey;

        Title ??= Localizer[nameof(Title)];
        Video ??= Localizer[nameof(Video)];
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var url = Navigator.ToBaseRelativePath(Navigator.Uri);
        var comNameWithHash = url.Split('#').First();
        var comName = comNameWithHash.Split('?').First();
        RazorFileName = $"{comName}.razor";
        CSharpFileName = $"{comName}.razor.cs";
        VideoFileName = comName;

        Tab?.ActiveTab(0);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Module = await JSRuntime.LoadModule("./Components/Layout/ComponentLayout.razor.js");
        }
        if (Module != null)
        {
            await Module.InvokeVoidAsync("init");
        }
    }

    private Task OnIconThemeChanged(string key)
    {
        IconThemeOptions.Value.ThemeKey = key;

        Navigator.NavigateTo(Navigator.Uri, true);
        return Task.CompletedTask;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            // 销毁 JSModule
            if (Module != null)
            {
                await Module.DisposeAsync();
                Module = null;
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
