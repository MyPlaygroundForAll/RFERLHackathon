using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.ModelBinders
{
    /// <summary>Represents identity model binder implementation for guid.</summary>
    public class GuidIdentityModelBinder : IdentityModelBinder<Guid>
    {
        /// <summary>Constructor for <see cref="GuidIdentityModelBinder"/>.</summary>
        /// <param name="formatters">Registered input formatters for mvc pipeline.</param>
        /// <param name="readerFactory">Stream reader factory instance.</param>
        public GuidIdentityModelBinder(IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory)
            : base(formatters, readerFactory)
        {
        }

        /// <summary>Converts value to corresponding type.</summary>
        /// <param name="value">value in string format.</param>
        /// <returns>Converted value in correct format.</returns>
        protected override Guid ConvertValue(string value)
        {
            Guid.TryParse(value, out Guid guidValue);
            return guidValue;
        }
    }
}
