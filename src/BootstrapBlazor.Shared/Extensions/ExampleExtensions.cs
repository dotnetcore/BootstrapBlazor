// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 示例代码获取服务
    /// </summary>
    public static class ExampleExtensions
    {
        /// <summary>
        /// 注入版本获取服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddExampleService(this IServiceCollection services)
        {
            services.AddScoped<ExampleService>();
            return services;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class ExampleService
    {
        private HttpClient Client { get; set; }

        private string ServerUrl { get; set; }
        static private Dictionary<string, string> RazorFileCache { get; set; } = new Dictionary<string, string>();
        private int BlockIndex = 0;  //服务注册成scoped后，访问第二个Page不会清零，所以加了个ResetCache函数。TODO:多人不知道是否有bug

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="client"></param>
        /// <param name="options"></param>
        public ExampleService(HttpClient client, IOptions<WebsiteOptions> options)
        {
            Client = client;
            Client.Timeout = TimeSpan.FromSeconds(5);
            Client.BaseAddress = new Uri(options.Value.RepositoryUrl);

            ServerUrl = options.Value.ServerUrl;

            //BlockIndex = 0;//TODO:测试一次请求是否从0开始，
        }
        public void ResetCache()
        {
            BlockIndex = 0;
            //FileCacheName = string.Empty;
            //FileCacheContent = string.Empty;
        }
        /// <summary>
        /// 获得组件代码段的方法
        /// </summary>
        /// <param name="CodeFile">*.razor文件名</param>
        /// <param name="BlockTitle">Block的标题，优先使用，但是有些是变量计算出来的就找不到了</param>
        /// <returns></returns>
        public async Task<string> GetCodeAsync(string CodeFile,string ? BlockTitle)
        {
            var content = string.Empty;
            int myBlockIndex = 0;
            if (!string.IsNullOrWhiteSpace(BlockTitle))
            {
                myBlockIndex = BlockIndex++; //提前+1, 渲染整个文件的时候不会到这里。
            }
            try
            {
                if(CodeFile.EndsWith(".razor"))
                {
                    //RazorCodeFileName = System.IO.Path.ChangeExtension(RazorCodeFileName, ".txt");
                    if(RazorFileCache.ContainsKey(CodeFile))
                    {
                        content = RazorFileCache[CodeFile];
                    }
                }
                if(content==string.Empty)
                {
                    if (OperatingSystem.IsBrowser())
                    {
                        Client.BaseAddress = new Uri($"{ServerUrl}/api/");
                        content = await Client.GetStringAsync($"Code?fileName={CodeFile}");
                    }
                    else
                    {
                        content = await Client.GetStringAsync(CodeFile);
                    }
                    //if (RazorCodeFileName.EndsWith(".txt"))
                    if (CodeFile.EndsWith(".razor"))
                    {
                        //lock(this)
                        {
                            if (!RazorFileCache.ContainsKey(CodeFile))
                            {
                                RazorFileCache[CodeFile] = content  ;
                                //BlockIndex = 0;//BlockTitle==null?0:1;
                            }
                        }
                    }
                }
                if(!string.IsNullOrWhiteSpace(BlockTitle))
                {
                    string[] segments = content.Split(new string[] { "<Block ", "</Block>" }, StringSplitOptions.None);
                    for(int i=1;i<segments.Length;i+=2)
                    {
                        string codeSeg = CodeFile+"("+ myBlockIndex.ToString()+")"+ BlockTitle + "\r\n"+ "<Block " + segments[i]+ "</Block>";
                        if (codeSeg.Contains("Title=\"" + BlockTitle + "\""))
                            return codeSeg;
                    }
                    if(BlockIndex*2-1<segments.Length)
                        return CodeFile + "[" + myBlockIndex.ToString() + "]" + BlockTitle + "\r\n" + "<Block " + segments[myBlockIndex * 2+1] + "</Block>";
                    //按BlockIndex匹配失败，暂时先返回所有的内容。
                    return CodeFile + "{" + myBlockIndex.ToString() + "}" + BlockTitle + "\r\n" + content;
                }
            }
            catch (HttpRequestException) { content = "网络错误"; }
            catch (TaskCanceledException) { }
            catch (Exception) { }
            return content;
        }
    }
}
