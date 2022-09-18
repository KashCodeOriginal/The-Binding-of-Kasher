using UnityEngine;

public class LighthouseDisplay : MonoBehaviour, IInteractable
{
   public bool IsActive { get; private set; } = false;
   
   [SerializeField] private GameObject _lighthouseInterface;
   
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
}
