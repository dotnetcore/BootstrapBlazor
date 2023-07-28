namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Anchors 组件示例
/// </summary>
public partial class Anchors
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Anchors>? Localizer { get; set; }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Target",
            Description = Localizer["Desc1"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Container",
            Description = Localizer["Desc2"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Offset",
            Description = Localizer["Desc3"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = "ChildContent",
            Description = Localizer["Desc4"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
