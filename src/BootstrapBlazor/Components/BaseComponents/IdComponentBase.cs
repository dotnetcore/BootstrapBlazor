// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 自动生成客户端 ID 组件基类
/// </summary>
public abstract class IdComponentBase : BootstrapComponentBase
{
    /// <summary>
    /// 获得/设置 组件 id 属性
    /// </summary>
    [Parameter]
    [NotNull]
    public virtual string? Id { get; set; }

    /// <summary>
    /// 获得 IComponentIdGenerator 实例
    /// </summary>
    [Inject]
    [NotNull]
    protected IComponentIdGenerator? ComponentIdGenerator { get; set; }

    /// <summary>
    /// 获得 弹窗客户端 ID
    /// </summary>
    protected virtual string? RetrieveId() => Id;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Id ??= ComponentIdGenerator.Generate(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Id ??= ComponentIdGenerator.Generate(this);
    }
}
