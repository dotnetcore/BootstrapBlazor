using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Samples.Table
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TablesWidth
    {

        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<TablesWidthModel>? items;


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Task OnInitializedAsync()
        {
            base.OnInitializedAsync();
            this.items = Enumerable.Range(0, 50).Select(i => new TablesWidthModel()
            {
                C1 = $"C1 {i} ABCDEFG HIJKLMN",
                C2 = $"C2 {i} ABCDEFG HIJKLMN",
                C3 = $"C3 {i} ABCDEFG HIJKLMN",
                C4 = $"C4 {i} ABCDEFG HIJKLMN",
                C5 = $"C5 {i} ABCDEFG HIJKLMN",
                C6 = $"C6 {i} ABCDEFG HIJKLMN",
                C7 = $"C7 {i} ABCDEFG HIJKLMN",
                C8 = $"C7 {i} ABCDEFG HIJKLMN",
                C9 = $"C9 {i} ABCDEFG HIJKLMN",
                C10 = $"C10 {i} ABCDEFG HIJKLMN",
                C11 = $"C11 {i} ABCDEFG HIJKLMN",
                C12 = $"C12 {i} ABCDEFG HIJKLMN",
                C13 = $"C13 {i} ABCDEFG HIJKLMN",
                C14 = $"C14 {i} ABCDEFG HIJKLMN",
                C15 = $"C15 {i} ABCDEFG HIJKLMN",
                C16 = $"C16 {i} ABCDEFG HIJKLMN",
                C17 = $"C17 {i} ABCDEFG HIJKLMN",
                C18 = $"C18 {i} ABCDEFG HIJKLMN",
                C19 = $"C19 {i} ABCDEFG HIJKLMN",
                C20 = $"C20 {i} ABCDEFG HIJKLMN",
            });
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [AutoGenerateClass(Width = 300, TextEllipsis = true, Align = Alignment.Center)]
    public class TablesWidthModel
    {
        /// <summary>
        /// 
        /// </summary>
        [AutoGenerateColumn(Text = "超长的标题栏啊哈哈哈哈")]
        public string C1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AutoGenerateColumn(Width = 50, Text = "宽50")]
        public string C2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AutoGenerateColumn(Align = Alignment.Right, Text = "右对齐")]
        public string C3 { get; set; }
        public string C4 { get; set; }
        public string C5 { get; set; }
        public string C6 { get; set; }
        public string C7 { get; set; }
        public string C8 { get; set; }
        public string C9 { get; set; }
        public string C10 { get; set; }
        public string C11 { get; set; }
        public string C12 { get; set; }
        public string C13 { get; set; }
        public string C14 { get; set; }
        public string C15 { get; set; }
        public string C16 { get; set; }
        public string C17 { get; set; }

        public string C18 { get; set; }
        public string C19 { get; set; }
        public string C20 { get; set; }

    }
}
