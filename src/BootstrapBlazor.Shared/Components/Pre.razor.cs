// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Services;

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// Pre 组件
/// </summary>
public partial class Pre
{
    private bool Loaded { get; set; }

    private bool CanCopy { get; set; }

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    private string? ClassString => CssBuilder.Default("pre-code")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    [Inject]
    [NotNull]
    private CodeSnippetService? Example { get; set; }

    /// <summary>
    /// 获得/设置 子组件 CodeFile 为空时生效
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 代码段的标题
    /// </summary>
    [Parameter]
    public string? BlockTitle { get; set; }

    /// <summary>
    /// 获得/设置 示例代码片段 默认 null 未设置
    /// </summary>
    [Parameter]
    public string? Demo { get; set; }

    /// <summary>
    /// 获得/设置 是否显示工具按钮组
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Pre>? Localizer { get; set; }

    private string? LoadingText { get; set; }

    private string? TooltipTitle { get; set; }

    private string? PlusTooltipTitle { get; set; }

    private string? MinusTooltipTitle { get; set; }

    private string? CopiedText { get; set; }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (ChildContent == null)
        {
            await GetCodeAsync();
        }
        else
        {
            Loaded = true;
            CanCopy = true;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        LoadingText ??= Localizer[nameof(LoadingText)];
        TooltipTitle ??= Localizer[nameof(TooltipTitle)];
        PlusTooltipTitle ??= Localizer[nameof(PlusTooltipTitle)];
        MinusTooltipTitle ??= Localizer[nameof(MinusTooltipTitle)];
        CopiedText ??= Localizer[nameof(CopiedText)];
    }

    /// <summary>
    /// OnAfterRender 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (Loaded)
        {
            await Hightlight();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, CopiedText);

    private async Task GetCodeAsync()
    {
        if (!string.IsNullOrEmpty(Demo))
        {
            var code = await Example.GetCodeAsync(Demo);
            if (!string.IsNullOrEmpty(code))
            {
                ChildContent = builder =>
                {
                    builder.AddContent(0, code);
                };
            }
            CanCopy = !string.IsNullOrEmpty(code) && !code.StartsWith("Error: ");
        }
        Loaded = true;
    }

    private Task Hightlight() => InvokeVoidAsync("execute", Id, "highlight");
}
