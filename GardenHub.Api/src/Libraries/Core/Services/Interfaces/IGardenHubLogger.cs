using System;

namespace Core.Services.Interfaces;

public interface IGardenHubLogger<T>
{
    void Debug(string message);

    void Info(string message);

    void Warning(string message);

    void Error(string message);

    void Error(Exception exception);
}
