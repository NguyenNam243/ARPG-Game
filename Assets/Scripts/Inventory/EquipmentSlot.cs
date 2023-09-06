using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Image iconImg = null;

    public bool HasItem { get; private set; }


    public static UnityEvent<InventorySlot> EquipEvent = new UnityEvent<InventorySlot>();


    private RectTransform rect = null;


    private void Awake()
    {
        EquipEvent.AddListener(OnEquip);
        rect = GetComponent<RectTransform>();
    }

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

    private void OnEquip(InventorySlot slot)
    {
        if (Vector3.Distance(transform.position, Input.mousePosition) <= rect.sizeDelta.x)
        {
            slot.iconImg.enabled = false;
            slot.iconImg.rectTransform.anchoredPosition = Vector2.zero;
            Attach(slot.itemModel);
        }
    }
}
