using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class Dummy
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "姓名不可以为空")]
        [DisplayName("姓名")]
        [EditorOrder(1)]
        public string? Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "年龄不可以为空")]
        [DisplayName("年龄")]
        [EditorOrder(2)]
        public int Age { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("生日")]
        [EditorOrder(3)]
        public DateTime BirthDay { get; set; } = DateTime.Today.AddYears(-20);

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "请选择一种爱好")]
        [DisplayName("爱好")]
        [EditorOrder(5)]
        public IEnumerable<string> Hobby { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "请选择学历")]
        [DisplayName("学历")]
        [EditorOrder(4)]
        public EnumEducation? Education { get; set; }
    }
}
