using System.Collections.Generic;
using UnityEngine;

public class PlantsGrowing : MonoBehaviour
{
    [SerializeField] private float _timeBetweenStages;

    [SerializeField] private int _currentStage;

    private float _currentStageTime;
    
    public int CurrentStage => _currentStage;

    public float CurrentStageTime => _currentStageTime;
    
    [SerializeField] private List<GameObject> _stages;
    private void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            _stages.Add(gameObject.transform.GetChild(i).gameObject);
        }
        
        SetStage(_currentStage);
    }

    private void Update()
    {
        if (_currentStage < _stages.Count - 1)
        {
            _currentStageTime += Time.deltaTime;

            if (_currentStageTime >= _timeBetweenStages)
            {
                _currentStageTime = 0;
                _currentStage++;
                SetStage(_currentStage);
            }
        }
    }

    public void SetStage(int stage)
    {
        if (stage == 0)
        {
            _stages[stage].SetActive(true);
            return;
        }
        
        _stages[stage - 1].SetActive(false);
        _stages[stage].SetActive(true);
    }
    public void SetCurrentGrowTime(float time)
    {
        _currentStageTime = time;
    }

    public void SetCurrentStage(int value)
    {
        _currentStage = value;
    }
}
