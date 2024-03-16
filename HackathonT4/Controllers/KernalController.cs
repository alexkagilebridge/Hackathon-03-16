using Microsoft.AspNetCore.Mvc;
using Microsoft.KernelMemory;

namespace HackathonT4.Controllers;

[ApiController]
[Route("[controller]")]
public class KernalController : ControllerBase
{
    
    private readonly IKernelMemory _kernelMemory;

    public KernalController(IKernelMemory kernelMemory)
    {
        _kernelMemory = kernelMemory;
    }

    [HttpGet]
    public async Task<string> Get(string question)
    {
        // await ImportDatabase();
        var answer = _kernelMemory.AskAsync(question).GetAwaiter().GetResult();
        return answer.Result;
    }
    
    private async Task ImportDatabase()
    {
        await _kernelMemory.DeleteDocumentAsync("file001");
    
        Document document = new Document("file001");
        DirectoryInfo directory = new DirectoryInfo(@"D:\\Repo\\Hackathon-03-16\\Hackathon-03-16\\HackathonT4\\MockData");
        var alldirFiles = directory.EnumerateFiles();

        foreach (var file in alldirFiles)
        {
            Console.WriteLine($"Importing {file.Name}");
            document.AddFile(file.FullName);
            document.AddTag("document_name", file.Name);
        }
        await _kernelMemory.ImportDocumentAsync(document);

        while (!await _kernelMemory.IsDocumentReadyAsync("file001"))
        {
            Console.WriteLine("Wait for document to be ready...");
        }
    }
}