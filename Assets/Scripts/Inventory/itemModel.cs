using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemModel : MonoBehaviour, IPool
{
    public ItemType type;
    public string itemName;

    public bool Active => gameObject.activeInHierarchy;
    public string Name => this.ObjectName();

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void OnMouseUp()
    {
        InventoryManager.Instance.ReciveItem(this);
        ObjectPooling.Instance.PushItem(this, true);
    }
}
