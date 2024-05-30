// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// 抽屉服务扩展方法
/// </summary>
public static class DrawerExtensions
{
    /// <summary>
    /// 弹出编辑抽屉
    /// </summary>
    /// <param name="service"><see cref="DrawerService"/> 服务实例</param>
    /// <param name="editOption"><see cref="TableEditDrawerOption{TModel}"/> 配置类实例</param>
    /// <param name="option"><see cref="DrawerOption"/> 配置类实例</param>
    public static async Task ShowEditDrawer<TModel>(this DrawerService service, TableEditDrawerOption<TModel> editOption, DrawerOption option)
    {
        option.ChildContent = BootstrapDynamicComponent.CreateComponent<EditDialog<TModel>>(editOption.ToParameter()).Render();
        await service.Show(option);
    }
}
