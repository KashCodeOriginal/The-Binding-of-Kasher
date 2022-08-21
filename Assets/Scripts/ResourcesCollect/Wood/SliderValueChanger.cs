using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderValueChanger : MonoBehaviour
{
   public event UnityAction ComboPointAdd;
   public event UnityAction ComboEnded;

   [SerializeField] private Slider _slider;
   [SerializeField] private bool _isSliderActivated;
   [SerializeField] private float _speed;
   [SerializeField] private bool _isValueIncreasing;

   [SerializeField] private SliderPoints _sliderPoints;

   [SerializeField] private CollectWoodDisplay _collectWoodDisplay;

   [SerializeField] private Animator _playerAnimator;

   public void StartSliderMoving()
   {
      _isSliderActivated = true;
      _sliderPoints.CreatePoints();

      _collectWoodDisplay.CollectWoodButtonActive(true);
      
      _playerAnimator.SetBool("IsCollectingWood", true);
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
         _speed += 0.3f;
         _sliderPoints.CreatePoints();
         ComboPointAdd?.Invoke();
      }
      else
      {
         ComboEnded?.Invoke();
         _speed = 0.3f;
         _isSliderActivated = false;
         _collectWoodDisplay.CollectWoodButtonActive(false);
         _collectWoodDisplay.CollectWoodInterfaceActive(false);
         
         _playerAnimator.SetBool("IsCollectingWood", false);
      }
   }
}
