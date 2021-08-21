// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Shared.Pages.Table
{
    /// <summary>
    /// 表格示例代码
    /// </summary>
    public partial class Tables
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Tables>? TablesLocalizer { get; set; }

        [NotNull]
        private List<Foo>? Items { get; set; }

        [NotNull]
        private string? RefreshText { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items = Foo.GenerateFoo(Localizer);

            RefreshText ??= TablesLocalizer[nameof(RefreshText)];
        }

        private void OnClick()
        {
            Items = Foo.GenerateFoo(Localizer);
        }

        private static IEnumerable<AttributeItem> GetTableColumnAttributes() => new AttributeItem[]
        {
            new AttributeItem() {
                Name = "Sortable",
                Description = "是否排序",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Filterable",
                Description = "是否可过滤数据",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Editable",
                Description = "是否生成编辑组件",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "Readonly",
                Description = "编辑时是否只读模式",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "AllowTextWrap",
                Description = "是否允许换行",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "TextEllipsis",
                Description = "是否文本超出时省略",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowTips",
                Description = "显示单元格 Tooltips",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Visible",
                Description = "是否显示此列",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "AutoGenerateColumns",
                Description = "是否自动生成列",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Text",
                Description = "表头显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Width",
                Description = "列宽度（像素px）",
                Type = "int",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "CssClass",
                Description = "自定义单元格样式",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "FormatString",
                Description = "数值格式化字符串",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Formatter",
                Description = "格式化回调委托",
                Type = "Func<object?, Task<string>>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Template",
                Description = "模板",
                Type = "RenderFragment<TableColumnContext<object, TItem>>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "EditTemplate",
                Description = "模板",
                Type = "RenderFragment<object>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "SearchTemplate",
                Description = "模板",
                Type = "RenderFragment<object>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };

        private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "TableSize",
                Description = "表格大小",
                Type = "TableSize",
                ValueList = "Normal|Compact",
                DefaultValue = "Normal"
            },
            new AttributeItem() {
                Name = "HeaderStyle",
                Description = "表格 Header 样式",
                Type = "TableHeaderStyle",
                ValueList = "None|Light|Dark",
                DefaultValue = "None"
            },
            new AttributeItem() {
                Name = "Height",
                Description = "固定表头",
                Type = "int",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "PageItems",
                Description = "IsPagination=true 设置每页显示数据数量",
                Type = "int",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "AutoRefreshInterval",
                Description = "自动刷新时间间隔",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "2000"
            },
            new AttributeItem() {
                Name = "ExtendButtonColumnWidth",
                Description = "行操作按钮列宽度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "130"
            },
            new AttributeItem() {
                Name = "RenderModelResponsiveWidth",
                Description = "组件布局模式自动切换阈值",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "768"
            },
            new AttributeItem() {
                Name = "IndentSize",
                Description = "树状数据缩进宽度（像素px）",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "16"
            },
            new AttributeItem() {
                Name = "Items",
                Description = "数据集合",
                Type = "IEnumerable<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "PageItemsSource",
                Description = "IsPagination=true 设置每页显示数据数量的外部数据源",
                Type = "IEnumerable<int>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "EditMode",
                Description = "设置编辑行数据模式",
                Type = "EditMode",
                ValueList = "Popup|Inline|InCell",
                DefaultValue = "Popup"
            },
            new AttributeItem() {
                Name = "MultiHeaderTemplate",
                Description = "表头分组模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "TableFooter",
                Description = "Table Footer 模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "TableToolbarTemplate",
                Description = "自定义按钮模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "EditTemplate",
                Description = "编辑弹窗模板",
                Type = "RenderFragment<TItem?>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "SearchTemplate",
                Description = "高级搜索模板",
                Type = "RenderFragment<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "BeforeRowButtonTemplate",
                Description = "Table 行按钮模板 放置到按钮前",
                Type = "RenderFragment<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "RowButtonTemplate",
                Description = "Table 行按钮模板 默认放置到按钮后",
                Type = "RenderFragment<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "DetailRowTemplate",
                Description = "Table 明细行模板",
                Type = "RenderFragment<TItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsBordered",
                Description = "边框",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsPagination",
                Description = "显示分页",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsStriped",
                Description = "斑马纹",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsRendered",
                Description = "组件是否渲染完毕",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsMultipleSelect",
                Description = "是否为多选模式，为 true 时第一列自动为复选框列",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsAutoRefresh",
                Description = "是否自动刷新表格",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsTree",
                Description = "是否为树形数据",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsDetails",
                Description = "是否为明细行表格，未设置时使用 DetailRowTemplate 进行逻辑判断",
                Type = "boolean",
                ValueList = "true / false / null",
                DefaultValue = "null"
            },
            new AttributeItem() {
                Name = "ClickToSelect",
                Description = "点击行即选中本行",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowCheckboxText",
                Description = "显示文字的选择列",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowFooter",
                Description = "是否显示表脚",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowSearch",
                Description = "显示搜索栏",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowSearchText",
                Description = "显示搜索文本框",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowResetButton",
                Description = "显示清空搜索按钮",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ShowSearchButton",
                Description = "显示搜索按钮",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ShowSearchButton",
                Description = "显示搜索按钮",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "SearchMode",
                Description = "搜索栏渲染方式",
                Type = "SearchMode",
                ValueList = "Popup / Top",
                DefaultValue = "Popup"
            },
            new AttributeItem() {
                Name = "ShowToolbar",
                Description = "显示 Toolbar",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowLineNo",
                Description = "显示 行号",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowDefaultButtons",
                Description = "显示默认按钮 增加编辑删除",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ShowAddButton",
                Description = "显示增加按钮",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ShowEditButton",
                Description = "显示编辑按钮",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ShowDeleteButton",
                Description = "显示删除按钮",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ShowExtendButtons",
                Description = "显示行操作按钮",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowSkeleton",
                Description = "加载时是否显示骨架屏",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowColumnList",
                Description = "是否显示列显示/隐藏控制按钮",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowEmpty",
                Description = "是否显示无数据提示",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "UseComponentWidth",
                Description = "组件渲染模式是否使用组件宽度来判断",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ScrollingDialogContent",
                Description = "编辑弹窗框是否为内部出现滚动条",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "FixedExtendButtonsColumn",
                Description = "是否固定扩展按钮列",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "OnQueryAsync",
                Description = "异步查询回调方法",
                Type = "Func<QueryPageOptions, Task<QueryData<TItem>>>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnAddAsync",
                Description = "新建按钮回调方法",
                Type = "Func<Task<TItem>>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnEditAsync",
                Description = "编辑按钮异步回调方法",
                Type = "Func<TItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnSaveAsync",
                Description = "保存按钮异步回调方法",
                Type = "Func<TItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnDeleteAsync",
                Description = "删除按钮异步回调方法",
                Type = "Func<IEnumerable<TItem>, Task<bool>>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnResetSearchAsync",
                Description = "重置搜索按钮异步回调方法",
                Type = "Func<TItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnSortAsync",
                Description = "排序方法",
                Type = "Func<string, SortOrder, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnClickRowCallback",
                Description = "点击行回调委托方法",
                Type = "Func<TItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnAfterSaveAsync",
                Description = "保存数据后异步回调方法",
                Type = "Func<TItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnTreeExpand",
                Description = "树形数据节点展开式回调委托方法",
                Type = "Func<TItem, Task<IEnumerable<TItem>>>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnDoubleClickRowCallback",
                Description = "双击行回调委托方法",
                Type = "Func<TItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "SortIcon",
                Description = "排序默认图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-sort"
            },
            new AttributeItem() {
                Name = "SortIconAsc",
                Description = "排序升序图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-sort-asc"
            },
            new AttributeItem() {
                Name = "SortIconDesc",
                Description = "排序降序图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-sort-desc"
            },
            new AttributeItem() {
                Name = "EditDialogSaveButtonText",
                Description = "编辑弹窗中保存按钮文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "AddModalTitle",
                Description = "新建数据弹窗 Title",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "EditModalTitle",
                Description = "编辑数据弹窗 Title",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "UnsetText",
                Description = "未设置排序时 tooltip 显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "点击升序"
            },
            new AttributeItem() {
                Name = "SortAscText",
                Description = "升序排序时 tooltip 显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "点击降序"
            },
            new AttributeItem() {
                Name = "SortDescText",
                Description = "降序排序时 tooltip 显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "取消排序"
            },
            new AttributeItem() {
                Name = "EmptyText",
                Description = "无数据时显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "RenderMode",
                Description = "Table 组件布局模式设置",
                Type = "TableRenderMode",
                ValueList = "Auto|Table|CardView",
                DefaultValue = "Auto"
            },
            new AttributeItem() {
                Name = "EmptyTemplate",
                Description = "无数据时显示模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "EditDialogItemsPerRow",
                Description = "每行显示组件数量",
                Type = "int?",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "EditDialogRowType",
                Description = "设置组件布局方式",
                Type = "RowType",
                ValueList = "Row|Inline",
                DefaultValue = "Row"
            },
            new AttributeItem() {
                Name = "EditDialogLabelAlign",
                Description = "Inline 布局模式下标签对齐方式",
                Type = "Alignment",
                ValueList = "None|Left|Center|Right",
                DefaultValue = "None"
            }

        };

        private static IEnumerable<MethodItem> GetMethods() => new MethodItem[]
        {
            new MethodItem()
            {
                Name = "AddAsync",
                Description = "手工添加数据方法",
                Parameters = " - ",
                ReturnValue = "Task"
            },
            new MethodItem()
            {
                Name = "Edit",
                Description = "手工编辑数据方法",
                Parameters = " - ",
                ReturnValue = " - "
            },
            new MethodItem()
            {
                Name = "QueryAsync",
                Description = "手工查询数据方法",
                Parameters = " - ",
                ReturnValue = "Task"
            },
        };
    }
}
