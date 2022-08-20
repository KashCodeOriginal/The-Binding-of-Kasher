using UnityEngine;

public class Lighthouse : MonoBehaviour
{
    [SerializeField] private float _woodBurningTime;
    [SerializeField] private int _woodAmount;

    [SerializeField] private LighthouseLight _lighthouseLight;

    [SerializeField] private LighthouseFill _lighthouseFill;
    
    private float _currentWoodBurningTime;

    private bool _isWoodBurning;
    private void Update()
    {
        if (_woodAmount > 0 && _isWoodBurning == false)
        {
            BurnWood();
        }
        else if (_isWoodBurning == true)
        {
            _currentWoodBurningTime += Time.deltaTime;
            
            if (_currentWoodBurningTime >= _woodBurningTime)
            {
                _isWoodBurning = false;
                _currentWoodBurningTime = 0;
                _lighthouseLight.LightsControll(false);
            }
        }
    }
    
    private void BurnWood()
    {
        _isWoodBurning = true;
        _woodAmount--;

        _lighthouseLight.LightsControll(true);
    }
}
