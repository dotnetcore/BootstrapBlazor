// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Data;

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
