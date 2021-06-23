﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Watchman.Shared.Extensions;

namespace Watchman.Application.Behaviors {

  public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(IHttpContextAccessor httpContextAccessor,
            ILogger<LoggingBehavior<TRequest, TResponse>> logger) {
      _httpContextAccessor = httpContextAccessor;
      _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {
      _logger.LogInformation("----- Handling command {CommandName} ({@Command})", request.GetGenericTypeName(),
          request);

      var response = await next();

      _logger.LogInformation("----- Command {CommandName} handled - response: {@Response}",
          request.GetGenericTypeName(), response);

      return response;
    }
  }
}