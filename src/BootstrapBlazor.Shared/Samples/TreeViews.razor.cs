// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class TreeViews
{
    [NotNull]
    private BlockLogger? Trace { get; set; }

    [NotNull]
    private BlockLogger? TraceChecked { get; set; }

    [NotNull]
    private BlockLogger? TraceCheckedItems { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    private Foo Model => Foo.Generate(Localizer);

    private List<TreeViewItem<TreeFoo>> Items { get; set; } = TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>> CheckedItems { get; set; } = GetCheckedItems();

    private List<TreeViewItem<TreeFoo>> RadioItems { get; set; } = GetCheckedItems();

    private List<TreeViewItem<TreeFoo>> ExpandItems { get; set; } = GetExpandItems();

    private List<TreeViewItem<TreeFoo>>? AsyncItems { get; set; }

    private bool AutoCheckChildren { get; set; }

    private bool AutoCheckParent { get; set; }

    private List<SelectedItem> ResetItems { get; } = new List<SelectedItem>()
    {
        new("True", "重置"),
        new("False", "保持")
    };

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await OnLoadAsyncItems();
    }

    private void OnRefresh()
    {
        CheckedItems = GetCheckedItems();
    }

    private bool IsReset { get; set; }

    private async Task OnLoadAsyncItems()
    {
        AsyncItems = null;
        await Task.Delay(2000);
        AsyncItems = TreeFoo.GetTreeItems();
        AsyncItems[2].Text = "延时加载";
        AsyncItems[2].HasChildren = true;
    }

    private static List<TreeViewItem<TreeFoo>> GetCheckedItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[1].IsActive = true;
        ret[1].Items[1].CheckedState = CheckboxState.Checked;
        return ret;
    }

    private static List<TreeViewItem<TreeFoo>> GetExpandItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[1].IsExpand = true;
        return ret;
    }

    private List<TreeViewItem<TreeFoo>> DisabledItems { get; set; } = GetDisabledItems();

    private static List<TreeViewItem<TreeFoo>> GetDisabledItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[1].Items[1].IsDisabled = true;
        return ret;
    }

    private static List<TreeViewItem<TreeFoo>> GetAccordionItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[1].Items[0].HasChildren = true;
        return ret;
    }

    private static List<TreeViewItem<TreeFoo>> GetIconItems() => TreeFoo.GetTreeItems();

    private static List<TreeViewItem<TreeFoo>> GetLazyItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[1].Items[0].IsExpand = true;
        ret[2].Text = "懒加载延时";
        ret[2].HasChildren = true;
        return ret;
    }

    private static List<TreeViewItem<TreeFoo>> GetTemplateItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[0].Template = foo => BootstrapDynamicComponent.CreateComponent<CustomerTreeItem>(new Dictionary<string, object?>()
        {
            [nameof(CustomerTreeItem.Foo)] = foo
        }).Render();
        return ret;
    }

    private static List<TreeViewItem<TreeFoo>> GetColorItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[0].CssClass = "text-primary";
        ret[1].CssClass = "text-success";
        ret[2].CssClass = "text-danger";
        return ret;
    }

    private class CustomerTreeItem : ComponentBase
    {
        [Inject]
        [NotNull]
        private ToastService? ToastService { get; set; }

        [Parameter]
        [NotNull]
        public TreeFoo? Foo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(3, "span");
            builder.AddAttribute(4, "class", "me-3");
            builder.AddContent(5, Foo.Text);
            builder.CloseElement();

            builder.OpenComponent<Button>(0);
            builder.AddAttribute(1, nameof(Button.Icon), "fa fa-fa");
            builder.AddAttribute(2, nameof(Button.Text), "Click");
            builder.AddAttribute(3, nameof(Button.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, e =>
            {
                ToastService.Warning("自定义 TreeItem", "测试 TreeItem 按钮点击事件");
            }));
            builder.CloseComponent();
        }
    }

    private Task OnTreeItemClick(TreeViewItem<TreeFoo> item)
    {
        Trace.Log($"TreeItem: {item.Text} clicked");
        return Task.CompletedTask;
    }

    private Task OnTreeItemChecked(TreeViewItem<TreeFoo> item)
    {
        var state = item.CheckedState == CheckboxState.Checked ? "选中" : "未选中";
        TraceChecked.Log($"TreeItem: {item.Text} {state}");
        return Task.CompletedTask;
    }

    private static async Task<IEnumerable<TreeViewItem<TreeFoo>>> OnExpandNodeAsync(TreeViewItem<TreeFoo> node)
    {
        await Task.Delay(800);
        var item = node.Value;
        return new TreeViewItem<TreeFoo>[]
        {
            new TreeViewItem<TreeFoo>(new TreeFoo() { Id = $"{item.Id}-101", ParentId = item.Id })
            {
                Text = "懒加载子节点1",
                HasChildren = true
            },
            new TreeViewItem<TreeFoo>(new TreeFoo(){ Id = $"{item.Id}-102", ParentId = item.Id })
            {
                Text = "懒加载子节点2"
            }
        };
    }

    private Task OnTreeItemChecked(List<TreeViewItem<TreeFoo>> items)
    {
        TraceCheckedItems.Log($"当前共选中{items.Count}项");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Items",
            Description = "菜单数据集合",
            Type = "IEnumerable<TreeItem>",
            ValueList = " — ",
            DefaultValue = "new List<TreeItem>(20)"
        },
        new AttributeItem() {
            Name = "ClickToggleNode",
            Description = "是否点击节点时展开或者收缩子项",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowCheckbox",
            Description = "是否显示 CheckBox",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowIcon",
            Description = "是否显示 Icon",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowSkeleton",
            Description = "是否显示加载骨架屏",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "OnTreeItemClick",
            Description = "树形控件节点点击时回调委托",
            Type = "Func<TreeItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnTreeItemChecked",
            Description = "树形控件节点选中时回调委托",
            Type = "Func<TreeItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnExpandNode",
            Description = "树形控件节点展开回调委托",
            Type = "Func<TreeItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnCheckedItems",
            Description = "树形控件获取所有选中节点回调委托",
            Type = "Func<List<TreeItem>, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };

    private static IEnumerable<AttributeItem> GetTreeItemAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.Items),
            Description = "子节点数据源",
            Type = "List<TreeItem<TItem>>",
            ValueList = " — ",
            DefaultValue = "new ()"
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.Text),
            Description = "显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.Icon),
            Description = "显示图标",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.CssClass),
            Description = "节点自定义样式",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.CheckedState),
            Description = "是否被选中",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.IsDisabled),
            Description = "是否被禁用",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.IsExpand),
            Description = "是否展开",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.HasChildren),
            Description = "是否有子节点",
            Type = "bool",
            ValueList = " true|false ",
            DefaultValue = " false "
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.ShowLoading),
            Description = "是否显示子节点加载动画",
            Type = "bool",
            ValueList = " true|false ",
            DefaultValue = " false "
        },
        new AttributeItem()
        {
            Name = nameof(TreeViewItem<TreeFoo>.Template),
            Description = "子节点模板",
            Type = nameof(RenderFragment),
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
