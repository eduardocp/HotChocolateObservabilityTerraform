using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using Azure.Monitor.OpenTelemetry.AspNetCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddLogging(builder =>
        {
            builder.AddOpenTelemetry(b =>
            {
                b.IncludeFormattedMessage = true; // The default is false to better storage and network traffic performance
                b.IncludeScopes = true; //Include scope 
                b.ParseStateValues = true; // True to avoid access state directly as the docs recommends
                b.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("DotNetHotChocolateService"));
            });
        });

        builder.Services
            .AddOpenTelemetry()
            .WithTracing(tracerProviderBuilder =>
            {
                tracerProviderBuilder
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    //.AddEntityFrameworkCoreInstrumentation() //if you use
                    .AddHotChocolateInstrumentation()
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("DotNetHotChocolateService"));
            })
            .UseAzureMonitor(options => options.ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

        builder.Services
            .AddGraphQLServer()
            .AddQueryType<Query>();

        var app = builder.Build();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
        });

        app.Run();
    }
}