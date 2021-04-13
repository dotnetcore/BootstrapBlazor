// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        private string? GetCssString(IEditorItem item) => CssBuilder.Default("form-group col-12")
            .AddClass("col-sm-6", item.Data == null && ItemsPerRow == null && item.Rows == 0)
            .AddClass($"col-sm-6 col-md-{Math.Floor(12d / (ItemsPerRow ?? 1))}", item.Data == null && ItemsPerRow != null && item.Rows == 0)
            .Build();

        /// <summary>
        /// 获得/设置 每行显示组件数量 默认为 null
        /// </summary>
        [Parameter]
        public int? ItemsPerRow { get; set; }

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
        /// 获得/设置 级联上下文 EditContext 实例 内置于 EditForm 或者 ValidateForm 时有值
        /// </summary>
        [CascadingParameter]
        private EditContext? CascadedEditContext { get; set; }

        /// <summary>
        /// 获得/设置 级联上下文绑定字段信息集合
        /// </summary>
        [CascadingParameter]
        private IEnumerable<IEditorItem>? CascadeEditorItems { get; set; }

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
        private List<IEditorItem> EditorItems { get; set; } = new List<IEditorItem>();

        /// <summary>
        /// 获得/设置 渲染的编辑项集合
        /// </summary>
        private List<IEditorItem> FormItems { get; set; } = new List<IEditorItem>();

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
                if (CascadedEditContext.Model.GetType() != typeof(TModel))
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

                if (CascadeEditorItems != null)
                {
                    // 通过级联参数渲染组件
                    FormItems.AddRange(CascadeEditorItems);
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
                                }
                            }
                        }
                        FormItems.AddRange(items);
                    }
                    else
                    {
                        FormItems.AddRange(EditorItems);
                    }
                }
                StateHasChanged();
            }
        }

        #region AutoEdit
        private RenderFragment AutoGenerateTemplate(IEditorItem item) => builder =>
        {
            var fieldType = item.PropertyType;
            if (fieldType != null && Model != null)
            {
                var fieldName = item.GetFieldName();
                // GetDisplayName
                // 先取 Text 属性值，然后取资源文件中的值
                var displayName = item.GetDisplayName() ?? Utility.GetDisplayName(Model, fieldName);

                // FieldValue
                var valueInvoker = GetPropertyValueLambdaCache.GetOrAdd((typeof(TModel), fieldName), key => LambdaExtensions.GetPropertyValueLambda<TModel, object?>(Model, key.FieldName).Compile());
                var fieldValue = valueInvoker.Invoke(Model);

                // ValueChanged
                var valueChangedInvoker = CreateLambda(fieldType).Compile();
                var fieldValueChanged = valueChangedInvoker(Model, fieldName);

                // ValueExpression
                var body = Expression.Property(Expression.Constant(Model), typeof(TModel), fieldName);
                var tDelegate = typeof(Func<>).MakeGenericType(fieldType);
                var valueExpression = Expression.Lambda(tDelegate, body);

                if (IsDisplay)
                {
                    builder.OpenComponent(0, typeof(Display<>).MakeGenericType(fieldType));
                    builder.AddAttribute(1, "DisplayText", displayName);
                    builder.AddAttribute(2, "Value", fieldValue);
                    builder.AddAttribute(3, "ValueChanged", fieldValueChanged);
                    builder.AddAttribute(4, "ValueExpression", valueExpression);
                    builder.AddAttribute(5, "ShowLabel", ShowLabel ?? true);
                    builder.CloseComponent();
                }
                else
                {
                    var componentType = item.ComponentType ?? EditorForm<TModel>.GenerateComponent(fieldType, item.Rows != 0);
                    builder.OpenComponent(0, componentType);
                    builder.AddAttribute(1, "DisplayText", displayName);
                    builder.AddAttribute(2, "Value", fieldValue);
                    builder.AddAttribute(3, "ValueChanged", fieldValueChanged);
                    builder.AddAttribute(4, "ValueExpression", valueExpression);
                    builder.AddAttribute(5, "IsDisabled", item.Readonly);
                    if (IsCheckboxList(fieldType) && item.Data != null)
                    {
                        builder.AddAttribute(6, nameof(CheckboxList<IEnumerable<string>>.Items), item.Data);
                    }
                    builder.AddMultipleAttributes(7, CreateMultipleAttributes(fieldType, fieldName, item));
                    builder.CloseComponent();
                }
            }
        };

        private IEnumerable<KeyValuePair<string, object?>> CreateMultipleAttributes(Type fieldType, string fieldName, IEditorItem item)
        {
            var ret = new List<KeyValuePair<string, object?>>();
            var type = Nullable.GetUnderlyingType(fieldType) ?? fieldType;
            if (type.IsEnum)
            {
                // 枚举类型
                // 通过字符串转化为枚举类实例
                var items = type.ToSelectList();
                if (items != null)
                {
                    ret.Add(new KeyValuePair<string, object?>("Items", items));
                }
            }
            else
            {
                switch (type.Name)
                {
                    case nameof(String):
                        ret.Add(new KeyValuePair<string, object?>("placeholder", Utility.GetPlaceHolder(Model, fieldName) ?? PlaceHolderText));
                        if (item.Rows != 0)
                        {
                            ret.Add(new KeyValuePair<string, object?>("rows", item.Rows));
                        }
                        break;
                    case nameof(Int16):
                    case nameof(Int32):
                    case nameof(Int64):
                    case nameof(Single):
                    case nameof(Double):
                    case nameof(Decimal):
                        ret.Add(new KeyValuePair<string, object?>("Step", item.Step!));
                        break;
                    default:
                        break;
                }
            }
            ret.Add(new KeyValuePair<string, object?>("ShowLabel", ShowLabel));
            return ret;
        }

        private static Type GenerateComponent(Type fieldType, bool hasRows)
        {
            Type? ret = null;
            var type = (Nullable.GetUnderlyingType(fieldType) ?? fieldType);
            if (type.IsEnum)
            {
                ret = typeof(Select<>).MakeGenericType(fieldType);
            }
            else if (IsCheckboxList(type))
            {
                ret = typeof(CheckboxList<IEnumerable<string>>);
            }
            else
            {
                switch (type.Name)
                {
                    case nameof(Boolean):
                        ret = typeof(Checkbox<>).MakeGenericType(fieldType);
                        break;
                    case nameof(DateTime):
                        ret = typeof(DateTimePicker<>).MakeGenericType(fieldType);
                        break;
                    case nameof(Int16):
                    case nameof(Int32):
                    case nameof(Int64):
                    case nameof(Single):
                    case nameof(Double):
                    case nameof(Decimal):
                        ret = typeof(BootstrapInputNumber<>).MakeGenericType(fieldType);
                        break;
                    case nameof(String):
                        if (hasRows)
                        {
                            ret = typeof(Textarea);
                        }
                        else
                        {
                            ret = typeof(BootstrapInput<>).MakeGenericType(typeof(string));
                        }
                        break;
                }
            }
            return ret ?? typeof(BootstrapInput<>).MakeGenericType(fieldType);
        }

        private static bool IsCheckboxList(Type fieldType)
        {
            var type = (Nullable.GetUnderlyingType(fieldType) ?? fieldType);
            return type.IsAssignableTo(typeof(IEnumerable<string>));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="model"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private EventCallback<TType> CreateCallback<TType>(TModel model, string fieldName)
        {
            return EventCallback.Factory.Create<TType>(this, t =>
            {
                if (model != null)
                {
                    var invoker = SetPropertyValueLambdaCache.GetOrAdd((typeof(TModel), fieldName), key => LambdaExtensions.SetPropertyValueLambda<TModel, object?>(model, key.FieldName).Compile());
                    invoker.Invoke(model, t);
                }
            });
        }

        private Expression<Func<TModel, string, object>> CreateLambda(Type fieldType)
        {
            var exp_p1 = Expression.Parameter(typeof(TModel));
            var exp_p2 = Expression.Parameter(typeof(string));
            var method = GetType().GetMethod("CreateCallback", BindingFlags.Instance | BindingFlags.NonPublic)!.MakeGenericMethod(fieldType);
            var body = Expression.Call(Expression.Constant(this), method, exp_p1, exp_p2);

            return Expression.Lambda<Func<TModel, string, object>>(Expression.Convert(body, typeof(object)), exp_p1, exp_p2);
        }

        private static ConcurrentDictionary<(Type ModelType, string FieldName), Func<TModel, object?>> GetPropertyValueLambdaCache { get; } = new ConcurrentDictionary<(Type, string), Func<TModel, object?>>();

        private static ConcurrentDictionary<(Type ModelType, string FieldName), Action<TModel, object?>> SetPropertyValueLambdaCache { get; } = new ConcurrentDictionary<(Type, string), Action<TModel, object?>>();
        #endregion
    }
}
