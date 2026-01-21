// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">树状组件骨架屏组件</para>
/// <para lang="en">SkeletonTree Component</para>
/// </summary>
public partial class SkeletonTree
{
    private string? TreeClassString => CssBuilder.Default("skeleton tree")
        .AddClass(ClassString)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 Loading 图标</para>
    /// <para lang="en">Gets or sets Loading Icon</para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.LoadingIcon);
    }
}
