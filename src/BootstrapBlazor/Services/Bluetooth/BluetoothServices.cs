﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Core.Converter;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Bluetooth 设备服务枚举
/// </summary>
public enum BluetoothServicesEnum
{
    /// <summary>
    /// 通用访问
    /// </summary>
    [JsonPropertyName("generic_access")]
    [BluetoothUuid("00001800-0000-1000-8000-00805f9b34fb")]
    GenericAccess,

    /// <summary>
    /// 通用属性
    /// </summary>
    [JsonPropertyName("generic_attribute")]
    [BluetoothUuid("00001801-0000-1000-8000-00805f9b34fb")]
    GenericAttribute,

    /// <summary>
    /// 即时闹钟
    /// </summary>
    [JsonPropertyName("immediate_alert")]
    [BluetoothUuid("00001802-0000-1000-8000-00805f9b34fb")]
    ImmediateAlert,

    /// <summary>
    /// 连接丢失
    /// </summary>
    [JsonPropertyName("link_loss")]
    [BluetoothUuid("00001803-0000-1000-8000-00805f9b34fb")]
    LinkLoss,

    /// <summary>
    /// 发送功率
    /// </summary>
    [JsonPropertyName("tx_power")]
    [BluetoothUuid("00001804-0000-1000-8000-00805f9b34fb")]
    TxPower,

    /// <summary>
    /// 当前时间
    /// </summary>
    [JsonPropertyName("current_time")]
    [BluetoothUuid("00001805-0000-1000-8000-00805f9b34fb")]
    CurrentTime,

    /// <summary>
    /// 参照时间更新
    /// </summary>
    [JsonPropertyName("reference_time_update")]
    [BluetoothUuid("00001806-0000-1000-8000-00805f9b34fb")]
    ReferenceTimeUpdate,

    /// <summary>
    /// 下个日光节约时间（夏令时）更改
    /// </summary>
    [JsonPropertyName("next_dst_change")]
    [BluetoothUuid("00001807-0000-1000-8000-00805f9b34fb")]
    NextDstChange,

    /// <summary>
    /// 葡萄糖
    /// </summary>
    [JsonPropertyName("glucose")]
    [BluetoothUuid("00001808-0000-1000-8000-00805f9b34fb")]
    Glucose,

    /// <summary>
    /// 温度计
    /// </summary>
    [JsonPropertyName("health_thermometer")]
    [BluetoothUuid("00001809-0000-1000-8000-00805f9b34fb")]
    HealthThermometer,

    /// <summary>
    /// 设备信息
    /// </summary>
    [JsonPropertyName("device_information")]
    [BluetoothUuid("0000180A-0000-1000-8000-00805f9b34fb")]
    DeviceInformation,

    /// <summary>
    /// 心率
    /// </summary>
    [JsonPropertyName("heart_rate")]
    [BluetoothUuid("0000180D-0000-1000-8000-00805f9b34fb")]
    HeartRate,

    /// <summary>
    /// 手机报警状态
    /// </summary>
    [JsonPropertyName("phone_alert_status")]
    [BluetoothUuid("0000180E-0000-1000-8000-00805f9b34fb")]
    PhoneAlertStatus,

    /// <summary>
    /// 电池数据
    /// </summary>
    [JsonPropertyName("battery_service")]
    [BluetoothUuid("0000180F-0000-1000-8000-00805f9b34fb")]
    BatteryService,

    /// <summary>
    /// 血压
    /// </summary>
    [JsonPropertyName("blood_pressure")]
    [BluetoothUuid("00001810-0000-1000-8000-00805f9b34fb")]
    BloodPressure,

    /// <summary>
    /// 闹钟通知
    /// </summary>
    [JsonPropertyName("alert_notification")]
    [BluetoothUuid("00001811-0000-1000-8000-00805f9b34fb")]
    AlertNotification,

    /// <summary>
    /// HID设备
    /// </summary>
    [JsonPropertyName("human_interface_device")]
    [BluetoothUuid("00001812-0000-1000-8000-00805f9b34fb")]
    HumanInterfaceDevice,

    /// <summary>
    /// 扫描参数
    /// </summary>
    [JsonPropertyName("scan_parameters")]
    [BluetoothUuid("00001813-0000-1000-8000-00805f9b34fb")]
    ScanParameters,

    /// <summary>
    /// 跑步速度、节奏
    /// </summary>
    [JsonPropertyName("running_speed_and_cadence")]
    [BluetoothUuid("00001814-0000-1000-8000-00805f9b34fb")]
    RunningSpeedAndCadence,

