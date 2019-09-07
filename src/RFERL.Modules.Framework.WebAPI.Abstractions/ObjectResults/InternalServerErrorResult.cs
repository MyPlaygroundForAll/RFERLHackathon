using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.ObjectResults
{
    /// <summary>Represents Internal server error result for web api.</summary>
    public class InternalServerErrorResult : ObjectResult
    {
        /// <summary>Initializes a new instance of the <see cref="InternalServerErrorResult"/> class.</summary>
        public InternalServerErrorResult()
            : base("Unexpected Exception occured, please contact system administrator!")
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}