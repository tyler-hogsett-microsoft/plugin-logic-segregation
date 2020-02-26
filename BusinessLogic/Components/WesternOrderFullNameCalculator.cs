using Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Services;

namespace Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Components
{
    public class WesternOrderFullNameCalculator : IFullNameCalculator
    {
        public string Calculate(IAttributeResolver person)
        {
            return $"{person.GetAttributeValue<string>("firstname")} {person.GetAttributeValue<string>("lastname")}";
        }
    }
}