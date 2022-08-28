using UnityEngine;

public class OvenDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _ovenInterface;

    public void DisplayOvenInterface()
    {
        _ovenInterface.SetActive(true);
    }
    public void HideOvenInterface()
    {
        _ovenInterface.SetActive(false);
    }
}
