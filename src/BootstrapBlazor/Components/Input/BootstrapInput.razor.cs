// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">BootstrapInput 组件</para>
///  <para lang="en">BootstrapInput Component</para>
/// </summary>
public partial class BootstrapInput<TValue>
{
    /// <summary>
    ///  <para lang="zh">获得/设置 是否为只读 默认 false</para>
    ///  <para lang="en">Get/Set Readonly. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool Readonly { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 用户删除后是否自动更改为默认值 0 默认 false</para>
    ///  <para lang="en">Get/Set Whether to automatically set default value when user deletes. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool AutoSetDefaultWhenNull { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示清空小按钮 默认 false</para>
    ///  <para lang="en">Get/Set Whether to show clear button. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 IsClearable 参数；Deprecated use the IsClearable parameter")]
    [ExcludeFromCodeCoverage]
    public bool Clearable { get => IsClearable; set => IsClearable = value; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示清空小按钮 默认 false</para>
    ///  <para lang="en">Get/Set Whether to show clear button. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 清空文本框时回调方法 默认 null</para>
    ///  <para lang="en">Get/Set Callback when clearing text box. Default null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnClear { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 清空小按钮图标 默认 null</para>
    ///  <para lang="en">Get/Set Clear button icon. Default null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 ClearIcon 参数；Deprecated use the ClearIcon parameter")]
    [ExcludeFromCodeCoverage]
    public string? ClearableIcon { get => ClearIcon; set => ClearIcon = value; }

    /// <summary>
    ///  <para lang="zh">获得/设置 清空小按钮图标 默认 null</para>
    ///  <para lang="en">Get/Set Clear button icon. Default null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ClearIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">图标主题服务</para>
    ///  <para lang="en">Icon Theme Service</para>
    /// </summary>
    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? ReadonlyString => Readonly ? "true" : null;

    private string? ClearableIconString => CssBuilder.Default("form-control-clear-icon")
        .AddClass(ClearIcon)
        .Build();

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.InputClearIcon);
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <param name="validationErrorMessage"></param>
    /// <returns></returns>
    protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TValue result, out string? validationErrorMessage)
    {
        bool ret;
        var v = IsTrim ? value.Trim() : value;
        if (AutoSetDefaultWhenNull && string.IsNullOrEmpty(v))
        {
            result = default!;
            validationErrorMessage = null;
            ret = true;
        }
        else
        {
            ret = base.TryParseValueFromString(v, out result, out validationErrorMessage);
        }
        return ret;
    }

    private async Task OnClickClear()
    {
        if (OnClear != null)
        {
            await OnClear(Value);
        }
        CurrentValueAsString = "";
    }
}
