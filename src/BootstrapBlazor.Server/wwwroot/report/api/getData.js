const categoryList = [
    {
        "TVOCReportObject": {
            "desc": "TVOC信息",
            "hasChannel": true
        }
    },
    {
        "CustomReportObject": {
            "desc": "Mock Report Object",
            "hasChannel": true
        }
    },
    {
        "Calibration": {
            "desc": "Calibration Item",
            "hasChannel": true
        }
    },
    {
        "IntegralEvent": {
            "desc": "Integral Event",
            "hasChannel": true
        }
    },
    {
        "PeakInfo": {
            "desc": "Peak Results",
            "hasChannel": true
        }
    },
    {
        "ReportInfo": {
            "desc": "Report Info",
            "hasChannel": false
        }
    },
    {
        "StatisticalResult": {
            "desc": "Statistical Results",
            "hasChannel": true
        }
    }
]

export const getCategoryList = () => {
    return categoryList.map(category => {
        const key = Object.keys(category)[0];
        return {
            value: key,
            ...category[key]
        }
    });
}

export const getVariableList = (category) => {
    const variableList = [
        {
            "Client": {
                "dataType": "string",
                "desc": "委托单位",
                "defaultFormat": null,
                "unit": null,
                "header": null
            }
        },
        {
            "Testerj": {
                "dataType": "string",
                "desc": "检测单位",
                "defaultFormat": null,
                "unit": null,
                "header": null
            }
        },
        {
            "Tests": {
                "dataType": "string",
                "desc": "检测项目",
                "defaultFormat": null,
                "unit": null,
                "header": null
            }
        },
        {
            "Operator": {
                "dataType": "string",
                "desc": "分析人员",
                "defaultFormat": null,
                "unit": null,
                "header": null
            }
        },
        {
            "TestCondition": {
                "dataType": "string",
                "desc": "分析条件",
                "defaultFormat": null,
                "unit": null,
                "header": null
            }
        },
        {
            "Project": {
                "dataType": "string",
                "desc": "工程名称",
                "defaultFormat": null,
                "unit": null,
                "header": null
            }
        },
        {
            "Location": {
                "dataType": "string",
                "desc": "采样地点",
                "defaultFormat": null,
                "unit": null,
                "header": null
            }
        },
        {
            "SampleID": {
                "dataType": "string",
                "desc": "样品编号",
                "defaultFormat": null,
                "unit": null,
                "header": null
            }
        },
        {
            "DeviceID": {
                "dataType": "string",
                "desc": "设备编号",
                "defaultFormat": null,
                "unit": null,
                "header": null
            }
        },
        {
            "CalibCurve": {
                "dataType": "string",
                "desc": "标准曲线",
                "defaultFormat": null,
                "unit": null,
                "header": null
            }
        },
        {
            "Amount": {
                "dataType": "string",
                "desc": "TVOC含量",
                "defaultFormat": null,
                "unit": null,
                "header": null
            }
        },
        {
            "Summary": {
                "dataType": "string",
                "desc": "总结",
                "defaultFormat": null,
                "unit": null,
                "header": null
            }
        }
    ]
    return variableList.map(variable => {
        const key = Object.keys(variable)[0];
        return {
            value: category.value + '_' + key,
            ...variable[key]
        }
    });
}