using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BootstrapBlazor.WebConsole.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class Code : ComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Parameter] public string? CodeFile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Inject] IWebHostEnvironment? Environment { get; set; }

        private string? Content()
        {
            string? content = "未设置";
            if (!string.IsNullOrEmpty(CodeFile) && Environment != null)
            {
                // 拼接实例文件路径
                var filePath = Environment.WebRootPath;
                var codeFile = Path.Combine(filePath, $"code{Path.DirectorySeparatorChar}{CodeFile}");
                if (File.Exists(codeFile))
                {
                    content = File.ReadAllText(codeFile);
                }
            }
            return content;
        }

        /// <summary>
        /// BuildRenderTree 方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var index = 0;
            builder.AddContent(index++, Content());
        }
    }
}
