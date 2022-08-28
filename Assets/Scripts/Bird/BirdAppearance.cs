using UnityEngine;
using System.Collections;

public class BirdAppearance : MonoBehaviour
{
    [SerializeField] private BirdSpawner _birdSpawner;

    [SerializeField] private float _timeBetweenBirdAppearance;

    [SerializeField] private float _birdAppearanceChance;

    private void Start()
    {
        StartCoroutine(BirdAppearanceChance());
    }
    
    private void Update()
    {
        if (gameObject.activeSelf == false)
        {
            
        }
    }

    private IEnumerator BirdAppearanceChance()
    {
        while (true)
        {
            if (gameObject.activeSelf == false)
            {
                var randomValue = Random.Range(0, 100);

                if (randomValue <= _birdAppearanceChance)
                {
                    _birdSpawner.SpawnBird();
                }
                yield return new WaitForSeconds(_timeBetweenBirdAppearance);
            }
        }
        
    }
}
