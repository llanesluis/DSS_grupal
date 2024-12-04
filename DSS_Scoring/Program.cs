using System.Reflection;
using DSS_Scoring.Client.Pages;
using DSS_Scoring.Components;
using DSS_Scoring.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddBlazorBootstrap();

// Agregar soporte para endpoint de API y rutas en Blazor
builder.Services.AddEndpointsApiExplorer();

// Add the controllers to the services collection
builder.Services.AddControllers().AddControllersAsServices();

// Agregar el DbContext para usarlo en la app
builder.Services.AddDbContext<MyDbContext>(options => {
    var connectionString = builder.Configuration.GetConnectionString("SUPABASE_CONNECTION");
    options.UseNpgsql(connectionString);
});

// Agregar el HttpClient para usar el servicio durante SSR
// El CLIENTE lo necesita para hacer peticiones al servidor (a veces)
// porque este Program.cs es el entry point de la app
builder.Services.AddScoped(sp=> new HttpClient {
    BaseAddress = new Uri("http://localhost:5173/")
});

// Configurar CORS
builder.Services.AddCors(opciones => {
    opciones.AddPolicy("dss_grupal", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add the Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { 
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DSS_Grupal", 
        Version = "v1",
        Description = "API DSS Scoring Grupal"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();
    app.UseSwaggerUI(c=>{
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DSS Scoring Grupal");
    });
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.UseHttpsRedirection();
}

app.UseCors("dss_grupal");

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(DSS_Scoring.Client._Imports).Assembly);

// Manera moderna de mapear los controladores 
app.MapControllers();

app.Run();
