// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    internal class TabAuthorizeView : ComponentBase
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

        private bool Authorized { get; set; }

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            Authorized = await IsAuthorizedAsync(RouteContext.Handler!);
        }

        /// <summary>
        /// BuildRenderTree 方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            // 判断是否开启权限
            if (Authorized)
            {
                var index = 0;
                builder.OpenComponent(index++, RouteContext.Handler!);
                foreach (var kv in (RouteContext.Parameters ?? new ReadOnlyDictionary<string, object>(new Dictionary<string, object>())))
                {
                    builder.AddAttribute(index++, kv.Key, kv.Value);
                }
                builder.CloseComponent();

            }
            else
            {
                builder.AddContent(0, NotAuthorized);
            }

        }

        private async Task<bool> IsAuthorizedAsync(Type type)
        {
            var ret = true;
            var authorizeData = AttributeAuthorizeDataCache.GetAuthorizeDataForType(type);
            if (authorizeData != null)
            {
                EnsureNoAuthenticationSchemeSpecified(authorizeData);

                if (AuthenticationState != null && AuthorizationPolicyProvider != null && AuthorizationService != null)
                {
                    var currentAuthenticationState = await AuthenticationState;
                    var user = currentAuthenticationState.User;
                    var policy = await AuthorizationPolicy.CombineAsync(AuthorizationPolicyProvider, authorizeData);
                    if (policy != null)
                    {
                        var result = await AuthorizationService.AuthorizeAsync(user, Resource, policy);
                        ret = result.Succeeded;
                    }
                }
            }
            return ret;
        }

        private static void EnsureNoAuthenticationSchemeSpecified(IAuthorizeData[] authorizeData)
        {
            // It's not meaningful to specify a nonempty scheme, since by the time Components
            // authorization runs, we already have a specific ClaimsPrincipal (we're stateful).
            // To avoid any confusion, ensure the developer isn't trying to specify a scheme.
            for (var i = 0; i < authorizeData.Length; i++)
            {
                var entry = authorizeData[i];
                if (!string.IsNullOrEmpty(entry.AuthenticationSchemes))
                {
                    throw new NotSupportedException($"The authorization data specifies an authentication scheme with value '{entry.AuthenticationSchemes}'. Authentication schemes cannot be specified for components.");
                }
            }
        }
    }
}
