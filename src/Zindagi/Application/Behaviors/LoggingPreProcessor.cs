using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Zindagi.SeedWork;

namespace Zindagi.Application.Behaviors
{
    [DebuggerStepThrough]
    public class LoggingPreProcessor<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILoggedInUser _loggedInUser;
        private readonly ILogger<LoggingPreProcessor<TRequest>> _logger;

        public LoggingPreProcessor(ILogger<LoggingPreProcessor<TRequest>> logger, ILoggedInUser loggedInUser)
        {
            _logger = logger;
            _loggedInUser = loggedInUser;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = await _loggedInUser.GetSerialNumber();

            if (userId == default)
            {
                _logger.LogInformation("Request: {Name} {@Request}", requestName, request);
            }
            else
            {
                _logger.LogInformation("Request: {Name} [{@UserId}] {@Request}",
                                       requestName, userId, request);
            }
        }
    }
}
