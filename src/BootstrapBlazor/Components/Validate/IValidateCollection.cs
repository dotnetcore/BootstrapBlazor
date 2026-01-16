// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IValidateCollection 多个验证结果接口 支持组件间联动验证</para>
/// <para lang="en">IValidateCollection 多个验证结果接口 支持component间联动验证</para>
/// </summary>
public interface IValidateCollection
{
    /// <summary>
    /// <para lang="zh">验证方法</para>
    /// <para lang="en">验证方法</para>
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

    /// <summary>
    /// <para lang="zh">返回合法成员集合</para>
    /// <para lang="en">返回合法成员collection</para>
    /// </summary>
    /// <returns></returns>
    List<string> GetValidMemberNames();

    /// <summary>
    /// <para lang="zh">返回非法成员集合</para>
    /// <para lang="en">返回非法成员collection</para>
    /// </summary>
    /// <returns></returns>
    List<ValidationResult> GetInvalidMemberNames();
}
