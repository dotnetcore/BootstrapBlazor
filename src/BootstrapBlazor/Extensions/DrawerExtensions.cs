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
    /// <param name="service">
    ///   <para lang="zh"><see cref="DrawerService"/> 服务实例</para>
    ///   <para lang="en"><see cref="DrawerService"/> instance</para>
    /// </param>
    /// <param name="editDialogOption">
    ///   <para lang="zh"><see cref="ITableEditDialogOption{TModel}"/> 配置类实例</para>
    ///   <para lang="en"><see cref="ITableEditDialogOption{TModel}"/> option instance</para>
    /// </param>
    /// <param name="option">
    ///   <para lang="zh"><see cref="DrawerOption"/> 配置类实例，编辑内容与关闭回调在当前展示的组件参数中组合，不改写原配置</para>
    ///   <para lang="en"><see cref="DrawerOption"/> option instance. Edit content and close callbacks are composed in the current display's component parameters without modifying the original configuration</para>
    /// </param>
    public static async Task ShowEditDrawer<TModel>(this DrawerService service, TableEditDrawerOption<TModel> editDialogOption, DrawerOption option)
    {
        var parameters = editDialogOption.ToParameter();
        RenderFragment content = builder =>
        {
            builder.OpenComponent<EditDialog<TModel>>(0);
            foreach (var parameter in parameters)
            {
                builder.AddAttribute(1, parameter.Key, parameter.Value);
            }
            builder.CloseComponent();
        };
        await service.Show(option, drawerParameters =>
        {
            var containerCloseAsync = (Func<Task>)drawerParameters[nameof(Drawer.OnCloseAsync)];
            var editCloseAsync = editDialogOption.OnCloseAsync;
            drawerParameters[nameof(Drawer.ChildContent)] = content;
            drawerParameters[nameof(Drawer.OnCloseAsync)] = new Func<Task>(async () =>
            {
                try
                {
                    if (editCloseAsync != null)
                    {
                        await editCloseAsync();
                    }
                }
                finally
                {
                    await containerCloseAsync();
                }
            });
        });
    }
}
