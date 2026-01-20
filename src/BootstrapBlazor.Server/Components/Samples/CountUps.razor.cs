// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// CountUps
/// </summary>
public partial class CountUps
{
    private static readonly CountUpOption _option = new() { DecimalPlaces = 2 };

    private double Value2 { get; set; }

    private double Value { get; set; }

    private ConsoleLogger? _logger;

    private bool _useOnCompleted;

    private readonly List<SelectedItem> _items = [];

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        _items.Add(new SelectedItem("", "Default (\"1234\")"));
        _items.Add(new SelectedItem("1", "Eastern Arabic (\"١٢٣٤\")"));
        OnUpdate();
    }

    private void OnUpdate()
    {
        Value = Value2;
    }

    private Task OnCompleted()
    {
        if (_useOnCompleted)
        {
            _logger?.Log($"{DateTime.Now}: from {_option.StartValue} to {Value2}");
        }
        return Task.CompletedTask;
    }

    private Task OnSelectedItemChanged(SelectedItem item)
    {
        var index = _items.IndexOf(item);
        if (index == 0)
        {
            _option.Numerals = null;
        }
        else if (index == 1)
        {
            _option.Numerals = ['٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩'];
        }
        return Task.CompletedTask;
    }
}
