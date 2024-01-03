// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Data;

namespace BootstrapBlazor.Server.Components.Samples.Tutorials.Translation;

class LanguageDataTable
{
    private readonly Dictionary<string, string?> _dataCache = [];

    private readonly Dictionary<string, string?> _newValueCache = [];

    private readonly Dictionary<string, LanguageWriter> _writerCache = [];

    private readonly List<string> _selectedLanguages = default!;

    private DataTable _dataTable = default!;

    public Func<Task>? OnUpdate { get; set; }

    private static readonly string[] enLanguageName = ["en-US"];

    public LanguageDataTable(List<string> languages)
    {
        _selectedLanguages = languages;

        CreateTable();
    }

    private void CreateTable()
    {
        _dataTable = new DataTable();
        _dataTable.Columns.Add(new DataColumn("SectionName", typeof(string)));
        _dataTable.Columns.Add(new DataColumn("KeyName", typeof(string)));
        _dataTable.Columns.Add(new DataColumn("en-US", typeof(string)));
        foreach (var language in _selectedLanguages)
        {
            _dataTable.Columns.Add(new DataColumn(language, typeof(string)));

            // 存储是否翻译 true 时表示翻译值
            _dataTable.Columns.Add(new DataColumn(GetColumnName(language), typeof(bool)));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public DataTableDynamicContext CreateContext()
    {
        return new DataTableDynamicContext(_dataTable, (context, col) =>
        {
            var fieldName = col.GetFieldName();
            if (fieldName == "SectionName")
            {
                col.Text = "SectionName";
                col.Fixed = true;
                col.Width = 200;
                col.CssClass = "col-section";
                col.Readonly = true;
            }
            if (fieldName == "KeyName")
            {
                col.Text = "ItemName";
                col.Fixed = true;
                col.Width = 140;
                col.CssClass = "col-key";
                col.Readonly = true;
            }
            if (fieldName == "en-US")
            {
                col.Fixed = true;
                col.CssClass = "col-lang";
            }

            col.ComponentType = typeof(Textarea);
            col.Rows = 3;

            if (_selectedLanguages.Union(new List<string>() { "en-US" }).Contains(fieldName))
            {
                col.OnCellRender = args =>
                {
                    args.Class = GetCellClassString(args.Row, args.ColumnName);
                };
            }

            if (col.PropertyType == typeof(bool))
            {
                col.Visible = false;
            }
        })
        {
            OnValueChanged = async (item, col, val) =>
            {
                // 将旧值保存到缓存中
                string key = $"{item.GetValue("SectionName")}.{item.GetValue("KeyName")}-{col.GetFieldName()}";
                if (!_dataCache.TryGetValue(key, out var value))
                {
                    var oldValue = item.GetValue(col.GetFieldName())?.ToString();
                    _dataCache.Add(key, oldValue);
                }
                else if (value == val?.ToString())
                {
                    _dataCache.Remove(key);
                }
                item.SetValue(col.GetFieldName(), val);

                // 将新值保存到缓存中
                if (!_newValueCache.ContainsKey(key))
                {
                    _newValueCache.Add(key, val?.ToString());
                }
                else
                {
                    _newValueCache[key] = val?.ToString();
                }

                if (OnUpdate != null)
                {
                    await OnUpdate();
                }
            }
        };

        string? GetCellClassString(object? row, string fieldName)
        {
            string? ret = null;
            if (row is DynamicObject item)
            {
                string key = $"{item.GetValue("SectionName")}.{item.GetValue("KeyName")}-{fieldName}";
                if (string.IsNullOrEmpty(item.GetValue(fieldName)?.ToString()))
                {
                    ret = "col-miss";
                }
                if (item.GetValue(GetColumnName(fieldName))?.ToString() == "True")
                {
                    ret = "col-temp";
                }
                if (_dataCache.TryGetValue(key, out var value))
                {
                    var v = value;
                    if (v != item.GetValue(fieldName)?.ToString())
                    {
                        ret = "col-not-save";
                    }
                }
            }
            return ret;
        }
    }

    /// <summary>
    /// 从文件中加载数据
    /// </summary>
    /// <returns></returns>
    public Task LoadAsync(string jsonFileDirectory) => Task.Run(() =>
    {
        _dataTable.Rows.Clear();

        // 解析 en-US.json 文件
        var fileName = Path.Combine(jsonFileDirectory, "en-US.json");
        var manager = new ConfigurationManager();
        manager.AddJsonFile(fileName);

        foreach (var section in manager.GetChildren())
        {
            foreach (var item in section.GetChildren())
            {
                // 生成行
                var row = _dataTable.NewRow();
                row["SectionName"] = section.Key;
                row["KeyName"] = item.Key;
                row["en-US"] = item.Value;

                // 生成对应语言资源文件
                foreach (var language in _selectedLanguages)
                {
                    // 读取现有目标文件
                    var targetLanguageManager = new ConfigurationManager();
                    var targetLanguageFileName = Path.Combine(jsonFileDirectory, $"{language}.json");
                    targetLanguageManager.AddJsonFile(targetLanguageFileName, true);

                    // 读取临时文件
                    var tempFileName = Path.Combine(jsonFileDirectory, $"{language}.temp");
                    targetLanguageManager.AddJsonFile(tempFileName, true);

                    // 读取修复文件
                    var fixFileName = Path.Combine(jsonFileDirectory, $"{language}.fix");
                    targetLanguageManager.AddJsonFile(fixFileName, true);

                    var fixManager = new ConfigurationManager();
                    fixManager.AddJsonFile(fixFileName, true);

                    var languageSection = targetLanguageManager.GetSection(section.Key);
                    if (languageSection.Exists())
                    {
                        // 读取 item
                        var languageItem = languageSection.GetChildren().FirstOrDefault(i => i.Key == item.Key);
                        if (languageItem != null)
                        {
                            // 读取缓存值
                            string key = $"{languageSection.Key}.{languageItem.Key}-{language}";
                            if (_newValueCache.TryGetValue(key, out var value))
                            {
                                row[language] = value;
                            }
                            else
                            {
                                row[language] = languageItem.Value;
                            }

                            // 标记翻译值 true 表示翻译还未保存 标记为紫色
                            if (File.Exists(tempFileName))
                            {
                                row[GetColumnName(language)] = true;
                            }

                            var fixSection = fixManager.GetSection(section.Key);
                            if (fixSection.Exists())
                            {
                                var fixItem = fixSection.GetChildren().FirstOrDefault(i => i.Key == item.Key);
                                if (fixItem != null)
                                {
                                    row[GetColumnName(language)] = true;
                                }
                            }
                        }
                    }
                }

                _dataTable.Rows.Add(row);
            }
        }
        _dataTable.AcceptChanges();
    });

    /// <summary>
    /// 过滤行
    /// </summary>
    /// <param name="row"></param>
    /// <returns>返回真时 表示数据不合法</returns>
    public bool FilterRow(DynamicObject row)
    {
        bool ret = false;
        for (var index = 0; index < _dataTable.Columns.Count - 3; index += 2)
        {
            var cell = row.GetValue(_dataTable.Columns[3 + index].ColumnName);
            if (string.IsNullOrEmpty(cell?.ToString()))
            {
                // 空值
                ret = true;
                break;
            }

            cell = row.GetValue(_dataTable.Columns[4 + index].ColumnName);
            if (cell?.ToString() == "True")
            {
                // 表示为 翻译值
                ret = true;
                break;
            }

            // 修改值
            var key = $"{row.GetValue("SectionName")}.{row.GetValue("KeyName")}-{_dataTable.Columns[3 + index].ColumnName}";
            if (_dataCache.ContainsKey(key))
            {
                ret = true;
                break;
            }
        }
        return ret;
    }

    /// <summary>
    /// 保存到内存流中
    /// </summary>
    /// <returns></returns>
    public async Task SaveAsync(string jsonFileDirectory)
    {
        var languages = enLanguageName.Union(_selectedLanguages);
        foreach (DataRow row in _dataTable.Rows)
        {
            // 生成对应语言资源文件
            foreach (var language in languages)
            {
                var sectionName = row["SectionName"].ToString();
                if (string.IsNullOrEmpty(sectionName)) { continue; }

                var itemName = row["KeyName"].ToString();
                if (string.IsNullOrEmpty(itemName)) { continue; }

                var value = row[language].ToString();
                if (string.IsNullOrEmpty(value)) { continue; }

                // 写入 Json
                var writer = GetWriter(language);
                writer.WriteSectionItem(sectionName, itemName, value);
            }
        }

        foreach (var language in languages)
        {
            var writer = GetWriter(language);
            await writer.SaveAsync(Path.Combine(jsonFileDirectory, $"{language}.json"));

            var tempFile = Path.Combine(jsonFileDirectory, $"{language}.temp");
            DeleteFile(tempFile);

            var fixFile = Path.Combine(jsonFileDirectory, $"{language}.fix");
            DeleteFile(fixFile);
        }

        // clear cache
        _dataCache.Clear();
        foreach (var writer in _writerCache.Values)
        {
            writer.Dispose();
        }
        _writerCache.Clear();

        static void DeleteFile(string targetFileName)
        {
            if (File.Exists(targetFileName))
            {
                try { File.Delete(targetFileName); }
                catch { }
            }
        }
    }

    private LanguageWriter GetWriter(string language)
    {
        LanguageWriter? writer;
        if (_writerCache.TryGetValue(language, out var value))
        {
            writer = value;
        }
        else
        {
            writer = new LanguageWriter();
            _writerCache.Add(language, writer);
        }
        return writer;
    }

    private static string GetColumnName(string language) => $"{language}-state";
}
