using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BootstrapBlazor.WebConsole.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Code
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
    }
}
