
using RealityCollective.ServiceFramework.Definitions;
using RealityCollective.ServiceFramework.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace LocalJoost.Services.PackageVersionChecking
{
    [CreateAssetMenu(menuName = "LocalJoost/PackageVersionLoaderProfile", 
        fileName = "PackageVersionLoaderProfile",
        order = (int)CreateProfileMenuItemIndices.ServiceConfig)]
    public class PackageVersionLoaderProfile : BaseServiceProfile<IServiceModule>
    {
        [FormerlySerializedAs("packageLocation")]
        [SerializeField]
        private string versionInfoLocation = string.Empty;
        
        public string VersionInfoLocation => versionInfoLocation;

        [SerializeField]
        private string deviceType = "Magic Leap";
        
        public string DeviceType => deviceType;
    }
}
