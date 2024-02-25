// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoGenerateColumn 标签类，用于 <see cref="Table{TItem}"/> 标识自动生成列
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class AutoGenerateClassAttribute : AutoGenerateBaseAttribute
{

}
