using Microsoft.KernelMemory;

namespace HackathonT4.Config
{
    public class KernalMemoryConfig
    {
        public static IKernelMemory GetMemoryConnector(IConfiguration configuration)
        {
            return new KernelMemoryBuilder()
                .WithAzureOpenAITextEmbeddingGeneration(new AzureOpenAIConfig
                {
                    APIType = AzureOpenAIConfig.APITypes.EmbeddingGeneration,
                    Endpoint = "https://ai-hack-team4.openai.azure.com/",
                    Deployment = "HackEmbedding",
                    Auth = AzureOpenAIConfig.AuthTypes.APIKey,
                    APIKey = configuration.GetSection("ApiKey").ToString() ?? string.Empty
                })
                .WithAzureOpenAITextGeneration(new AzureOpenAIConfig
                {
                    APIType = AzureOpenAIConfig.APITypes.ChatCompletion,
                    Endpoint = "https://ai-hack-team4.openai.azure.com/",
                    Deployment = "gpt4_vision",
                    Auth = AzureOpenAIConfig.AuthTypes.APIKey,
                    APIKey = configuration.GetSection("ApiKey").ToString() ?? string.Empty
                })
                .WithSimpleTextDb(new Microsoft.KernelMemory.MemoryStorage.DevTools.SimpleTextDbConfig()
                {
                    Directory = @"D:\\Repo\\Hackathon-03-16\\Hackathon-03-16\\HackathonT4\\Assets",
                    StorageType = Microsoft.KernelMemory.FileSystem.DevTools.FileSystemTypes.Disk
                })
                .Build();
        }
    }
}
