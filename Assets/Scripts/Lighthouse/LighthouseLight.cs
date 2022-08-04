using UnityEngine;

public class LighthouseLight : MonoBehaviour
{
    [SerializeField] private Light _light;
    
    public void LightsControll(bool isLightsOn)
    {
        if (isLightsOn == false)
        {
            _light.enabled = false;
            return;
        }
        _light.enabled = true;
    }
}
