// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 由于 Attribute 的属性不支持可空类型, 且必须是可读可写的,
    /// 假设类上的特性, 某个属性是 True, 属性上的特性没有设置该属性, 默认是 False,
    /// 这样, 就无法判断用户的本意, 到底是设置为 True 还是 False, 除非使用可空类型.
    /// 这里的 get 都是返回的 default, 因为特性要求属性必须是可读的, 但是它的值又无实际用处.
    /// </remarks>
    public abstract class AutoGenerateBaseAttribute : Attribute
    {

        private bool? _editable;
        /// <summary>
        /// 获得/设置 当前列是否可编辑 默认为 true 当设置为 false 时自动生成编辑 UI 不生成此列
        /// </summary>
        public bool Editable
        {
            get => default;
            set => this._editable = value;
        }


        private bool? _readonly;
        /// <summary>
        /// 获得/设置 当前列编辑时是否只读 默认为 false
        /// </summary>
        public bool Readonly
        {
            get => default;
            set => this._readonly = value;
        }


        private bool? _sortable;
        /// <summary>
        /// 获得/设置 是否允许排序 默认为 false
        /// </summary>
        public bool Sortable
        {
            get => default;
            set => this._sortable = value;
        }

        private bool? _filterable;
        /// <summary>
        /// 获得/设置 是否允许过滤数据 默认为 false
        /// </summary>
        public bool Filterable
        {
            get => default;
            set => this._filterable = value;
        }


        private bool? _searchable;
        /// <summary>
        /// 获得/设置 是否参与搜索 默认为 false
        /// </summary>
        public bool Searchable
        {
            get => default;
            set => this._searchable = value;
        }


        private bool? _textWrap;
        /// <summary>
        /// 获得/设置 本列是否允许换行 默认为 false
        /// </summary>
        public bool TextWrap
        {
            get => default;
            set => this._textWrap = value;
        }


        private bool? _textEllipsis;
        /// <summary>
        /// 获得/设置 本列文本超出省略 默认为 false
        /// </summary>
        public bool TextEllipsis
        {
            get => default;
            set => this._textEllipsis = value;
        }


        private Alignment? _align;
        /// <summary>
        /// 获得/设置 文字对齐方式 默认为 Alignment.None
        /// </summary>
        public Alignment Align
        {
            get => default;
            set => this._align = value;
        }


        private bool? _showTips;
        /// <summary>
        /// 获得/设置 字段鼠标悬停提示
        /// </summary>
        public bool ShowTips
        {
            get => default;
            set => this._showTips = value;
        }


        private int? _width;
        /// <summary>
        /// 获得/设置 列宽
        /// </summary>
        public int Width
        {
            get => default;
            set => this._width = value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AutoGenerateClassInfo GetAutoGenerateInfo()
        {
            return new AutoGenerateClassInfo()
            {
                Align = this._align,
                Editable = this._editable,
                Filterable = this._filterable,
                Readonly = this._readonly,
                Searchable = this._searchable,
                ShowTips = this._showTips,
                Sortable = this._sortable,
                TextEllipsis = this._textEllipsis,
                TextWrap = this._textWrap,
                Width = this._width
            };
        }
    }
}
