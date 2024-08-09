// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace UniTestIconPark;

public partial class UnitTest
{
    [Theory]
    [InlineData("filled")]
    [InlineData("outlined")]
    [InlineData("twotone")]
    public void Build(string category)
    {
        var services = new ServiceCollection();
        services.AddBootstrapBlazor();
        var provider = services.BuildServiceProvider();
        var zipService = provider.GetRequiredService<IZipArchiveService>();

        var root = AppContext.BaseDirectory;
        var downloadFile = Path.Combine(root, $"{category}.zip");
        Assert.True(File.Exists(downloadFile));

        var downloadFolder = Path.Combine(root, category);
        if (Directory.Exists(downloadFile))
        {
            Directory.Delete(downloadFile, true);
        }
        zipService.ExtractToDirectory(downloadFile, downloadFolder, true);

        var folder = new DirectoryInfo(downloadFolder);

        // 处理 List 文件
        var iconListFile = Path.Combine(root, $"../../../AntDesignIconList_{category}.razor");
        if (File.Exists(iconListFile))
        {
            File.Delete(iconListFile);
        }

        // 处理 svg 文件
        var svgFile = Path.Combine(root, $"../../../{category}.svg");
        if (File.Exists(svgFile))
        {
            File.Delete(svgFile);
        }
        using var listWriter = new StreamWriter(File.OpenWrite(iconListFile));
        using var writer = new StreamWriter(File.OpenWrite(svgFile));
        writer.WriteLine("<svg xmlns=\"http://www.w3.org/2000/svg\">");
        foreach (var icon in folder.EnumerateFiles())
        {
            var id = Path.GetFileNameWithoutExtension(icon.Name);
            using var reader = new StreamReader(icon.OpenRead());
            var data = reader.ReadToEnd();
            reader.Close();

            // find <svg
            var index = data.IndexOf("<svg ");
            if (index > -1)
            {
                data = data[index..];
            }
            index = data.IndexOf(">");
            if (index > -1)
            {
                data = data[(index + 1)..];
            }
            var target = data.Replace("</svg>", "").Trim();
            target = $"    <symbol viewBox=\"0 0 1024 1024\" id=\"{id}\">{target}</symbol>";
            writer.WriteLine(target);

            listWriter.WriteLine($"<AntDesignIcon Name=\"{id}\"></AntDesignIcon>");
        }
        writer.WriteLine("</svg>");
        writer.Close();
    }
}
