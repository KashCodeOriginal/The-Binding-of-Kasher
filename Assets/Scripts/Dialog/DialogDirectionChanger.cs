using UnityEngine;

public class DialogDirectionChanger : MonoBehaviour
{
   [SerializeField] private RectTransform _dialogWindow;
   
   public void ChangeDirectionToPlayer()
   {
      if (_dialogWindow.eulerAngles.y != 180)
      {
         _dialogWindow.eulerAngles = new Vector3(0, 180, 0);
      }
   }
   public void ChangeDirectionToDirector()
   {
      if (_dialogWindow.eulerAngles.y != 0)
      {
         _dialogWindow.eulerAngles = new Vector3(0, 0, 0);
      }
   }
}
