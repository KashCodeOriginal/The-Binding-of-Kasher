using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField] private ItemsData _item;
    public ItemsData Item => _item;
    
    public void OnAfterDeserialize()
    {
        
    }
    public void OnBeforeSerialize()
    {
        GetComponent<MeshFilter>().mesh = _item.Prefab;
        EditorUtility.SetDirty(GetComponent<MeshFilter>());
    }

}
