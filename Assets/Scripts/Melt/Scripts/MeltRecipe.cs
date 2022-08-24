using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/Melt")]
[System.Serializable]
public class MeltRecipe : ScriptableObject
{
    [SerializeField] private ItemsData _outputItem;

    [SerializeField] private int _outputItemAmount;
    
    [SerializeField] private int _meltTime;
    
    public int MeltTime => _meltTime;
    
    public int OutputItemAmount => _outputItemAmount;
    
    public ItemsData OutputItem => _outputItem;

    [SerializeField] private CraftItem[] _requiredItems;

    public CraftItem[] RequiredItems => _requiredItems;
}
