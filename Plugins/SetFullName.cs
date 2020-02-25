/*
# This Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment. 
# THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, 
# INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
# We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to reproduce and distribute the object code form of the Sample Code, provided that. 
# You agree: 
# (i) to not use Our name, logo, or trademarks to market Your software product in which the Sample Code is embedded; 
# (ii) to include a valid copyright notice on Your software product in which the Sample Code is embedded; 
# and (iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims or lawsuits, including attorneys’ fees, that arise or result from the use or distribution of the Sample Code 
*/

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