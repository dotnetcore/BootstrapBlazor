// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// ValidataResult 验证结果接口配合 <see cref="IValidatableObject"/> 接口使用
/// </summary>
public interface IValidataResult
{
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
