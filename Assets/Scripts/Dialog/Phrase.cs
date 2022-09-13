using UnityEngine;

[System.Serializable]
public class Phrase
{
    [SerializeField] private string _phraseText;
    [SerializeField] private Speaker _speakerType;

    public string PhraseText => _phraseText;
    public Speaker SpeakerType => _speakerType;

    public Phrase(string phraseText, Speaker speakerType)
    {
        _phraseText = phraseText;
        _speakerType = speakerType;
    }
    
    public enum Speaker
    {
        Player,
        Director
    }
}
