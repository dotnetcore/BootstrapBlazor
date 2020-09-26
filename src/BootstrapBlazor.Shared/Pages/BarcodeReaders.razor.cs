using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System;
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

        private Task OnInit(IEnumerable<Camera> devices)
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
                Name = "AutoStop",
                Description = "扫描到条码后是否自动停止",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "false"
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
