using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
   public bool WasTextWrittenFull => _wasTextWrittenFull;
   
   [SerializeField] private TextMeshProUGUI _textWindow;
   
   [SerializeField] private float _regularCharAppearTime;

   [SerializeField] private TextWritingSound _textWritingSound;

   private float _appearTime;

   private string _currentText;

   private float _currentCharAppearTime;

   private bool _isTextAbleToWrite;

   private int _charIndex;

   private bool _wasTextWrittenFull;
   
   public void WriteTextToDialogWindow(string text)
   {
      _isTextAbleToWrite = true;
      
      _currentText = text;
      
      _wasTextWrittenFull = false;
      
      _charIndex = 0;

      _appearTime = _regularCharAppearTime;
   }

   private void Update()
   {
      if (_isTextAbleToWrite == true)
      {
         _currentCharAppearTime += Time.fixedDeltaTime;

         if (_currentCharAppearTime >= _appearTime)
         {
            _charIndex += 1;
            
            string tempText = _currentText.Substring(0, _charIndex);
            
            tempText += "<color=#00000000>" + _currentText.Substring(_charIndex) + "</color>";
            
            _textWindow.text = tempText;

            _currentCharAppearTime = 0;

            if (_appearTime == _regularCharAppearTime)
            {
               _textWritingSound.PlayCharSound();
            }
            
            if (_charIndex >= _currentText.Length)
            {
               _isTextAbleToWrite = false;
               _wasTextWrittenFull = true;
            }
         }
      }
   }

   public void SpeedUpTextWriting()
   {
      _charIndex = _currentText.Length - 1;
   }
   public void SpeedDownTextWriting()
   {
      _appearTime = _regularCharAppearTime;
   }
}
