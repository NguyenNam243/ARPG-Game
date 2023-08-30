using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance = null;

    private CanvasGroup cvGr = null;

    private List<InventorySlot> slots = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        cvGr = GetComponent<CanvasGroup>();
        slots = GetComponentsInChildren<InventorySlot>().ToList();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void Show()
    {
        cvGr.Show();
    }

    public void Hide()
    {
        cvGr.Hide();
    }

    public void ReciveItem(itemModel item)
    {
        InventorySlot slot = GetSlotEmpty();
        if (slot == null)
        {
            // Full 
            return;
        }

        slot.Attach(item);
    }

    private InventorySlot GetSlotEmpty()
    {
        foreach (var slot in slots)
        {
            if (!slot.HasItem)
                return slot;
        }
        return null;
    }
}
