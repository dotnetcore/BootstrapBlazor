// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;


namespace BootstrapBlazor.Shared.Pages.Table
{
    public partial class TablesDynamicColumn
    {
        public MyDataService DataService { get; set; } = new MyDataService();
    }

    /// <summary>
    /// 示例Model，动态属性的User
    /// </summary>
    public class DunamicUser : IDynamicType
    {
        static DunamicUser()
        {
            var type = typeof(DunamicUser);
            //添加类型Attribute定义
            DynamicPropertyRegistry.AddAutoGenerateClassAttribute(type, new AutoGenerateClassAttribute());

            //添加属性Attribute定义
            DynamicPropertyRegistry.AddProperty(type, new DynamicPropertyInfo("Name", typeof(string), new Attribute[] { new AutoGenerateColumnAttribute() { Order = 1, Filterable = true, Searchable = true ,Text="名称"} }));
            DynamicPropertyRegistry.AddProperty(type, new DynamicPropertyInfo("Age", typeof(int), new Attribute[] { new AutoGenerateColumnAttribute() { Order = 2, Filterable = true, Searchable = true ,Text="年龄" } }));
        }
        private Dictionary<string, object?> propDic = new Dictionary<string, object?>();

        public DunamicUser(Dictionary<string, object?> propDic)
        {
            this.propDic = propDic;
        }

        static int i = 1;

        public DunamicUser()
        {
            propDic.Add("Name", $"张三--{i++}");
            propDic.Add("Age", i+10);
            Id = i++;
        }

        static int i = 1;

        public int Id { get; set; }

        private void InitProperty()
        {

        }
        public object? GetValue(string propName)
        {
            if (propDic.ContainsKey(propName))
            {
                return propDic[propName];
            }
            return $"属性不存在,{propName}";
        }
        /// <summary>
        /// 当前对象是否是创建时的临时对象
        /// </summary>
        public bool IsTemp { get; set; } = false;

        public DunamicUser SetName(string n)
        {
            propDic["Name"] = n;
            return this;
        }

        public void SetValue(string propName, object value)
        {
            //这里需要类型转换
            propDic[propName] = value;
        }

        /// <summary>
        /// 编辑时 调用Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Dictionary<string, object?> newPropDic = new Dictionary<string, object?>();
            foreach (var item in propDic)
            {
                newPropDic[item.Key] = item.Value;
            }
            return new DunamicUser(newPropDic) { Id = this.Id };
        }

        public void CopyFrom(IDynamicType other)
        {
            foreach (var item in propDic)
            {
                propDic[item.Key] = other.GetValue(item.Key);
            }
        }
    }


    public class MyDataService : IDataService<DunamicUser>
    {
        static MyDataService()
        {
            for (int i = 0; i < 10; i++)
            {
                AllUser.Add(new DunamicUser().SetName(DateTime.Now.ToString()));
            }
        }
        public static List<DunamicUser> AllUser = new List<DunamicUser>();
        public Task<bool> AddAsync(DunamicUser model)
        {
            model.IsTemp = true;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(IEnumerable<DunamicUser> models)
        {
            foreach (var item in models)
            {
                AllUser.Remove(item);
            }
            return Task.FromResult(true);
        }

        public Task<QueryData<DunamicUser>> QueryAsync(QueryPageOptions option)
        {
            return Task.FromResult(new QueryData<DunamicUser> { Items = AllUser });
        }

        public Task<bool> SaveAsync(DunamicUser model)
        {
            //临时对象 保存 就是新增操作
            if (model.IsTemp)
            {
                //新增
                model.IsTemp = false;
                AllUser.Add(model);
            }
            else
            {
                //编辑 直接替换对象
                var index = AllUser.FindIndex(p => p.Id == model.Id);
                AllUser[index] = model;
            }
            return Task.FromResult(true);
        }
    }
}
