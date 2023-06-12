﻿namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message) => _logger.Debug(message);

        public void LogError(string message) => _logger.Error(message);

        public void LogInformation(string message) => _logger.Info(message);

        public void LogTrace(string message) => _logger.Trace(message);

        public void LogWarning(string message) => _logger.Warn(message);
    }
}
