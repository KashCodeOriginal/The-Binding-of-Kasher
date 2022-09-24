using UnityEngine;

public class TextChanger : MonoBehaviour
{
    [SerializeField] private int _currentDialogPhraseIndex;

    [SerializeField] private PhraseContainer _phraseContainer;

    [SerializeField] private TextWriter _textWriter;

    [SerializeField] private DialogDirectionChanger _dialogDirectionChanger;
    
    [SerializeField] private TrainingDisplay _training;

    [SerializeField] private DialogImagesChanger _dialogImagesChanger;

    public void StartDialog()
    {
        _currentDialogPhraseIndex = 0;

        Phrase _currentPhrase = _phraseContainer.GetPhraseByIndex(_currentDialogPhraseIndex);

        CheckDialogDirection(_currentPhrase);

        _textWriter.WriteTextToDialogWindow(_currentPhrase.PhraseText);
        
        _dialogImagesChanger.ChangeImage(_currentPhrase);
    }

    public void ChangeDialogPhrase()
    {
        if (_textWriter.WasTextWrittenFull == false)
        {
            _textWriter.SpeedUpTextWriting();
            return;
        }
        
        _currentDialogPhraseIndex++;
        
        if (_currentDialogPhraseIndex >= _phraseContainer.PhraseContainerLenght)
        {
            _textWriter.SpeedDownTextWriting();
            _training.SetTrainingToComplete();
            return;
        }
        
        Phrase _currentPhrase = _phraseContainer.GetPhraseByIndex(_currentDialogPhraseIndex);

        CheckDialogDirection(_currentPhrase);
        
        _textWriter.WriteTextToDialogWindow(_currentPhrase.PhraseText);
        
        _dialogImagesChanger.ChangeImage(_currentPhrase);
    }

    private void CheckDialogDirection(Phrase phrase)
    {
        if (phrase.SpeakerType == Phrase.Speaker.Player)
        {
            _dialogDirectionChanger.ChangeDirectionToPlayer();
        }
        else if (phrase.SpeakerType == Phrase.Speaker.Director)
        {
            _dialogDirectionChanger.ChangeDirectionToDirector();
        }
    }

    public void SkipDialog()
    {
        _training.SetTrainingToComplete();
    }
}
