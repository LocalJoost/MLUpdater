using System;
using Newtonsoft.Json;

namespace LocalJoost.Services.PackageVersionChecking
{
    [Serializable]
    public class VersionData
    {
        [JsonProperty("version")]
        public string Version { get; set; }
        
        [JsonProperty("url")]
        public string Url { get; set; }

        public Version ToVersion()
        {
            return new Version(Version);
        }
    }
}