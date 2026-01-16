// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Bluetooth 设备服务枚举</para>
/// <para lang="en">Bluetooth Device Service Enum</para>
/// </summary>
public enum BluetoothServicesEnum
{
    /// <summary>
    /// <para lang="zh">通用访问</para>
    /// <para lang="en">Generic Access</para>
    /// </summary>
    [JsonPropertyName("generic_access")]
    [BluetoothUUID("00001800-0000-1000-8000-00805f9b34fb")]
    GenericAccess,

    /// <summary>
    /// <para lang="zh">通用属性</para>
    /// <para lang="en">Generic Attribute</para>
    /// </summary>
    [JsonPropertyName("generic_attribute")]
    [BluetoothUUID("00001801-0000-1000-8000-00805f9b34fb")]
    GenericAttribute,

    /// <summary>
    /// <para lang="zh">即时闹钟</para>
    /// <para lang="en">Immediate Alert</para>
    /// </summary>
    [JsonPropertyName("immediate_alert")]
    [BluetoothUUID("00001802-0000-1000-8000-00805f9b34fb")]
    ImmediateAlert,

    /// <summary>
    /// <para lang="zh">连接丢失</para>
    /// <para lang="en">Link Loss</para>
    /// </summary>
    [JsonPropertyName("link_loss")]
    [BluetoothUUID("00001803-0000-1000-8000-00805f9b34fb")]
    LinkLoss,

    /// <summary>
    /// <para lang="zh">发送功率</para>
    /// <para lang="en">Tx Power</para>
    /// </summary>
    [JsonPropertyName("tx_power")]
    [BluetoothUUID("00001804-0000-1000-8000-00805f9b34fb")]
    TxPower,

    /// <summary>
    /// <para lang="zh">当前时间</para>
    /// <para lang="en">Current Time</para>
    /// </summary>
    [JsonPropertyName("current_time")]
    [BluetoothUUID("00001805-0000-1000-8000-00805f9b34fb")]
    CurrentTime,

    /// <summary>
    /// <para lang="zh">参照时间更新</para>
    /// <para lang="en">Reference Time Update</para>
    /// </summary>
    [JsonPropertyName("reference_time_update")]
    [BluetoothUUID("00001806-0000-1000-8000-00805f9b34fb")]
    ReferenceTimeUpdate,

    /// <summary>
    /// <para lang="zh">下个日光节约时间（夏令时）更改</para>
    /// <para lang="en">Next DST Change</para>
    /// </summary>
    [JsonPropertyName("next_dst_change")]
    [BluetoothUUID("00001807-0000-1000-8000-00805f9b34fb")]
    NextDstChange,

    /// <summary>
    /// <para lang="zh">葡萄糖</para>
    /// <para lang="en">Glucose</para>
    /// </summary>
    [JsonPropertyName("glucose")]
    [BluetoothUUID("00001808-0000-1000-8000-00805f9b34fb")]
    Glucose,

    /// <summary>
    /// <para lang="zh">温度计</para>
    /// <para lang="en">Health Thermometer</para>
    /// </summary>
    [JsonPropertyName("health_thermometer")]
    [BluetoothUUID("00001809-0000-1000-8000-00805f9b34fb")]
    HealthThermometer,

    /// <summary>
    /// <para lang="zh">设备信息</para>
    /// <para lang="en">Device Information</para>
    /// </summary>
    [JsonPropertyName("device_information")]
    [BluetoothUUID("0000180A-0000-1000-8000-00805f9b34fb")]
    DeviceInformation,

    /// <summary>
    /// <para lang="zh">心率</para>
    /// <para lang="en">Heart Rate</para>
    /// </summary>
    [JsonPropertyName("heart_rate")]
    [BluetoothUUID("0000180D-0000-1000-8000-00805f9b34fb")]
    HeartRate,

    /// <summary>
    /// <para lang="zh">手机报警状态</para>
    /// <para lang="en">Phone Alert Status</para>
    /// </summary>
    [JsonPropertyName("phone_alert_status")]
    [BluetoothUUID("0000180E-0000-1000-8000-00805f9b34fb")]
    PhoneAlertStatus,

