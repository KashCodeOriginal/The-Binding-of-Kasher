using System.Collections;
using UnityEngine;

public class CowAnimations : MonoBehaviour
{
    [SerializeField] private Animation _animation;

    [SerializeField] private int _cowEatChance;

    private void Start()
    {
        StartCoroutine(CowRandomDelay());
    }

    private IEnumerator CowRandomDelay()
    {
        while (true)
        {
            var value = Random.Range(0, 100);

            if (value <= _cowEatChance)
            {
                _animation.Play("CowEating");
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
