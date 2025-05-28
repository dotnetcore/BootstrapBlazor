// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IValidateComponent 接口
/// </summary>
public interface IValidateComponent
{
    /// <summary>
    /// 获得/设置 是否不进行验证 默认为 false
    /// </summary>
    bool IsNeedValidate { get; }

    /// <summary>
    /// 判断是否需要进行复杂类验证
    /// </summary>
    /// <returns></returns>
    bool IsComplexValue(object? value);

    /// <summary>
    /// 数据验证方法
    /// </summary>
    /// <param name="propertyValue"></param>
    /// <param name="context"></param>
    /// <param name="results"></param>
    Task ValidatePropertyAsync(object? propertyValue, ValidationContext context, List<ValidationResult> results);

    /// <summary>
    /// 显示或者隐藏提示信息方法
    /// </summary>
    /// <param name="results"></param>
    void ToggleMessage(IReadOnlyCollection<ValidationResult> results);
}
