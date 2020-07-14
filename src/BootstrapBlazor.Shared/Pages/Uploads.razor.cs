using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Uploads
    {
        private Logger? Trace { get; set; }

        private string AllowFiles => "image/*";

        private Task OnUpload(string fileName, string prevUrl)
        {
            Trace?.Log($"{fileName} 成功上传 {prevUrl}");
            return Task.CompletedTask;
        }

        private Task OnRemoved(string fileName)
        {
            Trace?.Log($"{fileName} 成功移除");
            return Task.CompletedTask;
        }

        private Logger? PreviewUpload { get; set; }

        private Task OnPreviewUpload(string fileName, string prevUrl)
        {
            PreviewUpload?.Log($"{fileName} 成功上传 {prevUrl}");
            return Task.CompletedTask;
        }

        private IEnumerable<UploadHeader> OnSetHeaders()
        {
            return new UploadHeader[]
            {
                new UploadHeader("Authentication", "Bearer 12345")
            };
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "UploadUrl",
                Description = "组件上传接收地址",
                Type = "string",
                ValueList = "—",
                DefaultValue = "api/Upload"
            },
            new AttributeItem() {
                Name = "Text",
                Description = "上传按钮显示文字",
                Type = "string",
                ValueList = "—",
                DefaultValue = "上传文件"
            },
            new AttributeItem() {
                Name = "Icon",
                Description = "上传按钮显示图标",
                Type = "string",
                ValueList = "—",
                DefaultValue = "fa fa-cloud-upload"
            },
            new AttributeItem() {
                Name = "TipText",
                Description = "上传组件提示文字",
                Type = "string",
                ValueList = "—",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "ShowPreview",
                Description = "是否显示预览",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowProgress",
                Description = "是否显示上传进度",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowReset",
                Description = "是否显示重置",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Width",
                Description = "预览框宽度",
                Type = "int",
                ValueList = "—",
                DefaultValue = "0"
            },
            new AttributeItem() {
                Name = "Height",
                Description = "预览框高度",
                Type = "int",
                ValueList = "—",
                DefaultValue = "0"
            },
            new AttributeItem() {
                Name = "IsCard",
                Description = "是否为卡片式预览效果",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsCircle",
                Description = "是否为圆形头像模式",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsMultiple",
                Description = "是否允许多文件上传",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsPhotoWall",
                Description = "是否为照片墙效果",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsStack",
                Description = "是否为堆砌效果",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = "是否禁用",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "AllowFileType",
                Description = "设置允许上传文件扩展名 设置 file 控件的 accept 属性",
                Type = "string",
                ValueList = "—",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "MaxFileLength",
                Description = "设置上传文件最大值",
                Type = "long",
                ValueList = "—",
                DefaultValue = "0"
            }
        };

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnUploaded",
                Description="文件成功上传后回调此委托",
                Type ="Func<string, string, Task>"
            },
            new EventItem()
            {
                Name = "OnRemoved",
                Description="文件成功删除后回调此委托",
                Type ="Func<string, Task>"
            },
            new EventItem()
            {
                Name = "OnFailed",
                Description="文件上传失败后回调此委托",
                Type ="Func<string, Task>"
            },
            new EventItem()
            {
                Name = "OnSetHeaders",
                Description="客户端上传文件前设置请求头回调此委托",
                Type ="Func<IEnumerable<UploadHeader>>"
            }
        };

        private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
        {
            new MethodItem()
            {
                Name = "Reset",
                Description = "重置组件",
                Parameters = " - ",
                ReturnValue = "Task"
            }
        };
    }
}
