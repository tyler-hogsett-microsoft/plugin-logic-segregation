using Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Services;
using Microsoft.Pfe.Xrm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Pfe.Samples.PluginLogicSegregation.BusinessLogic.Components
{
    public class AllContactsFullNameApplicator : IExecutable
    {

        private readonly IFullNameCalculator FullNameCalculator;
        private readonly OrganizationServiceManager ServiceManager;

        public AllContactsFullNameApplicator(IFullNameCalculator fullNameCalculator, OrganizationServiceManager serviceManager)
        {
            FullNameCalculator = fullNameCalculator;
            ServiceManager = serviceManager;
        }

        public void Execute()
        {
            var service = ServiceManager.GetProxy();
            var query = CreateQuery();

            var contactsToUpdate = new List<Entity>();

            EntityCollection results;
            do {
                results = service.RetrieveMultiple(query);

                foreach(Entity contact in results.Entities)
                {
                    var resolver = new LayeredEntityAttributeResolver(contact);
                    var fullName = FullNameCalculator.Calculate(resolver);
                    if(fullName != resolver.GetAttributeValue<string>("mspfe_full_name"))
                    {
                        var toUpdate = new Entity("contact", contact.Id);
                        toUpdate["mspfe_full_name"] = fullName;
                        contactsToUpdate.Add(toUpdate);
                    }
                }

                query.PageInfo.PagingCookie = results.PagingCookie;
                query.PageInfo.PageNumber++;
            } while (results.MoreRecords);

            ServiceManager.ParallelProxy.Update(contactsToUpdate);
        }

        private static QueryExpression CreateQuery()
        {
            var query = new QueryExpression("contact")
            {
                ColumnSet = new ColumnSet("contactid", "firstname", "lastname", "mspfe_full_name"),
                PageInfo = new PagingInfo
                {
                    PageNumber = 1
                }
            };
            return query;
        }
    }
}
