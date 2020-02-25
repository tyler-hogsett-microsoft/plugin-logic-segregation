using Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Components;
using Microsoft.Pfe.Samples.PluginLogicSegregation.Utilities.CdsConnection.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateContacts
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionRetriever = new ConsoleCdsConnectionRetriever();
            var serviceManager = connectionRetriever.GetCdsConnection();
            var executable = new RandomContactsGenerator(serviceManager);
            executable.Execute();
        }
    }
}
