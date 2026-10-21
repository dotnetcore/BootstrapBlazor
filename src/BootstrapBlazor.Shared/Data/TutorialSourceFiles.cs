// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Data;

/// <summary>
/// Source files included in the tutorial download bundles.
/// </summary>
public static class TutorialSourceFiles
{
    private static IReadOnlyList<string> LayoutFiles { get; } =
    [
        "../Layout/TutorialsLayout.razor",
        "../Layout/TutorialsLayout.razor.css",
        "../Layout/TutorialsLoginLayout.razor",
        "../Layout/TutorialsLoginLayout.razor.css"
    ];

    internal static IReadOnlyList<string> Dashboard { get; } =
    [
        "Tutorials/Dashboard.razor",
        "Tutorials/Dashboard.razor.cs",
        "Tutorials/Dashboard.razor.css",
        "Tutorials/DashboardData.cs",
        "../../Services/DashboardService.cs"
    ];

    internal static IReadOnlyList<string> Template1 { get; } =
    [
        "Tutorials/LoginAndRegister/Template1.razor",
        "Tutorials/LoginAndRegister/Template1.razor.css",
        .. LayoutFiles
    ];

    internal static IReadOnlyList<string> Template2 { get; } =
    [
        "Tutorials/LoginAndRegister/Template2.razor",
        .. LayoutFiles
    ];

    internal static IReadOnlyList<string> Template3 { get; } =
    [
        "Tutorials/LoginAndRegister/Template3.razor",
        "Tutorials/LoginAndRegister/Template3.razor.css",
        .. LayoutFiles
    ];

    internal static IReadOnlyList<string> Template4 { get; } =
    [
        "Tutorials/LoginAndRegister/Template4.razor",
        "Tutorials/LoginAndRegister/Template4.razor.css",
        .. LayoutFiles
    ];

    internal static IReadOnlyList<string> Template5 { get; } =
    [
        "Tutorials/LoginAndRegister/Template5.razor",
        "Tutorials/LoginAndRegister/Template5.razor.cs",
        "Tutorials/LoginAndRegister/Template5.razor.js",
        "Tutorials/LoginAndRegister/Template5.razor.css",
        "../Layout/TutorialsLayout.razor",
        "../Layout/TutorialsLayout.razor.css"
    ];

    internal static IReadOnlyList<string> Waterfall { get; } =
    [
        "Tutorials/Waterfall.razor",
        "Tutorials/Waterfall.razor.cs",
        "Tutorials/Waterfall.razor.css"
    ];

    internal static IReadOnlyList<string> Translation { get; } =
    [
        "Tutorials/Translation/Translator.razor",
        "Tutorials/Translation/Translator.razor.cs",
        "Tutorials/Translation/Translator.razor.css",
        "Tutorials/Translation/LanguageWriter.cs",
        "Tutorials/Translation/LanguageDataTable.cs",
        "Tutorials/Translation/AzureTranslatorServiceExtensions.cs"
    ];

    internal static IReadOnlyList<string> Drawing { get; } =
    [
        "Tutorials/DrawingApp.razor",
        "Tutorials/DrawingApp.razor.cs",
        "Tutorials/DrawingApp.razor.js",
        "Tutorials/DrawingApp.razor.css",
    ];

    private static readonly HashSet<string> AllowedFiles = new([
        .. Dashboard, .. Template1, .. Template2, .. Template3, .. Template4,
        .. Template5, .. Waterfall, .. Translation, .. Drawing
    ], StringComparer.Ordinal);

    /// <summary>
    /// Determines whether a source file belongs to a tutorial download bundle.
    /// </summary>
    public static bool Contains(string fileName) => AllowedFiles.Contains(fileName);
}
