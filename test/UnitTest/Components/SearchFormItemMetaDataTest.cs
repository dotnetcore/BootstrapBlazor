// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class SearchFormItemMetaDataTest
{
    [Fact]
    public async Task StringSearchFormItemMetaData_Ok()
    {
        var valueChanged = false;
        var meta = new StringSearchMetaData()
        {
            FilterAction = FilterAction.Contains,
            FilterLogic = FilterLogic.And,
            PlaceHolder = "Test",
            Value = "string-value",
            ValueChanged = () =>
            {
                valueChanged = true;
                return Task.CompletedTask;
            },
            GetFilterCallback = v =>
            {
                return new FilterKeyValueAction()
                {
                    FieldKey = "fieldKey-test",
                    FieldValue = v,
                    FilterAction = FilterAction.Contains
                };
            }
        };

        Assert.Equal(FilterAction.Contains, meta.FilterAction);
        Assert.Equal(FilterLogic.And, meta.FilterLogic);
        Assert.Equal("Test", meta.PlaceHolder);
        Assert.Equal("string-value", meta.Value);

        await meta.ValueChangedHandler("new-value");
        Assert.True(valueChanged);
        Assert.Equal("new-value", meta.Value);

        await meta.ValueChangedHandler("");
        Assert.True(valueChanged);
        Assert.Null(meta.Value);

        await meta.ValueChangedHandler(null);
        Assert.True(valueChanged);
        Assert.Null(meta.Value);

        var action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Equal("fieldKey-test", action.FieldKey);

        meta.GetFilterCallback = null;
        meta.Value = "string-value";
        action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Equal("fieldKey", action.FieldKey);
        Assert.Equal("string-value", action.FieldValue);

        meta.Value = "";
        action = meta.GetFilter("fieldKey");
        Assert.Null(action);
    }

    [Fact]
    public void MultipleStringSearchFormItemMetaData_Ok()
    {
        var meta = new MultipleStringSearchMetaData();
        var action = meta.GetFilter("fieldKey");
        Assert.Null(action);

        meta.GetFilterCallback = v =>
        {
            return new FilterKeyValueAction()
            {
                FieldKey = "fieldKey-v1",
                FieldValue = "test"
            };
        };
        action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Equal("fieldKey-v1", action.FieldKey);
        Assert.Equal("test", action.FieldValue);

        meta.GetFilterCallback = null;
        meta.Value = "field-v1";
        action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Equal("fieldKey", action.FieldKey);
        Assert.Equal("field-v1", action.FieldValue);

        meta.Value = "field-v1 field-v2";
        action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Equal(2, action.Filters.Count);
        Assert.Equal("field-v1", action.Filters[0].FieldValue);
        Assert.Equal("field-v2", action.Filters[1].FieldValue);
        Assert.Equal(FilterLogic.And, action.FilterLogic);
    }

    [Fact]
    public void SelectSearchFormItemMetaData_Ok()
    {
        var meta = new SelectSearchMetaData()
        {
            Items = new List<SelectedItem>()
            {
                new SelectedItem("v1", "v1"),
                new SelectedItem("v2", "v2")
            }
        };
        var action = meta.GetFilter("fieldKey");
        Assert.Null(action);

        meta.Value = "v1";
        action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Equal("fieldKey", action.FieldKey);
        Assert.Equal("v1", action.FieldValue);
        Assert.Empty(action.Filters);
    }

    [Fact]
    public void MultipleSelectSearchFormItemMetaData_Ok()
    {
        var meta = new MultipleSelectSearchMetaData()
        {
            Items = new List<SelectedItem>()
            {
                new SelectedItem("v1", "v1"),
                new SelectedItem("v2", "v2")
            }
        };
        var action = meta.GetFilter("fieldKey");
        Assert.Null(action);

        meta.GetFilterCallback = v =>
        {
            return new FilterKeyValueAction()
            {
                FieldKey = "fieldKey-v1",
                FieldValue = "test"
            };
        };
        action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Equal("fieldKey-v1", action.FieldKey);
        Assert.Equal("test", action.FieldValue);

        meta.GetFilterCallback = null;
        meta.Value = "field-v1";
        action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Equal("fieldKey", action.FieldKey);
        Assert.Equal("field-v1", action.FieldValue);

        meta.Value = "field-v1 field-v2";
        action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Equal(2, action.Filters.Count);
        Assert.Equal("field-v1", action.Filters[0].FieldValue);
        Assert.Equal("field-v2", action.Filters[1].FieldValue);
        Assert.Equal(FilterLogic.And, action.FilterLogic);
    }

    [Fact]
    public async Task NumberSearchFormItemMetaData_Ok()
    {
        var meta = new NumberSearchMetaData()
        {
            StartValue = "10",
            StartValueLabelText = "Start",
            EndValue = "20",
            EndValueLabelText = "End",
            ValueType = typeof(int),
            GetFilterCallback = v =>
            {
                FilterKeyValueAction action = new();
                if (v is (string StartValue, string EndValue))
                {
                    action.Filters.Add(new FilterKeyValueAction()
                    {
                        FieldKey = "fieldKey",
                        FieldValue = 10,
                        FilterLogic = FilterLogic.And,
                        FilterAction = FilterAction.GreaterThanOrEqual
                    });
                    action.Filters.Add(new FilterKeyValueAction()
                    {
                        FieldKey = "fieldKey",
                        FieldValue = 20,
                        FilterLogic = FilterLogic.And,
                        FilterAction = FilterAction.LessThanOrEqual
                    });
                }
                return action;
            }
        };
        var action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Equal(2, action.Filters.Count);
        Assert.Equal(10, action.Filters[0].FieldValue);
        Assert.Equal(20, action.Filters[1].FieldValue);
        Assert.Equal(FilterLogic.And, action.FilterLogic);

        meta.GetFilterCallback = null;
        meta.StartValue = null;
        meta.EndValue = null;
        action = meta.GetFilter("fieldKey");
        Assert.Null(action);

        meta.StartValue = "10";
        action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Single(action.Filters);
        Assert.Equal(10, action.Filters[0].FieldValue);

        meta.EndValue = "20";
        action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Equal(2, action.Filters.Count);
        Assert.Equal(10, action.Filters[0].FieldValue);
        Assert.Equal(FilterAction.GreaterThanOrEqual, action.Filters[0].FilterAction);
        Assert.Equal(20, action.Filters[1].FieldValue);
        Assert.Equal(FilterAction.LessThanOrEqual, action.Filters[1].FilterAction);
        Assert.Equal(FilterLogic.And, action.FilterLogic);

        // 值无法转成 int 单元测试
        meta.StartValue = "test1";
        action = meta.GetFilter("fieldKey");
        Assert.Null(meta.StartValue);

        meta.EndValue = "test1";
        action = meta.GetFilter("fieldKey");
        Assert.Null(meta.EndValue);

        // 值变化处理方法 单元测试
        var valueChanged = false;
        meta.ValueChanged = () =>
        {
            valueChanged = true;
            return Task.CompletedTask;
        };
        await meta.StartValueChangedHandler("11");
        Assert.Equal("11", meta.StartValue);
        Assert.True(valueChanged);

        valueChanged = false;
        await meta.EndValueChangedHandler("22");
        Assert.Equal("22", meta.EndValue);
        Assert.True(valueChanged);
    }

    [Fact]
    public void NumberSearchFormItemMetaData_ValueType()
    {
        var meta = new NumberSearchMetaData()
        {
            StartValue = "10",
            EndValue = "20",
            ValueType = null
        };

        // 未赋值 ValueType 直接使用字符串拼接
        var action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Equal(2, action.Filters.Count);
        Assert.Equal("10", action.Filters[0].FieldValue);
        Assert.Equal(FilterAction.GreaterThanOrEqual, action.Filters[0].FilterAction);
        Assert.Equal("20", action.Filters[1].FieldValue);
        Assert.Equal(FilterAction.LessThanOrEqual, action.Filters[1].FilterAction);
        Assert.Equal(FilterLogic.And, action.FilterLogic);
    }

    [Fact]
    public async Task DateTimeSearchFormItemMetaData_Ok()
    {
        var meta = new DateTimeSearchMetaData();

        var valueChanged = false;
        meta.ValueChanged = () =>
        {
            valueChanged = true;
            return Task.CompletedTask;
        };
        await meta.ValueChangedHandler(DateTime.Today);
        Assert.Equal(DateTime.Today, meta.Value);
        Assert.True(valueChanged);

        meta.Value = null;
        var action = meta.GetFilter("fieldKey");
        Assert.Null(action);

        meta.GetFilterCallback = v =>
        {
            return new FilterKeyValueAction()
            {
                FieldKey = "fieldKey",
                FieldValue = DateTime.Today,
                FilterLogic = FilterLogic.And,
                FilterAction = FilterAction.Equal
            };
        };
        action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);

        meta.GetFilterCallback = null;
        meta.Value = DateTime.Today;
        action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Equal(FilterLogic.And, action.FilterLogic);
    }

    [Fact]
    public async Task DateTimeRangeSearchFormItemMetaData_Ok()
    {
        var meta = new DateTimeRangeSearchMetaData();

        var valueChanged = false;
        meta.ValueChanged = () =>
        {
            valueChanged = true;
            return Task.CompletedTask;
        };
        await meta.ValueChangedHandler(new DateTimeRangeValue() { Start = DateTime.Today, End = DateTime.Today });
        Assert.NotNull(meta.Value);
        Assert.Equal(DateTime.Today, meta.Value.Start);
        Assert.True(valueChanged);

        meta.Value = null;
        var action = meta.GetFilter("fieldKey");
        Assert.Null(action);

        meta.GetFilterCallback = v =>
        {
            return new FilterKeyValueAction()
            {
                FieldKey = "fieldKey",
                FieldValue = DateTime.Today,
                FilterLogic = FilterLogic.And,
                FilterAction = FilterAction.Equal
            };
        };
        action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);

        meta.GetFilterCallback = null;
        meta.Value = new DateTimeRangeValue() { Start = DateTime.Today, End = DateTime.Today };
        action = meta.GetFilter("fieldKey");
        Assert.NotNull(action);
        Assert.Equal(2, action.Filters.Count);
        Assert.Equal(DateTime.Today, action.Filters[0].FieldValue);
        Assert.Equal(FilterAction.GreaterThanOrEqual, action.Filters[0].FilterAction);
        Assert.Equal(DateTime.Today, action.Filters[1].FieldValue);
        Assert.Equal(FilterAction.LessThanOrEqual, action.Filters[1].FilterAction);
        Assert.Equal(FilterLogic.And, action.FilterLogic);
    }
}
