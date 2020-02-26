using Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Components;
using Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Configuration;
using Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Services;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace Microsoft.Pfe.Samples.PluginLogicSegregation.Plugins
{
    public class SetFullName : IPlugin
    {
        private readonly IFullNameCalculator FullNameCalculator;

        public SetFullName()
        {
            /// TODO: Configure which IFullNameCalculator to use (for example: read values from secure / insecure config)
            FullNameCalculator = new FullNameCalculator();
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            var context = ResolveFromServiceProvider<IPluginExecutionContext>(serviceProvider);

            var person = ResolveAttributeResolver(context);

            var target = (Entity)context.InputParameters["Target"];
            var fullName = FullNameCalculator.Calculate(person);
            if (person.GetAttributeValue<string>("mspfe_full_name") != fullName)
            {
                target["mspfe_full_name"] = fullName;
            }
        }

        private static T ResolveFromServiceProvider<T>(IServiceProvider serviceProvider)
        {
            return (T)serviceProvider.GetService(typeof(T));
        }

        private static IAttributeResolver ResolveAttributeResolver(IPluginExecutionContext context)
        {
            var target = (Entity)context.InputParameters["Target"];
            var entityLayers = new List<Entity>
            {
                target
            };
            entityLayers.AddRange(context.PreEntityImages.Values);
            return new LayeredEntityAttributeResolver(entityLayers.ToArray());
        }
    }
}