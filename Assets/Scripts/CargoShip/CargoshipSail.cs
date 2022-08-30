using UnityEngine;

public class CargoshipSail : MonoBehaviour
{
   [SerializeField] private CargoShipAnimator _shipAnimator;
   [SerializeField] private CargoShipItems _cargoShipItems;
   
   public void Sail()
   {
      _shipAnimator.StartShipSailing();
      _cargoShipItems.ClearCargoChest();
   }
}
