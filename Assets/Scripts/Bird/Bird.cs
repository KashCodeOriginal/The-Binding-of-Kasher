using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private BirdTakeOff _birdTakeOff;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            if (player != null)
            {
                _birdTakeOff.BirdTakingOff();
            }
        }
        else if (collider.CompareTag("Mine") || collider.CompareTag("Oven") || collider.CompareTag("Farm") || collider.CompareTag("Lighthouse"))
        {
            _birdTakeOff.BirdTakingOff();
        }
    }
}
