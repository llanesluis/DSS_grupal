using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// El servicio HttpClient estará disponible en todos los componentes .razor
// por medio de la inyección de dependencias, ej. -> @inyect HttpClient HttpClient
builder.Services.AddScoped(sp=> new HttpClient {
    BaseAddress = new Uri("http://localhost:5173/")
});

await builder.Build().RunAsync();

builder.Services.AddBlazorBootstrap();