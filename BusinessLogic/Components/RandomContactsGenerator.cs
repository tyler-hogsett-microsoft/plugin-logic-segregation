using Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Services;
using Microsoft.Pfe.Xrm;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Components
{
    public class RandomContactsGenerator : IExecutable
    {
        private readonly OrganizationServiceManager ServiceManager;
        private readonly Random Random;

        public RandomContactsGenerator(OrganizationServiceManager serviceManager)
        {
            ServiceManager = serviceManager;
            Random = new Random();
        }

        public void Execute()
        {
            const int numberOfContacts = 9105;

            var contactsToCreate = new List<Entity>();

            for(var i = 0; i < numberOfContacts; i++)
            {
                var contact = new Entity("contact");
                contact["firstname"] = GenerateNamePart();
                contact["lastname"] = GenerateNamePart();

                contactsToCreate.Add(contact);
            }

            ServiceManager.ParallelProxy.Create(contactsToCreate);
        }

        private string GenerateNamePart()
        {
            const string consonants = "bcdfghjklmnpqrstvwxyz";
            const string vowels = "aeiouy";

            var length = Random.Next(4, 11);
            var isTimeForVowel = Random.Next(1, 4) == 1;

            var namePart = new StringBuilder();

            for(var i = 0; i < length; i++)
            {
                while(namePart.Length == i)
                {
                    char nextCharacter;
                    if(isTimeForVowel)
                    {
                        nextCharacter = vowels[Random.Next(0, vowels.Length)];
                    } else
                    {
                        nextCharacter = consonants[Random.Next(0, consonants.Length)];
                    }
                    if(i > 0 && nextCharacter == namePart[i - 1])
                    {
                        continue;
                    }
                    if(nextCharacter == 'q')
                    {
                        if(i >= length - 2)
                        {
                            continue;
                        }
                    }
                    if(i == 0)
                    {
                        namePart.Append(nextCharacter.ToString().ToUpper());
                    } else
                    {
                        namePart.Append(nextCharacter);
                    }
                    if(nextCharacter == 'q')
                    {
                        namePart.Append('u');
                        i++;
                    }
                    isTimeForVowel = !isTimeForVowel;
                }
            }
            return namePart.ToString();
        }
    }
}
