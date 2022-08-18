using System.Collections.Generic;
using UnityEngine;

public class CraftPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> _buttons;

    public void TurnOffButtons()
    {
        foreach (var button in _buttons)
        {
            button.SetActive(false);
        }
    }
}
