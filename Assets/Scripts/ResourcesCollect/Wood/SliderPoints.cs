using UnityEngine;

public class SliderPoints : MonoBehaviour
{
    [SerializeField] private float _firstPoint;
    [SerializeField] private float _secondPoint;
    public float FirstPoint => _firstPoint;
    public float SecondPoint => _secondPoint;
    
    [SerializeField] private float _minFirstPointValue;
    [SerializeField] private float _maxFirstPointValue;

    [SerializeField] private float _fromFirstToSecondPointValue;
    
    [SerializeField] private GameObject _touchableArea;
    
    public void CreatePoints()
    {
        if (_touchableArea.activeSelf == false)
        {
            _touchableArea.SetActive(true);
        }

        _maxFirstPointValue = 1 - _fromFirstToSecondPointValue;
        
        _firstPoint = Random.Range(_minFirstPointValue, _maxFirstPointValue);
        _secondPoint = _firstPoint + _fromFirstToSecondPointValue;

        float middlePoint = ((_firstPoint + _secondPoint) / 2) * -130;
        
        _touchableArea.GetComponent<RectTransform>().localPosition = new Vector3(0, middlePoint + 70, 0);
    }
}
