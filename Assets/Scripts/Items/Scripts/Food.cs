using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/Item/Food")]
public class Food : ItemsData
{
    [SerializeField] private int _recoveryValue;

    public int RecoveryValue => _recoveryValue;
}
