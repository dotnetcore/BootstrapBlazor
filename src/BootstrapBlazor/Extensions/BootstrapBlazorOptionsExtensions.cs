// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BootstrapBlazorOptions 配置类扩展方法</para>
/// <para lang="en">BootstrapBlazorOptions extension methods</para>
/// </summary>
public static class BootstrapBlazorOptionsExtensions
{
    /// <summary>
    /// <para lang="zh">获得 步长</para>
    /// <para lang="en">Gets the step size</para>
    /// </summary>
    /// <typeparam name="TType">The type parameter</typeparam>
    /// <param name="options">The BootstrapBlazorOptions instance</param>
    public static string? GetStep<TType>(this BootstrapBlazorOptions options) => options.GetStep(typeof(TType));

    /// <summary>
    /// <para lang="zh">获得 步长</para>
    /// <para lang="en">Gets the step size</para>
    /// </summary>
    /// <param name="options">The BootstrapBlazorOptions instance</param>
    /// <param name="type">The data type</param>
    public static string? GetStep(this BootstrapBlazorOptions options, Type type)
    {
        var t = Nullable.GetUnderlyingType(type) ?? type;
        return options.StepSettings.GetStep(t);
    }

    /// <summary>
    /// <para lang="zh">获得 Modal IsFade 值</para>
    /// <para lang="en">Gets the Modal IsFade value</para>
    /// </summary>
    /// <param name="options">The BootstrapBlazorOptions instance</param>
    /// <param name="value">The default value</param>
    public static bool GetIsFadeValue(this BootstrapBlazorOptions options, bool? value) => value ?? options.ModalSettings.IsFade ?? true;

    /// <summary>
    /// <para lang="zh">获得 EditDialog 是否显示关闭确认弹窗值</para>
    /// <para lang="en">Gets whether to show the EditDialog close confirm dialog</para>
    /// </summary>
    /// <param name="options">The BootstrapBlazorOptions instance</param>
    /// <param name="value">The default value</param>
    /// <param name="modified">Indicates whether the value has been modified</param>
    public static bool GetEditDialogShowConfirmSwal(this BootstrapBlazorOptions options, bool? value, bool modified) => (value ?? options.EditDialogSettings.ShowConfirmCloseSwal ?? false) && modified;
}
