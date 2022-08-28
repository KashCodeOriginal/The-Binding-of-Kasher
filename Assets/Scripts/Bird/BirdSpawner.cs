using Unity.VisualScripting;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
   [SerializeField] private float _minX;
   [SerializeField] private float _maxX;
   [SerializeField] private float _minZ;
   [SerializeField] private float _maxZ;
   
   [SerializeField] private float _height;
   
   public void SpawnBird()
   {
      gameObject.SetActive(true);
      gameObject.transform.position = GetRandomPosition();
   }

   private Vector3 GetRandomPosition()
   {
      Vector3 position = new Vector3(Random.Range(_minX, _maxX), _height, Random.Range(_minZ, _maxZ));
      
      return position;
   }
}
