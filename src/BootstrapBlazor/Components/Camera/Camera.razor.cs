// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Camera
    {
        private ElementReference CameraElement { get; set; }

        private JSInterop<Camera>? Interop { get; set; }

        private string DeviceId { get; set; } = "";

        private bool Disabled { get; set; } = true;

        private bool CaptureDisabled { get; set; } = true;

        private IEnumerable<SelectedItem> Devices { get; set; } = Enumerable.Empty<SelectedItem>();

        private IEnumerable<SelectedItem> Cameras { get; set; } = new SelectedItem[]
        {
            new SelectedItem { Text = "前置", Value = "user", Active = true },
            new SelectedItem { Text = "后置", Value = "environment" }
        };

        private SelectedItem? ActiveCamera { get; set; }

        /// <summary>
        /// 获得/设置 是否显示 照片预览
        /// </summary>
        [Parameter]
        public bool ShowPreview { get; set; }

        /// <summary>
        /// 获得/设置 设备列表前置标签文字 默认为 摄像头
        /// </summary>
        [Parameter]
        public string DeviceLabel { get; set; } = "摄像头";

        /// <summary>
        /// 获得/设置 初始化设备列表文字 默认为 正在识别摄像头
        /// </summary>
        [Parameter]
        public string InitDevicesString { get; set; } = "正在识别摄像头";

        /// <summary>
        /// 获得/设置 初始化摄像头回调方法
        /// </summary>
        [Parameter]
        public Func<IEnumerable<DeviceItem>, Task>? OnInit { get; set; }

        /// <summary>
        /// 获得/设置 扫码出错回调方法
        /// </summary>
        [Parameter]
        public Func<string, Task>? OnError { get; set; }

        /// <summary>
        /// 获得/设置 开始扫码回调方法
        /// </summary>
        [Parameter]
        public Func<Task>? OnStart { get; set; }

        /// <summary>
        /// 获得/设置 关闭扫码回调方法
        /// </summary>
        [Parameter]
        public Func<Task>? OnClose { get; set; }

        /// <summary>
        /// 获得/设置 开始扫码回调方法
        /// </summary>
        [Parameter]
        public Func<Task>? OnCapture { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && JSRuntime != null)
            {
                Interop = new JSInterop<Camera>(JSRuntime);
                await Interop.Invoke(this, CameraElement, "bb_camera", "init");
            }
        }

        /// <summary>
        /// 初始化设备方法
        /// </summary>
        /// <param name="devices"></param>
        /// <returns></returns>
        [JSInvokable]
        public async Task InitDevices(IEnumerable<DeviceItem> devices)
        {
            Devices = devices.Select(i => new SelectedItem { Value = i.DeviceId, Text = i.Label });
            Disabled = !Devices.Any();

            if (OnInit != null) await OnInit(devices);
            if (devices.Any())
            {
                for (var index = 0; index < devices.Count(); index++)
                {
                    var d = devices.ElementAt(index);
                    if (string.IsNullOrEmpty(d.Label))
                    {
                        d.Label = $"Video device {index + 1}";
                    }
                }
                Disabled = false;
                ActiveCamera = Cameras.First();
            }
            StateHasChanged();
        }

        /// <summary>
        /// 扫描发生错误回调方法
        /// </summary>
        /// <param name="err"></param>
        /// <returns></returns>
        [JSInvokable]
        public async Task GetError(string err)
        {
            if (OnError != null) await OnError.Invoke(err);
        }

        /// <summary>
        /// 开启摄像头回调方法
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public async Task Start()
        {
            CaptureDisabled = false;
            if (OnStart != null) await OnStart.Invoke();
            StateHasChanged();
        }

        /// <summary>
        /// 停止摄像头回调方法
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public async Task Stop()
        {
            CaptureDisabled = true;
            if (OnClose != null) await OnClose.Invoke();
            StateHasChanged();
        }

        /// <summary>
        /// 拍照回调方法
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public async Task Capture()
        {
            if (OnCapture != null) await OnCapture.Invoke();
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
