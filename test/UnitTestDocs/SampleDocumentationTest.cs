// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Controllers.Api;
using BootstrapBlazor.Shared.Data;
using BootstrapBlazor.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;

namespace UnitTestDocs;

public class SampleDocumentationTest
{
    [Theory]
    [InlineData("../badge.razor")]
    [InlineData("badge.razor/../../Program.cs")]
    [InlineData("badge.json")]
    [InlineData("missing.razor")]
    public async Task Code_RejectsUnconfiguredFiles(string codeFile)
    {
        var service = new DocumentationService();
        var controller = new SampleDocumentationController(service, Options.Create(new WebsiteOptions()));

        Assert.IsType<NotFoundResult>(await controller.Code(codeFile));
        Assert.Null(service.CodeFile);
    }

    [Theory]
    [InlineData("badge.razor")]
    [InlineData("badge.razor.cs")]
    public async Task Code_ReturnsConfiguredSample(string codeFile)
    {
        var service = new DocumentationService();
        var controller = new SampleDocumentationController(service, Options.Create(new WebsiteOptions()));

        var result = Assert.IsType<ContentResult>(await controller.Code(codeFile));

        Assert.Equal(codeFile, service.CodeFile);
        Assert.Equal("sample source", result.Content);
    }

    [Theory]
    [InlineData("System.String")]
    [InlineData("BootstrapBlazor.Components.Color")]
    [InlineData("BootstrapBlazor.Components.ClientInfo")]
    [InlineData("BootstrapBlazor.Components.SortableList, BootstrapBlazor.Sortable")]
    [InlineData("BootstrapBlazor.Server.Components.Samples.Tabs")]
    public async Task Attributes_RejectsNonLibraryComponents(string typeName)
    {
        var controller = new SampleDocumentationController(new DocumentationService(), Options.Create(new WebsiteOptions()));

        Assert.IsType<NotFoundResult>(await controller.Attributes(typeName));
    }

    [Theory]
    [InlineData(typeof(CountUpOption))]
    [InlineData(typeof(NotificationItem))]
    [InlineData(typeof(MessageOption))]
    [InlineData(typeof(ConsoleMessageItem))]
    [InlineData(typeof(TimelineItem))]
    [InlineData(typeof(SortableList))]
    [InlineData(typeof(SortableOption))]
    [InlineData(typeof(SortableEvent))]
    [InlineData(typeof(SortableListItem))]
    [InlineData(typeof(Splitting))]
    [InlineData(typeof(BarcodeGenerator))]
    [InlineData(typeof(BarcodeGeneratorOption))]
    [InlineData(typeof(OnScreenKeyboard))]
    [InlineData(typeof(KeyboardOption))]
    [InlineData(typeof(QRCode))]
    [InlineData(typeof(SelectProvince))]
    [InlineData(typeof(SelectCity))]
    [InlineData(typeof(SelectRegion))]
    [InlineData(typeof(Mermaid))]
    [InlineData(typeof(MouseFollower))]
    [InlineData(typeof(BarcodeReader))]
    [InlineData(typeof(DriverJs))]
    [InlineData(typeof(SmilesDrawer))]
    [InlineData(typeof(TreeViewItem<TreeFoo>))]
    [InlineData(typeof(ImageCropper))]
    [InlineData(typeof(CodeEditor))]
    [InlineData(typeof(BootstrapBlazor.Components.Markdown))]
    [InlineData(typeof(MindMap))]
    [InlineData(typeof(Vditor))]
    [InlineData(typeof(VditorOptions))]
    [InlineData(typeof(VditorMode))]
    [InlineData(typeof(VditorIconStyle))]
    [InlineData(typeof(Gantt))]
    [InlineData(typeof(EmbedPDF))]
    [InlineData(typeof(JitViewer))]
    [InlineData(typeof(PdfViewer))]
    [InlineData(typeof(Live2DDisplay))]
    [InlineData(typeof(VideoPlayer))]
    [InlineData(typeof(Player))]
    public async Task Attributes_ReturnsSupportedSampleOptions(Type type)
    {
        var service = new DocumentationService();
        var controller = new SampleDocumentationController(service, Options.Create(new WebsiteOptions()));

        Assert.IsType<OkObjectResult>(await controller.Attributes(type.FullName!));
        Assert.Equal(type, service.AttributeType);
    }

    [Fact]
    public async Task Attributes_PreservesDescriptionLinksOverHttp()
    {
        using var handler = new DocumentationHandler(request =>
        {
            Assert.Contains("typeName=BootstrapBlazor.Components.Badge", request.RequestUri!.Query);
            Assert.Contains("culture=", request.RequestUri.Query);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(new[]
                {
                    new AttributeItem
                    {
                        Name = "Color",
                        DescriptionParts = [new AttributeDescriptionPart("IsPill", "IsPill")]
                    }
                })
            };
        });
        using var client = new HttpClient(handler) { BaseAddress = new Uri("https://example.test/") };
        var service = new HttpSampleDocumentationService(client);

        var items = await service.GetAttributesAsync(typeof(Badge));

        var item = Assert.Single(items);
        Assert.Equal(new AttributeDescriptionPart("IsPill", "IsPill"), Assert.Single(item.DescriptionParts!));
    }

    private sealed class DocumentationService : ISampleDocumentationService
    {
        public string? CodeFile { get; private set; }

        public Type? AttributeType { get; private set; }

        public Task<string> GetCodeAsync(string codeFile)
        {
            CodeFile = codeFile;
            return Task.FromResult("sample source");
        }

        public Task<List<AttributeItem>> GetAttributesAsync(Type componentType)
        {
            AttributeType = componentType;
            return Task.FromResult(new List<AttributeItem>());
        }
    }

    private sealed class DocumentationHandler(Func<HttpRequestMessage, HttpResponseMessage> send) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
            Task.FromResult(send(request));
    }
}