    /// <summary>
    /// <para lang="zh">电池数据</para>
    /// <para lang="en">Battery Service</para>
    /// </summary>
    [JsonPropertyName("battery_service")]
    [BluetoothUUID("0000180F-0000-1000-8000-00805f9b34fb")]
    BatteryService,

    /// <summary>
    /// <para lang="zh">血压</para>
    /// <para lang="en">Blood Pressure</para>
    /// </summary>
    [JsonPropertyName("blood_pressure")]
    [BluetoothUUID("00001810-0000-1000-8000-00805f9b34fb")]
    BloodPressure,

    /// <summary>
    /// <para lang="zh">闹钟通知</para>
    /// <para lang="en">Alert Notification</para>
    /// </summary>
    [JsonPropertyName("alert_notification")]
    [BluetoothUUID("00001811-0000-1000-8000-00805f9b34fb")]
    AlertNotification,

    /// <summary>
    /// <para lang="zh">HID设备</para>
    /// <para lang="en">Human Interface Device</para>
    /// </summary>
    [JsonPropertyName("human_interface_device")]
    [BluetoothUUID("00001812-0000-1000-8000-00805f9b34fb")]
    HumanInterfaceDevice,

    /// <summary>
    /// <para lang="zh">扫描参数</para>
    /// <para lang="en">Scan Parameters</para>
    /// </summary>
    [JsonPropertyName("scan_parameters")]
    [BluetoothUUID("00001813-0000-1000-8000-00805f9b34fb")]
    ScanParameters,

    /// <summary>
    /// <para lang="zh">跑步速度、节奏</para>
    /// <para lang="en">Running Speed and Cadence</para>
    /// </summary>
    [JsonPropertyName("running_speed_and_cadence")]
    [BluetoothUUID("00001814-0000-1000-8000-00805f9b34fb")]
    RunningSpeedAndCadence,

    /// <summary>
    /// <para lang="zh">自动化输入输出</para>
    /// <para lang="en">Automation IO</para>
    /// </summary>
    [JsonPropertyName("automation_io")]
    [BluetoothUUID("00001815-0000-1000-8000-00805f9b34fb")]
    AutomationIo,

    /// <summary>
    /// <para lang="zh">循环速度、节奏</para>
    /// <para lang="en">Cycling Speed and Cadence</para>
    /// </summary>
    [JsonPropertyName("cycling_speed_and_cadence")]
    [BluetoothUUID("00001816-0000-1000-8000-00805f9b34fb")]
    CyclingSpeedAndCadence,

    /// <summary>
    /// <para lang="zh">骑行能量</para>
    /// <para lang="en">Cycling Power</para>
    /// </summary>
    [JsonPropertyName("cycling_power")]
    [BluetoothUUID("00001818-0000-1000-8000-00805f9b34fb")]
    CyclingPower,

    /// <summary>
    /// <para lang="zh">定位及导航</para>
    /// <para lang="en">Location and Navigation</para>
    /// </summary>
    [JsonPropertyName("location_and_navigation")]
    [BluetoothUUID("00001819-0000-1000-8000-00805f9b34fb")]
    LocationAndNavigation,

    /// <summary>
    /// <para lang="zh">环境传感</para>
    /// <para lang="en">Environmental Sensing</para>
    /// </summary>
    [JsonPropertyName("environmental_sensing")]
    [BluetoothUUID("0000181A-0000-1000-8000-00805f9b34fb")]
    EnvironmentalSensing,

    /// <summary>
    /// <para lang="zh">身体组成</para>
    /// <para lang="en">Body Composition</para>
    /// </summary>
    [JsonPropertyName("body_composition")]
    [BluetoothUUID("0000181B-0000-1000-8000-00805f9b34fb")]
    BodyComposition,

    /// <summary>
    /// <para lang="zh">用户数据</para>
    /// <para lang="en">User Data</para>
    /// </summary>
    [JsonPropertyName("user_data")]
    [BluetoothUUID("0000181C-0000-1000-8000-00805f9b34fb")]
    UserData,

