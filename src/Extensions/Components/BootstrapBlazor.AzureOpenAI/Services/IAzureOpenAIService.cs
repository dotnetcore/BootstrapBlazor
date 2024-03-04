using Azure.AI.OpenAI;

namespace BootstrapBlazor.Components;

/// <summary>
/// IAzureOpenAIService 接口
/// </summary>
public interface IAzureOpenAIService
{
    /// <summary>
    /// 获得聊天响应集合
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<AzureOpenAIChatMessage>> GetChatCompletionsAsync(string context, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获得聊天响应集合流
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    IAsyncEnumerable<AzureOpenAIChatMessage> GetChatCompletionsStreamingAsync(string context, CancellationToken cancellationToken = default);

    /// <summary>
    /// 创建话题
    /// </summary>
    /// <returns></returns>
    Task CreateNewTopic();
}
