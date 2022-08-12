using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField] private ItemsData _item;
    [SerializeField] private int amount = 1;
    public ItemsData Item => _item;
    public int Amount => amount;
    
    public void OnAfterDeserialize()
    {
        
    }
    public void OnBeforeSerialize()
    {
        GetComponent<MeshFilter>().mesh = _item.Mesh;
        EditorUtility.SetDirty(GetComponent<MeshFilter>());
    }

    public void SetAmount(int value)
    {
        amount = value;
    }

}
