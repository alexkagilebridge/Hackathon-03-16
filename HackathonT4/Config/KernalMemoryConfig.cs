using Microsoft.KernelMemory;

namespace HackathonT4.Config
{
    public static class KernalMemoryConfig
    {
        public static IKernelMemory GetMemoryConnector(IConfiguration configuration)
        {
            var builder =  new KernelMemoryBuilder()
                .WithAzureOpenAITextEmbeddingGeneration(new AzureOpenAIConfig
                {
                    APIType = AzureOpenAIConfig.APITypes.EmbeddingGeneration,
                    Endpoint = "https://ai-hack-team4.openai.azure.com/",
                    Deployment = "HackEmbedding",
                    Auth = AzureOpenAIConfig.AuthTypes.APIKey,
                    APIKey = configuration.GetValue<string>("ApiKey") ?? string.Empty
                })
                .WithAzureOpenAITextGeneration(new AzureOpenAIConfig
                {
                    APIType = AzureOpenAIConfig.APITypes.ChatCompletion,
                    Endpoint = "https://ai-hack-team4.openai.azure.com/",
                    Deployment = "gpt4new",
                    Auth = AzureOpenAIConfig.AuthTypes.APIKey,
                    APIKey = configuration.GetValue<string>("ApiKey") ?? string.Empty
                })
                // .WithSimpleTextDb(new Microsoft.KernelMemory.MemoryStorage.DevTools.SimpleTextDbConfig()
                // {
                //     Directory = @"D:\\Repo\\Hackathon-03-16\\Hackathon-03-16\\HackathonT4\\Assets",
                //     StorageType = Microsoft.KernelMemory.FileSystem.DevTools.FileSystemTypes.Disk
                // })
                // .Build();
                .Build<MemoryServerless>();

            ImportDatabase(builder, @"D:\\Repo\\Hackathon-03-16\\Hackathon-03-16\\HackathonT4\\MockData").GetAwaiter();
            
            var answer = builder.AskAsync("Give me some data from \"company extract.txt\"").GetAwaiter().GetResult();
            
            return builder;
        }
        
        public static async Task ImportDatabase(IKernelMemory memory, string databasePath)
        {
            await memory.DeleteDocumentAsync("file001");
    
            Document document = new Document("file001");
            DirectoryInfo directory = new DirectoryInfo(databasePath);
            var alldirFiles = directory.EnumerateFiles();

            foreach (var file in alldirFiles)
            {
                Console.WriteLine($"Importing {file.Name}");
                document.AddFile(file.FullName);
                document.AddTag("document_name", file.Name);
            }
            await memory.ImportDocumentAsync(document);

            while (!await memory.IsDocumentReadyAsync("file001"))
            {
                Console.WriteLine("Wait for document to be ready...");
                // var status = await memory.GetDocumentStatusAsync("file001");
                // foreach (var RemainingSteps in status?.RemainingSteps)
                // {
                //     Console.WriteLine($"Status: {RemainingSteps} step remaining");
                // }
            }

            var info = await memory.GetDocumentStatusAsync("file001");
            //, tags: new() { { "meeting", "transcription" } }
        }
    }
}
