// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Concurrent;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Timelines
/// </summary>
public sealed partial class Timelines
{
    private bool IsReverse { get; set; }

    private IEnumerable<SelectedItem> Items1 { get; set; } = Enumerable.Empty<SelectedItem>();

    private IEnumerable<SelectedItem> Items { get; set; } = Enumerable.Empty<SelectedItem>();

    private readonly ConcurrentQueue<ConsoleMessageItem> _messages = new();

    private readonly CancellationTokenSource _cancelTokenSource = new();

    private readonly AutoResetEvent _locker = new(true);

    private IEnumerable<ConsoleMessageItem> Messages => _messages;

    private static Color GetColor()
    {
        var second = DateTime.Now.Second;
        return (second % 3) switch
        {
            1 => Color.Danger,
            2 => Color.Info,
            _ => Color.None
        };
    }

    private IEnumerable<TimelineItem> GetCustomerComponentTimelineItems() => new TimelineItem[]
    {
        new TimelineItem
        {
            Color = Color.Success,
            Component = BootstrapDynamicComponent.CreateComponent<BootstrapBlazor.Components.Console>(new Dictionary<string, object?>
            {
                [nameof(BootstrapBlazor.Components.Console.Items)] = Messages
            }),
            Description = Localizer["TimelinesDescription1"]
        },
        new TimelineItem
        {
            Color = Color.Info,
            Component = BootstrapDynamicComponent.CreateComponent<Counter>(),
            Description = Localizer["TimelinesDescription2"]
        },
        new TimelineItem
        {
            Color = Color.Warning,
            Component = BootstrapDynamicComponent.CreateComponent<FetchData>(),
            Description = Localizer["TimelinesDescription3"]
        }
    };

    private IEnumerable<TimelineItem> CustomerTimelineItems => new TimelineItem[]
    {
        new TimelineItem
        {
            Content = Localizer["TimelineItemContent4"],
            Description = DateTime.Now.ToString("yyyy-MM-dd")
        },
        new TimelineItem
        {
            Color = Color.Success,
            Content = Localizer["TimelineItemContent5"],
            Description = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd")
        },
        new TimelineItem
        {
            Color = Color.Danger,
            Icon = "fa-solid fa-fw fa-font-awesome",
            Content = Localizer["TimelineItemContent6"],
            Description = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd")
        }
    };

    private IEnumerable<TimelineItem> TimelineItems => new TimelineItem[]
    {
        new TimelineItem
        {
            Content = Localizer["TimelineItemContent1"],
            Description = DateTime.Now.ToString("yyyy-MM-dd")
        },
        new TimelineItem
        {
            Content =  Localizer["TimelineItemContent2"],
            Description = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")
        },
        new TimelineItem
        {
            Content =  Localizer["TimelineItemContent3"],
            Description = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd")
        }
    };

    private readonly IEnumerable<TimelineItem> AlternateTimelineItems = new TimelineItem[]
    {
        new TimelineItem
        {
            Content = "Create a services site 2015-09-01",
        },
        new TimelineItem
        {
            Color = Color.Success,
            Content = "Solve initial network problems 2015-09-01",
        },
        new TimelineItem
        {
            Color = Color.Danger,
            Content = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.",
        },
        new TimelineItem
        {
            Color = Color.Warning,
            Content = "Network problems being solved 2015-09-01",
        },
        new TimelineItem
        {
            Color = Color.Info,
            Content = "Create a services site 2015-09-01",
        }
    };
    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Task OnStateChanged(IEnumerable<SelectedItem> values, SelectedItem value)
    {
        IsReverse = value.Text == Localizer["TimelinesSelectedItem2"];
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Items1 = new SelectedItem[]
        {
            new SelectedItem("1", Localizer["TimelinesSelectedItem1"]) { Active=true },
            new SelectedItem("2", Localizer["TimelinesSelectedItem2"])
        };

        var _ = Task.Run(async () =>
        {
            do
            {
                _locker.WaitOne();
                _messages.Enqueue(new ConsoleMessageItem { Message = $"{DateTimeOffset.Now}: Dispatch Message", Color = GetColor() });

                if (_messages.Count > 8)
                {
                    _messages.TryDequeue(out var _);
                }
                await InvokeAsync(StateHasChanged);
                _locker.Set();
                await Task.Delay(2000, _cancelTokenSource.Token);
            }
            while (!_cancelTokenSource.IsCancellationRequested);
        });

        Items = new SelectedItem[]
        {
            new SelectedItem("1", Localizer["TimelinesSelectedItem1"]) { Active=true },
            new SelectedItem("2", Localizer["TimelinesSelectedItem2"])
        };
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new() {
            Name = "Items",
            Description = Localizer["TimelinesItems"],
            Type = "IEnumerable<TimelineItem>",
            ValueList = "—",
            DefaultValue = " — "
        },
        new() {
            Name = "IsReverse",
            Description = Localizer["TimelinesIsReverse"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsLeft",
            Description = Localizer["TimelinesIsLeft"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsAlternate",
            Description = Localizer["TimelinesIsAlternate"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetTimelineItemAttributes() => new AttributeItem[]
    {
        new() {
            Name = nameof(TimelineItem.Color),
            Description = Localizer["TimelinesColor"],
            Type = "Color",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(TimelineItem.Content),
            Description = Localizer["TimelinesContent"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(TimelineItem.Icon),
            Description = Localizer["TimelinesIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(TimelineItem.Description),
            Description = Localizer["TimelinesDescription"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(TimelineItem.Component),
            Description = Localizer["TimelinesComponent"],
            Type = nameof(BootstrapDynamicComponent),
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
