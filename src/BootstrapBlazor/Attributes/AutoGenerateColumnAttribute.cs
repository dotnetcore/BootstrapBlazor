// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AutoGenerateColumnAttribute : AutoGenerateBaseAttribute/*, ITableColumn*/
    {

        private int? _order;
        /// <summary>
        /// 获得/设置 显示顺序 ，规则如下：
        /// <para></para>
        /// &gt;0时排前面，1,2,3...
        /// <para></para>
        /// =0时排中间(默认)
        /// <para></para>
        /// &lt;0时排后面，...-3,-2,-1
        /// </summary>
        public int Order
        {
            get => default;
            set => this._order = value;
        }


        private bool? _ignore;
        /// <summary>
        /// 获得/设置 是否忽略 默认为 false 不忽略
        /// </summary>
        public bool Ignore
        {
            get => default;
            set => this._ignore = value;
        }


        private bool? _defaultSort;
        /// <summary>
        /// 获得/设置 是否为默认排序列 默认为 false
        /// </summary>
        public bool DefaultSort
        {
            get => default;
            set => this._defaultSort = value;
        }


        private bool? _skipValidate;
        /// <summary>
        /// 获得/设置 是否不进行验证 默认为 false
        /// </summary>
        public bool SkipValidate
        {
            get => default;
            set => this._skipValidate = value;
        }


        private bool? _isREadonlyWhenAdd;
        /// <summary>
        /// 获得/设置 新建时此列只读 默认为 false
        /// </summary>
        public bool IsReadonlyWhenAdd
        {
            get => default;
            set => this._isREadonlyWhenAdd = value;
        }


        private bool? _isReadonlyWhenEdit;
        /// <summary>
        /// 获得/设置 编辑时此列只读 默认为 false
        /// </summary>
        public bool IsReadonlyWhenEdit
        {
            get => default;
            set => this._isReadonlyWhenEdit = value;
        }



        private SortOrder? _defaultSortOrder;
        /// <summary>
        /// 获得/设置 是否为默认排序规则 默认为 SortOrder.Unset
        /// </summary>
        public SortOrder DefaultSortOrder
        {
            get => default;
            set => this._defaultSortOrder = value;
        }

        private bool? _fixed;
        /// <summary>
        /// 获得/设置 是否固定本列 默认 false 不固定
        /// </summary>
        public bool Fixed
        {
            get => default;
            set => this._fixed = value;
        }


        private bool? _visible;
        /// <summary>
        /// 获得/设置 列是否显示 默认为 true 可见的
        /// </summary>
        public bool Visible
        {
            get => default;
            set => this._visible = value;
        }


        private string? _cssClass;
        /// <summary>
        /// 获得/设置 列 td 自定义样式 默认为 null 未设置
        /// </summary>
        public string? CssClass
        {
            get => default;
            set => this._cssClass = value;
        }


        private BreakPoint? _showWithBreakPoint;
        /// <summary>
        /// 获得/设置 显示节点阈值 默认值 BreakPoint.None 未设置
        /// </summary>
        public BreakPoint ShownWithBreakPoint
        {
            get => default;
            set => this._showWithBreakPoint = value;
        }

        private string? _formatString;
        /// <summary>
        /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
        /// </summary>
        public string? FormatString
        {
            get => default;
            set => this._formatString = value;
        }


        private string? _placeHolder;
        /// <summary>
        /// 获得/设置 placeholder 文本 默认为 null
        /// </summary>
        public string? PlaceHolder
        {
            get => default;
            set => this._placeHolder = value;
        }

        private Type? _componentType;
        /// <summary>
        /// 获得/设置 组件类型 默认为 null
        /// </summary>
        public Type? ComponentType
        {
            get => default;
            set => this._componentType = value;
        }

        private object? _step;
        /// <summary>
        /// 获得/设置 步长 默认为 1
        /// </summary>
        public object? Step
        {
            get => default;
            set => this._step = value;
        }

        private int? _rows;
        /// <summary>
        /// 获得/设置 Textarea 行数
        /// </summary>
        public int Rows
        {
            get => default;
            set => this._rows = value;
        }

        private string? _text;
        /// <summary>
        /// 获得/设置 当前属性显示文字 列头或者标签名称
        /// </summary>
        public string? Text
        {
            get => default;
            set => this._text = value;
        }


        private string? _fieldName;
        /// <summary>
        /// 
        /// </summary>
        //[NotNull]
        internal string? FieldName
        {
            get => default;
            set => this._fieldName = value;
        }


        private string? _category;
        /// <summary>
        /// 分组
        /// </summary>
        public string? Category
        {
            get => default;
            set => this._category = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new AutoGenerateColumnInfo GetAutoGenerateInfo()
        {
            var info = base.GetAutoGenerateInfo();

            return new AutoGenerateColumnInfo()
            {
                Align = info.Align,
                Editable = info.Editable,
                Filterable = info.Filterable,
                Readonly = info.Readonly,
                Searchable = info.Searchable,
                ShowTips = info.ShowTips,
                Sortable = info.Sortable,
                TextEllipsis = info.TextEllipsis,
                TextWrap = info.TextWrap,
                Width = info.Width,

                Category = this._category,
                ComponentType = this._componentType,
                CssClass = this._cssClass,
                DefaultSort = this._defaultSort,
                DefaultSortOrder = this._defaultSortOrder,
                FieldName = this._fieldName,

                Fixed = this._fixed,
                FormatString = this._formatString,
                Ignore = this._ignore,
                IsReadonlyWhenAdd = this._isREadonlyWhenAdd,
                IsReadonlyWhenEdit = this._isReadonlyWhenEdit,
                Order = this._order,
                PlaceHolder = this._placeHolder,

                Rows = this._rows,
                ShownWithBreakPoint = this._showWithBreakPoint,
                SkipValidate = this._skipValidate,
                Step = this._step,
                Text = this._text,
                Visible = this._visible,
            };
        }
    }
}
