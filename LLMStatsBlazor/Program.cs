using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LLMStatsBlazor;
using LLMStatsBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient for general use
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register FirestoreDataService
builder.Services.AddScoped<FirestoreDataService>(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://firestore.googleapis.com/v1/projects/llm-stats-realtime/databases/(default)/documents/")
    };
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    return new FirestoreDataService(httpClient);
});

// Register MockDataService as fallback
builder.Services.AddSingleton<MockDataService>();

await builder.Build().RunAsync();
