using EncDecryptTool;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();
 
builder.Services.AddTransient<IEntrypoint, Entrypoint>();
builder.Services.AddTransient<IEncDecryptorWrapper, EncDecryptorWrapper>();
 
var host = builder.Build();
 
var entrypoint = host.Services.GetRequiredService<IEntrypoint>();
await entrypoint.RunAsync(args);