using System;
using UnityEngine;

public class SaveableObject : MonoBehaviour
{
    private void Awake()
    {
        WorldSaveSystem._savingObjects.Add(this);
    }

    private void OnDestroy()
    {
        WorldSaveSystem._savingObjects.Remove(this);
    }
}
