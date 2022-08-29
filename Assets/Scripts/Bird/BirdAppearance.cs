using UnityEngine;
using System.Collections;

public class BirdAppearance : MonoBehaviour
{
    [SerializeField] private BirdSpawner _birdSpawner;

    [SerializeField] private float _timeBetweenBirdAppearance;

    [SerializeField] private float _birdAppearanceChance;

    [SerializeField] private GameObject _bird;

    private void Start()
    {
        StartCoroutine(BirdAppearanceChance());
    }

    private IEnumerator BirdAppearanceChance()
    {
        while (true)
        {
            if (_bird.activeSelf == false)
            {
                var randomValue = Random.Range(0, 100);
            
                if (randomValue <= _birdAppearanceChance)
                {
                    _birdSpawner.SpawnBird();
                }
            }
            yield return new WaitForSeconds(_timeBetweenBirdAppearance);
        }
        
    }
}
