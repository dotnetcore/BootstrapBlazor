using BootstrapBlazor.Components;
using FreeSql.DataAnnotations;
using Magicodes.ExporterAndImporter.Excel;
using OfficeOpenXml.Table;
using System.ComponentModel;

namespace b03sqlite.Data;

[ExcelImporter(IsLabelingError = true)]
[ExcelExporter(Name = "导入商品中间表", TableStyle = TableStyles.Light10, AutoFitAllColumn = true)]
[AutoGenerateClass(Searchable = true, Filterable = true, Sortable = true)]
public class WeatherForecast
{
    [Column(IsIdentity = true)]
    [DisplayName("序号")]
    public int ID { get; set; }

    [DisplayName("日期")]
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }


    [DisplayName("未审批/价格隐藏")]
    [NullableBoolItems(NullValueDisplayText = "未设置", TrueValueDisplayText = "价格隐藏", FalseValueDisplayText = "未审批")]
    public bool? HidePrice { get => hidePrice ?? false; set => hidePrice = value; }
    bool? hidePrice = true;

    [DisplayName("显示库存")]
    [NullableBoolItems(NullValueDisplayText = "请选择 ...", TrueValueDisplayText = "已盘库", FalseValueDisplayText = "未盘库")]
    public bool? ShowStock { get; set; }
    
    [DisplayName("未审批/价格隐藏2")]
    public bool? HidePrice2 { get => hidePrice ?? false; set => hidePrice = value; }
 
    [DisplayName("显示库存4")]
    [AutoGenerateColumn(ComponentType =typeof(NullSwitch))]
    [DefaultValue(false)]
    public bool? ShowStock4 { get; set; }
 
    [DisplayName("显示库存5")]
    [NullableBoolItems(NullValueDisplayText = "请选择 ...", TrueValueDisplayText = "已盘库", FalseValueDisplayText = "未盘库")]
    [AutoGenerateColumn(ComponentType =typeof(NullSwitch))]
    [DefaultValue(true)]
    public bool? ShowStock5 { get; set; }
    
    [DisplayName("显示库存2")]
    public bool ShowStock2 { get; set; }

}
