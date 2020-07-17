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
    public class DataEditorContent<TModel> : ComponentBase where TModel : class, new()
    {
        /// <summary>
        /// 获得/设置 组件相关联的实体类
        /// </summary>
        [Parameter]
        public TModel? Model { get; set; }

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
                foreach (var mem in Collections.Columns)
                {
                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", "form-group col-12 col-sm-6");
                    builder.AddContent(index++, AutoGenerateTemplate(mem));
                    builder.CloseElement();
                }
            }
        }

        private RenderFragment AutoGenerateTemplate(ITableColumn col) => builder =>
        {
            var type = col.FieldType;
            if (type != null)
            {
                var index = 0;
                var componentType = GenerateComponent(type);
                builder.OpenComponent(index++, componentType);
                builder.AddAttribute(index++, "DisplayText", col.GetDisplayName());
                builder.AddAttribute(index++, "Value", Model?.GetPropertyValue<TModel, object>(col.GetFieldName()));
                builder.AddAttribute(index++, "ValueExpression", CreateValueExpression(type, col.GetFieldName()));
                if (Model != null) builder.AddAttribute(index++, "ValueChanged", Create(col, type));
                builder.AddMultipleAttributes(index++, CreateMultipleAttributes(col, type));
                builder.CloseComponent();
            }
        };

        private IEnumerable<KeyValuePair<string, object>> CreateMultipleAttributes(ITableColumn col, Type t)
        {
            var ret = new List<KeyValuePair<string, object>>();
            var type = Nullable.GetUnderlyingType(t) ?? t;
            switch (type.Name)
            {
                case nameof(Byte):
                    // 枚举类型
                    // 通过字符串转化为枚举类实例
                    // var items = Model?.EnumToSelectedItems(mem.GetFieldName(), Nullable.GetUnderlyingType(t) != null);
                    //if (items != null) ret.Add(new KeyValuePair<string, object>("Items", items));
                    break;
                case nameof(String):
                    ret.Add(new KeyValuePair<string, object>("placeholder", "请输入 ..."));
                    break;
                default:
                    break;
            }
            return ret;
        }

        private Type GenerateComponent(Type t)
        {
            Type? ret = null;
            var typeName = (Nullable.GetUnderlyingType(t) ?? t).Name;
            switch (typeName)
            {
                case nameof(Byte):
                    ret = typeof(Select<>).MakeGenericType(t);
                    break;
                case nameof(Boolean):
                    ret = typeof(Checkbox<>).MakeGenericType(t);
                    break;
                case nameof(DateTime):
                    ret = typeof(DateTimePicker<>).MakeGenericType(t);
                    break;
                case nameof(Int32):
                case nameof(Double):
                case nameof(Decimal):
                    ret = typeof(BootstrapInput<>).MakeGenericType(t);
                    break;
                case nameof(String):
                    ret = typeof(BootstrapInput<>).MakeGenericType(typeof(string));
                    break;
            }
            return ret ?? typeof(BootstrapInput<>).MakeGenericType(typeof(string));
        }

        private EventCallback<TType> CreateCallback<TType>(string fieldName)
        {
            return EventCallback.Factory.Create<TType>(this, t =>
            {
                Model?.SetPropertyValue(fieldName, t);
            });
        }

        private Expression<Func<string, object>> CreateLambda(Type type)
        {
            var exp_p1 = Expression.Parameter(typeof(string));
            var method = GetType().GetMethod("CreateCallback", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(type);
            var body = Expression.Call(Expression.Constant(this), method, exp_p1);

            return Expression.Lambda<Func<string, object>>(Expression.Convert(body, typeof(object)), exp_p1);
        }

        private Expression CreateValueExpression(Type type, string fieldName)
        {
            var p = Expression.Property(Expression.Constant(Model), typeof(TModel).GetProperty(fieldName));

            var tDelegate = typeof(Func<>).MakeGenericType(type);
            return Expression.Lambda(tDelegate, p);
        }

        private object Create(ITableColumn col, Type type)
        {
            var fieldName = col.GetFieldName();
            var invoker = EventCallbackCache.GetOrAdd((type, fieldName), key => (CreateLambda(type).Compile()));
            return invoker(fieldName);
        }

        private ConcurrentDictionary<(Type ModelType, string FieldName), Func<string, object>> EventCallbackCache { get; } = new ConcurrentDictionary<(Type, string), Func<string, object>>();
    }
}
