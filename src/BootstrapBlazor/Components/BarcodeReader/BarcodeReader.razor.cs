using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// BarcodeReader 条码扫描
    /// </summary>
    public sealed partial class BarcodeReader
    {
        private JSInterop<BarcodeReader>? Interop { get; set; }

        private string AutoStopString => AutoStop ? "true" : "false";

        private bool Disabled { get; set; } = true;

        /// <summary>
        /// 获得/设置 初始化摄像头回调方法
        /// </summary>
        [Parameter]
        public Func<IEnumerable<Camera>, Task>? OnInit { get; set; }

        /// <summary>
        /// 获得/设置 扫码结果回调方法
        /// </summary>
        [Parameter]
        public Func<string, Task>? OnResult { get; set; }

        /// <summary>
        /// 获得/设置 扫描条码后自动关闭
        /// </summary>
        [Parameter]
        public bool AutoStop { get; set; }

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

        private string DeviceId { get; set; } = "";

        private ElementReference ScannerElement { get; set; }

        private IEnumerable<SelectedItem> Devices { get; set; } = Enumerable.Empty<SelectedItem>();

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && JSRuntime != null)
            {
                Interop = new JSInterop<BarcodeReader>(JSRuntime);
                await Interop.Invoke(this, ScannerElement, "bb_barcode", "init");
            }
        }

        /// <summary>
        /// 初始化设备方法
        /// </summary>
        /// <param name="devices"></param>
        /// <returns></returns>
        [JSInvokable]
        public async Task InitDevices(IEnumerable<Camera> devices)
        {
            Devices = devices.Select(i => new SelectedItem { Value = i.DeviceId, Text = i.Label });
            Disabled = !Devices.Any();

            if (OnInit != null) await OnInit(devices);
            StateHasChanged();
        }

        /// <summary>
        /// 扫描完成回调方法
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [JSInvokable]
        public async Task GetResult(string val)
        {
            if (OnResult != null) await OnResult.Invoke(val);
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
        /// 开始扫描回调方法
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public async Task Start()
        {
            if (OnStart != null) await OnStart.Invoke();
        }

        /// <summary>
        /// 停止扫描回调方法
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public async Task Stop()
        {
            if (OnClose != null) await OnClose.Invoke();
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing && Interop != null)
            {
                Interop.Dispose();
            }
        }
    }
}
