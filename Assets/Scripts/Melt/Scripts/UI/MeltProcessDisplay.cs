using UnityEngine;
using UnityEngine.UI;

public class MeltProcessDisplay : MonoBehaviour
{
   [SerializeField] private Image _arrow;
   [SerializeField] private Image _coal;
   
   public void FillArrow(float value)
   {
      _arrow.fillAmount = value;
   } 
   public void WithdrawCoal(float value)
   {
      _coal.fillAmount = value;
   } 
}
