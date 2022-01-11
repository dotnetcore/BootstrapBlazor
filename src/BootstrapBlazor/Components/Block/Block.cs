// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    [Inject]
    private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }

    private bool IsShow { get; set; } = true;

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
        AuthenticationState? state = null;
        if (AuthenticationStateProvider != null)
        {
            state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        }
        var isAuthenticated = state!.User.Identity?.IsAuthenticated ?? false;
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
