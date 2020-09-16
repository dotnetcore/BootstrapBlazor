using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// SweetAlert Option 配置类
    /// </summary>
    public class SwalOption : PopupOptionBase
    {
        /// <summary>
        /// 获得/设置 相关弹窗实例
        /// </summary>
        public ModalBase? Dialog { get; internal set; }

        /// <summary>
        /// 获得/设置 提示类型 默认为 Sucess
        /// </summary>
        public SwalCategory Category { get; set; }

        /// <summary>
        /// 获得/设置 弹窗标题
        /// </summary>
        public string Title { get; set; } = "";

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
        /// 获得/设置 自定义组件
        /// </summary>
        public DynamicComponent? Component { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SwalOption()
        {
            IsAutoHide = false;
        }

        /// <summary>
        /// 将参数转换为组件属性方法
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, object>> ToAttributes()
        {
            return new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>(nameof(ModalDialogBase.Title), Title!),
                new KeyValuePair<string, object>(nameof(ModalDialogBase.Size), Size.Medium),
                new KeyValuePair<string, object>(nameof(ModalDialogBase.IsCentered), true),
                new KeyValuePair<string, object>(nameof(ModalDialogBase.IsScrolling), false),
                new KeyValuePair<string, object>(nameof(ModalDialogBase.ShowCloseButton), false),
                new KeyValuePair<string, object>(nameof(ModalDialogBase.ShowFooter), false),
                new KeyValuePair<string, object>(nameof(BodyContext), BodyContext!),
            };
        }
    }
}
