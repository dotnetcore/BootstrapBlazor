namespace BootstrapBlazor.Shared.Samples.Practices;

/// <summary>
/// 仪表盘数据
/// </summary>
public class DashboardData
{
    /// <summary>
    /// 检验数量全部统计
    /// </summary>
    public int TestAllCount { get; init; }

    /// <summary>
    /// 检验数量年统计
    /// </summary>
    public int TestYearCount { get; init; }

    /// <summary>
    /// 检验数量月统计
    /// </summary>
    public int TestMonthCount { get; init; }

    /// <summary>
    /// 检验数量日统计
    /// </summary>
    public int TestDayCount { get; init; }

    /// <summary>
    /// 日检验签发数量
    /// </summary>
    public int TestApprovedDayCount { get; init; }

    /// <summary>
    /// 日检验签发占比
    /// </summary>
    public double TestApprovedDayScale { get; set; }

    /// <summary>
    /// 月检验签发数量
    /// </summary>
    public int TestApprovedMonthCount { get; init; }

    /// <summary>
    /// 月检验签发占比
    /// </summary>
    public double TestApprovedMonthScale { get; set; }

    /// <summary>
    /// 年检验签发数量
    /// </summary>
    public int TestApprovedYearCount { get; init; }

    /// <summary>
    /// 年检验签发占比
    /// </summary>
    public double TestApprovedYearScale { get; set; }

    /// <summary>
    /// 全部检验签发数量
    /// </summary>
    public int TestApprovedAllCount { get; init; }

    /// <summary>
    /// 全部检验签发占比
    /// </summary>
    public double TestApprovedAllScale { get; set; }

    /// <summary>
    /// 当年月分组统计
    /// </summary>
    public List<TestDayGroupData> TestDayGroupList { get; init; } = new();

    /// <summary>
    /// KKS分类分组统计
    /// </summary>
    public List<TestKKSGroupData> TestKKSGroupList { get; init; } = new();
}

/// <summary>
/// TestDayGroupData
/// </summary>
public class TestDayGroupData
{
    /// <summary>
    /// Key
    /// </summary>
    public int Key { get; init; }

    /// <summary>
    /// Count
    /// </summary>
    public int Count { get; init; }
}

/// <summary>
/// TestKKSGroupData
/// </summary>
public class TestKKSGroupData
{
    /// <summary>
    /// KKS
    /// </summary>
    public string KKS { get; init; } = string.Empty;

    /// <summary>
    /// NAM
    /// </summary>
    public string NAM { get; init; } = string.Empty;

    /// <summary>
    /// Count
    /// </summary>
    public int Count { get; init; }

    /// <summary>
    /// ApprovedCount
    /// </summary>
    public int ApprovedCount { get; set; }

    /// <summary>
    /// Percent
    /// </summary>
    public double Percent { get; set; }
}
