using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    [SerializeField] private ItemsData _item;
    [SerializeField] private int _amount;
    public ItemsData Item => _item;
    public int Amount => _amount;
    
    public void SetAmount(int value)
    {
        _amount = value;
    }

    public void SetItem(ItemsData item)
    {
        _item = item;
    }
}
