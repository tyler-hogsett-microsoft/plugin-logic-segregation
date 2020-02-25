using Microsoft.Pfe.Xrm;
using Microsoft.Xrm.Tooling.Connector;

namespace Microsoft.Pfe.Samples.PluginLogicSegregation.Utilities.CdsConnection.Services
{
    public interface ICdsConnectionRetriever
    {
        OrganizationServiceManager GetCdsConnection();
    }
}
