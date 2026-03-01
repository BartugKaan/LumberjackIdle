using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

public class TreePool : MonoBehaviour
{
    [Header("Pool Settings")] 
    [SerializeField] private Tree treePrefab;

    [SerializeField] private int poolSize = 5;

    [Header("Spawn Areas")] 
    [SerializeField] private Vector2 spawnMin = new Vector2(-4f, -8f);
    [SerializeField] private Vector2 spawnMax = new Vector2(4f, 8f);

    private List<Tree> _trees = new List<Tree>();
    
    private IUpgradeManager _upgradeManager;

    [Inject]
    public void Construct(IUpgradeManager upgradeManager)
    {
        _upgradeManager = upgradeManager;
        _upgradeManager.OnUpgradePurchased += HandleUpgrade;
    }


    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Tree tree = Instantiate(treePrefab, transform);
            tree.transform.position = GetRandomPosition();
            tree.OnRegrown += HandleTreeRegrown;
            
            _trees.Add(tree);
        }
        
        Debug.Log($"TreePool: {poolSize} trees spawned");
    }

    private void HandleUpgrade(UpgradeData data, int newLevel)
    {
        if (data.upgradeType == UpgradeType.TreeCount)
        {
            int count = Mathf.RoundToInt(data.valuePerLevel);
            for (int i = 0; i < count; i++)
            {
                SpawnTree();
            }
            Debug.Log($"TreePool: +{count} trees added. Total: {_trees.Count}");
        }
    }

    private void SpawnTree()
    {
        Tree tree = Instantiate(treePrefab, transform);
        tree.transform.position = GetRandomPosition();
        tree.OnRegrown += HandleTreeRegrown;
        _trees.Add(tree);
    }

    private void HandleTreeRegrown(Tree tree)
    {
        tree.transform.position = GetRandomPosition();
        Debug.Log("TreePool: Tree spawned at new pos");
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(spawnMin.x, spawnMax.x);
        float y = Random.Range(spawnMin.y, spawnMax.y);
        return new Vector3(x, y, 0f);
    }

    private void OnDestroy()
    {
        foreach (Tree tree in _trees)
        {
            if (tree != null)
            {
                tree.OnRegrown -= HandleTreeRegrown;
            }
        }
    }
}