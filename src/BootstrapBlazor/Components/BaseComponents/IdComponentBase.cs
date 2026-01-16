// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">自动生成客户端 ID 组件基类</para>
///  <para lang="en">Base class for auto-generating client ID component</para>
/// </summary>
public abstract class IdComponentBase : BootstrapComponentBase
{
    /// <summary>
    ///  <para lang="zh">获得/设置 组件 id 属性</para>
    ///  <para lang="en">Gets or sets the component id</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Id { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 <see cref="IComponentIdGenerator"/> 实例</para>
    ///  <para lang="en">Gets the <see cref="IComponentIdGenerator"/> instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected IComponentIdGenerator? ComponentIdGenerator { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 弹窗客户端 ID</para>
    ///  <para lang="en">Gets the popup client ID</para>
    /// </summary>
    protected virtual string? RetrieveId() => Id;

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Id ??= ComponentIdGenerator.Generate(this);
    }
}
