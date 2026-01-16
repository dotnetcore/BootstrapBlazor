// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Popover 弹出窗组件</para>
/// <para lang="en">Popover Component</para>
/// </summary>
public partial class Popover
{
    /// <summary>
    /// <para lang="zh">获得/设置 显示文字，复杂内容可通过 <see cref="Template"/> 自定义</para>
    /// <para lang="en">Get/Set Display text. Complex content can be customized via <see cref="Template"/></para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示阴影 默认 true</para>
    /// <para lang="en">Get/Set Whether to show shadow. Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 内容模板 默认 null 设置值后 <see cref="Content"/> 参数失效</para>
    /// <para lang="en">Get/Set Content Template. Default null. <see cref="Content"/> parameter is invalid if set</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? Template { get; set; }

    private string? _lastContent;

    private string? ClassString => CssBuilder.Default("bb-popover")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? CustomClassString => CssBuilder.Default(CustomClass)
        .AddClass("shadow", ShowShadow)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new { Content, Template = Template != null });

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _lastContent = Content;
        }
        else if (_lastContent != Content)
        {
            _lastContent = Content;
            await InvokeInitAsync();
        }
    }
}
