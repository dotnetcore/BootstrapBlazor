using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SweetAlertBody
    {
        /// <summary>
        /// 获得/设置 弹窗类别默认为 Success
        /// </summary>
        [Parameter]
        public SwalCategory Category { get; set; }

        /// <summary>
        /// 获得/设置 显示标题
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "";

        /// <summary>
        /// 获得/设置 关闭按钮回调方法
        /// </summary>
        [Parameter]
        public Action? OnClose { get; set; }

        private string? IconClassString => CssBuilder.Default("swal2-icon")
            .AddClass("swal2-success swal2-animate-success-icon", Category == SwalCategory.Success)
            .AddClass("swal2-error swal2-animate-error-icon", Category == SwalCategory.Error)
            .AddClass("swal2-info", Category == SwalCategory.Information)
            .AddClass("swal2-question", Category == SwalCategory.Question)
            .AddClass("swal2-warning", Category == SwalCategory.Warning)
            .Build();

        /// <summary>
        /// 将配置信息转化为参数集合
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        internal static IEnumerable<KeyValuePair<string, object>> Parse(SwalOption option) => new List<KeyValuePair<string, object>>()
        {
            new KeyValuePair<string, object>(nameof(SweetAlertBody.Category) , option.Category),
            new KeyValuePair<string, object>(nameof(SweetAlertBody.Title) , option.Title),
            new KeyValuePair<string, object>(nameof(SweetAlertBody.OnClose) , new Action(() =>option.Dialog?.Toggle())),
        };
    }
}
