// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">CountButton 组件</para>
/// <para lang="en">CountButton component</para>
/// </summary>
public class CountButton : Button
{
    /// <summary>
    /// <para lang="zh">倒计时数量 默认 5 秒</para>
    /// <para lang="en">Countdown seconds. Default is 5</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Count { get; set; } = 5;

    /// <summary>
    /// <para lang="zh">倒计时文本 默认 null 使用 <see cref="ButtonBase.Text"/> 参数</para>
    /// <para lang="en">Countdown text. Default is null (uses <see cref="ButtonBase.Text"/>)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CountText { get; set; }

    /// <summary>
    /// <para lang="zh">倒计时格式化回调方法</para>
    /// <para lang="en">Countdown format callback method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<int, string>? CountTextCallback { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override async Task OnClickButton()
    {
        IsAsyncLoading = true;
        IsDisabled = true;

        await Task.Yield();
        await HandlerClick();
        await UpdateCount();

        IsDisabled = false;
        IsAsyncLoading = false;
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
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
