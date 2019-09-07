using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.ModelBinders
{
    /// <summary>Represents identity model binder implementation for integer.</summary>
    public class IntegerIdentityModelBinder : IdentityModelBinder<int>
    {
        /// <summary>Constructor for <see cref="IntegerIdentityModelBinder"/>.</summary>
        /// <param name="formatters">Registered input formatters for mvc pipeline.</param>
        /// <param name="readerFactory">Stream reader factory instance.</param>
        public IntegerIdentityModelBinder(IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory)
            : base(formatters, readerFactory)
        {
        }

        /// <summary>Converts value to corresponding type.</summary>
        /// <param name="value">value in string format.</param>
        /// <returns>Converted value in correct format.</returns>
        protected override int ConvertValue(string value)
        {
            int.TryParse(value, out var intValue);
            return intValue;
        }
    }
}
