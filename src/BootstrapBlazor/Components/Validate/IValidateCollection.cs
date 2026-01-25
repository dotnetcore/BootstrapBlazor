// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IValidateCollection 接口，支持多个验证结果和组件间联动验证</para>
/// <para lang="en">IValidateCollection Interface - Supports multiple validation results and cross-component validation linkage</para>
/// </summary>
public interface IValidateCollection
{
    /// <summary>
    /// <para lang="zh">验证方法</para>
    /// <para lang="en">Validates the specified validation context</para>
    /// </summary>
    /// <param name="validationContext"></param>
    IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

    /// <summary>
    /// <para lang="zh">返回合法成员集合</para>
    /// <para lang="en">Gets the valid member names collection</para>
    /// </summary>
    List<string> GetValidMemberNames();

    /// <summary>
    /// <para lang="zh">返回非法成员集合</para>
    /// <para lang="en">Gets the invalid member names collection</para>
    /// </summary>
    List<ValidationResult> GetInvalidMemberNames();
}
