// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// InputNumber 组件
    /// </summary>
    public abstract class BootstrapInputNumberBase<TValue> : BootstrapInput<TValue>
    {
        private object? MinValue { get; set; }

        private object? MaxValue { get; set; }

        /// <summary>
        /// 获得 按钮样式
        /// </summary>
        protected string? ButtonClassString => CssBuilder.Default("btn")
            .AddClass("btn-outline-secondary", Color == Color.None)
            .AddClass($"btn-outline-{Color.ToDescriptionString()}", Color != Color.None)
            .Build();

        /// <summary>
        /// 获得 文本框样式
        /// </summary>
        protected string? InputClassString => CssBuilder.Default("form-control")
            .AddClass(CssClass).AddClass(ValidCss)
            .AddClass("input-number-fix", ShowButton)
            .AddClass($"border-{Color.ToDescriptionString()} shadow-{Color.ToDescriptionString()}", Color != Color.None)
            .Build();

#nullable disable
        /// <summary>
        /// 获得/设置 数值步进步长
        /// </summary>
        [Parameter]
        public TValue Step { get; set; }
#nullable restore

        /// <summary>
        /// 获得/设置 数值增加时回调委托
        /// </summary>
        [Parameter]
        public Func<TValue, Task>? OnIncrement { get; set; }

        /// <summary>
        /// 获得/设置 数值减少时回调委托
        /// </summary>
        [Parameter]
        public Func<TValue, Task>? OnDecrement { get; set; }

        /// <summary>
        /// 获得/设置 最小值
        /// </summary>
        [Parameter]
        public string? Min { get; set; }

        /// <summary>
        /// 获得/设置 最大值
        /// </summary>
        [Parameter]
        public string? Max { get; set; }

        /// <summary>
        /// 获得/设置 是否显示加减按钮
        /// </summary>
        [Parameter]
        public bool ShowButton { get; set; }

        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            if (!typeof(TValue).IsNumber()) throw new InvalidOperationException($"The type '{typeof(TValue)}' is not a supported numeric type.");

            base.OnInitialized();

            // 本组件接受的类型均不可为空
            SetStep();

            // 设置最大值与最小值区间
            SetRange();

            if (AdditionalAttributes == null) AdditionalAttributes = new Dictionary<string, object>(100);

            if (MaxValue != null)
            {
                AdditionalAttributes["max"] = MaxValue;
            }
            if (MinValue != null)
            {
                AdditionalAttributes["min"] = MinValue;
            }
        }

        /// <summary>
        /// 点击减少按钮式时回调此方法
        /// </summary>
        /// <returns></returns>
        protected async Task OnClickDec()
        {
#nullable disable
            if (typeof(TValue) == typeof(sbyte))
            {
                var v = (sbyte)(object)Value;
                var s = (sbyte)(object)Step;

                CurrentValueAsString = (v - s).ToString();
            }
            else if (typeof(TValue) == typeof(byte))
            {
                var v = (byte)(object)Value;
                var s = (byte)(object)Step;

                CurrentValueAsString = (v - s).ToString();
            }
#nullable restore
            else
            {
                Range(Subtract(CurrentValue, Step));
                if (OnDecrement != null) await OnDecrement(CurrentValue);
            }
        }

        /// <summary>
        /// 点击增加按钮式时回调此方法
        /// </summary>
        /// <returns></returns>
        protected async Task OnClickInc()
        {
#nullable disable
            if (typeof(TValue) == typeof(sbyte))
            {
                var v = (sbyte)(object)Value;
                var s = (sbyte)(object)Step;

                CurrentValueAsString = (v + s).ToString();
            }
            else if (typeof(TValue) == typeof(byte))
            {
                var v = (byte)(object)Value;
                var s = (byte)(object)Step;

                CurrentValueAsString = (v + s).ToString();
            }
#nullable restore
            else
            {
                Range(Add(CurrentValue, Step));
                if (OnIncrement != null) await OnIncrement(CurrentValue);
            }
        }

        /// <summary>
        /// 失去焦点是触发此方法
        /// </summary>
        /// <returns></returns>
        protected void OnBlur()
        {
            if (MinValue != null || MaxValue != null)
            {
                Range(Value);
            }
        }

        /// <summary>
        /// 通过 MinValue 与 MaxValue 区间判断当前值方法
        /// </summary>
        /// <returns></returns>
        private void Range(TValue val)
        {
            if (MinValue != null)
            {
                val = MathMax(val, (TValue)MinValue);
            }
            if (MaxValue != null)
            {
                val = MathMin(val, (TValue)MaxValue);
            }
            CurrentValue = val;
        }

        /// <summary>
        /// 设置默认步长方法
        /// </summary>
        private void SetStep()
        {
            if (TryParse("0", out var step0) && LessThanOrEqual(Step, step0) && TryParse("1", out var step1))
            {
                Step = step1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetRange()
        {
            if (!string.IsNullOrEmpty(Min) && TryParse(Min, out var min))
            {
                MinValue = min;
            }
            if (!string.IsNullOrEmpty(Max) && TryParse(Max, out var max))
            {
                MaxValue = max;
            }

            if (typeof(TValue) == typeof(sbyte)
                || typeof(TValue) == typeof(byte)
                || typeof(TValue) == typeof(short)
                || typeof(TValue) == typeof(int))
            {
                if (MaxValue == null)
                {
                    MaxValue = MaxValueCache.GetOrAdd(typeof(TValue), key =>
                    {
                        var invoker = GetMaxValue().Compile();
                        return invoker();
                    });
                }
                if (MinValue == null)
                {
                    MinValue = MinValueCache.GetOrAdd(typeof(TValue), key =>
                    {
                        var invoker = GetMinValue().Compile();
                        return invoker();
                    });
                }
            }
        }

        private static ConcurrentDictionary<Type, TValue> MinValueCache { get; } = new ConcurrentDictionary<Type, TValue>();

        private static ConcurrentDictionary<Type, TValue> MaxValueCache { get; } = new ConcurrentDictionary<Type, TValue>();

        private static Expression<Func<TValue>> GetMinValue()
        {
            var type = typeof(TValue);
            var p = type.GetField("MinValue");
            var body = Expression.Field(null, p!);
            return Expression.Lambda<Func<TValue>>(body);
        }

        private static Expression<Func<TValue>> GetMaxValue()
        {
            var type = typeof(TValue);
            var p = type.GetField("MaxValue");
            var body = Expression.Field(null, p!);
            return Expression.Lambda<Func<TValue>>(body);
        }

        /// <summary>
        /// 
        /// </summary>
        internal static ConcurrentDictionary<Type, LambdaExtensions.FuncEx<string, TValue, bool>> TryParseCache { get; set; } = new ConcurrentDictionary<Type, LambdaExtensions.FuncEx<string, TValue, bool>>();

        /// <summary>
        /// TryParse 泛型方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        protected bool TryParse(string source, out TValue item)
        {
            var invoker = TryParseCache.GetOrAdd(typeof(TValue), key => LambdaExtensions.TryParse<TValue>().Compile());
            return invoker(source, out item);
        }

        #region LessThan
        /// <summary>
        /// 泛型中的大于逻辑判断
        /// </summary>
        /// <returns></returns>
        private static Expression<Func<TValue, TValue, bool>> LessThan()
        {
            var p1 = Expression.Parameter(typeof(TValue));
            var p2 = Expression.Parameter(typeof(TValue));
            var body = Expression.LessThanOrEqual(p1, p2);
            return Expression.Lambda<Func<TValue, TValue, bool>>(body, p1, p2);
        }

        /// <summary>
        /// 小于判断方法 v1 &lt; v2
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private static bool LessThanOrEqual(TValue v1, TValue v2)
        {
            var invoker = LessThanOrEqualCache.GetOrAdd(typeof(TValue), key => LessThan().Compile());
            return invoker(v1, v2);
        }

        private static readonly ConcurrentDictionary<Type, Func<TValue, TValue, bool>> LessThanOrEqualCache = new ConcurrentDictionary<Type, Func<TValue, TValue, bool>>();
        #endregion

        #region Operation
        /// <summary>
        /// V++
        /// </summary>
        /// <returns></returns>
        private static Expression<Func<TValue, TValue, TValue>> Add()
        {
            var exp_p1 = Expression.Parameter(typeof(TValue));
            var exp_p2 = Expression.Parameter(typeof(TValue));
            return Expression.Lambda<Func<TValue, TValue, TValue>>(Expression.AddChecked(exp_p1, exp_p2), exp_p1, exp_p2);
        }

        private static TValue Add(TValue v1, TValue v2)
        {
            var invoker = AddCache.GetOrAdd(typeof(TValue), key => Add().Compile());
            return invoker(v1, v2);
        }

        /// <summary>
        /// V--
        /// </summary>
        /// <returns></returns>
        private static Expression<Func<TValue, TValue, TValue>> Subtract()
        {
            var exp_p1 = Expression.Parameter(typeof(TValue));
            var exp_p2 = Expression.Parameter(typeof(TValue));
            return Expression.Lambda<Func<TValue, TValue, TValue>>(Expression.SubtractChecked(exp_p1, exp_p2), exp_p1, exp_p2);
        }

        private static TValue Subtract(TValue v1, TValue v2)
        {
            var invoker = SubtractCache.GetOrAdd(typeof(TValue), key => Subtract().Compile());
            return invoker(v1, v2);
        }

        private static readonly ConcurrentDictionary<Type, Func<TValue, TValue, TValue>> AddCache = new ConcurrentDictionary<Type, Func<TValue, TValue, TValue>>();

        private static readonly ConcurrentDictionary<Type, Func<TValue, TValue, TValue>> SubtractCache = new ConcurrentDictionary<Type, Func<TValue, TValue, TValue>>();
        #endregion

        #region Math
        private static Expression<Func<TValue, TValue, TValue>> MathMin()
        {
            var exp_p1 = Expression.Parameter(typeof(TValue));
            var exp_p2 = Expression.Parameter(typeof(TValue));

            var method_min = typeof(Math).GetMethod(nameof(Math.Min), new Type[] { typeof(TValue), typeof(TValue) });
            var body = Expression.Call(method_min!, exp_p1, exp_p2);
            return Expression.Lambda<Func<TValue, TValue, TValue>>(body, exp_p1, exp_p2);
        }

        private static Expression<Func<TValue, TValue, TValue>> MathMax()
        {
            var exp_p1 = Expression.Parameter(typeof(TValue));
            var exp_p2 = Expression.Parameter(typeof(TValue));

            var method_min = typeof(Math).GetMethod(nameof(Math.Max), new Type[] { typeof(TValue), typeof(TValue) });
            var body = Expression.Call(method_min!, exp_p1, exp_p2);
            return Expression.Lambda<Func<TValue, TValue, TValue>>(body, exp_p1, exp_p2);
        }

        private static TValue MathMin(TValue v1, TValue v2)
        {
            var invoker = MinCache.GetOrAdd(typeof(TValue), key => MathMin().Compile());
            return invoker(v1, v2);
        }

        private static TValue MathMax(TValue v1, TValue v2)
        {
            var invoker = MaxCache.GetOrAdd(typeof(TValue), key => MathMax().Compile());
            return invoker(v1, v2);
        }

        private static readonly ConcurrentDictionary<Type, Func<TValue, TValue, TValue>> MinCache = new ConcurrentDictionary<Type, Func<TValue, TValue, TValue>>();

        private static readonly ConcurrentDictionary<Type, Func<TValue, TValue, TValue>> MaxCache = new ConcurrentDictionary<Type, Func<TValue, TValue, TValue>>();
        #endregion
    }
}
