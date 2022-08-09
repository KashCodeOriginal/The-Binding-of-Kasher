using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _woodInterface;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Tree")
        {
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            _woodInterface.SetActive(true);
            _woodInterface.GetComponent<CollectWoodDisplay>().StartCollectWoodButton(true);
        }
    }
}
