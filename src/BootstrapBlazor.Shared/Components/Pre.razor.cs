// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Services;
using System.Text.RegularExpressions;

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
    private CodeSnippetService? CodeSnippetService { get; set; }

    /// <summary>
    /// 获得/设置 子组件 CodeFile 为空时生效
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 代码段的标题
    /// </summary>
    [Parameter]
    public string? BlockName { get; set; }

    /// <summary>
    /// 获得/设置 示例代码片段 默认 null 未设置
    /// </summary>
    [Parameter]
    public string? CodeFile { get; set; }

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
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        LoadingText ??= Localizer[nameof(LoadingText)];
        TooltipTitle ??= Localizer[nameof(TooltipTitle)];
        PlusTooltipTitle ??= Localizer[nameof(PlusTooltipTitle)];
        MinusTooltipTitle ??= Localizer[nameof(MinusTooltipTitle)];
        CopiedText ??= Localizer[nameof(CopiedText)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
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
    /// OnAfterRender 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (Loaded)
        {
            await InvokeVoidAsync("highlight", Id);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, CopiedText);

    private async Task GetCodeAsync()
    {
        if (!string.IsNullOrEmpty(CodeFile))
        {
            var code = await CodeSnippetService.GetCodeAsync(CodeFile);
            if (!string.IsNullOrEmpty(code))
            {
                code = FindCodeSnippetByName(code);
                ChildContent = builder =>
                {
                    builder.AddContent(0, code);
                };
            }
            CanCopy = !string.IsNullOrEmpty(code) && !code.StartsWith("Error: ");
        }
        else
        {
            ChildContent = builder =>
            {
                builder.AddContent(0, "网站改版中 ... Refactoring website. Coming soon ...");
            };
            CanCopy = false;
        }
        Loaded = true;
    }

    private string FindCodeSnippetByName(string code)
    {
        var content = code;
        if (!string.IsNullOrEmpty(BlockName))
        {
            var regex = new Regex($"<DemoBlock [\\s\\S]* Name=\"{BlockName}\">([\\s\\S]*?)</DemoBlock>");
            var match = regex.Match(content);
            if (match.Success && match.Groups.Count == 2)
            {
                content = match.Groups[1].Value.Replace("\r\n", "\n").Replace("\n    ", "\n").TrimStart('\n');
            }

            // 移除 ConsoleLogger
            regex = ConsoleLoggerRegex();
            match = regex.Match(content);
            if (match.Success)
            {
                content = content.Replace(match.Value, "");
            }

            // 移除 Tips
            regex = TipsRegex();
            match = regex.Match(content);
            if (match.Success)
            {
                content = content.Replace(match.Value, "").TrimStart('\n');
            }
        }
        return content.TrimEnd('\n');
    }

    [GeneratedRegex("<ConsoleLogger [\\s\\S]* />")]
    private static partial Regex ConsoleLoggerRegex();

    [GeneratedRegex("<Tips[\\s\\S]*>[\\s\\S]*?</Tips>")]
    private static partial Regex TipsRegex();
}
