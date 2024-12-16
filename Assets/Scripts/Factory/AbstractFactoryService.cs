using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFactoryService: BaseService
{
    [SerializeField] private List<FactoryItem> availableItems = new List<FactoryItem>();
    private Dictionary<string, GenericPool<GameObject>> itemPools;

    private void Awake()
    {
        itemPools = new Dictionary<string, GenericPool<GameObject>>();

        foreach (var item in availableItems)
        {
            itemPools[item.name] = new GenericPool<GameObject>(
                parameters => GameObject.Instantiate(item.prefab)
            );
        }
    }

    public GameObject CreateItem(string itemType)
    {
        if (!itemPools.TryGetValue(itemType, out var pool))
        {
            throw new System.InvalidOperationException($"Item type '{itemType}' not found in availableItems.");
        }

        GameObject item = pool.GetObject();
        OnItemCreated(item);
        return item;
    }

    public void StoreItem(string itemType, GameObject item)
    {
        if (itemPools.TryGetValue(itemType, out var pool))
        {
            pool.ReleaseObject(item);
            item.SetActive(false);
        }
    }

    protected virtual void OnItemCreated(GameObject item) { }
}

[System.Serializable]
public struct FactoryItem
{
    public string name;
    public GameObject prefab;
}
