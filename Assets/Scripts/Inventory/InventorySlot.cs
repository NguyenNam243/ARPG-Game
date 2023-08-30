using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image iconImg = null;

    public bool HasItem { get; private set; }


    public void Attach(itemModel data)
    {
        HasItem = true;
        ItemObject itemObj = Resources.Load<ItemObject>("Object/Items/" + data.itemName);
        iconImg.sprite = itemObj.itemIcon;
        iconImg.enabled = true;
    }

    public void UnAttach()
    {
        HasItem = false;
    }
}
