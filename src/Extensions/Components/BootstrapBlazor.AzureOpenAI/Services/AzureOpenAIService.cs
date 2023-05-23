// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace BootstrapBlazor.Components;

class AzureOpenAIService : IAzureOpenAIService
{
    private AzureOpenAIOption Options { get; set; }

    private OpenAIClient? Client { get; set; }

    private ChatCompletionsOptions ChatCompletionsOptions { get; } = new()
    {
        //Temperature = 0.5f, //浮点数，控制模型的输出的多样性。值越高，输出越多样化。值越低，输出越简单。默认值为 0.5
        //MaxTokens = 500,//完成时生成的最大令牌数
        //NucleusSamplingFactor = 0.95f,
        //FrequencyPenalty = 0,
        //PresencePenalty = 0,
    };

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    public AzureOpenAIService(IOptions<AzureOpenAIOption> options)
    {
        Options = options.Value;

        if (string.IsNullOrEmpty(Options.Endpoint))
        {
            throw new InvalidOperationException($"{nameof(AzureOpenAIOption.Endpoint)} is null or empty");
        }
        if (string.IsNullOrEmpty(Options.Key))
        {
            throw new InvalidOperationException($"{nameof(AzureOpenAIOption.Key)} is null or empty");
        }
        if (string.IsNullOrEmpty(Options.DeploymentName))
        {
            throw new InvalidOperationException($"{nameof(AzureOpenAIOption.DeploymentName)} is null or empty");
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<AzureOpenAIChatMessage>> GetChatCompletionsAsync(string context, CancellationToken cancellationToken = default)
    {
        ChatCompletionsOptions.Messages.Add(new ChatMessage(ChatRole.User, context));

        Client ??= new(new Uri(Options.Endpoint), new AzureKeyCredential(Options.Key));

        var completionsResponse = await Client.GetChatCompletionsAsync(Options.DeploymentName, ChatCompletionsOptions, cancellationToken);
        return completionsResponse.Value.Choices.Select(choice => new AzureOpenAIChatMessage()
        {
            Content = choice.Message.Content,
            Role = choice.Message.Role
        });
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async IAsyncEnumerable<AzureOpenAIChatMessage> GetChatCompletionsStreamingAsync(string context, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        ChatCompletionsOptions.Messages.Add(new ChatMessage(ChatRole.User, context));
        Client ??= new(new Uri(Options.Endpoint), new AzureKeyCredential(Options.Key));

        var completionsResponse = await Client.GetChatCompletionsStreamingAsync(Options.DeploymentName, ChatCompletionsOptions, cancellationToken);
        if (!cancellationToken.IsCancellationRequested)
        {
            await foreach (var choice in completionsResponse.Value.GetChoicesStreaming())
            {
                await foreach (var message in choice.GetMessageStreaming(cancellationToken))
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        yield return new AzureOpenAIChatMessage
                        {
                            Content = message.Content,
                            Role = message.Role
                        };
                    }
                }
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public Task CreateNewTopic()
    {
        ChatCompletionsOptions.Messages.Clear();

        return Task.CompletedTask;
    }
}
