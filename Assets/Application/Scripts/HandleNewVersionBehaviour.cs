using LocalJoost.Services.PackageVersionChecking;
using RealityCollective.ServiceFramework.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HandleNewVersionBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject menuRoot;
    
    [SerializeField]
    private TextMeshProUGUI newVersionText;

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
        if (packageLoadingService.IsNewVersionAvailable)
        {
            newVersionText.text = 
                $"New version {packageLoadingService.LatestVersion} available!";
            menuRoot.SetActive(true);
        }
        else
        {
            menuRoot.SetActive(false);
        }
    }

    public void Dismiss()
    {
        Destroy(gameObject);
    }

    public void DownloadNewVersion()
    {
        packageLoadingService.DownloadNewVersion();
        menuRoot.SetActive(false);
    }
}
