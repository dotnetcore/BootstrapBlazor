// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class BarcodeReaders
    {
        private Logger? Trace { get; set; }

        private Logger? Trace2 { get; set; }

        private Task OnInit(IEnumerable<DeviceItem> devices)
        {
            var cams = string.Join("", devices.Select(i => i.Label));
            Trace?.Log($"初始化摄像头完成 {cams}");
            return Task.CompletedTask;
        }

        private Task OnImageResult(string barcode)
        {
            Trace2?.Log($"扫描到条码 {barcode}");
            return Task.CompletedTask;
        }

        private Task OnImageError(string err)
        {
            Trace2?.Log($"发生错误 {err}");
            return Task.CompletedTask;
        }

        private Task OnResult(string barcode)
        {
            Trace?.Log($"扫描到条码 {barcode}");
            return Task.CompletedTask;
        }

        private Task OnError(string error)
        {
            Trace?.Log($"发生错误 {error}");
            return Task.CompletedTask;
        }

        private Task OnStart()
        {
            Trace?.Log($"打开摄像头");
            return Task.CompletedTask;
        }

        private Task OnClose()
        {
            Trace?.Log($"关闭摄像头");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获得属性
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            new AttributeItem()
            {
                Name = "ButtonScanText",
                Description = "扫描按钮文字",
                Type = "string",
                ValueList = " - ",
                DefaultValue = "扫描"
            },
            new AttributeItem()
            {
                Name = "ButtonStopText",
                Description = "关闭按钮文字",
                Type = "string",
                ValueList = " - ",
                DefaultValue = "关闭"
            },
            new AttributeItem()
            {
                Name = "AutoStopText",
                Description = "自动关闭按钮文字",
                Type = "string",
                ValueList = " - ",
                DefaultValue = "自动关闭"
            },
            new AttributeItem()
            {
                Name = "DeviceLabel",
                Description = "设备列表前置标签文字",
                Type = "string",
                ValueList = " - ",
                DefaultValue = "摄像头"
            },
            new AttributeItem()
            {
                Name = "InitDevicesString",
                Description = "初始化设备列表文字",
                Type = "string",
                ValueList = " - ",
                DefaultValue = "正在识别摄像头"
            },
            new AttributeItem()
            {
                Name = "NotFoundDevicesString",
                Description = "未找到视频相关设备文字",
                Type = "string",
                ValueList = " - ",
                DefaultValue = "未找到视频相关设备"
            },
            new AttributeItem()
            {
                Name = "AutoStart",
                Description = "组件初始化时是否自动开启摄像头",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem()
            {
                Name = "AutoStop",
                Description = "扫描到条码后是否自动停止",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem()
            {
                Name = "ScanType",
                Description = "扫描方式摄像头或者图片",
                Type = "ScanType",
                ValueList = "Camera|Image",
                DefaultValue = "Camera"
            },
            new AttributeItem()
            {
                Name = "OnInit",
                Description = "初始化摄像头回调方法",
                Type = "Func<IEnumerable<Camera>, Task>",
                ValueList = " - ",
                DefaultValue = " - "
            },
            new AttributeItem()
            {
                Name = "OnResult",
                Description = "扫描到条码回调方法",
                Type = "Func<string, Task>",
                ValueList = " - ",
                DefaultValue = " - "
            },
            new AttributeItem()
            {
                Name = "OnStart",
                Description = "打开摄像头回调方法",
                Type = "Func<Task>",
                ValueList = " - ",
                DefaultValue = " - "
            },
            new AttributeItem()
            {
                Name = "OnClose",
                Description = "关闭摄像头回调方法",
                Type = "Func<Task>",
                ValueList = " - ",
                DefaultValue = " - "
            },
            new AttributeItem()
            {
                Name = "OnError",
                Description = "发生错误回调方法",
                Type = "Func<string, Task>",
                ValueList = " - ",
                DefaultValue = " - "
            }
        };
    }
}
