using System;
using UnityEngine;

public class SaveObject : MonoBehaviour
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
