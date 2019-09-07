using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RFERL.Modules.Framework.WebAPI.Abstractions.Model;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.ObjectResults
{
    /// <summary>Represents validation failure error result for web api.</summary>
    public class ValidationFailedResult : ObjectResult
    {
        /// <summary>Constructor for <see cref="ValidationFailedResult"/>.</summary>
        /// <param name="validationFailures">Validation error list.</param>
        public ValidationFailedResult(List<ValidationResult.ValidationError> validationFailures)
            : base(new ValidationResult(validationFailures))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}