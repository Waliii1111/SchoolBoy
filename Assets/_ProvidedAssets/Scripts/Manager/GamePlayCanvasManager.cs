using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GamePlayCanvasManager : MonoBehaviour
{
    public CanvasStates currentState;
    public CanvasStates prevState;
    public CanvasStates defaultState;

    public GameObject[] AllUiPanel;

    private void OnEnable()
    {
        StateChanger(defaultState);
        //AllUiPanel[0].CompareTag("");
    }
    
    public void StateChanger(CanvasStates newStates)
    {
        prevState = currentState;
        currentState = newStates;
        AllUiPanel[(int)prevState].SetActive(false);
        AllUiPanel[(int)currentState].SetActive(false);
    }
}
public enum CanvasStates
{
    Controls,
    Win,
    Lose,
    Pause,
    Objectives,

}