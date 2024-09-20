using Azure.AI.OpenAI;
using Azure;
using OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnhancedTextApp
{
    public static class LLMClientFactory
    {
        public static OpenAIClient CreateOpenAIClient()
        {
            string? _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException(@"API key is not set in the environment variables.
                                                        Please set the OPENAI_API_KEY variable");
            }

            return new OpenAIClient(_apiKey, new OpenAIClientOptions());
        }

        public static AzureOpenAIClient CreateAzureOpenAIClient()
        {
            string? endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
            string? apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");

            if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException(@"Endpoint or API key is not set in the environment variables.
                                                        Please set the AZURE_OPENAI_ENDPOINT and AZURE_OPENAI_API_KEY variables");
            }

            return new AzureOpenAIClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
        }

    }
}
