using UnityEngine;

[System.Serializable]
public class ItemBuff
{
    [SerializeField] private Attributes _attribute;
    [SerializeField] private int _value;

    [SerializeField] private int _min;
    [SerializeField] private int _max;

    public Attributes Attribute => _attribute;
    public int Value => _value;
    public int Min => _min;
    public int Max => _max;

    public ItemBuff(int min, int max)
    {
        _min = min;
        _max = max;
        GenerateValues();
    }

    public void GenerateValues()
    {
        _value = Random.Range(_min, _max);
    }

    public void SetAttribute(Attributes attribute)
    {
        _attribute = attribute;
    }
}
