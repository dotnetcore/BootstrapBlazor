// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Step 组件项类</para>
///  <para lang="en">Step Component Option Class</para>
/// </summary>
public class StepOption
{
    /// <summary>
    ///  <para lang="zh">获得/设置 步骤显示文字</para>
    ///  <para lang="en">Get/Set Text</para>
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 步骤显示图标</para>
    ///  <para lang="en">Get/Set Icon</para>
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 步骤完成显示图标</para>
    ///  <para lang="en">Get/Set Finished Icon</para>
    /// </summary>
    public string? FinishedIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 步骤显示标题</para>
    ///  <para lang="en">Get/Set Title</para>
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 描述信息</para>
    ///  <para lang="en">Get/Set Description</para>
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Header 模板</para>
    ///  <para lang="en">Get/Set Header Template</para>
    /// </summary>
    public RenderFragment<StepOption>? HeaderTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Title 模板</para>
    ///  <para lang="en">Get/Set Title Template</para>
    /// </summary>
    public RenderFragment<StepOption>? TitleTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 每个 step 内容模板</para>
    ///  <para lang="en">Get/Set Template</para>
    /// </summary>
    public RenderFragment? Template { get; set; }
}
