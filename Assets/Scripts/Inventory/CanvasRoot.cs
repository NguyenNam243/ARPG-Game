using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRoot : MonoBehaviour
{
    public static CanvasRoot Instance = null;


    public RectTransform rect = null;


    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

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
}