    /// <summary>
    /// <para lang="zh">体重秤</para>
    /// <para lang="en">Weight Scale</para>
    /// </summary>
    [JsonPropertyName("weight_scale")]
    [BluetoothUUID("0000181D-0000-1000-8000-00805f9b34fb")]
    WeightScale,

    /// <summary>
    /// <para lang="zh">设备绑定管理</para>
    /// <para lang="en">Bond Management</para>
    /// </summary>
    [JsonPropertyName("bond_management")]
    [BluetoothUUID("0000181E-0000-1000-8000-00805f9b34fb")]
    BondManagement,

    /// <summary>
    /// <para lang="zh">动态血糖检测</para>
    /// <para lang="en">Continuous Glucose Monitoring</para>
    /// </summary>
    [JsonPropertyName("continuous_glucose_monitoring")]
    [BluetoothUUID("0000181F-0000-1000-8000-00805f9b34fb")]
    ContinuousGlucoseMonitoring,

    /// <summary>
    /// <para lang="zh">互联网协议支持</para>
    /// <para lang="en">Internet Protocol Support</para>
    /// </summary>
    [JsonPropertyName("internet_protocol_support")]
    [BluetoothUUID("00001820-0000-1000-8000-00805f9b34fb")]
    InternetProtocolSupport,

    /// <summary>
    /// <para lang="zh">室内定位</para>
    /// <para lang="en">Indoor Positioning</para>
    /// </summary>
    [JsonPropertyName("indoor_positioning")]
    [BluetoothUUID("00001821-0000-1000-8000-00805f9b34fb")]
    IndoorPositioning,

    /// <summary>
    /// <para lang="zh">脉搏血氧计</para>
    /// <para lang="en">Pulse Oximeter</para>
    /// </summary>
    [JsonPropertyName("pulse_oximeter")]
    [BluetoothUUID("00001822-0000-1000-8000-00805f9b34fb")]
    PulseOximeter,

    /// <summary>
    /// <para lang="zh">HTTP代理</para>
    /// <para lang="en">HTTP Proxy</para>
    /// </summary>
    [JsonPropertyName("http_proxy")]
    [BluetoothUUID("00001823-0000-1000-8000-00805f9b34fb")]
    HttpProxy,

    /// <summary>
    /// <para lang="zh">传输发现</para>
    /// <para lang="en">Transport Discovery</para>
    /// </summary>
    [JsonPropertyName("transport_discovery")]
    [BluetoothUUID("00001824-0000-1000-8000-00805f9b34fb")]
    TransportDiscovery,

    /// <summary>
    /// <para lang="zh">对象传输</para>
    /// <para lang="en">Object Transfer</para>
    /// </summary>
    [JsonPropertyName("object_transfer")]
    [BluetoothUUID("00001825-0000-1000-8000-00805f9b34fb")]
    ObjectTransfer,

    /// <summary>
    /// <para lang="zh">健康设备</para>
    /// <para lang="en">Fitness Machine</para>
    /// </summary>
    [JsonPropertyName("fitness_machine")]
    [BluetoothUUID("00001826-0000-1000-8000-00805f9b34fb")]
    FitnessMachine,

    /// <summary>
    /// <para lang="zh">节点配置</para>
    /// <para lang="en">Mesh Provisioning</para>
    /// </summary>
    [JsonPropertyName("mesh_provisioning")]
    [BluetoothUUID("00001827-0000-1000-8000-00805f9b34fb")]
    MeshProvisioning,

    /// <summary>
    /// <para lang="zh">节点代理</para>
    /// <para lang="en">Mesh Proxy</para>
    /// </summary>
    [JsonPropertyName("mesh_proxy")]
    [BluetoothUUID("00001828-0000-1000-8000-00805f9b34fb")]
    MeshProxy,

    /// <summary>
    /// <para lang="zh">重连配置</para>
    /// <para lang="en">Reconnection Configuration</para>
    /// </summary>
    [JsonPropertyName("reconnection_configuration")]
    [BluetoothUUID("00001829-0000-1000-8000-00805f9b34fb")]
    ReconnectionConfiguration
}
