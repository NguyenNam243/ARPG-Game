using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesTest : MonoBehaviour
{
    private SkinnedMeshRenderer skinMesh = null;

    private void Awake()
    {
        skinMesh = GetComponent<SkinnedMeshRenderer>();
        Transform[] bones = skinMesh.bones;
        foreach (var item in bones)
        {
            Debug.Log(item.name);
        }
    }
}
