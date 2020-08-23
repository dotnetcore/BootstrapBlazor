using Microsoft.AspNetCore.Components;
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
    public abstract class TableDialog<TModel> : ComponentBase
    {
#nullable disable
        /// <summary>
        /// 获得/设置 EditModel 实例
        /// </summary>
        [Parameter]
        public TModel Model { get; set; }

        /// <summary>
        /// 获得/设置 BodyTemplate 实例
        /// </summary>
        [Parameter]
        public RenderFragment<TModel> BodyTemplate { get; set; }

        /// <summary>
        /// 获得 表头集合
        /// </summary>
        [Parameter]
        public List<ITableColumn> Columns { get; set; }
#nullable restore

        /// <summary>
        /// 获得/设置 是否显示标签
        /// </summary>
        [Parameter]
        public bool ShowLabel { get; set; }

        #region AutoEdit
        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected RenderFragment AutoGenerateTemplate(ITableColumn col, TModel model) => builder =>
        {
            var fieldType = col.FieldType;
            if (fieldType != null && model != null)
            {
                // GetDisplayName
                var displayName = col.GetDisplayName();
                var fieldName = col.GetFieldName();

                // FieldValue
                var valueInvoker = GetPropertyValueLambdaCache.GetOrAdd((typeof(TModel), fieldName), key => model.GetPropertyValueLambda<TModel, object?>(key.FieldName).Compile());
                var fieldValue = valueInvoker.Invoke(model);

                // ValueChanged
                var valueChangedInvoker = CreateLambda(fieldType).Compile();
                var fieldValueChanged = valueChangedInvoker(model, fieldName);

                // ValueExpression
                var body = Expression.Property(Expression.Constant(model), typeof(TModel), fieldName);
                var tDelegate = typeof(Func<>).MakeGenericType(fieldType);
                var valueExpression = Expression.Lambda(tDelegate, body);

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

            if (ShowLabel)
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="model"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        protected EventCallback<TType> CreateCallback<TType>(TModel model, string fieldName)
        {
            return EventCallback.Factory.Create<TType>(this, t =>
            {
                if (model != null)
                {
                    var invoker = SetPropertyValueLambdaCache.GetOrAdd((typeof(TModel), fieldName), key => model.SetPropertyValueLambda<TModel, object?>(key.FieldName).Compile());
                    invoker.Invoke(model, t);
                }
            });
        }

        private Expression<Func<TModel, string, object>> CreateLambda(Type fieldType)
        {
            var exp_p1 = Expression.Parameter(typeof(TModel));
            var exp_p2 = Expression.Parameter(typeof(string));
            var method = GetType().GetMethod("CreateCallback", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(fieldType);
            var body = Expression.Call(Expression.Constant(this), method, exp_p1, exp_p2);

            return Expression.Lambda<Func<TModel, string, object>>(Expression.Convert(body, typeof(object)), exp_p1, exp_p2);
        }

        private static ConcurrentDictionary<(Type ModelType, string FieldName), Func<TModel, object?>> GetPropertyValueLambdaCache { get; } = new ConcurrentDictionary<(Type, string), Func<TModel, object?>>();

        private static ConcurrentDictionary<(Type ModelType, string FieldName), Action<TModel, object?>> SetPropertyValueLambdaCache { get; } = new ConcurrentDictionary<(Type, string), Action<TModel, object?>>();
        #endregion
    }
}
