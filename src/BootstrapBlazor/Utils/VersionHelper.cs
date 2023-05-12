// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Xml;

namespace BootstrapBlazor.Utils;

static class VersionHelper
{
    /// <summary>
    /// 通过项目文件获得版本号信息
    /// </summary>
    /// <param name="projectFile"></param>
    /// <returns></returns>
    public static string GetVersion(string projectFile)
    {
        string? ver = null;
        if (File.Exists(projectFile))
        {
            try
            {
                var doc = new System.Xml.XmlDocument();
                doc.Load(projectFile);
                var root = doc.DocumentElement;
                if (root != null)
                {
                    foreach (XmlNode node in root.ChildNodes)
                    {
                        if (node.Name == "PropertyGroup")
                        {
                            foreach (XmlNode item in node.ChildNodes)
                            {
                                if (item.Name == "Version")
                                {
                                    ver = item.InnerText;
                                    break;
                                }
                            }
                        }
                        if (ver != null)
                        {
                            break;
                        }
                    }
                }
            }
            catch { }
        }
        return ver ?? "7.0.0.0";
    }
}
