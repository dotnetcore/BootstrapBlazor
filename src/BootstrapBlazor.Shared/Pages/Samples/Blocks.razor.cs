// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Blocks
    {
        private bool IsShow { get; set; } = true;

        private void ToggleCondition() => IsShow = !IsShow;

        private string GetIcon() => IsShow ? "fa fa-eye-slash" : "fa fa-eye";

        private string GetText() => IsShow ? "隐藏" : "显示";

        private Task<bool> OnQueryCondition() => Task.FromResult(IsShow);

        #region 示例二
        [Inject]
        [NotNull]
        private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }

        private bool IsAuth { get; set; }

        private string? UserName { get; set; }

        private async Task ToggleAuthor()
        {
            if (AuthenticationStateProvider is MockAuthenticationStateProvider mock)
            {
                if (!IsAuth)
                {
                    mock.Login();
                    var state = await mock.GetAuthenticationStateAsync();
                    UserName = state.User.Identity?.Name;
                    IsAuth = state.User.Identity?.IsAuthenticated ?? false;
                }
                else
                {
                    mock.Logout();
                    IsAuth = false;
                    UserName = "";
                }
            }
        }

        private string GetUser() => IsAuth ? "fa fa-user-secret" : "fa fa-user";

        private string GetUserText() => IsAuth ? "登出" : "登入";

        private Task<bool> OnQueryUser() => Task.FromResult(IsAuth);
        #endregion

        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            new AttributeItem()
            {
                Name = nameof(Block.OnQueryCondition),
                Description = "是否显示此 Block",
                Type = "Func<Task<bool>>",
                ValueList = " - ",
                DefaultValue = "true"
            },
            new AttributeItem()
            {
                Name = nameof(Block.ChildContent),
                Description = "Block 块内显示内容",
                Type = "RenderFragment",
                ValueList = " - ",
                DefaultValue = " - "
            }
        };
    }
}
