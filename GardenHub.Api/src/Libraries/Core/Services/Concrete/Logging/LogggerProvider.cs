using Core.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Services.Concrete.Logging;

public class LoggerProvider<T> : ILoggerProvider<T>
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    private ILoggerFactory _loggerFactory;

    private ILoggerFactory _LoggerFactory
    {
        get { return _loggerFactory ??= InitLoggerFactory(); }
    }

    public LoggerProvider(IWebHostEnvironment env)
    {
        _webHostEnvironment = env;
    }

    private ILoggerFactory InitLoggerFactory()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();

            if (_webHostEnvironment.IsProduction())
            {
                builder.SetMinimumLevel(LogLevel.Warning);
            }
            else
            {
                builder.AddDebug().SetMinimumLevel(LogLevel.Information);
            }
        });

        return loggerFactory;
    }

    public ILogger BuildLogger()
    {
        return _LoggerFactory.CreateLogger<T>();
    }
}