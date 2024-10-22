﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 蓝牙设备信息
/// </summary>
public class BluetoothDeviceInfo
{
    /// <summary>
    /// 获得/设置 ManufacturerName
    /// </summary>
    public string? ManufacturerName { get; set; }

    /// <summary>
    /// 获得/设置 ModelNumber
    /// </summary>
    public string? ModelNumber { get; set; }

    /// <summary>
    /// 获得/设置 HardwareRevision
    /// </summary>
    public string? HardwareRevision { get; set; }

    /// <summary>
    /// 获得/设置 FirmwareRevision
    /// </summary>
    public string? FirmwareRevision { get; set; }

    /// <summary>
    /// 获得/设置 SoftwareRevision
    /// </summary>
    public string? SoftwareRevision { get; set; }

    /// <summary>
    /// 获得/设置 SystemId
    /// </summary>
    public SystemId? SystemId { get; set; }

    /// <summary>
    /// 获得/设置 IEEERegulatoryCertificationDataList
    /// </summary>
    public string? IEEERegulatoryCertificationDataList { get; set; }

    /// <summary>
    /// 获得/设置 PnPID
    /// </summary>
    public PnPID? PnPID { get; set; }
}

/// <summary>
/// SystemId 类
/// </summary>
public class SystemId
{
    /// <summary>
    /// 获得/设置 ManufacturerIdentifier
    /// </summary>
    public string? ManufacturerIdentifier { get; set; }

    /// <summary>
    /// 获得/设置 OrganizationallyUniqueIdentifier
    /// </summary>
    public string? OrganizationallyUniqueIdentifier { get; set; }
}

/// <summary>
/// PnPID 类
/// </summary>
public class PnPID
{
    /// <summary>
    /// 获得/设置 VendorIdSource
    /// </summary>
    public string? VendorIdSource { get; set; }

    /// <summary>
    /// 获得/设置 ProductId
    /// </summary>
    public string? ProductId { get; set; }

    /// <summary>
    /// 获得/设置 ProductVersion
    /// </summary>
    public string? ProductVersion { get; set; }
}
