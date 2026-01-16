// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">条件输出自组件</para>
///  <para lang="en">Conditional Output Component</para>
/// </summary>
public class Block : BootstrapComponentBase
{
    /// <summary>
    ///  <para lang="zh">获得/设置 Block 名字 此名字通过 <see cref="OnQueryCondition"/> 第一个参数传递给使用者</para>
    ///  <para lang="en">Gets or sets the Block name. This name is passed to the user via the first parameter of <see cref="OnQueryCondition"/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Block 允许的角色集合</para>
    ///  <para lang="en">Gets or sets the allowed roles for the Block</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public IEnumerable<string>? Roles { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Block 允许的用户集合</para>
    ///  <para lang="en">Gets or sets the allowed users for the Block</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public IEnumerable<string>? Users { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示此 Block 默认显示 返回 true 时显示</para>
    ///  <para lang="en">Gets or sets whether to show this Block. Default is true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<string?, Task<bool>>? OnQueryCondition { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示此 Block 默认显示 null 未参与判断 设置 true 时显示</para>
    ///  <para lang="en">Gets or sets whether to show this Block. Default is null (not participating in judgment). Show if set to true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? Condition { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 子组件内容</para>
    ///  <para lang="en">Gets or sets the child content</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 符合条件显示的内容</para>
    ///  <para lang="en">Gets or sets the authorized content</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? Authorized { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 不符合条件显示的内容</para>
    ///  <para lang="en">Gets or sets the not authorized content</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? NotAuthorized { get; set; }

    [Inject, NotNull]
    private IServiceProvider? ServiceProvider { get; set; }

    private bool IsShow { get; set; }

    /// <summary>
    ///  <para lang="zh">OnParametersSetAsync 方法</para>
    ///  <para lang="en">OnParametersSetAsync method</para>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

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
        bool isAuthenticated = false;
        AuthenticationState? state = null;
        var provider = ServiceProvider.GetService<AuthenticationStateProvider>();
        if (provider != null)
        {
            state = await provider.GetAuthenticationStateAsync();
        }
        if (state != null)
        {
            var identity = state.User.Identity;
            if (identity != null)
            {
                isAuthenticated = identity.IsAuthenticated;
            }
        }
        if (isAuthenticated)
        {
            if (Users?.Any() ?? false)
            {
                var userName = state!.User.Identity!.Name;
                isAuthenticated = Users.Any(i => i.Equals(userName, StringComparison.OrdinalIgnoreCase));
            }
            if (Roles?.Any() ?? false)
            {
                isAuthenticated = Roles.Any(i => state!.User.IsInRole(i));
            }
        }
        return isAuthenticated;
    }

    /// <summary>
    ///  <para lang="zh">BuildRenderTree 方法</para>
    ///  <para lang="en">BuildRenderTree method</para>
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
