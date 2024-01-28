using Core.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using WebApi;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.AddCors(co =>
{
    co.AddPolicy("Policy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddHealthChecks()
               .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
               name: "applicationDb-check",
               failureStatus: HealthStatus.Unhealthy,
               tags: new string[] { "api", "SqlDb" });

ServiceRegisterer serviceRegistry = new();
serviceRegistry.RegisterServices(builder.Services, builder.Configuration);
serviceRegistry.AddConfiguredSwagger(builder.Services);
serviceRegistry.AddConfiguredAuthenticationWithAuthorization(builder.Services, builder.Configuration);
serviceRegistry.AddConfiguredDbContextWithIdentity(builder.Services, builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, AppExtensions.GetJsonPatchInputFormatter());
})
.AddNewtonsoftJson(c =>
{
    c.SerializerSettings.Converters.Add(new StringEnumConverter());
    c.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.ConfigureApplicationCookie(options =>
{

});

builder.Services.AddRouting(options => options.LowercaseUrls = true);


builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
});
var app = builder.Build();

app.UseCors("Policy");
app.UseHttpsRedirection();
app.UseRouting();

app.UseErrorHandlingMiddleware();

app.UseAuthentication();
app.UseAuthorization();

//error middleware

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Api V1"); });


app.MapControllers();


app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

await new PrepareAppService().Prepare(app);

app.Run();