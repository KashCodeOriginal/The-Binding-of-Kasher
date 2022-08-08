using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChanger : MonoBehaviour
{
   [SerializeField] private Slider _slider;
   [SerializeField] private bool _isSliderActivated;
   [SerializeField] private float _speed;
   [SerializeField] private bool _isValueIncreasing;

   [SerializeField] private SliderPoints _sliderPoints;

   [SerializeField] private GameObject _woodCollectButton;

   public void StartSliderMoving()
   {
      _isSliderActivated = true;
      _sliderPoints.CreatePoints();
      
      _woodCollectButton.SetActive(true);
   }

   private void Update()
   {
      if (_isSliderActivated == true)
      {
         SliderMove();
      }
   }

   private void SliderMove()
   {
      if (_slider.value == 1)
      {
         _isValueIncreasing = false;
      }
      else if (_slider.value == 0)
      {
         _isValueIncreasing = true;
      }

      if (_isValueIncreasing == true)
      {
         _slider.value += _speed * Time.deltaTime;
      }
      else
      {
         _slider.value -= _speed * Time.deltaTime;
      }
   }

   public void CheckTapPosition()
   {
      if (_slider.value >= _sliderPoints.FirstPoint && _slider.value <= _sliderPoints.SecondPoint)
      {
         _speed += 0.5f;
         _sliderPoints.CreatePoints();
      }
      else
      {
         _isSliderActivated = false;
      }
   }
}
