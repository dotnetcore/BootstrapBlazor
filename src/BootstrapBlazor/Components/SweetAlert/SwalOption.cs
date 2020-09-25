using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        internal ModalBase? Dialog { get; set; }

        /// <summary>
        /// 获得/设置 相关弹窗实例
        /// </summary>
        internal ModalDialogBase? Body { get; set; }

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
        /// 获得/设置 是否显示关闭按钮 默认为 true 显示
        /// </summary>
        public bool ShowClose { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否保持弹窗内组件状态 默认为 false 不保持
        /// </summary>
        public bool KeepChildrenState { get; set; }

        /// <summary>
        /// 获得/设置 按钮模板
        /// </summary>
        public RenderFragment? ButtonTemplate { get; set; }

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
        public IEnumerable<KeyValuePair<string, object>> ToAttributes() => new List<KeyValuePair<string, object>>
        {
            new KeyValuePair<string, object>(nameof(ModalDialogBase.Title), Title),
            new KeyValuePair<string, object>(nameof(ModalDialogBase.Size), Size.Medium),
            new KeyValuePair<string, object>(nameof(ModalDialogBase.IsCentered), true),
            new KeyValuePair<string, object>(nameof(ModalDialogBase.IsScrolling), false),
            new KeyValuePair<string, object>(nameof(ModalDialogBase.ShowCloseButton), false),
            new KeyValuePair<string, object>(nameof(ModalDialogBase.ShowFooter), false),
            new KeyValuePair<string, object>(nameof(BodyContext), BodyContext!)
        };

        /// <summary>
        /// 关闭弹窗方法
        /// </summary>
        public async Task Close()
        {
            if (Dialog != null)
            {
                await Dialog.Toggle();
            }

            if (Body != null && Body.OnClose != null)
            {
                await Task.Delay(500);
                await Body.OnClose();
            }
        }
    }
}
