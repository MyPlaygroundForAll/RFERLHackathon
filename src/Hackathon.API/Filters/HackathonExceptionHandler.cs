using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using RFERL.Modules.Framework.WebAPI.Abstractions.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.API.Filters
{
    /// <summary>Identity management API global exception handler.</summary>
    public class HackathonExceptionHandler : GlobalExceptionHandler
    {
        /// <summary>Initializes a new instance of the <see cref="IdentityManagementExceptionHandler"/> class.</summary>
        /// <param name="logger">Logger instance.</param>
        public HackathonExceptionHandler(ILogger<GlobalExceptionHandler> logger)
            : base(logger)
        {
        }

        /// <summary>Registers additional exceptions which desired to be handled.</summary>
        /// <param name="context">Context which contains information about exception.</param>
        protected override void CatchAdditionalExceptions(ExceptionContext context)
        {
        }
    }
}
