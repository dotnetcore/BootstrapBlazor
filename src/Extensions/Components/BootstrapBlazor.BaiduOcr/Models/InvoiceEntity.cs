// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 发票实体类
/// </summary>
public class InvoiceEntity
{
    /// <summary>
    /// 获得/设置 发票消费类型
    /// </summary>
    public string? ServiceType { get; set; }

    /// <summary>
    /// 获得/设置 联次信息。专票第一联到第三联分别输出：第一联：记账联、第二联：抵扣联、第三联：发票联；普通发票第一联到第二联分别输出：第一联：记账联、第二联：发票联
    /// </summary>
    public string? SheetNum { get; set; }

    /// <summary>
    /// 获得/设置 发票种类。不同类型发票输出：普通发票、专用发票、电子普通发票、电子专用发票、通行费电子普票、区块链发票、通用机打电子发票、电子发票(专用发票)、电子发票(普通发票)
    /// </summary>
    public string? InvoiceType { get; set; }

    /// <summary>
    /// 获得/设置 发票名称
    /// </summary>
    public string? InvoiceTypeOrg { get; set; }

    /// <summary>
    /// 获得/设置 发票代码
    /// </summary>
    public string InvoiceCode { get; set; } = string.Empty;

    /// <summary>
    /// 获得/设置 发票号码
    /// </summary>
    public string InvoiceNum { get; set; } = string.Empty;

    /// <summary>
    /// 获得/设置 增值税发票左上角标志。 包含：通行费,销项负数、代开、收购、成品油、其他
    /// </summary>
    public string? InvoiceTag { get; set; }

    /// <summary>
    /// 获得/设置 开票日期
    /// </summary>
    public string InvoiceDate { get; set; } = string.Empty;

    /// <summary>
    /// 获得/设置 机打号码。仅增值税卷票含有此参数
    /// </summary>
    public string? MachineNum { get; set; }

    /// <summary>
    /// 获得/设置 机器编号。仅增值税卷票含有此参数
    /// </summary>
    public string? MachineCode { get; set; }

    /// <summary>
    /// 获得/设置 校验码。增值税专票无此参数
    /// </summary>
    public string? CheckCode { get; set; }

    /// <summary>
    /// 获得/设置 购方名称
    /// </summary>
    public string? PurchaserName { get; set; }

    /// <summary>
    /// 获得/设置 购方纳税人识别号
    /// </summary>
    public string? PurchaserRegisterNum { get; set; }

    /// <summary>
    /// 获得/设置 购方地址及电话
    /// </summary>
    public string? PurchaserAddress { get; set; }

    /// <summary>
    /// 获得/设置 购方开户行及账号
    /// </summary>
    public string? PurchaserBank { get; set; }

    /// <summary>
    /// 获得/设置 密码区
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 获得/设置 省
    /// </summary>
    public string? Province { get; set; }

    /// <summary>
    /// 获得/设置 市
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// 获得/设置 是否代开
    /// </summary>
    public string? Agent { get; set; }

    /// <summary>
    /// 获得/设置 备注
    /// </summary>
    public string? Remarks { get; set; }

    /// <summary>
    /// 获得/设置 销售方名称
    /// </summary>
    public string? SellerName { get; set; }

    /// <summary>
    /// 获得/设置 销售方地址及电话
    /// </summary>
    public string? SellerAddress { get; set; }

    /// <summary>
    /// 获得/设置 销售方开户行及账号
    /// </summary>
    public string? SellerBank { get; set; }

    /// <summary>
    /// 获得/设置 销售方纳税人识别号
    /// </summary>
    public string? SellerRegisterNum { get; set; }

    /// <summary>
    /// 获得/设置 合计金额
    /// </summary>
    public string? TotalAmount { get; set; }

    /// <summary>
    /// 获得/设置 合计税额
    /// </summary>
    public string? TotalTax { get; set; }

    /// <summary>
    /// 获得/设置 价税合计(大写)
    /// </summary>
    public string? AmountInWords { get; set; }

    /// <summary>
    /// 获得/设置 价税合计(小写)
    /// </summary>
    public string? AmountInFiguers { get; set; }

    /// <summary>
    /// 获得/设置 收款人
    /// </summary>
    public string? Payee { get; set; }

    /// <summary>
    /// 获得/设置 复核
    /// </summary>
    public string? Checker { get; set; }

    /// <summary>
    /// 获得/设置 开票人
    /// </summary>
    public string? NoteDrawer { get; set; }

    /// <summary>
    /// 获得/设置 货物名称
    /// </summary>
    public List<CommodityEntity>? CommodityName { get; set; }

    /// <summary>
    /// 获得/设置 税率
    /// </summary>
    public List<CommodityTaxRateEntity>? CommodityTaxRate { get; set; }
}
