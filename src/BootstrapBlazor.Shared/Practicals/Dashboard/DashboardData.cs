namespace BootstrapBlazor.Shared.Practicals.Dashboard;

/// <summary>
/// 仪表盘数据
/// </summary>
public class DashboardData
{
    /// <summary>
    /// 检验数量全部统计
    /// </summary>
    public int TestAllCount { get; set; }

    /// <summary>
    /// 检验数量年统计
    /// </summary>
    public int TestYearCount { get; set; }

    /// <summary>
    /// 检验数量月统计
    /// </summary>
    public int TestMonthCount { get; set; }

    /// <summary>
    /// 检验数量日统计
    /// </summary>
    public int TestDayCount { get; set; }

    /// <summary>
    /// 日检验签发数量
    /// </summary>
    public int TestApprovedDayCount { get; set; }

    /// <summary>
    /// 日检验签发占比
    /// </summary>
    public double TestApprovedDayScale { get; set; }

    /// <summary>
    /// 月检验签发数量
    /// </summary>
    public int TestApprovedMonthCount { get; set; }

    /// <summary>
    /// 月检验签发占比
    /// </summary>
    public double TestApprovedMonthScale { get; set; }

    /// <summary>
    /// 年检验签发数量
    /// </summary>
    public int TestApprovedYearCount { get; set; }

    /// <summary>
    /// 年检验签发占比
    /// </summary>
    public double TestApprovedYearScale { get; set; }

    /// <summary>
    /// 全部检验签发数量
    /// </summary>
    public int TestApprovedAllCount { get; set; }

    /// <summary>
    /// 全部检验签发占比
    /// </summary>
    public double TestApprovedAllScale { get; set; }

    /// <summary>
    /// 当年月分组统计
    /// </summary>
    public List<TestDayGroupData> TestDayGroupList { get; set; } = new();

    /// <summary>
    /// KKS分类分组统计
    /// </summary>
    public List<TestKKSGroupData> TestKKSGroupList { get; set; } = new();
}

/// <summary>
/// TestDayGroupData
/// </summary>
public class TestDayGroupData
{
    public int Key { get; set; }

    public int Count { get; set; }
}

/// <summary>
/// TestKKSGroupData
/// </summary>
public class TestKKSGroupData
{
    public string KKS { get; set; } = string.Empty;

    public string NAM { get; set; } = string.Empty;

    public int Count { get; set; }

    public int ApprovedCount { get; set; }

    public double Percent { get; set; }
}
