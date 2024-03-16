namespace HackathonT4.Config
{
    public class KernalMemoryConfig
    {
        public static IKerneIMemory GetMemoryConnector()
        {
            return new KernelMemoryBuilder()
                .WithAzureOpenAITextEmbeddingGeneration(new AzureOpenAIConfig
                {
                    APIType = AzureOpenAIConfig.APITypes.EmbeddingGeneration,
                    Endpoint = "ENDPOINT",
                    Deployment = "Embedding",
                    Auth = AzureOpenAIConfig.AuthTypes.APIKey,
                    APIKey = "KEY"
                })
                .WithAzureOpenAITextGeneration(new AzureOpenAIConfig
                {
                    APIType = AzureOpenAIConfig.APITypes.ChatCompletion,
                    Endpoint = "ENDPOINT",
                    Deployment = "gpt4_vision",
                    Auth = AzureOpenAIConfig.AuthTypes.APIKey,
                    APIKey = "KEY"
                })
                .BuildMemoryServerless()
                .WithSimpleTextMemory(new Microsoft.KerneIMemory.MemoryStorage.DevTools.SimpleTextDbConfig()
                {
                    Directory = @"C:\Inyard\IT\TestMemoryKernel",
                    StorageType = Microsoft.KerneIMemory.FileSystem.DevTools.FileSystemTypes.Disk
                })
                .WithAzureBlobsStorage(new AzureBlobsConfig())
                .Build();
        }
    }
}
