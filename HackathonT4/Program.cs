
using HackathonT4.Config;
using Microsoft.Extensions.Azure;
using Microsoft.KernelMemory;

namespace EmployeeDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json")
                .Build();
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // builder.Services.AddScoped<IKernelMemory>(provider => KernalMemoryConfig.GetMemoryConnector(configuration));
            builder.Services.AddSingleton<IKernelMemory>(serviceProvider =>
            {
                var builder = new KernelMemoryBuilder()
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
                    .WithSimpleTextDb(new Microsoft.KernelMemory.MemoryStorage.DevTools.SimpleTextDbConfig()
                    {
                        Directory = @"D:\\Repo\\Hackathon-03-16\\Hackathon-03-16\\HackathonT4\\Assets",
                        StorageType = Microsoft.KernelMemory.FileSystem.DevTools.FileSystemTypes.Disk
                    })
                    .Build();
                
                return builder;
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}