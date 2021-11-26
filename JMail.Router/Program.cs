global using System.Net;
global using System.Text.Json;
using JMail.NET.Datastore;
using JMail.Relay.Lib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


ConfigureRelay(ref builder);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureRelay(ref WebApplicationBuilder builderRef)
{
    var relayConfig = JsonDocument.Parse(File.ReadAllText("relayconfig.json"));

    //configure
    RelayData.Domains = relayConfig.RootElement.GetProperty("Host").GetProperty("Domains").Deserialize<string[]>();
    RelayData.Address.IP = relayConfig.RootElement.GetProperty("Host").GetProperty("IP").GetString();
    RelayData.Address.Port = relayConfig.RootElement.GetProperty("Host").GetProperty("Port").GetInt32();
    SymmetricEncryptor.Key = relayConfig.RootElement.GetProperty("Security").GetProperty("AESKey").GetString();


    builderRef.WebHost.UseKestrel(x => {
        x.Listen(
            IPAddress.Parse(relayConfig.RootElement.GetProperty("Host").GetProperty("IP").GetString()),
            relayConfig.RootElement.GetProperty("Host").GetProperty("Port").GetInt32());

    });
}
