using UnityEngine;
using UnityEngine.UI;

public class DialogImagesChanger : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    [SerializeField] private PhraseContainer _phraseContainer;

    public void ChangeImage(Phrase currentPhrase)
    {
        if (currentPhrase.Sprite != null)
        {
            _image.sprite = currentPhrase.Sprite;
        }
    }
}
