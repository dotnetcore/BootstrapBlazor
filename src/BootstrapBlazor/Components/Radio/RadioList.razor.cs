// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RadioList<TValue>
    {
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
            base.OnInitialized();

            var t = NullableUnderlyingType ?? typeof(TValue);
            if (t.IsEnum)
            {
                Items = t.ToSelectList();
            }

            if (!Items.Any(i => i.Value == CurrentValueAsString))
            {
                CurrentValueAsString = Items.FirstOrDefault(i => i.Active)?.Value
                    ?? Items.FirstOrDefault()?.Value
                    ?? "";
            }
        }

        /// <summary>
        /// 点击选择框方法
        /// </summary>
        private async Task OnClick(SelectedItem item)
        {
            if (!IsDisabled)
            {
                if (typeof(TValue) == typeof(SelectedItem))
                {
                    CurrentValue = (TValue)(object)item;
                }
                else
                {
                    CurrentValueAsString = item.Value;
                }
                if (OnSelectedChanged != null) await OnSelectedChanged.Invoke(new SelectedItem[] { item }, Value);

                StateHasChanged();
            }
        }

        private CheckboxState CheckState(SelectedItem item)
        {
            return item.Value == CurrentValueAsString ? CheckboxState.Checked : CheckboxState.UnChecked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="validationErrorMessage"></param>
        /// <returns></returns>
        protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TValue result, out string? validationErrorMessage)
        {
            var ret = false;
            if (typeof(TValue) == typeof(SelectedItem))
            {
                var val = Items.FirstOrDefault(i => i.Value == value)
                    ?? Items.FirstOrDefault();
                if (val != null)
                {
                    result = (TValue)(object)val;
                }
                else
                {
                    result = default;
                }
                validationErrorMessage = null;
                ret = true;
            }
            else
            {
                ret = base.TryParseValueFromString(value, out result, out validationErrorMessage);
            }
            return ret;
        }

        /// <summary>
        /// 将 Value 格式化为 String 方法
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>A string representation of the value.</returns>
        protected override string? FormatValueAsString(TValue value) => typeof(TValue).Name switch
        {
            nameof(SelectedItem) => (value as SelectedItem)?.Value,
            _ => value?.ToString()
        };
    }
}
