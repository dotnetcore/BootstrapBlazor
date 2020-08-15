using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class TableEditorContent<TModel> : ComponentBase where TModel : class, new()
    {
        /// <summary>
        /// 获得/设置 组件相关联的实体类
        /// </summary>
        [Parameter]
        public TModel? Model { get; set; }

        /// <summary>
        /// 获得/设置 是否显示前置文字 默认 false 
        /// </summary>
        [Parameter]
        public bool IsSearch { get; set; }

        /// <summary>
        /// 获得/设置 Table Header 实例
        /// </summary>
        [CascadingParameter]
        protected TableColumnCollection? Collections { get; set; }

        /// <summary>
        /// 渲染组件方法
        /// </summary>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            // 渲染正常按钮
            if (Collections != null)
            {
                var index = 0;
                foreach (var mem in Collections.Columns.Where(col => IsSearch ? col.Searchable : col.Editable))
                {
                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", "form-group col-12 col-sm-6");

                    RenderFragment? content = null;
                    if (IsSearch) content = mem.SearchTemplate?.Invoke(Model);
                    if (content == null) content = mem.EditTemplate?.Invoke(Model) ?? AutoGenerateTemplate(mem);
                    if (content != null) builder.AddContent(index++, content);

                    builder.CloseElement();
                }
            }
        }

        private RenderFragment AutoGenerateTemplate(ITableColumn col) => builder =>
        {
            var fieldType = col.FieldType;
            if (fieldType != null)
            {
                // GetDisplayName
                var displayName = col.GetDisplayName();
                var fieldName = col.GetFieldName();

                // FieldValue
                object? fieldValue = "";
                if (Model != null)
                {
                    var valueInvoker = GetPropertyValueLambdaCache.GetOrAdd((typeof(TModel), fieldName), key => Model.GetPropertyValueLambda<TModel, object?>(key.FieldName).Compile());
                    fieldValue = valueInvoker.Invoke(Model);
                }

                // ValueChanged
                var valueChangedInvoker = EventCallbackCache.GetOrAdd((typeof(TModel), fieldName), key => (CreateLambda(fieldType).Compile()));
                var fieldValueChanged = valueChangedInvoker(fieldName);

                // ValueExpression
                var p = Expression.Property(Expression.Constant(Model), typeof(TModel).GetProperty(fieldName));
                var tDelegate = typeof(Func<>).MakeGenericType(fieldType);
                var valueExpression = Expression.Lambda(tDelegate, p);

                var index = 0;
                var componentType = GenerateComponent(fieldType);
                builder.OpenComponent(index++, componentType);
                builder.AddAttribute(index++, "DisplayText", displayName);
                builder.AddAttribute(index++, "Value", fieldValue);
                builder.AddAttribute(index++, "ValueChanged", fieldValueChanged);
                builder.AddAttribute(index++, "ValueExpression", valueExpression);
                builder.AddMultipleAttributes(index++, CreateMultipleAttributes(fieldType));
                builder.CloseComponent();
            }
        };

        private IEnumerable<KeyValuePair<string, object>> CreateMultipleAttributes(Type fieldType)
        {
            var ret = new List<KeyValuePair<string, object>>();
            var type = Nullable.GetUnderlyingType(fieldType) ?? fieldType;
            if (type.IsEnum)
            {
                // 枚举类型
                // 通过字符串转化为枚举类实例
                var items = type.ToSelectList();
                if (items != null) ret.Add(new KeyValuePair<string, object>("Items", items));
            }
            else
            {
                switch (type.Name)
                {
                    case nameof(String):
                        ret.Add(new KeyValuePair<string, object>("placeholder", "请输入 ..."));
                        break;
                    default:
                        break;
                }
            }

            if (IsSearch)
            {
                ret.Add(new KeyValuePair<string, object>("ShowLabel", true));
            }
            return ret;
        }

        private Type GenerateComponent(Type fieldType)
        {
            Type? ret = null;
            var type = (Nullable.GetUnderlyingType(fieldType) ?? fieldType);
            if (type.IsEnum)
            {
                ret = typeof(Select<>).MakeGenericType(fieldType);
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
                    case nameof(Int32):
                    case nameof(Double):
                    case nameof(Decimal):
                        ret = typeof(BootstrapInput<>).MakeGenericType(fieldType);
                        break;
                    case nameof(String):
                        ret = typeof(BootstrapInput<>).MakeGenericType(typeof(string));
                        break;
                }
            }
            return ret ?? typeof(BootstrapInput<>).MakeGenericType(typeof(string));
        }

        private EventCallback<TType> CreateCallback<TType>(string fieldName)
        {
            return EventCallback.Factory.Create<TType>(this, t =>
            {
                if (Model != null)
                {
                    var invoker = SetPropertyValueLambdaCache.GetOrAdd((typeof(TModel), fieldName), key => Model.SetPropertyValueLambda<TModel, object?>(key.FieldName).Compile());
                    invoker.Invoke(Model, t);
                }
            });
        }

        private Expression<Func<string, object>> CreateLambda(Type fieldType)
        {
            var exp_p1 = Expression.Parameter(typeof(string));
            var method = GetType().GetMethod("CreateCallback", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(fieldType);
            var body = Expression.Call(Expression.Constant(this), method, exp_p1);

            return Expression.Lambda<Func<string, object>>(Expression.Convert(body, typeof(object)), exp_p1);
        }

        private static ConcurrentDictionary<(Type ModelType, string FieldName), Func<TModel, object?>> GetPropertyValueLambdaCache { get; } = new ConcurrentDictionary<(Type, string), Func<TModel, object?>>();

        private static ConcurrentDictionary<(Type ModelType, string FieldName), Action<TModel, object?>> SetPropertyValueLambdaCache { get; } = new ConcurrentDictionary<(Type, string), Action<TModel, object?>>();

        private static ConcurrentDictionary<(Type ModelType, string FieldName), Func<string, object>> EventCallbackCache { get; } = new ConcurrentDictionary<(Type, string), Func<string, object>>();
    }
}
