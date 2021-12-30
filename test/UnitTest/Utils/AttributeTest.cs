// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Utils;

public class AttributeTest
{

    [Fact]
    public void DefaultValueTest()
    {
        var type = typeof(Table);
        var cat = TypeDescriptor.GetAttributes(type).Cast<TestAttribute>().First();
        var p = TypeDescriptor.GetProperties(type).Cast<PropertyDescriptor>().First();
        var pat = p.Attributes.OfType<TestAttribute>().First();

        var ct = cat.GetTest();
        var pt = pat.GetTest();
    }



    [Test(IsValid = true)]
    public class Table
    {
        [Test(Width = 100)]
        public int Column1 { get; set; }
    }

    public class TestInfo
    {
        public int? Width { get; set; }

        public string? Name { get; set; }

        public bool? IsActive { get; set; }
    }


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class TestAttribute : Attribute
    {


        private int? _width;

        private string? _name;

        private bool? _isValid;

        public int Width
        {
            set => this._width = value;
            get => this._width ?? 0;
        }

        public string Name
        {
            set => this._name = value;
            get => this._name ?? "";
        }

        public bool IsValid
        {
            set => this._isValid = value;
            get => this._isValid ?? false;
        }

        public TestInfo GetTest()
        {
            return new TestInfo()
            {
                Width = this._width,
                Name = this._name,
                IsActive = this._isValid
            };
        }
    }
}
