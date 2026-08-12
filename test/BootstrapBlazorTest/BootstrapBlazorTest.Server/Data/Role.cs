using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazorTest.Server.Data
{
    public class Role
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Display(Name = "角色ID")]
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Display(Name ="角色名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string RoleName { get; set; } = string.Empty;
    }
}
