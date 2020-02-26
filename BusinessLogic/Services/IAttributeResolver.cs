namespace Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Services
{
    public interface IAttributeResolver
    {
        T GetAttributeValue<T>(string attributeLogicalName);
    }
}