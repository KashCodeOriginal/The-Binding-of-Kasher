using UnityEngine;

public class LighthouseDisplay : MonoBehaviour
{
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
}
