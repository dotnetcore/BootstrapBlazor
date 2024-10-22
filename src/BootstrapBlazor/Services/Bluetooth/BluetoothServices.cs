// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Core.Converter;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Bluetooth 设备服务枚举
/// </summary>
[JsonEnumConverter]
public enum BluetoothServices
{
    /// <summary>
    /// 通用访问
    /// </summary>
    [JsonPropertyName("generic_access")]
    GenericAccess,

    /// <summary>
    /// 通用属性
    /// </summary>
    [JsonPropertyName("generic_attribute")]
    GenericAttribute,

    /// <summary>
    /// 即时闹钟	
    /// </summary>
    [JsonPropertyName("immediate_alert")]
    ImmediateAlert,

    /// <summary>
    /// 连接丢失	
    /// </summary>
    [JsonPropertyName("link_loss")]
    LinkLoss,

    /// <summary>
    /// 发送功率
    /// </summary>
    [JsonPropertyName("tx_power")]
    TXPower,

    /// <summary>
    /// 当前时间	
    /// </summary>
    [JsonPropertyName("current_time")]
    CurrentTime,

    /// <summary>
    /// 参照时间更新
    /// </summary>
    [JsonPropertyName("reference_time_update")]
    ReferenceTimeUpdate,

    /// <summary>
    /// 下个日光节约时间（夏令时）更改
    /// </summary>
    [JsonPropertyName("next_dst_change")]
    NextDstChange,

    /// <summary>
    /// 葡萄糖
    /// </summary>
    [JsonPropertyName("glucose")]
    Glucose,

    /// <summary>
    /// 温度计
    /// </summary>
    [JsonPropertyName("health_thermometer")]
    HealthThermometer,

    /// <summary>
    /// 设备信息
    /// </summary>
    [JsonPropertyName("device_information")]
    DeviceInformation,

    /// <summary>
    /// 心率	
    /// </summary>
    [JsonPropertyName("heart_rate")]
    HeartRate,

    /// <summary>
    /// 手机报警状态
    /// </summary>
    [JsonPropertyName("phone_alert_status")]
    PhoneAlertStatus,

    /// <summary>
    /// 电池数据
    /// </summary>
    [JsonPropertyName("battery_service")]
    BatteryService,

    /// <summary>
    /// 血压
    /// </summary>
    [JsonPropertyName("blood_pressure")]
    BloodPressure,

    /// <summary>
    /// 闹钟通知
    /// </summary>
    [JsonPropertyName("alert_notification")]
    AlertNotification,

    /// <summary>
    /// HID设备
    /// </summary>
    [JsonPropertyName("human_interface_device")]
    HumanInterfaceDevice,

    /// <summary>
    /// 扫描参数
    /// </summary>
    [JsonPropertyName("scan_parameters")]
    ScanParameters,

    /// <summary>
    /// 跑步速度、节奏
    /// </summary>
    [JsonPropertyName("running_speed_and_cadence")]
    RunningSpeedAndCadence,

    /// <summary>
    /// 自动化输入输出
    /// </summary>
    [JsonPropertyName("automation_io")]
    AutomationIO,

    /// <summary>
    /// 循环速度、节奏
    /// </summary>
    [JsonPropertyName("cycling_speed_and_cadence")]
    CyclingSpeedAndCadence,

    /// <summary>
    /// 骑行能量	
    /// </summary>
    [JsonPropertyName("cycling_power")]
    CyclingPower,

    /// <summary>
    /// 定位及导航
    /// </summary>
    [JsonPropertyName("location_and_navigation")]
    LocationAndNavigation,

    /// <summary>
    /// 环境传感
    /// </summary>
    [JsonPropertyName("environmental_sensing")]
    EnvironmentalSensing,

    /// <summary>
    /// 身体组成
    /// </summary>
    [JsonPropertyName("body_composition")]
    BodyComposition,

    /// <summary>
    /// 用户数据
    /// </summary>
    [JsonPropertyName("user_data")]
    UserData,

    /// <summary>
    /// 体重秤
    /// </summary>
    [JsonPropertyName("weight_scale")]
    WeightScale,

    /// <summary>
    /// 设备绑定管理
    /// </summary>
    [JsonPropertyName("bond_management")]
    BondManagement,

    /// <summary>
    /// 动态血糖检测	
    /// </summary>
    [JsonPropertyName("continuous_glucose_monitoring")]
    ContinuousGlucoseMonitoring,

    /// <summary>
    /// 互联网协议支持
    /// </summary>
    [JsonPropertyName("internet_protocol_support")]
    InternetProtocolSupport,

    /// <summary>
    /// 室内定位
    /// </summary>
    [JsonPropertyName("indoor_positioning")]
    IndoorPositioning,

    /// <summary>
    /// 脉搏血氧计
    /// </summary>
    [JsonPropertyName("pulse_oximeter")]
    PulseOximeter,

    /// <summary>
    /// HTTP代理
    /// </summary>
    [JsonPropertyName("http_proxy")]
    HttpProxy,

    /// <summary>
    /// 传输发现
    /// </summary>
    [JsonPropertyName("transport_discovery")]
    TransportDiscovery,

    /// <summary>
    /// 对象传输
    /// </summary>
    [JsonPropertyName("object_transfer")]
    ObjectTransfer,

    /// <summary>
    /// 健康设备
    /// </summary>
    [JsonPropertyName("fitness_machine")]
    FitnessMachine,

    /// <summary>
    /// 节点配置
    /// </summary>
    [JsonPropertyName("mesh_provisioning")]
    MeshProvisioning,

    /// <summary>
    /// 节点代理
    /// </summary>
    [JsonPropertyName("mesh_proxy")]
    MeshProxy,

    /// <summary>
    /// 重连配置
    /// </summary>
    [JsonPropertyName("reconnection_configuration")]
    ReconnectionConfiguration
}
