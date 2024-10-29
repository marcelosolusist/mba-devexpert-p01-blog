using BlogExpert.Mvc.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder
        .AddMvcConfig()
        .AddAutoMapperConfig()
        .AddDatabaseSelector();

builder.Services.ResolveDependencies();

var app = builder.Build();

app.AddAppConfig();

app.Run();
