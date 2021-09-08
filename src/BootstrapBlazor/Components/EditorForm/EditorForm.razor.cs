// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 编辑表单基类
    /// </summary>
    public sealed partial class EditorForm<TModel> : IShowLabel
    {
        /// <summary>
        /// 支持每行多少个控件功能
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string? GetCssString(IEditorItem item) => CssBuilder.Default("col-12")
            .AddClass($"col-sm-6 col-md-{Math.Floor(12d / (ItemsPerRow ?? 1))}", item.Data == null && ItemsPerRow != null && item.Rows == 0)
            .Build();

        private string? FormClassString => CssBuilder.Default("row g-3")
            .AddClass("form-inline", RowType == RowType.Inline)
            .AddClass($"is-{LabelAlign.ToDescriptionString()}", RowType == RowType.Inline && LabelAlign != Alignment.None)
            .Build();

        /// <summary>
        /// 获得/设置 每行显示组件数量 默认为 null
        /// </summary>
        [Parameter]
        public int? ItemsPerRow { get; set; }

        /// <summary>
        /// 获得/设置 设置行格式 默认 Row 布局
        /// </summary>
        [Parameter]
        public RowType RowType { get; set; }

        /// <summary>
        /// 获得/设置 设置 <see cref="RowType" /> Inline 模式下标签对齐方式 默认 None 等效于 Left 左对齐
        /// </summary>
        [Parameter]
        public Alignment LabelAlign { get; set; }

        /// <summary>
        /// 获得/设置 列模板
        /// </summary>
        [Parameter]
        public RenderFragment<TModel>? FieldItems { get; set; }

        /// <summary>
        /// 获得/设置 按钮模板
        /// </summary>
        [Parameter]
        public RenderFragment? Buttons { get; set; }

        /// <summary>
        /// 获得/设置 绑定模型
        /// </summary>
        [Parameter]
        [NotNull]
        public TModel? Model { get; set; }

        /// <summary>
        /// 获得/设置 是否显示前置标签 默认为 null 未设置时默认显示标签
        /// </summary>
        [Parameter]
        public bool? ShowLabel { get; set; }

        /// <summary>
        /// 获得/设置 是否显示为 Display 组件 默认为 false
        /// </summary>
        [Parameter]
        public bool IsDisplay { get; set; }

        /// <summary>
        /// 获得/设置 是否自动生成模型的所有属性 默认为 true 生成所有属性
        /// </summary>
        [Parameter]
        public bool AutoGenerateAllItem { get; set; } = true;

        /// <summary>
        /// 获得/设置 级联上下文绑定字段信息集合
        /// </summary>
        [Parameter]
        public IEnumerable<IEditorItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 级联上下文 EditContext 实例 内置于 EditForm 或者 ValidateForm 时有值
        /// </summary>
        [CascadingParameter]
        private EditContext? CascadedEditContext { get; set; }

        /// <summary>
        /// 获得 ValidateForm 实例
        /// </summary>
        [CascadingParameter]
        private ValidateForm? ValidateForm { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<EditorForm<TModel>>? Localizer { get; set; }

        /// <summary>
        /// 获得/设置 配置编辑项目集合
        /// </summary>
        private List<IEditorItem> EditorItems { get; } = new List<IEditorItem>();

        /// <summary>
        /// 获得/设置 渲染的编辑项集合
        /// </summary>
        private List<IEditorItem> FormItems { get; } = new List<IEditorItem>();

        [NotNull]
        private string? PlaceHolderText { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (CascadedEditContext != null)
            {
                var message = Localizer["ModelInvalidOperationExceptionMessage", nameof(EditorForm<TModel>)];
                if (!CascadedEditContext.Model.GetType().IsAssignableTo(typeof(TModel)))
                {
                    throw new InvalidOperationException(message);
                }

                Model = (TModel)CascadedEditContext.Model;
            }

            if (Model == null)
            {
                throw new ArgumentNullException(nameof(Model));
            }

            PlaceHolderText ??= Localizer[nameof(PlaceHolderText)];
        }

        /// <summary>
        /// OnParametersSet 方法
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            // 为空时使用级联参数 ValidateForm 的 ShowLabel
            ShowLabel ??= ValidateForm?.ShowLabel;
        }

        /// <summary>
        /// 
        /// </summary>
        private bool FirstRender { get; set; } = true;

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                FirstRender = false;

                if (Items != null)
                {
                    // 通过级联参数渲染组件
                    FormItems.AddRange(Items);
                }
                else
                {
                    // 如果 EditorItems 有值表示 用户自定义列
                    if (AutoGenerateAllItem)
                    {
                        // 获取绑定模型所有属性
                        var items = InternalTableColumn.GetProperties<TModel>().ToList();

                        // 通过设定的 FieldItems 模板获取项进行渲染
                        foreach (var el in EditorItems)
                        {
                            var item = items.FirstOrDefault(i => i.GetFieldName() == el.GetFieldName());
                            if (item != null)
                            {
                                // 过滤掉不编辑的列
                                if (!el.Editable)
                                {
                                    items.Remove(item);
                                }
                                else
                                {
                                    // 设置只读属性与列模板
                                    item.Readonly = el.Readonly;
                                    item.EditTemplate = el.EditTemplate;
                                    item.Text = el.Text;
                                    item.Data = el.Data;
                                    item.Lookup = el.Lookup;
                                    item.ComponentType = el.ComponentType;
                                    item.ComponentParameters = el.ComponentParameters;
                                    item.SkipValidate = el.SkipValidate;
                                }
                            }
                        }
                        FormItems.AddRange(items);
                    }
                    else
                    {
                        FormItems.AddRange(EditorItems.Where(i => i.Editable));
                    }
                }
                StateHasChanged();
            }
        }

        private RenderFragment AutoGenerateTemplate(IEditorItem item) => builder =>
        {
            if (IsDisplay || item.Readonly)
            {
                builder.CreateDisplayByFieldType(this, item, Model, ShowLabel);
            }
            else
            {
                builder.CreateComponentByFieldType(this, item, Model, ShowLabel, PlaceHolderText);
            }
        };
    }
}
