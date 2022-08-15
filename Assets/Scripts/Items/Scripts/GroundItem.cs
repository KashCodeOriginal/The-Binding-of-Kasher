using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField] private ItemsData _item;
    [SerializeField] private int _amount;
    public ItemsData Item => _item;
    public int Amount => _amount;
    
    public void OnAfterDeserialize()
    {
        
    }
    public void OnBeforeSerialize()
    {
    #if UNITY_EDITOR
        GetComponent<MeshFilter>().mesh = _item.Mesh;
        EditorUtility.SetDirty(GetComponent<MeshFilter>());
    #endif
    }

    public void SetAmount(int value)
    {
        _amount = value;
    }

}
