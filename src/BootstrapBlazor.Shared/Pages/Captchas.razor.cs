using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class Captchas
    {
        /// <summary>
        /// 获得/设置 图床路径 默认值为 images
        /// </summary>
        public string ImagesPath { get; set; } = "_content/BootstrapBlazor.Shared/images";

        /// <summary>
        /// 获得/设置 图床路径 默认值为 Pic.jpg
        /// </summary>
        public string ImagesName { get; set; } = "Pic.jpg";

        /// <summary>
        /// 
        /// </summary>
        private Captcha? Captcha { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        private void OnValid(bool ret)
        {
            var result = ret ? "成功" : "失败";
            Trace?.Log($"验证码结果 -> {result}");
            if (ret)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    Captcha?.Reset();
                });
            }
        }

        private static Random ImageRandomer { get; set; } = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetImageName()
        {
            var index = Convert.ToInt32(ImageRandomer.Next(0, 8) / 1.0);
            var imageName = Path.GetFileNameWithoutExtension(ImagesName);
            var extendName = Path.GetExtension(ImagesName);
            var fileName = $"{imageName}{index}{extendName}";
            return Path.Combine(ImagesPath, fileName);
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "ImagesPath",
                Description = "图床路径",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "images"
            },
            new AttributeItem() {
                Name = "ImagesName",
                Description = "滑块背景图文件名称",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "Pic.jpg"
            },
            new AttributeItem() {
                Name = "HeaderText",
                Description = "组件 Header 显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "请完成安全验证"
            },
            new AttributeItem() {
                Name = "BarText",
                Description = "拖动滑块显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "向右滑动填充拼图"
            },
            new AttributeItem() {
                Name = "FailedText",
                Description = "背景图加载失败显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "加载失败"
            },
            new AttributeItem() {
                Name = "LoadText",
                Description = "背景图加载时显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "正在加载 ..."
            },
            new AttributeItem() {
                Name = "TryText",
                Description = "拼图失败滑块显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "再试一次"
            },
            new AttributeItem() {
                Name = "Offset",
                Description = "拼图对齐偏移量",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "5"
            },
            new AttributeItem() {
                Name = "Width",
                Description = "拼图宽度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "280"
            },
            new AttributeItem() {
                Name = "Height",
                Description = "拼图高度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "155"
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
                Name = "OnValid",
                Description="滑块验证码进行验证结果判断后回调此方法",
                Type ="Action<bool>"
            }
        };

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
        {
            new MethodItem()
            {
                Name = "GetImageName",
                Description="自定义获取背景图文件名称方法",
                Parameters =" — ",
                ReturnValue = "string"
            }
        };
    }
}
