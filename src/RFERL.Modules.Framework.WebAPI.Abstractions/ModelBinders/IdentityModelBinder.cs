using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using RFERL.Modules.Framework.Common.Interfaces;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.ModelBinders
{
    /// <summary>Binds identity value of object which implements <see cref="IRequestHasIdentity{TKey}"/>.</summary>
    /// <typeparam name="TKey">Type of object for model.</typeparam>
    public abstract class IdentityModelBinder<TKey> : IModelBinder
        where TKey : IComparable
    {
        private readonly BodyModelBinder _defaultBinder;

        /// <summary>Initializes a new instance of the <see cref="IdentityModelBinder{TKey}"/> class.</summary>
        /// <param name="formatters">Registered input formatters for mvc pipeline.</param>
        /// <param name="readerFactory">Stream reader factory instance.</param>
        protected IdentityModelBinder(IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory)
        {
            _defaultBinder = new BodyModelBinder(formatters, readerFactory);
        }

        /// <summary>Binds value from binding context.</summary>
        /// <param name="bindingContext">Model binding context instance.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            await _defaultBinder.BindModelAsync(bindingContext);

            if (bindingContext.Result.IsModelSet && bindingContext.Result.Model is IRequestHasIdentity<TKey> data)
            {
                var value = bindingContext.ValueProvider.GetValue("Id").FirstValue;
                var convertedValue = ConvertValue(value);
                if (convertedValue != null)
                {
                    data.Id = convertedValue;
                }

                bindingContext.Result = ModelBindingResult.Success(data);
            }
        }

        /// <summary>Converts value to corresponding type.</summary>
        /// <param name="value">value in string format.</param>
        /// <returns>Converted value in correct format.</returns>
        protected abstract TKey ConvertValue(string value);
    }
}