    /// <summary>
    /// 自动化输入输出
    /// </summary>
    [JsonPropertyName("automation_io")]
    [BluetoothUuid("00001815-0000-1000-8000-00805f9b34fb")]
    AutomationIo,

    /// <summary>
    /// 循环速度、节奏
    /// </summary>
    [JsonPropertyName("cycling_speed_and_cadence")]
    [BluetoothUuid("00001816-0000-1000-8000-00805f9b34fb")]
    CyclingSpeedAndCadence,

    /// <summary>
    /// 骑行能量
    /// </summary>
    [JsonPropertyName("cycling_power")]
    [BluetoothUuid("00001818-0000-1000-8000-00805f9b34fb")]
    CyclingPower,

    /// <summary>
    /// 定位及导航
    /// </summary>
    [JsonPropertyName("location_and_navigation")]
    [BluetoothUuid("00001819-0000-1000-8000-00805f9b34fb")]
    LocationAndNavigation,

    /// <summary>
    /// 环境传感
    /// </summary>
    [JsonPropertyName("environmental_sensing")]
    [BluetoothUuid("0000181A-0000-1000-8000-00805f9b34fb")]
    EnvironmentalSensing,

    /// <summary>
    /// 身体组成
    /// </summary>
    [JsonPropertyName("body_composition")]
    [BluetoothUuid("0000181B-0000-1000-8000-00805f9b34fb")]
    BodyComposition,

    /// <summary>
    /// 用户数据
    /// </summary>
    [JsonPropertyName("user_data")]
    [BluetoothUuid("0000181C-0000-1000-8000-00805f9b34fb")]
    UserData,

    /// <summary>
    /// 体重秤
    /// </summary>
    [JsonPropertyName("weight_scale")]
    [BluetoothUuid("0000181D-0000-1000-8000-00805f9b34fb")]
    WeightScale,

    /// <summary>
    /// 设备绑定管理
    /// </summary>
    [JsonPropertyName("bond_management")]
    [BluetoothUuid("0000181E-0000-1000-8000-00805f9b34fb")]
    BondManagement,

    /// <summary>
    /// 动态血糖检测
    /// </summary>
    [JsonPropertyName("continuous_glucose_monitoring")]
    [BluetoothUuid("0000181F-0000-1000-8000-00805f9b34fb")]
    ContinuousGlucoseMonitoring,

    /// <summary>
    /// 互联网协议支持
    /// </summary>
    [JsonPropertyName("internet_protocol_support")]
    [BluetoothUuid("00001820-0000-1000-8000-00805f9b34fb")]
    InternetProtocolSupport,

    /// <summary>
    /// 室内定位
    /// </summary>
    [JsonPropertyName("indoor_positioning")]
    [BluetoothUuid("00001821-0000-1000-8000-00805f9b34fb")]
    IndoorPositioning,

    /// <summary>
    /// 脉搏血氧计
    /// </summary>
    [JsonPropertyName("pulse_oximeter")]
    [BluetoothUuid("00001822-0000-1000-8000-00805f9b34fb")]
    PulseOximeter,

    /// <summary>
    /// HTTP代理
    /// </summary>
    [JsonPropertyName("http_proxy")]
    [BluetoothUuid("00001823-0000-1000-8000-00805f9b34fb")]
    HttpProxy,

    /// <summary>
    /// 传输发现
    /// </summary>
    [JsonPropertyName("transport_discovery")]
    [BluetoothUuid("00001824-0000-1000-8000-00805f9b34fb")]
    TransportDiscovery,

    /// <summary>
    /// 对象传输
    /// </summary>
    [JsonPropertyName("object_transfer")]
    [BluetoothUuid("00001825-0000-1000-8000-00805f9b34fb")]
    ObjectTransfer,

    /// <summary>
    /// 健康设备
    /// </summary>
    [JsonPropertyName("fitness_machine")]
    [BluetoothUuid("00001826-0000-1000-8000-00805f9b34fb")]
    FitnessMachine,

    /// <summary>
    /// 节点配置
    /// </summary>
    [JsonPropertyName("mesh_provisioning")]
    [BluetoothUuid("00001827-0000-1000-8000-00805f9b34fb")]
    MeshProvisioning,

    /// <summary>
    /// 节点代理
    /// </summary>
    [JsonPropertyName("mesh_proxy")]
    [BluetoothUuid("00001828-0000-1000-8000-00805f9b34fb")]
    MeshProxy,

    /// <summary>
    /// 重连配置
    /// </summary>
    [JsonPropertyName("reconnection_configuration")]
    [BluetoothUuid("00001829-0000-1000-8000-00805f9b34fb")]
    ReconnectionConfiguration
}
