// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Data;

internal static class CalendarDemoDataHelper
{
    public static List<Crew> Crews { get; } =
    [
        new("张三", "text-success"),
        new("李四", "text-primary"),
        new("王五", "text-danger")
    ];

    private static Random Random { get; } = new Random();

    public static List<Crew> GetCrewsByDate(DateTime d)
    {
        var count = Random.Next(1, 4);

        var tags = new List<Crew>();
        for (var index = 0; index < count; index++)
        {
            tags.Add(new(Crews[index].Name, Crews[index].Color));
        }
        return tags;
    }
}

/// <summary>
/// 
/// </summary>
public class Crew
{
    /// <summary>
    /// 
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public required string Color { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="color"></param>
    [SetsRequiredMembers]
    public Crew(string name, string color)
    {
        Name = name;
        Color = color;
    }
}
