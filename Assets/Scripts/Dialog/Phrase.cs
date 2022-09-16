using UnityEngine;

[System.Serializable]
public class Phrase
{
    [SerializeField] private string _phraseText;
    [SerializeField] private Speaker _speakerType;
    [SerializeField] private Sprite _sprite; 

    public string PhraseText => _phraseText;
    public Speaker SpeakerType => _speakerType;
    public Sprite Sprite => _sprite;

    public Phrase(string phraseText, Speaker speakerType, Sprite sprite)
    {
        _phraseText = phraseText;
        _speakerType = speakerType;
        _sprite = sprite;
    }
    
    public enum Speaker
    {
        Player,
        Director
    }
}
