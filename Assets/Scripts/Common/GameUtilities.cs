using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GameUtilities
{
    public static void CreateContainer(this MonoBehaviour mono, string name, ref Transform trans)
    {
        GameObject obj = new GameObject(name);
        obj.transform.parent = mono.transform;
        trans = obj.transform;
    }

    public static string ObjectName(this MonoBehaviour mono)
    {
        return mono.gameObject.name.Replace("(Clone)", "");
    }

    public static void Show(this CanvasGroup canvas)
    {
        canvas.alpha = 1;
        canvas.blocksRaycasts = canvas.interactable = true;
    }

    public static void Hide(this CanvasGroup canvas)
    {
        canvas.alpha = 0;
        canvas.blocksRaycasts = canvas.interactable = false;
    }
}
