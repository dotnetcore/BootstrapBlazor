// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">抽屉服务扩展方法</para>
/// <para lang="en">Drawer Service Extensions</para>
/// </summary>
public static class DrawerExtensions
{
    /// <summary>
    /// <para lang="zh">弹出编辑抽屉</para>
    /// <para lang="en">Show edit drawer</para>
    /// </summary>
    /// <param name="service"><para lang="zh"><see cref="DrawerService"/> 服务实例</para><para lang="en"><see cref="DrawerService"/> instance</para></param>
    /// <param name="editDialogOption"><para lang="zh"><see cref="ITableEditDialogOption{TModel}"/> 配置类实例</para><para lang="en"><see cref="ITableEditDialogOption{TModel}"/> option instance</para></param>
    /// <param name="option"><para lang="zh"><see cref="DrawerOption"/> 配置类实例</para><para lang="en"><see cref="DrawerOption"/> option instance</para></param>
    public static async Task ShowEditDrawer<TModel>(this DrawerService service, TableEditDrawerOption<TModel> editDialogOption, DrawerOption option)
    {
        var parameters = editDialogOption.ToParameter();
        parameters.Add(nameof(EditDialog<TModel>.OnCloseAsync), editDialogOption.OnCloseAsync);
        option.ChildContent = BootstrapDynamicComponent.CreateComponent<EditDialog<TModel>>(parameters).Render();
        await service.Show(option);
    }
}
