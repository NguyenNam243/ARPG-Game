using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DG.Tweening;
using UnityEngine;

public class ChessHandle : MonoBehaviour
{
    public LayerMask groundMask;
    public float distanceDrop = 2f;
    public float dropHeight = 1f;
    public float dropTime = 0.3f;
    public List<GameObject> items = null;

    private Vector3 dropPos;
    private Animator ator = null;

    private bool opened = false;

    private void Awake()
    {
        ator = GetComponent<Animator>();

    }

    private void OnMouseEnter()
    {
        ator.Play("Bounce");

    }

    private void OnMouseExit()
    {
        ator.Play("Close");
    }

    private void OnMouseUp()
    {
        ator.Play("Open");
        DropItem();
        opened = true;
    }

    private void DropItem()
    {
        GameObject obj = items[Random.Range(0, items.Count)];
        GameObject item = Instantiate(obj);

        if (Physics.Raycast(transform.position + transform.forward * distanceDrop + transform.up * 5, Vector3.down, out RaycastHit hit, 20, groundMask))
        {
            dropPos = hit.point;
        }

        item.transform.position = transform.position;
        item.SetActive(true);

        item.transform.DOJump(dropPos + Vector3.up * (item.GetComponent<BoxCollider>().size.y + 0.5f), dropHeight, 1, dropTime);
    }

}
