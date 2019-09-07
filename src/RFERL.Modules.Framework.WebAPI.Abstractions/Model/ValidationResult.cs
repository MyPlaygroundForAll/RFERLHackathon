using System.Collections.Generic;
using Newtonsoft.Json;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.Model
{
    /// <summary>Validation container.</summary>
    public class ValidationResult
    {
        /// <summary>Initializes a new instance of the <see cref="ValidationResult"/> class.</summary>
        /// <param name="validationErrors">List of validation errors.</param>
        public ValidationResult(List<ValidationError> validationErrors)
            : this()
        {
            Message = "Validation Failed";
            Errors = validationErrors;
        }

        /// <summary>Initializes a new instance of the <see cref="ValidationResult"/> class.</summary>
        private ValidationResult()
        {
            Errors = new List<ValidationError>();
        }

        /// <summary>Gets message of validation container.</summary>
        public string Message { get; private set; }

        /// <summary>Gets validation errors.</summary>
        public List<ValidationError> Errors { get; private set; }

        /// <summary>Represents validation error model.</summary>
        public class ValidationError
        {
            /// <summary>Initializes a new instance of the <see cref="ValidationError"/> class.</summary>
            /// <param name="field">Field name of error.</param>
            /// <param name="message">Validation message of error.</param>
            public ValidationError(string field, string message)
                : this()
            {
                Field = field != string.Empty ? field : null;
                Message = message;
            }

            /// <summary>Initializes a new instance of the <see cref="ValidationError"/> class.</summary>
            private ValidationError()
            {
            }

            /// <summary>Gets field name of validation error.</summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Field { get; private set; }

            /// <summary>Gets message of validation error.</summary>
            public string Message { get; private set; }
        }
    }
}