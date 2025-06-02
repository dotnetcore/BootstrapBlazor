// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    public string? Id { get; set; }

    /// <summary>
    /// 获得 <see cref="IComponentIdGenerator"/> 实例
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
}
