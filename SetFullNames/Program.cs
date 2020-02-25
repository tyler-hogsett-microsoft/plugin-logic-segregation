using Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Components;
using Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Configuration;
using Microsoft.Pfe.Samples.PluginLogicSegregation.Utilities.CdsConnection.Components;

namespace Microsoft.Pfe.Samples.PluginLogicSegregation.ConsoleApps.SetFullNames
{
    class Program
    {
        static void Main(string[] args)
        {
            var fullNameCalculator = new FullNameCalculator();

            var connectionRetriever = new ConsoleCdsConnectionRetriever();
            var serviceManager = connectionRetriever.GetCdsConnection();

            var executable = new AllContactsFullNameApplicator(fullNameCalculator, serviceManager);

            executable.Execute();
        }
    }
}