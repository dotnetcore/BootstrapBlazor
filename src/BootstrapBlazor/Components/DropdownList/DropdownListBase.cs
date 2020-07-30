using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// DropdownListBase 组件基类
    /// </summary>
    public abstract class DropdownListBase<TValue> : BootstrapInputBase<TValue>
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected new string? ClassName => CssBuilder.Default("form-select dropdown")
            .Build();

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected string? InputClassName => CssBuilder.Default("form-control form-select-input")
            .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None)
            .Build();

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected string? ArrowClassName => CssBuilder.Default("form-select-append")
            .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
            .Build();

        /// <summary>
        /// 设置当前项是否 Active 方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual string? ActiveItem(SelectedItem item) => CssBuilder.Default("dropdown-item")
            .AddClass("active", () => item.Value == CurrentValueAsString)
            .Build();

        /// <summary>
        /// 设置当前项是否 Active 方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual string? ActiveItem(object item) => CssBuilder.Default("dropdown-item")
            .AddClass("active", () => item == CurrentItem)
            .Build();

        /// <summary>
        /// 
        /// </summary>
        protected string Text => GetText(CurrentItem);

        /// <summary>
        /// 
        /// </summary>
        protected object? CurrentItem { get; set; }

        /// <summary>
        /// 获得 PlaceHolder 属性
        /// </summary>
        protected string? PlaceHolder
        {
            get
            {
                string? placeHolder = "请选择 ...";
                if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("placeholder", out var ph) && !string.IsNullOrEmpty(Convert.ToString(ph)))
                {
                    placeHolder = ph.ToString();
                }
                return placeHolder;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string DefaultText { get; set; } = "请选择 ...";

        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.None;

        /// <summary>
        /// 获得/设置 绑定数据集
        /// </summary>
        [Parameter]
        public IEnumerable<object>? Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string TextField { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string ValueField { get; set; } = "";

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            // 双向绑定其他组件更改了数据源值时
            if (Data != null && CurrentItem != null && CurrentItem.ToString() != CurrentValueAsString)
            {
                CurrentItem = Data.FirstOrDefault(i => GetValue(i) == CurrentValueAsString);
            }

            // 设置数据集合后 SelectedItem 设置默认值
            if (CurrentItem == null || !(Data?.Any(i => GetValue(i) == CurrentItem.ToString() && GetText(i) == Text) ?? false))
            {
                var item = Data?.FirstOrDefault();
                if (item == null) item = Data?.FirstOrDefault(i => GetValue(i) == CurrentValueAsString) ?? Data?.FirstOrDefault();
                if (item != null)
                {
                    CurrentItem = item;
                    if (Value != null && CurrentValueAsString != GetValue(CurrentItem))
                    {
                        item = Data.FirstOrDefault(i => GetValue(i) == CurrentValueAsString);
                        if (item != null) CurrentItem = item;
                    }
                    CurrentValueAsString = GetValue(CurrentItem);
                }
            }
        }

        /// <summary>
        /// 下拉框选项点击时调用此方法
        /// </summary>
        protected void OnItemClick(object item)
        {
            CurrentItem = item;

            // ValueChanged
            CurrentValueAsString = GetValue(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected string GetValue(object? model) => GetFieldValue(model, ValueField);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected string GetText(object? model) => GetFieldValue(model, TextField);

        private string GetFieldValue(object? model, string fieldName)
        {
            var ret = "";
            if (model != null)
            {
                var v = model.GetPropertyValue<object, object>(fieldName);
                ret = v?.ToString() ?? "";
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="validationErrorMessage"></param>
        /// <returns></returns>
        protected override bool TryParseValueFromString(string value, out TValue result, out string? validationErrorMessage)
        {
            bool ret;
            if (typeof(TValue) == typeof(object))
            {
                result = (TValue)(object)value;
                validationErrorMessage = null;
                ret = true;
            }
            else
            {
                ret = base.TryParseValueFromString(value, out result, out validationErrorMessage);
            }
            return ret;
        }
    }
}
