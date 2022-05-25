// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace BootstrapBlazor.Shared.Services;

internal class FanControllerDataService : IDisposable
{
    public Func<IEnumerable<TopologyItem>, Task>? OnDataChange { get; set; }

    private static readonly Random random = new();

    private static string GenerateFanValue() => random.Next(1000, 1200).ToString();

    public bool IsOpen { get; private set; } = true;

    private CancellationTokenSource GeneratorCancellationToken { get; } = new CancellationTokenSource();

    public FanControllerDataService()
    {
        DoWork();
    }

    private void DoWork() => Task.Run(async () =>
    {
        try
        {
            while (!GeneratorCancellationToken.IsCancellationRequested)
            {
                if (IsOpen)
                {
                    var data = new TopologyItem[]
                    {
                        new()
                        {
                            ID = "77f220c7",
                            ShowChild = 0,
                        },
                        new()
                        {
                            ID = "b45ab55",
                            Text = GenerateFanValue()
                        }
                    };
                    if (OnDataChange != null)
                    {
                        await OnDataChange(data);
                    }
                }
                await Task.Delay(2000, GeneratorCancellationToken.Token);
            }
        }
        catch (TaskCanceledException)
        {

        }
    });

    public IEnumerable<TopologyItem> GetDatas()
    {
        var data = new List<TopologyItem>()
        {
            new TopologyItem()
            {
                ID = "77f220c7",
                ShowChild = 1,
            },
            new TopologyItem()
            {
                ID = "b45ab55",
                Text = GenerateFanValue()
            }
        };
        return data;
    }

    public async Task UpdateStatus(bool open)
    {
        IsOpen = open;
        var data = new TopologyItem[]
        {
            new()
            {
                ID = "77f220c7",
                ShowChild = 1,
            },
            new()
            {
                ID = "b45ab55",
                Text = "0"
            }
        };
        if (OnDataChange != null)
        {
            await OnDataChange(data);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            GeneratorCancellationToken.Cancel();
            GeneratorCancellationToken.Dispose();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
