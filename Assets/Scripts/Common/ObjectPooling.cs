using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance = null;


    private readonly string ITEM_PATH = "Items/{0}";

    private List<itemModel> items = new List<itemModel>();


    private Transform itemsContainer = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        this.CreateContainer("Items", ref itemsContainer);
    }


    public itemModel PopItem(string itemName, Transform newParent, bool show)
    {
        itemModel item = PopFromPool(itemName, items, string.Format(ITEM_PATH, itemName), newParent, show);
        return item;
    }

    public void PushItem(itemModel item, bool hide)
    {
        PushToPool(item, items, itemsContainer, hide);
    }

    public T PopFromPool<T>(string itemName, List<T> collection, string itemPath, Transform newParent, bool show) where T : IPool, new()
    {
        T item = collection.Find(i => i.Name.Equals(itemName));
        if (item == null)
        {
            GameObject obj = Resources.Load<GameObject>(itemPath);
            GameObject prefab = Instantiate(obj, newParent);
            item = prefab.GetComponent<T>();
        }

        if (show)
            item.Show();
        item.SetParent(newParent);

        if (collection.Contains(item))
            collection.Remove(item);

        return item;
    }

    public void PushToPool<T>(T item, List<T> collection, Transform container, bool hide) where T : IPool, new()
    {
        if (!collection.Contains(item))
            collection.Add(item);

        item.SetParent(container);

        if (hide)
            item.Hide();
    }
}
