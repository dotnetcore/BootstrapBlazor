// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">蓝牙设备信息</para>
/// <para lang="en">Bluetooth Device Info</para>
/// </summary>
public class BluetoothDeviceInfo
{
    /// <summary>
    /// <para lang="zh">获得/设置 ManufacturerName</para>
    /// <para lang="en">Gets or sets ManufacturerName</para>
    /// </summary>
    public string? ManufacturerName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ModelNumber</para>
    /// <para lang="en">Gets or sets ModelNumber</para>
    /// </summary>
    public string? ModelNumber { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 HardwareRevision</para>
    /// <para lang="en">Gets or sets HardwareRevision</para>
    /// </summary>
    public string? HardwareRevision { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 FirmwareRevision</para>
    /// <para lang="en">Gets or sets FirmwareRevision</para>
    /// </summary>
    public string? FirmwareRevision { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 SoftwareRevision</para>
    /// <para lang="en">Gets or sets SoftwareRevision</para>
    /// </summary>
    public string? SoftwareRevision { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 SystemId</para>
    /// <para lang="en">Gets or sets SystemId</para>
    /// </summary>
    public SystemId? SystemId { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 IEEERegulatoryCertificationDataList</para>
    /// <para lang="en">Gets or sets IEEERegulatoryCertificationDataList</para>
    /// </summary>
    public string? IEEERegulatoryCertificationDataList { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 PnPID</para>
    /// <para lang="en">Gets or sets PnPID</para>
    /// </summary>
    public PnPID? PnPID { get; set; }
}

/// <summary>
/// <para lang="zh">SystemId 类</para>
/// <para lang="en">SystemId Class</para>
/// </summary>
public class SystemId
{
    /// <summary>
    /// <para lang="zh">获得/设置 ManufacturerIdentifier</para>
    /// <para lang="en">Gets or sets ManufacturerIdentifier</para>
    /// </summary>
    public string? ManufacturerIdentifier { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 OrganizationallyUniqueIdentifier</para>
    /// <para lang="en">Gets or sets OrganizationallyUniqueIdentifier</para>
    /// </summary>
    public string? OrganizationallyUniqueIdentifier { get; set; }
}

/// <summary>
/// <para lang="zh">PnPID 类</para>
/// <para lang="en">PnPID Class</para>
/// </summary>
public class PnPID
{
    /// <summary>
    /// <para lang="zh">获得/设置 VendorIdSource</para>
    /// <para lang="en">Gets or sets VendorIdSource</para>
    /// </summary>
    public string? VendorIdSource { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ProductId</para>
    /// <para lang="en">Gets or sets ProductId</para>
    /// </summary>
    public string? ProductId { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ProductVersion</para>
    /// <para lang="en">Gets or sets ProductVersion</para>
    /// </summary>
    public string? ProductVersion { get; set; }
}
