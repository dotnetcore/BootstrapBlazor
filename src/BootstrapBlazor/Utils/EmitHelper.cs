// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;
using System.Reflection.Emit;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Emit 方法帮助类</para>
///  <para lang="en">Emit helper class</para>
/// </summary>
public static class EmitHelper
{
    /// <summary>
    ///  <para lang="zh">通过 ITableColumn 创建动态类</para>
    ///  <para lang="en">Create dynamic class by ITableColumn</para>
    /// </summary>
    /// <param name="typeName"><para lang="zh">动态类名称</para><para lang="en">动态类name</para></param>
    /// <param name="cols"><para lang="zh">ITableColumn 集合</para><para lang="en">ITableColumn collection</para></param>
    /// <param name="parent"><para lang="zh">父类类型</para><para lang="en">父类type</para></param>
    /// <param name="creatingCallback"><para lang="zh">回调委托</para><para lang="en">回调delegate</para></param>
    /// <returns></returns>
    public static Type? CreateTypeByName(string typeName, IEnumerable<ITableColumn> cols, Type? parent = null, Func<ITableColumn, IEnumerable<CustomAttributeBuilder>>? creatingCallback = null)
    {
        var typeBuilder = CreateTypeBuilderByName(typeName, parent);

        foreach (var col in cols)
        {
            var attributeBuilds = creatingCallback?.Invoke(col);
            typeBuilder.CreateProperty(col, attributeBuilds);
        }
        return typeBuilder.CreateType();
    }

    private static TypeBuilder CreateTypeBuilderByName(string typeName, Type? parent = null)
    {
        var assemblyName = new AssemblyName("BootstrapBlazor_DynamicAssembly");
        var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
        var moduleBuilder = assemblyBuilder.DefineDynamicModule("BootstrapBlazor_DynamicAssembly_Module");
        var typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public, parent);
        return typeBuilder;
    }

    private static void CreateProperty(this TypeBuilder typeBuilder, IEditorItem col, IEnumerable<CustomAttributeBuilder>? attributeBuilds = null) => CreateProperty(typeBuilder, col.GetFieldName(), col.PropertyType, attributeBuilds);

    private static void CreateProperty(this TypeBuilder typeBuilder, string propertyName, Type propertyType, IEnumerable<CustomAttributeBuilder>? attributeBuilds = null)
    {
        var fieldName = propertyName;
        var field = typeBuilder.DefineField($"_{fieldName}", propertyType, FieldAttributes.Private);

        var methodGetField = typeBuilder.DefineMethod($"Get{fieldName}", MethodAttributes.Public, propertyType, null);
        var methodSetField = typeBuilder.DefineMethod($"Set{fieldName}", MethodAttributes.Public, null, [propertyType]);

        var ilOfGetId = methodGetField.GetILGenerator();
        ilOfGetId.Emit(OpCodes.Ldarg_0);
        ilOfGetId.Emit(OpCodes.Ldfld, field);
        ilOfGetId.Emit(OpCodes.Ret);

        var ilOfSetId = methodSetField.GetILGenerator();
        ilOfSetId.Emit(OpCodes.Ldarg_0);
        ilOfSetId.Emit(OpCodes.Ldarg_1);
        ilOfSetId.Emit(OpCodes.Stfld, field);
        ilOfSetId.Emit(OpCodes.Ret);

        var propertyId = typeBuilder.DefineProperty(fieldName, PropertyAttributes.None, propertyType, null);
        propertyId.SetGetMethod(methodGetField);
        propertyId.SetSetMethod(methodSetField);

        foreach (var cab in attributeBuilds ?? [])
        {
            propertyId.SetCustomAttribute(cab);
        }
    }
}
