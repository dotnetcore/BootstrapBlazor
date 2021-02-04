// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public static class NavigationManagerExtensions
    {
        /// <summary>
        /// 导航并添加 TabItem 方法
        /// </summary>
        /// <param name="navigation"></param>
        /// <param name="url"></param>
        /// <param name="text"></param>
        /// <param name="icon"></param>
        public static void NavigateTo(this NavigationManager navigation, string url, string text, string? icon = null)
        {
            var option = ServiceProviderHelper.ServiceProvider.GetRequiredService<TabItemTextOptions>();
            option.Text = text;
            option.Icon = icon;
            navigation.NavigateTo(url);
        }
    }
}
