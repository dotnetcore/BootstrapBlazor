using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// MultiSelect 组件
    /// </summary>
    public partial class MultiSelect<TValue>
    {
        private ElementReference SelectElement { get; set; }

        private List<SelectedItem> SelectedItems { get; set; } = new List<SelectedItem>();

        private bool IsShow { get; set; }

        private string? ClassString => CssBuilder.Default("multi-select")
            .AddClass("show", IsShow)
            .Build();

        private string? MenuClassString => CssBuilder.Default("multi-select-menu fade")
            .AddClass("show", IsShow)
            .Build();

        private string? ToggleClassString => CssBuilder.Default("multi-select-toggle")
            .AddClass("is-disabled", IsDisabled)
            .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled)
            .AddClass("selected", SelectedItems.Any())
            .AddClass(CssClass).AddClass(ValidCss)
            .Build();

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        private string? AppendClassString => CssBuilder.Default("append")
            .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled)
            .Build();

        private string? GetItemClassString(SelectedItem item) => CssBuilder.Default("multi-select-menu-item")
            .AddClass("active", GetCheckedState(item))
            .Build();

        private JSInterop<MultiSelect<TValue>>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 是否显示搜索框 默认为 false 不显示
        /// </summary>
        [Parameter]
        public bool ShowSearch { get; set; }

        /// <summary>
        /// 获得/设置 是否显示全选功能按钮 默认为 false 不显示
        /// </summary>
        [Parameter]
        public bool ShowSelectAllButton { get; set; }

        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.None;

        /// <summary>
        /// 获得/设置 绑定数据集
        /// </summary>
        [Parameter]
        public IEnumerable<SelectedItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 搜索文本发生变化时回调此方法
        /// </summary>
        [Parameter]
        public Func<string, IEnumerable<SelectedItem>>? OnSearchTextChanged { get; set; }

        /// <summary>
        /// 获得/设置 选中项集合发生改变时回调委托方法
        /// </summary>
        [Parameter]
        public Func<IEnumerable<SelectedItem>, Task>? OnSelectedItemsChanged { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 通过 Value 对集合进行赋值
            if (Value != null)
            {
                var typeValue = typeof(TValue);
                IList? list = null;
                if (typeValue == typeof(string))
                {
                    list = CurrentValueAsString.Split(',', StringSplitOptions.RemoveEmptyEntries);
                }
                else if (typeValue.IsGenericType)
                {
                    var t = typeValue.GenericTypeArguments;
                    var instance = Activator.CreateInstance(typeof(List<>).MakeGenericType(t));
                    var mi = instance.GetType().GetMethod("AddRange");
                    if (mi != null)
                    {
                        mi.Invoke(instance, new object[] { Value });
                    }

                    list = instance as IList;
                }
                if (list != null && Items != null)
                {
                    foreach (var item in Items)
                    {
                        var v = item.Value;
                        if (!string.IsNullOrEmpty(v))
                        {
                            foreach (var l in list)
                            {
                                if (v == l.ToString())
                                {
                                    SelectedItems.Add(item);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (OnSearchTextChanged == null && Items != null)
            {
                OnSearchTextChanged = text => Items.Where(i => i.Text.Contains(text, StringComparison.OrdinalIgnoreCase));
            }
        }

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
                Interop = new JSInterop<MultiSelect<TValue>>(JSRuntime);
                await Interop.Invoke(this, SelectElement, "bb_multi_select", nameof(Close));
            }
        }

        /// <summary>
        /// 客户端关闭下拉框方法
        /// </summary>
        [JSInvokable]
        public void Close()
        {
            SearchText = "";
            IsShow = false;
            StateHasChanged();
        }

        private void ToggleMenu()
        {
            if (!IsDisabled)
            {
                IsShow = !IsShow;
            }
        }

        private void ToggleRow(SelectedItem item)
        {
            if (!IsDisabled)
            {
                if (SelectedItems.Contains(item))
                {
                    SelectedItems.Remove(item);
                }
                else
                {
                    SelectedItems.Add(item);
                }

                SetValue();

                _ = TriggerSelectedItemChanged();
            }
        }

        private async Task TriggerSelectedItemChanged()
        {
            if (OnSelectedItemsChanged != null) await OnSelectedItemsChanged.Invoke(SelectedItems);
        }

        private void SetValue()
        {
            var typeValue = typeof(TValue);
            if (typeValue == typeof(string))
            {
                CurrentValueAsString = string.Join(",", SelectedItems.Select(i => i.Value));
            }
            else if (typeValue.IsGenericType)
            {
                var t = typeValue.GenericTypeArguments;
                var instance = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(t));

                foreach (var item in SelectedItems)
                {
                    var val = item.Value;
                    instance.Add(val);
                }
                CurrentValue = (TValue)instance;
            }
        }

        private async Task Clear()
        {
            SelectedItems.Clear();

            await TriggerSelectedItemChanged();
        }

        private async Task SelectAll()
        {
            SelectedItems.AddRange(Items);

            await TriggerSelectedItemChanged();
        }

        private async Task InvertSelect()
        {
            var items = Items.Where(i => !SelectedItems.Any(item => item == i)).ToList();
            SelectedItems.Clear();
            SelectedItems.AddRange(items);

            await TriggerSelectedItemChanged();
        }

        private bool GetCheckedState(SelectedItem item) => SelectedItems.Contains(item);

        private string SearchText { get; set; } = "";

        private IEnumerable<SelectedItem> GetData()
        {
            var data = Items;
            if (ShowSearch && !string.IsNullOrEmpty(SearchText))
            {
                data = OnSearchTextChanged?.Invoke(SearchText);
            }
            return data ?? Enumerable.Empty<SelectedItem>();
        }

        /// <summary>
        /// 客户端检查完成时调用此方法
        /// </summary>
        /// <param name="valid"></param>
        protected override void OnValidate(bool valid)
        {
            Color = valid ? Color.Success : Color.Danger;
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                Interop?.Dispose();
            }
        }
    }
}
