using ImagePrompter.Components;
using Persistence;
using Application;
using Application.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var dbCredentials = await DatabaseConnectionStringRetriever.GetDatabaseCredentials();
builder.Services.AddPersistenceServices(options => 
    options.ConnectionString = dbCredentials);

builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

//TODO: Refactor constructors into primary constructors.
//TODO: Treat all warnings and, consequently, all null values
//TODO: Add exception filtering
