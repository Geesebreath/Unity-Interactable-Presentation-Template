using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Easy way to set actions on a toggle or to set up objects with 2 different states.
/// </summary>
public class ToggleAction : MonoBehaviour, CommonInterface
{
    [SerializeField]
    // State object starts in and resets to
    private bool defaultState;
    private bool currentState;
	public bool CurrentState { get => currentState; }
    public UnityEvent activeActions, inactiveActions;

	// Start is called before the first frame update
	void Start()
    {
        currentState = defaultState;
    }

    void ActivateActions(bool state)
    {
		if (state)
		{
            if(activeActions != null)
			    activeActions.Invoke();
		}
		else
		{
            if(inactiveActions != null)  
                inactiveActions.Invoke();
		}
	}

   public void SetState(bool state)
    {
        currentState = state;
        ActivateActions(state);
    }

    public void ToggleState()
    {
        currentState = !currentState;
        ActivateActions(currentState);
    }

	public void Reset()
	{
        SetState(defaultState);
	}
}
