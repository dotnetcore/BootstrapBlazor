// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">条件输出自组件</para>
/// <para lang="en">Conditional Output Component</para>
/// </summary>
public class Block : BootstrapComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 Block 名字 此名字通过 <see cref="OnQueryCondition"/> 第一个参数传递给使用者</para>
    /// <para lang="en">Gets or sets the Block name. This name is passed to the user via the first parameter of <see cref="OnQueryCondition"/></para>
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Block 允许的角色集合</para>
    /// <para lang="en">Gets or sets the allowed roles for the Block</para>
    /// </summary>
    [Parameter]
    public IEnumerable<string>? Roles { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Block 允许的用户集合</para>
    /// <para lang="en">Gets or sets the allowed users for the Block</para>
    /// </summary>
    [Parameter]
    public IEnumerable<string>? Users { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示此 Block 设置 true 时显示</para>
    /// <para lang="en">Gets or sets whether to show this Block. The content is shown when set to true</para>
    /// </summary>
    [Parameter]
    public Func<string?, Task<bool>>? OnQueryCondition { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示此 Block 默认值为 null（不参与判断），未设置任何判断条件时不显示</para>
    /// <para lang="en">Gets or sets whether to show this Block. Default is null (not participating in judgment); the content is hidden when no condition is configured</para>
    /// </summary>
    [Parameter]
    public bool? Condition { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件内容</para>
    /// <para lang="en">Gets or sets the child content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 符合条件显示的内容</para>
    /// <para lang="en">Gets or sets the authorized content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? Authorized { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 不符合条件显示的内容</para>
    /// <para lang="en">Gets or sets the not authorized content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? NotAuthorized { get; set; }

    [Inject, NotNull]
    private IServiceProvider? ServiceProvider { get; set; }

    private bool IsShow { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        IsShow = false;
        if (Users != null || Roles != null)
        {
            IsShow = await ProcessAuthorizeAsync();
        }
        else if (Condition.HasValue)
        {
            IsShow = Condition.Value;
        }
        else if (OnQueryCondition != null)
        {
            IsShow = await OnQueryCondition(Name);
        }
    }

    private async Task<bool> ProcessAuthorizeAsync()
    {
        var ret = false;

        var provider = ServiceProvider.GetService<AuthenticationStateProvider>();
        if (provider != null)
        {
            var state = await provider.GetAuthenticationStateAsync();
            var user = state.User;
            if (user.Identity is { IsAuthenticated: true })
            {
                ret = IsAllowed(Users, i => i.Equals(user.Identity.Name, StringComparison.OrdinalIgnoreCase)) && IsAllowed(Roles, user.IsInRole);
            }
        }

        return ret;
    }

    private static bool IsAllowed(IEnumerable<string>? values, Func<string, bool> predicate)
    {
        // 为空是直接返回 true 允许
        if (values == null)
        {
            return true;
        }

        var hasValue = false;
        foreach (var value in values)
        {
            hasValue = true;
            if (predicate(value))
            {
                return true;
            }
        }

        // values 集合为空时返回 true 允许
        return !hasValue;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (IsShow)
        {
            builder.AddContent(0, Authorized ?? ChildContent);
        }
        else
        {
            builder.AddContent(0, NotAuthorized);
        }
    }
}
