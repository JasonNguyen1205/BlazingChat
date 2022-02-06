using FRIWOCenter.WebAPI.Logging;
using FRIWOCenter.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FRIWOCenter.WebAPI.Logging
{
    public class ApplicationLoggerProvider : ILoggerProvider
    {
        private readonly IDbContextFactory<LoggingFRIWOCenterContext> _contextFactory;

        public ApplicationLoggerProvider(IDbContextFactory<LoggingFRIWOCenterContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(_contextFactory);
        }

        public void Dispose()
        {

        }
    }
}
