// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Data;

namespace BootstrapBlazor.Server.Components.Pages;

/// <summary>
/// 在线人数统计
/// </summary>
public partial class Online : IDisposable
{
    [Inject]
    [NotNull]
    private IConnectionService? ConnectionService { get; set; }

    private DynamicObjectContext? DataTableDynamicContext { get; set; }

    private readonly DataTable _table = new();

    private CancellationTokenSource? _cancellationTokenSource = null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        CreateTable();
        BuildContext();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            Task.Run(async () =>
            {
                _cancellationTokenSource = new();
                while (_cancellationTokenSource is { IsCancellationRequested: false })
                {
                    try
                    {
                        await Task.Delay(2000, _cancellationTokenSource.Token);
                        BuildContext();
                        await InvokeAsync(StateHasChanged);
                    }
                    catch { }
                }
            });
        }
    }

    private void CreateTable()
    {
        _table.Columns.Add("ConnectionTime", typeof(DateTimeOffset));
        _table.Columns.Add("LastBeatTime", typeof(DateTimeOffset));
        _table.Columns.Add("Ip", typeof(string));
        _table.Columns.Add("City", typeof(string));
        _table.Columns.Add("OS", typeof(string));
        _table.Columns.Add("Device", typeof(string));
        _table.Columns.Add("Browser", typeof(string));
        _table.Columns.Add("Language", typeof(string));
        _table.Columns.Add("Engine", typeof(string));
        _table.Columns.Add("RequestUrl", typeof(string));
    }

    private void BuildContext()
    {
        _table.Rows.Clear();
        foreach (var item in ConnectionService.Connections)
        {
            _table.Rows.Add(
                item.Id,
                item.ConnectionTime,
                item.LastBeatTime,
                item.ClientInfo?.Ip ?? "",
                item.ClientInfo?.City ?? "",
                item.ClientInfo?.OS ?? "",
                item.ClientInfo?.Device.ToString() ?? "",
                item.ClientInfo?.Browser ?? "",
                item.ClientInfo?.Language ?? "",
                item.ClientInfo?.Engine ?? "",
                item.ClientInfo?.RequestUrl ?? ""
            );
        }
        _table.AcceptChanges();

        //table
        DataTableDynamicContext = new DataTableDynamicContext(_table, (context, col) =>
        {
            if (col.GetFieldName() == "ConnectionTime")
            {
                col.FormatString = "yyyy/MM/dd HH:mm:ss";
                col.Width = 118;
            }
            else if (col.GetFieldName() == "LastBeatTime")
            {
                col.FormatString = "yyyy/MM/dd HH:mm:ss";
                col.Width = 118;
            }
        });
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
