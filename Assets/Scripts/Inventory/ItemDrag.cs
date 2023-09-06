using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class ItemDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rect = null;

    private float timeOut = 0f;

    private bool isClick = false;
    private bool isDrag = false;


    public void OnPointerDown(PointerEventData eventData)
    {
        isClick = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isClick && Input.GetMouseButton(0))
        {
            timeOut += Time.deltaTime;
            if (timeOut >= 0.3f)
            {
                isDrag = true;
                GetComponentInParent<Canvas>().sortingOrder = 100;
                transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            }
        }

        if (isDrag && Input.GetMouseButtonUp(0))
        {
            isClick = isDrag = false;
            GetComponentInParent<Canvas>().sortingOrder = 0;
            InventorySlot.EquipEvent.Invoke(GetComponentInParent<InventorySlot>());
        }
    }

}
