using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EditorDemo2
    {
#nullable disable
        private Logger Trace2 { get; set; }
#nullable restore

        private Dummy DummyModel = new Dummy();

        private class Dummy
        {
            [Required(ErrorMessage = "姓名不可以为空")]
            [DisplayName("姓名")]
            public string? Name { get; set; }

            [Required(ErrorMessage = "年龄不可以为空")]
            [DisplayName("年龄")]
            public int Age { get; set; }

            [DisplayName("生日")]
            public DateTime BirthDay { get; set; } = DateTime.Today.AddYears(-20);

            [Required(ErrorMessage = "请选择一种爱好")]
            [DisplayName("爱好")]
            public IEnumerable<string> Hobby { get; set; } = new List<string>();

            [Required(ErrorMessage = "请选择学历")]
            [DisplayName("学历")]
            public EnumEducation? Education { get; set; }
        }

        private IEnumerable<SelectedItem>? Educations { get; set; }

        private enum EnumEducation
        {
            [Description("小学")]
            Primary,

            [Description("中学")]
            Middel
        }

        private IEnumerable<SelectedItem> Hobbys = new List<SelectedItem>()
        {
            new SelectedItem("游泳", "游泳"),
            new SelectedItem("登山", "登山"),
            new SelectedItem("打球", "打球"),
            new SelectedItem("下棋", "下棋")
        };

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 初始化参数
            var list = new List<SelectedItem>() { new SelectedItem("", "请选择 ...") };
            list.AddRange(typeof(EnumEducation).ToSelectList());

            Educations = list;
        }
        private Task OnValidSubmit(EditContext context)
        {
            Trace2.Log("OnValidSubmit 回调委托");
            return Task.CompletedTask;
        }

        private Task OnInvalidSubmit(EditContext context)
        {
            Trace2.Log("OnInvalidSubmit 回调委托");
            return Task.CompletedTask;
        }
    }
}
