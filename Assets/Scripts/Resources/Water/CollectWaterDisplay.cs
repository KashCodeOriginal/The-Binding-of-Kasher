using UnityEngine;

public class CollectWaterDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _waterInterface;
    
    public void DisplayWaterInterface()
    {
        _waterInterface.SetActive(true);
        
    }
    public void HideWaterInterface()
    {
        _waterInterface.SetActive(false);
    }
}
