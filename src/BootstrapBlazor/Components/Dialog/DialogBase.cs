using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Dialog 对话框组件
    /// </summary>
    public abstract class DialogBase : BootstrapComponentBase
    {
        /// <summary>
        /// Modal 容器组件实例
        /// </summary>
        protected Modal? ModalContainer { get; set; }

        private bool IsShowDialog { get; set; }

        /// <summary>
        /// DialogServices 服务实例
        /// </summary>
        [Inject]
        public DialogService? DialogService { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 注册 Toast 弹窗事件
            if (DialogService != null)
            {
                DialogService.Subscribe(Show);
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

            if (ModalContainer != null && IsShowDialog)
            {
                IsShowDialog = false;
                await ModalContainer.Toggle();
            }
        }

        private async Task Show(DialogOption option)
        {
            if (ModalContainer != null)
            {
                await ModalContainer.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object>()
                {
                    [nameof(Modal.ChildContent)] = new RenderFragment(builder =>
                    {
                        var index = 0;
                        builder.OpenComponent<ModalDialog>(index++);
                        builder.AddMultipleAttributes(index++, option.ToAttributes());
                        if (option.ComponentType != null)
                        {
                            option.BodyTemplate = new RenderFragment(builder =>
                            {
                                builder.OpenComponent(index++, option.ComponentType);
                                if (option.BodyComponentParameters != null) builder.AddMultipleAttributes(index++, option.BodyComponentParameters);
                                builder.CloseComponent();
                            });
                            builder.AddAttribute(index++, nameof(ModalDialog.BodyTemplate), option.BodyTemplate);
                        }
                        if (option.BodyTemplate != null) builder.AddAttribute(index++, nameof(ModalDialog.BodyTemplate), option.BodyTemplate);

                        if (option.FooterTemplate != null)
                        {
                            builder.AddAttribute(index++, nameof(ModalDialog.FooterTemplate), option.FooterTemplate);
                        }
                        builder.CloseComponent();
                    })
                }));
                IsShowDialog = true;
                StateHasChanged();
            }
        }
    }
}
