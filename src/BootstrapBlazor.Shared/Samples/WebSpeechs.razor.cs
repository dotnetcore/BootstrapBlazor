// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/


using System.ComponentModel;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// WebSpeechs
/// </summary>
public partial class WebSpeechs
{

    [NotNull]
    WebSpeech? WebSpeech { get; set; }
    [NotNull]
    WebSpeech? WebSpeechGame { get; set; }
    [NotNull]
    WebSpeech? WebSpeechSynthesis { get; set; }
    [NotNull]
    WebSpeech? WebSpeechDIY { get; set; }

    [DisplayName("识别结果")]
    string? Result { get; set; } = "";

    string? Result2 { get; set; } = "";

    [DisplayName("内容")]
    private string SpeakText { get; set; } = "我们一直与Blazor同行";

    private string? SelectLang { get; set; }

    SpeechRecognitionOption Options { get; set; } = new SpeechRecognitionOption();
    SpeechSynthesisOption Options2 { get; set; } = new SpeechSynthesisOption();

    /// <summary>
    /// 
    /// </summary>
    [NotNull]
    protected Message? Message { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    protected MessageService? MessageService { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    protected async Task ShowBottomMessage(string message, bool error = false)
    {
        await MessageService.Show(new MessageOption()
        {
            Content = message,
            Icon = "fa-solid fa-circle-info",
            Color = error ? Color.Warning : Color.Primary
        }, Message);
    }

    #region SpeechRecognition

    async Task SpeechRecognition() => Result2 = await WebSpeech.SpeechRecognition(option: Options);
    async Task SpeechRecognitionHK() => Result2 = await WebSpeech.SpeechRecognition("zh-HK", Options);
    async Task SpeechRecognitionEN() => Result2 = await WebSpeech.SpeechRecognition("en-US", Options);
    async Task SpeechRecognitionES() => Result2 = await WebSpeech.SpeechRecognition("es-ES", Options);
    async Task SpeechRecognitionStop() => await WebSpeech.SpeechRecognitionStop();

    #endregion


    #region SpeechRecognitionDemo

    async Task SpeechRecognitionDemo() => Result2 = await WebSpeechGame.SpeechRecognitionDemo();
    async Task SpeechRecognitionHKDemo() => Result2 = await WebSpeechGame.SpeechRecognitionDemo("zh-HK");
    async Task SpeechRecognitionENDemo() => Result2 = await WebSpeechGame.SpeechRecognitionDemo("en-US");
    async Task SpeechRecognitionESDemo() => Result2 = await WebSpeechGame.SpeechRecognitionDemo("es-ES");
    async Task SpeechRecognitionDemoStop() => await WebSpeechGame.SpeechRecognitionStop();

    #endregion

    #region SpeechSynthesis

    async Task SpeechSynthesis() => await WebSpeechSynthesis.SpeechSynthesis("你好 blazor,现在是" + NowString());
    async Task SpeechSynthesisHK() => await WebSpeechSynthesis.SpeechSynthesis("早晨 blazor,依家系 " + NowString(), "zh-HK");
    async Task SpeechSynthesisEN() => await WebSpeechSynthesis.SpeechSynthesis("Hello blazor,now is " + NowString(), "en-US");
    async Task SpeechSynthesisES() => await WebSpeechSynthesis.SpeechSynthesis("Hola blazor,ahora es " + NowString(), "es-ES");
    async Task SpeechStop() => await WebSpeech.SpeechStop();

    #endregion

    #region SpeechSynthesisDIY

    async Task SpeechSynthesisDIY() => await WebSpeechDIY.SpeechSynthesis(SpeakText, Options2, "", SelectLang ?? WebVoiceList?.FirstOrDefault()?.VoiceURI);
    async Task SpeechDIYStop() => await WebSpeechDIY.SpeechStop();

    #endregion


    string NowString() => DateTime.Now.ToShortTimeString();

    List<WebVoice>? WebVoiceList { get; set; }
    async Task GetVoiceList()
    {
        WebVoiceList = await WebSpeechDIY.GetVoiceList();
        if (WebVoiceList != null && WebVoiceList.Any()) StateHasChanged();
    }

    private Task OnIsBusy(bool flag)
    {
        StateHasChanged();
        return Task.CompletedTask;
    }

    private void OnChange(ChangeEventArgs val)
    {
        if (val?.Value != null) SelectLang = val.Value.ToString();
    }

    private Task OnResult(string message)
    {
        Result = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private async Task OnStatus(string message)
    {
        Result2 = message;
        await ShowBottomMessage(message);
    }

    private async Task OnError(string message)
    {
        Result2 = message;
        await ShowBottomMessage(message, true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {

        if (firstRender)
        {
            await Task.Delay(500);
            await Task.Delay(1500);
            while (WebVoiceList == null || !WebVoiceList.Any())
            {
                await Task.Delay(100);
                await GetVoiceList();
                if (WebSpeech.SpeechUndefined)
                {
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "SpeechRecognition",
            Description = "语音识别",
            Type = "Task<string>",
            ValueList = "",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "SpeechRecognitionStop",
            Description = "停止语音识别",
            Type = "Task",
            ValueList = "",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "SpeechSynthesis",
            Description = "语音合成",
            Type = "Task",
            ValueList = "",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "SpeechStop",
            Description = "停止语音合成",
            Type = "Task",
            ValueList = "",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnResult",
            Description = "识别完成回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnIsBusy",
            Description = "工作状态回调方法",
            Type = "Func<bool, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnStatus",
            Description = "状态信息回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "OnError",
            Description = "错误回调方法",
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Element",
            Description = "UI界面元素的引用对象,为空则使用整个页面",
            Type = "ElementReference",
            ValueList = "-",
            DefaultValue = "-"
        },
    }; 

}
