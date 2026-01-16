// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IValidateComponent 接口
///</para>
/// <para lang="en">IValidateComponent 接口
///</para>
/// </summary>
public interface IValidateComponent
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否不进行验证 默认为 false
    ///</para>
    /// <para lang="en">Gets or sets whether不进行验证 Default is为 false
    ///</para>
    /// </summary>
    bool IsNeedValidate { get; }

    /// <summary>
    /// <para lang="zh">判断是否需要进行复杂类验证
    ///</para>
    /// <para lang="en">判断whether需要进行复杂类验证
    ///</para>
    /// </summary>
    /// <returns></returns>
    bool IsComplexValue(object? value);

    /// <summary>
    /// <para lang="zh">数据验证方法
    ///</para>
    /// <para lang="en">data验证方法
    ///</para>
    /// </summary>
    /// <param name="propertyValue"></param>
    /// <param name="context"></param>
    /// <param name="results"></param>
    Task ValidatePropertyAsync(object? propertyValue, ValidationContext context, List<ValidationResult> results);

    /// <summary>
    /// <para lang="zh">显示或者隐藏提示信息方法
    ///</para>
    /// <para lang="en">display或者隐藏提示信息方法
    ///</para>
    /// </summary>
    /// <param name="results"></param>
    Task ToggleMessage(IReadOnlyCollection<ValidationResult> results);
}
