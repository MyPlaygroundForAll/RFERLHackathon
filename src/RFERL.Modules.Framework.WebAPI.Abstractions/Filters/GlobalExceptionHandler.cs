using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using RFERL.Modules.Framework.WebAPI.Abstractions.ObjectResults;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.Filters
{
    /// <summary>Global exception handler implementation.</summary>
    public class GlobalExceptionHandler : IExceptionFilter
    {
        /// <summary>Backing field for <see cref="ILogger{TCategoryName}"/>.</summary>
        private readonly ILogger<GlobalExceptionHandler> _logger;

        /// <summary>Constructor for <see cref="GlobalExceptionHandler"/>.</summary>
        /// <param name="logger">Logger instance for global exception handler.</param>
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>Intercepts web api pipeline when an exception occurs.</summary>
        /// <param name="context">Context which contains information about exception.</param>
        public void OnException(ExceptionContext context)
        {
            // we check that we have additional registered exception handling strategies or not.
            CatchAdditionalExceptions(context);
            if (context.ExceptionHandled)
            {
                return;
            }

            // If we still didn't handle exception, we assume that it's an unexpected error. Log it and handle exception.
            _logger.LogError(context.Exception, "Unexpected exception occured.");
            context.Result = new InternalServerErrorResult();
            context.ExceptionHandled = true;
        }

        /// <summary>Registers additional exceptions which desired to be handled.</summary>
        /// <param name="context">Context which contains information about exception.</param>
        protected virtual void CatchAdditionalExceptions(ExceptionContext context)
        {
        }
    }
}