// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Uploads : IDisposable
    {
        private static readonly Random random = new Random();

        [Inject]
        [NotNull]
        private IWebHostEnvironment? WebHost { get; set; }

        [Inject]
        [NotNull]
        private ToastService? ToastService { get; set; }

        private Logger? Trace { get; set; }

        private Task OnFileChange(IEnumerable<UploadFile> files)
        {
            // 未真正保存文件
            // files.First().SaveToFile()
            Trace?.Log($"{files.First().File!.Name} 上传成功");
            return Task.FromResult("");
        }

        private async Task OnClickToUpload(IEnumerable<UploadFile> files)
        {
            // 示例代码，模拟 80% 几率保存成功
            var error = random.Next(1, 100) > 80;
            if (error)
            {
                files.First().Code = 1;
                files.First().Error = "模拟上传失败";
            }
            else
            {
                await SaveToFile(files.First());
            }
        }

        private CancellationTokenSource? UploadFolderToken { get; set; }
        private async Task OnUploadFolder(IEnumerable<UploadFile> files)
        {
            foreach (var file in files)
            {
                await SaveToFile(file);
            }
        }

        private CancellationTokenSource? ReadAvatarToken { get; set; }
        private async Task OnAvatarUpload(IEnumerable<UploadFile> files)
        {
            // 示例代码，使用 base64 格式
            var file = files.FirstOrDefault();
            if (file != null && file.File != null)
            {
                var format = file.File.ContentType;
                if (CheckValidAvatarFormat(format))
                {
                    ReadAvatarToken ??= new CancellationTokenSource();
                    if (ReadAvatarToken.IsCancellationRequested)
                    {
                        ReadAvatarToken.Dispose();
                        ReadAvatarToken = new CancellationTokenSource();
                    }

                    await file.RequestBase64ImageFileAsync(format, 640, 480, 20 * 1024 * 1024, ReadAvatarToken.Token);
                }
                else
                {
                    file.Code = 1;
                    file.Error = "文件格式不正确";
                }

                if (file.Code != 0)
                {
                    await ToastService.Error("头像上传", $"{file.Error} {format}");
                }
            }
        }

        private CancellationTokenSource? ReadToken { get; set; }
        private async Task OnCardUpload(IEnumerable<UploadFile> files)
        {
            // 示例代码，使用 IWebHostEnviroment 注入获取硬盘文件夹 示例
            var file = files.FirstOrDefault();
            if (file != null && file.File != null)
            {
                await SaveToFile(file);
            }
        }

        private async Task<bool> SaveToFile(UploadFile file)
        {
            // 生成写入文件名称
            var uploaderFolder = Path.Combine(WebHost.WebRootPath, $"images{Path.DirectorySeparatorChar}uploader");
            file.FileName = $"{Path.GetFileNameWithoutExtension(file.OriginFileName)}-{DateTimeOffset.Now:yyyyMMddHHmmss}{Path.GetExtension(file.OriginFileName)}";
            var fileName = Path.Combine(uploaderFolder, file.FileName);

            ReadToken ??= new CancellationTokenSource();
            var ret = await file.SaveToFile(fileName, 20 * 1024 * 1024, ReadToken.Token);

            if (ret)
            {
                // 保存成功
                file.PrevUrl = $"images/uploader/{file.FileName}";
            }
            else
            {
                await ToastService.Error("上传文件", $"保存文件失败 {file.OriginFileName}");
            }

            return ret;
        }

        private static bool CheckValidAvatarFormat(string format)
        {
            return "jpg;png;bmp;gif;jpeg".Split(';').Any(f => format.Contains(f, StringComparison.OrdinalIgnoreCase));
        }

        private Task<bool> OnFileDelete(string fileName)
        {
            Trace?.Log($"{fileName} 成功移除");
            return Task.FromResult(true);
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            new AttributeItem() {
                Name = "Style",
                Description = "组件风格",
                Type = "UploadStyle",
                ValueList = "—",
                DefaultValue = "Normal"
            },
            new AttributeItem() {
                Name = "ShowDeleteButton",
                Description = "是否显示删除按钮",
                Type = "boolean",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "PlaceHolder",
                Description = "占位字符串",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "DeleteButtonClass",
                Description = "删除按钮样式",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "btn-danger"
            },
            new AttributeItem() {
                Name = "BrowserButtonClass",
                Description = "上传按钮样式",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "btn-primary"
            },
            new AttributeItem() {
                Name = "DeleteButtonIcon",
                Description = "删除按钮图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-trash-o"
            },
            new AttributeItem() {
                Name = "BrowserButtonIcon",
                Description = "浏览按钮图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-folder-open-o"
            },
            new AttributeItem() {
                Name = "DefaultFileList",
                Description = "已上传文件集合",
                Type = "List<UploadFile>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "MaxFileCount",
                Description = "最大上传文件数量",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "10"
            },
            new AttributeItem() {
                Name = "Width",
                Description = "预览框宽度",
                Type = "int",
                ValueList = " — ",
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
                Name = "IsCircle",
                Description = "是否为圆形头像模式",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsDirectory",
                Description = "是否上传整个目录",
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
                Name = "IsDisabled",
                Description = "是否禁用",
                Type = "boolean",
                ValueList = "true / false",
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
        private static IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnChange",
                Description="上传文件回调委托方法",
                Type ="Func<IEnumerable<UploadFile>, Task>"
            },
            new EventItem()
            {
                Name = "OnDelete",
                Description="删除文件回调委托方法",
                Type ="Func<string, Task<bool>>"
            }
        };

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            UploadFolderToken?.Dispose();
            ReadAvatarToken?.Cancel();
            ReadToken?.Cancel();
            GC.SuppressFinalize(this);
        }
    }
}
