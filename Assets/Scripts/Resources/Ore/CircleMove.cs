using UnityEngine;
using UnityEngine.Events;

public class CircleMove : MonoBehaviour
{
    [SerializeField] private GameObject _ball;

    [SerializeField] private float _ballStep;

    [SerializeField] private float _ballSpeed;

    [SerializeField] private CirclePoints _circlePoints;

    [SerializeField] private bool _isCircleActivated;

    [SerializeField] private CollectOreDisplay _collectOreDisplay;

    [SerializeField] private CollectOre _collectOre;

    [SerializeField] private EnergyForActionCheck _energyForActionCheck;
    
    public event UnityAction ComboPointAdd;
    
    public event UnityAction ComboEnded;
    

    public void StartCircleMoving()
    {
        _isCircleActivated = true;
        _circlePoints.CreatePoints();

        _collectOreDisplay.CollectOreButtonActive(true);
      
        //_playerAnimator.SetBool("IsCollectingWood", true);
    }
    
    private void Update()
    {
        if (_isCircleActivated == true)
        {
            BallMove();
        }
    }

    private void BallMove()
    {
        _ball.transform.Rotate(new Vector3(0,0,_ballSpeed * _ballStep) * Time.deltaTime);
    }
    
    public void OreCollectTapped()
    {
        CheckTapPosition();
    }
    
    private void CheckTapPosition()
    {
        if (_energyForActionCheck.IsPlayerHasEnoughEnergy(_collectOre.SpentEnergyByClick) == true)
        {
            var rotation = _ball.transform.rotation.eulerAngles.z;
            if (rotation >= _circlePoints.FirstPointValue && rotation <= _circlePoints.SecondPointValue)
            {
                _ballStep *= -1;
                _ballSpeed += 5;
                _circlePoints.CreatePoints();
            
                ComboPointAdd?.Invoke();
            }
            else
            {
                EndOreCollecting();
            }
        }
        else
        {
            EndOreCollecting();
        }
        
    }

    private void EndOreCollecting()
    {
        ComboEnded?.Invoke();
        _ballSpeed = 10;
        _isCircleActivated = false;
        _collectOreDisplay.CollectOreButtonActive(false);
        _collectOreDisplay.CollectOreInterfaceActive(false);
            
        _circlePoints.SetDistanceBetweenPoints(80);
         
        //_playerAnimator.SetBool("IsCollectingWood", false);
    }
}
