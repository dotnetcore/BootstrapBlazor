// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace BootstrapBlazor.Server.Components.Samples.Tutorials.Translation;

class LanguageWriter : IDisposable
{
    private readonly MemoryStream _stream = default!;

    private readonly Utf8JsonWriter _writer = default!;

    public static JsonWriterOptions EncoderOptions = new()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        Indented = true
    };

    public LanguageWriter()
    {
        _stream = new MemoryStream();

        _writer = new Utf8JsonWriter(_stream, EncoderOptions);
        _writer.WriteStartObject();
    }

    public Utf8JsonWriter Writer => _writer;

    public string? LastSectionName { get; set; }

    public async Task<Stream> SaveAsync()
    {
        if (!string.IsNullOrEmpty(LastSectionName))
        {
            // end section 
            _writer.WriteEndObject();
        }

        // end object
        _writer.WriteEndObject();
        await _writer.FlushAsync();
        _stream.Position = 0;
        return _stream;
    }

    public async Task SaveAsync(string fileName)
    {
        await SaveAsync();
        await using var fileWriter = File.CreateText(fileName);
        await _stream.CopyToAsync(fileWriter.BaseStream);
        await fileWriter.WriteLineAsync();
        fileWriter.Close();
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _writer.Dispose();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

static class LanguageWriterExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="sectionName"></param>
    /// <param name="itemName"></param>
    /// <param name="value"></param>
    public static void WriteSectionItem(this LanguageWriter writer, string sectionName, string itemName, string value)
    {
        if (writer.LastSectionName != sectionName)
        {
            if (!string.IsNullOrEmpty(writer.LastSectionName))
            {
                writer.Writer.WriteEndObject();
            }

            writer.LastSectionName = sectionName;
            writer.Writer.WriteStartObject(sectionName);
        }

        writer.Writer.WriteString(itemName, value);
    }
}
