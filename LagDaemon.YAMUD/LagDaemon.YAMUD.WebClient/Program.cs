using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.WebClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MongoDB.Driver;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddScoped<IMongoClient>(sp => new MongoClient("mongodb://localhost:27017"`));
builder.Services.AddScoped<IMongoRoomService>(sp =>
{
    return new MongoRoomService(new MongoClient("mongodb://localhost:27017"), "yamud", "dungeon");
});
    

await builder.Build().RunAsync();
