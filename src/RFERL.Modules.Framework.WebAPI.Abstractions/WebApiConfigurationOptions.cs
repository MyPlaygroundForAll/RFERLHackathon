using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RFERL.Modules.Framework.WebAPI.Abstractions.Filters;

namespace RFERL.Modules.Framework.WebAPI.Abstractions
{
    /// <summary>Represents web api configuration options for initializing web api.</summary>
    public class WebApiConfigurationOptions
    {
        /// <summary>Initializes a new instance of the <see cref="WebApiConfigurationOptions"/> class.</summary>
        public WebApiConfigurationOptions()
        {
            ExceptionHandlerType = typeof(GlobalExceptionHandler);
            SetCustomMvcJsonOptions = opt => { };
            SetCustomMvcOptions = opt => { };
        }

        /// <summary>Gets type of global exception handler.</summary>
        public Type ExceptionHandlerType { get; private set; }

        /// <summary>Gets mvc json options delegate for setting custom rules.</summary>
        public Action<MvcJsonOptions> SetCustomMvcJsonOptions { get; private set; }

        /// <summary>Gets mvc options delegate for setting custom rules.</summary>
        public Action<MvcOptions> SetCustomMvcOptions { get; private set; }

        /// <summary>Adds additional mvc json options which are provided.</summary>
        /// <param name="options">Customized mvc json options.</param>
        public void AddAdditionalJsonOptions(Action<MvcJsonOptions> options)
        {
            SetCustomMvcJsonOptions = options;
        }

        /// <summary>Adds additional mvc options which are provided.</summary>
        /// <param name="options">Customized mvc options.</param>
        public void AddAdditionalMvcOptions(Action<MvcOptions> options)
        {
            SetCustomMvcOptions = options;
        }

        /// <summary>Sets exception handler for api.</summary>
        /// <typeparam name="TException">Type of exception filter.</typeparam>
        public void UseExceptionHandler<TException>()
            where TException : IExceptionFilter
        {
            ExceptionHandlerType = typeof(TException);
        }
    }
}
