// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Services;

internal class WeatherForecastService
{
    private string[] Summaries { get; } = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="startDate"></param>
    /// <returns></returns>
    public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
    {
        var rng = new Random();
        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        }).ToArray());
    }
}
