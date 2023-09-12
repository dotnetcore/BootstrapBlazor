// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.RegularExpressions;

namespace UniTest.Sass;

public partial class VariableTest
{
    private ITestOutputHelper _outputHelper;

    public VariableTest(ITestOutputHelper testOutputHelper)
    {
        _outputHelper = testOutputHelper;
    }

    [Fact]
    public void Variable_Ok()
    {
        // 检查 Sass 文件内变量是否定义
        var rootPath = Path.Combine(AppContext.BaseDirectory, "../../../../../src/BootstrapBlazor");
        var sassFilePath = Path.Combine(rootPath, "Components");
        Assert.True(Directory.Exists(sassFilePath));

        var variableFile = Path.Combine(rootPath, "wwwroot/scss/bootstrap.blazor.scss");
        Assert.True(File.Exists(variableFile));

        // 获取所有 Sass 文件所有变量
        var variables = CreateVariableTable();
        Assert.NotEmpty(variables);

        var regex = VariableRegex();
        var mixRegex = MixVariableRegex();
        var noMatches = new List<string>();
        foreach (var scss in Directory.EnumerateFiles(sassFilePath, "*.razor.scss", SearchOption.AllDirectories))
        {
            ValidateVariable(scss, variables);
        }
        Assert.Empty(noMatches);

        void ValidateVariable(string scssFile, List<string> variables)
        {
            using var fs = new StreamReader(File.OpenRead(scssFile));
            while (!fs.EndOfStream)
            {
                var item = fs.ReadLine();
                if (!string.IsNullOrEmpty(item))
                {
                    // 兼容 @mixin
                    CheckMixVariable(item);

                    // #{$alert-icon-margin-right}
                    var matches = regex.Matches(item);
                    if (matches.Any())
                    {
                        var v = matches.Where(i => !variables.Contains(i.Groups[1].Value)).Select(i => i.Groups[1].Value);
                        if (v.Any())
                        {
                            noMatches.AddRange(v);
                        }
                    }
                }
            }
            fs.Close();


            void CheckMixVariable(string item)
            {
                // @mixin cell-margin($direction)
                var matches = mixRegex.Matches(item);
                if (item.Contains("@mixin "))
                {
                    if (matches.Any())
                    {
                        var groups = matches[0];
                        for (int index = 1; index < groups.Groups.Count; index++)
                        {
                            var v = groups.Groups[index].Value;
                            if (!variables.Contains(v))
                            {
                                variables.Add(v);
                            }
                        }
                    }
                }
            }
        }

        List<string> CreateVariableTable()
        {
            var ret = new List<string>();
            using var fs = new StreamReader(File.OpenRead(variableFile));
            while (!fs.EndOfStream)
            {
                var item = fs.ReadLine();
                if (item != null && item.Contains(':'))
                {
                    var variable = item.Split(':')[0];
                    if (variable.StartsWith('$'))
                    {
                        ret.Add(variable);
                    }
                }
            }
            fs.Close();
            return ret;
        }
    }

    [GeneratedRegex(@"#\{(\$\w+)\}")]
    private static partial Regex VariableRegex();

    [GeneratedRegex(@"(\$\w+)(?:,\s*(\$\w+))?(,\s*(\$\w+))?")]
    private static partial Regex MixVariableRegex();
}
