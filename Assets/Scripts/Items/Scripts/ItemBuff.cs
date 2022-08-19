using UnityEngine;

[System.Serializable]
public class ItemBuff
{
    [SerializeField] private Attributes _attribute;
    [SerializeField] private int _value;
    
    public Attributes Attribute => _attribute;
    public int Value => _value;

    public ItemBuff(int value)
    {
        _value = value;
    }
    
    public void SetAttribute(Attributes attribute)
    {
        _attribute = attribute;
    }
}
