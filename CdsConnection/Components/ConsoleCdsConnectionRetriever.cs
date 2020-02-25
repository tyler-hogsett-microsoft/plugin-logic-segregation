using Microsoft.Crm.Sdk.Messages;
using Microsoft.Pfe.Samples.PluginLogicSegregation.Utilities.CdsConnection.Services;
using Microsoft.Pfe.Xrm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Security;

namespace Microsoft.Pfe.Samples.PluginLogicSegregation.Utilities.CdsConnection.Components
{
    public class ConsoleCdsConnectionRetriever : ICdsConnectionRetriever
    {
        public OrganizationServiceManager GetCdsConnection()
        {
            var environmentUrl = PromptFor("environment url");
            var uri = XrmServiceUriFactory.CreateOrganizationServiceUri(environmentUrl);
            var username = PromptFor("username");
            var password = PromptForPassword();

            var serviceManager = new OrganizationServiceManager(uri, username, password);
            return serviceManager;
        }

        private static string PromptFor(string field)
        {
            Console.Write($"Enter {field}: ");
            return Console.ReadLine();
        }

        private static string PromptForPassword()
        {
            Console.Write("Enter password: ");

            var password = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            Console.WriteLine();

            return password;
        }
    }
}
