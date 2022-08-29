using System.Collections.Generic;
using UnityEngine;

public class PlantsGrowing : MonoBehaviour
{
    [SerializeField] private float _timeBetweenStages;

    [SerializeField] private int _currentStage;

    private float _currentStageTime;
    
    [SerializeField] private List<GameObject> _stages;
    private void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            _stages.Add(gameObject.transform.GetChild(i).gameObject);
        }
        
        _currentStage = 0;
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

    private void SetStage(int stage)
    {
        if (stage == 0)
        {
            _stages[stage].SetActive(true);
            return;
        }
        _stages[stage - 1].SetActive(false);
        _stages[stage].SetActive(true);
    }
}
