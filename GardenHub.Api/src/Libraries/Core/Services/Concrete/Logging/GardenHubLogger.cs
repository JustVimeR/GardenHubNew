using Core.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace Core.Services.Concrete.Logging;

public class GardenHubLogger<T> : IGardenHubLogger<T>
{
    private readonly ILogger _logger;

    public GardenHubLogger(ILoggerProvider<T> factory)
    {
        _logger = factory.BuildLogger();
    }

    public void Debug(string message)
    {
        _logger.LogDebug(message);
    }

    public void Info(string message)
    {
        _logger.LogInformation(message);
    }

    public void Error(string message)
    {
        _logger.LogError(message);
    }

    public void Error(Exception exception)
    {
        _logger.LogError($"Error: {exception.Message}");
    }

    public void Warning(string message)
    {
        _logger.LogWarning(message);
    }
}

