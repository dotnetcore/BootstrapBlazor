// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Routing;
using System.Collections.ObjectModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
internal class BootstrapBlazorAuthorizeView : ComponentBase
{
    /// <summary>
    /// 获得/设置 路由关联上下文
    /// </summary>
    [Parameter]
    [NotNull]
    public RouteContext? RouteContext { get; set; }

    /// <summary>
    /// 获得/设置 NotAuthorized 模板
    /// </summary>
    [Parameter]
    public RenderFragment? NotAuthorized { get; set; }

    /// <summary>
    /// The resource to which access is being controlled.
    /// </summary>
    [Parameter]
    public object? Resource { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState>? AuthenticationState { get; set; }

    [Inject]
    private IAuthorizationPolicyProvider? AuthorizationPolicyProvider { get; set; }

    [Inject]
    private IAuthorizationService? AuthorizationService { get; set; }

#if NET6_0_OR_GREATER
    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }
#endif

    private bool Authorized { get; set; }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Authorized = RouteContext.Handler == null
            || await RouteContext.Handler.IsAuthorizedAsync(AuthenticationState, AuthorizationPolicyProvider, AuthorizationService, Resource);
    }

    /// <summary>
    /// BuildRenderTree 方法
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        // 判断是否开启权限
        if (Authorized && RouteContext.Handler != null)
        {
            var index = 0;
            builder.OpenComponent(index++, RouteContext.Handler);
            foreach (var kv in (RouteContext.Parameters ?? new ReadOnlyDictionary<string, object>(new Dictionary<string, object>())))
            {
                builder.AddAttribute(index++, kv.Key, kv.Value);
            }
#if NET6_0_OR_GREATER
            BuildQueryParameters();
#endif
            builder.CloseComponent();
        }
        else
        {
            builder.AddContent(0, NotAuthorized);
        }

#if NET6_0_OR_GREATER
        void BuildQueryParameters()
        {
            var queryParameterSupplier = QueryParameterValueSupplier.ForType(RouteContext.Handler);
            if (queryParameterSupplier is not null)
            {
                // Since this component does accept some parameters from query, we must supply values for all of them,
                // even if the querystring in the URI is empty. So don't skip the following logic.
                var url = NavigationManager.Uri;
                var queryStartPos = url.IndexOf('?');
                var query = queryStartPos < 0 ? default : url.AsMemory(queryStartPos);
                queryParameterSupplier.RenderParametersFromQueryString(builder, query);
            }
        }
#endif
    }
}
