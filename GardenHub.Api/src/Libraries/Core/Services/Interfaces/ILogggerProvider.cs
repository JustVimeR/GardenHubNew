using Microsoft.Extensions.Logging;

namespace Core.Services.Interfaces;

public interface ILoggerProvider<T>
{
    ILogger BuildLogger();
}