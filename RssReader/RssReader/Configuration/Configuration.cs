using System.IO;
using System.Text.Json;

namespace RssReader
{
    public static class Configuration
    {
        public static ConfigurationModel GetConfiguration()
            => JsonSerializer.Deserialize<ConfigurationModel>(File.ReadAllText("Configuration\\config.json"));
    }
}
