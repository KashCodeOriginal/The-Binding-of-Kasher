using UnityEngine;

[System.Serializable]
public class SavingObject
{
    [SerializeField] private string _name;
    
    [SerializeField] private string _tag;
    
    [SerializeField] private float[] _position;

    [SerializeField] private int _currentStage;

    [SerializeField] private float _currentStageTime;
    
    public string Name => _name;
    public string Tag => _tag;
    public float[] Position => _position;
    public int CurrentStage => _currentStage;
    public float CurrentStageTime => _currentStageTime;
    
    public SavingObject(SaveObject saveObject)
    {
        _name = saveObject.name;

        if (saveObject.tag != null)
        {
            _tag = saveObject.tag;
        }

        Vector3 objectPosition = saveObject.transform.position;

        _position = new float[]
        {
            objectPosition.x, objectPosition.y, objectPosition.z
        };

        saveObject.TryGetComponent(out PlantsGrowing plantsGrowing);

        if (plantsGrowing != null)
        {
            _currentStage = plantsGrowing.CurrentStage;
            _currentStageTime = plantsGrowing.CurrentStageTime;
        }
    }
}
