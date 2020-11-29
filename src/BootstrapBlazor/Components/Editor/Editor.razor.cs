// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Editor 组件基类
    /// </summary>
    public sealed partial class Editor
    {
        /// <summary>
        /// 获得/设置 组件 DOM 实例
        /// </summary>
        private ElementReference EditorElement { get; set; }

        /// <summary>
        /// 获得/设置 JSInterop 实例
        /// </summary>
        private JSInterop<Editor>? Interope { get; set; }

        /// <summary>
        /// 获得 Editor 样式
        /// </summary>
        private string? EditClassString => CssBuilder.Default("editor-body form-control")
            .AddClass("open", IsEditor)
            .Build();

        /// <summary>
        /// 获得/设置 Placeholder 提示消息
        /// </summary>
        [Parameter]
        [NotNull]
        public string? PlaceHolder { get; set; }

        /// <summary>
        /// 获得/设置 是否直接显示为富文本编辑框
        /// </summary>
        [Parameter]
        public bool IsEditor { get; set; }

        /// <summary>
        /// 获得/设置 设置组件高度
        /// </summary>
        [Parameter]
        public int Height { get; set; }

        /// <summary>
        /// 获得/设置 富文本框工具栏工具，若为空，则使用默认值
        /// </summary>
        [Parameter]
        public List<object>? ToolbarItems { get; set; }

        /// <summary>
        /// 获得/设置 插件的信息
        /// </summary>
        [Parameter]
        public List<EditorPluginItem>? EditorPluginItems { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Editor>? Localizer { get; set; }

        private string? _value;
        private bool _renderValue;
        /// <summary>
        /// 获得/设置 组件值
        /// </summary>
        [Parameter]
        public string? Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    _renderValue = true;
                }
            }
        }

        /// <summary>
        /// 获得/设置 组件值变化后的回调委托
        /// </summary>
        [Parameter]
        public EventCallback<string?> ValueChanged { get; set; }

        /// <summary>
        /// 获取/设置 插件点击时的回调委托
        /// </summary>
        [Parameter]
        public EventCallback<EditorEventArgs> OnPluginClick { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            PlaceHolder ??= Localizer[nameof(PlaceHolder)];
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
                Interope = new JSInterop<Editor>(JSRuntime);
                if (EditorPluginItems != null && EditorPluginItems.Count > 0)
                {
                    await Interope.Invoke(this, EditorElement, "editor_plugin", nameof(GetPluginAttrs), nameof(PluginClick));
                }
                await Interope.Invoke(this, EditorElement, "editor", nameof(Update), Height, Value ?? "");
            }
            if (_renderValue)
            {
                _renderValue = false;
                await JSRuntime.InvokeVoidAsync(EditorElement, "editor", "code", "", "", Value ?? "");
            }
        }

        /// <summary>
        /// Update 方法
        /// </summary>
        /// <param name="value"></param>
        [JSInvokable]
        public async Task Update(string value)
        {
            Value = value;
            if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(Value);
            _renderValue = false;
        }

        /// <summary>
        /// 获取编辑器的toolbar
        /// </summary>
        /// <returns>toolbar</returns>
        [JSInvokable]
        public Task<List<object>> GetToolBar()
        {
            List<object>? list = ToolbarItems;
            if (list == null || list.Count == 0)
            {
                list = DefaultToolBar();
            }
            if (EditorPluginItems != null && EditorPluginItems.Count > 0)
            {
                var itemList = new List<object>();
                itemList.Add("custom");
                var pluginList = new List<string>();
                foreach (var editorPluginItem in EditorPluginItems)
                {
                    pluginList.Add(editorPluginItem.PluginName ?? "");
                }
                itemList.Add(pluginList);
                list.Add(itemList);
            }
            return Task.FromResult(list);
        }

        /// <summary>
        /// 获取插件信息
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public Task<List<EditorPluginItem>?> GetPluginAttrs()
        {
            return Task.FromResult(EditorPluginItems);
        }

        /// <summary>
        /// 插件点击事件
        /// </summary>
        /// <param name="pluginName">插件名</param>
        /// <returns>插件回调的文本</returns>
        [JSInvokable]
        public async Task<string> PluginClick(string pluginName)
        {
            
            if (OnPluginClick.HasDelegate)
            {
                EditorEventArgs eventArgs = new EditorEventArgs();
                eventArgs.PluginName = pluginName;
                await OnPluginClick.InvokeAsync(eventArgs);
                return eventArgs.ResultValue ?? "";
            }

            return "";
        }

        /// <summary>
        /// 生成默认的富文本框状态栏
        /// </summary>
        /// <returns></returns>
        private List<object> DefaultToolBar()
        {
            return new List<object>
            {
                new List<object> {"style", new List<string>() {"style"}},
                new List<object> {"font", new List<string>() {"bold", "underline", "clear"}},
                new List<object> {"fontname", new List<string>() {"fontname"}},
                new List<object> {"color", new List<string>() {"color"}},
                new List<object> {"para", new List<string>() {"ul", "ol", "paragraph"}},
                new List<object> {"table", new List<string>() {"table"}},
                new List<object> {"insert", new List<string>() {"link", "picture", "video"}},
                new List<object> {"view", new List<string>() {"fullscreen", "codeview", "help"}}
            };
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
                Interope?.Dispose();
            }
        }
    }
}
