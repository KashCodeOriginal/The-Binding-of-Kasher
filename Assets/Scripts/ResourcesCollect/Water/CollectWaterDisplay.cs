using UnityEngine;

public class CollectWaterDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _waterInterface;
    
    public bool DisplayWaterInterface()
    {
        if (_waterInterface.activeSelf == true)
        {
            _waterInterface.SetActive(false);
            return false;
        }
        _waterInterface.SetActive(true);
        return true;
    }
}
