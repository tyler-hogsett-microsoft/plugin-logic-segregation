namespace Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Services
{
    public interface IFullNameCalculator
    {
        string Calculate(IAttributeResolver person);
    }
}