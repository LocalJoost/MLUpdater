
using RealityCollective.ServiceFramework.Interfaces;
using UnityEngine.Events;

namespace LocalJoost.Services.PackageVersionChecking
{
    public interface IPackageVersionLoader : IService
    {
        UnityEvent LatestVersionDataLoaded { get; }
        
        bool IsNewVersionAvailable { get; }
        
        string LatestVersion { get; }

        void DownloadNewVersion();
    }
}