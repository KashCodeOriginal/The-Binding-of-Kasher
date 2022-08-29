using System.Collections;
using UnityEngine;

public class BirdTakeOff : MonoBehaviour
{
    [SerializeField] private GameObject _bird;
    
    [SerializeField] private BirdAnimation _birdAnimation;

    [SerializeField] private DropResource _dropResource;

    [SerializeField] private ItemsData _seed;

    public void BirdTakingOff()
    {
        _dropResource.PlaceItem(_seed.Data, Random.Range(1,3), new Vector3(_bird.transform.position.x, _bird.transform.position.y / 2, _bird.transform.position.z));
        
        _birdAnimation.BirdTakingOff();

        StartCoroutine(AnimationDelay());
    }

    private IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(2f);
        _bird.SetActive(false);
    }
}
