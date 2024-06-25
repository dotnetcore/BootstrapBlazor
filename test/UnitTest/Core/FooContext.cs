// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.EntityFrameworkCore;

namespace UnitTest.Core;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="options"></param>
public class FooContext(DbContextOptions<FooContext> options) : DbContext(options)
{
    /// <summary>
    /// 
    /// </summary>
    [NotNull]
    public DbSet<Foo>? Foos { get; set; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Foo>().ToTable("Foo");
        modelBuilder.Entity<Foo>().Ignore(f => f.DateTime);
        modelBuilder.Entity<Foo>().Ignore(f => f.Count);
        modelBuilder.Entity<Foo>().Ignore(f => f.Complete);
        modelBuilder.Entity<Foo>().Ignore(f => f.Education);
        modelBuilder.Entity<Foo>().Ignore(f => f.Hobby);
        modelBuilder.Entity<Foo>().Ignore(f => f.ReadonlyColumn);
    }
}
