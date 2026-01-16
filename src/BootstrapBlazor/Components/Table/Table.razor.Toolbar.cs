// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

public partial class Table<TItem>
{
    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示工具栏 默认 false 不显示</para>
    ///  <para lang="en">Get/Set Whether to show toolbar. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the 模板 of table toolbar. 默认为 null.</para>
    ///  <para lang="en">Gets or sets the template of table toolbar. Default is null.</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ToolbarTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 首次加载是否显示加载骨架屏 默认 false 不显示 使用 <see cref="ShowLoadingInFirstRender" /> 参数值</para>
    ///  <para lang="en">Get/Set Whether to show skeleton when first loading. Default false. Use <see cref="ShowLoadingInFirstRender" /> parameter value</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowSkeleton { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 首次加载是否显示加载动画 默认 true 显示 设置 <see cref="ShowSkeleton"/> 值覆盖此参数</para>
    ///  <para lang="en">Get/Set Whether to show loading animation when first loading. Default true. Setting <see cref="ShowSkeleton"/> value covers this parameter</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowLoadingInFirstRender { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示按钮列 默认为 true</para>
    ///  <para lang="en">Get/Set Whether to show Button Column. Default true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>本属性设置为 true 新建编辑删除按钮设置为 false 可单独控制每个按钮是否显示</remarks>
    [Parameter]
    public bool ShowDefaultButtons { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示新建按钮 默认为 true 显示</para>
    ///  <para lang="en">Get/Set Whether to show Add Button. Default true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowAddButton { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示编辑按钮 默认为 true 行内是否显示请使用 <see cref="ShowExtendEditButton"/> 与 <see cref="ShowExtendEditButtonCallback" /></para>
    ///  <para lang="en">Get/Set Whether to show Edit Button. Default true. Use <see cref="ShowExtendEditButton"/> and <see cref="ShowExtendEditButtonCallback" /> for in-row display</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowEditButton { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示删除按钮 默认为 true 行内是否显示请使用 <see cref="ShowExtendDeleteButton"/> 与 <see cref="ShowExtendDeleteButtonCallback" /></para>
    ///  <para lang="en">Get/Set Whether to show Delete Button. Default true. Use <see cref="ShowExtendDeleteButton"/> and <see cref="ShowExtendDeleteButtonCallback" /> for in-row display</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowDeleteButton { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示导出按钮 默认为 false 不显示</para>
    ///  <para lang="en">Get/Set Whether to show Export Button. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowExportButton { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示 Excel 导出按钮 默认为 true 显示</para>
    ///  <para lang="en">Get/Set Whether to show Export Excel Button. Default true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowExportExcelButton { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示 Csv 导出按钮 默认为 false 显示</para>
    ///  <para lang="en">Get/Set Whether to show Export Csv Button. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowExportCsvButton { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示 Pdf 导出按钮 默认为 false 显示</para>
    ///  <para lang="en">Get/Set Whether to show Export Pdf Button. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowExportPdfButton { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 导出按钮图标</para>
    ///  <para lang="en">Get/Set Export Button Icon</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ExportButtonIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 内置导出 Csv 按钮图标</para>
    ///  <para lang="en">Get/Set Default Export Csv Button Icon</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CsvExportIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 内置导出 Excel 按钮图标</para>
    ///  <para lang="en">Get/Set Default Export Excel Button Icon</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ExcelExportIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 内置导出 Pdf 按钮图标</para>
    ///  <para lang="en">Get/Set Default Export Pdf Button Icon</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? PdfExportIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 导出数据前是否弹出 Toast 提示框 默认 true</para>
    ///  <para lang="en">Get/Set Whether to show Toast before export. Default true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowToastBeforeExport { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 导出数据后是否弹出 Toast 提示框 默认 true</para>
    ///  <para lang="en">Get/Set Whether to show Toast after export. Default true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowToastAfterExport { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 导出数据前回调方法 默认 null</para>
    ///  <para lang="en">Get/Set Before Export Callback. Default null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? BeforeExportCallback { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 导出数据后回调方法 默认 null</para>
    ///  <para lang="en">Get/Set After Export Callback. Default null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<bool, Task>? AfterExportCallback { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 导出按钮下拉菜单模板 默认 null</para>
    ///  <para lang="en">Get/Set Export Button Dropdown Template. Default null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<ITableExportContext<TItem>>? ExportButtonDropdownTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 内置导出微软 Csv 按钮文本 默认 null 读取资源文件</para>
    ///  <para lang="en">Get/Set Export Microsoft Csv Button Text. Default null (Read from resource file)</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ExportCsvDropdownItemText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 内置导出微软 Excel 按钮文本 默认 null 读取资源文件</para>
    ///  <para lang="en">Get/Set Export Microsoft Excel Button Text. Default null (Read from resource file)</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ExportExcelDropdownItemText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 内置导出 Pdf 按钮文本 默认 null 读取资源文件</para>
    ///  <para lang="en">Get/Set Export Pdf Button Text. Default null (Read from resource file)</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ExportPdfDropdownItemText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示扩展按钮 默认为 false</para>
    ///  <para lang="en">Get/Set Whether to show Extension Button. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowExtendButtons { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否自动收缩工具栏按钮 默认 true</para>
    ///  <para lang="en">Get/Set Whether to auto collapse toolbar buttons. Default true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsAutoCollapsedToolbarButton { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 工具栏按钮收缩后是否继承原先按钮的颜色样式和中空化 默认 false</para>
    ///  <para lang="en">Get/Set Whether to inherit button style when toolbar buttons collapsed. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowColorWhenToolbarButtonsCollapsed { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 工具栏移动端按钮图标</para>
    ///  <para lang="en">Get/Set Toolbar Mobile Button Icon</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? GearIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 扩展按钮是否在前面 默认 false 在行尾</para>
    ///  <para lang="en">Get/Set Whether extension buttons are in front. Default false (At the end)</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsExtendButtonsInRowHeader { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 行内操作列宽度 默认为 130</para>
    ///  <para lang="en">Get/Set Extension Column Width. Default 130</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int ExtendButtonColumnWidth { get; set; } = 130;

    /// <summary>
    ///  <para lang="zh">获得/设置 行内操作列对齐方式 默认 center</para>
    ///  <para lang="en">Get/Set Extension Column Alignment. Default center</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Alignment ExtendButtonColumnAlignment { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示行内扩展编辑按钮 默认 true 显示</para>
    ///  <para lang="en">Get/Set Whether to show inline extension edit button. Default true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowExtendEditButton { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示行内扩展编辑按钮 默认为 null 未设置时使用 <see cref="ShowExtendEditButton"/> 值</para>
    ///  <para lang="en">Get/Set Whether to show inline extension edit button. Default null. use <see cref="ShowExtendEditButton"/> value if not set</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? ShowExtendEditButtonCallback { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否禁用行内扩展编辑按钮 默认 false 不禁用</para>
    ///  <para lang="en">Get/Set Whether to disable inline extension edit button. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool DisableExtendEditButton { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否禁用行内扩展编辑按钮 默认为 null 未设置时使用 <see cref="DisableExtendEditButton"/> 值</para>
    ///  <para lang="en">Get/Set Whether to disable inline extension edit button. Default null. use <see cref="DisableExtendEditButton"/> value if not set</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? DisableExtendEditButtonCallback { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否禁用行内扩展删除按钮 默认 false 不禁用</para>
    ///  <para lang="en">Get/Set Whether to disable inline extension delete button. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool DisableExtendDeleteButton { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否禁用行内扩展删除按钮 默认为 null 未设置时使用 <see cref="DisableExtendDeleteButton"/> 值</para>
    ///  <para lang="en">Get/Set Whether to disable inline extension delete button. Default null. use <see cref="DisableExtendDeleteButton"/> value if not set</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? DisableExtendDeleteButtonCallback { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示行内扩展编辑按钮 默认为 null 未设置时使用 <see cref="ShowExtendEditButton"/> 值</para>
    ///  <para lang="en">Get/Set Whether to show inline extension edit button. Default null. use <see cref="ShowExtendEditButton"/> value if not set</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 ShowExtendEditButtonCallback 参数. Deprecated Use ShowExtendEditButtonCallback instead.")]
    [ExcludeFromCodeCoverage]
    public Func<TItem, bool>? ShowEditButtonCallback
    {
        get => ShowExtendEditButtonCallback;
        set => ShowExtendEditButtonCallback = value;
    }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示行内扩展删除按钮 默认 true 显示</para>
    ///  <para lang="en">Get/Set Whether to show inline extension delete button. Default true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowExtendDeleteButton { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示行内扩展删除按钮 默认为 null 未设置时使用 <see cref="ShowExtendDeleteButton"/> 值</para>
    ///  <para lang="en">Get/Set Whether to show inline extension delete button. Default null. use <see cref="ShowExtendDeleteButton"/> value if not set</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? ShowExtendDeleteButtonCallback { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示行内扩展删除按钮 默认为 null 未设置时使用 <see cref="ShowExtendDeleteButton"/> 值</para>
    ///  <para lang="en">Get/Set Whether to show inline extension delete button. Default null. use <see cref="ShowExtendDeleteButton"/> value if not set</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete(" 已过期，请使用 ShowExtendDeleteButtonCallback 参数. Deprecated Use ShowExtendDeleteButtonCallback instead.")]
    [ExcludeFromCodeCoverage]
    public Func<TItem, bool>? ShowDeleteButtonCallback
    {
        get => ShowExtendDeleteButtonCallback;
        set => ShowExtendDeleteButtonCallback = value;
    }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否固定扩展按钮列 默认为 false 不固定</para>
    ///  <para lang="en">Get/Set Whether to fix Extension Button Column. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool FixedExtendButtonsColumn { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否固定多选列 默认为 false 不固定</para>
    ///  <para lang="en">Get/Set Whether to fix Multiple Select Column. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool FixedMultipleColumn { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否固定明细行 Header 列 默认为 false 不固定</para>
    ///  <para lang="en">Get/Set Whether to fix Detail Row Header Column. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool FixedDetailRowHeaderColumn { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否固定 LineNo 列 默认为 false 不固定</para>
    ///  <para lang="en">Get/Set Whether to fix LineNo Column. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool FixedLineNoColumn { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示刷新按钮 默认为 true</para>
    ///  <para lang="en">Get/Set Whether to show Refresh Button. Default true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowRefresh { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示视图按钮 默认为 false <see cref="IsExcel"/> 模式下此设置无效</para>
    ///  <para lang="en">Get/Set Whether to show Card View Button. Default false. Not effective in <see cref="IsExcel"/> mode</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowCardView { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示列选择下拉框 默认为 false 不显示 点击下拉框内列控制是否显示后触发 <see cref="OnColumnVisibleChanged"/> 回调方法</para>
    ///  <para lang="en">Get/Set Whether to show Column List Dropdown. Default false. Trigger <see cref="OnColumnVisibleChanged"/> when column visible changed</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowColumnList { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 列选择下拉框中是否显示控制功能按钮默认为 false 不显示</para>
    ///  <para lang="en">Get/Set Whether to show control buttons in Column List Dropdown. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowColumnListControls { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 列选择下拉框图标</para>
    ///  <para lang="en">Get/Set Column List Dropdown Icon</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ColumnListButtonIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 保存、删除失败后是否显示 Toast 提示框 默认为 true 显示</para>
    ///  <para lang="en">Get/Set Whether to show Toast when save or delete failed. Default true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowToastAfterSaveOrDeleteModel { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 表格 Toolbar 按钮模板</para>
    ///  <para lang="en">Get/Set Table Toolbar Button Template</para>
    ///  <para lang="zh">表格工具栏左侧按钮模板，模板中内容出现在默认按钮前面</para>
    ///  <para lang="en">Table toolbar left button template, content appears before default buttons</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? TableToolbarBeforeTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 表格 Toolbar 按钮模板</para>
    ///  <para lang="en">Get/Set Table Toolbar Button Template</para>
    ///  <para lang="zh">表格工具栏左侧按钮模板，模板中内容出现在默认按钮后面</para>
    ///  <para lang="en">Table toolbar left button template, content appears after default buttons</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? TableToolbarTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 表格 Toolbar 按钮模板</para>
    ///  <para lang="en">Get/Set Table Toolbar Button Template</para>
    ///  <para lang="zh">表格工具栏右侧按钮模板，模板中内容出现在默认按钮前面</para>
    ///  <para lang="en">Table toolbar right button template, content appears before default buttons</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? TableExtensionToolbarBeforeTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 表格 Toolbar 按钮模板</para>
    ///  <para lang="en">Get/Set Table Toolbar Button Template</para>
    ///  <para lang="zh">表格工具栏右侧按钮模板，模板中内容出现在默认按钮后面</para>
    ///  <para lang="en">Table toolbar right button template, content appears after default buttons</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? TableExtensionToolbarTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 新建按钮回调方法 用于补充模型字段值</para>
    ///  <para lang="en">Get/Set Add Button Callback. Used to populate model field values</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>
    ///  <para lang="zh">有些场景下新建模型有些属性字段默认值需要更改为默认业务值，或者该属性数据库中设置不可为空，新建模型默认值为空时，可通过此回调进行属性值补充更新</para>
    ///  <para lang="en">In some scenarios, the default value of some attribute fields of the new model needs to be changed to the default business value, or the attribute cannot be null in the database. When the default value of the new model is null, this callback can be used to update the attribute value</para>
    /// </remarks>
    [Parameter]
    public Func<Task<TItem>>? OnAddAsync { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 编辑按钮回调方法</para>
    ///  <para lang="en">Get/Set Edit Button Callback</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TItem, Task>? OnEditAsync { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 保存按钮异步回调方法</para>
    ///  <para lang="en">Get/Set Save Button Async Callback</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TItem, ItemChangedType, Task<bool>>? OnSaveAsync { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 删除按钮异步回调方法</para>
    ///  <para lang="en">Get/Set Delete Button Async Callback</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<IEnumerable<TItem>, Task<bool>>? OnDeleteAsync { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 导出按钮异步回调方法</para>
    ///  <para lang="en">Get/Set Export Button Async Callback</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<ITableExportDataContext<TItem>, Task<bool>>? OnExportAsync { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 保存弹窗中的保存按钮显示文本 默认为资源文件中的 保存</para>
    ///  <para lang="en">Get/Set Save Button Text in Edit Dialog. Default "Save" in resource file</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? EditDialogSaveButtonText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 保存弹窗中的保存按钮图标 默认 null 使用当前主题图标</para>
    ///  <para lang="en">Get/Set Save Button Icon in Edit Dialog. Default null (Use current theme icon)</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? EditDialogSaveButtonIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 保存弹窗中的关闭按钮显示文本 默认为资源文件中的 关闭</para>
    ///  <para lang="en">Get/Set Close Button Text in Edit Dialog. Default "Close" in resource file</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? EditDialogCloseButtonText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 保存弹窗中的关闭按钮图标 默认 null 使用当前主题图标</para>
    ///  <para lang="en">Get/Set Close Button Icon in Edit Dialog. Default null (Use current theme icon)</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? EditDialogCloseButtonIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 导出数据弹窗 Title 默认为资源文件 导出数据</para>
    ///  <para lang="en">Get/Set Export Dialog Title. Default "Export Data" in resource file</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ExportToastTitle { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 导出数据提示内容 默认为资源文件</para>
    ///  <para lang="en">Get/Set Export Toast Content. Default in resource file</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ExportToastContent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 正在导出数据提示内容 默认为资源文件</para>
    ///  <para lang="en">Get/Set Export In Progress Toast Content. Default in resource file</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ExportToastInProgressContent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 编辑弹窗配置类扩展回调方法 新建/编辑弹窗弹出前回调此方法用于设置弹窗配置信息</para>
    ///  <para lang="en">Get/Set Edit Dialog Option Callback. Called before showing Add/Edit Dialog to configure options</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Action<ITableEditDialogOption<TItem>>? BeforeShowEditDialogCallback { get; set; }

    /// <summary>
    ///  <para lang="zh">ToastService 服务实例</para>
    ///  <para lang="en">ToastService Instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected ToastService? Toast { get; set; }

    /// <summary>
    ///  <para lang="zh">DialogService 服务实例</para>
    ///  <para lang="en">DialogService Instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected DialogService? DialogService { get; set; }

    /// <summary>
    ///  <para lang="zh">DrawerService 服务实例</para>
    ///  <para lang="en">DrawerService Instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected DrawerService? DrawerService { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 抽屉打开之前回调方法 用于设置 <see cref="DrawerOption"/> 抽屉配置信息</para>
    ///  <para lang="en">Get/Set Before Show Drawer Callback. Used to configure <see cref="DrawerOption"/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<DrawerOption, Task>? OnBeforeShowDrawer { get; set; }

    [Inject]
    [NotNull]
    private ITableExport? TableExport { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 各列是否显示状态集合</para>
    ///  <para lang="en">Get/Set Columns Visibility Status Collection</para>
    /// </summary>
    private readonly List<ColumnVisibleItem> _visibleColumns = [];

    /// <summary>
    ///  <para lang="zh">获得当前可见列集合</para>
    ///  <para lang="en">Get Visible Columns Collection</para>
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ITableColumn> GetVisibleColumns()
    {
        // <para lang="zh">不可见列</para>
        // <para lang="en">Invisible columns</para>
        var items = _visibleColumns.Where(i => i.Visible).Select(a => a.Name).ToHashSet();
        return Columns.Where(i => !i.GetIgnore() && items.Contains(i.GetFieldName()) && ScreenSize >= i.ShownWithBreakPoint);
    }

    private bool GetColumnsListState(ColumnVisibleItem item)
    {
        var items = _visibleColumns.Where(i => i.Visible).Select(a => a.Name).Distinct().ToHashSet();
        return items.Contains(item.Name) && items.Count == 1;
    }

    private bool ShowAddForm { get; set; }

    private bool EditInCell { get; set; }

    private bool AddInCell { get; set; }

    private bool InCellMode => AddInCell || EditInCell;

    /// <summary>
    ///  <para lang="zh">获得 InCell 模式下的 ValidateForm 实例</para>
    ///  <para lang="en">Get ValidateForm Instance in InCell Mode</para>
    /// </summary>
    private ValidateForm _inCellValidateForm = default!;

    /// <summary>
    ///  <para lang="zh">新建按钮方法</para>
    ///  <para lang="en">Add Button Method</para>
    /// </summary>
    public async Task AddAsync()
    {
        if (DynamicContext != null)
        {
            // <para lang="zh">数据源为 DataTable 新建后重建行与列</para>
            // <para lang="en">Data source is DataTable, rebuild rows and columns after adding</para>
            await DynamicContext.AddAsync(SelectedRows.OfType<IDynamicObject>());
            ResetDynamicContext();

            if (!IsKeepSelectedRowAfterAdd)
            {
                SelectedRows.Clear();
                await OnSelectedRowsChanged();
            }
        }
        else if (IsExcel)
        {
            await InternalOnAddAsync();
            await QueryAsync(false);
            await OnSelectedRowsChanged();
        }
        else
        {
            await ToggleLoading(true);
            await InternalOnAddAsync();
            EditModalTitleString = AddModalTitle;
            if (EditMode == EditMode.Popup)
            {
                await ShowEditDialog(ItemChangedType.Add);
            }
            else if (EditMode == EditMode.EditForm)
            {
                ShowAddForm = true;
                ShowEditForm = false;
            }
            else if (EditMode == EditMode.InCell)
            {
                AddInCell = true;
                EditInCell = false;
                SelectedRows.Add(EditModel);
            }
            else if (EditMode == EditMode.Drawer)
            {
                await ShowEditDrawer(ItemChangedType.Add);
            }
            await OnSelectedRowsChanged();
            await ToggleLoading(false);
        }
    }

    private bool ShowEditForm { get; set; }

    /// <summary>
    ///  <para lang="zh">编辑按钮方法</para>
    ///  <para lang="en">Edit Button Method</para>
    /// </summary>
    public async Task EditAsync()
    {
        if (SelectedRows.Count == 1)
        {
            // <para lang="zh">检查是否选中了不可编辑行（行内无编辑按钮），同时检查按钮禁用状态（禁用时不可编辑）</para>
            // <para lang="en">Check if an uneditable row is selected (no inline edit button), and check button disabled state (cannot edit when disabled)</para>
            // <para lang="zh">ShowExtendEditButton 不参与逻辑，不显示扩展编辑按钮时用户可能自定义按钮调用 EditAsync 方法</para>
            // <para lang="en">ShowExtendEditButton does not participate in logic. Users may invoke EditAsync method via custom buttons even when extension edit button is hidden</para>
            if (ProhibitEdit())
            {
                // <para lang="zh">提示不可编辑</para>
                // <para lang="en">Toast uneditable</para>
                await ShowToastAsync(EditButtonToastTitle, EditButtonToastReadonlyContent);
            }
            else
            {
                await ToggleLoading(true);

                // <para lang="zh">跟踪模式与动态类型时使用原始数据，否则使用克隆数据</para>
                // <para lang="en">Use original data in tracking mode and dynamic type, otherwise use clone data</para>
                EditModel = (IsTracking || DynamicContext != null) ? SelectedRows[0] : Utility.Clone(SelectedRows[0]);
                if (OnEditAsync != null)
                {
                    await OnEditAsync(EditModel);
                }
                else
                {
                    var d = DataService ?? InjectDataService;
                    if (d is IEntityFrameworkCoreDataService ef)
                    {
                        await ef.EditAsync(EditModel);
                    }
                }
                EditModalTitleString = EditModalTitle;

                // <para lang="zh">显示编辑框</para>
                // <para lang="en">Show Edit Dialog</para>
                if (EditMode == EditMode.Popup)
                {
                    await ShowEditDialog(ItemChangedType.Update);
                }
                else if (EditMode == EditMode.EditForm)
                {
                    ShowEditForm = true;
                    ShowAddForm = false;
                    StateHasChanged();
                }
                else if (EditMode == EditMode.InCell)
                {
                    AddInCell = false;
                    EditInCell = true;
                    StateHasChanged();
                }
                else if (EditMode == EditMode.Drawer)
                {
                    await ShowEditDrawer(ItemChangedType.Update);
                }
                await ToggleLoading(false);
            }
        }
        else
        {
            // <para lang="zh">不选或者多选弹窗提示</para>
            // <para lang="en">Toast if not selected or multiple selected</para>
            var content = SelectedRows.Count == 0 ? EditButtonToastNotSelectContent : EditButtonToastMoreSelectContent;
            await ShowToastAsync(EditButtonToastTitle, content);
        }
    }

    private async Task ShowToastAsync(string title, string content, ToastCategory category = ToastCategory.Information)
    {
        var option = GetToastOption(title);
        option.Category = category;
        option.Content = content;
        await Toast.Show(option);
    }

    private async Task ShowDeleteToastAsync(string title, string content, ToastCategory category = ToastCategory.Information)
    {
        var option = GetToastOption(title);
        option.Category = category;
        option.Content = string.Format(content, Math.Ceiling(option.Delay / 1000.0));
        await Toast.Show(option);
    }

    /// <summary>
    ///  <para lang="zh">取消保存方法</para>
    ///  <para lang="en">Cancel Save Method</para>
    /// </summary>
    /// <returns></returns>
    protected async Task CancelSave()
    {
        if (EditMode == EditMode.EditForm)
        {
            ShowAddForm = false;
            ShowEditForm = false;
        }
        else if (EditMode == EditMode.InCell)
        {
            SelectedRows.Clear();
            AddInCell = false;
            EditInCell = false;
        }

        if (OnAfterCancelSaveAsync != null)
        {
            await OnAfterCancelSaveAsync();
        }
    }

    /// <summary>
    ///  <para lang="zh">保存数据方法</para>
    ///  <para lang="en">Save Data Method</para>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="changedType"></param>
    /// <returns></returns>
    protected async Task<bool> SaveModelAsync(EditContext context, ItemChangedType changedType)
    {
        bool valid;
        if (DynamicContext != null)
        {
            await DynamicContext.SetValue(context.Model);
            _rowsCache = null;
            valid = true;
        }
        else
        {
            valid = await InternalOnSaveAsync((TItem)context.Model, changedType);
        }

        // <para lang="zh">回调外部自定义方法</para>
        // <para lang="en">Callback external custom method</para>
        if (OnAfterSaveAsync != null)
        {
            await OnAfterSaveAsync((TItem)context.Model);
        }
        if (OnAfterModifyAsync != null)
        {
            await OnAfterModifyAsync();
        }
        if (ShowToastAfterSaveOrDeleteModel)
        {
            var option = GetToastOption(SaveButtonToastTitle);
            option.Category = valid ? ToastCategory.Success : ToastCategory.Error;
            option.Content = string.Format(SaveButtonToastResultContent, valid ? SuccessText : FailText, Math.Ceiling(option.Delay / 1000.0));
            await Toast.Show(option);
        }
        return valid;
    }

    /// <summary>
    ///  <para lang="zh">保存数据</para>
    ///  <para lang="en">Save Data</para>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="changedType"></param>
    protected async Task SaveAsync(EditContext context, ItemChangedType changedType)
    {
        await ToggleLoading(true);
        if (await SaveModelAsync(context, changedType))
        {
            if (EditMode == EditMode.EditForm)
            {
                ShowEditForm = false;
                if (ShowAddForm)
                {
                    ShowAddForm = false;
                    if (IsTracking)
                    {
                        var index = InsertRowMode == InsertRowMode.First ? 0 : Rows.Count;
                        Rows.Insert(index, EditModel);
                        await InvokeItemsChanged();
                    }
                    else
                    {
                        await QueryAsync();
                    }
                }

                // TODO: 如果有双绑时（bind-SelectedRow）其实这里不需要手动更新，多刷新一次
                StateHasChanged();
            }
            else if (EditMode == EditMode.InCell)
            {
                EditInCell = false;
                AddInCell = false;

                // 通过 EditModel 恢复 编辑数据
                if (changedType == ItemChangedType.Add)
                {
                    var index = InsertRowMode == InsertRowMode.First ? 0 : Rows.Count;
                    Rows.Insert(index, EditModel);
                }
                else
                {
                    var index = Rows.IndexOf(SelectedRows[0]);
                    Rows.RemoveAt(index);
                    Rows.Insert(index, EditModel);
                }
                SelectedRows.Clear();
                if (ItemsChanged.HasDelegate)
                {
                    await ItemsChanged.InvokeAsync(Rows);
                }
                else
                {
                    await QueryAsync();
                }
            }
        }
        await ToggleLoading(false);
    }

    /// <summary>
    ///  <para lang="zh">编辑框的大小</para>
    ///  <para lang="en">编辑框的大小</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Size EditDialogSize { get; set; } = Size.ExtraExtraLarge;

    /// <summary>
    ///  <para lang="zh">获得/设置 编辑框是否可以拖拽 默认 false 不可以拖拽，参数 <see cref="EditDialogShowMaximizeButton"/> 值为 false 时此参数才生效</para>
    ///  <para lang="en">Gets or sets 编辑框whether可以拖拽 Default is false 不可以拖拽，参数 <see cref="EditDialogShowMaximizeButton"/> 值为 false 时此参数才生效</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool EditDialogIsDraggable { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 编辑框 FullScreenSize 参数 默认 none</para>
    ///  <para lang="en">Gets or sets 编辑框 FullScreenSize 参数 Default is none</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public FullScreenSize EditDialogFullScreenSize { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 编辑框是否显示最大化按钮 默认 true 显示，此时 <see cref="EditDialogIsDraggable"/> 参数无效</para>
    ///  <para lang="en">Gets or sets 编辑框whetherdisplay最大化button Default is true display，此时 <see cref="EditDialogIsDraggable"/> 参数无效</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool EditDialogShowMaximizeButton { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 未分组编辑项布局位置 默认 false 在尾部</para>
    ///  <para lang="en">Gets or sets 未分组编辑项布局位置 Default is false 在尾部</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowUnsetGroupItemsOnTop { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 弹窗 Footer</para>
    ///  <para lang="en">Gets or sets 弹窗 Footer</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? EditFooterTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 编辑弹窗关闭前回调方法</para>
    ///  <para lang="en">Get/Set Before Close Edit Dialog Callback</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TItem, bool, Task>? EditDialogCloseAsync { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 编辑弹窗 Dialog, 可避免弹窗中 Table 再次弹窗时隐藏原表格问题</para>
    ///  <para lang="en">Get/Set Edit Dialog. To avoid hiding the original table when the table popup again in the popup window</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Dialog? EditDialog { get; set; }

    private async Task AddItem(EditContext context)
    {
        var index = InsertRowMode == InsertRowMode.First ? 0 : Rows.Count;
        Rows.Insert(index, (TItem)context.Model);
        await UpdateRow();
    }

    private async Task EditItem(EditContext context)
    {
        // <para lang="zh">使用 Comparer 确保能找到集合中的编辑项</para>
        // <para lang="en">Use Comparer to ensure the edit item is found in the collection</para>
        // <para lang="zh">解决可能使用 Clone 副本导致编辑数据与 Items 中数据不一致</para>
        // <para lang="en">To solve the problem that the edit data may be inconsistent with the data in Items due to using Clone copy</para>
        var entity = Rows.FirstOrDefault(i => this.Equals<TItem>(i, (TItem)context.Model));
        if (entity != null)
        {
            var index = Rows.IndexOf(entity);
            Rows.RemoveAt(index);
            Rows.Insert(index, (TItem)context.Model);
            await UpdateRow();
        }
    }

    private async Task UpdateRow()
    {
        if (ItemsChanged.HasDelegate)
        {
            await InvokeItemsChanged();
        }
        else
        {
            Items = Rows;
        }
    }

    private void AppendOptions(ITableEditDialogOption<TItem> option, ItemChangedType changedType)
    {
        option.ShowLoading = ShowLoading;
        option.Model = EditModel;
        option.Items = Columns.Where(i => !i.GetIgnore() && !string.IsNullOrEmpty(i.GetFieldName()));
        option.SaveButtonIcon = EditDialogSaveButtonIcon;
        option.SaveButtonText = EditDialogSaveButtonText;
        option.CloseButtonIcon = EditDialogCloseButtonIcon;
        option.CloseButtonText = EditDialogCloseButtonText;
        option.DialogBodyTemplate = EditTemplate;
        option.RowType = EditDialogRowType;
        option.ItemsPerRow = EditDialogItemsPerRow;
        option.LabelAlign = EditDialogLabelAlign;
        option.ItemChangedType = changedType;
        option.ShowUnsetGroupItemsOnTop = ShowUnsetGroupItemsOnTop;
        option.DisableAutoSubmitFormByEnter = DisableAutoSubmitFormByEnter;
        option.IsTracking = IsTracking;
        option.DialogFooterTemplate = EditFooterTemplate;

        BeforeShowEditDialogCallback?.Invoke(option);
    }

    /// <summary>
    ///  <para lang="zh">弹出编辑对话框方法</para>
    ///  <para lang="en">Show Edit Dialog Method</para>
    /// </summary>
    protected async Task ShowEditDialog(ItemChangedType changedType)
    {
        var saved = false;
        var triggerFromSave = false;
        var option = new EditDialogOption<TItem>()
        {
            Class = "modal-dialog-table",
            IsScrolling = ScrollingDialogContent,
            IsKeyboard = IsKeyboard,
            Title = EditModalTitleString,
            Size = EditDialogSize,
            IsDraggable = EditDialogIsDraggable,
            ShowMaximizeButton = EditDialogShowMaximizeButton,
            FullScreenSize = EditDialogFullScreenSize,
            OnCloseAsync = async () =>
            {
                if (triggerFromSave == false && OnAfterCancelSaveAsync != null)
                {
                    await OnAfterCancelSaveAsync();
                }
                await OnCloseEditDialogCallbackAsync(saved);
            },
            OnEditAsync = async context =>
            {
                saved = await OnSaveEditCallbackAsync(context, changedType);
                triggerFromSave = true;
                return saved;
            }
        };
        AppendOptions(option, changedType);
        await DialogService.ShowEditDialog(option, EditDialog);
    }

    /// <summary>
    ///  <para lang="zh">弹出编辑抽屉方法</para>
    ///  <para lang="en">Show Edit Drawer Method</para>
    /// </summary>
    protected async Task ShowEditDrawer(ItemChangedType changedType)
    {
        var saved = false;
        var editOption = new TableEditDrawerOption<TItem>()
        {
            OnCloseAsync = async () =>
            {
                if (OnAfterCancelSaveAsync != null)
                {
                    await OnAfterCancelSaveAsync();
                }
                await OnCloseEditDialogCallbackAsync(saved);
            },
            OnEditAsync = async context =>
            {
                saved = await OnSaveEditCallbackAsync(context, changedType);
                return saved;
            }
        };
        AppendOptions(editOption, changedType);

        var option = new DrawerOption()
        {
            Class = "drawer-table-edit",
            Placement = Placement.Right,
            AllowResize = true,
            IsBackdrop = true,
            Width = "600px"
        };
        if (OnBeforeShowDrawer != null)
        {
            await OnBeforeShowDrawer(option);
        }
        await DrawerService.ShowEditDrawer(editOption, option);
    }

    private async Task OnCloseEditDialogCallbackAsync(bool saved)
    {
        if (EditDialogCloseAsync != null)
        {
            await EditDialogCloseAsync(EditModel, saved);
        }

        if (!saved)
        {
            // <para lang="zh">EFCore 模式保存失败后调用 CancelAsync 回调</para>
            // <para lang="en">Call CancelAsync callback after EFCore mode save failed</para>
            var d = DataService ?? InjectDataService;
            if (d is IEntityFrameworkCoreDataService ef)
            {
                // EFCore
                await ToggleLoading(true);
                await ef.CancelAsync();
                await ToggleLoading(false);
            }
        }
    }

    private async Task<bool> OnSaveEditCallbackAsync(EditContext context, ItemChangedType changedType)
    {
        bool saved;
        await ToggleLoading(true);
        if (IsTracking)
        {
            saved = true;
            if (changedType == ItemChangedType.Add)
            {
                var index = InsertRowMode == InsertRowMode.First ? 0 : Rows.Count;
                Rows.Insert(index, EditModel);
            }
            await InvokeItemsChanged();
        }
        else
        {
            saved = await SaveModelAsync(context, changedType);
            if (saved)
            {
                if (Items != null)
                {
                    if (changedType == ItemChangedType.Add)
                    {
                        await AddItem(context);
                    }
                    else if (changedType == ItemChangedType.Update)
                    {
                        await EditItem(context);
                    }
                }
                else
                {
                    await QueryAsync();
                }
            }
        }
        await ToggleLoading(false);
        return saved;
    }

    /// <summary>
    ///  <para lang="zh">确认删除按钮方法</para>
    ///  <para lang="en">Confirm Delete Button Method</para>
    /// </summary>
    protected async Task<bool> ConfirmDelete()
    {
        var ret = false;
        if (SelectedRows.Count == 0)
        {
            await ShowDeleteToastAsync(DeleteButtonToastTitle, DeleteButtonToastContent);
        }
        else if (ProhibitDelete())
        {
            await ShowDeleteToastAsync(DeleteButtonToastTitle, DeleteButtonToastCanNotDeleteContent);
        }
        else
        {
            ret = true;
        }
        return ret;
    }

    private bool ProhibitEdit() => (ShowExtendEditButtonCallback != null && !ShowExtendEditButtonCallback(SelectedRows[0]))
            || (DisableExtendEditButtonCallback != null && DisableExtendEditButtonCallback(SelectedRows[0]))
            || DisableExtendEditButton;

    private bool ProhibitDelete() => (ShowExtendDeleteButtonCallback != null && SelectedRows.Any(i => !ShowExtendDeleteButtonCallback(i)))
            || (DisableExtendDeleteButtonCallback != null && SelectedRows.Any(x => DisableExtendDeleteButtonCallback(x)))
            || DisableExtendDeleteButton;

    /// <summary>
    ///  <para lang="zh">删除数据方法</para>
    ///  <para lang="en">Delete Data Method</para>
    /// </summary>
    protected async Task DeleteAsync()
    {
        if (DynamicContext != null)
        {
            await DynamicContext.DeleteAsync(SelectedRows.OfType<IDynamicObject>());
            ResetDynamicContext();
            SelectedRows.Clear();
            await OnSelectedRowsChanged();
        }
        else if (IsExcel)
        {
            await InternalOnDeleteAsync();
            await QueryAsync();
        }
        else
        {
            await ToggleLoading(true);
            var ret = await DeleteItemsAsync();

            if (ShowToastAfterSaveOrDeleteModel)
            {
                var option = GetToastOption(DeleteButtonToastTitle);
                option.Category = ret ? ToastCategory.Success : ToastCategory.Error;
                option.Content = string.Format(DeleteButtonToastResultContent, ret ? SuccessText : FailText, Math.Ceiling(option.Delay / 1000.0));
                await Toast.Show(option);
            }
            await ToggleLoading(false);
        }

        async Task<bool> DeleteItemsAsync()
        {
            var ret = await InternalOnDeleteAsync();
            if (ret)
            {
                if (Items != null)
                {
                    SelectedRows.ForEach(i => Rows.Remove(i));
                    if (ItemsChanged.HasDelegate)
                    {
                        await InvokeItemsChanged();
                    }
                }
                else
                {
                    if (IsPagination)
                    {
                        // <para lang="zh">删除成功 重新查询</para>
                        // <para lang="en">Delete success, re-query</para>
                        // <para lang="zh">由于数据删除导致页码会改变，尤其是最后一页</para>
                        // <para lang="en">The page number will change due to data deletion, especially the last page</para>
                        // <para lang="zh">重新计算页码</para>
                        // <para lang="en">Recalculate page number</para>
                        // https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I1UJSL
                        PageIndex = GetSafePageIndex();
                        var items = PageItemsSource.Where(item => item >= (TotalCount - SelectedRows.Count)).ToList();
                        if (items.Count > 0)
                        {
                            _pageItems = Math.Min(_pageItems, items.Min());
                        }
                    }
                }
                if (OnAfterDeleteAsync != null)
                {
                    await OnAfterDeleteAsync(SelectedRows);
                }
                if (OnAfterModifyAsync != null)
                {
                    await OnAfterModifyAsync();
                }
                SelectedRows.Clear();
                await QueryAsync();
            }
            return ret;
        }
    }

    private void ResetDynamicContext()
    {
        if (DynamicContext != null)
        {
            AutoGenerateColumns = false;

            var cols = DynamicContext.GetColumns();
            Columns.Clear();
            Columns.AddRange(cols);

            // <para lang="zh">Columns 重构 清空缓存</para>
            // <para lang="en">Columns Reconstruct, clear cache</para>
            FirstFixedColumnCache.Clear();
            LastFixedColumnCache.Clear();

            InternalResetVisibleColumns(Columns);

            var queryOption = BuildQueryPageOptions();
            // <para lang="zh">设置是否为首次查询</para>
            // <para lang="en">Set whether it is the first query</para>
            queryOption.IsFirstQuery = _firstQuery;

            QueryDynamicItems(queryOption, DynamicContext);

            // <para lang="zh">重新绑定列拖拽</para>
            // <para lang="en">Rebind column resize</para>
            _bindResizeColumn = true;
        }
    }

    private void QueryDynamicItems(QueryPageOptions queryOption, IDynamicObjectContext? context)
    {
        _rowsCache = null;
        if (context != null)
        {
            var items = context.GetItems();
            if (context.OnFilterCallback != null)
            {
                items = context.OnFilterCallback(queryOption, items);
            }
            if (IsPagination)
            {
                TotalCount = items.Count();
                PageCount = (int)Math.Ceiling(TotalCount * 1.0 / Math.Max(1, _pageItems));
                PageIndex = GetSafePageIndex();
                items = items.Skip((PageIndex - 1) * _pageItems).Take(_pageItems);
            }
            QueryItems = items.Cast<TItem>().ToList();

            // <para lang="zh">重置选中行</para>
            // <para lang="en">Reset selected rows</para>
            ResetSelectedRows(QueryItems);
        }
    }

    private int GetSafePageIndex() => Math.Max(1, Math.Min(PageIndex, (int)Math.Ceiling((TotalCount - SelectedRows.Count) * 1.0 / _pageItems)));

    private async Task ExecuteExportAsync(Func<Task<bool>> callback)
    {
        if (BeforeExportCallback != null)
        {
            await BeforeExportCallback();
        }
        else if (ShowToastBeforeExport)
        {
            var option = GetToastOption(ExportToastTitle);
            option.Category = ToastCategory.Information;
            option.Content = string.Format(ExportToastInProgressContent, Math.Ceiling(option.Delay / 1000.0));
            await Toast.Show(option);
        }

        var ret = await callback();

        if (AfterExportCallback != null)
        {
            await AfterExportCallback(ret);
        }
        else if (ShowToastAfterExport)
        {
            var option = GetToastOption(ExportToastTitle);
            option.Category = ret ? ToastCategory.Success : ToastCategory.Error;
            option.Content = string.Format(ExportToastContent, ret ? SuccessText : FailText, Math.Ceiling(option.Delay / 1000.0));
            await Toast.Show(option);
        }
    }

    private ToastOption GetToastOption(string title)
    {
        var option = new ToastOption()
        {
            Title = title,
        };
        if (Options.CurrentValue.ToastDelay > 0)
        {
            option.Delay = Options.CurrentValue.ToastDelay;
        }
        return option;
    }

    private Task ExportAsync() => ExecuteExportAsync(() => OnExportAsync != null
        ? OnExportAsync(new TableExportDataContext<TItem>(TableExportType.Unknown, Rows, GetExportColumns(), BuildQueryPageOptions()))
        : TableExport.ExportAsync(Rows, GetExportColumns()));

    private Task ExportCsvAsync() => ExecuteExportAsync(() => OnExportAsync != null
        ? OnExportAsync(new TableExportDataContext<TItem>(TableExportType.Csv, Rows, GetExportColumns(), BuildQueryPageOptions()))
        : TableExport.ExportCsvAsync(Rows, GetExportColumns()));

    private Task ExportPdfAsync() => ExecuteExportAsync(() => OnExportAsync != null
        ? OnExportAsync(new TableExportDataContext<TItem>(TableExportType.Pdf, Rows, GetExportColumns(), BuildQueryPageOptions()))
        : TableExport.ExportPdfAsync(Rows, GetExportColumns()));

    private Task ExportExcelAsync() => ExecuteExportAsync(() => OnExportAsync != null
        ? OnExportAsync(new TableExportDataContext<TItem>(TableExportType.Excel, Rows, GetExportColumns(), BuildQueryPageOptions()))
        : TableExport.ExportExcelAsync(Rows, GetExportColumns()));

    /// <summary>
    ///  <para lang="zh">获得 the export column 集合.</para>
    ///  <para lang="en">Gets the export column collection.</para>
    /// </summary>
    /// <returns></returns>
    public List<ITableColumn> GetExportColumns() => [.. GetVisibleColumns().Where(i => i.IgnoreWhenExport is not true)];

    /// <summary>
    ///  <para lang="zh">获取当前 Table 选中的所有行数据</para>
    ///  <para lang="en">Get user selected rows</para>
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<TItem> GetSelectedRows() => SelectedRows;

    /// <summary>
    ///  <para lang="zh">是否显示行内编辑按钮</para>
    ///  <para lang="en">Whether to show inline edit button</para>
    /// </summary>
    /// <returns></returns>
    protected bool GetShowExtendEditButton(TItem item) => ShowExtendEditButtonCallback?.Invoke(item) ?? ShowExtendEditButton;

    /// <summary>
    ///  <para lang="zh">是否显示行内删除按钮</para>
    ///  <para lang="en">Whether to show inline delete button</para>
    /// </summary>
    /// <returns></returns>
    protected bool GetShowExtendDeleteButton(TItem item) => ShowExtendDeleteButtonCallback?.Invoke(item) ?? ShowExtendDeleteButton;
}
