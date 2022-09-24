using UnityEngine;

public class TrainingDisplay : MonoBehaviour
{
    [SerializeField] private int _wasTrainingCompleted;

    [SerializeField] private TextChanger _textChanger;

    [SerializeField] private GameStarted _gameStarted;

    [SerializeField] private GameObject _dialogInterface;

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveTrainingInfo();
        } 
    }
#endif
    private void OnApplicationQuit()
    {
        SaveTrainingInfo();
    }


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

    public void SaveTrainingInfo()
    {
        PlayerPrefs.SetInt("WasTrainingCompleted", _wasTrainingCompleted);
    }

    public void LoadTrainingInfo()
    {
        _wasTrainingCompleted = PlayerPrefs.GetInt("WasTrainingCompleted", 0);
    }

    public void SetTrainingToComplete()
    {
        _wasTrainingCompleted = 1;
        _gameStarted.StartBlackScreenDisappearAnimation();
        _dialogInterface.SetActive(false);
    }
}
