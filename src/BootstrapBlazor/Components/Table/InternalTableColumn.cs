// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    internal class InternalTableColumn : ITableColumn
    {
        private string FieldName { get; }

        public bool Sortable { get; set; }

        public bool DefaultSort { get; set; }

        public SortOrder DefaultSortOrder { get; set; }

        public bool Filterable { get; set; }

        public bool Searchable { get; set; }

        public int? Width { get; set; }

        public bool Fixed { get; set; }

        public bool Visible { get; set; } = true;

        public bool TextWrap { get; set; }

        public bool TextEllipsis { get; set; }

        /// <summary>
        /// 获得/设置 是否不进行验证 默认为 false
        /// </summary>
        public bool SkipValidate { get; set; }

        /// <summary>
        /// 获得/设置 新建时此列只读 默认为 false
        /// </summary>
        public bool IsReadonlyWhenAdd { get; set; }

        /// <summary>
        /// 获得/设置 编辑时此列只读 默认为 false
        /// </summary>
        public bool IsReadonlyWhenEdit { get; set; }

        public string? CssClass { get; set; }

        public BreakPoint ShownWithBreakPoint { get; set; }

        public RenderFragment<object>? Template { get; set; }

        public RenderFragment<object>? SearchTemplate { get; set; }

        public RenderFragment? FilterTemplate { get; set; }

        /// <summary>
        /// 获得/设置 表头模板
        /// </summary>
        public RenderFragment<ITableColumn>? HeaderTemplate { get; set; }

        public IFilter? Filter { get; set; }

        public string? FormatString { get; set; }

        /// <summary>
        /// 获得/设置 placeholder 文本 默认为 null
        /// </summary>
        public string? PlaceHolder { get; set; }

        public Func<object?, Task<string>>? Formatter { get; set; }

        public Alignment Align { get; set; }

        public bool ShowTips { get; set; }

        public Type PropertyType { get; }

        public bool Editable { get; set; } = true;

        public bool Readonly { get; set; }

        public object? Step { get; set; }

        public int Rows { get; set; }

        [NotNull]
        public string? Text { get; set; }

        public RenderFragment<object>? EditTemplate { get; set; }

        /// <summary>
        /// 获得/设置 组件类型 默认为 null
        /// </summary>
        public Type? ComponentType { get; set; }

        /// <summary>
        /// 获得/设置 组件自定义类型参数集合 默认为 null
        /// </summary>
        public IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

        /// <summary>
        /// 获得/设置 额外数据源一般用于下拉框或者 CheckboxList 这种需要额外配置数据源组件使用
        /// </summary>
        public IEnumerable<SelectedItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 显示顺序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 获得/设置 字典数据源 常用于外键自动转换为名称操作
        /// </summary>
        public IEnumerable<SelectedItem>? Lookup { get; set; }

        /// <summary>
        /// 获得/设置 单元格回调方法
        /// </summary>
        public Action<TableCellArgs>? OnCellRender { get; set; }

        /// <summary>
        /// 获得/设置 自定义验证集合
        /// </summary>
        public List<IValidator>? ValidateRules { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="fieldText">显示文字</param>
        /// <param name="category">分组</param>
        public InternalTableColumn(string fieldName, Type fieldType, string? fieldText = null, string? category = null)
        {
            FieldName = fieldName;
            PropertyType = fieldType;
            Text = fieldText;
            Category = category;
        }

        public string GetDisplayName() => Text;

        public string GetFieldName() => FieldName;

        /// <summary>
        /// 通过泛型模型获取模型属性集合
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<ITableColumn> GetProperties<TModel>(IEnumerable<ITableColumn>? source = null) => GetProperties(typeof(TModel), source);

        /// <summary>
        /// 通过特定类型模型获取模型属性集合
        /// </summary>
        /// <param name="type"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<ITableColumn> GetProperties(Type type, IEnumerable<ITableColumn>? source = null)
        {
            var cols = new List<ITableColumn>(50);
            //使用 TypeDescriptor 代替 Reflection,
            //使 TypeDescriptor.AddProviderTransparent 添加的分离式特性可以读取出来.
            //因为有些类型是自动生成的(EF), 或第三方库中的类型, 不方便添加特性.
            //针对这部分类型, 可以用 TypeDescriptor.AddProviderTransparent 附加特性.
            var topInfo = TypeDescriptor.GetAttributes(type).OfType<AutoGenerateClassAttribute>().FirstOrDefault()?.GetAutoGenerateInfo();

            var props = TypeDescriptor.GetProperties(type).Cast<PropertyDescriptor>();

            foreach (var prop in props)
            {
                var propInfo = prop.Attributes.OfType<AutoGenerateColumnAttribute>().FirstOrDefault()?.GetAutoGenerateInfo();

                var catAttr = prop.Attributes.OfType<CategoryAttribute>().FirstOrDefault();

                var displayName = propInfo?.Text ?? Utility.GetDisplayName(type, prop.Name);
                var category = propInfo?.Category ?? catAttr?.Category;

                if (propInfo?.Ignore ?? false)
                    continue;

                var tc = new InternalTableColumn(prop.Name, prop.PropertyType, displayName, category);
                var col = source?.FirstOrDefault(c => c.GetFieldName() == tc.GetFieldName());
                //就近合并
                tc.Merge(col, topInfo, propInfo);
                cols.Add(tc);
            }

            return cols.Where(a => a.Order > 0).OrderBy(a => a.Order)
                .Concat(cols.Where(a => a.Order == 0))
                .Concat(cols.Where(a => a.Order < 0).OrderBy(a => a.Order));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="topInfo"></param>
        /// <param name="propInfo"></param>
        private void Merge(ITableColumn? col, AutoGenerateClassInfo? topInfo, AutoGenerateColumnInfo? propInfo)
        {
            //构造函数传入
            //this.Category = propInfo?.Category;
            //this.Text = propInfo?.Text;
            this.Category = col?.Category ?? this.Category;
            this.Text = col?.Text ?? this.Text;

            //就近原则
            this.Align = col?.Align ?? propInfo?.Align ?? topInfo?.Align ?? Alignment.None;
            this.Editable = col?.Editable ?? propInfo?.Editable ?? topInfo?.Editable ?? true;
            this.Filterable = col?.Filterable ?? propInfo?.Filterable ?? topInfo?.Filterable ?? false;
            this.Readonly = col?.Readonly ?? propInfo?.Readonly ?? topInfo?.Readonly ?? false;
            this.Searchable = col?.Searchable ?? propInfo?.Searchable ?? topInfo?.Searchable ?? false;
            this.ShowTips = col?.ShowTips ?? propInfo?.ShowTips ?? topInfo?.ShowTips ?? false;
            this.Sortable = col?.Sortable ?? propInfo?.Sortable ?? topInfo?.Sortable ?? false;
            this.TextEllipsis = col?.TextEllipsis ?? propInfo?.TextEllipsis ?? topInfo?.TextEllipsis ?? false;
            this.TextWrap = col?.TextWrap ?? propInfo?.TextWrap ?? topInfo?.TextWrap ?? false;
            this.Width = col?.Width ?? propInfo?.Width ?? topInfo?.Width;


            this.ComponentType = col?.ComponentType ?? propInfo?.ComponentType;
            this.CssClass = col?.CssClass ?? propInfo?.CssClass;
            this.DefaultSort = col?.DefaultSort ?? propInfo?.DefaultSort ?? false;
            this.DefaultSortOrder = col?.DefaultSortOrder ?? propInfo?.DefaultSortOrder ?? SortOrder.Unset;
            this.Fixed = col?.Fixed ?? propInfo?.Fixed ?? false;
            this.FormatString = col?.FormatString ?? propInfo?.FormatString;
            this.IsReadonlyWhenAdd = col?.IsReadonlyWhenAdd ?? propInfo?.IsReadonlyWhenAdd ?? false;
            this.IsReadonlyWhenEdit = col?.IsReadonlyWhenEdit ?? propInfo?.IsReadonlyWhenEdit ?? false;
            this.Order = col?.Order ?? propInfo?.Order ?? 0;
            this.PlaceHolder = col?.PlaceHolder ?? propInfo?.PlaceHolder;
            this.Rows = col?.Rows ?? propInfo?.Rows ?? 0;
            this.ShownWithBreakPoint = col?.ShownWithBreakPoint ?? propInfo?.ShownWithBreakPoint ?? BreakPoint.None;
            this.SkipValidate = col?.SkipValidate ?? propInfo?.SkipValidate ?? false;
            this.Step = col?.Step ?? propInfo?.Step;
            this.Visible = col?.Visible ?? propInfo?.Visible ?? true;
        }


        ///// <summary>
        ///// 集成 class 标签中设置的参数值
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="dest"></param>
        //private static void InheritValue(AutoGenerateClassAttribute source, ITableColumn dest)
        //{
        //    if (source.Align != Alignment.None) dest.Align = source.Align;
        //    if (source.TextWrap) dest.TextWrap = source.TextWrap;
        //    if (!source.Editable) dest.Editable = source.Editable;
        //    if (source.Filterable) dest.Filterable = source.Filterable;
        //    if (source.Readonly) dest.Readonly = source.Readonly;
        //    if (source.Searchable) dest.Searchable = source.Searchable;
        //    if (source.ShowTips) dest.ShowTips = source.ShowTips;
        //    if (source.Sortable) dest.Sortable = source.Sortable;
        //    if (source.TextEllipsis) dest.TextEllipsis = source.TextEllipsis;
        //    //对于有很多列的表格, 一列一列的设置列宽, 太麻烦
        //    //疑惑:
        //    //AutoGenerateClassAttribute 设计是用来提供默认的, 还是用来覆盖 AutoGenerateColumnAttribute 上的??
        //    if (source.Width > 0 && (dest.Width == null || dest.Width <= 0)) dest.Width = source.Width;
        //}

        ///// <summary>
        ///// 属性赋值方法
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="dest"></param>
        //private static void CopyValue(ITableColumn source, ITableColumn dest)
        //{
        //    if (source.Align != Alignment.None) dest.Align = source.Align;
        //    if (source.TextWrap) dest.TextWrap = source.TextWrap;
        //    if (source.ComponentType != null) dest.ComponentType = source.ComponentType;
        //    if (source.ComponentParameters != null) dest.ComponentParameters = source.ComponentParameters;
        //    if (!string.IsNullOrEmpty(source.CssClass)) dest.CssClass = source.CssClass;
        //    if (source.DefaultSort) dest.DefaultSort = source.DefaultSort;
        //    if (source.DefaultSortOrder != SortOrder.Unset) dest.DefaultSortOrder = source.DefaultSortOrder;
        //    if (!source.Editable) dest.Editable = source.Editable;
        //    if (source.EditTemplate != null) dest.EditTemplate = source.EditTemplate;
        //    if (source.Filter != null) dest.Filter = source.Filter;
        //    if (source.Filterable) dest.Filterable = source.Filterable;
        //    if (source.FilterTemplate != null) dest.FilterTemplate = source.FilterTemplate;
        //    if (source.Fixed) dest.Fixed = source.Fixed;
        //    if (source.FormatString != null) dest.FormatString = source.FormatString;
        //    if (source.Formatter != null) dest.Formatter = source.Formatter;
        //    if (source.HeaderTemplate != null) dest.HeaderTemplate = source.HeaderTemplate;
        //    if (source.Items != null) dest.Items = source.Items;
        //    if (source.Lookup != null) dest.Lookup = source.Lookup;
        //    if (source.IsReadonlyWhenAdd) dest.IsReadonlyWhenAdd = source.IsReadonlyWhenAdd;
        //    if (source.IsReadonlyWhenEdit) dest.IsReadonlyWhenEdit = source.IsReadonlyWhenEdit;
        //    if (source.OnCellRender != null) dest.OnCellRender = source.OnCellRender;
        //    if (source.Readonly) dest.Readonly = source.Readonly;
        //    if (source.Rows > 0) dest.Rows = source.Rows;
        //    if (source.Searchable) dest.Searchable = source.Searchable;
        //    if (source.SearchTemplate != null) dest.SearchTemplate = source.SearchTemplate;
        //    if (source.ShownWithBreakPoint != BreakPoint.None) dest.ShownWithBreakPoint = source.ShownWithBreakPoint;
        //    if (source.ShowTips) dest.ShowTips = source.ShowTips;
        //    if (source.SkipValidate) dest.SkipValidate = source.SkipValidate;
        //    if (source.Sortable) dest.Sortable = source.Sortable;
        //    if (source.Template != null) dest.Template = source.Template;
        //    if (!string.IsNullOrEmpty(source.Text)) dest.Text = source.Text;
        //    if (source.TextEllipsis) dest.TextEllipsis = source.TextEllipsis;
        //    if (!source.Visible) dest.Visible = source.Visible;
        //    if (source.Width != null) dest.Width = source.Width;
        //    if (source.ValidateRules != null) dest.ValidateRules = source.ValidateRules;
        //}
    }
}
