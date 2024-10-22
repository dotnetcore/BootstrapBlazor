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
    /// 
    /// </summary>
    [JsonPropertyName("generic_access")]
    GenericAccess,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("generic_attribute")]
    GenericAttribute,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("immediate_alert")]
    ImmediateAlert,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("link_loss")]
    LinkLoss,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("tx_power")]
    TXPower,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("current_time")]
    CurrentTime,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("reference_time_update")]
    ReferenceTimeUpdate,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("next_dst_change")]
    NextDstChange,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("glucose")]
    Glucose,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("health_thermometer")]
    HealthThermometer,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("device_information")]
    DeviceInformation,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("heart_rate")]
    HeartRate,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("phone_alert_status")]
    PhoneAlertStatus,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("battery_service")]
    BatteryService,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("blood_pressure")]
    BloodPressure,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("alert_notification")]
    AlertNotification,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("human_interface_device")]
    HumanInterfaceDevice,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("scan_parameters")]
    ScanParameters,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("running_speed_and_cadence")]
    RunningSpeedAndCadence,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("automation_io")]
    AutomationIO,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("cycling_speed_and_cadence")]
    CyclingSpeedAndCadence,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("cycling_power")]
    CyclingPower,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("location_and_navigation")]
    LocationAndNavigation,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("environmental_sensing")]
    EnvironmentalSensing,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("body_composition")]
    BodyComposition,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("user_data")]
    UserData,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("weight_scale")]
    WeightScale,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("bond_management")]
    BondManagement,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("continuous_glucose_monitoring")]
    ContinuousGlucoseMonitoring,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("internet_protocol_support")]
    InternetProtocolSupport,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("indoor_positioning")]
    IndoorPositioning,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("pulse_oximeter")]
    PulseOximeter,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("http_proxy")]
    HttpProxy,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("transport_discovery")]
    TransportDiscovery,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("object_transfer")]
    ObjectTransfer,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("fitness_machine")]
    FitnessMachine,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("mesh_provisioning")]
    MeshProvisioning,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("mesh_proxy")]
    MeshProxy,

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("reconnection_configuration")]
    ReconnectionConfiguration
}
