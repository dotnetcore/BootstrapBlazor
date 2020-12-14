// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Dialog 组件配置类
    /// </summary>
    public class DialogOption
    {
        /// <summary>
        /// 获得/设置 相关弹窗实例
        /// </summary>
        public ModalBase? Dialog { get; internal set; }

        /// <summary>
        /// 获得/设置 弹窗标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 获得/设置 弹窗大小
        /// </summary>
        public Size Size { get; set; } = Size.Large;

        /// <summary>
        /// 获得/设置 是否垂直居中 默认为 true
        /// </summary>
        public bool IsCentered { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否弹窗正文超长时滚动 默认为 false
        /// </summary>
        public bool IsScrolling { get; set; } = false;

        /// <summary>
        /// 获得/设置 是否显示关闭按钮 默认为 true
        /// </summary>
        public bool ShowCloseButton { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示 Footer 默认为 true
        /// </summary>
        public bool ShowFooter { get; set; } = true;

        /// <summary>
        /// 获得/设置 相关连数据，多用于传值使用
        /// </summary>
        public object? BodyContext { get; set; }

        /// <summary>
        /// 获得/设置 ModalBody 组件
        /// </summary>
        public RenderFragment? BodyTemplate { get; set; }

        /// <summary>
        /// 获得/设置 ModalFooter 组件
        /// </summary>
        public RenderFragment? FooterTemplate { get; set; }

        /// <summary>
        /// 获得/设置 是否保持弹窗内组件状态 默认为 false 不保持
        /// </summary>
        public bool KeepChildrenState { get; set; }

        /// <summary>
        /// 获得/设置 自定义组件
        /// </summary>
        public DynamicComponent? Component { get; set; }

        /// <summary>
        /// 获得/设置 关闭弹窗回调方法
        /// </summary>
        public Func<Task>? OnCloseAsync { get; set; }

        /// <summary>
        /// 将参数转换为组件属性方法
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, object>> ToAttributes()
        {
            return new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>(nameof(Title), Title!),
                new KeyValuePair<string, object>(nameof(Size), Size),
                new KeyValuePair<string, object>(nameof(IsCentered), IsCentered),
                new KeyValuePair<string, object>(nameof(IsScrolling), IsScrolling),
                new KeyValuePair<string, object>(nameof(ShowCloseButton), ShowCloseButton),
                new KeyValuePair<string, object>(nameof(ShowFooter), ShowFooter),
                new KeyValuePair<string, object>(nameof(BodyContext), BodyContext!),
            };
        }
    }
}
