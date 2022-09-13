using System.Collections.Generic;
using UnityEngine;

public class PhraseContainer : MonoBehaviour
{
    [SerializeField] private List<Phrase> _dialogPhrases = new List<Phrase>();

     public int PhraseContainerLenght => _dialogPhrases.Count;
    
     public Phrase GetPhraseByIndex(int index)
     {
         return _dialogPhrases[index];
     }
}
