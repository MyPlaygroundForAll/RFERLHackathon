using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace RFERL.Modules.Framework.WebAPI.Abstractions.Conventions
{
    /// <summary>Provides route convention globally for all web api controllers.</summary>
    public class RouteConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _centralPrefix;

        /// <summary>Initializes a new instance of the <see cref="RouteConvention"/> class.</summary>
        /// <param name="routeTemplateProvider">Route template provider instance.</param>
        public RouteConvention(IRouteTemplateProvider routeTemplateProvider)
        {
            _centralPrefix = new AttributeRouteModel(routeTemplateProvider);
        }

        /// <summary>Scans controllers in application and applies routing convention.</summary>
        /// <param name="application">Application model instance.</param>
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                // If controller already has a route attribute combines it with global convention
                var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
                if (matchedSelectors.Any())
                {
                    foreach (var selectorModel in matchedSelectors)
                    {
                        selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(
                            _centralPrefix,
                            selectorModel.AttributeRouteModel);
                    }
                }

                // If controller does not have then sets global routing convention to it
                var unmatchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
                if (unmatchedSelectors.Any())
                {
                    foreach (var selectorModel in unmatchedSelectors)
                    {
                        selectorModel.AttributeRouteModel = _centralPrefix;
                    }
                }
            }
        }
    }
}