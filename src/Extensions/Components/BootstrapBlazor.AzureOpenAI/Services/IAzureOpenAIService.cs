using Azure.AI.OpenAI;

namespace BootstrapBlazor.Components;

/// <summary>
/// IAzureOpenAIService 接口
/// </summary>
public interface IAzureOpenAIService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<AzureOpenAIChatMessage>> GetChatCompletionsAsync(string context, CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task CreateNewTopic();
}
