using RealityCollective.ServiceFramework.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

namespace LocalJoost.Services.PackageVersionChecking
{
    [System.Runtime.InteropServices.Guid("799a03ef-ca81-470a-9dd1-4999df128fd9")]
    public class PackageVersionLoader : BaseServiceWithConstructor, IPackageVersionLoader
    {
        private readonly PackageVersionLoaderProfile profile;
        public PackageVersionLoader(string name, uint priority, PackageVersionLoaderProfile profile)
            : base(name, priority)
        {
            this.profile = profile;
        }

        public override void Start()
        {
            DetectNewVersion();
        }

        private VersionData loadedVersionData;

        public UnityEvent LatestVersionDataLoaded { get; } = new ();
        
        public string LatestVersion => loadedVersionData?.Version;
        
        public bool IsNewVersionAvailable { get; private set; }
        
        private async Task DetectNewVersion()
        {
#if !UNITY_EDITOR
            if (SystemInfo.deviceModel.Contains(profile.DeviceType))
#endif
            {
                var currentApplicationVersion = new Version(Application.version);
                loadedVersionData = await DownloadVersionData();

                if (loadedVersionData != null)
                {
                    var newVersion = loadedVersionData.ToVersion();
                    IsNewVersionAvailable = newVersion > currentApplicationVersion;
                }
                
                LatestVersionDataLoaded.Invoke();
            }
        }
        
        private async Task<VersionData> DownloadVersionData()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(profile.VersionInfoLocation);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<VersionData>(
                            await response.Content.ReadAsStringAsync());
                        return result;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DownloadNewVersion()
        {
            if (!IsNewVersionAvailable)
            {
                return;
            }
            Application.OpenURL(loadedVersionData.Url);
        }
    }
}
