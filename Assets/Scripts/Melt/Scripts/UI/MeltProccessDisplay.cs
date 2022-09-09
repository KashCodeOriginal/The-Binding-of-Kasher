using UnityEngine;
using UnityEngine.UI;

public class MeltProccessDisplay : MonoBehaviour
{
   public void FillArrow(float value)
   {
      gameObject.GetComponent<Image>().fillAmount = value;
   } 
}
