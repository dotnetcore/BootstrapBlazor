// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// CustomTreeItem 组件
/// </summary>
class CustomTreeItem : ComponentBase
{
    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    /// <summary>
    /// Foo 实例
    /// </summary>
    [Parameter]
    [NotNull]
    public TreeFoo? Foo { get; set; }

    /// <summary>
    /// BuildRenderTree
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(3, "span");
        builder.AddAttribute(4, "class", "me-3");
        builder.AddContent(5, Foo.Text);
        builder.CloseElement();

        builder.OpenComponent<Button>(0);
        builder.AddAttribute(1, nameof(Button.Icon), "fa-solid fa-font-awesome");
        builder.AddAttribute(2, nameof(Button.Text), "Click");
        builder.AddAttribute(3, nameof(Button.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, e =>
        {
            ToastService.Warning("自定义 TreeViewItem", "测试 TreeViewItem 按钮点击事件");
        }));
        builder.CloseComponent();
    }
}
