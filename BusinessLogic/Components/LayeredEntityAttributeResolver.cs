using Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Services;
using Microsoft.Xrm.Sdk;

namespace Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Components
{
    public class LayeredEntityAttributeResolver : IAttributeResolver
    {
        private readonly Entity[] Entities;

        public LayeredEntityAttributeResolver(params Entity[] entities)
        {
            Entities = entities;
        }

        public T GetAttributeValue<T>(string attributeLogicalName)
        {
            foreach(var entity in Entities)
            {
                if(entity.Contains(attributeLogicalName))
                {
                    return entity.GetAttributeValue<T>(attributeLogicalName);
                }
            }
            return default;
        }
    }
}
