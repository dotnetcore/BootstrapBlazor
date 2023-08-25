// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// CountButton 组件
/// </summary>
public class CountButton : Button
{
    /// <summary>
    /// 倒计时数量 默认 5 秒
    /// </summary>
    [Parameter]
    public int Count { get; set; } = 5;

    /// <summary>
    /// 倒计时文本 默认 null 使用 <see cref="ButtonBase.Text"/> 参数
    /// </summary>
    [Parameter]
    public string? CountText { get; set; }

    /// <summary>
    /// 倒计时格式化回调方法
    /// </summary>
    [Parameter]
    public Func<int, string>? CountTextCallback { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void SetClickHandler()
    {
        OnClickButton = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
        {
            IsAsyncLoading = true;
            ButtonIcon = LoadingIcon;
            IsDisabled = true;

            await Task.Run(() => InvokeAsync(HandlerClick));
            await UpdateCount();

            IsDisabled = false;
            ButtonIcon = Icon;
            IsAsyncLoading = false;
        });
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task HandlerClick()
    {
        if (OnClickWithoutRender != null)
        {
            await OnClickWithoutRender.Invoke();
        }
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }
    }

    private async Task UpdateCount()
    {
        var count = Count;
        var text = Text;
        ButtonIcon = null;
        while (count > 0)
        {
            Text = GetCountText(count--, text);
            StateHasChanged();
            await Task.Delay(1000);
        }
        Text = text;
    }

    private string GetCountText(int count, string? text)
    {
        var ret = "";
        if (CountTextCallback != null)
        {
            ret = CountTextCallback(count);
        }
        else
        {
            var countText = CountText ?? text;
            if (!string.IsNullOrEmpty(countText))
            {
                ret = $" {countText}";
            }
            ret = $"({count}){ret}";
        }
        return ret;
    }
}
