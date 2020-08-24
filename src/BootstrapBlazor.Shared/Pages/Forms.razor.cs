using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Forms
    {
#nullable disable
        private Logger Trace { get; set; }
        private Logger Trace2 { get; set; }
#nullable restore

        private Foo Model = new Foo();

        private class Foo
        {
            [Required]
            [DisplayName("姓名")]
            public string? Name { get; set; }
        }

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

        private Task OnSubmit(EditContext context)
        {
            Trace.Log("OnSubmit 回调委托");
            return Task.CompletedTask;
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

        #region 参数说明
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Model",
                Description = "表单组件绑定的数据模型，必填属性",
                Type = "object",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = "子组件模板实例",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnSubmit",
                Description = "表单提交时的回调委托",
                Type = "EventCallback<EditContext>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnValidSubmit",
                Description = "表单提交时数据合规检查通过时的回调委托",
                Type = "EventCallback<EditContext>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnInvalidSubmit",
                Description = "表单提交时数据合规检查未通过时的回调委托",
                Type = "EventCallback<EditContext>",
                ValueList = " — ",
                DefaultValue = " — "
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
                Name = "Validate",
                Description="表单验证方法",
                Parameters =" — ",
                ReturnValue = "Task<bool>"
            }
        };
        #endregion
    }
}
