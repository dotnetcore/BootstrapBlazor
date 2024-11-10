// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace BootstrapBlazor.Components;

/// <summary>
/// 条件输出自组件
/// </summary>
public class Block : BootstrapComponentBase
{
    /// <summary>
    /// 获得/设置 Block 名字 此名字通过 <see cref="OnQueryCondition"/> 第一个参数传递给使用者
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// 获得/设置 Block 允许的角色集合
    /// </summary>
    [Parameter]
    public IEnumerable<string>? Roles { get; set; }

    /// <summary>
    /// 获得/设置 Block 允许的用户集合
    /// </summary>
    [Parameter]
    public IEnumerable<string>? Users { get; set; }

    /// <summary>
    /// 获得/设置 是否显示此 Block 默认显示 返回 true 时显示
    /// </summary>
    [Parameter]
    public Func<string?, Task<bool>>? OnQueryCondition { get; set; }

    /// <summary>
    /// 获得/设置 是否显示此 Block 默认显示 null 未参与判断 设置 true 时显示
    /// </summary>
    [Parameter]
    public bool? Condition { get; set; }

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 符合条件显示的内容
    /// </summary>
    [Parameter]
    public RenderFragment? Authorized { get; set; }

    /// <summary>
    /// 获得/设置 不符合条件显示的内容
    /// </summary>
    [Parameter]
    public RenderFragment? NotAuthorized { get; set; }

    [Inject, NotNull]
    private IServiceProvider? ServiceProvider { get; set; }

    private bool IsShow { get; set; }

    /// <summary>
    /// OnParametersSetAsync 方法
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
    /// BuildRenderTree 方法
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
