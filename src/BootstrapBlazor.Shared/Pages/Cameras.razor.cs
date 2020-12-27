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
    public sealed partial class Cameras
    {
#nullable disable
        /// <summary>
        /// 
        /// </summary>
        private Logger Trace { get; set; }
#nullable restore

        private Task OnInit(IEnumerable<DeviceItem> devices)
        {
            var cams = string.Join("", devices.Select(i => i.Label));
            Trace?.Log($"初始化摄像头完成 {cams}");
            return Task.CompletedTask;
        }

        private Task OnError(string err)
        {
            Trace?.Log($"发生错误 {err}");
            return Task.CompletedTask;
        }

        private Task OnStart()
        {
            Trace?.Log("打开摄像头");
            return Task.CompletedTask;
        }

        private Task OnClose()
        {
            Trace?.Log("关闭摄像头");
            return Task.CompletedTask;
        }

        private Task OnCapture()
        {
            Trace?.Log("拍照完成");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "ShowPreview",
                Description = "是否显示 照片预览",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "AutoStart",
                Description = "是否直接开启摄像头",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "DeviceLabel",
                Description = "设备列表前置标签文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "FrontText",
                Description = "前置显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "BackText",
                Description = "后置显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "PlayText",
                Description = "开启按钮显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "StopText",
                Description = "关闭按钮显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "PhotoText",
                Description = "拍照按钮显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "DeviceLabel",
                Description = "设备列表前置标签文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "InitDevicesString",
                Description = "初始化设备列表文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
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
            new AttributeItem() {
                Name = "OnInit",
                Description = "初始化摄像头回调方法",
                Type = "Func<IEnumerable<DeviceItem>, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnStart",
                Description = "开始扫码回调方法",
                Type = "Func<Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnClose",
                Description = "关闭扫码回调方法",
                Type = "Func<Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnCapture",
                Description = "扫码成功回调方法",
                Type = "Func<Task>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
