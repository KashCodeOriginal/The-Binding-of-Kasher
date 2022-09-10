using UnityEngine;

[System.Serializable]
public class ObjectsSaving
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
    
    public ObjectsSaving(SaveableObject saveableObject)
    {
        _name = saveableObject.name;

        if (saveableObject.tag != null)
        {
            _tag = saveableObject.tag;
        }

        Vector3 objectPosition = saveableObject.transform.position;

        _position = new float[]
        {
            objectPosition.x, objectPosition.y, objectPosition.z
        };

        saveableObject.TryGetComponent(out PlantsGrowing plantsGrowing);

        if (plantsGrowing != null)
        {
            _currentStage = plantsGrowing.CurrentStage;
            _currentStageTime = plantsGrowing.CurrentStageTime;
        }
    }
}
