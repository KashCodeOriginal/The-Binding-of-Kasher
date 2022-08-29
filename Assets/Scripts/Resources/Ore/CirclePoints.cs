using UnityEngine;

public class CirclePoints : MonoBehaviour
{
    [SerializeField] private float _firstPointValue;
    [SerializeField] private float _secondPointValue;
    
    public float FirstPointValue => _firstPointValue;
    public float SecondPointValue => _secondPointValue;

    [SerializeField] private GameObject _firstPoint;
    [SerializeField] private GameObject _secondPoint;

    [SerializeField] private int _maxPointsValue;

    [SerializeField] private int _distanceBetweenFirstAndSecondPoints;
    
    public void CreatePoints()
    {
        _firstPointValue = Random.Range(0, _maxPointsValue - _distanceBetweenFirstAndSecondPoints);

        _secondPointValue = _firstPointValue + _distanceBetweenFirstAndSecondPoints;
        
        var localFirstPointValue = (_firstPointValue < 180f) ? _firstPointValue : _firstPointValue - 360;
        var localSecondPointValue = (_secondPointValue < 180f) ? _secondPointValue : _secondPointValue - 360;

        Vector3 rotation = new Vector3(0, 0, localFirstPointValue);

        _firstPoint.transform.rotation = Quaternion.Euler(rotation);
        
        rotation = new Vector3(0, 0, localSecondPointValue);
        
        _secondPoint.transform.rotation = Quaternion.Euler(rotation);

        if (_distanceBetweenFirstAndSecondPoints > 15)
        {
            _distanceBetweenFirstAndSecondPoints -= 5;
        }
    }

    public void SetDistanceBetweenPoints(int value)
    {
        _distanceBetweenFirstAndSecondPoints = value;
    }
}
