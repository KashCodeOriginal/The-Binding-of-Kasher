using UnityEngine;

public class CargoshipSail : MonoBehaviour
{
   [SerializeField] private Animation _shipAnimation;
   [SerializeField] private CargoShipItems _cargoShipItems;
   
   public void Sail()
   {
      _shipAnimation.Play("ShipSailing");
      _cargoShipItems.ClearCargoChest();
   }
}
