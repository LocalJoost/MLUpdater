using LocalJoost.Services.PackageVersionChecking;
using RealityCollective.ServiceFramework.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HandleNewNoVersionBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject menuRoot;

    private IPackageVersionLoader packageLoadingService;

    void Start()
    {
        menuRoot.SetActive(false);
        packageLoadingService = ServiceManager.Instance.GetService<IPackageVersionLoader>();
        packageLoadingService.LatestVersionDataLoaded.AddListener(ProcessVersionData);
        if (packageLoadingService.LatestVersion != null)
        {
            ProcessVersionData();
        }
    }

    private void ProcessVersionData()
    {
        menuRoot.SetActive(!packageLoadingService.IsNewVersionAvailable);
    }

    public void Dismiss()
    {
        Destroy(gameObject);
    }
}