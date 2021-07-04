// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Emit 方法帮助类
    /// </summary>
    public static class EmitHelper
    {
        /// <summary>
        /// 通过 ITableColumn 创建动态类
        /// </summary>
        public static Type CreateTypeByName(string typeName, IEnumerable<ITableColumn> cols, Type? parent = null, Func<ITableColumn, IEnumerable<CustomAttributeBuilder>>? creatingCallback = null)
        {
            var typeBuilder = CreateTypeBuilderByName(typeName, parent);

            foreach (var col in cols)
            {
                var attributeBuilds = creatingCallback?.Invoke(col);
                typeBuilder.CreateProperty(col, attributeBuilds);
            }

            return typeBuilder.CreateType()!;
        }

        private static TypeBuilder CreateTypeBuilderByName(string typeName, Type? parent = null)
        {
            var assemblyName = new AssemblyName("BootstrapBlazor_DynamicAssembly");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("BootstrapBlazor_DynamicAssembly_Module");
            var typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public, parent);
            return typeBuilder;
        }

        private static void CreateProperty(this TypeBuilder typeBuilder, IEditorItem col, IEnumerable<CustomAttributeBuilder>? attributeBuilds)
        {
            var fieldName = col.GetFieldName();
            var field = typeBuilder.DefineField($"_{fieldName}", col.PropertyType, FieldAttributes.Private);

            var methodGetField = typeBuilder.DefineMethod($"Get{fieldName}", MethodAttributes.Public, col.PropertyType, null);
            var methodSetField = typeBuilder.DefineMethod(
              $"Set{fieldName}", MethodAttributes.Public, null, new Type[] { col.PropertyType });

            var ilOfGetId = methodGetField.GetILGenerator();
            ilOfGetId.Emit(OpCodes.Ldarg_0);
            ilOfGetId.Emit(OpCodes.Ldfld, field);
            ilOfGetId.Emit(OpCodes.Ret);

            var ilOfSetId = methodSetField.GetILGenerator();
            ilOfSetId.Emit(OpCodes.Ldarg_0);
            ilOfSetId.Emit(OpCodes.Ldarg_1);
            ilOfSetId.Emit(OpCodes.Stfld, field);
            ilOfSetId.Emit(OpCodes.Ret);

            var propertyId = typeBuilder.DefineProperty(fieldName, PropertyAttributes.None, col.PropertyType, null);
            propertyId.SetGetMethod(methodGetField);
            propertyId.SetSetMethod(methodSetField);

            foreach (var cab in attributeBuilds ?? Enumerable.Empty<CustomAttributeBuilder>())
            {
                propertyId.SetCustomAttribute(cab);
            }
        }
    }
}
