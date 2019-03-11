# MoqNetCoreGenerator
generator of mock using Moq on net core

# Wanted features
Given:

MyController.cs
```csharp
public class MyController: ApiController 
{
    private readonly IFooService _fooService;

    public MyController(IFooService fooService) 
    {
        _fooService = fooService;
    }

    [HttpPost("delete")]
    public async Task Delete(long id)
    {
        await _fooService.DeleteAsync(id);
    } 
}
```

Startup.cs
```csharp
    ...
    services.AddScoped<IFooService, FooServiceImpl>();
    services.AddScoped<IBarRepository, BarRepositoryImpl>();
    ...
```

FooServiceImpl.cs
```csharp
public class FooServiceImpl: IFooService
{
    private readonly IBarRepository _barRepo;

    public FooServiceImpl(IBarRepository barRepo) 
    {
        _barRepo = barRepo;
    }

    public async Task DeleteAsync(long id) 
    {
        var entity = await _barRepo.Get(id);

        if (entity == null)
        {
            throw new FancyException();
        }

        await _barRepo.Delete(entity);
    }
}
```
* Parse entire SyntaxTree, select following parts: 
    * DI configuration from Startup
    * Dependencies
    * Method/property calls inside given method
* Generate call graph
* Setup mock-object for each call:
    * Mock specific interface method (`IFooService.DeleteAsync(long id)`)
    * Mock any injected dependency being used in `DeleteAsync`: 
        (`IBarRepository.Get` and `IBarRepository.Delete`)
    * Recursively mock any other dependent calls
* Produce output as method in test-class as templated `Moq.Setup` call sequence (input params? field assign?)

## dotnet-cli tool
`dotnet <tool> class=FooServiceImpl method=DeleteAsync <output_file_path>`