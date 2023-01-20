// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 发票验证真伪结果实体类
/// </summary>
public class InvoiceVerifyResult
{
    /// <summary>
    /// 获得/设置 查验次数。为历史查验次数
    /// </summary>
    public string? VerifyFrequency { get; set; }

    /// <summary>
    /// 获得/设置 机器编号
    /// </summary>
    public string? MachineCode { get; set; }

    /// <summary>
    /// 获得/设置 发票号码
    /// </summary>
    public string? InvoiceNum { get; set; }

    /// <summary>
    /// 获得/设置 开票日期
    /// </summary>
    public string? InvoiceDate { get; set; }

    /// <summary>
    /// 获得/设置 查验结果 查验成功返回 "0001"，查验失败返回对应查验结果错误码
    /// </summary>
    public string? VerifyResult { get; set; }

    /// <summary>
    /// 获得/设置 是否作废（冲红）。Y：已作废；H：已冲红；N：未作废
    /// </summary>
    public string? InvalidSign { get; set; }

    /// <summary>
    /// 获得/设置 发票代码
    /// </summary>
    public string? InvoiceCode { get; set; }

    /// <summary>
    /// 获得/设置 发票种类。即增值税专用发票、增值税电子专用发票、增值税普通发票、增值税普通发票（电子）、增值税普通发票（卷式）、通行费增值税电子普通发票、区块链电子发票、全电发票（专用发票）、全电发票（普通发票）、机动车销售发票、二手车销售发票、货物运输业增值税专用发票
    /// </summary>
    public string? InvoiceType { get; set; }

    /// <summary>
    /// 获得/设置 校验码
    /// </summary>
    public string? CheckCode { get; set; }

    /// <summary>
    /// 获得/设置 查验结果信息。查验成功且发票为真返回 "查验成功发票一致"，查验失败返回对应错误原因
    /// </summary>
    public string? VerifyMessage { get; set; }

    /// <summary>
    /// 获得/设置 发票实例
    /// </summary>
    public InvoiceEntity? Invoice { get; set; }

    /// <summary>
    /// 获得/设置 是否验证通过
    /// </summary>
    public bool Valid => VerifyResult == "0001";
}
