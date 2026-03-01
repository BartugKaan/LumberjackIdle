using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class WorkerSpawner : MonoBehaviour
{
    [SerializeField] private WorkerController workerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform basePoint;
        

    private IUpgradeManager _upgradeManager;
    private IObjectResolver _resolver;

    [Inject]
    public void Construct(IUpgradeManager upgradeManager, IObjectResolver resolver)
    {
        _upgradeManager = upgradeManager;
        _resolver = resolver;

        _upgradeManager.OnUpgradePurchased += HandleUpgrade;
    }

    private void HandleUpgrade(UpgradeData data, int newLevel)
    {
        if (data.upgradeType == UpgradeType.HireWorker)
        {
            SpawnWorker();
        }
    }

    private void SpawnWorker()
    {
        WorkerController worker = Instantiate(workerPrefab, spawnPoint.position, Quaternion.identity);
        
        _resolver.InjectGameObject(worker.gameObject);
        worker.SetBasePoint(basePoint);
        Debug.Log("WorkerSpawner: New Worker Hired!");
    }

    private void OnDestroy()
    {
        if (_upgradeManager != null)
        {
            _upgradeManager.OnUpgradePurchased -= HandleUpgrade;
        }
    }
}
