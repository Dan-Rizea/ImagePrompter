using ImagePrompter.Components;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddPersistenceServices(options => 
    options.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!);

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
