// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace UniTestIconPark;

public partial class UnitTest
{
    [Fact]
    public void IconPark_Ok()
    {
        var services = new ServiceCollection();
        services.AddBootstrapBlazor();
        var provider = services.BuildServiceProvider();
        var zipService = provider.GetRequiredService<IZipArchiveService>();

        var root = AppContext.BaseDirectory;
        var downloadFile = Path.Combine(root, "download.zip");
        Assert.True(File.Exists(downloadFile));

        var downloadFolder = Path.Combine(root, "download");
        if (Directory.Exists(downloadFile))
        {
            Directory.Delete(downloadFile, true);
        }
        zipService.ExtractToDirectory(downloadFile, downloadFolder, true);

        var folder = new DirectoryInfo(downloadFolder);

        // 处理 List 文件
        var iconListFile = Path.Combine(root, "../../../IconParkList.razor");
        if (File.Exists(iconListFile))
        {
            File.Delete(iconListFile);
        }

        // 处理 svg 文件
        var svgFile = Path.Combine(root, "../../../icon-park.svg");
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

            var regex = SvgRegex();
            var m = regex.Match(data);
            var target = m.Groups[1].Value;

            // remove stroke
            target = $"    <symbol viewBox=\"0 0 48 48\" fill=\"none\" id=\"{id}\">{target.Replace("stroke=\"#333\" ", string.Empty)}</symbol>";
            writer.WriteLine(target);

            listWriter.WriteLine($"<ByteDanceIcon Name=\"{id}\"></ByteDanceIcon>");
        }
        writer.WriteLine("</svg>");
        writer.Close();
    }

    [Theory]
    [InlineData("filled")]
    [InlineData("outlined")]
    [InlineData("twotone")]
    public void AntDesignIcon_Ok(string category)
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

    [Fact]
    public void ElementIcon_Ok()
    {
        var category = "element";
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
        var iconListFile = Path.Combine(root, $"../../../ElementIconList.razor");
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

            listWriter.WriteLine($"<a><ElementIcon Name=\"{id}\"></ElementIcon><span>{id}</span></a>");
        }
        writer.WriteLine("</svg>");
        writer.Close();
    }

    [GeneratedRegex("svg\">(.*)</svg>")]
    private static partial Regex SvgRegex();
}
