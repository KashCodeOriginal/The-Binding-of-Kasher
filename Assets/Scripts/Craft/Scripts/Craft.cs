using UnityEngine;

public class Craft : MonoBehaviour
{
    [SerializeField] private CraftRecipe _craftRecipe;
    
    public void CraftButtonClick()
    {
        TryCraftItem();
    }

    private void TryCraftItem()
    {
        if (_craftRecipe.CanCraftItem())
        {
            _craftRecipe.Craft();
        }
    }
}
