namespace BootstrapBlazor.Shared.Practicals.Dashboard;

/// <summary>
/// 仪表盘数据提供服务
/// </summary>
public class DashboardService
{
    private readonly Random _random;

    /// <summary>
    /// 仪表盘数据提供服务
    /// </summary>
    public DashboardService()
    {
        _random = new Random();
    }

    /// <summary>
    /// 获取仪表盘数据
    /// </summary>
    /// <returns></returns>
    public async Task<DashboardData> GetDashboardDataAsync(DateTime dateTime)
    {
        //月初
        var startOfMonthTime = new DateTime(dateTime.Year, dateTime.Month, 1);
        //月底
        var edt = dateTime.AddDays(1 - dateTime.Day).AddMonths(1).AddDays(-1);
        var endOfMonthTime = new DateTime(edt.Year, edt.Month, edt.Day, 23, 59, 59);
        //年初
        var startOfYearTime = new DateTime(dateTime.Year, 1, 1);
        //年底
        var endOfYearTime = new DateTime(dateTime.Year, 12, 31);

        //填充随机数据，仅做展示用
        var data = new DashboardData
        {
            TestDayCount = _random.Next(10, 99),
            TestMonthCount = _random.Next(100, 999),
            TestYearCount = _random.Next(1000, 4999),
            TestAllCount = _random.Next(5000, 9999),

            TestApprovedDayCount = _random.Next(10, 59),
            TestApprovedMonthCount = _random.Next(100, 499),
            TestApprovedYearCount = _random.Next(1000, 2999),
            TestApprovedAllCount = _random.Next(4000, 4999),

            TestDayGroupList = await GetDayOfMonthGroupAsync(startOfMonthTime, endOfMonthTime),
            TestKKSGroupList = await GetTestKKSGroupAsync(startOfMonthTime, endOfMonthTime)
        };

        //日签发占比
        var dayScale = Math.Round(data.TestApprovedDayCount / (double)data.TestDayCount * 100, 1);
        data.TestApprovedDayScale = dayScale is double.NaN ? 0 : dayScale;

        //月签发占比
        var monthScale = Math.Round(data.TestApprovedMonthCount / (double)data.TestMonthCount * 100, 1);
        data.TestApprovedMonthScale = monthScale is double.NaN ? 0 : monthScale;

        //年签发占比
        var yearScale = Math.Round(data.TestApprovedYearCount / (double)data.TestYearCount * 100, 1);
        data.TestApprovedYearScale = yearScale is double.NaN ? 0 : yearScale;

        //全部签发占比
        var allScale = Math.Round(data.TestApprovedAllCount / (double)data.TestAllCount * 100, 1);
        data.TestApprovedAllScale = allScale is double.NaN ? 0 : allScale;

        return data;
    }

    /// <summary>
    /// 按年月日分组统计
    /// </summary>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    private async Task<List<TestDayGroupData>> GetDayOfMonthGroupAsync(DateTime startTime, DateTime endTime)
    {
        var result = new List<TestDayGroupData>();

        //按照当前月的每一天填充数据
        for (int i = 1; i <= endTime.Day; i++)
        {
            result.Add(new TestDayGroupData() { Key = i, Count = _random.Next(1, 99) });
        }

        //重新排序
        result = result.OrderBy(x => x.Key).ToList();

        return await Task.FromResult(result);
    }

    /// <summary>
    /// KKS分类分组统计
    /// </summary>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    private async Task<List<TestKKSGroupData>> GetTestKKSGroupAsync(DateTime startTime, DateTime endTime)
    {
        var result = new List<TestKKSGroupData>();

        for (int i = 0; i < 20; i++)
        {
            result.Add(new TestKKSGroupData()
            {
                NAM = $"Blazor",
                KKS = $"Bootstrap",
                Count = _random.Next(10, 99),
                Percent = _random.Next(1, 99)
            });
        }

        foreach (var item in result)
        {
            item.ApprovedCount = item.Count * (int)item.Percent / 100;
        }

        return await Task.FromResult(result.OrderByDescending(x => x.Percent).ToList());
    }
}
