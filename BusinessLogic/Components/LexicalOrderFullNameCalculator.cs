using Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Services;

namespace Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Components
{
    public class LexicalOrderFullNameCalculator : IFullNameCalculator
    {
        public string Calculate(IAttributeResolver person)
        {
            return $"{person.GetAttributeValue<string>("lastname")}, {person.GetAttributeValue<string>("firstname")}";
        }
    }
}