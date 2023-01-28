using Newtonsoft.Json;

namespace ServicePostServer.Serializable
{
    public class AppSettingsDeserialize
    {
        private readonly string jsonPatch;

        public AppSettingsDeserialize(string jsonPatch)
        {
            this.jsonPatch = jsonPatch;
        }

        public AppSettings GetSettingsModels()
        {
            JsonSerializer serializer = new();
            using StreamReader sw = new(jsonPatch);
            using JsonReader reader = new JsonTextReader(sw);
            return serializer.Deserialize<AppSettings>(reader);
        }
    }
}
