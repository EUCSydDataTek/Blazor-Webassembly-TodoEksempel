using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApiEksempel.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddSingleton<ITodoService, TodoService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Todo Api",
        Description = "A simple todo api",
        Contact = new OpenApiContact() { Name = "EUC Syd" , Url = new Uri("https://www.eucsyd.dk/") }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});

//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowedOrigins",
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:5002")
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging(); //Slå debugging af Webassembly til i Development
}

app.UseHttpsRedirection();

app.UseCors("AllowedOrigins"); //Cors

app.UseAuthorization();

app.UseBlazorFrameworkFiles(); // Sørger for at blazor webassembly appen kan loade de frameworks (DLL) den skal bruge for at køre i browseren 

app.UseStaticFiles(); //loader statiske filer fx billeder fra serveren

app.MapControllers();

app.MapFallbackToFile("index.html");  // setter endpoint til index.html

app.Run();
