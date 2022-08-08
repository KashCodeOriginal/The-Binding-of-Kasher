using UnityEngine;

public class WoodCollectionEnter : MonoBehaviour
{
    [SerializeField] private GameObject _woodInterface;

    [SerializeField] private GameObject _player;
    
    private void Start()
    {
        _woodInterface = GameObject.FindWithTag("CollectWood");
        _player = GameObject.FindWithTag("Player");
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Player>() == true)
        {
            _woodInterface.transform.GetChild(0).gameObject.SetActive(true);
            _player.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
