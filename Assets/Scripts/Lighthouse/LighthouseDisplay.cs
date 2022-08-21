using UnityEngine;

public class LighthouseDisplay : MonoBehaviour
{
   [SerializeField] private GameObject _lighthouseInterface;

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
