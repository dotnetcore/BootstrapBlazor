// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Collapse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collapsed"></param>
        /// <returns></returns>
        private static string? GetButtonClassString(bool collapsed) => CssBuilder.Default("btn btn-link")
            .AddClass("collapsed", collapsed)
            .Build();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collpased"></param>
        /// <returns></returns>
        private static string? GetClassString(bool collpased) => CssBuilder.Default("collapse-item")
            .AddClass("collapse", collpased)
            .AddClass("collapse show", !collpased)
            .Build();

        private static string? TitleClassString(Color color) => CssBuilder.Default("card-header")
            .AddClass($"bg-{color.ToDescriptionString()}", color != Color.None)
            .Build();

        private async Task OnClickItem(CollapseItem item)
        {
            item.SetCollapsed(!item.IsCollapsed);
            if (OnCollapseChanged != null)
            {
                await OnCollapseChanged(item);
            }
        }
    }
}
