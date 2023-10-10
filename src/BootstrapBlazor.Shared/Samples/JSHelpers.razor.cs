// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

public partial class JSHelpers
{
    #region RunJSWithEval Demo

    private string? evalJSContent { get; set; } = "alert('Hello BootstrapBlazor')";

    private string? evalJSResult { get; set; }

    private async Task RunJSWithEvalDemo()
    {
        if (!string.IsNullOrEmpty(evalJSContent))
        {
            var res = await JSHelper.RunJSWithEval<object>(evalJSContent);
            if (res is not null)
            {
                evalJSResult = res.ToString();
            }
            else
            {
                evalJSResult = string.Empty;
            }
        }
    }

    #endregion

    #region JS Alert Demo

    private string jsAlertContent { get; set; } = "Hello BootstrapBlazor";

    private async Task RunJSAlert() => await JSHelper.Alert(jsAlertContent);

    #endregion

    private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
    {
        new()
        {
            Name = nameof(IBootstrapBlazorJSHelper.AddLink),
            Description = "动态添加link Dynamically adding links",
            Parameters = " - ",
            ReturnValue = "ValueTask"
        },
        new()
        {
            Name = nameof(IBootstrapBlazorJSHelper.RemoveLink),
            Description = "动态移除link Dynamically remove links",
            Parameters = " - ",
            ReturnValue = "ValueTask"
        },
        new()
        {
            Name = nameof(IBootstrapBlazorJSHelper.AddScript),
            Description = "动态添加script Dynamically add script",
            Parameters = " - ",
            ReturnValue = "ValueTask"
        },
        new()
        {
            Name = nameof(IBootstrapBlazorJSHelper.RemoveScript),
            Description = "动态移除script Dynamically remove script",
            Parameters = " - ",
            ReturnValue = "ValueTask"
        },
        new()
        {
            Name = nameof(IBootstrapBlazorJSHelper.RunJSWithEval),
            Description = "同步运行js代码，并返回值，当前作用域",
            Parameters = " - ",
            ReturnValue = "ValueTask | ValueTask<T?>"
        },
        new()
        {
            Name = nameof(IBootstrapBlazorJSHelper.RunJSWithFunction),
            Description = "同步运行js代码，并返回值，全局作用域",
            Parameters = " - ",
            ReturnValue = "ValueTask | ValueTask<T?>"
        },
        new()
        {
            Name = nameof(IBootstrapBlazorJSHelper.GetElementPropertiesByTagAsync),
            Description = "通过tag获取Element元素的属性值",
            Parameters = " - ",
            ReturnValue = "ValueTask<T?>"
        },
        new()
        {
            Name = nameof(IBootstrapBlazorJSHelper.GetDocumentPropertiesByTagAsync),
            Description = "通过tag获取元素的属性值",
            Parameters = " - ",
            ReturnValue = "ValueTask<T?>"
        },
        new()
        {
            Name = nameof(IBootstrapBlazorJSHelper.GetElementPropertiesByTagFromIdAsync),
            Description = "通过id查找元素，获取tag的属性值",
            Parameters = " - ",
            ReturnValue = "ValueTask<T?>"
        },
        new()
        {
            Name = nameof(IBootstrapBlazorJSHelper.RegisterEvent),
            Description = "注册浏览器事件",
            Parameters = " - ",
            ReturnValue = "ValueTask"
        }
    };
}
