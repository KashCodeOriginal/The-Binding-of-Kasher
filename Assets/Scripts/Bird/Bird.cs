using UnityEngine;

public class Bird : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            if (player != null)
            {
                gameObject.GetComponent<Animation>().Play("BirdTakeOff");
            }
        }
        else if (collider.CompareTag("Mine") || collider.CompareTag("Oven") || collider.CompareTag("Farm") || collider.CompareTag("Lighthouse"))
        {
            gameObject.GetComponent<Animation>().Play("BirdTakeOff");
        }
    }
}
