using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// AutoComplete 组件基类
    /// </summary>
    public abstract class AutoCompleteBase : BootstrapInputBase<string>
    {
        private bool _isLoading;
        private bool _isShown;

        /// <summary>
        /// 获得 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("auto-complete")
            .AddClass("is-loading", _isLoading)
            .AddClass("is-complete", _isShown)
            .Build();

        /// <summary>
        /// 获得/设置 通过输入字符串获得匹配数据集合
        /// </summary>
        [Parameter]
        public IEnumerable<string> Items { get; set; } = new string[0];

        /// <summary>
        /// 获得/设置 组件值变化时回调委托方法用于通过客户端输入值获取自动完成数据
        /// </summary>
        [Parameter]
        public Action<string>? OnValueChanged { get; set; }

        private string? _placeholder;
        /// <summary>
        /// 获得 PlaceHolder 属性
        /// </summary>
        [Parameter]
        public string? PlaceHolder
        {
            get
            {
                if (string.IsNullOrEmpty(_placeholder))
                {
                    _placeholder = "请输入";
                    if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("placeholder", out var ph) && !string.IsNullOrEmpty(Convert.ToString(ph)))
                    {
                        _placeholder = ph.ToString();
                    }
                }
                return _placeholder;
            }
            set
            {
                _placeholder = value;
            }
        }

        /// <summary>
        /// 当无匹配数据时，默认提示"无匹配数据"
        /// </summary>
        [Parameter]
        public string DefaultValue { get; set; } = "无匹配数据";

        /// <summary>
        /// OnParametersSet
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            Items = Items.Where(s => s.StartsWith(CurrentValueAsString));
        }

        /// <summary>
        /// OnInput 方法
        /// </summary>
        protected void OnInput(ChangeEventArgs args)
        {
            CurrentValueAsString = Convert.ToString(args.Value) ?? "";
            _isLoading = true;
            Task.Run(() =>
            {
                OnValueChanged?.Invoke(CurrentValueAsString);
                _isLoading = false;
                _isShown = true;
                InvokeAsync(StateHasChanged);
            });
        }

        /// <summary>
        /// OnBlur 方法
        /// </summary>
        protected void OnBlur()
        {
            InvokeAsync(async () =>
            {
                await Task.Delay(100);
                if (!_itemTrigger)
                {
                    _isShown = false;
                    _itemTrigger = false;
                    StateHasChanged();
                }
            });
        }

        private bool _itemTrigger;

        /// <summary>
        /// 
        /// </summary>
        protected void OnItemClick(string val)
        {
            _itemTrigger = true;
            _isShown = false;
            CurrentValueAsString = val;
        }
    }
}
