using System;
using UnityEngine;
using UnityEngine.UI;

public class LighthouseDisplay : MonoBehaviour, IInteractable
{
   public bool IsActive { get; private set; } = false;
   
   [SerializeField] private GameObject _lighthouseInterface;

   [SerializeField] private Image _lighthouseIndicatorImage;

   [SerializeField] private Lighthouse _lighthouse;
   
   private void Start()
   {
      HideLighthouseInterface();
   }

   public void DisplayLighthouseInterface()
   {
      _lighthouseInterface.SetActive(true);
   }
   public void HideLighthouseInterface()
   {
      _lighthouseInterface.SetActive(false);
   }

   
   public void Interact()
   {
      if (IsActive == false)
      {
         DisplayLighthouseInterface();
         IsActive = true;
         return;
      }
      HideLighthouseInterface();
      IsActive = false;
   }

   private void OnEnable()
   {
      _lighthouse.IsLighthouseIndicatorTurnedOn += TurnOnLighthouseIndicator;
      _lighthouse.IsLighthouseIndicatorTurnedOff += TurnOffLighthouseIndicator;
   }

   private void OnDisable()
   {
      _lighthouse.IsLighthouseIndicatorTurnedOn -= TurnOnLighthouseIndicator;
      _lighthouse.IsLighthouseIndicatorTurnedOff -= TurnOffLighthouseIndicator;
   }

   private void TurnOnLighthouseIndicator()
   {
      _lighthouseIndicatorImage.color = new Color(0, 255, 0, 255);
   }
   private void TurnOffLighthouseIndicator()
   {
      _lighthouseIndicatorImage.color = new Color(255, 0, 0, 255);
   }
}
