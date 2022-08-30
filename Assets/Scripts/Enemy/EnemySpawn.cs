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

         for (int i = 0; i <= _maxEnemiesAmount; i++)
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
         for (int i = 0; i < gameObject.transform.childCount - 1; i++)
         {
            Destroy(gameObject.transform.GetChild(i));
         }
         
         _isEnemiesSpawned = false;
      }
   }

   private void CheckDayPart(string partText)
   {
      if (_dayAndNightCycle.CurrentDayPart == DayAndNightCycle.DayPart.Night)
      {
         SpawnEnemies();
      }
      else if (_dayAndNightCycle.CurrentDayPart == DayAndNightCycle.DayPart.Morning)
      {
         DestroyEnemies();
      }
   }

   private void OnEnable()
   {
      _dayAndNightCycle.CurrentDayPartChanged += CheckDayPart;
   }
}
