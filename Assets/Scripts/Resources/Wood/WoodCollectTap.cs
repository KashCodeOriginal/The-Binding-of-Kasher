using UnityEngine;

public class WoodCollectTap : MonoBehaviour
{
    [SerializeField] private SliderValueChanger _sliderValueChanger;
    
    public void WoodCollectTapped()
    {
        _sliderValueChanger.CheckTapPosition();
    }
}
