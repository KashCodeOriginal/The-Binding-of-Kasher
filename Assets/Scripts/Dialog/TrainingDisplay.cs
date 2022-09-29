using System;
using UnityEngine;

public class TrainingDisplay : MonoBehaviour
{
    [SerializeField] private float _wasTrainingCompleted;

    [SerializeField] private TextChanger _textChanger;

    [SerializeField] private GameStarted _gameStarted;

    [SerializeField] private GameObject _dialogInterface;
    
    public void Start()
    {
        LoadTrainingInfo();

        if (_wasTrainingCompleted == 0)
        {
            _textChanger.StartDialog();
        }
        else
        {
            _gameStarted.StartBlackScreenDisappearAnimation();
            _dialogInterface.SetActive(false);
        }
    }

    private void SaveTrainingInfo()
    {
        PlayerPrefs.SetFloat("WasTrainingCompleted", _wasTrainingCompleted);
    }

    private void LoadTrainingInfo()
    {
        _wasTrainingCompleted = 0;  //PlayerPrefs.GetFloat("WasTrainingCompleted");
    }

    public void SetTrainingToComplete()
    {
        _wasTrainingCompleted = 1;
        _gameStarted.StartBlackScreenDisappearAnimation();
        _dialogInterface.SetActive(false);
        SaveTrainingInfo();
    }
}
