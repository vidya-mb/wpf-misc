using Azure.AI.OpenAI;
using OpenAI;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EnhancedTextApp
{
    internal static class LLMClientManager
    {

        public static ChatClient? GetChatClient(DependencyObject obj, string? deploymentName = null)
        {
            ArgumentNullException.ThrowIfNull(obj);

            if(string.IsNullOrEmpty(deploymentName) && string.IsNullOrEmpty(DeploymentName))
            {
                throw new ArgumentNullException(nameof(DeploymentName), "Either pass in the deploymentName or set the LLMClientManager.DeploymentName property");
            }

            if(string.IsNullOrEmpty(deploymentName))
            {
                deploymentName = DeploymentName;
            }

            if (obj is FrameworkElement fe)
            {
                ChatClient client;
                if (!_chatClientsCache.TryGetValue(fe, out client))
                {
                    client = AzureOpenAIClientInstance.GetChatClient(deploymentName);
                    _chatClientsCache[fe] = client;
                }

                return client;
            }

            return null;
        }

        public static OpenAIClient OpenAIClientInstance
        {
            get
            {
                if(_openAIClient == null)
                {
                    _openAIClient = LLMClientFactory.CreateOpenAIClient();
                }

                return _openAIClient;
            }
        }

        public static AzureOpenAIClient AzureOpenAIClientInstance
        {
            get
            {
                if (_azureOpenAIClient == null)
                {
                    _azureOpenAIClient = LLMClientFactory.CreateAzureOpenAIClient();
                }

                return _azureOpenAIClient;
            }
        }

        public static string DeploymentName
        {
            get { return _deploymentName; }
            set { _deploymentName = value; }
        }


        private static string _deploymentName;
        private static OpenAIClient? _openAIClient;
        private static AzureOpenAIClient? _azureOpenAIClient;

        public static Dictionary<DependencyObject, ChatClient> _chatClientsCache = new Dictionary<DependencyObject, ChatClient>();
    }
}
