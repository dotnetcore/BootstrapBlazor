// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    List<string> ValidMemberNames();

    /// <summary>
    /// 返回非法成员集合
    /// </summary>
    /// <returns></returns>
    List<ValidationResult> InvalidMemberNames();
}
