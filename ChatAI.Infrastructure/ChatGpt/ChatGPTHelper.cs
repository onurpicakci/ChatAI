﻿using OpenAI.API;
using OpenAI.API.Completions;
using OpenAI.API.Models;

namespace ChatAI.Helper.ChatGpt;

public class ChatGPTHelper
{
    private readonly OpenAIAPI _openAI;

    public ChatGPTHelper()
    {
        string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        _openAI = new OpenAIAPI(apiKey);
    }

    public async Task<string?> GenerateResponseAsync(string prompt)
    {
        var completionRequest = new CompletionRequest
        {
            Model = "gpt-3.5-turbo",
            Prompt = prompt,
            MaxTokens = 150,
            Temperature = 0.7
        };

        var response = await _openAI.Completions.CreateCompletionAsync(completionRequest);
        return response.Completions.FirstOrDefault()?.Text?.Trim();
    }
}