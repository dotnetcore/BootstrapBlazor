// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// QueryPageOptions json converter
/// </summary>
public class JsonQueryPageOptionsConverter : JsonConverter<QueryPageOptions>
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override QueryPageOptions? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var ret = new QueryPageOptions();
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    if (propertyName == "searchText")
                    {
                        reader.Read();
                        ret.SearchText = reader.GetString();
                    }
                    else if (propertyName == "sortName")
                    {
                        reader.Read();
                        ret.SortName = reader.GetString();
                    }
                    else if (propertyName == "sortOrder")
                    {
                        reader.Read();
                        if (Enum.TryParse<SortOrder>(reader.GetString(), out var sortOrder))
                        {
                            ret.SortOrder = sortOrder;
                        }
                    }
                    else if (propertyName == "sortList")
                    {
                        reader.Read();
                        var val = JsonSerializer.Deserialize<List<string>>(ref reader, options);
                        if (val != null)
                        {
                            ret.SortList.AddRange(val);
                        }
                    }
                    else if (propertyName == "advancedSortList")
                    {
                        reader.Read();
                        var val = JsonSerializer.Deserialize<List<string>>(ref reader, options);
                        if (val != null)
                        {
                            ret.AdvancedSortList.AddRange(val);
                        }
                    }
                    else if (propertyName == "searchModel")
                    {
                        reader.Read();
                        var val = JsonSerializer.Deserialize<object>(ref reader, options);
                        if (val != null)
                        {
                            ret.SearchModel = val;
                        }
                    }
                    else if (propertyName == "pageIndex")
                    {
                        reader.Read();
                        ret.PageIndex = reader.GetInt32();
                    }
                    else if (propertyName == "startIndex")
                    {
                        reader.Read();
                        ret.StartIndex = reader.GetInt32();
                    }
                    else if (propertyName == "pageItems")
                    {
                        reader.Read();
                        ret.PageItems = reader.GetInt32();
                    }
                    else if (propertyName == "isPage")
                    {
                        reader.Read();
                        ret.IsPage = reader.GetBoolean();
                    }
                    else if (propertyName == "isVirtualScroll")
                    {
                        reader.Read();
                        ret.IsVirtualScroll = reader.GetBoolean();
                    }
                    else if (propertyName == "searches")
                    {
                        reader.Read();
                        if (reader.TokenType == JsonTokenType.StartArray)
                        {
                            while (reader.Read())
                            {
                                if (reader.TokenType == JsonTokenType.EndArray)
                                {
                                    break;
                                }
                                var val = JsonSerializer.Deserialize<SearchFilterAction>(ref reader, options);
                                if (val != null)
                                {
                                    ret.Searches.Add(val);
                                }
                            }
                        }
                    }
                    else if (propertyName == "customerSearches")
                    {
                        reader.Read();
                        if (reader.TokenType == JsonTokenType.StartArray)
                        {
                            while (reader.Read())
                            {
                                if (reader.TokenType == JsonTokenType.EndArray)
                                {
                                    break;
                                }
                                var val = JsonSerializer.Deserialize<SearchFilterAction>(ref reader, options);
                                if (val != null)
                                {
                                    ret.CustomerSearches.Add(val);
                                }
                            }
                        }
                    }
                    else if (propertyName == "advanceSearches")
                    {
                        reader.Read();
                        if (reader.TokenType == JsonTokenType.StartArray)
                        {
                            while (reader.Read())
                            {
                                if (reader.TokenType == JsonTokenType.EndArray)
                                {
                                    break;
                                }
                                var val = JsonSerializer.Deserialize<SearchFilterAction>(ref reader, options);
                                if (val != null)
                                {
                                    ret.AdvanceSearches.Add(val);
                                }
                            }
                        }
                    }
                    else if (propertyName == "filters")
                    {
                        reader.Read();
                        if (reader.TokenType == JsonTokenType.StartArray)
                        {
                            while (reader.Read())
                            {
                                if (reader.TokenType == JsonTokenType.EndArray)
                                {
                                    break;
                                }
                                var val = JsonSerializer.Deserialize<SearchFilterAction>(ref reader, options);
                                if (val != null)
                                {
                                    ret.Filters.Add(val);
                                }
                            }
                        }
                    }
                    else if (propertyName == "isFirstQuery")
                    {
                        reader.Read();
                        ret.IsFirstQuery = reader.GetBoolean();
                    }
                    else if (propertyName == "isTriggerByPagination")
                    {
                        reader.Read();
                        ret.IsTriggerByPagination = reader.GetBoolean();
                    }
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    /// <exception cref="NotImplementedException"></exception>
    public override void Write(Utf8JsonWriter writer, QueryPageOptions value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        if (!string.IsNullOrEmpty(value.SearchText))
        {
            writer.WriteString("searchText", value.SearchText);
        }
        if (!string.IsNullOrEmpty(value.SortName))
        {
            writer.WriteString("sortName", value.SortName);
        }
        if (value.SortOrder != SortOrder.Unset)
        {
            writer.WriteString("sortOrder", value.SortOrder.ToString());
        }
        if (value.SortList.Count != 0)
        {
            writer.WritePropertyName("sortList");
            writer.WriteRawValue(JsonSerializer.Serialize(value.SortList, options));
        }
        if (value.AdvancedSortList.Count != 0)
        {
            writer.WritePropertyName("advancedSortList");
            writer.WriteRawValue(JsonSerializer.Serialize(value.AdvancedSortList, options));
        }
        if (value.SearchModel != null)
        {
            writer.WritePropertyName("searchModel");
            writer.WriteRawValue(JsonSerializer.Serialize(value.SearchModel, options));
        }
        if (value.PageIndex > 1)
        {
            writer.WriteNumber("pageIndex", value.PageIndex);
        }
        if (value.StartIndex != 0)
        {
            writer.WriteNumber("startIndex", value.StartIndex);
        }
        if (value.PageItems != 20)
        {
            writer.WriteNumber("pageItems", value.PageItems);
        }
        if (value.IsPage)
        {
            writer.WriteBoolean("isPage", value.IsPage);
        }
        if (value.IsVirtualScroll)
        {
            writer.WriteBoolean("isVirtualScroll", value.IsVirtualScroll);
        }
        if (value.Searches.Count != 0)
        {
            writer.WriteStartArray("searches");
            foreach (var filter in value.Searches)
            {
                writer.WriteRawValue(JsonSerializer.Serialize(filter, options));
            }
            writer.WriteEndArray();
        }
        if (value.CustomerSearches.Count != 0)
        {
            writer.WriteStartArray("customerSearches");
            foreach (var filter in value.CustomerSearches)
            {
                writer.WriteRawValue(JsonSerializer.Serialize(filter, options));
            }
            writer.WriteEndArray();
        }
        if (value.AdvanceSearches.Count != 0)
        {
            writer.WriteStartArray("advanceSearches");
            foreach (var filter in value.AdvanceSearches)
            {
                writer.WriteRawValue(JsonSerializer.Serialize(filter, options));
            }
            writer.WriteEndArray();
        }
        if (value.Filters.Count != 0)
        {
            writer.WriteStartArray("filters");
            foreach (var filter in value.Filters)
            {
                writer.WriteRawValue(JsonSerializer.Serialize(filter, options));
            }
            writer.WriteEndArray();
        }
        if (value.IsFirstQuery)
        {
            writer.WriteBoolean("isFirstQuery", value.IsFirstQuery);
        }
        if (value.IsTriggerByPagination)
        {
            writer.WriteBoolean("isTriggerByPagination", value.IsFirstQuery);
        }
        writer.WriteEndObject();
    }
}
