// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IValidateCollection 多个验证结果接口 支持组件间联动验证
/// </summary>
public interface IValidateCollection
{
    /// <summary>
    /// 验证方法
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

    /// <summary>
    /// 返回合法成员集合
    /// </summary>
    /// <returns></returns>
    List<string> GetValidMemberNames();

    /// <summary>
    /// 返回非法成员集合
    /// </summary>
    /// <returns></returns>
    List<ValidationResult> GetInvalidMemberNames();
}
