// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 选项最小数验证实现类
/// </summary>
public class MinValidator : MaxValidator
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public MinValidator()
    {
        ErrorMessage = "Select at least {0} items";
    }

    /// <summary>
    /// 验证方法 大于等于 Value 时 返回 true
    /// </summary>
    protected override bool Validate(int count) => count >= Value;
}
