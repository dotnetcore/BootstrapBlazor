// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

    [Inject]
    [NotNull]
    private IWebClientService? WebClientService { get; set; }

    private DynamicObjectContext? DataTableDynamicContext { get; set; }

    private readonly DataTable _table = new();

    private CancellationTokenSource? _cancellationTokenSource;

    private string? _clientId;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        CreateTable();
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
                var client = await WebClientService.GetClientInfo();
                _clientId = client.Id;
                _cancellationTokenSource ??= new CancellationTokenSource();
                while (_cancellationTokenSource is { IsCancellationRequested: false })
                {
                    try
                    {
                        BuildContext();
                        await InvokeAsync(StateHasChanged);
                        await Task.Delay(10000, _cancellationTokenSource.Token);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            });
        }
    }

    private void CreateTable()
    {
        _table.Columns.Add("Id", typeof(string));
        _table.Columns.Add("ConnectionTime", typeof(DateTimeOffset));
        _table.Columns.Add("LastBeatTime", typeof(DateTimeOffset));
        _table.Columns.Add("Dur", typeof(TimeSpan));
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
        var rows = ConnectionService.Connections.Sort(["ConnectionTime"]);
        foreach (var item in rows)
        {
            _table.Rows.Add(
                item.Id,
                item.ConnectionTime,
                item.LastBeatTime,
                item.LastBeatTime - item.ConnectionTime,
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
            col.Text = Localizer[col.GetFieldName()];
            if (col.GetFieldName() == "Id")
            {
                col.Ignore = true;
            }
            else if (col.GetFieldName() == "ConnectionTime")
            {
                col.FormatString = "yyyy/MM/dd HH:mm:ss";
                col.Width = 118;
            }
            else if (col.GetFieldName() == "LastBeatTime")
            {
                col.FormatString = "yyyy/MM/dd HH:mm:ss";
                col.Width = 118;
            }
            else if (col.GetFieldName() == "Dur")
            {
                col.FormatString = @"dd\.hh\:mm\:ss";
                col.Width = 54;
            }
            else if (col.GetFieldName() == "Ip")
            {
                col.Template = v => builder => builder.AddContent(0, FormatIp(v));
            }
            else if (col.GetFieldName() == "RequestUrl")
            {
                col.Template = v => builder =>
                {
                    if (v is IDynamicObject val)
                    {
                        var url = val.GetValue("RequestUrl")?.ToString();
                        if (!string.IsNullOrEmpty(url))
                        {
                            builder.AddContent(0, new MarkupString($"<a href=\"{url}\" target=\"_blank\">{url}</a>"));
                        }
                    }
                };
            }
        });
    }

    private static string FormatIp(object v)
    {
        var ret = "";
        if (v is IDynamicObject val)
        {
            var ip = val.GetValue("Ip")?.ToString();
            if (!string.IsNullOrEmpty(ip))
            {
                ret = ip.MaskIpString();
            }
        }
        return ret;
    }

    private string? SetRowClassFormatter(DynamicObject context)
    {
        var id = context.GetValue("id")?.ToString();
        return _clientId == id ? "active" : null;
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
