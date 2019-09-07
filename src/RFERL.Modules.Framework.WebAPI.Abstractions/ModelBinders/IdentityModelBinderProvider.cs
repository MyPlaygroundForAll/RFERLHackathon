using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RFERL.Modules.Framework.Common.Interfaces;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.ModelBinders
{
    /// <summary>Represents model binder provider for models which implements IUpdateRequest.</summary>
    public class IdentityModelBinderProvider : IModelBinderProvider
    {
        private readonly IList<IInputFormatter> _formatters;
        private readonly IHttpRequestStreamReaderFactory _readerFactory;

        /// <summary>Constructor for <see cref="IdentityModelBinderProvider"/>.</summary>
        /// <param name="formatters">Registered input formatters for mvc pipeline.</param>
        /// <param name="readerFactory">Stream reader factory instance.</param>
        public IdentityModelBinderProvider(IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory)
        {
            _formatters = formatters;
            _readerFactory = readerFactory;
        }

        /// <summary>Gets corresponding model binder from provider context.</summary>
        /// <param name="context">Model binder provider context instance.</param>
        /// <returns>Corresponding Model binder.</returns>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (typeof(IRequestHasIdentity<Guid>).IsAssignableFrom(context.Metadata.ModelType))
            {
                return new GuidIdentityModelBinder(_formatters, _readerFactory);
            }

            if (typeof(IRequestHasIdentity<int>).IsAssignableFrom(context.Metadata.ModelType))
            {
                return new IntegerIdentityModelBinder(_formatters, _readerFactory);
            }

            return null;
        }
    }
}
