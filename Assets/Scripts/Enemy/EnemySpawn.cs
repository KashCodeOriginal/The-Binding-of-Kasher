using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
   [SerializeField] private GameObject _enemyPrefab;

   [SerializeField] private DayAndNightCycle _dayAndNightCycle;
   
   [SerializeField] private int _maxEnemiesAmount;

   private bool _isEnemiesSpawned;
   
   private void SpawnEnemies()
   {
      if (_isEnemiesSpawned == false)
      {
         var randomEnemiesAmount = Random.Range(0, _maxEnemiesAmount);

         for (int i = 0; i < randomEnemiesAmount; i++)
         {
            Instantiate(_enemyPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
         }

         _isEnemiesSpawned = true;
      }
   }

   private void DestroyEnemies()
   {
      if (gameObject.transform.childCount > 0)
      {
         for (int i = 0; i < gameObject.transform.childCount; i++)
         {
            Destroy(gameObject.transform.GetChild(i).gameObject);
         }
         
         _isEnemiesSpawned = false;
      }
   }

   private void CheckDayPart(string partText)
   {
      switch (_dayAndNightCycle.CurrentDayPart)
      {
         case DayAndNightCycle.DayPart.Night:
            SpawnEnemies();
            break;
         case DayAndNightCycle.DayPart.Morning:
            DestroyEnemies();
            break;
      }
   }

   private void OnEnable()
   {
      _dayAndNightCycle.CurrentDayPartChanged += CheckDayPart;
   }
   private void OnDisable()
   {
      _dayAndNightCycle.CurrentDayPartChanged -= CheckDayPart;
   }
}
