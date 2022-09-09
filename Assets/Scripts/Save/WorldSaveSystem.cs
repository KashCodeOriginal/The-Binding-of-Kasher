using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class WorldSaveSystem : MonoBehaviour
{
    [SerializeField] private GameObject _dynamicMap;

    [SerializeField] private GameObject _woodPrefab;
    [SerializeField] private GameObject _wheatPrefab;
    [SerializeField] private GameObject _torchPrefab;

    public static List<SaveObject> _savingObjects = new List<SaveObject>();

    [SerializeField] private string _savingPath;
    [SerializeField] private string _savingObjectsCountPath;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Save();
        }
    }

    private void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        
        string path = Path.Combine( Application.persistentDataPath + _savingPath);
        string countPath = Path.Combine(Application.persistentDataPath + _savingObjectsCountPath);

        FileStream countStream = new FileStream(countPath, FileMode.Create);
        
        binaryFormatter.Serialize(countStream, _savingObjects.Count);
        countStream.Close();
        
        for (int i = 0; i < _savingObjects.Count; i++)
        {
            FileStream fileStream = new FileStream(path + i, FileMode.Create);
            SavingObject savingObject = new SavingObject(_savingObjects[i]);
            
            binaryFormatter.Serialize(fileStream, savingObject);
            
            fileStream.Close();
        }
    }

    private void Load()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath + _savingPath);
        string countPath = Path.Combine(Application.persistentDataPath + _savingObjectsCountPath);

        int fishCount = 0;

        if (File.Exists(countPath))
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open);
           fishCount = (int)binaryFormatter.Deserialize(countStream); 
           countStream.Close();
        }
        
        for (int i = 0; i < fishCount; i++)
        {
            if (File.Exists(path + i))
            {
                FileStream fileStream = new FileStream(path + i, FileMode.Open);

                SavingObject obj = (SavingObject)binaryFormatter.Deserialize(fileStream);
                
                GameObject prefab = null;
                
                if (obj.Name.ToLower().Contains("tree"))
                {
                    prefab = _woodPrefab;
                }
                else if (obj.Name.ToLower().Contains("wheat"))
                {
                    prefab = _wheatPrefab;
                }
                else if (obj.Name.ToLower().Contains("torch"))
                {
                    prefab = _torchPrefab;
                }
                
                if (prefab != null)
                {
                    GameObject gameObject = Instantiate(prefab, new Vector3(obj.Position[0], obj.Position[1], obj.Position[2]), Quaternion.identity, _dynamicMap.transform);
                
                    gameObject.TryGetComponent(out PlantsGrowing plantsGrowing);
                
                     if (plantsGrowing != null)
                     {
                         plantsGrowing.SetCurrentStage(obj.CurrentStage);
                         plantsGrowing.SetCurrentGrowTime(obj.CurrentStageTime);
                     }
                }
                fileStream.Close();
            }
            
        }
    }
}