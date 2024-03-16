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
    public string Get()
    {
        var answer = _kernelMemory.AskAsync("Hello World!").GetAwaiter().GetResult();
        return answer.ToString() ?? string.Empty;
    }
}