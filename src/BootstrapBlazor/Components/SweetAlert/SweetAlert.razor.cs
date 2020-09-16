using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// SweetAlert 组件
    /// </summary>
    sealed partial class SweetAlert
    {
#nullable disable
        /// <summary>
        /// 获得/设置 Modal 容器组件实例
        /// </summary>
        private Modal ModalContainer { get; set; }

        /// <summary>
        /// 获得/设置 弹出对话框实例
        /// </summary>
        private ModalDialog ModalDialog { get; set; }

        /// <summary>
        /// DialogServices 服务实例
        /// </summary>
        [Inject]
        public SwalService SwalService { get; set; }
#nullable restore
        private bool IsShowDialog { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 注册 Swal 弹窗事件
            SwalService.Register(this, Show);
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

        private async Task Show(SwalOption option)
        {
            option.Dialog = ModalContainer;
            var parameters = option.ToAttributes().ToList();

            if (option.BodyTemplate != null)
            {
                parameters.Add(new KeyValuePair<string, object>(nameof(ModalDialogBase.BodyTemplate), option.BodyTemplate));
            }
            else if (option.Component != null)
            {
                parameters.Add(new KeyValuePair<string, object>(nameof(ModalDialogBase.BodyTemplate), option.Component.Render()));
            }
            else
            {
                parameters.Add(new KeyValuePair<string, object>(nameof(ModalDialogBase.BodyTemplate), DynamicComponent.CreateComponent<SweetAlertBody>(SweetAlertBody.Parse(option)).Render()));
            }

            // 不保持状态
            parameters.Add(new KeyValuePair<string, object>(nameof(ModalDialogBase.OnClose), new Func<Task>(async () =>
            {
                await ModalDialog.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object>()
                {
                    [nameof(ModalDialogBase.BodyContext)] = null!,
                    [nameof(ModalDialogBase.BodyTemplate)] = null!
                }));
            })));

            await ModalDialog.SetParametersAsync(ParameterView.FromDictionary(parameters.ToDictionary(key => key.Key, value => value.Value)));
            IsShowDialog = true;
            StateHasChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                SwalService.UnRegister(this);
            }
        }
    }
}
