using Revolt.Net.Core.JsonModels;

namespace Revolt.Net.Rest.Entities
{

    internal sealed class RestServer
    {
        private readonly JsonServer JsonModel;

        internal RestServer(JsonServer jsonModel)
        {
            JsonModel = jsonModel;
        }

        public string Id => JsonModel.Id;
        public string Name => JsonModel.Name;
        public string OwnerUserId => JsonModel.OwnerUserId;
        public bool IsNSFW => JsonModel.IsNSFW;
    }
}
