using UnityEngine;

public class LighthouseDisplay : MonoBehaviour
{
   [SerializeField] private GameObject _lighthouseInterface;

   private void Start()
   {
      LighthouseInterfaceDisplay();
   }

   public void LighthouseInterfaceDisplay()
   {
      if (_lighthouseInterface.activeSelf == true)
      {
         _lighthouseInterface.SetActive(false);
         return;
      }
      _lighthouseInterface.SetActive(true);
   }
}
